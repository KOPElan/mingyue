using MingYue.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MingYue.Services
{
    /// <summary>
    /// Provides Docker container and image management services including listing, starting, stopping, and removing containers.
    /// </summary>
    public class DockerManagementService : IDockerManagementService
    {
        private readonly ILogger<DockerManagementService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DockerManagementService"/> class.
        /// </summary>
        /// <param name="logger">The logger for recording Docker operations and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when logger is null.</exception>
        public DockerManagementService(ILogger<DockerManagementService> logger)
        {
            ArgumentNullException.ThrowIfNull(logger);
            _logger = logger;
        }

        public async Task<bool> IsDockerAvailableAsync()
        {
            try
            {
                var result = await ExecuteDockerCommandAsync("version", "--format", "json");
                return !string.IsNullOrEmpty(result);
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<DockerContainerInfo>> GetContainersAsync(bool showAll = true)
        {
            try
            {
                var args = showAll ? "ps -a --format json" : "ps --format json";
                var output = await ExecuteDockerCommandAsync(args);

                if (string.IsNullOrEmpty(output))
                {
                    return new List<DockerContainerInfo>();
                }

                var containers = new List<DockerContainerInfo>();
                
                // Helper to safely get string properties
                static string GetStringProperty(JsonElement element, string propertyName)
                {
                    if (element.TryGetProperty(propertyName, out var property) &&
                        property.ValueKind == JsonValueKind.String)
                    {
                        return property.GetString() ?? string.Empty;
                    }
                    return string.Empty;
                }

                // Helper to safely get CreatedAt as DateTime from a Unix timestamp
                static DateTime GetCreatedAt(JsonElement element)
                {
                    if (element.TryGetProperty("CreatedAt", out var property) &&
                        property.ValueKind == JsonValueKind.Number &&
                        property.TryGetInt64(out var seconds))
                    {
                        return DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;
                    }
                    return DateTime.MinValue;
                }

                // Docker outputs one JSON object per line
                foreach (var line in output.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                {
                    try
                    {
                        var json = JsonSerializer.Deserialize<JsonElement>(line);
                        var id = GetStringProperty(json, "ID");
                        var name = GetStringProperty(json, "Names");
                        if (!string.IsNullOrEmpty(name) && name.StartsWith('/'))
                        {
                            name = name.TrimStart('/');
                        }

                        containers.Add(new DockerContainerInfo
                        {
                            Id = id,
                            Name = name,
                            Image = GetStringProperty(json, "Image"),
                            Status = GetStringProperty(json, "Status"),
                            State = GetStringProperty(json, "State"),
                            Created = GetCreatedAt(json),
                            Ports = ParsePorts(GetStringProperty(json, "Ports")),
                            Labels = ParseLabels(GetStringProperty(json, "Labels"))
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error parsing container JSON: {Line}", line);
                    }
                }

                return containers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting containers");
                return new List<DockerContainerInfo>();
            }
        }

        public async Task<List<DockerImageInfo>> GetImagesAsync()
        {
            try
            {
                var output = await ExecuteDockerCommandAsync("images", "--format", "json");

                if (string.IsNullOrEmpty(output))
                {
                    return new List<DockerImageInfo>();
                }

                var images = new List<DockerImageInfo>();

                foreach (var line in output.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                {
                    try
                    {
                        var json = JsonSerializer.Deserialize<JsonElement>(line);
                        var repoTag = $"{json.GetProperty("Repository").GetString()}:{json.GetProperty("Tag").GetString()}";
                        
                        images.Add(new DockerImageInfo
                        {
                            Id = json.GetProperty("ID").GetString() ?? string.Empty,
                            RepoTags = new List<string> { repoTag },
                            Size = json.GetProperty("Size").GetInt64(),
                            Created = DateTimeOffset.FromUnixTimeSeconds(json.GetProperty("CreatedAt").GetInt64()).DateTime
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error parsing image JSON: {Line}", line);
                    }
                }

                return images;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting images");
                return new List<DockerImageInfo>();
            }
        }

        public async Task StartContainerAsync(string containerId)
        {
            await ExecuteDockerCommandAsync("start", containerId);
            _logger.LogInformation("Started container: {ContainerId}", containerId);
        }

        public async Task StopContainerAsync(string containerId)
        {
            await ExecuteDockerCommandAsync("stop", containerId);
            _logger.LogInformation("Stopped container: {ContainerId}", containerId);
        }

        public async Task RestartContainerAsync(string containerId)
        {
            await ExecuteDockerCommandAsync("restart", containerId);
            _logger.LogInformation("Restarted container: {ContainerId}", containerId);
        }

        public async Task RemoveContainerAsync(string containerId)
        {
            await ExecuteDockerCommandAsync("rm", "-f", containerId);
            _logger.LogInformation("Removed container: {ContainerId}", containerId);
        }

        public async Task RemoveImageAsync(string imageId)
        {
            await ExecuteDockerCommandAsync("rmi", imageId);
            _logger.LogInformation("Removed image: {ImageId}", imageId);
        }

        public async Task PullImageAsync(string imageName)
        {
            await ExecuteDockerCommandAsync("pull", imageName);
            _logger.LogInformation("Pulled image: {ImageName}", imageName);
        }

        public async Task<string> GetContainerLogsAsync(string containerId, int tailLines = 1000)
        {
            try
            {
                var output = await ExecuteDockerCommandAsync("logs", "--tail", tailLines.ToString(), containerId);
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting container logs for {ContainerId}", containerId);
                return $"Error: {ex.Message}";
            }
        }

        private async Task<string> ExecuteDockerCommandAsync(params string[] args)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "docker",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Use ArgumentList for safer command execution
            foreach (var arg in args)
            {
                psi.ArgumentList.Add(arg);
            }

            using var process = new Process { StartInfo = psi };
            process.Start();

            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0 && !string.IsNullOrEmpty(error))
            {
                throw new Exception($"Docker command failed: {error}");
            }

            return output.Trim();
        }

        private List<string> ParsePorts(string portsString)
        {
            if (string.IsNullOrEmpty(portsString))
                return new List<string>();

            return portsString.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        }

        private Dictionary<string, string> ParseLabels(string labelsString)
        {
            var labels = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(labelsString))
                return labels;

            foreach (var label in labelsString.Split(','))
            {
                var parts = label.Split('=', 2);
                if (parts.Length == 2)
                {
                    labels[parts[0].Trim()] = parts[1].Trim();
                }
            }

            return labels;
        }
    }
}
