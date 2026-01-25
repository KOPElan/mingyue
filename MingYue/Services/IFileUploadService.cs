namespace MingYue.Services
{
    public interface IFileUploadService
    {
        Task<bool> UploadFileAsync(string targetPath, Stream fileStream, string fileName);
        Task<List<string>> UploadFilesAsync(string targetPath, IEnumerable<(Stream stream, string fileName)> files);
        Task<bool> DeleteUploadedFileAsync(string filePath);
    }
}
