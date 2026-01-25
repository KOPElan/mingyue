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
                if (taskData == null || !taskData.ContainsKey("command"))
                {
                    return (false, "", "Invalid task data: missing 'command' field");
                }

                var command = taskData["command"];
                var workingDir = taskData.ContainsKey("workingDirectory") ? taskData["workingDirectory"] : "/tmp";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{command}\"",
                        WorkingDirectory = workingDir,
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
                if (taskData == null || !taskData.ContainsKey("script"))
                {
                    return (false, "", "Invalid task data: missing 'script' field");
                }

                var script = taskData["script"];
                var interpreter = taskData.ContainsKey("interpreter") ? taskData["interpreter"] : "/bin/bash";
                
                // Write script to temp file
                var tempFile = Path.Combine(Path.GetTempPath(), $"task_{task.Id}_{Guid.NewGuid()}.sh");
                await File.WriteAllTextAsync(tempFile, script);

                try
                {
                    var process = new Process
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
                if (taskData == null || !taskData.ContainsKey("url"))
                {
                    return (false, "", "Invalid task data: missing 'url' field");
                }

                var url = taskData["url"];
                var method = taskData.ContainsKey("method") ? taskData["method"] : "GET";

                using var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromMinutes(5);

                HttpResponseMessage? response = null;
                if (method.ToUpperInvariant() == "POST")
                {
                    var body = taskData.ContainsKey("body") ? taskData["body"] : "";
                    response = await httpClient.PostAsync(url, new StringContent(body));
                }
                else
                {
                    response = await httpClient.GetAsync(url);
                }

                var content = await response.Content.ReadAsStringAsync();
                var success = response.IsSuccessStatusCode;
                var output = $"Status: {(int)response.StatusCode} {response.StatusCode}\n{content}";
                
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
                if (taskData == null || !taskData.ContainsKey("path"))
                {
                    return (false, "", "Invalid task data: missing 'path' field");
                }

                var path = taskData["path"];
                var recursive = taskData.ContainsKey("recursive") && taskData["recursive"].ToLowerInvariant() == "true";

                // Get FileIndexService from service provider
                using var scope = _serviceProvider.CreateScope();
                var fileIndexService = scope.ServiceProvider.GetService<IFileIndexService>();
                
                if (fileIndexService == null)
                {
                    return (false, "", "FileIndexService not available");
                }

                // Index files
                var startTime = DateTime.UtcNow;
                await fileIndexService.IndexDirectoryAsync(path, recursive);
                var duration = (DateTime.UtcNow - startTime).TotalSeconds;

                var output = $"File indexing completed for path: {path}\n";
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
                if (taskData == null || !taskData.ContainsKey("daysToKeep"))
                {
                    return (false, "", "Invalid task data: missing 'daysToKeep' field");
                }

                if (!int.TryParse(taskData["daysToKeep"], out int daysToKeep))
                {
                    return (false, "", "Invalid daysToKeep value: must be an integer");
                }

                // Get services from service provider
                using var scope = _serviceProvider.CreateScope();
                var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MingYueDbContext>>();
                
                using var context = await contextFactory.CreateDbContextAsync();
                var cutoffDate = DateTime.UtcNow.AddDays(-daysToKeep);
                
                // Find old messages
                var oldMessages = await context.AnydropMessages
                    .Include(m => m.Attachments)
                    .Where(m => m.CreatedAt < cutoffDate && m.IsRead)
                    .ToListAsync();

                var messageCount = oldMessages.Count;
                var attachmentCount = 0;
                long totalSize = 0;

                // Delete old messages and their attachments
                foreach (var message in oldMessages)
                {
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
