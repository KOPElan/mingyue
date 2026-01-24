using System.ComponentModel.DataAnnotations;

namespace MingYue.Models
{
    /// <summary>
    /// Application entity for application management
    /// </summary>
    public class Application
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string AppId { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Url { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Icon { get; set; } = string.Empty;

        [MaxLength(50)]
        public string IconColor { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        public int Order { get; set; } = 0;

        public bool IsVisible { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Dock item entity for quick access bar
    /// </summary>
    public class DockItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ItemId { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Icon { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Url { get; set; } = string.Empty;

        [MaxLength(200)]
        public string IconBackground { get; set; } = string.Empty;

        [MaxLength(50)]
        public string IconColor { get; set; } = string.Empty;

        [MaxLength(100)]
        public string AssociatedAppId { get; set; } = string.Empty;

        public int Order { get; set; } = 0;

        public bool IsPinned { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// System setting entity
    /// </summary>
    public class SystemSetting
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Key { get; set; } = string.Empty;

        [Required]
        public string Value { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(50)]
        public string DataType { get; set; } = "string";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
