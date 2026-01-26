using Microsoft.AspNetCore.Mvc;
using MingYue.Services;

namespace MingYue.Controllers;

[IgnoreAntiforgeryToken]
[ApiController]
[Route("api/files")]
public class FileUploadController : ControllerBase
{
    private readonly IFileManagementService _fileService;
    private readonly ILogger<FileUploadController> _logger;

    public FileUploadController(
        IFileManagementService fileService,
        ILogger<FileUploadController> logger)
    {
        _fileService = fileService;
        _logger = logger;
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

            // Get chunk information from dropzone
            var chunkIndex = Request.Form["dzchunkindex"].ToString();
            var totalChunks = Request.Form["dztotalchunkcount"].ToString();
            var chunkByteOffset = Request.Form["dzchunkbyteoffset"].ToString();
            
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

            var fullPath = Path.Combine(path, fileName);
            
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
                
                // Create temp directory for chunks
                var tempDir = Path.Combine(Path.GetTempPath(), "mingyue-chunks", Path.GetRandomFileName());
                Directory.CreateDirectory(tempDir);
                
                var chunkPath = Path.Combine(tempDir, $"{fileName}.part{chunkIdx}");
                
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
                                var partPath = Path.Combine(tempDir, $"{fileName}.part{i}");
                                if (!System.IO.File.Exists(partPath))
                                {
                                    throw new InvalidOperationException($"Missing chunk {i} of {totalCh}");
                                }
                            }
                            
                            // Combine all chunks in order
                            for (int i = 0; i < totalCh; i++)
                            {
                                var partPath = Path.Combine(tempDir, $"{fileName}.part{i}");
                                using (var partStream = System.IO.File.OpenRead(partPath))
                                {
                                    await partStream.CopyToAsync(finalStream);
                                }
                            }
                            
                            finalStream.Position = 0;
                            await _fileService.UploadFileStreamAsync(path, fileName, finalStream, finalStream.Length);
                        }
                        
                        // Clean up chunks
                        Directory.Delete(tempDir, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error combining chunks for file: {FileName}", fileName);
                        
                        // Clean up on error
                        if (Directory.Exists(tempDir))
                        {
                            Directory.Delete(tempDir, true);
                        }
                        
                        throw;
                    }
                }
                
                return Ok(new { success = true, chunkIndex = chunkIdx, totalChunks = totalCh });
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
}
