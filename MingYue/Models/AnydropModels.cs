using System.ComponentModel.DataAnnotations;

namespace MingYue.Models
{
    /// <summary>
    /// Anydrop message entity for cross-device messaging
    /// </summary>
    public class AnydropMessage
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string SenderDeviceId { get; set; } = string.Empty;

        [MaxLength(200)]
        public string SenderDeviceName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;

        // Navigation property
        public ICollection<AnydropAttachment> Attachments { get; set; } = new List<AnydropAttachment>();
    }

    /// <summary>
    /// Anydrop file attachment entity
    /// </summary>
    public class AnydropAttachment
    {
        public int Id { get; set; }

        public int MessageId { get; set; }

        [Required]
        [MaxLength(500)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string FilePath { get; set; } = string.Empty;

        public long FileSize { get; set; }

        [MaxLength(200)]
        public string ContentType { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public AnydropMessage? Message { get; set; }
    }
}
