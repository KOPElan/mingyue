using System.ComponentModel.DataAnnotations;

namespace MingYue.Models
{
    /// <summary>
    /// Scheduled task entity for cron job scheduling
    /// </summary>
    public class ScheduledTask
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string TaskType { get; set; } = string.Empty; // Script, Command, Http

        [Required]
        public string TaskData { get; set; } = string.Empty; // JSON configuration

        [Required]
        [MaxLength(100)]
        public string CronExpression { get; set; } = string.Empty;

        public bool IsEnabled { get; set; } = true;

        public DateTime? LastRunAt { get; set; }

        public DateTime? NextRunAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Scheduled task execution history
    /// </summary>
    public class ScheduledTaskExecutionHistory
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public bool Success { get; set; }

        public string Output { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
