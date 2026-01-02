using MingYue.Models;

namespace MingYue.Services
{
    public interface ISystemMonitorService
    {
        Task<SystemResourceInfo> GetSystemResourceInfoAsync();
        Task<CpuInfo> GetCpuInfoAsync();
        Task<MemoryInfo> GetMemoryInfoAsync();
        Task<List<NetworkInfo>> GetNetworkInfoAsync();
    }
}
