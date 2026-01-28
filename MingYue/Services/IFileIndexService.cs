using MingYue.Models;

namespace MingYue.Services
{
    public interface IFileIndexService
    {
        Task IndexFileAsync(string filePath);
        Task IndexDirectoryAsync(string directoryPath, bool recursive = false);
        Task<List<FileIndex>> SearchFilesAsync(string searchPattern);
        Task<List<FileIndex>> SearchFilesInDirectoryAsync(string? directoryPath, string searchPattern);
        Task<List<FileIndex>> AdvancedSearchAsync(string startDirectory, string searchPattern, bool includeSubdirectories = true, bool includeDirectories = true);
        Task<bool> HasIndexAsync(string directoryPath);
        Task RemoveFromIndexAsync(string filePath);
        Task ClearIndexAsync();
    }
}
