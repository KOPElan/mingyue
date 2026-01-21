using MingYue.Models;

namespace MingYue.Services
{
    public interface IDockItemService
    {
        Task<List<DockItem>> GetAllDockItemsAsync();
        Task<DockItem?> GetDockItemByIdAsync(int id);
        Task<DockItem> CreateDockItemAsync(DockItem dockItem);
        Task<DockItem?> UpdateDockItemAsync(DockItem dockItem);
        Task<bool> DeleteDockItemAsync(int id);
        Task<bool> ReorderDockItemsAsync(List<int> dockItemIds);
        Task<DockItem?> PinApplicationToDockAsync(string appId);
    }
}
