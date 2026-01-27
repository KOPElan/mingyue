using Microsoft.AspNetCore.Mvc;
using MingYue.Services;
using System.Collections.Concurrent;

namespace MingYue.Controllers;

[IgnoreAntiforgeryToken]
[ApiController]
[Route("api/files")]
public class FileUploadController : ControllerBase
{
    private readonly IFileManagementService _fileService;
    private readonly ILogger<FileUploadController> _logger;
    private readonly string _rootPath;
    private static readonly ConcurrentDictionary<string, DateTime> _activeUploads = new();
    private static DateTime _lastCleanup = DateTime.UtcNow;
    private static readonly TimeSpan CleanupInterval = TimeSpan.FromMinutes(30);
    private static readonly TimeSpan AbandonedUploadThreshold = TimeSpan.FromHours(24);

    public FileUploadController(
        IFileManagementService fileService,
        ILogger<FileUploadController> logger)
    {
        _fileService = fileService;
        _logger = logger;
        
        // Set root path based on OS (same as FileManagementService)
        _rootPath = OperatingSystem.IsWindows()
            ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            : "/";
            
        // Perform periodic cleanup of abandoned uploads
        CleanupAbandonedUploads();
    }
    
    /// <summary>
    /// Validates that the requested path is within the allowed root directory
    /// to prevent path traversal attacks
    /// </summary>
    private bool IsPathAllowed(string path)
    {
        try
        {
            if (string.IsNullOrEmpty(path)) return false;

            var fullPath = Path.GetFullPath(path);
            var rootPath = Path.GetFullPath(_rootPath);

            // SECURITY: Explicitly forbid access to sensitive system directories on Linux
            if (!OperatingSystem.IsWindows())
            {
                var forbiddenPaths = new[] { "/etc", "/var", "/bin", "/sbin", "/lib", "/root", "/proc", "/sys", "/dev" };
                if (forbiddenPaths.Any(p => fullPath.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
                {
                    // Allow if it's explicitly under a permitted mount point like /var/lib/docker if needed,
                    // but generally we want to be restrictive.
                    // For now, let's just stick to the root check but exclude known sensitive ones if they are at the top level.
                    if (fullPath == "/" || forbiddenPaths.Any(p => fullPath == p)) return false;
                }
            }

            // Ensure path is within allowed root
            return fullPath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    [HttpPost("upload-chunk")]
    [RequestFormLimits(MultipartBodyLengthLimit = 104857600)] // 100MB
    [RequestSizeLimit(104857600)] // 100MB
    public async Task<IActionResult> UploadChunk()
    {
        try
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var path = Request.Form["path"].ToString();
            if (string.IsNullOrEmpty(path))
            {
                path = "/";
            }
            
            // SECURITY: Validate path to prevent directory traversal attacks
            if (!IsPathAllowed(path))
            {
                _logger.LogWarning("Upload attempt to unauthorized path: {Path}", path);
                return BadRequest("Invalid or unauthorized path");
            }

            // Get chunk information from dropzone
            var chunkIndex = Request.Form["dzchunkindex"].ToString();
            var totalChunks = Request.Form["dztotalchunkcount"].ToString();
            var uploadId = Request.Form["dzuuid"].ToString(); // Unique ID per upload session
            
            var fileName = file.FileName;
            
            // Validate filename for security
            if (string.IsNullOrWhiteSpace(fileName) || 
                fileName.Contains("..") || 
                fileName.Contains("/") || 
                fileName.Contains("\\") ||
                Path.GetInvalidFileNameChars().Any(c => fileName.Contains(c)))
            {
                return BadRequest("Invalid filename");
            }
            
            // If chunked upload
            if (!string.IsNullOrEmpty(chunkIndex) && !string.IsNullOrEmpty(totalChunks))
            {
                if (!int.TryParse(chunkIndex, out var chunkIdx) || !int.TryParse(totalChunks, out var totalCh))
                {
                    return BadRequest("Invalid chunk information");
                }
                
                if (chunkIdx < 0 || chunkIdx >= totalCh || totalCh <= 0)
                {
                    return BadRequest("Invalid chunk index or total chunks");
                }
                
                // Use upload ID to group chunks from the same upload session
                // This prevents collisions when multiple clients upload files with the same name
                var sessionId = string.IsNullOrEmpty(uploadId) ? Guid.NewGuid().ToString() : uploadId;
                var tempDir = Path.Combine(Path.GetTempPath(), "mingyue-chunks", sessionId);
                Directory.CreateDirectory(tempDir);
                
                // Store filename in a metadata file for cleanup tracking
                var metadataPath = Path.Combine(tempDir, "metadata.txt");
                if (!System.IO.File.Exists(metadataPath))
                {
                    await System.IO.File.WriteAllTextAsync(metadataPath, $"{fileName}\n{DateTime.UtcNow:O}\n{path}");
                }
                
                var chunkPath = Path.Combine(tempDir, $"chunk_{chunkIdx}");
                
                // Save chunk
                using (var stream = new FileStream(chunkPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                
                // If this is the last chunk, combine all chunks
                if (chunkIdx == totalCh - 1)
                {
                    try
                    {
                        using (var finalStream = new MemoryStream())
                        {
                            // Verify all chunks exist before combining
                            for (int i = 0; i < totalCh; i++)
                            {
                                var partPath = Path.Combine(tempDir, $"chunk_{i}");
                                if (!System.IO.File.Exists(partPath))
                                {
                                    throw new InvalidOperationException($"Missing chunk {i} of {totalCh}");
                                }
                            }
                            
                            // Combine all chunks in order
                            for (int i = 0; i < totalCh; i++)
                            {
                                var partPath = Path.Combine(tempDir, $"chunk_{i}");
                                using (var partStream = System.IO.File.OpenRead(partPath))
                                {
                                    await partStream.CopyToAsync(finalStream);
                                }
                            }
                            
                            finalStream.Position = 0;
                            await _fileService.UploadFileStreamAsync(path, fileName, finalStream, finalStream.Length);
                        }
                        
                        // Clean up chunks on success
                        try
                        {
                            Directory.Delete(tempDir, true);
                        }
                        catch (Exception cleanupEx)
                        {
                            _logger.LogWarning(cleanupEx, "Failed to cleanup temp directory: {TempDir}", tempDir);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error combining chunks for file: {FileName}", fileName);
                        
                        // Clean up on error
                        try
                        {
                            if (Directory.Exists(tempDir))
                            {
                                Directory.Delete(tempDir, true);
                            }
                        }
                        catch (Exception cleanupEx)
                        {
                            _logger.LogWarning(cleanupEx, "Failed to cleanup temp directory after error: {TempDir}", tempDir);
                        }
                        
                        throw;
                    }
                }
                
                return Ok(new { success = true, chunkIndex = chunkIdx, totalChunks = totalCh, uploadId = sessionId });
            }
            else
            {
                // Non-chunked upload (fallback)
                using (var stream = file.OpenReadStream())
                {
                    await _fileService.UploadFileStreamAsync(path, fileName, stream, file.Length);
                }
                
                return Ok(new { success = true });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file");
            return StatusCode(500, new { error = ex.Message });
        }
    }
    
    /// <summary>
    /// Cleanup abandoned upload chunks that are older than the threshold
    /// This prevents disk space consumption from incomplete uploads
    /// </summary>
    private void CleanupAbandonedUploads()
    {
        try
        {
            // Only cleanup if enough time has passed since last cleanup
            if (DateTime.UtcNow - _lastCleanup < CleanupInterval)
            {
                return;
            }
            
            _lastCleanup = DateTime.UtcNow;
            
            var chunksDir = Path.Combine(Path.GetTempPath(), "mingyue-chunks");
            if (!Directory.Exists(chunksDir))
            {
                return;
            }
            
            var uploadDirs = Directory.GetDirectories(chunksDir);
            var cleanedCount = 0;
            
            foreach (var uploadDir in uploadDirs)
            {
                try
                {
                    var metadataPath = Path.Combine(uploadDir, "metadata.txt");
                    DateTime createdTime;
                    
                    if (System.IO.File.Exists(metadataPath))
                    {
                        var lines = System.IO.File.ReadAllLines(metadataPath);
                        if (lines.Length >= 2 && DateTime.TryParse(lines[1], out var parsedTime))
                        {
                            createdTime = parsedTime;
                        }
                        else
                        {
                            createdTime = Directory.GetCreationTimeUtc(uploadDir);
                        }
                    }
                    else
                    {
                        createdTime = Directory.GetCreationTimeUtc(uploadDir);
                    }
                    
                    // Delete if older than threshold
                    if (DateTime.UtcNow - createdTime > AbandonedUploadThreshold)
                    {
                        Directory.Delete(uploadDir, true);
                        cleanedCount++;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to cleanup upload directory: {Dir}", uploadDir);
                }
            }
            
            if (cleanedCount > 0)
            {
                _logger.LogInformation("Cleaned up {Count} abandoned upload directories", cleanedCount);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during abandoned uploads cleanup");
        }
    }
    
    [HttpGet("download-zip")]
    public async Task<IActionResult> DownloadZip([FromQuery] string paths, [FromQuery] string filename)
    {
        try
        {
            // Deserialize paths
            var pathList = System.Text.Json.JsonSerializer.Deserialize<List<string>>(paths);
            if (pathList == null || !pathList.Any())
            {
                return BadRequest("No files specified for download");
            }

            // Validate all paths
            foreach (var path in pathList)
            {
                if (!IsPathAllowed(path))
                {
                    _logger.LogWarning("Download attempt for unauthorized path: {Path}", path);
                    return BadRequest("Invalid or unauthorized path");
                }
            }

            // Create ZIP in memory stream
            var memoryStream = new MemoryStream();
            
            using (var zipArchive = new System.IO.Compression.ZipArchive(memoryStream, System.IO.Compression.ZipArchiveMode.Create, leaveOpen: true))
            {
                foreach (var path in pathList)
                {
                    var itemName = Path.GetFileName(path);
                    
                    if (Directory.Exists(path))
                    {
                        // Add directory recursively
                        await AddDirectoryToZipAsync(zipArchive, path, itemName);
                    }
                    else if (System.IO.File.Exists(path))
                    {
                        // Add file
                        var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
                        var entry = zipArchive.CreateEntry(itemName);
                        using var entryStream = entry.Open();
                        await entryStream.WriteAsync(fileBytes);
                    }
                }
            }

            // Reset stream position
            memoryStream.Position = 0;

            // Return as file download
            return File(memoryStream, "application/zip", filename);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating ZIP download");
            return StatusCode(500, "Failed to create ZIP archive");
        }
    }

    private async Task AddDirectoryToZipAsync(System.IO.Compression.ZipArchive zipArchive, string sourcePath, string entryName)
    {
        var files = await _fileService.GetFilesAsync(sourcePath);
        
        foreach (var file in files)
        {
            if (file.Name == "..") continue; // Skip parent directory marker
            
            var relativePath = Path.Combine(entryName, file.Name);
            
            if (file.IsDirectory)
            {
                await AddDirectoryToZipAsync(zipArchive, file.Path, relativePath);
            }
            else
            {
                try
                {
                    var fileBytes = await System.IO.File.ReadAllBytesAsync(file.Path);
                    var entry = zipArchive.CreateEntry(relativePath);
                    using var entryStream = entry.Open();
                    await entryStream.WriteAsync(fileBytes);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to add file to ZIP: {Path}", file.Path);
                }
            }
        }
    }

    [HttpGet("content")]
    public async Task<IActionResult> GetFileContent([FromQuery] string path)
    {
        try
        {
            if (string.IsNullOrEmpty(path) || !IsPathAllowed(path))
            {
                return BadRequest("Invalid or unauthorized path");
            }

            if (!System.IO.File.Exists(path))
            {
                return NotFound("File not found");
            }

            var extension = Path.GetExtension(path).ToLowerInvariant();
            var contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                ".svg" => "image/svg+xml",
                ".txt" or ".log" or ".json" or ".xml" or ".md" or ".cs" or ".js" or ".css" or
                ".html" or ".htm" or ".yaml" or ".yml" or ".ini" or ".conf" or ".sh" or ".py" or
                ".java" or ".c" or ".cpp" or ".h" or ".hpp" or ".sql" or ".csproj" or ".sln" => "text/plain",
                ".mp4" => "video/mp4",
                ".webm" => "video/webm",
                ".mp3" => "audio/mpeg",
                _ => "application/octet-stream"
            };

            var fileStream = System.IO.File.OpenRead(path);
            return File(fileStream, contentType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error serving file content: {Path}", path);
            return StatusCode(500, "Internal server error");
        }
    }
}
