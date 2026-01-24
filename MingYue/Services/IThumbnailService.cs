namespace MingYue.Services
{
    public interface IThumbnailService
    {
        Task<byte[]?> GetThumbnailAsync(string filePath);
        Task<byte[]?> GenerateThumbnailAsync(string filePath, int width = 200, int height = 200);
        Task ClearThumbnailCacheAsync(string? filePath = null);
    }
}
