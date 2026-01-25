using MingYue.Models;

namespace MingYue.Services
{
    public interface IFileIndexService
    {
        Task IndexFileAsync(string filePath);
        Task IndexDirectoryAsync(string directoryPath, bool recursive = false);
        Task<List<FileIndex>> SearchFilesAsync(string searchPattern);
        Task RemoveFromIndexAsync(string filePath);
        Task ClearIndexAsync();
    }
}
