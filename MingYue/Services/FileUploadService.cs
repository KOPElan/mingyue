namespace MingYue.Services
{
    /// <summary>
    /// Provides file upload services with support for single and batch file uploads.
    /// </summary>
    public class FileUploadService : IFileUploadService
    {
        private readonly ILogger<FileUploadService> _logger;
        private readonly IFileManagementService _fileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadService"/> class.
        /// </summary>
        /// <param name="logger">The logger for recording file upload operations and errors.</param>
        /// <param name="fileService">The file management service for file operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public FileUploadService(ILogger<FileUploadService> logger, IFileManagementService fileService)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(fileService);

            _logger = logger;
            _fileService = fileService;
        }

        public async Task<bool> UploadFileAsync(string targetPath, Stream fileStream, string fileName)
        {
            try
            {
                // Sanitize filename to prevent path traversal
                var sanitizedFileName = SanitizeFileName(fileName);
                if (string.IsNullOrEmpty(sanitizedFileName))
                {
                    _logger.LogWarning("Invalid filename rejected: {FileName}", fileName);
                    return false;
                }

                var fullPath = Path.Combine(targetPath, sanitizedFileName);
                
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

        private static string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return string.Empty;

            // Remove path separators and traversal attempts
            if (fileName.Contains("..") || fileName.Contains("/") || fileName.Contains("\\"))
                return string.Empty;

            // Remove invalid characters
            var invalidChars = Path.GetInvalidFileNameChars();
            if (invalidChars.Any(c => fileName.Contains(c)))
                return string.Empty;

            return fileName;
        }
    }
}
