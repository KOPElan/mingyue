using MingYue.Models;

namespace MingYue.Services
{
    public interface IAnydropService
    {
        /// <summary>
        /// Event raised when a new message is created
        /// </summary>
        event EventHandler? MessagesChanged;

        /// <summary>
        /// Get all Anydrop messages
        /// </summary>
        Task<List<AnydropMessage>> GetAllMessagesAsync();

        /// <summary>
        /// Get unread messages
        /// </summary>
        Task<List<AnydropMessage>> GetUnreadMessagesAsync();

        /// <summary>
        /// Get message by ID
        /// </summary>
        Task<AnydropMessage?> GetMessageByIdAsync(int id);

        /// <summary>
        /// Create a new message
        /// </summary>
        Task<AnydropMessage> CreateMessageAsync(string content, string deviceId, string deviceName);

        /// <summary>
        /// Add attachment to a message
        /// </summary>
        Task<AnydropAttachment> AddAttachmentAsync(int messageId, string fileName, string filePath, long fileSize, string contentType);

        /// <summary>
        /// Mark message as read
        /// </summary>
        Task MarkAsReadAsync(int id);

        /// <summary>
        /// Delete message
        /// </summary>
        Task DeleteMessageAsync(int id);

        /// <summary>
        /// Get message attachments
        /// </summary>
        Task<List<AnydropAttachment>> GetMessageAttachmentsAsync(int messageId);

        /// <summary>
        /// Delete attachment
        /// </summary>
        Task DeleteAttachmentAsync(int id);

        /// <summary>
        /// Get device ID for this device
        /// </summary>
        string GetDeviceId();

        /// <summary>
        /// Get device name for this device
        /// </summary>
        string GetDeviceName();
    }
}
