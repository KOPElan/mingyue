using MingYue.Models;

namespace MingYue.Services
{
    public interface INotificationService
    {
        /// <summary>
        /// Event raised when a notification is created or updated
        /// </summary>
        event EventHandler? NotificationsChanged;

        /// <summary>
        /// Get all notifications
        /// </summary>
        Task<List<Notification>> GetAllNotificationsAsync();

        /// <summary>
        /// Get unread notifications
        /// </summary>
        Task<List<Notification>> GetUnreadNotificationsAsync();

        /// <summary>
        /// Get notification by ID
        /// </summary>
        Task<Notification?> GetNotificationByIdAsync(int id);

        /// <summary>
        /// Create a new notification
        /// </summary>
        Task<Notification> CreateNotificationAsync(string title, string message, string type = "Info", string? actionUrl = null, string? icon = null);

        /// <summary>
        /// Mark notification as read
        /// </summary>
        Task MarkAsReadAsync(int id);

        /// <summary>
        /// Mark all notifications as read
        /// </summary>
        Task MarkAllAsReadAsync();

        /// <summary>
        /// Delete notification
        /// </summary>
        Task DeleteNotificationAsync(int id);

        /// <summary>
        /// Delete all read notifications
        /// </summary>
        Task DeleteAllReadAsync();

        /// <summary>
        /// Get unread notification count
        /// </summary>
        Task<int> GetUnreadCountAsync();
    }
}
