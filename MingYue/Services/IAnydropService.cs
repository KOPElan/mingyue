using MingYue.Models;

namespace MingYue.Services
{
    /// <summary>
    /// Service interface for Anydrop message and file sharing functionality.
    /// Provides cross-device communication with support for text, media, links, and attachments.
    /// </summary>
    public interface IAnydropService
    {
        #region Events

        /// <summary>
        /// Event raised when messages change (created, updated, or deleted).
        /// </summary>
        event EventHandler? MessagesChanged;

        /// <summary>
        /// Event raised when upload progress changes.
        /// </summary>
        event EventHandler<AnydropUploadTask>? UploadProgressChanged;

        #endregion

        #region Device Information

        /// <summary>
        /// Gets the unique device identifier for this device.
        /// </summary>
        /// <returns>Device ID string.</returns>
        string GetDeviceId();

        /// <summary>
        /// Gets the human-readable device name for this device.
        /// </summary>
        /// <returns>Device name string.</returns>
        string GetDeviceName();

        #endregion

        #region Message Operations

        /// <summary>
        /// Gets all Anydrop messages.
        /// </summary>
        /// <returns>List of all messages ordered by creation date descending.</returns>
        Task<List<AnydropMessage>> GetAllMessagesAsync();

        /// <summary>
        /// Gets paginated messages with optional filtering.
        /// </summary>
        /// <param name="query">Query parameters for filtering and pagination.</param>
        /// <returns>Paginated result of messages.</returns>
        Task<PagedResult<AnydropMessage>> GetMessagesAsync(AnydropMessageQuery query);

        /// <summary>
        /// Gets unread messages.
        /// </summary>
        /// <returns>List of unread messages.</returns>
        Task<List<AnydropMessage>> GetUnreadMessagesAsync();

        /// <summary>
        /// Gets a message by its ID.
        /// </summary>
        /// <param name="id">Message ID.</param>
        /// <returns>The message if found, null otherwise.</returns>
        Task<AnydropMessage?> GetMessageByIdAsync(int id);

        /// <summary>
        /// Creates a new message.
        /// </summary>
        /// <param name="content">Message text content.</param>
        /// <param name="deviceId">Sender device ID.</param>
        /// <param name="deviceName">Sender device name.</param>
        /// <param name="messageType">Type of message content.</param>
        /// <returns>The created message.</returns>
        Task<AnydropMessage> CreateMessageAsync(string content, string deviceId, string deviceName, MessageType messageType = MessageType.Text);

        /// <summary>
        /// Marks a message as read.
        /// </summary>
        /// <param name="id">Message ID.</param>
        Task MarkAsReadAsync(int id);

        /// <summary>
        /// Marks multiple messages as read.
        /// </summary>
        /// <param name="ids">Collection of message IDs.</param>
        Task MarkMultipleAsReadAsync(IEnumerable<int> ids);

        /// <summary>
        /// Deletes a message and its attachments.
        /// </summary>
        /// <param name="id">Message ID.</param>
        Task DeleteMessageAsync(int id);

        /// <summary>
        /// Deletes multiple messages and their attachments.
        /// </summary>
        /// <param name="ids">Collection of message IDs.</param>
        Task DeleteMultipleMessagesAsync(IEnumerable<int> ids);

        #endregion

        #region Attachment Operations

        /// <summary>
        /// Adds an attachment to a message.
        /// </summary>
        /// <param name="messageId">Parent message ID.</param>
        /// <param name="fileName">Original filename.</param>
        /// <param name="filePath">Path to stored file.</param>
        /// <param name="fileSize">File size in bytes.</param>
        /// <param name="contentType">MIME content type.</param>
        /// <param name="thumbnailPath">Optional path to thumbnail.</param>
        /// <returns>The created attachment.</returns>
        Task<AnydropAttachment> AddAttachmentAsync(int messageId, string fileName, string filePath, long fileSize, string contentType, string? thumbnailPath = null);

        /// <summary>
        /// Gets attachments for a specific message.
        /// </summary>
        /// <param name="messageId">Message ID.</param>
        /// <returns>List of attachments.</returns>
        Task<List<AnydropAttachment>> GetMessageAttachmentsAsync(int messageId);

        /// <summary>
        /// Deletes an attachment.
        /// </summary>
        /// <param name="id">Attachment ID.</param>
        Task DeleteAttachmentAsync(int id);

        /// <summary>
        /// Gets all attachments filtered by type with pagination.
        /// </summary>
        /// <param name="contentTypePrefix">Content type prefix filter (e.g., "image/", "video/").</param>
        /// <param name="page">Page number (1-based).</param>
        /// <param name="pageSize">Items per page.</param>
        /// <param name="fromDate">Optional start date filter.</param>
        /// <param name="toDate">Optional end date filter.</param>
        /// <returns>Paginated attachments.</returns>
        Task<PagedResult<AnydropAttachment>> GetAttachmentsByTypeAsync(string? contentTypePrefix, int page = 1, int pageSize = 20, DateTime? fromDate = null, DateTime? toDate = null);

        #endregion

        #region Search Operations

        /// <summary>
        /// Searches messages by content with context and highlighting.
        /// </summary>
        /// <param name="searchTerm">Search term.</param>
        /// <param name="page">Page number (1-based).</param>
        /// <param name="pageSize">Items per page.</param>
        /// <param name="includeContext">Whether to include surrounding messages.</param>
        /// <returns>Paginated search results with highlighting.</returns>
        Task<PagedResult<AnydropSearchResult>> SearchMessagesAsync(string searchTerm, int page = 1, int pageSize = 20, bool includeContext = true);

        #endregion

        #region Link Metadata Operations

        /// <summary>
        /// Parses URLs in content and fetches their metadata.
        /// </summary>
        /// <param name="messageId">Message ID.</param>
        /// <param name="content">Content containing URLs.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of parsed link metadata.</returns>
        Task<List<AnydropLinkMetadata>> ParseAndSaveLinkMetadataAsync(int messageId, string content, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets link metadata for a message.
        /// </summary>
        /// <param name="messageId">Message ID.</param>
        /// <returns>List of link metadata.</returns>
        Task<List<AnydropLinkMetadata>> GetLinkMetadataAsync(int messageId);

        #endregion

        #region Statistics

        /// <summary>
        /// Gets message statistics (counts by type, unread count, etc.).
        /// </summary>
        /// <returns>Statistics dictionary.</returns>
        Task<Dictionary<string, int>> GetStatisticsAsync();

        #endregion
    }
}
