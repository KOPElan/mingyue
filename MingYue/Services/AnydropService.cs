using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace MingYue.Services
{
    /// <summary>
    /// Provides Anydrop message and file sharing services for cross-device communication.
    /// Supports text, media, links, and file attachments with search and filtering capabilities.
    /// </summary>
    public partial class AnydropService : IAnydropService
    {
        private readonly IDbContextFactory<MingYueDbContext> _contextFactory;
        private readonly ILogger<AnydropService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _deviceId;
        private readonly string _deviceName;
        
        // Configuration constants
        private const int DefaultContextMessageCount = 2;
        private const int MaxUrlsPerMessage = 5;
        private const int LinkMetadataFetchTimeoutSeconds = 10;

        /// <inheritdoc />
        public event EventHandler? MessagesChanged;

        /// <inheritdoc />
        public event EventHandler<AnydropUploadTask>? UploadProgressChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnydropService"/> class.
        /// </summary>
        /// <param name="contextFactory">The database context factory for accessing Anydrop data.</param>
        /// <param name="logger">The logger for recording Anydrop operations and errors.</param>
        /// <param name="httpClientFactory">HTTP client factory for fetching link metadata.</param>
        /// <exception cref="ArgumentNullException">Thrown when any required parameter is null.</exception>
        public AnydropService(
            IDbContextFactory<MingYueDbContext> contextFactory,
            ILogger<AnydropService> logger,
            IHttpClientFactory httpClientFactory)
        {
            ArgumentNullException.ThrowIfNull(contextFactory);
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(httpClientFactory);

            _contextFactory = contextFactory;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _deviceId = GenerateDeviceId();
            _deviceName = Environment.MachineName;
        }

        #region Device Information

        /// <inheritdoc />
        public string GetDeviceId() => _deviceId;

        /// <inheritdoc />
        public string GetDeviceName() => _deviceName;

        #endregion

        #region Message Operations

        /// <inheritdoc />
        public async Task<List<AnydropMessage>> GetAllMessagesAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                return await context.AnydropMessages
                    .Include(m => m.Attachments)
                    .Include(m => m.LinkMetadatas)
                    .OrderByDescending(m => m.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all Anydrop messages");

                return new List<AnydropMessage>();
            }
        }

        /// <inheritdoc />
        public async Task<PagedResult<AnydropMessage>> GetMessagesAsync(AnydropMessageQuery query)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var queryable = context.AnydropMessages
                    .Include(m => m.Attachments)
                    .Include(m => m.LinkMetadatas)
                    .AsQueryable();

                // Apply filters
                if (!string.IsNullOrWhiteSpace(query.SearchQuery))
                {
                    var searchTerm = query.SearchQuery.ToLowerInvariant();
                    queryable = queryable.Where(m =>
                        m.Content.ToLower().Contains(searchTerm) ||
                        m.SenderDeviceName.ToLower().Contains(searchTerm) ||
                        m.Attachments.Any(a => a.FileName.ToLower().Contains(searchTerm)));
                }

                if (query.MessageType.HasValue)
                {
                    queryable = queryable.Where(m => m.MessageType == query.MessageType.Value);
                }

                if (query.IsRead.HasValue)
                {
                    queryable = queryable.Where(m => m.IsRead == query.IsRead.Value);
                }

                if (query.FromDate.HasValue)
                {
                    queryable = queryable.Where(m => m.CreatedAt >= query.FromDate.Value);
                }

                if (query.ToDate.HasValue)
                {
                    queryable = queryable.Where(m => m.CreatedAt <= query.ToDate.Value);
                }

                if (!string.IsNullOrWhiteSpace(query.SenderDeviceId))
                {
                    queryable = queryable.Where(m => m.SenderDeviceId == query.SenderDeviceId);
                }

                if (query.HasAttachments == true)
                {
                    queryable = queryable.Where(m => m.Attachments.Any());
                }

                // Get total count before pagination
                var totalCount = await queryable.CountAsync();

                // Apply sorting
                queryable = query.SortBy.ToLowerInvariant() switch
                {
                    "createdat" => query.SortDescending
                        ? queryable.OrderByDescending(m => m.CreatedAt)
                        : queryable.OrderBy(m => m.CreatedAt),
                    "senderdevicename" => query.SortDescending
                        ? queryable.OrderByDescending(m => m.SenderDeviceName)
                        : queryable.OrderBy(m => m.SenderDeviceName),
                    _ => queryable.OrderByDescending(m => m.CreatedAt)
                };

                // Apply pagination
                var skip = (query.Page - 1) * query.PageSize;
                var items = await queryable
                    .Skip(skip)
                    .Take(query.PageSize)
                    .ToListAsync();

                return new PagedResult<AnydropMessage>
                {
                    Items = items,
                    TotalCount = totalCount,
                    Page = query.Page,
                    PageSize = query.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated Anydrop messages");

                return new PagedResult<AnydropMessage>();
            }
        }

        /// <inheritdoc />
        public async Task<List<AnydropMessage>> GetUnreadMessagesAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                return await context.AnydropMessages
                    .Include(m => m.Attachments)
                    .Include(m => m.LinkMetadatas)
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

        /// <inheritdoc />
        public async Task<AnydropMessage?> GetMessageByIdAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                return await context.AnydropMessages
                    .Include(m => m.Attachments)
                    .Include(m => m.LinkMetadatas)
                    .FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Anydrop message {Id}", id);

                return null;
            }
        }

        /// <inheritdoc />
        public async Task<AnydropMessage> CreateMessageAsync(string content, string deviceId, string deviceName, MessageType messageType = MessageType.Text)
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
                    IsRead = false,
                    MessageType = messageType
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

        /// <inheritdoc />
        public async Task MarkAsReadAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var message = await context.AnydropMessages.FindAsync(id);

                if (message is not null && !message.IsRead)
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

        /// <inheritdoc />
        public async Task MarkMultipleAsReadAsync(IEnumerable<int> ids)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var messages = await context.AnydropMessages
                    .Where(m => ids.Contains(m.Id) && !m.IsRead)
                    .ToListAsync();

                foreach (var message in messages)
                {
                    message.IsRead = true;
                }

                await context.SaveChangesAsync();
                OnMessagesChanged();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking multiple Anydrop messages as read");
            }
        }

        /// <inheritdoc />
        public async Task DeleteMessageAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var message = await context.AnydropMessages
                    .Include(m => m.Attachments)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (message is not null)
                {
                    // Delete attachment files
                    foreach (var attachment in message.Attachments)
                    {
                        DeleteAttachmentFiles(attachment);
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

        /// <inheritdoc />
        public async Task DeleteMultipleMessagesAsync(IEnumerable<int> ids)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var messages = await context.AnydropMessages
                    .Include(m => m.Attachments)
                    .Where(m => ids.Contains(m.Id))
                    .ToListAsync();

                foreach (var message in messages)
                {
                    foreach (var attachment in message.Attachments)
                    {
                        DeleteAttachmentFiles(attachment);
                    }

                    context.AnydropMessages.Remove(message);
                }

                await context.SaveChangesAsync();
                OnMessagesChanged();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting multiple Anydrop messages");
            }
        }

        #endregion

        #region Attachment Operations

        /// <inheritdoc />
        public async Task<AnydropAttachment> AddAttachmentAsync(int messageId, string fileName, string filePath, long fileSize, string contentType, string? thumbnailPath = null)
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
                    ThumbnailPath = thumbnailPath,
                    CreatedAt = DateTime.UtcNow
                };

                context.AnydropAttachments.Add(attachment);
                await context.SaveChangesAsync();

                // Update message type based on attachment
                var message = await context.AnydropMessages.FindAsync(messageId);

                if (message is not null)
                {
                    message.MessageType = DetermineMessageType(message, attachment);
                    await context.SaveChangesAsync();
                }

                OnMessagesChanged();

                return attachment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding attachment to message {MessageId}", messageId);
                throw;
            }
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task DeleteAttachmentAsync(int id)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var attachment = await context.AnydropAttachments.FindAsync(id);

                if (attachment is not null)
                {
                    DeleteAttachmentFiles(attachment);
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

        /// <inheritdoc />
        public async Task<PagedResult<AnydropAttachment>> GetAttachmentsByTypeAsync(string? contentTypePrefix, int page = 1, int pageSize = 20, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var queryable = context.AnydropAttachments.AsQueryable();

                if (!string.IsNullOrWhiteSpace(contentTypePrefix))
                {
                    queryable = queryable.Where(a => a.ContentType.StartsWith(contentTypePrefix));
                }

                if (fromDate.HasValue)
                {
                    queryable = queryable.Where(a => a.CreatedAt >= fromDate.Value);
                }

                if (toDate.HasValue)
                {
                    queryable = queryable.Where(a => a.CreatedAt <= toDate.Value);
                }

                var totalCount = await queryable.CountAsync();
                var skip = (page - 1) * pageSize;
                var items = await queryable
                    .OrderByDescending(a => a.CreatedAt)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedResult<AnydropAttachment>
                {
                    Items = items,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting attachments by type {ContentTypePrefix}", contentTypePrefix);

                return new PagedResult<AnydropAttachment>();
            }
        }

        #endregion

        #region Search Operations

        /// <inheritdoc />
        public async Task<PagedResult<AnydropSearchResult>> SearchMessagesAsync(string searchTerm, int page = 1, int pageSize = 20, bool includeContext = true)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return new PagedResult<AnydropSearchResult>();
                }

                using var context = await _contextFactory.CreateDbContextAsync();
                var normalizedSearchTerm = searchTerm.ToLowerInvariant();

                // Search in content and file names
                var queryable = context.AnydropMessages
                    .Include(m => m.Attachments)
                    .Include(m => m.LinkMetadatas)
                    .Where(m =>
                        m.Content.ToLower().Contains(normalizedSearchTerm) ||
                        m.SenderDeviceName.ToLower().Contains(normalizedSearchTerm) ||
                        m.Attachments.Any(a => a.FileName.ToLower().Contains(normalizedSearchTerm)));

                var totalCount = await queryable.CountAsync();
                var skip = (page - 1) * pageSize;
                var messages = await queryable
                    .OrderByDescending(m => m.CreatedAt)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync();

                var results = new List<AnydropSearchResult>();

                foreach (var message in messages)
                {
                    var result = new AnydropSearchResult
                    {
                        Message = message,
                        HighlightedContent = HighlightSearchTerm(message.Content, searchTerm),
                        Score = CalculateRelevanceScore(message, searchTerm)
                    };

                    if (includeContext)
                    {
                        result.ContextMessages = await GetContextMessagesAsync(context, message.Id, DefaultContextMessageCount);
                    }

                    results.Add(result);
                }

                // Sort by relevance score
                results = results.OrderByDescending(r => r.Score).ToList();

                return new PagedResult<AnydropSearchResult>
                {
                    Items = results,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Anydrop messages for term: {SearchTerm}", searchTerm);

                return new PagedResult<AnydropSearchResult>();
            }
        }

        #endregion

        #region Link Metadata Operations

        /// <inheritdoc />
        public async Task<List<AnydropLinkMetadata>> ParseAndSaveLinkMetadataAsync(int messageId, string content, CancellationToken cancellationToken = default)
        {
            var results = new List<AnydropLinkMetadata>();

            try
            {
                var urls = ExtractUrls(content);

                if (!urls.Any())
                {
                    return results;
                }

                using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

                foreach (var url in urls.Take(MaxUrlsPerMessage))
                {
                    var metadata = await FetchLinkMetadataAsync(url, cancellationToken);
                    metadata.MessageId = messageId;

                    context.AnydropLinkMetadatas.Add(metadata);
                    results.Add(metadata);
                }

                await context.SaveChangesAsync(cancellationToken);
                OnMessagesChanged();
            }
            catch (OperationCanceledException)
            {
                _logger.LogDebug("Link metadata parsing was cancelled for message {MessageId}", messageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing link metadata for message {MessageId}", messageId);
            }

            return results;
        }

        /// <inheritdoc />
        public async Task<List<AnydropLinkMetadata>> GetLinkMetadataAsync(int messageId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                return await context.AnydropLinkMetadatas
                    .Where(l => l.MessageId == messageId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting link metadata for message {MessageId}", messageId);

                return new List<AnydropLinkMetadata>();
            }
        }

        #endregion

        #region Statistics

        /// <inheritdoc />
        public async Task<Dictionary<string, int>> GetStatisticsAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var stats = new Dictionary<string, int>
                {
                    ["TotalMessages"] = await context.AnydropMessages.CountAsync(),
                    ["UnreadMessages"] = await context.AnydropMessages.CountAsync(m => !m.IsRead),
                    ["TotalAttachments"] = await context.AnydropAttachments.CountAsync(),
                    ["ImageAttachments"] = await context.AnydropAttachments.CountAsync(a => a.ContentType.StartsWith("image/")),
                    ["VideoAttachments"] = await context.AnydropAttachments.CountAsync(a => a.ContentType.StartsWith("video/")),
                    ["AudioAttachments"] = await context.AnydropAttachments.CountAsync(a => a.ContentType.StartsWith("audio/")),
                    ["TodayMessages"] = await context.AnydropMessages.CountAsync(m => m.CreatedAt.Date == DateTime.UtcNow.Date),
                    ["TotalLinks"] = await context.AnydropLinkMetadatas.CountAsync()
                };

                return stats;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Anydrop statistics");

                return new Dictionary<string, int>();
            }
        }

        #endregion

        #region Private Helper Methods

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

        private void DeleteAttachmentFiles(AnydropAttachment attachment)
        {
            try
            {
                if (File.Exists(attachment.FilePath))
                {
                    File.Delete(attachment.FilePath);
                }

                if (!string.IsNullOrWhiteSpace(attachment.ThumbnailPath) && File.Exists(attachment.ThumbnailPath))
                {
                    File.Delete(attachment.ThumbnailPath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error deleting attachment files: {FilePath}", attachment.FilePath);
            }
        }

        private static MessageType DetermineMessageType(AnydropMessage message, AnydropAttachment attachment)
        {
            // If there's text content and attachments, it's mixed
            if (!string.IsNullOrWhiteSpace(message.Content) && message.Content.Trim().Length > 0)
            {
                return MessageType.Mixed;
            }

            // Determine by attachment type
            if (attachment.IsImage)
            {
                return MessageType.Image;
            }

            if (attachment.IsVideo)
            {
                return MessageType.Video;
            }

            if (attachment.IsAudio)
            {
                return MessageType.Audio;
            }

            return MessageType.Attachment;
        }

        private static string HighlightSearchTerm(string content, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(searchTerm))
            {
                return content;
            }

            // Use regex for case-insensitive replacement with highlighting
            var pattern = Regex.Escape(searchTerm);
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.Replace(content, match => $"<mark>{match.Value}</mark>");
        }

        private static double CalculateRelevanceScore(AnydropMessage message, string searchTerm)
        {
            double score = 0;
            var normalizedTerm = searchTerm.ToLowerInvariant();

            // Content match
            if (!string.IsNullOrWhiteSpace(message.Content))
            {
                var normalizedContent = message.Content.ToLowerInvariant();
                var matches = Regex.Matches(normalizedContent, Regex.Escape(normalizedTerm)).Count;
                score += matches * 10;

                // Boost for exact match at start
                if (normalizedContent.StartsWith(normalizedTerm))
                {
                    score += 20;
                }
            }

            // Device name match
            if (message.SenderDeviceName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            {
                score += 5;
            }

            // Attachment filename match
            foreach (var attachment in message.Attachments)
            {
                if (attachment.FileName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    score += 8;
                }
            }

            // Recency boost (newer messages score slightly higher)
            var daysSinceCreation = (DateTime.UtcNow - message.CreatedAt).TotalDays;

            if (daysSinceCreation < 7)
            {
                score += 5 - (daysSinceCreation * 0.5);
            }

            return score;
        }

        private async Task<List<AnydropMessage>> GetContextMessagesAsync(MingYueDbContext context, int messageId, int contextCount)
        {
            var message = await context.AnydropMessages.FindAsync(messageId);

            if (message is null)
            {
                return new List<AnydropMessage>();
            }

            // Get messages before and after
            var before = await context.AnydropMessages
                .Where(m => m.CreatedAt < message.CreatedAt)
                .OrderByDescending(m => m.CreatedAt)
                .Take(contextCount)
                .ToListAsync();

            var after = await context.AnydropMessages
                .Where(m => m.CreatedAt > message.CreatedAt)
                .OrderBy(m => m.CreatedAt)
                .Take(contextCount)
                .ToListAsync();

            var result = new List<AnydropMessage>();
            result.AddRange(before.OrderBy(m => m.CreatedAt));
            result.AddRange(after);

            return result;
        }

        private List<string> ExtractUrls(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return new List<string>();
            }

            var matches = UrlRegex().Matches(content);

            return matches
                .Select(m => m.Value)
                .Distinct()
                .ToList();
        }

        private async Task<AnydropLinkMetadata> FetchLinkMetadataAsync(string url, CancellationToken cancellationToken)
        {
            var metadata = new AnydropLinkMetadata
            {
                Url = url,
                FetchedAt = DateTime.UtcNow
            };

            try
            {
                using var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(LinkMetadataFetchTimeoutSeconds);
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MingYue Anydrop)");

                var response = await client.GetAsync(url, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    metadata.IsFetchSuccessful = false;
                    metadata.FetchError = $"HTTP {(int)response.StatusCode}";

                    return metadata;
                }

                var html = await response.Content.ReadAsStringAsync(cancellationToken);

                // Parse Open Graph and basic meta tags
                metadata.Title = ExtractMetaContent(html, "og:title") ?? ExtractTitle(html);
                metadata.Description = ExtractMetaContent(html, "og:description") ?? ExtractMetaContent(html, "description");
                metadata.ImageUrl = ExtractMetaContent(html, "og:image");
                metadata.SiteName = ExtractMetaContent(html, "og:site_name");

                // Extract favicon
                var faviconMatch = FaviconRegex().Match(html);

                if (faviconMatch.Success)
                {
                    var faviconHref = faviconMatch.Groups[1].Value;

                    if (!faviconHref.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    {
                        var uri = new Uri(url);
                        faviconHref = new Uri(uri, faviconHref).ToString();
                    }

                    metadata.FaviconUrl = faviconHref;
                }
                else
                {
                    // Default to /favicon.ico
                    var uri = new Uri(url);
                    metadata.FaviconUrl = $"{uri.Scheme}://{uri.Host}/favicon.ico";
                }

                metadata.IsFetchSuccessful = true;
            }
            catch (Exception ex)
            {
                metadata.IsFetchSuccessful = false;
                metadata.FetchError = ex.Message.Length > 500 ? ex.Message[..500] : ex.Message;
                _logger.LogWarning(ex, "Failed to fetch metadata for URL: {Url}", url);
            }

            return metadata;
        }

        private static string? ExtractMetaContent(string html, string property)
        {
            // Try og: property first
            var ogPattern = $@"<meta[^>]+(?:property|name)=[""']{Regex.Escape(property)}[""'][^>]+content=[""']([^""']+)[""']";
            var match = Regex.Match(html, ogPattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return System.Net.WebUtility.HtmlDecode(match.Groups[1].Value);
            }

            // Try reverse order (content before property)
            var reversePattern = $@"<meta[^>]+content=[""']([^""']+)[""'][^>]+(?:property|name)=[""']{Regex.Escape(property)}[""']";
            match = Regex.Match(html, reversePattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return System.Net.WebUtility.HtmlDecode(match.Groups[1].Value);
            }

            return null;
        }

        private static string? ExtractTitle(string html)
        {
            var match = Regex.Match(html, @"<title[^>]*>([^<]+)</title>", RegexOptions.IgnoreCase);

            return match.Success ? System.Net.WebUtility.HtmlDecode(match.Groups[1].Value.Trim()) : null;
        }

        [GeneratedRegex(@"https?://[^\s<>""']+", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        private static partial Regex UrlRegex();

        [GeneratedRegex(@"<link[^>]+rel=[""'](?:shortcut )?icon[""'][^>]+href=[""']([^""']+)[""']", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        private static partial Regex FaviconRegex();

        #endregion
    }
}
