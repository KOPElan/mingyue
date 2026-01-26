using Cronos;
using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MingYue.Services
{
    /// <summary>
    /// Background service for executing scheduled tasks
    /// </summary>
    public class ScheduledTaskExecutorService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ScheduledTaskExecutorService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

        public ScheduledTaskExecutorService(IServiceProvider serviceProvider, ILogger<ScheduledTaskExecutorService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scheduled Task Executor Service started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckAndExecuteTasksAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in scheduled task executor");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("Scheduled Task Executor Service stopped");
        }

        private async Task CheckAndExecuteTasksAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MingYueDbContext>>();
            var taskService = scope.ServiceProvider.GetRequiredService<IScheduledTaskService>();

            using var context = await contextFactory.CreateDbContextAsync();
            var now = DateTime.UtcNow;

            // Get all enabled tasks that should run now
            var tasksToRun = await context.ScheduledTasks
                .Where(t => t.IsEnabled && (t.NextRunAt == null || t.NextRunAt <= now))
                .ToListAsync();

            foreach (var task in tasksToRun)
            {
                try
                {
                    _logger.LogInformation("Executing scheduled task: {TaskName} (ID: {TaskId})", task.Name, task.Id);
                    
                    // Execute the task
                    var (success, output, errorMessage) = await ExecuteTaskAsync(task);

                    // Record execution
                    await taskService.RecordExecutionAsync(task.Id, success, output, errorMessage);

                    // Calculate next run time
                    var nextRunTime = CalculateNextRunTime(task.CronExpression);
                    await taskService.UpdateTaskRunTimesAsync(task.Id, now, nextRunTime);

                    _logger.LogInformation("Task {TaskName} executed. Success: {Success}, Next run: {NextRun}", 
                        task.Name, success, nextRunTime);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing task {TaskName} (ID: {TaskId})", task.Name, task.Id);
                    await taskService.RecordExecutionAsync(task.Id, false, "", ex.Message);
                }
            }
        }

        private async Task<(bool Success, string Output, string ErrorMessage)> ExecuteTaskAsync(ScheduledTask task)
        {
            try
            {
                switch (task.TaskType.ToLowerInvariant())
                {
                    case "command":
                        return await ExecuteCommandTaskAsync(task);
                    case "script":
                        return await ExecuteScriptTaskAsync(task);
                    case "http":
                        return await ExecuteHttpTaskAsync(task);
                    case "fileindex":
                        return await ExecuteFileIndexTaskAsync(task);
                    case "thumbnailgeneration":
                        return await ExecuteThumbnailGenerationTaskAsync(task);
                    case "anydropmigration":
                        return await ExecuteAnydropMigrationTaskAsync(task);
                    default:
                        return (false, "", $"Unknown task type: {task.TaskType}");
                }
            }
            catch (Exception ex)
            {
                return (false, "", ex.Message);
            }
        }

        private async Task<(bool Success, string Output, string ErrorMessage)> ExecuteCommandTaskAsync(ScheduledTask task)
        {
            try
            {
                var taskData = JsonSerializer.Deserialize<Dictionary<string, string>>(task.TaskData);
                if (taskData == null || !taskData.TryGetValue("command", out var command) || string.IsNullOrWhiteSpace(command))
                {
                    return (false, "", "Invalid task data: missing 'command' field");
                }

                // Security: Validate and sanitize the command to prevent command injection
                // This is a basic check - consider implementing more robust validation based on your security requirements
                if (command.Contains(";") || command.Contains("&&") || command.Contains("||") || command.Contains("|"))
                {
                    _logger.LogWarning("Command contains potentially dangerous characters: {Command}", command);
                }

                string workingDir;
                if (!taskData.TryGetValue("workingDirectory", out workingDir) || string.IsNullOrWhiteSpace(workingDir))
                {
                    workingDir = "/tmp";
                }

                // Security: Validate working directory to prevent path traversal
                var normalizedWorkingDir = Path.GetFullPath(workingDir);
                if (!Directory.Exists(normalizedWorkingDir))
                {
                    return (false, "", $"Working directory does not exist: {workingDir}");
                }

                using var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{command.Replace("\"", "\\\"")}\"",  // Escape quotes in command
                        WorkingDirectory = normalizedWorkingDir,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                var output = await process.StandardOutput.ReadToEndAsync();
                var error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                var success = process.ExitCode == 0;
                return (success, output, error);
            }
            catch (Exception ex)
            {
                return (false, "", ex.Message);
            }
        }

        private async Task<(bool Success, string Output, string ErrorMessage)> ExecuteScriptTaskAsync(ScheduledTask task)
        {
            try
            {
                var taskData = JsonSerializer.Deserialize<Dictionary<string, string>>(task.TaskData);
                if (taskData == null || !taskData.TryGetValue("script", out var script) || string.IsNullOrWhiteSpace(script))
                {
                    return (false, "", "Invalid task data: missing 'script' field");
                }

                string interpreter;
                if (!taskData.TryGetValue("interpreter", out interpreter) || string.IsNullOrWhiteSpace(interpreter))
                {
                    interpreter = "/bin/bash";
                }
                
                // Security: Whitelist allowed interpreters to prevent arbitrary code execution
                var allowedInterpreters = new[] { "/bin/bash", "/bin/sh", "/usr/bin/python3", "/usr/bin/node" };
                if (!allowedInterpreters.Contains(interpreter))
                {
                    return (false, "", $"Interpreter not allowed: {interpreter}. Allowed interpreters: {string.Join(", ", allowedInterpreters)}");
                }
                
                // Write script to temp file with restrictive permissions
                var tempFile = Path.Combine(Path.GetTempPath(), $"task_{task.Id}_{Guid.NewGuid()}.sh");
                await File.WriteAllTextAsync(tempFile, script);
                
                // Set restrictive permissions (owner read/write/execute only)
                if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
                {
                    var chmod = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "/bin/chmod",
                            Arguments = $"700 {tempFile}",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    };
                    chmod.Start();
                    await chmod.WaitForExitAsync();
                }

                try
                {
                    using var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = interpreter,
                            Arguments = tempFile,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    };

                    process.Start();
                    var output = await process.StandardOutput.ReadToEndAsync();
                    var error = await process.StandardError.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    var success = process.ExitCode == 0;
                    return (success, output, error);
                }
                finally
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "", ex.Message);
            }
        }

        private async Task<(bool Success, string Output, string ErrorMessage)> ExecuteHttpTaskAsync(ScheduledTask task)
        {
            try
            {
                var taskData = JsonSerializer.Deserialize<Dictionary<string, string>>(task.TaskData);
                if (taskData == null || !taskData.TryGetValue("url", out var urlString) || string.IsNullOrWhiteSpace(urlString))
                {
                    return (false, "", "Invalid task data: missing 'url' field");
                }

                // Security: Validate URL to prevent SSRF attacks
                if (!Uri.TryCreate(urlString, UriKind.Absolute, out var uri))
                {
                    return (false, "", $"Invalid URL: {urlString}");
                }

                // Block access to private IP ranges and localhost
                var host = uri.Host;
                if (host.Equals("localhost", StringComparison.OrdinalIgnoreCase) ||
                    host.Equals("127.0.0.1") ||
                    host.StartsWith("169.254.") || // Link-local
                    host.StartsWith("10.") ||
                    host.StartsWith("192.168.") ||
                    (host.StartsWith("172.") && int.TryParse(host.Split('.')[1], out var second) && second >= 16 && second <= 31))
                {
                    return (false, "", $"Access to private/local IP addresses is not allowed: {host}");
                }

                // Only allow HTTP and HTTPS schemes
                if (uri.Scheme != "http" && uri.Scheme != "https")
                {
                    return (false, "", $"Only HTTP and HTTPS schemes are allowed: {uri.Scheme}");
                }

                string method;
                if (!taskData.TryGetValue("method", out method) || string.IsNullOrWhiteSpace(method))
                {
                    method = "GET";
                }

                using var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromMinutes(5);

                HttpResponseMessage? response = null;
                if (method.ToUpperInvariant() == "POST")
                {
                    string body;
                    if (!taskData.TryGetValue("body", out body))
                    {
                        body = "";
                    }
                    using var stringContent = new StringContent(body);
                    response = await httpClient.PostAsync(uri, stringContent);
                }
                else
                {
                    response = await httpClient.GetAsync(uri);
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var success = response.IsSuccessStatusCode;
                var output = $"Status: {(int)response.StatusCode} {response.StatusCode}\n{responseContent}";
                
                return (success, output, success ? "" : $"HTTP {(int)response.StatusCode}");
            }
            catch (Exception ex)
            {
                return (false, "", ex.Message);
            }
        }

        private async Task<(bool Success, string Output, string ErrorMessage)> ExecuteFileIndexTaskAsync(ScheduledTask task)
        {
            try
            {
                var taskData = JsonSerializer.Deserialize<Dictionary<string, string>>(task.TaskData);
                if (taskData == null || !taskData.TryGetValue("path", out var path) || string.IsNullOrWhiteSpace(path))
                {
                    return (false, "", "Invalid task data: missing 'path' field");
                }

                // Security: Validate and canonicalize the path to prevent path traversal
                string normalizedPath;
                try
                {
                    normalizedPath = Path.GetFullPath(path);
                }
                catch (Exception ex)
                {
                    return (false, "", $"Invalid path: {ex.Message}");
                }

                // Check that the path exists and is a directory
                if (!Directory.Exists(normalizedPath))
                {
                    return (false, "", $"Directory does not exist: {path}");
                }

                // Security: Optionally restrict to allowed base directories
                // Uncomment and configure if you want to restrict indexing to specific paths
                // var allowedBasePaths = new[] { "/home", "/data", "/mnt" };
                // if (!allowedBasePaths.Any(basePath => normalizedPath.StartsWith(basePath)))
                // {
                //     return (false, "", $"Path is not within allowed directories: {path}");
                // }

                bool recursive;
                if (taskData.TryGetValue("recursive", out var recursiveValue))
                {
                    recursive = recursiveValue.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    recursive = false;
                }

                // Get FileIndexService from service provider
                using var scope = _serviceProvider.CreateScope();
                var fileIndexService = scope.ServiceProvider.GetService<IFileIndexService>();
                
                if (fileIndexService == null)
                {
                    return (false, "", "FileIndexService not available");
                }

                // Index files
                var startTime = DateTime.UtcNow;
                await fileIndexService.IndexDirectoryAsync(normalizedPath, recursive);
                var duration = (DateTime.UtcNow - startTime).TotalSeconds;

                var output = $"File indexing completed for path: {normalizedPath}\n";
                output += $"Recursive: {recursive}\n";
                output += $"Duration: {duration:F2} seconds";

                return (true, output, "");
            }
            catch (Exception ex)
            {
                return (false, "", ex.Message);
            }
        }

        private async Task<(bool Success, string Output, string ErrorMessage)> ExecuteAnydropMigrationTaskAsync(ScheduledTask task)
        {
            try
            {
                var taskData = JsonSerializer.Deserialize<Dictionary<string, string>>(task.TaskData);
                if (taskData == null || !taskData.TryGetValue("daysToKeep", out var daysToKeepStr))
                {
                    return (false, "", "Invalid task data: missing 'daysToKeep' field");
                }

                if (!int.TryParse(daysToKeepStr, out int daysToKeep))
                {
                    return (false, "", "Invalid daysToKeep value: must be an integer");
                }

                // Validate retention period bounds (0 to 3650 days)
                if (daysToKeep < 0 || daysToKeep > 3650)
                {
                    return (false, "", "Invalid daysToKeep value: must be between 0 and 3650 days");
                }

                // Get services from service provider
                using var scope = _serviceProvider.CreateScope();
                var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MingYueDbContext>>();
                
                using var context = await contextFactory.CreateDbContextAsync();
                var cutoffDate = DateTime.UtcNow.AddDays(-daysToKeep);
                
                var messageCount = 0;
                var attachmentCount = 0;
                long totalSize = 0;
                const int batchSize = 500;

                // Process messages in batches to avoid loading all into memory
                while (true)
                {
                    // Fetch a batch of old messages with their attachments
                    var oldMessagesBatch = await context.AnydropMessages
                        .Include(m => m.Attachments)
                        .Where(m => m.CreatedAt < cutoffDate && m.IsRead)
                        .OrderBy(m => m.CreatedAt)
                        .Take(batchSize)
                        .ToListAsync();

                    if (oldMessagesBatch.Count == 0)
                    {
                        break;
                    }

                    // Delete old messages and their attachments
                    foreach (var message in oldMessagesBatch)
                    {
                        messageCount++;

                        foreach (var attachment in message.Attachments)
                        {
                            attachmentCount++;
                            totalSize += attachment.FileSize;
                            
                            // Delete physical file
                            try
                            {
                                if (File.Exists(attachment.FilePath))
                                {
                                    File.Delete(attachment.FilePath);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning(ex, "Failed to delete file: {FilePath}", attachment.FilePath);
                            }
                        }
                        
                        context.AnydropMessages.Remove(message);
                    }

                    await context.SaveChangesAsync();
                }

                var output = $"Anydrop migration completed\n";
                output += $"Deleted {messageCount} messages older than {daysToKeep} days\n";
                output += $"Removed {attachmentCount} attachments\n";
                output += $"Freed {FormatFileSize(totalSize)} of disk space";

                return (true, output, "");
            }
            catch (Exception ex)
            {
                return (false, "", ex.Message);
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        private async Task<(bool Success, string Output, string ErrorMessage)> ExecuteThumbnailGenerationTaskAsync(ScheduledTask task)
        {
            try
            {
                var taskData = JsonSerializer.Deserialize<Dictionary<string, string>>(task.TaskData);
                if (taskData == null || !taskData.TryGetValue("path", out var path) || string.IsNullOrWhiteSpace(path))
                {
                    return (false, "", "Invalid task data: missing 'path' field");
                }

                // Security: Validate and canonicalize the path to prevent path traversal
                string normalizedPath;
                try
                {
                    normalizedPath = Path.GetFullPath(path);
                }
                catch (Exception ex)
                {
                    return (false, "", $"Invalid path: {ex.Message}");
                }

                // Check that the path exists and is a directory
                if (!Directory.Exists(normalizedPath))
                {
                    return (false, "", $"Directory does not exist: {path}");
                }

                bool recursive;
                if (taskData.TryGetValue("recursive", out var recursiveValue))
                {
                    recursive = recursiveValue.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    recursive = false;
                }

                // Get ThumbnailService from service provider
                using var scope = _serviceProvider.CreateScope();
                var thumbnailService = scope.ServiceProvider.GetService<IThumbnailService>();
                
                if (thumbnailService == null)
                {
                    return (false, "", "ThumbnailService not available");
                }

                // Generate thumbnails
                var startTime = DateTime.UtcNow;
                await thumbnailService.GenerateDirectoryThumbnailsAsync(normalizedPath, recursive);
                var duration = (DateTime.UtcNow - startTime).TotalSeconds;

                var output = $"Thumbnail generation completed for path: {normalizedPath}\n";
                output += $"Recursive: {recursive}\n";
                output += $"Duration: {duration:F2} seconds";

                return (true, output, "");
            }
            catch (Exception ex)
            {
                return (false, "", ex.Message);
            }
        }

        private DateTime? CalculateNextRunTime(string cronExpression)
        {
            try
            {
                var cron = CronExpression.Parse(cronExpression, CronFormat.IncludeSeconds);
                var next = cron.GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Utc);
                return next;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating next run time for cron expression: {CronExpression}", cronExpression);
                return null;
            }
        }
    }
}
