using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MingYue.Models
{
    /// <summary>
    /// Defines the type of message content for display and handling purposes.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Plain text message.
        /// </summary>
        Text = 0,

        /// <summary>
        /// Image file message.
        /// </summary>
        Image = 1,

        /// <summary>
        /// Video file message.
        /// </summary>
        Video = 2,

        /// <summary>
        /// URL link message with meta preview.
        /// </summary>
        Link = 3,

        /// <summary>
        /// Generic file attachment message.
        /// </summary>
        Attachment = 4,

        /// <summary>
        /// Audio file message.
        /// </summary>
        Audio = 5,

        /// <summary>
        /// Mixed content message (text + attachments).
        /// </summary>
        Mixed = 6
    }

    /// <summary>
    /// Anydrop message entity for cross-device messaging.
    /// Supports multiple content types including text, images, videos, links, and attachments.
    /// </summary>
    public class AnydropMessage
    {
        /// <summary>
        /// Unique identifier for the message.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Text content of the message. Can be empty for attachment-only messages.
        /// </summary>
        [Required]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Unique identifier of the sending device.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string SenderDeviceId { get; set; } = string.Empty;

        /// <summary>
        /// Human-readable name of the sending device.
        /// </summary>
        [MaxLength(200)]
        public string SenderDeviceName { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when the message was created (UTC).
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indicates whether the message has been read.
        /// </summary>
        public bool IsRead { get; set; } = false;

        /// <summary>
        /// Type of the message content for UI rendering decisions.
        /// </summary>
        public MessageType MessageType { get; set; } = MessageType.Text;

        /// <summary>
        /// Navigation property for message attachments.
        /// </summary>
        public ICollection<AnydropAttachment> Attachments { get; set; } = new List<AnydropAttachment>();

        /// <summary>
        /// Navigation property for link metadata (if message contains URLs).
        /// </summary>
        public ICollection<AnydropLinkMetadata> LinkMetadatas { get; set; } = new List<AnydropLinkMetadata>();
    }

    /// <summary>
    /// Anydrop file attachment entity supporting images, videos, and generic files.
    /// </summary>
    public class AnydropAttachment
    {
        /// <summary>
        /// Unique identifier for the attachment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the parent message.
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// Original filename of the uploaded file.
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Full path to the stored file on disk.
        /// </summary>
        [Required]
        [MaxLength(2000)]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Size of the file in bytes.
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// MIME type of the file content.
        /// </summary>
        [MaxLength(200)]
        public string ContentType { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when the attachment was created (UTC).
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Path to the generated thumbnail for images and videos.
        /// Null if no thumbnail is available.
        /// </summary>
        [MaxLength(2000)]
        public string? ThumbnailPath { get; set; }

        /// <summary>
        /// Width of the media in pixels (for images and videos).
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Height of the media in pixels (for images and videos).
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Duration of the media in seconds (for videos and audio).
        /// </summary>
        public double? Duration { get; set; }

        /// <summary>
        /// Navigation property to the parent message.
        /// </summary>
        [JsonIgnore]
        public AnydropMessage? Message { get; set; }

        /// <summary>
        /// Determines if the attachment is an image based on content type.
        /// </summary>
        [NotMapped]
        public bool IsImage => ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines if the attachment is a video based on content type.
        /// </summary>
        [NotMapped]
        public bool IsVideo => ContentType.StartsWith("video/", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines if the attachment is audio based on content type.
        /// </summary>
        [NotMapped]
        public bool IsAudio => ContentType.StartsWith("audio/", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Stores parsed metadata from URLs included in messages.
    /// Used for rich link previews in the chat interface.
    /// </summary>
    public class AnydropLinkMetadata
    {
        /// <summary>
        /// Unique identifier for the link metadata.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the parent message.
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// The original URL that was parsed.
        /// </summary>
        [Required]
        [MaxLength(2000)]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Page title from og:title or title tag.
        /// </summary>
        [MaxLength(500)]
        public string? Title { get; set; }

        /// <summary>
        /// Page description from og:description or meta description.
        /// </summary>
        [MaxLength(2000)]
        public string? Description { get; set; }

        /// <summary>
        /// URL of the preview image from og:image.
        /// </summary>
        [MaxLength(2000)]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Site name from og:site_name.
        /// </summary>
        [MaxLength(200)]
        public string? SiteName { get; set; }

        /// <summary>
        /// Favicon URL of the site.
        /// </summary>
        [MaxLength(2000)]
        public string? FaviconUrl { get; set; }

        /// <summary>
        /// Timestamp when the metadata was fetched (UTC).
        /// </summary>
        public DateTime FetchedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indicates if metadata fetch was successful.
        /// </summary>
        public bool IsFetchSuccessful { get; set; } = false;

        /// <summary>
        /// Error message if metadata fetch failed.
        /// </summary>
        [MaxLength(500)]
        public string? FetchError { get; set; }

        /// <summary>
        /// Navigation property to the parent message.
        /// </summary>
        [JsonIgnore]
        public AnydropMessage? Message { get; set; }
    }

    /// <summary>
    /// Represents an upload task in the upload queue for tracking progress.
    /// Not persisted to database - used for runtime tracking only.
    /// </summary>
    public class AnydropUploadTask
    {
        /// <summary>
        /// Unique identifier for the upload task.
        /// </summary>
        public string TaskId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Original filename being uploaded.
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Total file size in bytes.
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// Bytes uploaded so far.
        /// </summary>
        public long UploadedSize { get; set; }

        /// <summary>
        /// Current upload status.
        /// </summary>
        public UploadStatus Status { get; set; } = UploadStatus.Pending;

        /// <summary>
        /// Progress percentage (0-100).
        /// </summary>
        public int ProgressPercent => TotalSize > 0 ? (int)(UploadedSize * 100 / TotalSize) : 0;

        /// <summary>
        /// Upload start time for calculating speed.
        /// </summary>
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// Error message if upload failed.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Message ID once created (for associating attachments).
        /// </summary>
        public int? MessageId { get; set; }

        /// <summary>
        /// Cancellation token source for cancelling the upload.
        /// </summary>
        [JsonIgnore]
        public CancellationTokenSource? CancellationTokenSource { get; set; }
    }

    /// <summary>
    /// Upload task status enumeration.
    /// </summary>
    public enum UploadStatus
    {
        /// <summary>
        /// Upload is queued and waiting to start.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Upload is currently in progress.
        /// </summary>
        Uploading = 1,

        /// <summary>
        /// Upload completed successfully.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// Upload failed with an error.
        /// </summary>
        Failed = 3,

        /// <summary>
        /// Upload was cancelled by user.
        /// </summary>
        Cancelled = 4,

        /// <summary>
        /// Upload is paused.
        /// </summary>
        Paused = 5
    }

    /// <summary>
    /// Paginated result wrapper for API responses.
    /// </summary>
    /// <typeparam name="T">Type of items in the result.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Items in the current page.
        /// </summary>
        public List<T> Items { get; set; } = new();

        /// <summary>
        /// Total number of items across all pages.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Current page number (1-based).
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of pages.
        /// </summary>
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;

        /// <summary>
        /// Indicates if there is a next page.
        /// </summary>
        public bool HasNextPage => Page < TotalPages;

        /// <summary>
        /// Indicates if there is a previous page.
        /// </summary>
        public bool HasPreviousPage => Page > 1;
    }

    /// <summary>
    /// Search result item with context highlighting information.
    /// </summary>
    public class AnydropSearchResult
    {
        /// <summary>
        /// The matched message.
        /// </summary>
        public AnydropMessage Message { get; set; } = null!;

        /// <summary>
        /// Content snippet with search term highlighted.
        /// </summary>
        public string HighlightedContent { get; set; } = string.Empty;

        /// <summary>
        /// Match score for relevance ranking.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Context messages before and after the match.
        /// </summary>
        public List<AnydropMessage> ContextMessages { get; set; } = new();
    }

    /// <summary>
    /// Query parameters for filtering and searching messages.
    /// </summary>
    public class AnydropMessageQuery
    {
        /// <summary>
        /// Search query string for full-text search.
        /// </summary>
        public string? SearchQuery { get; set; }

        /// <summary>
        /// Filter by message type.
        /// </summary>
        public MessageType? MessageType { get; set; }

        /// <summary>
        /// Filter by read status.
        /// </summary>
        public bool? IsRead { get; set; }

        /// <summary>
        /// Filter messages created after this date.
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Filter messages created before this date.
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Filter by sender device ID.
        /// </summary>
        public string? SenderDeviceId { get; set; }

        /// <summary>
        /// Include only messages with attachments.
        /// </summary>
        public bool? HasAttachments { get; set; }

        /// <summary>
        /// Page number for pagination (1-based).
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Number of items per page.
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Sort field name.
        /// </summary>
        public string SortBy { get; set; } = "CreatedAt";

        /// <summary>
        /// Sort in descending order.
        /// </summary>
        public bool SortDescending { get; set; } = true;
    }
}
