using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace MingYue.Services
{
    /// <summary>
    /// Service for managing network interfaces and configurations
    /// </summary>
    public class NetworkManagementService : INetworkManagementService
    {
        private readonly ILogger<NetworkManagementService> _logger;

        public NetworkManagementService(ILogger<NetworkManagementService> logger)
        {
            _logger = logger;
        }

        public async Task<List<NetworkInterfaceInfo>> GetAllNetworkInterfacesAsync()
        {
            return await Task.Run(() =>
            {
                var interfaces = new List<NetworkInterfaceInfo>();

                try
                {
                    var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                    foreach (var ni in networkInterfaces)
                    {
                        var interfaceInfo = new NetworkInterfaceInfo
                        {
                            Id = ni.Id,
                            Name = ni.Name,
                            Description = ni.Description,
                            Status = ni.OperationalStatus.ToString(),
                            Type = ni.NetworkInterfaceType.ToString(),
                            MacAddress = ni.GetPhysicalAddress().ToString(),
                            Speed = ni.Speed,
                            IsUp = ni.OperationalStatus == OperationalStatus.Up
                        };

                        // Get IP addresses
                        var ipProperties = ni.GetIPProperties();
                        var ipAddresses = ipProperties.UnicastAddresses
                            .Where(addr => addr.Address.AddressFamily == AddressFamily.InterNetwork || 
                                          addr.Address.AddressFamily == AddressFamily.InterNetworkV6)
                            .Select(addr => addr.Address.ToString())
                            .ToList();
                        interfaceInfo.IpAddresses = ipAddresses;

                        // Get statistics
                        var stats = ni.GetIPv4Statistics();
                        interfaceInfo.BytesReceived = stats.BytesReceived;
                        interfaceInfo.BytesSent = stats.BytesSent;

                        interfaces.Add(interfaceInfo);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting network interfaces");
                }

                return interfaces;
            });
        }

        public async Task<NetworkInterfaceInfo?> GetNetworkInterfaceAsync(string interfaceId)
        {
            var interfaces = await GetAllNetworkInterfacesAsync();
            return interfaces.FirstOrDefault(i => i.Id == interfaceId);
        }

        public async Task<NetworkStatistics> GetNetworkStatisticsAsync()
        {
            return await Task.Run(() =>
            {
                var stats = new NetworkStatistics
                {
                    CollectedAt = DateTime.Now
                };

                try
                {
                    var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                    foreach (var ni in networkInterfaces)
                    {
                        if (ni.OperationalStatus == OperationalStatus.Up)
                        {
                            var ipv4Stats = ni.GetIPv4Statistics();
                            stats.TotalBytesReceived += ipv4Stats.BytesReceived;
                            stats.TotalBytesSent += ipv4Stats.BytesSent;
                            stats.TotalPacketsReceived += ipv4Stats.UnicastPacketsReceived;
                            stats.TotalPacketsSent += ipv4Stats.UnicastPacketsSent;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting network statistics");
                }

                return stats;
            });
        }

        public async Task<NetworkStatistics?> GetInterfaceStatisticsAsync(string interfaceId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                    var ni = networkInterfaces.FirstOrDefault(i => i.Id == interfaceId);

                    if (ni == null) return null;

                    var ipv4Stats = ni.GetIPv4Statistics();

                    return new NetworkStatistics
                    {
                        TotalBytesReceived = ipv4Stats.BytesReceived,
                        TotalBytesSent = ipv4Stats.BytesSent,
                        TotalPacketsReceived = ipv4Stats.UnicastPacketsReceived,
                        TotalPacketsSent = ipv4Stats.UnicastPacketsSent,
                        CollectedAt = DateTime.Now
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting interface statistics for {InterfaceId}", interfaceId);
                    return null;
                }
            });
        }

        public async Task<bool> SetInterfaceEnabledAsync(string interfaceId, bool enabled)
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Note: Enabling/disabling network interfaces requires administrative privileges
                    // and is platform-specific. This is a simplified implementation.
                    
                    if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        _logger.LogWarning("Network interface management is only supported on Linux");
                        return false;
                    }

                    var interfaces = NetworkInterface.GetAllNetworkInterfaces();
                    var ni = interfaces.FirstOrDefault(i => i.Id == interfaceId);

                    if (ni == null)
                    {
                        _logger.LogWarning("Network interface {InterfaceId} not found", interfaceId);
                        return false;
                    }

                    // On Linux, use ip command to enable/disable interface
                    var action = enabled ? "up" : "down";
                    var process = new System.Diagnostics.Process
                    {
                        StartInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = "/bin/bash",
                            Arguments = $"-c \"ip link set {ni.Name} {action}\"",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    };

                    process.Start();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        _logger.LogInformation("Network interface {InterfaceName} set to {State}", ni.Name, action);
                        return true;
                    }
                    else
                    {
                        var error = process.StandardError.ReadToEnd();
                        _logger.LogError("Failed to set network interface {InterfaceName} to {State}: {Error}", 
                            ni.Name, action, error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error setting interface {InterfaceId} enabled state", interfaceId);
                    return false;
                }
            });
        }

        public async Task<bool> TestConnectivityAsync(string host, int timeout = 5000)
        {
            try
            {
                using var ping = new Ping();
                var reply = await ping.SendPingAsync(host, timeout);
                return reply.Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error testing connectivity to {Host}", host);
                return false;
            }
        }
    }
}
