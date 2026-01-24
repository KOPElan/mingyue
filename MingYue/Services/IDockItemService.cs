using MingYue.Models;

namespace MingYue.Services
{
    /// <summary>
    /// Provides CRUD operations for managing dock items
    /// </summary>
    public interface IDockItemService
    {
        /// <summary>
        /// Retrieves all dock items (both pinned and unpinned)
        /// </summary>
        /// <returns>A list of all dock items ordered by their display order</returns>
        Task<List<DockItem>> GetAllDockItemsAsync();

        /// <summary>
        /// Retrieves a dock item by its numeric identifier
        /// </summary>
        /// <param name="id">The dock item's unique numeric identifier</param>
        /// <returns>The dock item if found; otherwise, null</returns>
        Task<DockItem?> GetDockItemByIdAsync(int id);

        /// <summary>
        /// Creates a new dock item. ItemId is auto-generated if not provided.
        /// </summary>
        /// <param name="dockItem">The dock item to create</param>
        /// <returns>The created dock item with generated values</returns>
        Task<DockItem> CreateDockItemAsync(DockItem dockItem);

        /// <summary>
        /// Updates an existing dock item
        /// </summary>
        /// <param name="dockItem">The dock item with updated values</param>
        /// <returns>The updated dock item if successful; otherwise, null</returns>
        Task<DockItem?> UpdateDockItemAsync(DockItem dockItem);

        /// <summary>
        /// Deletes a dock item by its numeric identifier
        /// </summary>
        /// <param name="id">The dock item's unique numeric identifier</param>
        /// <returns>True if the deletion was successful; otherwise, false</returns>
        Task<bool> DeleteDockItemAsync(int id);

        /// <summary>
        /// Updates the display order of dock items
        /// </summary>
        /// <param name="dockItemIds">The ordered list of dock item identifiers</param>
        /// <returns>True if the reordering was successful; otherwise, false</returns>
        Task<bool> ReorderDockItemsAsync(List<int> dockItemIds);

        /// <summary>
        /// Creates a dock item from an existing application
        /// </summary>
        /// <param name="appId">The application's unique string identifier</param>
        /// <returns>The created dock item if successful; otherwise, null</returns>
        Task<DockItem?> PinApplicationToDockAsync(string appId);
    }
}
