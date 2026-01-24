namespace MingYue.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly ILogger<FileUploadService> _logger;
        private readonly IFileManagementService _fileService;

        public FileUploadService(ILogger<FileUploadService> logger, IFileManagementService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        public async Task<bool> UploadFileAsync(string targetPath, Stream fileStream, string fileName)
        {
            try
            {
                var fullPath = Path.Combine(targetPath, fileName);
                
                // Ensure target directory exists
                var directory = Path.GetDirectoryName(fullPath);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Write file
                await using var fileStreamOut = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                await fileStream.CopyToAsync(fileStreamOut);
                
                _logger.LogInformation("File uploaded successfully: {FullPath}", fullPath);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file {FileName} to {TargetPath}", fileName, targetPath);
                return false;
            }
        }

        public async Task<List<string>> UploadFilesAsync(string targetPath, IEnumerable<(Stream stream, string fileName)> files)
        {
            var uploadedFiles = new List<string>();

            foreach (var (stream, fileName) in files)
            {
                if (await UploadFileAsync(targetPath, stream, fileName))
                {
                    uploadedFiles.Add(fileName);
                }
            }

            return uploadedFiles;
        }

        public async Task<bool> DeleteUploadedFileAsync(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    _logger.LogInformation("Uploaded file deleted: {FilePath}", filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting uploaded file: {FilePath}", filePath);
                return false;
            }
        }
    }
}
