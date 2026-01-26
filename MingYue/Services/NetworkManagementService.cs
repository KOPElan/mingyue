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

        public Task<List<NetworkInterfaceInfo>> GetAllNetworkInterfacesAsync()
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

            return Task.FromResult(interfaces);
        }

        public async Task<NetworkInterfaceInfo?> GetNetworkInterfaceAsync(string interfaceId)
        {
            var interfaces = await GetAllNetworkInterfacesAsync();
            return interfaces.FirstOrDefault(i => i.Id == interfaceId);
        }

        public Task<NetworkStatistics> GetNetworkStatisticsAsync()
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

            return Task.FromResult(stats);
        }

        public Task<NetworkStatistics?> GetInterfaceStatisticsAsync(string interfaceId)
        {
            try
            {
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                var ni = networkInterfaces.FirstOrDefault(i => i.Id == interfaceId);

                if (ni == null) return Task.FromResult<NetworkStatistics?>(null);

                var ipv4Stats = ni.GetIPv4Statistics();

                return Task.FromResult<NetworkStatistics?>(new NetworkStatistics
                {
                    TotalBytesReceived = ipv4Stats.BytesReceived,
                    TotalBytesSent = ipv4Stats.BytesSent,
                    TotalPacketsReceived = ipv4Stats.UnicastPacketsReceived,
                    TotalPacketsSent = ipv4Stats.UnicastPacketsSent,
                    CollectedAt = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting interface statistics for {InterfaceId}", interfaceId);
                return Task.FromResult<NetworkStatistics?>(null);
            }
        }

        public async Task<bool> SetInterfaceEnabledAsync(string interfaceId, bool enabled)
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
                
                // Whitelist-based validation: only allow alphanumeric, hyphen, underscore, and period
                var safeName = System.Text.RegularExpressions.Regex.Replace(ni.Name, @"[^a-zA-Z0-9\-_\.]", "");
                
                if (string.IsNullOrEmpty(safeName) || safeName != ni.Name)
                {
                    _logger.LogWarning("Interface name {InterfaceName} contains invalid characters", ni.Name);
                    return false;
                }
                
                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "/usr/sbin/ip",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                
                // Use ArgumentList for safer argument passing (no shell interpolation)
                process.StartInfo.ArgumentList.Add("link");
                process.StartInfo.ArgumentList.Add("set");
                process.StartInfo.ArgumentList.Add(safeName);
                process.StartInfo.ArgumentList.Add(action);

                process.Start();
                await process.WaitForExitAsync();

                if (process.ExitCode == 0)
                {
                    _logger.LogInformation("Network interface {InterfaceName} set to {State}", safeName, action);
                    return true;
                }
                else
                {
                    var error = await process.StandardError.ReadToEndAsync();
                    _logger.LogError("Failed to set network interface {InterfaceName} to {State}: {Error}", 
                        safeName, action, error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting interface {InterfaceId} enabled state", interfaceId);
                return false;
            }
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
