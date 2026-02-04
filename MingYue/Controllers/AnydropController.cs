using Microsoft.AspNetCore.Mvc;
using MingYue.Models;
using MingYue.Services;
using System.ComponentModel.DataAnnotations;

namespace MingYue.Controllers;

/// <summary>
/// RESTful API controller for Anydrop message and file sharing operations.
/// Provides endpoints for managing messages, attachments, and search functionality.
/// </summary>
/// <remarks>
/// This controller supports:
/// - CRUD operations for messages
/// - File attachment management
/// - Full-text search with context
/// - Pagination and filtering
/// - Link metadata retrieval
/// - Batch operations
/// </remarks>
/// <example>
/// <code>
/// GET /api/anydrop/messages - List messages with pagination
/// GET /api/anydrop/messages/123 - Get specific message
/// POST /api/anydrop/messages - Create new message
/// DELETE /api/anydrop/messages/123 - Delete message
/// GET /api/anydrop/search?q=keyword - Search messages
/// </code>
/// </example>
[ApiController]
[Route("api/anydrop")]
[Produces("application/json")]
public class AnydropController : ControllerBase
{
    private readonly IAnydropService _anydropService;
    private readonly IFileUploadService _fileUploadService;
    private readonly ILogger<AnydropController> _logger;
    
    // Configuration constants
    private const long MaxUploadSizeBytes = 104857600; // 100MB
    
    /// <summary>
    /// The upload path for Anydrop files. Uses a platform-appropriate default path.
    /// </summary>
    private static readonly string AnydropUploadPath = OperatingSystem.IsWindows()
        ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "mingyue", "anydrop")
        : "/var/lib/mingyue/anydrop";

    /// <summary>
    /// Initializes a new instance of the <see cref="AnydropController"/> class.
    /// </summary>
    /// <param name="anydropService">The Anydrop service for message operations.</param>
    /// <param name="fileUploadService">The file upload service for handling attachments.</param>
    /// <param name="logger">The logger for recording API operations and errors.</param>
    /// <exception cref="ArgumentNullException">Thrown when any required parameter is null.</exception>
    public AnydropController(
        IAnydropService anydropService,
        IFileUploadService fileUploadService,
        ILogger<AnydropController> logger)
    {
        ArgumentNullException.ThrowIfNull(anydropService);
        ArgumentNullException.ThrowIfNull(fileUploadService);
        ArgumentNullException.ThrowIfNull(logger);

        _anydropService = anydropService;
        _fileUploadService = fileUploadService;
        _logger = logger;
    }

    #region Message Endpoints

    /// <summary>
    /// Gets paginated messages with optional filtering.
    /// </summary>
    /// <param name="query">Query parameters for filtering, sorting, and pagination.</param>
    /// <returns>Paginated list of messages.</returns>
    /// <response code="200">Returns the paginated messages.</response>
    [HttpGet("messages")]
    [ProducesResponseType(typeof(PagedResult<AnydropMessage>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<AnydropMessage>>> GetMessages([FromQuery] AnydropMessageQuery query)
    {
        var result = await _anydropService.GetMessagesAsync(query);

        return Ok(result);
    }

    /// <summary>
    /// Gets a specific message by ID.
    /// </summary>
    /// <param name="id">The message ID.</param>
    /// <returns>The message if found.</returns>
    /// <response code="200">Returns the message.</response>
    /// <response code="404">Message not found.</response>
    [HttpGet("messages/{id}")]
    [ProducesResponseType(typeof(AnydropMessage), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AnydropMessage>> GetMessage(int id)
    {
        var message = await _anydropService.GetMessageByIdAsync(id);

        if (message is null)
        {
            return NotFound(new { error = "Message not found" });
        }

        return Ok(message);
    }

    /// <summary>
    /// Gets unread messages.
    /// </summary>
    /// <returns>List of unread messages.</returns>
    /// <response code="200">Returns unread messages.</response>
    [HttpGet("messages/unread")]
    [ProducesResponseType(typeof(List<AnydropMessage>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AnydropMessage>>> GetUnreadMessages()
    {
        var messages = await _anydropService.GetUnreadMessagesAsync();

        return Ok(messages);
    }

    /// <summary>
    /// Creates a new message.
    /// </summary>
    /// <param name="request">The message creation request.</param>
    /// <returns>The created message.</returns>
    /// <response code="201">Message created successfully.</response>
    /// <response code="400">Invalid request data.</response>
    [HttpPost("messages")]
    [ProducesResponseType(typeof(AnydropMessage), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AnydropMessage>> CreateMessage([FromBody] CreateMessageRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Content) && (request.FileNames is null || !request.FileNames.Any()))
        {
            return BadRequest(new { error = "Message must have content or attachments" });
        }

        try
        {
            var deviceId = _anydropService.GetDeviceId();
            var deviceName = _anydropService.GetDeviceName();

            // Use provided device info if available (for API clients)
            if (!string.IsNullOrWhiteSpace(request.DeviceId))
            {
                deviceId = request.DeviceId;
            }

            if (!string.IsNullOrWhiteSpace(request.DeviceName))
            {
                deviceName = request.DeviceName;
            }

            var message = await _anydropService.CreateMessageAsync(
                request.Content ?? string.Empty,
                deviceId,
                deviceName,
                request.MessageType);

            // Parse URLs in content for link metadata
            if (!string.IsNullOrWhiteSpace(request.Content))
            {
                // Fire and forget - don't block the response
                _ = _anydropService.ParseAndSaveLinkMetadataAsync(message.Id, request.Content);
            }

            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating message");

            return BadRequest(new { error = "Failed to create message" });
        }
    }

    /// <summary>
    /// Marks a message as read.
    /// </summary>
    /// <param name="id">The message ID.</param>
    /// <returns>No content on success.</returns>
    /// <response code="204">Message marked as read.</response>
    /// <response code="404">Message not found.</response>
    [HttpPut("messages/{id}/read")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var message = await _anydropService.GetMessageByIdAsync(id);

        if (message is null)
        {
            return NotFound(new { error = "Message not found" });
        }

        await _anydropService.MarkAsReadAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Marks multiple messages as read.
    /// </summary>
    /// <param name="ids">List of message IDs.</param>
    /// <returns>No content on success.</returns>
    /// <response code="204">Messages marked as read.</response>
    /// <response code="400">Invalid request.</response>
    [HttpPut("messages/read")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MarkMultipleAsRead([FromBody] List<int> ids)
    {
        if (ids is null || !ids.Any())
        {
            return BadRequest(new { error = "No message IDs provided" });
        }

        await _anydropService.MarkMultipleAsReadAsync(ids);

        return NoContent();
    }

    /// <summary>
    /// Deletes a message.
    /// </summary>
    /// <param name="id">The message ID.</param>
    /// <returns>No content on success.</returns>
    /// <response code="204">Message deleted.</response>
    /// <response code="404">Message not found.</response>
    [HttpDelete("messages/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMessage(int id)
    {
        var message = await _anydropService.GetMessageByIdAsync(id);

        if (message is null)
        {
            return NotFound(new { error = "Message not found" });
        }

        await _anydropService.DeleteMessageAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Deletes multiple messages.
    /// </summary>
    /// <param name="ids">List of message IDs.</param>
    /// <returns>No content on success.</returns>
    /// <response code="204">Messages deleted.</response>
    /// <response code="400">Invalid request.</response>
    [HttpDelete("messages")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMultipleMessages([FromBody] List<int> ids)
    {
        if (ids is null || !ids.Any())
        {
            return BadRequest(new { error = "No message IDs provided" });
        }

        await _anydropService.DeleteMultipleMessagesAsync(ids);

        return NoContent();
    }

    #endregion

    #region Attachment Endpoints

    /// <summary>
    /// Gets attachments with optional filtering by type and date.
    /// </summary>
    /// <param name="type">Content type prefix filter (e.g., "image/", "video/").</param>
    /// <param name="page">Page number (1-based).</param>
    /// <param name="pageSize">Items per page.</param>
    /// <param name="fromDate">Start date filter.</param>
    /// <param name="toDate">End date filter.</param>
    /// <returns>Paginated list of attachments.</returns>
    /// <response code="200">Returns the paginated attachments.</response>
    [HttpGet("attachments")]
    [ProducesResponseType(typeof(PagedResult<AnydropAttachment>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<AnydropAttachment>>> GetAttachments(
        [FromQuery] string? type = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null)
    {
        var result = await _anydropService.GetAttachmentsByTypeAsync(type, page, pageSize, fromDate, toDate);

        return Ok(result);
    }

    /// <summary>
    /// Gets attachments for a specific message.
    /// </summary>
    /// <param name="messageId">The message ID.</param>
    /// <returns>List of attachments.</returns>
    /// <response code="200">Returns the attachments.</response>
    [HttpGet("messages/{messageId}/attachments")]
    [ProducesResponseType(typeof(List<AnydropAttachment>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AnydropAttachment>>> GetMessageAttachments(int messageId)
    {
        var attachments = await _anydropService.GetMessageAttachmentsAsync(messageId);

        return Ok(attachments);
    }

    /// <summary>
    /// Uploads an attachment to a message.
    /// </summary>
    /// <param name="messageId">The message ID.</param>
    /// <param name="file">The file to upload.</param>
    /// <returns>The created attachment.</returns>
    /// <response code="201">Attachment uploaded successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="404">Message not found.</response>
    [HttpPost("messages/{messageId}/attachments")]
    [RequestFormLimits(MultipartBodyLengthLimit = 104857600)] // 100MB
    [RequestSizeLimit(104857600)]
    [ProducesResponseType(typeof(AnydropAttachment), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AnydropAttachment>> UploadAttachment(int messageId, IFormFile file)
    {
        var message = await _anydropService.GetMessageByIdAsync(messageId);

        if (message is null)
        {
            return NotFound(new { error = "Message not found" });
        }

        if (file is null || file.Length == 0)
        {
            return BadRequest(new { error = "No file provided" });
        }

        // Validate filename
        var fileName = file.FileName;

        if (string.IsNullOrWhiteSpace(fileName) ||
            fileName.Contains("..") ||
            Path.GetInvalidFileNameChars().Any(c => fileName.Contains(c)))
        {
            return BadRequest(new { error = "Invalid filename" });
        }

        try
        {
            var uniqueFileName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{fileName}";
            var filePath = Path.Combine(AnydropUploadPath, uniqueFileName);

            Directory.CreateDirectory(AnydropUploadPath);

            using var stream = file.OpenReadStream();
            var success = await _fileUploadService.UploadFileAsync(AnydropUploadPath, stream, uniqueFileName);

            if (!success)
            {
                return BadRequest(new { error = "Failed to upload file" });
            }

            var attachment = await _anydropService.AddAttachmentAsync(
                messageId,
                fileName,
                filePath,
                file.Length,
                file.ContentType);

            return CreatedAtAction(
                nameof(GetMessageAttachments),
                new { messageId },
                attachment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading attachment for message {MessageId}", messageId);

            return BadRequest(new { error = "Failed to upload attachment" });
        }
    }

    /// <summary>
    /// Deletes an attachment.
    /// </summary>
    /// <param name="id">The attachment ID.</param>
    /// <returns>No content on success.</returns>
    /// <response code="204">Attachment deleted.</response>
    [HttpDelete("attachments/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAttachment(int id)
    {
        await _anydropService.DeleteAttachmentAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Downloads an attachment file.
    /// </summary>
    /// <param name="id">The attachment ID.</param>
    /// <returns>The file content.</returns>
    /// <response code="200">Returns the file.</response>
    /// <response code="404">Attachment or file not found.</response>
    [HttpGet("attachments/{id}/download")]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadAttachment(int id)
    {
        try
        {
            // Get all attachments and find the one with matching ID
            var attachments = await _anydropService.GetAttachmentsByTypeAsync(null, 1, int.MaxValue);
            var attachment = attachments.Items.FirstOrDefault(a => a.Id == id);

            if (attachment is null)
            {
                return NotFound(new { error = "Attachment not found" });
            }

            if (!System.IO.File.Exists(attachment.FilePath))
            {
                return NotFound(new { error = "File not found on disk" });
            }

            var stream = System.IO.File.OpenRead(attachment.FilePath);

            return File(stream, attachment.ContentType, attachment.FileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error downloading attachment {Id}", id);

            return StatusCode(500, new { error = "Failed to download attachment" });
        }
    }

    #endregion

    #region Search Endpoints

    /// <summary>
    /// Searches messages by content with context and highlighting.
    /// </summary>
    /// <param name="q">Search query.</param>
    /// <param name="page">Page number (1-based).</param>
    /// <param name="pageSize">Items per page.</param>
    /// <param name="includeContext">Whether to include surrounding messages.</param>
    /// <returns>Search results with highlighting.</returns>
    /// <response code="200">Returns search results.</response>
    /// <response code="400">Invalid search query.</response>
    [HttpGet("search")]
    [ProducesResponseType(typeof(PagedResult<AnydropSearchResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<AnydropSearchResult>>> Search(
        [FromQuery][Required] string q,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool includeContext = true)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest(new { error = "Search query is required" });
        }

        var result = await _anydropService.SearchMessagesAsync(q, page, pageSize, includeContext);

        return Ok(result);
    }

    #endregion

    #region Link Metadata Endpoints

    /// <summary>
    /// Gets link metadata for a message.
    /// </summary>
    /// <param name="messageId">The message ID.</param>
    /// <returns>List of link metadata.</returns>
    /// <response code="200">Returns link metadata.</response>
    [HttpGet("messages/{messageId}/links")]
    [ProducesResponseType(typeof(List<AnydropLinkMetadata>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AnydropLinkMetadata>>> GetLinkMetadata(int messageId)
    {
        var metadata = await _anydropService.GetLinkMetadataAsync(messageId);

        return Ok(metadata);
    }

    /// <summary>
    /// Refreshes link metadata for a message by re-fetching from URLs.
    /// </summary>
    /// <param name="messageId">The message ID.</param>
    /// <returns>Updated link metadata.</returns>
    /// <response code="200">Returns updated link metadata.</response>
    /// <response code="404">Message not found.</response>
    [HttpPost("messages/{messageId}/links/refresh")]
    [ProducesResponseType(typeof(List<AnydropLinkMetadata>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<AnydropLinkMetadata>>> RefreshLinkMetadata(int messageId)
    {
        var message = await _anydropService.GetMessageByIdAsync(messageId);

        if (message is null)
        {
            return NotFound(new { error = "Message not found" });
        }

        var metadata = await _anydropService.ParseAndSaveLinkMetadataAsync(messageId, message.Content);

        return Ok(metadata);
    }

    #endregion

    #region Statistics Endpoints

    /// <summary>
    /// Gets Anydrop statistics.
    /// </summary>
    /// <returns>Statistics dictionary.</returns>
    /// <response code="200">Returns statistics.</response>
    [HttpGet("statistics")]
    [ProducesResponseType(typeof(Dictionary<string, int>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Dictionary<string, int>>> GetStatistics()
    {
        var stats = await _anydropService.GetStatisticsAsync();

        return Ok(stats);
    }

    /// <summary>
    /// Gets device information for the current server.
    /// </summary>
    /// <returns>Device information.</returns>
    /// <response code="200">Returns device info.</response>
    [HttpGet("device")]
    [ProducesResponseType(typeof(DeviceInfo), StatusCodes.Status200OK)]
    public ActionResult<DeviceInfo> GetDeviceInfo()
    {
        return Ok(new DeviceInfo
        {
            DeviceId = _anydropService.GetDeviceId(),
            DeviceName = _anydropService.GetDeviceName()
        });
    }

    #endregion
}

#region Request/Response Models

/// <summary>
/// Request model for creating a new message.
/// </summary>
public class CreateMessageRequest
{
    /// <summary>
    /// Text content of the message. Can be empty if attachments are provided.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Type of the message content.
    /// </summary>
    public MessageType MessageType { get; set; } = MessageType.Text;

    /// <summary>
    /// Optional sender device ID (for API clients).
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// Optional sender device name (for API clients).
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    /// List of filenames for attachments (files uploaded separately).
    /// </summary>
    public List<string>? FileNames { get; set; }
}

/// <summary>
/// Device information response model.
/// </summary>
public class DeviceInfo
{
    /// <summary>
    /// Unique device identifier.
    /// </summary>
    public string DeviceId { get; set; } = string.Empty;

    /// <summary>
    /// Human-readable device name.
    /// </summary>
    public string DeviceName { get; set; } = string.Empty;
}

#endregion
