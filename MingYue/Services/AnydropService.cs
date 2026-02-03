using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;
using System.Net.NetworkInformation;

namespace MingYue.Services
{
    /// <summary>
    /// Provides Anydrop message and file sharing services for cross-device communication.
    /// </summary>
    public class AnydropService : IAnydropService
    {
        private readonly IDbContextFactory<MingYueDbContext> _contextFactory;
        private readonly ILogger<AnydropService> _logger;
        private readonly string _deviceId;
        private readonly string _deviceName;

        /// <summary>
        /// Occurs when messages are changed (added, updated, or removed).
        /// </summary>
        public event EventHandler? MessagesChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnydropService"/> class.
        /// </summary>
        /// <param name="contextFactory">The database context factory for accessing Anydrop data.</param>
        /// <param name="logger">The logger for recording Anydrop operations and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public AnydropService(IDbContextFactory<MingYueDbContext> contextFactory, ILogger<AnydropService> logger)
        {
            ArgumentNullException.ThrowIfNull(contextFactory);
            ArgumentNullException.ThrowIfNull(logger);

            _contextFactory = contextFactory;
            _logger = logger;
            _deviceId = GenerateDeviceId();
            _deviceName = Environment.MachineName;
        }

        public string GetDeviceId() => _deviceId;
        public string GetDeviceName() => _deviceName;

        public async Task<List<AnydropMessage>> GetAllMessagesAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.AnydropMessages
                    .Include(m => m.Attachments)
                    .OrderByDescending(m => m.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all Anydrop messages");
                return new List<AnydropMessage>();
            }
        }

        public async Task<List<AnydropMessage>> GetUnreadMessagesAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.AnydropMessages
                    .Include(m => m.Attachments)
                    .Where(m => !m.IsRead)
                    .OrderByDescending(m => m.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread Anydrop messages");
                return new List<AnydropMessage>();
            }
        }

        public async Task<AnydropMessage?> GetMessageByIdAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.AnydropMessages
                    .Include(m => m.Attachments)
                    .FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Anydrop message {Id}", id);
                return null;
            }
        }

        public async Task<AnydropMessage> CreateMessageAsync(string content, string deviceId, string deviceName)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var message = new AnydropMessage
                {
                    Content = content,
                    SenderDeviceId = deviceId,
                    SenderDeviceName = deviceName,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                };

                context.AnydropMessages.Add(message);
                await context.SaveChangesAsync();

                OnMessagesChanged();
                return message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Anydrop message");
                throw;
            }
        }

        public async Task<AnydropAttachment> AddAttachmentAsync(int messageId, string fileName, string filePath, long fileSize, string contentType)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var attachment = new AnydropAttachment
                {
                    MessageId = messageId,
                    FileName = fileName,
                    FilePath = filePath,
                    FileSize = fileSize,
                    ContentType = contentType,
                    CreatedAt = DateTime.UtcNow
                };

                context.AnydropAttachments.Add(attachment);
                await context.SaveChangesAsync();

                OnMessagesChanged();
                return attachment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding attachment to message {MessageId}", messageId);
                throw;
            }
        }

        public async Task MarkAsReadAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var message = await context.AnydropMessages.FindAsync(id);
                if (message != null && !message.IsRead)
                {
                    message.IsRead = true;
                    await context.SaveChangesAsync();
                    OnMessagesChanged();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking Anydrop message {Id} as read", id);
            }
        }

        public async Task DeleteMessageAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var message = await context.AnydropMessages
                    .Include(m => m.Attachments)
                    .FirstOrDefaultAsync(m => m.Id == id);
                
                if (message != null)
                {
                    // Delete attachment files
                    foreach (var attachment in message.Attachments)
                    {
                        try
                        {
                            if (File.Exists(attachment.FilePath))
                            {
                                File.Delete(attachment.FilePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Error deleting attachment file {FilePath}", attachment.FilePath);
                        }
                    }

                    context.AnydropMessages.Remove(message);
                    await context.SaveChangesAsync();
                    OnMessagesChanged();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Anydrop message {Id}", id);
            }
        }

        public async Task<List<AnydropAttachment>> GetMessageAttachmentsAsync(int messageId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.AnydropAttachments
                    .Where(a => a.MessageId == messageId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting attachments for message {MessageId}", messageId);
                return new List<AnydropAttachment>();
            }
        }

        public async Task DeleteAttachmentAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var attachment = await context.AnydropAttachments.FindAsync(id);
                if (attachment != null)
                {
                    // Delete file
                    try
                    {
                        if (File.Exists(attachment.FilePath))
                        {
                            File.Delete(attachment.FilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Error deleting attachment file {FilePath}", attachment.FilePath);
                    }

                    context.AnydropAttachments.Remove(attachment);
                    await context.SaveChangesAsync();
                    OnMessagesChanged();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting attachment {Id}", id);
            }
        }

        private string GenerateDeviceId()
        {
            try
            {
                // Try to get MAC address as device ID
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(n => n.OperationalStatus == OperationalStatus.Up 
                        && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .ToList();

                if (networkInterfaces.Any())
                {
                    var mac = networkInterfaces.First().GetPhysicalAddress().ToString();
                    return mac;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error getting MAC address for device ID");
            }

            // Fallback to machine name + random ID
            return $"{Environment.MachineName}-{Guid.NewGuid().ToString()[..8]}";
        }

        private void OnMessagesChanged()
        {
            MessagesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
