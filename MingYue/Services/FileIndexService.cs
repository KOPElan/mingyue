using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;

namespace MingYue.Services
{
    public class FileIndexService : IFileIndexService
    {
        private readonly ILogger<FileIndexService> _logger;
        private readonly IDbContextFactory<MingYueDbContext> _dbContextFactory;

        public FileIndexService(ILogger<FileIndexService> logger, IDbContextFactory<MingYueDbContext> dbContextFactory)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
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
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                var existing = await context.FileIndexes
                    .Where(f => f.FilePath == filePath)
                    .FirstOrDefaultAsync();

                if (existing != null)
                {
                    // Update existing index
                    existing.FileName = fileInfo.Name;
                    existing.FileSize = fileInfo.Length;
                    existing.ModifiedAt = fileInfo.LastWriteTimeUtc;
                    existing.FileType = Path.GetExtension(filePath);
                    existing.IndexedAt = DateTime.UtcNow;
                }
                else
                {
                    // Add new index
                    context.FileIndexes.Add(new FileIndex
                    {
                        FilePath = filePath,
                        FileName = fileInfo.Name,
                        FileSize = fileInfo.Length,
                        ModifiedAt = fileInfo.LastWriteTimeUtc,
                        FileType = Path.GetExtension(filePath)
                    });
                }

                await context.SaveChangesAsync();
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

                var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                var files = Directory.GetFiles(directoryPath, "*", searchOption);

                foreach (var file in files)
                {
                    await IndexFileAsync(file);
                }

                _logger.LogInformation("Directory indexed: {DirectoryPath}, Files: {Count}, Recursive: {Recursive}", 
                    directoryPath, files.Length, recursive);
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
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                if (string.IsNullOrWhiteSpace(searchPattern))
                {
                    return await context.FileIndexes
                        .OrderByDescending(f => f.IndexedAt)
                        .Take(100)
                        .ToListAsync();
                }

                // Simple wildcard search
                var pattern = searchPattern.Replace("*", "%").Replace("?", "_");

                return await context.FileIndexes
                    .Where(f => EF.Functions.Like(f.FileName, $"%{pattern}%"))
                    .OrderByDescending(f => f.IndexedAt)
                    .Take(100)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching files with pattern {SearchPattern}", searchPattern);
                return new List<FileIndex>();
            }
        }

        public async Task RemoveFromIndexAsync(string filePath)
        {
            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                var fileIndex = await context.FileIndexes
                    .Where(f => f.FilePath == filePath)
                    .FirstOrDefaultAsync();

                if (fileIndex != null)
                {
                    context.FileIndexes.Remove(fileIndex);
                    await context.SaveChangesAsync();
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
            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();
                await context.Database.ExecuteSqlRawAsync("DELETE FROM FileIndexes");
                _logger.LogInformation("File index cleared");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing file index");
            }
        }
    }
}
