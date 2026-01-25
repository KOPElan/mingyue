using System.ComponentModel.DataAnnotations;

namespace MingYue.Models
{
    /// <summary>
    /// Notification entity for system notifications
    /// </summary>
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = "Info"; // Info, Success, Warning, Error

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? ActionUrl { get; set; }

        [MaxLength(100)]
        public string? Icon { get; set; }
    }
}
