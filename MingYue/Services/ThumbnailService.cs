using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Security.Cryptography;
using System.Text;

namespace MingYue.Services
{
    public class ThumbnailService : IThumbnailService
    {
        private readonly ILogger<ThumbnailService> _logger;
        private readonly IDbContextFactory<MingYueDbContext> _dbContextFactory;
        private readonly IConfiguration _configuration;
        private readonly string _cacheDirectory;
        private readonly HashSet<string> _supportedImageExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"
        };
        private readonly HashSet<string> _supportedVideoExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".mp4", ".avi", ".mov", ".mkv", ".wmv", ".flv", ".webm", ".m4v"
        };

        public ThumbnailService(ILogger<ThumbnailService> logger, IDbContextFactory<MingYueDbContext> dbContextFactory, IConfiguration configuration)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
            _configuration = configuration;
            
            // Get cache directory from environment variable first, then configuration, then default
            var cacheDir = Environment.GetEnvironmentVariable("MINGYUE_CACHE_DIR");
            if (string.IsNullOrWhiteSpace(cacheDir))
            {
                cacheDir = _configuration["Storage:CacheDirectory"];
            }
            
            // If still not set, use a default path
            if (string.IsNullOrWhiteSpace(cacheDir))
            {
                // Default to /srv/mingyue/cache on Linux, or .mingyue-cache in user profile on Windows
                if (OperatingSystem.IsLinux())
                {
                    cacheDir = "/srv/mingyue/cache";
                }
                else
                {
                    var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    cacheDir = Path.Combine(homeDir, ".mingyue-cache");
                }
            }
            
            _cacheDirectory = cacheDir;
            
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

        public async Task<byte[]?> GetThumbnailAsync(string filePath)
        {
            try
            {
                // Try file-based cache first
                var thumbnailPath = GetThumbnailPath(filePath);
                if (File.Exists(thumbnailPath))
                {
                    return await File.ReadAllBytesAsync(thumbnailPath);
                }

                // Don't generate thumbnail on demand - return null if not exists
                // This prevents UI blocking on first load
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting thumbnail for {FilePath}", filePath);
                return null;
            }
        }

        /// <summary>
        /// Gets the path where the thumbnail should be stored
        /// Thumbnails are stored in a centralized cache directory with a structure based on the file path hash
        /// </summary>
        private string GetThumbnailPath(string filePath)
        {
            // Normalize path to ensure consistent hashing
            var normalizedPath = Path.GetFullPath(filePath);
            
            // Compute hash of the full file path to create a unique identifier
            var pathHash = ComputeFileHash(normalizedPath);
            
            // Use first 2 characters of hash for subdirectory to avoid too many files in one directory
            var subDir = pathHash.Length >= 2 ? pathHash.Substring(0, 2) : pathHash;
            var thumbnailDir = Path.Combine(_cacheDirectory, "thumbnails", subDir);
            
            // Ensure directory exists
            if (!Directory.Exists(thumbnailDir))
            {
                Directory.CreateDirectory(thumbnailDir);
            }
            
            var thumbnailFileName = $"{pathHash}.jpg";
            return Path.Combine(thumbnailDir, thumbnailFileName);
        }

        /// <summary>
        /// Computes a hash for the file path to create a unique thumbnail identifier
        /// </summary>
        private string ComputeFileHash(string filePath)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(filePath));
            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }

        public async Task<byte[]?> GenerateThumbnailAsync(string filePath, int width = 200, int height = 200)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    _logger.LogWarning("File not found for thumbnail generation: {FilePath}", filePath);
                    return null;
                }

                var extension = Path.GetExtension(filePath);
                
                // Check if it's a supported image
                if (_supportedImageExtensions.Contains(extension))
                {
                    return await GenerateImageThumbnailAsync(filePath, width, height);
                }
                // Check if it's a supported video
                else if (_supportedVideoExtensions.Contains(extension))
                {
                    return await GenerateVideoThumbnailAsync(filePath, width, height);
                }
                
                _logger.LogDebug("File type not supported for thumbnail: {Extension}", extension);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating thumbnail for {FilePath}", filePath);
                return null;
            }
        }

        private async Task<byte[]?> GenerateImageThumbnailAsync(string filePath, int width, int height)
        {
            try
            {
                // Check if file is too large for thumbnail generation (e.g., > 50MB)
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 50 * 1024 * 1024) // 50MB limit
                {
                    _logger.LogWarning("File too large for thumbnail generation: {FilePath} ({Size} bytes)", filePath, fileInfo.Length);
                    return null;
                }

                // Generate thumbnail
                byte[] thumbnailData;
                using (var image = await Image.LoadAsync(filePath))
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(width, height),
                        Mode = ResizeMode.Max
                    }));

                    using var ms = new MemoryStream();
                    await image.SaveAsync(ms, new JpegEncoder { Quality = 80 });
                    thumbnailData = ms.ToArray();
                }

                // Save to file-based cache
                await SaveThumbnailToFileAsync(filePath, thumbnailData);
                
                _logger.LogInformation("Image thumbnail generated and cached for {FilePath}", filePath);
                return thumbnailData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating image thumbnail for {FilePath}", filePath);
                return null;
            }
        }

        private async Task<byte[]?> GenerateVideoThumbnailAsync(string filePath, int width, int height)
        {
            try
            {
                // Video thumbnail generation requires FFmpeg integration
                // See issue for video thumbnail support
                _logger.LogDebug("Video thumbnail generation not yet implemented for {FilePath}", filePath);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating video thumbnail for {FilePath}", filePath);
                return null;
            }
        }

        private async Task SaveThumbnailToFileAsync(string filePath, byte[] thumbnailData)
        {
            var thumbnailPath = GetThumbnailPath(filePath);
            await File.WriteAllBytesAsync(thumbnailPath, thumbnailData);
        }

        public async Task ClearThumbnailCacheAsync(string? filePath = null)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    // Can't clear all thumbnails easily with file-based storage
                    _logger.LogInformation("Clear all thumbnails not supported with file-based storage");
                    return;
                }
                
                // Clear specific thumbnail
                var thumbnailPath = GetThumbnailPath(filePath);
                if (File.Exists(thumbnailPath))
                {
                    File.Delete(thumbnailPath);
                    _logger.LogInformation("Thumbnail cache cleared for {FilePath}", filePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing thumbnail cache for {FilePath}", filePath ?? "all");
            }
        }

        /// <summary>
        /// Generate thumbnails for all images in a directory
        /// </summary>
        public async Task GenerateDirectoryThumbnailsAsync(string directoryPath, bool recursive = false)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    _logger.LogWarning("Directory not found for thumbnail generation: {DirectoryPath}", directoryPath);
                    return;
                }

                var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                var allExtensions = _supportedImageExtensions.Concat(_supportedVideoExtensions).ToList();
                
                var files = Directory.GetFiles(directoryPath, "*.*", searchOption)
                    .Where(f => allExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                    .ToList();

                _logger.LogInformation("Generating thumbnails for {Count} files in {DirectoryPath}", files.Count, directoryPath);

                foreach (var file in files)
                {
                    var thumbnailPath = GetThumbnailPath(file);
                    
                    // Skip if thumbnail already exists and is newer than source file
                    if (File.Exists(thumbnailPath))
                    {
                        var sourceModified = File.GetLastWriteTimeUtc(file);
                        var thumbnailModified = File.GetLastWriteTimeUtc(thumbnailPath);
                        
                        if (thumbnailModified >= sourceModified)
                        {
                            continue; // Thumbnail is up to date
                        }
                    }

                    await GenerateThumbnailAsync(file);
                }

                _logger.LogInformation("Thumbnail generation complete for {DirectoryPath}", directoryPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating thumbnails for directory {DirectoryPath}", directoryPath);
            }
        }
    }
}
