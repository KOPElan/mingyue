using System.Net.NetworkInformation;

namespace MingYue.Services
{
    /// <summary>
    /// Network interface information
    /// </summary>
    public class NetworkInterfaceInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<string> IpAddresses { get; set; } = new();
        public string MacAddress { get; set; } = string.Empty;
        public long Speed { get; set; } // bits per second
        public long BytesReceived { get; set; }
        public long BytesSent { get; set; }
        public bool IsUp { get; set; }
    }

    /// <summary>
    /// Network statistics
    /// </summary>
    public class NetworkStatistics
    {
        public long TotalBytesReceived { get; set; }
        public long TotalBytesSent { get; set; }
        public long TotalPacketsReceived { get; set; }
        public long TotalPacketsSent { get; set; }
        public DateTime CollectedAt { get; set; }
    }

    /// <summary>
    /// Service for managing network interfaces and configurations
    /// </summary>
    public interface INetworkManagementService
    {
        /// <summary>
        /// Get all network interfaces
        /// </summary>
        Task<List<NetworkInterfaceInfo>> GetAllNetworkInterfacesAsync();

        /// <summary>
        /// Get a specific network interface by ID
        /// </summary>
        Task<NetworkInterfaceInfo?> GetNetworkInterfaceAsync(string interfaceId);

        /// <summary>
        /// Get network statistics for all interfaces
        /// </summary>
        Task<NetworkStatistics> GetNetworkStatisticsAsync();

        /// <summary>
        /// Get network statistics for a specific interface
        /// </summary>
        Task<NetworkStatistics?> GetInterfaceStatisticsAsync(string interfaceId);

        /// <summary>
        /// Enable or disable a network interface
        /// </summary>
        Task<bool> SetInterfaceEnabledAsync(string interfaceId, bool enabled);

        /// <summary>
        /// Test network connectivity to a host
        /// </summary>
        Task<bool> TestConnectivityAsync(string host, int timeout = 5000);
    }
}
