namespace MingYue.Models
{
    public class SystemResourceInfo
    {
        public CpuInfo Cpu { get; set; } = new();
        public MemoryInfo Memory { get; set; } = new();
        public List<DiskInfo> Disks { get; set; } = new();
        public List<NetworkInfo> Networks { get; set; } = new();
    }

    public class CpuInfo
    {
        public double UsagePercent { get; set; }
        public int CoreCount { get; set; }
        public string ProcessorName { get; set; } = string.Empty;
    }

    public class MemoryInfo
    {
        public long TotalBytes { get; set; }
        public long UsedBytes { get; set; }
        public long AvailableBytes { get; set; }
        public double UsagePercent { get; set; }

        public string TotalGB => $"{TotalBytes / 1024.0 / 1024.0 / 1024.0:F2} GB";
        public string UsedGB => $"{UsedBytes / 1024.0 / 1024.0 / 1024.0:F2} GB";
        public string AvailableGB => $"{AvailableBytes / 1024.0 / 1024.0 / 1024.0:F2} GB";
    }

    public class DiskInfo
    {
        public string Name { get; set; } = string.Empty;
        public string MountPoint { get; set; } = string.Empty;
        public string FileSystem { get; set; } = string.Empty;
        public long TotalBytes { get; set; }
        public long UsedBytes { get; set; }
        public long AvailableBytes { get; set; }
        public double UsagePercent { get; set; }
        public bool IsReady { get; set; }

        // Enhanced properties for advanced disk management
        public string DevicePath { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // disk, part, loop, etc.
        public string UUID { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public bool IsRemovable { get; set; }
        public bool IsReadOnly { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public List<DiskInfo> Children { get; set; } = new();

        // Power management
        public bool? IsSpinningDown { get; set; }
        public int? ApmLevel { get; set; }

        public string TotalGB => $"{TotalBytes / 1024.0 / 1024.0 / 1024.0:F2} GB";
        public string UsedGB => $"{UsedBytes / 1024.0 / 1024.0 / 1024.0:F2} GB";
        public string AvailableGB => $"{AvailableBytes / 1024.0 / 1024.0 / 1024.0:F2} GB";
        public string SizeDisplay => TotalBytes > 0 ? TotalGB : "N/A";
    }

    public class NetworkInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long BytesSent { get; set; }
        public long BytesReceived { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<string> IpAddresses { get; set; } = new();

        public string SentMB => $"{BytesSent / 1024.0 / 1024.0:F2} MB";
        public string ReceivedMB => $"{BytesReceived / 1024.0 / 1024.0:F2} MB";
    }

    public enum NetworkDiskType
    {
        CIFS,
        NFS
    }

    public class NetworkDiskInfo
    {
        public string Server { get; set; } = string.Empty;
        public string SharePath { get; set; } = string.Empty;
        public string MountPoint { get; set; } = string.Empty;
        public NetworkDiskType DiskType { get; set; }
        public string FileSystem { get; set; } = string.Empty; // cifs, nfs, nfs4, etc.
        public long TotalBytes { get; set; }
        public long UsedBytes { get; set; }
        public long AvailableBytes { get; set; }
        public double UsagePercent { get; set; }
        public bool IsReady { get; set; }
        public string Options { get; set; } = string.Empty;

        public string TotalGB => $"{TotalBytes / 1024.0 / 1024.0 / 1024.0:F2} GB";
        public string UsedGB => $"{UsedBytes / 1024.0 / 1024.0 / 1024.0:F2} GB";
        public string AvailableGB => $"{AvailableBytes / 1024.0 / 1024.0 / 1024.0:F2} GB";
        public string SizeDisplay => TotalBytes > 0 ? TotalGB : "N/A";
        public string FullPath => DiskType == NetworkDiskType.NFS
            ? $"{Server}:/{SharePath}"
            : $"//{Server}/{SharePath}";
    }

    public class DiskPowerSettings
    {
        /// <summary>
        /// Spindown timeout in minutes. 0 means disabled.
        /// </summary>
        public int SpinDownTimeoutMinutes { get; set; }

        /// <summary>
        /// APM (Advanced Power Management) level. 1-255, where 1 = minimum power, 255 = maximum performance.
        /// null if not set.
        /// </summary>
        public int? ApmLevel { get; set; }
    }

    /// <summary>
    /// Result of a disk operation (mount, unmount, power management, etc.)
    /// </summary>
    public class DiskOperationResult
    {
        /// <summary>
        /// Whether the operation was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Human-readable message describing the result
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Optional error details if the operation failed
        /// </summary>
        public string? ErrorDetails { get; set; }

        /// <summary>
        /// Optional data associated with the operation result.
        /// This can contain operation-specific information such as disk information, mount details, etc.
        /// Common types: DiskInfo, string (device paths), null (for simple success/failure)
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// Creates a successful result
        /// </summary>
        public static DiskOperationResult Successful(string message, object? data = null) =>
            new() { Success = true, Message = message, Data = data };

        /// <summary>
        /// Creates a failed result
        /// </summary>
        public static DiskOperationResult Failed(string message, string? errorDetails = null) =>
            new() { Success = false, Message = message, ErrorDetails = errorDetails };
    }

    /// <summary>
    /// Result of a disk power status query
    /// </summary>
    public class PowerStatusResult
    {
        /// <summary>
        /// Power status constants for hdparm output
        /// </summary>
        public static class StatusValues
        {
            public const string Active = "active";
            public const string Idle = "idle";
            public const string Standby = "standby";
            public const string Sleeping = "sleeping";
            public const string ActiveIdle = "active/idle";
            public const string Unknown = "unknown";
        }

        /// <summary>
        /// Whether the query was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Human-readable message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Power status: "active", "standby", "sleeping", "idle", "active/idle", or "unknown"
        /// Use StatusValues constants for comparisons.
        /// </summary>
        public string Status { get; set; } = StatusValues.Unknown;

        /// <summary>
        /// Raw output from the power status command
        /// </summary>
        public string RawOutput { get; set; } = string.Empty;

        /// <summary>
        /// Creates a successful result
        /// </summary>
        public static PowerStatusResult Successful(string status, string rawOutput) =>
            new() { Success = true, Status = status, RawOutput = rawOutput, Message = $"磁盘电源状态: {status}" };

        /// <summary>
        /// Creates a failed result
        /// </summary>
        public static PowerStatusResult Failed(string message) =>
            new() { Success = false, Message = message, Status = StatusValues.Unknown };
    }

    /// <summary>
    /// SMART (Self-Monitoring, Analysis and Reporting Technology) information for a disk
    /// </summary>
    public class SmartInfo
    {
        /// <summary>
        /// Whether SMART is supported on this device
        /// </summary>
        public bool IsSupported { get; set; }

        /// <summary>
        /// Whether SMART is enabled on this device
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Overall health status (PASSED, FAILED, etc.)
        /// </summary>
        public string HealthStatus { get; set; } = string.Empty;

        /// <summary>
        /// Device model
        /// </summary>
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Serial number
        /// </summary>
        public string SerialNumber { get; set; } = string.Empty;

        /// <summary>
        /// Firmware version
        /// </summary>
        public string FirmwareVersion { get; set; } = string.Empty;

        /// <summary>
        /// Device capacity
        /// </summary>
        public string Capacity { get; set; } = string.Empty;

        /// <summary>
        /// Temperature in Celsius (if available)
        /// </summary>
        public int? Temperature { get; set; }

        /// <summary>
        /// Power-on hours
        /// </summary>
        public long? PowerOnHours { get; set; }

        /// <summary>
        /// Power cycle count
        /// </summary>
        public long? PowerCycleCount { get; set; }

        /// <summary>
        /// List of SMART attributes
        /// </summary>
        public List<SmartAttribute> Attributes { get; set; } = new();

        /// <summary>
        /// Raw output from smartctl
        /// </summary>
        public string RawOutput { get; set; } = string.Empty;

        /// <summary>
        /// Whether the query was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Error message if query failed
        /// </summary>
        public string? ErrorMessage { get; set; }
    }

    /// <summary>
    /// Individual SMART attribute
    /// </summary>
    public class SmartAttribute
    {
        /// <summary>
        /// Attribute ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Attribute name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Current value
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Worst value recorded
        /// </summary>
        public int Worst { get; set; }

        /// <summary>
        /// Threshold value
        /// </summary>
        public int Threshold { get; set; }

        /// <summary>
        /// Raw value
        /// </summary>
        public string RawValue { get; set; } = string.Empty;

        /// <summary>
        /// Whether this attribute has failed
        /// </summary>
        public bool Failed => Value <= Threshold && Threshold > 0;
    }

}
