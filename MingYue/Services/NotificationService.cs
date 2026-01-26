using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;

namespace MingYue.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IDbContextFactory<MingYueDbContext> _contextFactory;
        private readonly ILogger<NotificationService> _logger;

        public event EventHandler? NotificationsChanged;

        public NotificationService(IDbContextFactory<MingYueDbContext> contextFactory, ILogger<NotificationService> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.Notifications
                    .OrderByDescending(n => n.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all notifications");
                return new List<Notification>();
            }
        }

        public async Task<List<Notification>> GetUnreadNotificationsAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.Notifications
                    .Where(n => !n.IsRead)
                    .OrderByDescending(n => n.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread notifications");
                return new List<Notification>();
            }
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.Notifications.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting notification {Id}", id);
                return null;
            }
        }

        public async Task<Notification> CreateNotificationAsync(string title, string message, string type = "Info", string? actionUrl = null, string? icon = null)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var notification = new Notification
                {
                    Title = title,
                    Message = message,
                    Type = type,
                    ActionUrl = actionUrl,
                    Icon = icon,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                };

                context.Notifications.Add(notification);
                await context.SaveChangesAsync();

                OnNotificationsChanged();
                return notification;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating notification");
                throw;
            }
        }

        public async Task MarkAsReadAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var notification = await context.Notifications.FindAsync(id);
                if (notification != null && !notification.IsRead)
                {
                    notification.IsRead = true;
                    await context.SaveChangesAsync();
                    OnNotificationsChanged();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notification {Id} as read", id);
            }
        }

        public async Task MarkAllAsReadAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var unreadNotifications = await context.Notifications
                    .Where(n => !n.IsRead)
                    .ToListAsync();

                foreach (var notification in unreadNotifications)
                {
                    notification.IsRead = true;
                }

                await context.SaveChangesAsync();
                OnNotificationsChanged();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking all notifications as read");
            }
        }

        public async Task DeleteNotificationAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var notification = await context.Notifications.FindAsync(id);
                if (notification != null)
                {
                    context.Notifications.Remove(notification);
                    await context.SaveChangesAsync();
                    OnNotificationsChanged();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting notification {Id}", id);
            }
        }

        public async Task DeleteAllReadAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var readNotifications = await context.Notifications
                    .Where(n => n.IsRead)
                    .ToListAsync();

                context.Notifications.RemoveRange(readNotifications);
                await context.SaveChangesAsync();
                OnNotificationsChanged();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting all read notifications");
            }
        }

        public async Task<int> GetUnreadCountAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.Notifications.CountAsync(n => !n.IsRead);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread count");
                return 0;
            }
        }

        private void OnNotificationsChanged()
        {
            NotificationsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
