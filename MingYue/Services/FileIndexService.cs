using MingYue.Models;
using System.Text.Json;

namespace MingYue.Services
{
    public class FileIndexService : IFileIndexService
    {
        private readonly ILogger<FileIndexService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _cacheDirectory;

        public FileIndexService(ILogger<FileIndexService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            
            // Get cache directory from configuration, default to .mingyue-cache in user's home
            var configuredCache = _configuration["Storage:CacheDirectory"];
            if (string.IsNullOrWhiteSpace(configuredCache))
            {
                configuredCache = ".mingyue-cache";
            }
            var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            _cacheDirectory = Path.Combine(homeDir, configuredCache);
            
            // Ensure base cache directory exists - fail fast if there are permission issues
            try
            {
                if (!Directory.Exists(_cacheDirectory))
                {
                    Directory.CreateDirectory(_cacheDirectory);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create cache directory at {CacheDirectory}", _cacheDirectory);
                throw;
            }
        }

        /// <summary>
        /// Gets the path to the index file for a directory
        /// Index files are stored in a centralized cache directory with a structure based on the directory path hash
        /// </summary>
        private string GetIndexFilePath(string directoryPath)
        {
            // Normalize path to ensure consistent hashing
            var normalizedPath = Path.GetFullPath(directoryPath);
            
            // Compute hash of the full directory path to create a unique identifier
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(normalizedPath));
            var pathHash = Convert.ToHexString(hashBytes).ToLowerInvariant();
            
            // Use first 2 characters of hash for subdirectory to avoid too many files in one directory
            var subDir = pathHash.Length >= 2 ? pathHash.Substring(0, 2) : pathHash;
            var indexDir = Path.Combine(_cacheDirectory, "indexes", subDir);
            
            // Ensure directory exists
            if (!Directory.Exists(indexDir))
            {
                Directory.CreateDirectory(indexDir);
            }
            
            var indexFileName = $"{pathHash}.json";
            return Path.Combine(indexDir, indexFileName);
        }

        /// <summary>
        /// Loads the index from file
        /// </summary>
        private async Task<DirectoryIndex?> LoadIndexAsync(string directoryPath)
        {
            try
            {
                var indexPath = GetIndexFilePath(directoryPath);
                if (!File.Exists(indexPath))
                {
                    return null;
                }

                var json = await File.ReadAllTextAsync(indexPath);
                return JsonSerializer.Deserialize<DirectoryIndex>(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading index from {DirectoryPath}", directoryPath);
                return null;
            }
        }

        /// <summary>
        /// Saves the index to file
        /// </summary>
        private async Task SaveIndexAsync(string directoryPath, DirectoryIndex index)
        {
            try
            {
                var indexPath = GetIndexFilePath(directoryPath);
                var json = JsonSerializer.Serialize(index, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
                await File.WriteAllTextAsync(indexPath, json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving index to {DirectoryPath}", directoryPath);
            }
        }

        public async Task IndexFileAsync(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    _logger.LogWarning("File not found for indexing: {FilePath}", filePath);
                    return;
                }

                var fileInfo = new FileInfo(filePath);
                var directory = fileInfo.DirectoryName;
                
                if (string.IsNullOrEmpty(directory))
                {
                    return;
                }

                // Load or create index
                var index = await LoadIndexAsync(directory) ?? new DirectoryIndex
                {
                    DirectoryPath = directory,
                    IndexedAt = DateTime.UtcNow
                };

                // Update or add file entry
                var fileName = fileInfo.Name;
                var existingEntry = index.Files.FirstOrDefault(f => f.FileName == fileName);
                
                var fileEntry = new FileIndexEntry
                {
                    FileName = fileName,
                    FileSize = fileInfo.Length,
                    ModifiedAt = fileInfo.LastWriteTimeUtc,
                    FileType = Path.GetExtension(filePath),
                    IndexedAt = DateTime.UtcNow
                };

                if (existingEntry != null)
                {
                    index.Files.Remove(existingEntry);
                }
                index.Files.Add(fileEntry);
                index.IndexedAt = DateTime.UtcNow;

                await SaveIndexAsync(directory, index);
                _logger.LogDebug("File indexed: {FilePath}", filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error indexing file {FilePath}", filePath);
            }
        }

        public async Task IndexDirectoryAsync(string directoryPath, bool recursive = false)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    _logger.LogWarning("Directory not found for indexing: {DirectoryPath}", directoryPath);
                    return;
                }

                var index = new DirectoryIndex
                {
                    DirectoryPath = directoryPath,
                    IndexedAt = DateTime.UtcNow
                };

                // Always use TopDirectoryOnly - we'll recurse manually if needed
                var files = Directory.GetFiles(directoryPath, "*", SearchOption.TopDirectoryOnly)
                    .ToList();

                foreach (var file in files)
                {
                    try
                    {
                        var fileInfo = new FileInfo(file);
                        
                        index.Files.Add(new FileIndexEntry
                        {
                            FileName = fileInfo.Name,
                            FileSize = fileInfo.Length,
                            ModifiedAt = fileInfo.LastWriteTimeUtc,
                            FileType = Path.GetExtension(file),
                            IndexedAt = DateTime.UtcNow
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Error indexing file {FilePath}", file);
                    }
                }

                await SaveIndexAsync(directoryPath, index);

                // If recursive, index subdirectories
                if (recursive)
                {
                    var subdirs = Directory.GetDirectories(directoryPath)
                        .ToList();
                    
                    foreach (var subdir in subdirs)
                    {
                        await IndexDirectoryAsync(subdir, true);
                    }
                }

                _logger.LogInformation("Directory indexed: {DirectoryPath}, Files: {Count}, Recursive: {Recursive}", 
                    directoryPath, index.Files.Count, recursive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error indexing directory {DirectoryPath}", directoryPath);
            }
        }

        public async Task<List<FileIndex>> SearchFilesAsync(string searchPattern)
        {
            return await SearchFilesInDirectoryAsync(null, searchPattern);
        }

        /// <summary>
        /// Search files in directory hierarchy, checking parent directories if needed
        /// </summary>
        public async Task<List<FileIndex>> SearchFilesInDirectoryAsync(string? directoryPath, string searchPattern)
        {
            try
            {
                if (string.IsNullOrEmpty(directoryPath))
                {
                    // No directory specified, can't search
                    return new List<FileIndex>();
                }

                var results = new List<FileIndex>();
                var currentDir = directoryPath;

                // Search up the directory tree
                while (!string.IsNullOrEmpty(currentDir))
                {
                    var index = await LoadIndexAsync(currentDir);
                    if (index != null)
                    {
                        // Found an index, search it
                        var matchingFiles = SearchInIndex(index, searchPattern, currentDir);
                        results.AddRange(matchingFiles);
                        
                        // Stop searching parent directories once we found an index
                        break;
                    }

                    // Move to parent directory
                    var parent = Directory.GetParent(currentDir);
                    currentDir = parent?.FullName;
                }

                return results.Take(100).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching files with pattern {SearchPattern} in {DirectoryPath}", 
                    searchPattern, directoryPath);
                return new List<FileIndex>();
            }
        }

        private List<FileIndex> SearchInIndex(DirectoryIndex index, string searchPattern, string basePath)
        {
            var results = new List<FileIndex>();
            
            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                // Return all files if no pattern
                foreach (var entry in index.Files.Take(100))
                {
                    results.Add(ConvertToFileIndex(entry, basePath));
                }
                return results;
            }

            // Simple wildcard matching: escape input first, then reintroduce wildcard semantics
            var escapedPattern = System.Text.RegularExpressions.Regex.Escape(searchPattern);
            var pattern = escapedPattern.Replace(@"\*", ".*").Replace(@"\?", ".");
            var regex = new System.Text.RegularExpressions.Regex(
                pattern, 
                System.Text.RegularExpressions.RegexOptions.IgnoreCase,
                System.TimeSpan.FromSeconds(1));

            var matchingFiles = index.Files.Where(entry => regex.IsMatch(entry.FileName));
            
            foreach (var entry in matchingFiles)
            {
                results.Add(ConvertToFileIndex(entry, basePath));
            }

            return results;
        }

        private FileIndex ConvertToFileIndex(FileIndexEntry entry, string basePath)
        {
            return new FileIndex
            {
                FilePath = Path.Combine(basePath, entry.FileName),
                FileName = entry.FileName,
                FileSize = entry.FileSize,
                ModifiedAt = entry.ModifiedAt,
                FileType = entry.FileType,
                IndexedAt = entry.IndexedAt
            };
        }

        /// <summary>
        /// Check if an index exists for the directory or any parent directory
        /// </summary>
        public async Task<bool> HasIndexAsync(string directoryPath)
        {
            try
            {
                var currentDir = directoryPath;
                while (!string.IsNullOrEmpty(currentDir))
                {
                    var indexPath = GetIndexFilePath(currentDir);
                    if (File.Exists(indexPath))
                    {
                        return true;
                    }

                    var parent = Directory.GetParent(currentDir);
                    currentDir = parent?.FullName;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task RemoveFromIndexAsync(string filePath)
        {
            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (string.IsNullOrEmpty(directory))
                {
                    return;
                }

                var index = await LoadIndexAsync(directory);
                if (index == null)
                {
                    return;
                }

                var fileName = Path.GetFileName(filePath);
                var entry = index.Files.FirstOrDefault(f => f.FileName == fileName);
                
                if (entry != null)
                {
                    index.Files.Remove(entry);
                    index.IndexedAt = DateTime.UtcNow;
                    await SaveIndexAsync(directory, index);
                    _logger.LogInformation("File removed from index: {FilePath}", filePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing file from index {FilePath}", filePath);
            }
        }

        public async Task ClearIndexAsync()
        {
            // This method is not applicable for file-based indexing
            // Each directory has its own index file
            _logger.LogInformation("ClearIndexAsync not applicable for file-based indexing");
            await Task.CompletedTask;
        }
    }

    /// <summary>
    /// Represents a directory index stored in a file
    /// </summary>
    public class DirectoryIndex
    {
        public string DirectoryPath { get; set; } = string.Empty;
        public DateTime IndexedAt { get; set; }
        public List<FileIndexEntry> Files { get; set; } = new();
    }

    /// <summary>
    /// Represents a file entry in the directory index
    /// </summary>
    public class FileIndexEntry
    {
        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string FileType { get; set; } = string.Empty;
        public DateTime IndexedAt { get; set; }
    }
}
