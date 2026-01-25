using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;

namespace MingYue.Services
{
    public class ScheduledTaskService : IScheduledTaskService
    {
        private readonly IDbContextFactory<MingYueDbContext> _contextFactory;
        private readonly ILogger<ScheduledTaskService> _logger;

        public ScheduledTaskService(IDbContextFactory<MingYueDbContext> contextFactory, ILogger<ScheduledTaskService> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<ScheduledTask>> GetAllTasksAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.ScheduledTasks
                    .OrderBy(t => t.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all scheduled tasks");
                return new List<ScheduledTask>();
            }
        }

        public async Task<ScheduledTask?> GetTaskByIdAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.ScheduledTasks.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting scheduled task {Id}", id);
                return null;
            }
        }

        public async Task<ScheduledTask> CreateTaskAsync(ScheduledTask task)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                task.CreatedAt = DateTime.UtcNow;
                task.UpdatedAt = DateTime.UtcNow;
                
                context.ScheduledTasks.Add(task);
                await context.SaveChangesAsync();
                
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating scheduled task");
                throw;
            }
        }

        public async Task UpdateTaskAsync(ScheduledTask task)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                task.UpdatedAt = DateTime.UtcNow;
                
                context.ScheduledTasks.Update(task);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating scheduled task {Id}", task.Id);
                throw;
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var task = await context.ScheduledTasks.FindAsync(id);
                if (task != null)
                {
                    // Also delete execution history
                    var history = await context.ScheduledTaskExecutionHistories
                        .Where(h => h.TaskId == id)
                        .ToListAsync();
                    context.ScheduledTaskExecutionHistories.RemoveRange(history);
                    
                    context.ScheduledTasks.Remove(task);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting scheduled task {Id}", id);
                throw;
            }
        }

        public async Task SetTaskEnabledAsync(int id, bool enabled)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var task = await context.ScheduledTasks.FindAsync(id);
                if (task != null)
                {
                    task.IsEnabled = enabled;
                    task.UpdatedAt = DateTime.UtcNow;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting task {Id} enabled status to {Enabled}", id, enabled);
                throw;
            }
        }

        public async Task<List<ScheduledTaskExecutionHistory>> GetTaskHistoryAsync(int taskId, int limit = 50)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.ScheduledTaskExecutionHistories
                    .Where(h => h.TaskId == taskId)
                    .OrderByDescending(h => h.StartedAt)
                    .Take(limit)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting task history for task {TaskId}", taskId);
                return new List<ScheduledTaskExecutionHistory>();
            }
        }

        public async Task<ScheduledTaskExecutionHistory> RecordExecutionAsync(int taskId, bool success, string output, string errorMessage)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var history = new ScheduledTaskExecutionHistory
                {
                    TaskId = taskId,
                    StartedAt = DateTime.UtcNow,
                    CompletedAt = DateTime.UtcNow,
                    Success = success,
                    Output = output.Length > 4000 ? output.Substring(0, 4000) : output,
                    ErrorMessage = errorMessage.Length > 4000 ? errorMessage.Substring(0, 4000) : errorMessage
                };

                context.ScheduledTaskExecutionHistories.Add(history);
                await context.SaveChangesAsync();

                return history;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recording execution for task {TaskId}", taskId);
                throw;
            }
        }

        public async Task UpdateTaskRunTimesAsync(int taskId, DateTime lastRunAt, DateTime? nextRunAt)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var task = await context.ScheduledTasks.FindAsync(taskId);
                if (task != null)
                {
                    task.LastRunAt = lastRunAt;
                    task.NextRunAt = nextRunAt;
                    task.UpdatedAt = DateTime.UtcNow;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating run times for task {TaskId}", taskId);
            }
        }
    }
}
