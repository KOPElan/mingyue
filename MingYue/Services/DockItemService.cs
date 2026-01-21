using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;

namespace MingYue.Services
{
    public class DockItemService : IDockItemService
    {
        private readonly IDbContextFactory<MingYueDbContext> _dbFactory;
        private readonly ILogger<DockItemService> _logger;

        public DockItemService(
            IDbContextFactory<MingYueDbContext> dbFactory,
            ILogger<DockItemService> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
        }

        public async Task<List<DockItem>> GetAllDockItemsAsync()
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                return await context.DockItems
                    .AsNoTracking()
                    .Where(d => d.IsPinned)
                    .OrderBy(d => d.Order)
                    .ThenBy(d => d.Title)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all dock items");
                return new List<DockItem>();
            }
        }

        public async Task<DockItem?> GetDockItemByIdAsync(int id)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                return await context.DockItems
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dock item by ID {Id}", id);
                return null;
            }
        }

        public async Task<DockItem> CreateDockItemAsync(DockItem dockItem)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();

                // Generate unique ItemId if not provided
                if (string.IsNullOrEmpty(dockItem.ItemId))
                {
                    dockItem.ItemId = Guid.NewGuid().ToString();
                }

                // Set timestamps
                dockItem.CreatedAt = DateTime.UtcNow;
                dockItem.UpdatedAt = DateTime.UtcNow;

                // Get max order and set new order
                var maxOrder = await context.DockItems.MaxAsync(d => (int?)d.Order) ?? 0;
                dockItem.Order = maxOrder + 1;

                context.DockItems.Add(dockItem);
                await context.SaveChangesAsync();

                _logger.LogInformation("Dock item {Title} created with ID {Id}", 
                    dockItem.Title, dockItem.Id);

                return dockItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating dock item {Title}", dockItem.Title);
                throw;
            }
        }

        public async Task<DockItem?> UpdateDockItemAsync(DockItem dockItem)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();

                var existingItem = await context.DockItems.FindAsync(dockItem.Id);
                if (existingItem == null)
                {
                    _logger.LogWarning("Dock item with ID {Id} not found for update", dockItem.Id);
                    return null;
                }

                // Update properties
                existingItem.Title = dockItem.Title;
                existingItem.Url = dockItem.Url;
                existingItem.Icon = dockItem.Icon;
                existingItem.IconBackground = dockItem.IconBackground;
                existingItem.IconColor = dockItem.IconColor;
                existingItem.IsPinned = dockItem.IsPinned;
                existingItem.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync();

                _logger.LogInformation("Dock item {Title} updated", dockItem.Title);

                return existingItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating dock item {Id}", dockItem.Id);
                return null;
            }
        }

        public async Task<bool> DeleteDockItemAsync(int id)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();

                var dockItem = await context.DockItems.FindAsync(id);
                if (dockItem == null)
                {
                    _logger.LogWarning("Dock item with ID {Id} not found for deletion", id);
                    return false;
                }

                context.DockItems.Remove(dockItem);
                await context.SaveChangesAsync();

                _logger.LogInformation("Dock item {Title} deleted", dockItem.Title);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting dock item {Id}", id);
                return false;
            }
        }

        public async Task<bool> ReorderDockItemsAsync(List<int> dockItemIds)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();

                for (int i = 0; i < dockItemIds.Count; i++)
                {
                    var item = await context.DockItems.FindAsync(dockItemIds[i]);
                    if (item != null)
                    {
                        item.Order = i;
                        item.UpdatedAt = DateTime.UtcNow;
                    }
                }

                await context.SaveChangesAsync();

                _logger.LogInformation("Dock items reordered successfully");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reordering dock items");
                return false;
            }
        }

        public async Task<DockItem?> PinApplicationToDockAsync(string appId)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();

                // Get the application
                var application = await context.Applications
                    .FirstOrDefaultAsync(a => a.AppId == appId);

                if (application == null)
                {
                    _logger.LogWarning("Application with AppId {AppId} not found", appId);
                    return null;
                }

                // Check if already pinned
                var existingDockItem = await context.DockItems
                    .FirstOrDefaultAsync(d => d.AssociatedAppId == appId);

                if (existingDockItem != null)
                {
                    _logger.LogInformation("Application {Title} is already pinned to dock", 
                        application.Title);
                    return existingDockItem;
                }

                // Create new dock item
                var dockItem = new DockItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    Title = application.Title,
                    Url = application.Url,
                    Icon = application.Icon,
                    IconColor = application.IconColor,
                    IconBackground = string.Empty,
                    AssociatedAppId = appId,
                    IsPinned = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Get max order and set new order
                var maxOrder = await context.DockItems.MaxAsync(d => (int?)d.Order) ?? 0;
                dockItem.Order = maxOrder + 1;

                context.DockItems.Add(dockItem);
                await context.SaveChangesAsync();

                _logger.LogInformation("Application {Title} pinned to dock", application.Title);

                return dockItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error pinning application {AppId} to dock", appId);
                return null;
            }
        }
    }
}
