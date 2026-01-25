using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace MingYue.Services
{
    public class ThumbnailService : IThumbnailService
    {
        private readonly ILogger<ThumbnailService> _logger;
        private readonly IDbContextFactory<MingYueDbContext> _dbContextFactory;
        private readonly HashSet<string> _supportedImageExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"
        };

        public ThumbnailService(ILogger<ThumbnailService> logger, IDbContextFactory<MingYueDbContext> dbContextFactory)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
        }

        public async Task<byte[]?> GetThumbnailAsync(string filePath)
        {
            try
            {
                // Check cache first
                await using var context = await _dbContextFactory.CreateDbContextAsync();
                var cached = await context.Thumbnails
                    .Where(t => t.FilePath == filePath)
                    .FirstOrDefaultAsync();

                if (cached != null)
                {
                    return cached.ThumbnailData;
                }

                // Generate if not cached
                return await GenerateThumbnailAsync(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting thumbnail for {FilePath}", filePath);
                return null;
            }
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
                if (!_supportedImageExtensions.Contains(extension))
                {
                    _logger.LogDebug("File type not supported for thumbnail: {Extension}", extension);
                    return null;
                }

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

                // Cache the thumbnail
                await using var context = await _dbContextFactory.CreateDbContextAsync();
                
                // Check if already exists
                var existing = await context.Thumbnails
                    .Where(t => t.FilePath == filePath)
                    .FirstOrDefaultAsync();

                if (existing != null)
                {
                    existing.ThumbnailData = thumbnailData;
                    existing.CreatedAt = DateTime.UtcNow;
                }
                else
                {
                    context.Thumbnails.Add(new Thumbnail
                    {
                        FilePath = filePath,
                        ThumbnailData = thumbnailData
                    });
                }

                await context.SaveChangesAsync();
                _logger.LogInformation("Thumbnail generated and cached for {FilePath}", filePath);

                return thumbnailData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating thumbnail for {FilePath}", filePath);
                return null;
            }
        }

        public async Task ClearThumbnailCacheAsync(string? filePath = null)
        {
            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                if (string.IsNullOrEmpty(filePath))
                {
                    // Clear all thumbnails
                    await context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Thumbnails");
                    _logger.LogInformation("All thumbnail cache cleared");
                }
                else
                {
                    // Clear specific thumbnail
                    var thumbnail = await context.Thumbnails
                        .Where(t => t.FilePath == filePath)
                        .FirstOrDefaultAsync();

                    if (thumbnail != null)
                    {
                        context.Thumbnails.Remove(thumbnail);
                        await context.SaveChangesAsync();
                        _logger.LogInformation("Thumbnail cache cleared for {FilePath}", filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing thumbnail cache for {FilePath}", filePath ?? "all");
            }
        }
    }
}
