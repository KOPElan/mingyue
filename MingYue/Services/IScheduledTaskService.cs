using MingYue.Models;

namespace MingYue.Services
{
    public interface IScheduledTaskService
    {
        /// <summary>
        /// Get all scheduled tasks
        /// </summary>
        Task<List<ScheduledTask>> GetAllTasksAsync();

        /// <summary>
        /// Get scheduled task by ID
        /// </summary>
        Task<ScheduledTask?> GetTaskByIdAsync(int id);

        /// <summary>
        /// Create a new scheduled task
        /// </summary>
        Task<ScheduledTask> CreateTaskAsync(ScheduledTask task);

        /// <summary>
        /// Update an existing scheduled task
        /// </summary>
        Task UpdateTaskAsync(ScheduledTask task);

        /// <summary>
        /// Delete a scheduled task
        /// </summary>
        Task DeleteTaskAsync(int id);

        /// <summary>
        /// Enable/disable a scheduled task
        /// </summary>
        Task SetTaskEnabledAsync(int id, bool enabled);

        /// <summary>
        /// Get task execution history
        /// </summary>
        Task<List<ScheduledTaskExecutionHistory>> GetTaskHistoryAsync(int taskId, int limit = 50);

        /// <summary>
        /// Record task execution
        /// </summary>
        Task<ScheduledTaskExecutionHistory> RecordExecutionAsync(int taskId, bool success, string output, string errorMessage);

        /// <summary>
        /// Update task last run time and calculate next run time
        /// </summary>
        Task UpdateTaskRunTimesAsync(int taskId, DateTime lastRunAt, DateTime? nextRunAt);
    }
}
