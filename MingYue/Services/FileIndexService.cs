using MingYue.Models;
using MingYue.Utilities;
using System.Text.Json;

namespace MingYue.Services
{
    public class FileIndexService : IFileIndexService
    {
        private readonly ILogger<FileIndexService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _cacheDirectory;
        private const int MaxSearchResults = 100;
        
        // Common system and development directories to exclude from indexing
        private static readonly HashSet<string> ExcludedDirectoryNames = new(StringComparer.OrdinalIgnoreCase)
        {
            "node_modules", ".git", ".svn", ".hg", "bin", "obj", 
            ".vs", ".vscode", ".idea", "packages", "__pycache__",
            ".next", ".nuxt", "dist", "build", "target"
        };

        public FileIndexService(ILogger<FileIndexService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            
            // Use helper to get cache directory
            _cacheDirectory = PathHelper.GetCacheDirectory(configuration);
            
            // Ensure base cache directory exists - fail fast if there are permission issues
            try
            {
                PathHelper.EnsureDirectoryExists(_cacheDirectory);
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
                
                // Ensure directory exists before writing
                var indexDir = Path.GetDirectoryName(indexPath);
                if (!string.IsNullOrEmpty(indexDir) && !Directory.Exists(indexDir))
                {
                    Directory.CreateDirectory(indexDir);
                }
                
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
                        
                        // Skip hidden and system files
                        if ((fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ||
                            (fileInfo.Attributes & FileAttributes.System) == FileAttributes.System)
                        {
                            continue;
                        }
                        
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
                        var dirInfo = new DirectoryInfo(subdir);
                        
                        // Skip system/hidden directories and common development directories
                        if (ShouldExcludeDirectory(dirInfo))
                        {
                            _logger.LogDebug("Skipping excluded directory: {Directory}", subdir);
                            continue;
                        }
                        
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
            try
            {
                var results = new List<FileIndex>();
                var indexCacheDir = Path.Combine(_cacheDirectory, "indexes");
                
                // If no indexes exist yet, return empty
                if (!Directory.Exists(indexCacheDir))
                {
                    return results;
                }
                
                // Search through all index files in the cache
                var indexFiles = Directory.GetFiles(indexCacheDir, "*.json", SearchOption.AllDirectories);
                
                foreach (var indexFile in indexFiles)
                {
                    try
                    {
                        var json = await File.ReadAllTextAsync(indexFile);
                        var index = JsonSerializer.Deserialize<DirectoryIndex>(json);
                        
                        if (index != null)
                        {
                            var matchingFiles = SearchInIndex(index, searchPattern, index.DirectoryPath);
                            results.AddRange(matchingFiles);
                            
                            // Early exit after adding results if we have enough
                            if (results.Count >= MaxSearchResults)
                            {
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Error loading index file {IndexFile}", indexFile);
                    }
                }
                
                return results.Take(MaxSearchResults).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching files with pattern {SearchPattern}", searchPattern);
                return new List<FileIndex>();
            }
        }

        /// <summary>
        /// Search files in a specific directory's index
        /// </summary>
        public async Task<List<FileIndex>> SearchFilesInDirectoryAsync(string? directoryPath, string searchPattern)
        {
            try
            {
                if (string.IsNullOrEmpty(directoryPath))
                {
                    // No directory specified, search all indexes
                    return await SearchFilesAsync(searchPattern);
                }

                var results = new List<FileIndex>();
                
                // Load the index for this specific directory from centralized cache
                var index = await LoadIndexAsync(directoryPath);
                if (index != null)
                {
                    var matchingFiles = SearchInIndex(index, searchPattern, index.DirectoryPath);
                    results.AddRange(matchingFiles);
                }

                return results.Take(MaxSearchResults).ToList();
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
                foreach (var entry in index.Files.Take(MaxSearchResults))
                {
                    results.Add(ConvertToFileIndex(entry, basePath));
                }
                return results;
            }

            // Use CreateSearchRegex for consistent fuzzy/wildcard matching behavior
            var regex = CreateSearchRegex(searchPattern);

            // Limit results to avoid unnecessary conversions when there are many matches
            var matchingFiles = index.Files.Where(entry => regex.IsMatch(entry.FileName)).Take(MaxSearchResults);
            
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

        /// <summary>
        /// Advanced search that recursively searches through indexed directories.
        /// Searches all subdirectory indexes starting from the specified directory.
        /// </summary>
        /// <param name="startDirectory">The directory to start searching from</param>
        /// <param name="searchPattern">The search pattern with wildcard support (* and ?)</param>
        /// <param name="includeSubdirectories">Whether to search subdirectories recursively</param>
        /// <param name="includeDirectories">Whether to include directories in the results</param>
        /// <returns>List of matching files and/or directories with full paths</returns>
        public async Task<List<FileIndex>> AdvancedSearchAsync(string startDirectory, string searchPattern, bool includeSubdirectories = true, bool includeDirectories = true)
        {
            var results = new List<FileIndex>();
            
            try
            {
                if (!Directory.Exists(startDirectory))
                {
                    _logger.LogWarning("Start directory not found for advanced search: {StartDirectory}", startDirectory);
                    return results;
                }

                // Normalize the start directory path
                var normalizedStart = Path.GetFullPath(startDirectory);
                
                // Search the starting directory's index
                var startIndex = await LoadIndexAsync(normalizedStart);
                if (startIndex != null)
                {
                    var matchingFiles = SearchInIndex(startIndex, searchPattern, normalizedStart);
                    results.AddRange(matchingFiles);
                }
                
                // If recursive, search all subdirectory indexes
                if (includeSubdirectories)
                {
                    await SearchIndexesRecursive(normalizedStart, searchPattern, results);
                }
                
                // If including directories, search for matching directory names
                if (includeDirectories)
                {
                    var regex = CreateSearchRegex(searchPattern);
                    SearchDirectoriesRecursive(normalizedStart, regex, results, includeSubdirectories);
                }
                
                _logger.LogInformation("Advanced search completed: {Count} results for pattern '{Pattern}' in {Directory}", 
                    results.Count, searchPattern, startDirectory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during advanced search in {StartDirectory} with pattern {SearchPattern}", 
                    startDirectory, searchPattern);
            }
            
            return results.Take(MaxSearchResults).ToList();
        }

        /// <summary>
        /// Recursively searches through subdirectory indexes
        /// </summary>
        private async Task SearchIndexesRecursive(string directoryPath, string searchPattern, List<FileIndex> results)
        {
            if (results.Count >= MaxSearchResults)
            {
                return;
            }

            try
            {
                var subdirs = Directory.GetDirectories(directoryPath);
                
                foreach (var subdir in subdirs)
                {
                    if (results.Count >= MaxSearchResults)
                        break;
                    
                    // Skip system/hidden directories
                    var dirInfo = new DirectoryInfo(subdir);
                    if (ShouldExcludeDirectory(dirInfo))
                        continue;
                    
                    // Load and search this subdirectory's index
                    var index = await LoadIndexAsync(subdir);
                    if (index != null)
                    {
                        var matchingFiles = SearchInIndex(index, searchPattern, subdir);
                        results.AddRange(matchingFiles);
                    }
                    
                    // Recurse into this subdirectory
                    await SearchIndexesRecursive(subdir, searchPattern, results);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Skip directories we don't have permission to access
                _logger.LogDebug("Access denied to directory: {Directory}", directoryPath);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error searching subdirectories of: {Directory}", directoryPath);
            }
        }

        /// <summary>
        /// Recursively searches for directories matching the pattern
        /// </summary>
        private void SearchDirectoriesRecursive(string directoryPath, System.Text.RegularExpressions.Regex regex, List<FileIndex> results, bool includeSubdirectories)
        {
            if (results.Count >= MaxSearchResults)
            {
                return;
            }

            try
            {
                var subdirs = Directory.GetDirectories(directoryPath);
                
                foreach (var subdir in subdirs)
                {
                    if (results.Count >= MaxSearchResults)
                        break;
                    
                    var dirInfo = new DirectoryInfo(subdir);
                    
                    // Skip system/hidden directories
                    if (ShouldExcludeDirectory(dirInfo))
                        continue;
                    
                    // Check if directory name matches pattern
                    if (regex.IsMatch(dirInfo.Name))
                    {
                        results.Add(new FileIndex
                        {
                            FilePath = dirInfo.FullName,
                            FileName = dirInfo.Name,
                            FileSize = 0,
                            ModifiedAt = dirInfo.LastWriteTimeUtc,
                            FileType = "directory",
                            IndexedAt = DateTime.UtcNow
                        });
                    }
                    
                    // Recurse if requested
                    if (includeSubdirectories)
                    {
                        SearchDirectoriesRecursive(subdir, regex, results, includeSubdirectories);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogDebug("Access denied to directory: {Directory}", directoryPath);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error searching directories in: {Directory}", directoryPath);
            }
        }

        /// <summary>
        /// Determines if a directory should be excluded from indexing/searching
        /// </summary>
        private bool ShouldExcludeDirectory(DirectoryInfo dirInfo)
        {
            // Exclude hidden and system directories
            if ((dirInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                return true;
            
            if ((dirInfo.Attributes & FileAttributes.System) == FileAttributes.System)
                return true;
            
            // Exclude the cache directory itself to prevent self-referential indexing
            // Normalize both paths and ensure proper path separator handling
            var normalizedDirPath = Path.GetFullPath(dirInfo.FullName).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var normalizedCachePath = Path.GetFullPath(_cacheDirectory).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            
            if (normalizedDirPath.Equals(normalizedCachePath, StringComparison.OrdinalIgnoreCase) ||
                normalizedDirPath.StartsWith(normalizedCachePath + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            
            // Exclude common system and development directories
            return ExcludedDirectoryNames.Contains(dirInfo.Name);
        }

        /// <summary>
        /// Creates a regex from a search pattern with wildcard support.
        /// Supports * (matches any characters) and ? (matches single character).
        /// If pattern doesn't contain wildcards, performs fuzzy matching (contains).
        /// </summary>
        /// <param name="searchPattern">The search pattern with optional wildcards</param>
        /// <returns>Compiled regex with case-insensitive matching and 1-second timeout</returns>
        private System.Text.RegularExpressions.Regex CreateSearchRegex(string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(searchPattern) || searchPattern == "*")
            {
                // Match everything
                return new System.Text.RegularExpressions.Regex(
                    "^.*$", 
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase,
                    System.TimeSpan.FromSeconds(1));
            }
            
            // Check if pattern contains wildcards
            bool hasWildcards = searchPattern.Contains('*') || searchPattern.Contains('?');
            
            // Escape special regex characters except * and ?
            var escapedPattern = System.Text.RegularExpressions.Regex.Escape(searchPattern);
            var pattern = escapedPattern.Replace(@"\*", ".*").Replace(@"\?", ".");
            
            // If no wildcards, use fuzzy matching (contains) instead of exact match
            if (!hasWildcards)
            {
                pattern = $".*{pattern}.*";
            }
            
            return new System.Text.RegularExpressions.Regex(
                $"^{pattern}$", 
                System.Text.RegularExpressions.RegexOptions.IgnoreCase,
                System.TimeSpan.FromSeconds(1));
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
