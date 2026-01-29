using MingYue.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MingYue.Services
{
    public class DiskManagementService : IDiskManagementService
    {
        ILogger<DiskManagementService> _logger;
        public DiskManagementService(ILogger<DiskManagementService> logger) { _logger = logger; }

        private static readonly string[] InvalidChars = ["&&", ";", "|", "`", "$", "(", ")", "<", ">", "\"", "'", "\n", "\r", " "];
        private static readonly char[] InvalidCredentialChars = ['\n', '\r', '='];
        private static readonly HashSet<char> InvalidCredentialCharsSet = new(InvalidCredentialChars);
        private static readonly Regex WhitespaceRegex = new(@"\s+", RegexOptions.Compiled);
        private static readonly Regex SafeNameRegex = new(@"^[a-zA-Z0-9_.\-]+$", RegexOptions.Compiled);

        // Known valid hdparm flags (without assignment)
        private static readonly HashSet<string> ValidHdparmFlags = new(StringComparer.OrdinalIgnoreCase)
    {
        "quiet", "standby", "sleep", "disable_seagate"
    };

        // Known valid hdparm parameters (with assignment)
        private static readonly HashSet<string> ValidHdparmParams = new(StringComparer.OrdinalIgnoreCase)
    {
        "read_ahead_sect", "lookahead", "bus", "apm", "apm_battery", "io32_support",
        "dma", "defect_mgmt", "cd_speed", "keep_settings_over_reset",
        "keep_features_over_reset", "mult_sect_io", "prefetch_sect", "read_only",
        "write_read_verify", "poweron_standby", "spindown_time", "force_spindown_time",
        "interrupt_unmask", "write_cache", "transfer_mode", "acoustic_management",
        "chipset_pio_mode", "security_freeze", "security_unlock", "security_pass",
        "security_disable", "user-master", "security_mode"
    };

        private static readonly Regex HdparmSettingRegex = new(@"^\s*([a-z_-]+)\s*=\s*(.+)\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Check if an error indicates permission issues and return appropriate error result
        /// </summary>
        private DiskOperationResult CheckPermissionError(string error, string operation)
        {
            // Check for specific sudo permission errors (legacy, when systemd uses sudo)
            if (error.Contains("sudo: a password is required") || 
                error.Contains("sudo: password is required") ||
                (error.Contains("sudo") && error.Contains("not allowed to execute")))
            {
                _logger.LogError("{Operation} failed due to sudo permission issue. Error: {Error}", operation, error);
                return DiskOperationResult.Failed($"{operation}失败：需要配置 sudo 权限", 
                    "请配置 sudoers 文件允许无密码执行 mount/umount 命令。参考文档：sudo visudo -f /etc/sudoers.d/mingyue");
            }
            
            // Check for "no new privileges" error (legacy, should not occur with AmbientCapabilities)
            if (error.Contains("no new privileges") || error.Contains("已设置'no new privileges'标志") || error.Contains("已设置\"no new privileges\"标志"))
            {
                _logger.LogError("{Operation} failed due to 'no new privileges' restriction. Error: {Error}", operation, error);
                return DiskOperationResult.Failed($"{operation}失败：服务被 NoNewPrivileges 限制", 
                    "systemd 服务配置阻止了 sudo 权限提升。解决方法请参考 CONFIGURATION.md 中的 'Permission errors (disk mount, file operations, service management)' 章节");
            }
            
            // Check for capability/permission errors when using direct mount (without sudo)
            if (error.Contains("Operation not permitted") || error.Contains("Permission denied") || 
                error.Contains("不允许的操作") || error.Contains("权限不够"))
            {
                _logger.LogError("{Operation} failed due to insufficient capabilities. Error: {Error}", operation, error);
                return DiskOperationResult.Failed($"{operation}失败：权限不足", 
                    "服务缺少必要的系统权限。请确保 systemd 服务配置中包含 AmbientCapabilities=CAP_SYS_ADMIN 并执行 'sudo systemctl daemon-reload && sudo systemctl restart mingyue'");
            }
            
            return null!; // Return null to indicate no permission error detected
        }

        public async Task<List<DiskInfo>> GetAllDisksAsync()
        {
            // On Linux, use GetAllBlockDevicesAsync for better device information and filtering
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var allDevices = await GetAllBlockDevicesAsync();
                return FilterLocalDisks(allDevices);
            }

            // Fallback for non-Linux systems
            var disks = new List<DiskInfo>();

            foreach (var drive in DriveInfo.GetDrives())
            {
                try
                {
                    var diskInfo = new DiskInfo
                    {
                        Name = drive.Name,
                        DevicePath = drive.Name,
                        MountPoint = drive.RootDirectory.FullName,
                        FileSystem = drive.IsReady ? drive.DriveFormat : "Unknown",
                        IsReady = drive.IsReady
                    };

                    if (drive.IsReady)
                    {
                        diskInfo.TotalBytes = drive.TotalSize;
                        diskInfo.AvailableBytes = drive.AvailableFreeSpace;
                        diskInfo.UsedBytes = drive.TotalSize - drive.AvailableFreeSpace;
                        diskInfo.UsagePercent = diskInfo.TotalBytes > 0
                            ? Math.Round((double)diskInfo.UsedBytes / diskInfo.TotalBytes * 100, 2)
                            : 0;
                    }

                    disks.Add(diskInfo);
                }
                catch
                {
                    // Skip drives that can't be accessed
                    continue;
                }
            }

            return disks;
        }

        /// <summary>
        /// Filter disks to show only local, internal, mounted, and external disks.
        /// Excludes: loop devices, ram disks, rom/cd drives, and other virtual devices.
        /// </summary>
        private List<DiskInfo> FilterLocalDisks(List<DiskInfo> allDisks)
        {
            var filtered = new List<DiskInfo>();

            foreach (var disk in allDisks)
            {
                // Allow:
                //   - physical disks and their partitions (Type == "disk" / "part")
                //   - non-virtual devices that are mounted or ready (e.g., lvm/crypt/raid)
                // Exclude clearly virtual devices such as loop/ram/rom, even if they appear.
                var isDiskOrPart =
                    disk.Type.Equals("disk", StringComparison.OrdinalIgnoreCase) ||
                    disk.Type.Equals("part", StringComparison.OrdinalIgnoreCase);

                // Known virtual / ephemeral block device types to exclude
                var isVirtualType =
                    disk.Type.Equals("loop", StringComparison.OrdinalIgnoreCase) ||
                    disk.Type.Equals("ram", StringComparison.OrdinalIgnoreCase) ||
                    disk.Type.Equals("zram", StringComparison.OrdinalIgnoreCase) ||
                    disk.Type.Equals("rom", StringComparison.OrdinalIgnoreCase);

                var hasMountOrReady =
                    !string.IsNullOrWhiteSpace(disk.MountPoint) ||
                    disk.IsReady;

                if (isDiskOrPart || (!isVirtualType && hasMountOrReady))
                {
                    // Exclude loop devices (virtual block devices) by name as a safeguard
                    if (disk.Name.StartsWith("loop", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // Create a shallow copy to avoid mutating original object
                    var filteredDisk = new DiskInfo
                    {
                        Name = disk.Name,
                        DevicePath = disk.DevicePath,
                        Type = disk.Type,
                        TotalBytes = disk.TotalBytes,
                        FileSystem = disk.FileSystem,
                        UUID = disk.UUID,
                        Label = disk.Label,
                        IsRemovable = disk.IsRemovable,
                        IsReadOnly = disk.IsReadOnly,
                        Model = disk.Model,
                        Serial = disk.Serial,
                        MountPoint = disk.MountPoint,
                        IsReady = disk.IsReady,
                        UsedBytes = disk.UsedBytes,
                        AvailableBytes = disk.AvailableBytes,
                        UsagePercent = disk.UsagePercent,
                        IsSpinningDown = disk.IsSpinningDown,
                        ApmLevel = disk.ApmLevel,
                        // Recursively filter children
                        Children = disk.Children.Count > 0 ? FilterLocalDisks(disk.Children) : new List<DiskInfo>()
                    };

                    filtered.Add(filteredDisk);
                }
            }

            return filtered;
        }

        public async Task<List<DiskInfo>> GetAllBlockDevicesAsync()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // Fall back to basic disk info on non-Linux systems
                return await GetAllDisksAsync();
            }

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "lsblk",
                    Arguments = "-J -b -o NAME,TYPE,SIZE,MOUNTPOINT,FSTYPE,UUID,LABEL,RM,RO,MODEL,SERIAL",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(processInfo);
                if (process == null)
                {
                    return await GetAllDisksAsync();
                }

                await process.WaitForExitAsync();
                var output = await process.StandardOutput.ReadToEndAsync();

                if (process.ExitCode != 0)
                {
                    return await GetAllDisksAsync();
                }

                var lsblkData = JsonSerializer.Deserialize<LsblkOutput>(
                    output,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (lsblkData?.Blockdevices == null)
                {
                    return await GetAllDisksAsync();
                }

                var disks = new List<DiskInfo>();
                foreach (var device in lsblkData.Blockdevices)
                {
                    var diskInfo = ConvertLsblkDevice(device);
                    disks.Add(diskInfo);
                }

                // Get usage information for mounted disks
                await EnrichWithUsageInfo(disks);

                return disks;
            }
            catch
            {
                return await GetAllDisksAsync();
            }
        }

        private DiskInfo ConvertLsblkDevice(LsblkDevice device)
        {
            var diskInfo = new DiskInfo
            {
                Name = device.Name ?? "",
                DevicePath = $"/dev/{device.Name}",
                Type = device.Type ?? "",
                TotalBytes = device.Size,
                FileSystem = device.Fstype ?? "",
                UUID = device.Uuid ?? "",
                Label = device.Label ?? "",
                IsRemovable = device.Rm,
                IsReadOnly = device.Ro,
                Model = device.Model ?? "",
                Serial = device.Serial ?? "",
                MountPoint = device.Mountpoint ?? "",
                IsReady = !string.IsNullOrEmpty(device.Mountpoint)
            };

            if (device.Children != null)
            {
                foreach (var child in device.Children)
                {
                    diskInfo.Children.Add(ConvertLsblkDevice(child));
                }
            }

            return diskInfo;
        }

        private async Task EnrichWithUsageInfo(List<DiskInfo> disks)
        {
            foreach (var disk in disks)
            {
                if (!string.IsNullOrEmpty(disk.MountPoint) && Directory.Exists(disk.MountPoint))
                {
                    try
                    {
                        var driveInfo = new DriveInfo(disk.MountPoint);
                        if (driveInfo.IsReady)
                        {
                            disk.TotalBytes = driveInfo.TotalSize;
                            disk.AvailableBytes = driveInfo.AvailableFreeSpace;
                            disk.UsedBytes = driveInfo.TotalSize - driveInfo.AvailableFreeSpace;
                            disk.UsagePercent = disk.TotalBytes > 0
                                ? Math.Round((double)disk.UsedBytes / disk.TotalBytes * 100, 2)
                                : 0;
                        }
                    }
                    catch
                    {
                        // Ignore errors getting usage info
                    }
                }

                if (disk.Children.Count > 0)
                {
                    await EnrichWithUsageInfo(disk.Children);
                }
            }
        }

        public async Task<DiskInfo?> GetDiskInfoAsync(string devicePath)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return null;
            }

            if (!ValidateDevicePath(devicePath))
            {
                return null;
            }

            var allDisks = await GetAllBlockDevicesAsync();
            return FindDiskByPath(allDisks, devicePath);
        }

        private DiskInfo? FindDiskByPath(List<DiskInfo> disks, string devicePath)
        {
            foreach (var disk in disks)
            {
                if (disk.DevicePath == devicePath)
                {
                    return disk;
                }

                if (disk.Children.Count > 0)
                {
                    var found = FindDiskByPath(disk.Children, devicePath);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }

            return null;
        }

        public async Task<DiskOperationResult> MountDiskAsync(string devicePath, string mountPoint, string? fileSystem = null, string? options = null)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return DiskOperationResult.Failed("磁盘挂载仅在 Linux 系统上支持");
            }

            if (!ValidateDevicePath(devicePath))
            {
                return DiskOperationResult.Failed("无效的设备路径");
            }

            if (!ValidateMountPoint(mountPoint))
            {
                return DiskOperationResult.Failed("无效的挂载点");
            }

            if (fileSystem != null && InvalidChars.Any(c => fileSystem.Contains(c)))
            {
                return DiskOperationResult.Failed("文件系统类型包含无效字符");
            }

            if (options != null && InvalidChars.Any(c => options.Contains(c)))
            {
                return DiskOperationResult.Failed("挂载选项包含无效字符");
            }

            try
            {
                // Create mount point if it doesn't exist
                if (!Directory.Exists(mountPoint))
                {
                    Directory.CreateDirectory(mountPoint);
                }

                var processInfo = new ProcessStartInfo
                {
                    FileName = "sudo",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Build mount command arguments using ArgumentList for proper escaping
                processInfo.ArgumentList.Add("mount");
                
                if (!string.IsNullOrEmpty(fileSystem))
                {
                    processInfo.ArgumentList.Add("-t");
                    processInfo.ArgumentList.Add(fileSystem);
                }
                
                if (!string.IsNullOrEmpty(options))
                {
                    processInfo.ArgumentList.Add("-o");
                    processInfo.ArgumentList.Add(options);
                }
                
                processInfo.ArgumentList.Add(devicePath);
                processInfo.ArgumentList.Add(mountPoint);

                using var process = Process.Start(processInfo);
                if (process != null)
                {
                    await process.WaitForExitAsync();
                    var output = await process.StandardOutput.ReadToEndAsync();
                    var error = await process.StandardError.ReadToEndAsync();

                    if (process.ExitCode == 0)
                    {
                        return DiskOperationResult.Successful($"成功将 {devicePath} 挂载到 {mountPoint}");
                    }
                    else
                    {
                        // Check if the error is due to permission issues
                        var permError = CheckPermissionError(error, "挂载");
                        if (permError != null)
                        {
                            return permError;
                        }
                        return DiskOperationResult.Failed($"挂载失败", error);
                    }
                }

                return DiskOperationResult.Failed("无法启动挂载进程");
            }
            catch (Exception ex)
            {
                return DiskOperationResult.Failed("挂载磁盘时出错", ex.Message);
            }
        }

        public async Task<DiskOperationResult> MountDiskPermanentAsync(string devicePath, string mountPoint, string? fileSystem = null, string? options = null)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return DiskOperationResult.Failed("永久挂载磁盘仅在 Linux 系统上支持");
            }

            // First mount temporarily
            var mountResult = await MountDiskAsync(devicePath, mountPoint, fileSystem, options);
            if (!mountResult.Success)
            {
                return mountResult;
            }

            try
            {
                // Get disk info to find UUID
                var diskInfo = await GetDiskInfoAsync(devicePath);
                if (diskInfo == null)
                {
                    return DiskOperationResult.Failed("无法获取磁盘信息");
                }

                var fstabEntry = string.IsNullOrEmpty(diskInfo.UUID)
                    ? devicePath
                    : $"UUID={diskInfo.UUID}";

                fstabEntry += $" {mountPoint}";
                fstabEntry += $" {(string.IsNullOrEmpty(fileSystem) ? (diskInfo.FileSystem ?? "auto") : fileSystem)}";
                fstabEntry += $" {(string.IsNullOrEmpty(options) ? "defaults" : options)}";
                fstabEntry += " 0 2";

                // Use temp file approach for ProtectSystem=full compatibility
                try
                {
                    var fstabPath = "/etc/fstab";
                    string[] existingLines;

                    try
                    {
                        existingLines = await File.ReadAllLinesAsync(fstabPath);
                    }
                    catch (FileNotFoundException)
                    {
                        existingLines = Array.Empty<string>();
                    }

                    foreach (var line in existingLines)
                    {
                        if (line.Trim() == fstabEntry)
                        {
                            return DiskOperationResult.Successful($"成功将 {devicePath} 挂载到 {mountPoint}；/etc/fstab 中已存在匹配条目");
                        }
                    }

                    // Create temp file in /tmp (writable with PrivateTmp=true)
                    var tempPath = Path.Combine("/tmp", $"fstab.{Guid.NewGuid():N}.tmp");
                    var newContent = string.Join(Environment.NewLine, existingLines) + 
                                   (existingLines.Length > 0 ? Environment.NewLine : "") + 
                                   fstabEntry + Environment.NewLine;
                    
                    await File.WriteAllTextAsync(tempPath, newContent);
                    
                    // Use File.Move with overwrite (works with CAP_DAC_OVERRIDE even on read-only /etc)
                    File.Move(tempPath, fstabPath, overwrite: true);
                    
                    return DiskOperationResult.Successful($"成功将 {devicePath} 挂载到 {mountPoint} 并添加到 /etc/fstab");
                }
                catch (UnauthorizedAccessException ex)
                {
                    _logger.LogError(ex, "Permission denied when writing to /etc/fstab for device '{Device}'", devicePath);
                    return DiskOperationResult.Failed($"挂载成功但写入 /etc/fstab 时权限被拒绝。条目: {fstabEntry}");
                }
                catch (IOException ex)
                {
                    _logger.LogError(ex, "I/O error when writing to /etc/fstab for device '{Device}'", devicePath);
                    return DiskOperationResult.Failed($"挂载成功但写入 /etc/fstab 时出错", $"{ex.Message}。条目: {fstabEntry}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error appending to /etc/fstab for device '{Device}' at mount point '{MountPoint}'", devicePath, mountPoint);
                    Debug.WriteLine($"Error appending to /etc/fstab for device '{devicePath}' at mount point '{mountPoint}'. Exception: {ex}");
                    return DiskOperationResult.Failed($"挂载成功但更新 /etc/fstab 时出错", $"{ex.Message}。条目: {fstabEntry}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during permanent mount setup for device '{devicePath}' at mount point '{mountPoint}'. Exception: {ex}");
                return DiskOperationResult.Failed("挂载成功但更新 /etc/fstab 时出错", ex.Message);
            }
        }

        public async Task<DiskOperationResult> UnmountDiskAsync(string mountPoint)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return DiskOperationResult.Failed("磁盘卸载仅在 Linux 系统上支持");
            }

            if (!ValidateMountPoint(mountPoint))
            {
                return DiskOperationResult.Failed("无效的挂载点");
            }

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "sudo",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Build umount command arguments using ArgumentList for proper escaping
                processInfo.ArgumentList.Add("umount");
                processInfo.ArgumentList.Add(mountPoint);

                using var process = Process.Start(processInfo);
                if (process != null)
                {
                    await process.WaitForExitAsync();
                    var output = await process.StandardOutput.ReadToEndAsync();
                    var error = await process.StandardError.ReadToEndAsync();

                    if (process.ExitCode == 0)
                    {
                        return DiskOperationResult.Successful($"成功卸载 {mountPoint}");
                    }
                    else
                    {
                        // Check if the error is due to permission issues
                        var permError = CheckPermissionError(error, "卸载");
                        if (permError != null)
                        {
                            return permError;
                        }
                        return DiskOperationResult.Failed($"卸载失败", error);
                    }
                }

                return DiskOperationResult.Failed("无法启动卸载进程");
            }
            catch (Exception ex)
            {
                return DiskOperationResult.Failed("卸载磁盘时出错", ex.Message);
            }
        }

        public async Task<List<string>> GetSharesAsync()
        {
            var shares = new List<string>();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // Read Samba shares configuration
                try
                {
                    var sambaConfigPath = "/etc/samba/smb.conf";
                    if (File.Exists(sambaConfigPath))
                    {
                        var lines = await File.ReadAllLinesAsync(sambaConfigPath);
                        foreach (var line in lines)
                        {
                            if (line.Trim().StartsWith("[") && line.Trim().EndsWith("]"))
                            {
                                var shareName = line.Trim().Trim('[', ']');
                                if (shareName != "global" && shareName != "homes" && shareName != "printers")
                                {
                                    shares.Add(shareName);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // Ignore errors reading Samba config
                }
            }

            return shares;
        }

        public Task<List<string>> GetAvailableFileSystemsAsync()
        {
            // Common Linux file systems
            var fileSystems = new List<string>
        {
            "ext4",
            "ext3",
            "ext2",
            "btrfs",
            "xfs",
            "ntfs",
            "vfat",
            "exfat"
        };

            return Task.FromResult(fileSystems);
        }

        public async Task<DiskOperationResult> SetDiskSpinDownAsync(string devicePath, int timeoutMinutes)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return DiskOperationResult.Failed("磁盘电源管理仅在 Linux 系统上支持");
            }

            if (!ValidateDevicePath(devicePath))
            {
                return DiskOperationResult.Failed("无效的设备路径");
            }

            if (timeoutMinutes < 0 || timeoutMinutes > 330)
            {
                return DiskOperationResult.Failed("超时时间必须在 0 到 330 分钟之间（0 = 禁用，最多 5.5 小时）");
            }

            try
            {
                // Update /etc/hdparm.conf for persistent settings
                var confResult = await UpdateHdparmConfAsync(devicePath, spindownTime: timeoutMinutes);
                if (!confResult.Success)
                {
                    return DiskOperationResult.Failed(confResult.Message);
                }

                // Convert minutes to hdparm encoding (0-255)
                var hdparmValue = ConvertMinutesToHdparmEncoding(timeoutMinutes).ToString();

                // Also apply immediately using hdparm command
                var processInfo = new ProcessStartInfo
                {
                    FileName = "hdparm",
                    Arguments = $"-S {hdparmValue} {devicePath}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(processInfo);
                if (process != null)
                {
                    await process.WaitForExitAsync();
                    var output = await process.StandardOutput.ReadToEndAsync();
                    var error = await process.StandardError.ReadToEndAsync();

                    if (process.ExitCode == 0)
                    {
                        var msg = timeoutMinutes == 0
                            ? $"成功为 {devicePath} 禁用自动休眠（持久配置）"
                            : $"成功为 {devicePath} 设置自动休眠时间为 {timeoutMinutes} 分钟（持久配置）";
                        return DiskOperationResult.Successful(msg);
                    }
                    else
                    {
                        return DiskOperationResult.Failed("配置已保存，但立即应用失败", error);
                    }
                }

                return DiskOperationResult.Failed("配置已保存，但无法启动 hdparm 进程");
            }
            catch (Exception ex)
            {
                return DiskOperationResult.Failed("设置磁盘自动休眠时出错", ex.Message);
            }
        }

        public async Task<DiskOperationResult> SetDiskApmLevelAsync(string devicePath, int level)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return DiskOperationResult.Failed("磁盘电源管理仅在 Linux 系统上支持");
            }

            if (!ValidateDevicePath(devicePath))
            {
                return DiskOperationResult.Failed("无效的设备路径");
            }

            if (level < 1 || level > 255)
            {
                return DiskOperationResult.Failed("APM 级别必须在 1 到 255 之间（1=最低功耗，255=最高性能）");
            }

            try
            {
                // Update /etc/hdparm.conf for persistent settings
                var confResult = await UpdateHdparmConfAsync(devicePath, apmLevel: level);
                if (!confResult.Success)
                {
                    return DiskOperationResult.Failed(confResult.Message);
                }

                // Also apply immediately using hdparm command
                var processInfo = new ProcessStartInfo
                {
                    FileName = "hdparm",
                    Arguments = $"-B {level} {devicePath}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(processInfo);
                if (process != null)
                {
                    await process.WaitForExitAsync();
                    var output = await process.StandardOutput.ReadToEndAsync();
                    var error = await process.StandardError.ReadToEndAsync();

                    return process.ExitCode == 0
                        ? DiskOperationResult.Successful($"成功为 {devicePath} 设置 APM 级别为 {level}（持久配置）")
                        : DiskOperationResult.Failed("配置已保存，但立即应用失败", error);
                }

                return DiskOperationResult.Failed("配置已保存，但无法启动 hdparm 进程");
            }
            catch (Exception ex)
            {
                return DiskOperationResult.Failed("设置 APM 级别时出错", ex.Message);
            }
        }

        public async Task<PowerStatusResult> GetDiskPowerStatusAsync(string devicePath)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return PowerStatusResult.Failed("磁盘电源状态检查仅在 Linux 系统上支持");
            }

            if (!ValidateDevicePath(devicePath))
            {
                return PowerStatusResult.Failed("无效的设备路径");
            }

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "hdparm",
                    Arguments = $"-C {devicePath}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(processInfo);
                if (process != null)
                {
                    await process.WaitForExitAsync();
                    var output = await process.StandardOutput.ReadToEndAsync();
                    var error = await process.StandardError.ReadToEndAsync();

                    if (process.ExitCode == 0)
                    {
                        // Parse the output to extract status
                        // Output format: "/dev/sda:\n drive state is:  active/idle\n"
                        var status = PowerStatusResult.StatusValues.Unknown;

                        // Look for the line containing "drive state is:" or "drive state:"
                        var statusLine = output
                            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(line => line.Trim())
                            .FirstOrDefault(line =>
                                line.Contains("drive state is:", StringComparison.OrdinalIgnoreCase) ||
                                line.Contains("drive state:", StringComparison.OrdinalIgnoreCase));

                        if (statusLine != null)
                        {
                            // Extract the part after the colon
                            var colonIndex = statusLine.IndexOf(':');
                            if (colonIndex >= 0 && colonIndex + 1 < statusLine.Length)
                            {
                                var statePart = statusLine[(colonIndex + 1)..].Trim();

                                // Check for specific states in priority order
                                // Check combined states first, then individual states
                                if (statePart.Equals(PowerStatusResult.StatusValues.ActiveIdle, StringComparison.OrdinalIgnoreCase))
                                {
                                    status = PowerStatusResult.StatusValues.ActiveIdle;
                                }
                                else if (statePart.Contains(PowerStatusResult.StatusValues.Standby, StringComparison.OrdinalIgnoreCase))
                                {
                                    status = PowerStatusResult.StatusValues.Standby;
                                }
                                else if (statePart.Contains(PowerStatusResult.StatusValues.Sleeping, StringComparison.OrdinalIgnoreCase))
                                {
                                    status = PowerStatusResult.StatusValues.Sleeping;
                                }
                                else if (statePart.Contains(PowerStatusResult.StatusValues.Active, StringComparison.OrdinalIgnoreCase))
                                {
                                    status = PowerStatusResult.StatusValues.Active;
                                }
                                else if (statePart.Contains(PowerStatusResult.StatusValues.Idle, StringComparison.OrdinalIgnoreCase))
                                {
                                    status = PowerStatusResult.StatusValues.Idle;
                                }
                                else if (!string.IsNullOrWhiteSpace(statePart))
                                {
                                    // If we got a non-empty state that doesn't match known values,
                                    // preserve it as-is instead of defaulting to "unknown"
                                    status = statePart.ToLowerInvariant();
                                }
                            }
                        }

                        return PowerStatusResult.Successful(status, output);
                    }
                    else
                    {
                        return PowerStatusResult.Failed($"获取电源状态失败: {error}");
                    }
                }

                return PowerStatusResult.Failed("无法启动 hdparm 进程");
            }
            catch (Exception ex)
            {
                return PowerStatusResult.Failed($"获取磁盘电源状态时出错: {ex.Message}");
            }
        }

        public async Task<DiskPowerSettings> GetDiskPowerSettingsAsync(string devicePath)
        {
            var settings = new DiskPowerSettings
            {
                SpinDownTimeoutMinutes = 0,
                ApmLevel = null
            };

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return settings;
            }

            if (!ValidateDevicePath(devicePath))
            {
                return settings;
            }

            const string hdparmConfPath = "/etc/hdparm.conf";

            try
            {
                // Check if hdparm.conf exists
                if (!File.Exists(hdparmConfPath))
                {
                    return settings;
                }

                var lines = await File.ReadAllLinesAsync(hdparmConfPath);

                // Find the device block
                bool inDeviceBlock = false;
                for (int i = 0; i < lines.Length; i++)
                {
                    var trimmedLine = lines[i].Trim();

                    // Check if this is the start of our device block
                    if (trimmedLine == $"{devicePath} {{")
                    {
                        inDeviceBlock = true;
                        continue;
                    }

                    // Check if we've reached the end of the device block
                    if (inDeviceBlock && trimmedLine == "}")
                    {
                        break;
                    }

                    // Parse settings within the device block
                    if (inDeviceBlock && !string.IsNullOrWhiteSpace(trimmedLine) && !trimmedLine.StartsWith("#"))
                    {
                        var match = HdparmSettingRegex.Match(trimmedLine);
                        if (match.Success)
                        {
                            var paramName = match.Groups[1].Value.Trim();
                            var paramValue = match.Groups[2].Value.Trim();

                            if (paramName.Equals("spindown_time", StringComparison.OrdinalIgnoreCase) ||
                                paramName.Equals("force_spindown_time", StringComparison.OrdinalIgnoreCase))
                            {
                                if (int.TryParse(paramValue, out int hdparmValue))
                                {
                                    settings.SpinDownTimeoutMinutes = ConvertHdparmEncodingToMinutes(hdparmValue);
                                }
                            }
                            else if (paramName.Equals("apm", StringComparison.OrdinalIgnoreCase))
                            {
                                if (int.TryParse(paramValue, out int apmValue))
                                {
                                    settings.ApmLevel = apmValue;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading disk power settings from {hdparmConfPath}: {ex.Message}");
            }

            return settings;
        }

        /// <summary>
        /// Converts minutes to hdparm standby timeout encoding.
        /// hdparm -S uses a special encoding:
        /// - 0 = disabled
        /// - 1-240 = multiples of 5 seconds (5 seconds to 20 minutes)
        /// - 241-251 = fixed 30-minute increments (241 = 30min, 242 = 60min, ..., 251 = 330min / 5.5 hours).
        ///             Input values from 22-330 minutes are rounded up to the nearest 30-minute boundary:
        ///             22-30 → 30min (241), 31-60 → 60min (242), 61-90 → 90min (243), etc.
        /// - 252 = 21 minutes
        /// - 253 = vendor-defined (8-12 hours)
        /// - 254 = reserved
        /// - 255 = 21 minutes + 15 seconds
        /// </summary>
        /// <param name="minutes">Timeout in minutes (0-330). Values from 22-330 (except exactly 21) 
        /// will be rounded up to the nearest 30-minute boundary.</param>
        /// <returns>hdparm encoded value (0-255)</returns>
        private static int ConvertMinutesToHdparmEncoding(int minutes)
        {
            if (minutes <= 0)
            {
                return 0; // Disabled
            }

            if (minutes <= 20)
            {
                // 1-240: multiples of 5 seconds
                // minutes * 60 seconds / 5 = minutes * 12
                return minutes * 12;
            }

            if (minutes == 21)
            {
                return 252; // Special value for exactly 21 minutes
            }

            if (minutes <= 330)
            {
                // 241-251: multiples of 30 minutes (30 to 330 minutes = 5.5 hours)
                // 241 = 30 min, 242 = 60 min, ..., 251 = 330 min
                // Round up to the nearest 30-minute boundary
                // Examples: 22-30 => 241, 31-60 => 242, 61-90 => 243
                int thirtyMinuteUnits = (minutes + 29) / 30; // Round up to nearest 30 min unit
                return 240 + thirtyMinuteUnits;
            }

            // For values > 330 minutes, use the highest available value (251 = 330 minutes)
            return 251;
        }

        /// <summary>
        /// Converts hdparm standby timeout encoding to minutes.
        /// This is the reverse of ConvertMinutesToHdparmEncoding.
        /// Note: Values 1-11 represent less than 1 minute and will be rounded up to 1 minute for display.
        /// </summary>
        /// <param name="hdparmValue">hdparm encoded value (0-255)</param>
        /// <returns>Timeout in minutes (rounded up for values less than 1 minute)</returns>
        private static int ConvertHdparmEncodingToMinutes(int hdparmValue)
        {
            if (hdparmValue <= 0)
            {
                return 0; // Disabled
            }

            if (hdparmValue <= 240)
            {
                // 1-240: multiples of 5 seconds
                // hdparmValue * 5 seconds / 60 = hdparmValue / 12
                // Round up to ensure we never show 0 for non-zero values
                int minutes = hdparmValue / 12;
                if (minutes == 0 && hdparmValue > 0)
                {
                    return 1; // Values 1-11 (5-55 seconds) are displayed as 1 minute
                }
                return minutes;
            }

            if (hdparmValue >= 241 && hdparmValue <= 251)
            {
                // 241-251: multiples of 30 minutes
                // 241 = 30 min, 242 = 60 min, ..., 251 = 330 min
                return (hdparmValue - 240) * 30;
            }

            if (hdparmValue == 252 || hdparmValue == 255)
            {
                // 252 = 21 minutes, 255 = 21 minutes + 15 seconds (displayed as 21 for UI purposes)
                return 21;
            }

            if (hdparmValue == 253)
            {
                // 253 = vendor-defined (typically 8-12 hours), not commonly used
                // Return 0 as we can't reliably determine the actual timeout
                return 0;
            }

            // For value 254 (reserved) and other unknown values, return 0 (disabled/unknown)
            return 0;
        }

        /// <summary>
        /// Result of hdparm configuration update operation
        /// </summary>
        private record HdparmConfigResult(bool Success, string Message);

        /// <summary>
        /// Updates /etc/hdparm.conf with persistent power management settings for a device.
        /// Creates or updates the device block in the configuration file.
        /// </summary>
        /// <param name="devicePath">The device path (e.g., /dev/sda)</param>
        /// <param name="spindownTime">Optional spindown timeout in minutes (0-330). Uses hdparm's encoding scheme internally.</param>
        /// <param name="apmLevel">Optional APM level (1-255)</param>
        /// <returns>Result indicating success or failure with a message</returns>
        private static async Task<HdparmConfigResult> UpdateHdparmConfAsync(string devicePath, int? spindownTime = null, int? apmLevel = null)
        {
            const string hdparmConfPath = "/etc/hdparm.conf";

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new HdparmConfigResult(false, "Updating /etc/hdparm.conf is only supported on Linux systems.");
            }

            // Fail fast if the application is not running with sufficient privileges (typically root).
            // Note: This is a heuristic check - actual write permissions may still vary
            if (!string.Equals(Environment.UserName, "root", StringComparison.Ordinal))
            {
                return new HdparmConfigResult(false, "Failed to update /etc/hdparm.conf: Permission denied. The application needs to run with sufficient privileges (e.g., as root).");
            }

            try
            {
                // Read existing configuration or create new one
                var lines = new List<string>();
                if (File.Exists(hdparmConfPath))
                {
                    lines = (await File.ReadAllLinesAsync(hdparmConfPath)).ToList();
                }
                else
                {
                    // Create a basic hdparm.conf header if file doesn't exist
                    lines.Add("## hdparm configuration file");
                    lines.Add("## Auto-generated by QingFeng disk management");
                    lines.Add("");
                    lines.Add("quiet");
                    lines.Add("");
                }

                // Find if a block for this device already exists
                int blockStartIndex = -1;
                int blockEndIndex = -1;

                for (int i = 0; i < lines.Count; i++)
                {
                    var trimmedLine = lines[i].Trim();
                    // Use exact matching on the device header line to avoid partial device path matches
                    // e.g., when searching for /dev/sda, do not accidentally identify the /dev/sda1 block
                    if (trimmedLine == $"{devicePath} {{")
                    {
                        blockStartIndex = i;
                        // Find the closing brace
                        for (int j = i + 1; j < lines.Count; j++)
                        {
                            if (lines[j].Trim() == "}")
                            {
                                blockEndIndex = j;
                                break;
                            }
                        }
                        break;
                    }
                }

                // Build the device block content
                var blockLines = new List<string>();
                var existingSpindown = false;
                var existingApm = false;

                if (blockStartIndex >= 0 && blockEndIndex >= 0)
                {
                    // Parse existing block to preserve other settings
                    for (int i = blockStartIndex + 1; i < blockEndIndex; i++)
                    {
                        var line = lines[i].Trim();

                        // Skip comments and empty lines
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                        {
                            continue;
                        }

                        // Extract parameter name for exact matching
                        // Split on first '=' only to handle values that might contain '='
                        var parts = line.Split('=', 2);
                        var paramName = parts[0].Trim();

                        if (paramName.Equals("spindown_time", StringComparison.OrdinalIgnoreCase) ||
                            paramName.Equals("force_spindown_time", StringComparison.OrdinalIgnoreCase))
                        {
                            existingSpindown = true;
                            if (spindownTime.HasValue)
                            {
                                var hdparmValue = ConvertMinutesToHdparmEncoding(spindownTime.Value);
                                blockLines.Add($"\tspindown_time = {hdparmValue}");
                            }
                            else
                            {
                                // Preserve existing spindown_time if not being updated
                                blockLines.Add($"\t{line}");
                            }
                        }
                        else if (paramName.Equals("apm", StringComparison.OrdinalIgnoreCase))
                        {
                            existingApm = true;
                            if (apmLevel.HasValue)
                            {
                                blockLines.Add($"\tapm = {apmLevel.Value}");
                            }
                            else
                            {
                                // Preserve existing apm if not being updated
                                blockLines.Add($"\t{line}");
                            }
                        }
                        else
                        {
                            // Preserve other non-comment settings that are valid
                            // Check if it's a valid flag or a valid parameter with assignment
                            var match = HdparmSettingRegex.Match(line);
                            if (match.Success && ValidHdparmParams.Contains(match.Groups[1].Value))
                            {
                                blockLines.Add($"\t{line}");
                            }
                            else if (ValidHdparmFlags.Contains(paramName))
                            {
                                blockLines.Add($"\t{line}");
                            }
                        }
                    }
                }

                // Add new settings if they weren't in the existing block
                if (spindownTime.HasValue && !existingSpindown)
                {
                    var hdparmValue = ConvertMinutesToHdparmEncoding(spindownTime.Value);
                    blockLines.Add($"\tspindown_time = {hdparmValue}");
                }
                if (apmLevel.HasValue && !existingApm)
                {
                    blockLines.Add($"\tapm = {apmLevel.Value}");
                }

                // Build the complete new block
                var newBlock = new List<string>
            {
                $"{devicePath} {{",
            };
                newBlock.AddRange(blockLines);
                newBlock.Add("}");

                // Remove old block if it exists
                if (blockStartIndex >= 0 && blockEndIndex >= 0)
                {
                    lines.RemoveRange(blockStartIndex, blockEndIndex - blockStartIndex + 1);
                }

                // Add the new block at the end
                if (lines.Count > 0 && !string.IsNullOrWhiteSpace(lines[^1]))
                {
                    lines.Add(""); // Add blank line before new block
                }
                lines.AddRange(newBlock);

                // Write the updated configuration using atomic file operations
                // Write to a temporary file first, then move it to the final location
                // This prevents corruption if the write operation fails
                var tempPath = $"{hdparmConfPath}.tmp";
                try
                {
                    await File.WriteAllLinesAsync(tempPath, lines);

                    // Move the temporary file to the final location
                    // This is an atomic operation on most filesystems
                    File.Move(tempPath, hdparmConfPath, overwrite: true);
                }
                catch
                {
                    // Clean up temporary file if it exists
                    if (File.Exists(tempPath))
                    {
                        try
                        {
                            File.Delete(tempPath);
                        }
                        catch (IOException ex)
                        {
                            // Ignore cleanup errors - temporary file will be cleaned up by system eventually
                            Debug.WriteLine($"Failed to delete temporary file '{tempPath}' during cleanup: {ex}");
                        }
                    }
                    throw;
                }

                return new HdparmConfigResult(true, "Successfully updated /etc/hdparm.conf");
            }
            catch (UnauthorizedAccessException)
            {
                return new HdparmConfigResult(false, "Failed to update /etc/hdparm.conf: Permission denied. The application needs to run with sufficient privileges.");
            }
            catch (Exception ex)
            {
                return new HdparmConfigResult(false, $"Failed to update /etc/hdparm.conf: {ex.Message}");
            }
        }

        private static bool ValidateDevicePath(string devicePath)
        {
            if (string.IsNullOrWhiteSpace(devicePath))
            {
                return false;
            }

            if (InvalidChars.Any(c => devicePath.Contains(c)))
            {
                return false;
            }

            if (!devicePath.StartsWith("/dev/", StringComparison.Ordinal))
            {
                return false;
            }

            return true;
        }

        private static bool ValidateMountPoint(string mountPoint)
        {
            if (string.IsNullOrWhiteSpace(mountPoint))
            {
                return false;
            }

            if (InvalidChars.Any(c => mountPoint.Contains(c)))
            {
                return false;
            }

            if (!Path.IsPathRooted(mountPoint))
            {
                return false;
            }

            return true;
        }

        // Network disk management implementation
        public async Task<List<NetworkDiskInfo>> GetNetworkDisksAsync()
        {
            var networkDisks = new List<NetworkDiskInfo>();

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return networkDisks;
            }

            try
            {
                // Read /proc/mounts to find network mounts
                var mountsPath = "/proc/mounts";
                if (!File.Exists(mountsPath))
                {
                    return networkDisks;
                }

                var lines = await File.ReadAllLinesAsync(mountsPath);
                foreach (var line in lines)
                {
                    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 4)
                        continue;

                    var device = parts[0];
                    var mountPoint = parts[1];
                    var fsType = parts[2];
                    var options = parts.Length > 3 ? parts[3] : "";

                    // Check if it's a network filesystem
                    if (fsType == "cifs" || fsType == "nfs" || fsType == "nfs4")
                    {
                        var diskInfo = new NetworkDiskInfo
                        {
                            MountPoint = mountPoint,
                            FileSystem = fsType,
                            Options = options,
                            IsReady = true
                        };

                        // Parse server and share path
                        if (fsType == "cifs")
                        {
                            diskInfo.DiskType = NetworkDiskType.CIFS;
                            // Format: //server/share
                            if (device.StartsWith("//") && device.Length > 2)
                            {
                                var pathParts = device[2..].Split('/', 2);
                                if (pathParts.Length >= 1)
                                {
                                    diskInfo.Server = pathParts[0];
                                    diskInfo.SharePath = pathParts.Length > 1 ? pathParts[1] : "";
                                }
                            }
                        }
                        else if (fsType == "nfs" || fsType == "nfs4")
                        {
                            diskInfo.DiskType = NetworkDiskType.NFS;
                            // Format: server:/path
                            var colonIndex = device.IndexOf(':');
                            if (colonIndex > 0 && colonIndex < device.Length - 1)
                            {
                                diskInfo.Server = device[..colonIndex];
                                diskInfo.SharePath = device[(colonIndex + 1)..];
                            }
                        }

                        // Get usage information
                        if (Directory.Exists(mountPoint))
                        {
                            try
                            {
                                var driveInfo = new DriveInfo(mountPoint);
                                if (driveInfo.IsReady)
                                {
                                    diskInfo.TotalBytes = driveInfo.TotalSize;
                                    diskInfo.AvailableBytes = driveInfo.AvailableFreeSpace;
                                    diskInfo.UsedBytes = driveInfo.TotalSize - driveInfo.AvailableFreeSpace;
                                    diskInfo.UsagePercent = diskInfo.TotalBytes > 0
                                        ? Math.Round((double)diskInfo.UsedBytes / diskInfo.TotalBytes * 100, 2)
                                        : 0;
                                }
                            }
                            catch
                            {
                                // Ignore errors getting usage info
                            }
                        }

                        networkDisks.Add(diskInfo);
                    }
                }
            }
            catch
            {
                // Return empty list on error
            }

            return networkDisks;
        }

        public async Task<DiskOperationResult> MountNetworkDiskAsync(string server, string sharePath, string mountPoint, NetworkDiskType diskType, string? username = null, string? password = null, string? domain = null, string? options = null)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return DiskOperationResult.Failed("网络磁盘挂载仅在 Linux 系统上支持");
            }

            if (!ValidateNetworkPath(server, sharePath))
            {
                return DiskOperationResult.Failed("无效的服务器或共享路径");
            }

            if (!ValidateMountPoint(mountPoint))
            {
                return DiskOperationResult.Failed("无效的挂载点");
            }

            if (options != null && InvalidChars.Any(c => options.Contains(c)))
            {
                return DiskOperationResult.Failed("挂载选项包含无效字符");
            }

            // Validate credentials don't contain invalid characters
            if (!string.IsNullOrEmpty(username) && username.Any(c => InvalidCredentialCharsSet.Contains(c)))
            {
                return DiskOperationResult.Failed("用户名包含无效字符（换行符或等号）");
            }
            if (!string.IsNullOrEmpty(password) && password.Any(c => InvalidCredentialCharsSet.Contains(c)))
            {
                return DiskOperationResult.Failed("密码包含无效字符（换行符或等号）");
            }
            if (!string.IsNullOrEmpty(domain) && domain.Any(c => InvalidCredentialCharsSet.Contains(c)))
            {
                return DiskOperationResult.Failed("域名包含无效字符（换行符或等号）");
            }

            try
            {
                // Create mount point if it doesn't exist
                if (!Directory.Exists(mountPoint))
                {
                    Directory.CreateDirectory(mountPoint);
                }

                string device;
                string? tempCredFile = null;

                var processInfo = new ProcessStartInfo
                {
                    FileName = "sudo",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Add mount command as first argument to sudo
                processInfo.ArgumentList.Add("mount");

                if (diskType == NetworkDiskType.CIFS)
                {
                    // CIFS mount
                    device = $"//{server}/{sharePath}";

                    processInfo.ArgumentList.Add("-t");
                    processInfo.ArgumentList.Add("cifs");

                    // Build credentials using a temporary credentials file for security
                    var credOptions = new List<string>();

                    // If credentials are provided, create a temporary credential file with secure permissions
                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                    {
                        try
                        {
                            // Use SHA256 hash for better collision resistance
                            var hashInput = $"{Guid.NewGuid()}-{DateTime.UtcNow.Ticks}";
                            var hashBytes = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(hashInput));
                            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant()[..16];
                            tempCredFile = Path.Combine("/tmp", $"cifs-cred-{hashString}");

                            var credContent = $"username={username}\npassword={password}\n";
                            if (!string.IsNullOrEmpty(domain))
                            {
                                credContent += $"domain={domain}\n";
                            }

                            // Create the credential file with secure permissions atomically
                            var fsOptions = new FileStreamOptions
                            {
                                Mode = FileMode.CreateNew,
                                Access = FileAccess.Write,
                                Share = FileShare.None,
                                Options = FileOptions.Asynchronous,
                                UnixCreateMode = UnixFileMode.UserRead | UnixFileMode.UserWrite
                            };

                            await using (var fs = new FileStream(tempCredFile, fsOptions))
                            {
                                await using (var writer = new StreamWriter(fs))
                                {
                                    await writer.WriteAsync(credContent);
                                }
                            }

                            credOptions.Add($"credentials={tempCredFile}");
                        }
                        catch (Exception ex)
                        {
                            // Clean up the credential file if it was created
                            if (tempCredFile != null && File.Exists(tempCredFile))
                            {
                                try { File.Delete(tempCredFile); } catch { }
                                tempCredFile = null;
                            }
                            Debug.WriteLine($"Failed to create secure credential file: {ex}");
                            return DiskOperationResult.Failed("创建安全凭据文件失败", ex.Message);
                        }
                    }

                    // Add custom options
                    if (!string.IsNullOrEmpty(options))
                    {
                        credOptions.AddRange(options.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(o => o.Trim()));
                    }

                    if (credOptions.Count > 0)
                    {
                        processInfo.ArgumentList.Add("-o");
                        processInfo.ArgumentList.Add(string.Join(",", credOptions));
                    }

                    processInfo.ArgumentList.Add(device);
                    processInfo.ArgumentList.Add(mountPoint);
                }
                else // NFS
                {
                    // NFS mount - normalize share path
                    var normalizedSharePath = sharePath.StartsWith("/") ? sharePath : "/" + sharePath;
                    device = $"{server}:{normalizedSharePath}";

                    if (!string.IsNullOrEmpty(options))
                    {
                        processInfo.ArgumentList.Add("-o");
                        processInfo.ArgumentList.Add(options);
                    }

                    processInfo.ArgumentList.Add(device);
                    processInfo.ArgumentList.Add(mountPoint);
                }

                // Log the mount command for debugging
                var commandArgs = string.Join(" ", processInfo.ArgumentList.Select(arg =>
                {
                    // Mask credential file path but preserve other options
                    if (arg.StartsWith("credentials="))
                    {
                        var parts = arg.Split(',');
                        parts[0] = "credentials=***";
                        return string.Join(",", parts);
                    }
                    return arg;
                }));
                _logger.LogInformation("Executing mount command: {FileName} {Arguments}", processInfo.FileName, commandArgs);

                try
                {
                    using var process = Process.Start(processInfo);
                    if (process != null)
                    {
                        await process.WaitForExitAsync();
                        var error = await process.StandardError.ReadToEndAsync();

                        if (process.ExitCode == 0)
                        {
                            return DiskOperationResult.Successful($"成功将 {device} 挂载到 {mountPoint}");
                        }
                        else
                        {
                            // Check if the error is due to permission issues
                            var permError = CheckPermissionError(error, "挂载");
                            if (permError != null)
                            {
                                return permError;
                            }
                            
                            // Log the error details to help with debugging
                            _logger.LogError("Failed to mount network disk. Device: {Device}, MountPoint: {MountPoint}, DiskType: {DiskType}, ExitCode: {ExitCode}, Error: {Error}", 
                                device, mountPoint, diskType, process.ExitCode, error);
                            return DiskOperationResult.Failed("挂载失败", error);
                        }
                    }

                    _logger.LogError("Failed to start mount process for device: {Device}, MountPoint: {MountPoint}", device, mountPoint);
                    return DiskOperationResult.Failed("无法启动挂载进程");
                }
                finally
                {
                    // Clean up temporary credential file
                    if (tempCredFile != null && File.Exists(tempCredFile))
                    {
                        try
                        {
                            File.Delete(tempCredFile);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Failed to delete temporary credential file '{tempCredFile}': {ex}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error mounting network disk. Server: {Server}, SharePath: {SharePath}, MountPoint: {MountPoint}, DiskType: {DiskType}", 
                    server, sharePath, mountPoint, diskType);
                return DiskOperationResult.Failed("挂载网络磁盘时出错", ex.Message);
            }
        }

        public async Task<DiskOperationResult> MountNetworkDiskPermanentAsync(string server, string sharePath, string mountPoint, NetworkDiskType diskType, string? username = null, string? password = null, string? domain = null, string? options = null)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return DiskOperationResult.Failed("永久挂载网络磁盘仅在 Linux 系统上支持");
            }

            // First mount temporarily
            var mountResult = await MountNetworkDiskAsync(server, sharePath, mountPoint, diskType, username, password, domain, options);
            if (!mountResult.Success)
            {
                return mountResult;
            }

            try
            {
                string device;
                string fsType;
                var fstabOptions = new List<string>();

                if (diskType == NetworkDiskType.CIFS)
                {
                    device = $"//{server}/{sharePath}";
                    fsType = "cifs";

                    // For CIFS, we should use a credentials file for security
                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                    {
                        // Use SHA256 hash for better collision resistance
                        var hashInput = $"{server}-{sharePath}";
                        var hashBytes = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(hashInput));
                        var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant()[..32];
                        var credFile = $"/tmp/cifs-credentials-{hashString}";
                        try
                        {
                            var credContent = $"username={username}\npassword={password}\n";
                            if (!string.IsNullOrEmpty(domain))
                            {
                                credContent += $"domain={domain}\n";
                            }

                            // Create the credential file with secure permissions atomically
                            var fsOptions = new FileStreamOptions
                            {
                                Mode = FileMode.Create,
                                Access = FileAccess.Write,
                                Share = FileShare.None,
                                Options = FileOptions.Asynchronous,
                                UnixCreateMode = UnixFileMode.UserRead | UnixFileMode.UserWrite
                            };

                            await using (var fs = new FileStream(credFile, fsOptions))
                            {
                                await using (var writer = new StreamWriter(fs))
                                {
                                    await writer.WriteAsync(credContent);
                                }
                            }

                            // Verify file permissions were set correctly
                            var chmodInfo = new ProcessStartInfo
                            {
                                FileName = "chmod",
                                RedirectStandardOutput = true,
                                RedirectStandardError = true,
                                UseShellExecute = false,
                                CreateNoWindow = true
                            };
                            chmodInfo.ArgumentList.Add("600");
                            chmodInfo.ArgumentList.Add(credFile);

                            using var chmodProcess = Process.Start(chmodInfo);
                            if (chmodProcess == null)
                            {
                                throw new InvalidOperationException("Failed to start chmod process to secure credential file.");
                            }

                            await chmodProcess.WaitForExitAsync();
                            if (chmodProcess.ExitCode != 0)
                            {
                                var errorOutput = await chmodProcess.StandardError.ReadToEndAsync();
                                // Remove potentially insecure credential file
                                try
                                {
                                    if (File.Exists(credFile))
                                    {
                                        File.Delete(credFile);
                                    }
                                }
                                catch
                                {
                                    // Ignore file deletion errors
                                }
                                throw new InvalidOperationException($"chmod failed for credential file '{credFile}' with exit code {chmodProcess.ExitCode}: {errorOutput}");
                            }

                            fstabOptions.Add($"credentials={credFile}");
                        }
                        catch (Exception ex)
                        {
                            // If we can't securely write credentials file, fail the operation
                            try
                            {
                                if (File.Exists(credFile))
                                {
                                    File.Delete(credFile);
                                }
                            }
                            catch
                            {
                                // Ignore file deletion errors
                            }
                            Debug.WriteLine($"Failed to create CIFS credentials file: {ex}");
                            return DiskOperationResult.Failed($"创建安全的 CIFS 凭据文件 '{credFile}' 失败。拒绝在 /etc/fstab 中不安全地存储凭据", ex.Message);
                        }
                    }
                }
                else // NFS
                {
                    // NFS mount - normalize share path
                    var normalizedSharePath = sharePath.Trim().TrimStart('/');
                    if (string.IsNullOrEmpty(normalizedSharePath))
                    {
                        return DiskOperationResult.Failed("无效的 NFS 共享路径：共享路径不能为空或仅包含 '/'");
                    }

                    device = $"{server}:/{normalizedSharePath}";
                    fsType = "nfs";
                }

                // Add custom options with validation
                if (!string.IsNullOrEmpty(options))
                {
                    foreach (var rawOption in options.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        var option = rawOption.Trim();
                        if (string.IsNullOrEmpty(option))
                        {
                            continue;
                        }

                        // Reject options containing invalid characters or whitespace
                        if (InvalidChars.Any(c => option.Contains(c)) || option.Any(char.IsWhiteSpace))
                        {
                            Debug.WriteLine($"Ignoring invalid fstab option: '{rawOption}' after trimming to '{option}'.");
                            continue;
                        }

                        fstabOptions.Add(option);
                    }
                }

                // Add default options if none specified
                if (fstabOptions.Count == 0)
                {
                    fstabOptions.Add("defaults");
                }

                var fstabEntry = $"{device} {mountPoint} {fsType} {string.Join(",", fstabOptions)} 0 0";

                // Use temp file approach for ProtectSystem=full compatibility
                try
                {
                    var fstabPath = "/etc/fstab";
                    string[] existingLines;

                    try
                    {
                        existingLines = await File.ReadAllLinesAsync(fstabPath);
                    }
                    catch (FileNotFoundException)
                    {
                        existingLines = Array.Empty<string>();
                    }

                    // Check if entry already exists using proper field parsing
                    var hasExistingEntry = existingLines
                        .Select(line => line.Trim())
                        .Where(trimmed => !string.IsNullOrEmpty(trimmed) && !trimmed.StartsWith("#"))
                        .Select(trimmed => WhitespaceRegex.Split(trimmed))
                        .Where(fields => fields.Length >= 2)
                        .Any(fields => fields[0] == device && fields[1] == mountPoint);

                    if (hasExistingEntry)
                    {
                        return DiskOperationResult.Successful($"成功将 {device} 挂载到 {mountPoint}；/etc/fstab 中已存在匹配条目");
                    }

                    // Create temp file in /tmp (writable with PrivateTmp=true)
                    var tempPath = Path.Combine("/tmp", $"fstab.{Guid.NewGuid():N}.tmp");
                    var newContent = string.Join(Environment.NewLine, existingLines) + 
                                   (existingLines.Length > 0 ? Environment.NewLine : "") + 
                                   fstabEntry + Environment.NewLine;
                    
                    await File.WriteAllTextAsync(tempPath, newContent);
                    
                    // Use File.Move with overwrite (works with CAP_DAC_OVERRIDE even on read-only /etc)
                    File.Move(tempPath, fstabPath, overwrite: true);
                    
                    return DiskOperationResult.Successful($"成功将 {device} 挂载到 {mountPoint} 并添加到 /etc/fstab");
                }
                catch (UnauthorizedAccessException ex)
                {
                    _logger.LogError(ex, "Permission denied when writing to /etc/fstab for network disk '{Device}'", device);
                    return DiskOperationResult.Failed($"挂载成功但写入 /etc/fstab 时权限被拒绝。条目: {fstabEntry}");
                }
                catch (IOException ex)
                {
                    _logger.LogError(ex, "I/O error when writing to /etc/fstab for network disk '{Device}'", device);
                    return DiskOperationResult.Failed($"挂载成功但写入 /etc/fstab 时出错", $"{ex.Message}。条目: {fstabEntry}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error appending to /etc/fstab for network disk '{Device}' at mount point '{MountPoint}'", device, mountPoint);
                    Debug.WriteLine($"Error appending to /etc/fstab for network disk '{device}' at mount point '{mountPoint}'. Exception: {ex}");
                    return DiskOperationResult.Failed($"挂载成功但更新 /etc/fstab 时出错", $"{ex.Message}。条目: {fstabEntry}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during permanent network mount setup. Exception: {ex}");
                return DiskOperationResult.Failed("挂载成功但更新 /etc/fstab 时出错", ex.Message);
            }
        }

        private static bool ValidateNetworkPath(string server, string sharePath)
        {
            if (string.IsNullOrWhiteSpace(server) || string.IsNullOrWhiteSpace(sharePath))
            {
                return false;
            }

            if (InvalidChars.Any(c => server.Contains(c) || sharePath.Contains(c)))
            {
                return false;
            }

            return true;
        }

        // Feature detection implementation
        public async Task<DiskManagementFeatureDetection> DetectFeaturesAsync()
        {
            var detection = new DiskManagementFeatureDetection
            {
                Requirements = new List<FeatureRequirement>()
            };

            // Detect OS
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                detection.DetectedOS = "Linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                detection.DetectedOS = "Windows";
                detection.Summary = "磁盘管理功能主要设计用于 Linux 系统。Windows 系统功能有限。";
                detection.AllRequiredFeaturesAvailable = false;
                return detection;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                detection.DetectedOS = "macOS";
                detection.Summary = "磁盘管理功能主要设计用于 Linux 系统。macOS 系统功能有限。";
                detection.AllRequiredFeaturesAvailable = false;
                return detection;
            }
            else
            {
                detection.DetectedOS = "Unknown";
                detection.Summary = "无法识别操作系统类型。磁盘管理功能仅支持 Linux。";
                detection.AllRequiredFeaturesAvailable = false;
                return detection;
            }

            // Check for required tools on Linux
            var requirements = new List<(string name, string desc, bool required, string installUbuntu, string installRhel)>
        {
            ("lsblk", "列出块设备信息 - 查看所有磁盘和分区", true,
             "sudo apt-get install util-linux",
             "sudo yum install util-linux"),
            ("mount", "挂载文件系统 - 挂载磁盘到指定位置", true,
             "内置命令，通常已安装",
             "内置命令，通常已安装"),
            ("umount", "卸载文件系统 - 卸载已挂载的磁盘", true,
             "内置命令，通常已安装",
             "内置命令，通常已安装"),
            ("hdparm", "磁盘电源管理工具 - 设置磁盘休眠和电源管理", false,
             "sudo apt-get install hdparm",
             "sudo yum install hdparm"),
            ("mount.cifs", "CIFS/SMB 挂载工具 - 挂载 Windows 网络共享", false,
             "sudo apt-get install cifs-utils",
             "sudo yum install cifs-utils"),
            ("mount.nfs", "NFS 挂载工具 - 挂载 NFS 网络共享", false,
             "sudo apt-get install nfs-common",
             "sudo yum install nfs-utils")
        };

            // Check all tools in parallel for better performance
            var checkTasks = requirements.Select(async req =>
            {
                var (name, desc, required, installUbuntu, installRhel) = req;
                var status = await CheckCommandAvailabilityAsync(name);
                var installCmd = $"Ubuntu/Debian: {installUbuntu}\nCentOS/RHEL/Fedora: {installRhel}";

                return new FeatureRequirement
                {
                    Name = name,
                    Description = desc,
                    Status = status ? FeatureStatus.Available : FeatureStatus.Missing,
                    IsRequired = required,
                    CheckCommand = $"which {name}",
                    InstallCommand = installCmd,
                    Notes = required
                        ? "此工具是必需的，没有它将无法使用基本磁盘管理功能"
                        : "此工具是可选的，用于增强功能"
                };
            }).ToList();

            detection.Requirements = (await Task.WhenAll(checkTasks)).ToList();

            // Check if all required features are available
            detection.AllRequiredFeaturesAvailable = detection.Requirements
                .Where(r => r.IsRequired)
                .All(r => r.Status == FeatureStatus.Available);

            // Generate summary
            var missingRequired = detection.Requirements
                .Where(r => r.IsRequired && r.Status == FeatureStatus.Missing)
                .ToList();
            var missingOptional = detection.Requirements
                .Where(r => !r.IsRequired && r.Status == FeatureStatus.Missing)
                .ToList();

            if (missingRequired.Any())
            {
                detection.Summary = $"缺少 {missingRequired.Count} 个必需工具：{string.Join(", ", missingRequired.Select(r => r.Name))}。请安装这些工具以使用磁盘管理功能。";
            }
            else if (missingOptional.Any())
            {
                detection.Summary = $"所有必需工具已安装。缺少 {missingOptional.Count} 个可选工具：{string.Join(", ", missingOptional.Select(r => r.Name))}。";
            }
            else
            {
                detection.Summary = "所有工具已安装，磁盘管理功能完全可用。";
            }

            return detection;
        }

        private static async Task<bool> CheckCommandAvailabilityAsync(string command)
        {
            try
            {
                // Validate command name to prevent command injection
                if (string.IsNullOrWhiteSpace(command) ||
                    !SafeNameRegex.IsMatch(command))
                {
                    Debug.WriteLine($"Invalid command name: {command}");
                    return false;
                }

                var processInfo = new ProcessStartInfo
                {
                    FileName = "which",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(processInfo);
                if (process == null)
                {
                    Debug.WriteLine($"Failed to start which process for command: {command}");
                    return false;
                }

                // Add timeout to prevent indefinite blocking
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));
                var processTask = process.WaitForExitAsync();
                var completedTask = await Task.WhenAny(processTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    Debug.WriteLine($"Timeout waiting for which to check command: {command}");
                    try
                    {
                        process.Kill();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Failed to kill which process for command '{command}': {ex.Message}");
                    }
                    return false;
                }

                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to check availability of command '{command}': {ex}");
                return false;
            }
        }

        /// <summary>
        /// Gets SMART (Self-Monitoring, Analysis and Reporting Technology) information for a disk
        /// </summary>
        public async Task<SmartInfo> GetDiskSmartInfoAsync(string devicePath)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new SmartInfo
                {
                    Success = false,
                    ErrorMessage = "SMART 信息查询仅在 Linux 系统上支持"
                };
            }

            if (!ValidateDevicePath(devicePath))
            {
                return new SmartInfo
                {
                    Success = false,
                    ErrorMessage = "无效的设备路径"
                };
            }

            try
            {
                // Check SMART availability using existing helper
                var smartctlAvailable = await CheckCommandAvailabilityAsync("smartctl");
                if (!smartctlAvailable)
                {
                    return new SmartInfo
                    {
                        Success = false,
                        ErrorMessage = "smartctl 未安装。请安装 smartmontools 软件包 (sudo apt install smartmontools)"
                    };
                }

                // Run smartctl to get SMART information
                var processInfo = new ProcessStartInfo
                {
                    FileName = "smartctl",
                    Arguments = $"-a {devicePath}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(processInfo);
                if (process == null)
                {
                    return new SmartInfo
                    {
                        Success = false,
                        ErrorMessage = "无法启动 smartctl 进程"
                    };
                }

                // Start reading output before waiting to avoid deadlocks
                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();

                // Wait for process to exit with timeout
                var completed = await Task.WhenAny(
                    process.WaitForExitAsync(),
                    Task.Delay(TimeSpan.FromSeconds(30))
                );

                string output = "";
                string error = "";
                
                if (completed == process.WaitForExitAsync())
                {
                    output = await outputTask;
                    error = await errorTask;
                }
                else
                {
                    // Timeout occurred
                    try
                    {
                        process.Kill();
                    }
                    catch { }
                    
                    return new SmartInfo
                    {
                        Success = false,
                        ErrorMessage = "smartctl 命令超时"
                    };
                }

                // Check exit code
                if (process.ExitCode != 0)
                {
                    var errorMsg = !string.IsNullOrWhiteSpace(error) ? error : "smartctl 命令执行失败";
                    return new SmartInfo
                    {
                        Success = false,
                        ErrorMessage = $"smartctl 错误 (exit code {process.ExitCode}): {errorMsg}"
                    };
                }

                // Parse the output
                var smartInfo = ParseSmartOutput(output);
                smartInfo.RawOutput = output;
                smartInfo.Success = true;

                return smartInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting SMART info for {DevicePath}", devicePath);
                return new SmartInfo
                {
                    Success = false,
                    ErrorMessage = $"获取 SMART 信息时出错: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Parses smartctl output to extract SMART information
        /// </summary>
        private SmartInfo ParseSmartOutput(string output)
        {
            var smartInfo = new SmartInfo();
            var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            bool inAttributeSection = false;

            foreach (var line in lines)
            {
                var trimmed = line.Trim();

                // Check SMART support
                if (trimmed.Contains("SMART support is:"))
                {
                    // Some smartctl versions output two lines, e.g.:
                    //   "SMART support is: Available - device has SMART capability."
                    //   "SMART support is: Enabled"
                    // Only update each flag when the corresponding keyword is present
                    if (trimmed.Contains("Available"))
                    {
                        smartInfo.IsSupported = true;
                    }
                    else if (trimmed.Contains("Unavailable"))
                    {
                        smartInfo.IsSupported = false;
                    }

                    if (trimmed.Contains("Enabled"))
                    {
                        smartInfo.IsEnabled = true;
                    }
                    else if (trimmed.Contains("Disabled"))
                    {
                        smartInfo.IsEnabled = false;
                    }
                }
                // Health status
                else if (trimmed.StartsWith("SMART overall-health self-assessment test result:", StringComparison.OrdinalIgnoreCase) ||
                         trimmed.StartsWith("SMART Health Status:", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = trimmed.Split(':', 2);
                    if (parts.Length == 2)
                    {
                        smartInfo.HealthStatus = parts[1].Trim();
                    }
                }
                // Model
                else if (trimmed.StartsWith("Device Model:", StringComparison.OrdinalIgnoreCase) ||
                         trimmed.StartsWith("Model Number:", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = trimmed.Split(':', 2);
                    if (parts.Length == 2)
                    {
                        smartInfo.Model = parts[1].Trim();
                    }
                }
                // Serial Number
                else if (trimmed.StartsWith("Serial Number:", StringComparison.OrdinalIgnoreCase) ||
                         trimmed.StartsWith("Serial number:", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = trimmed.Split(':', 2);
                    if (parts.Length == 2)
                    {
                        smartInfo.SerialNumber = parts[1].Trim();
                    }
                }
                // Firmware Version
                else if (trimmed.StartsWith("Firmware Version:", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = trimmed.Split(':', 2);
                    if (parts.Length == 2)
                    {
                        smartInfo.FirmwareVersion = parts[1].Trim();
                    }
                }
                // Capacity
                else if (trimmed.StartsWith("User Capacity:", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = trimmed.Split(':', 2);
                    if (parts.Length == 2)
                    {
                        smartInfo.Capacity = parts[1].Trim();
                    }
                }
                // Attribute table header
                else if (trimmed.Contains("ID#") && trimmed.Contains("ATTRIBUTE_NAME"))
                {
                    inAttributeSection = true;
                    continue;
                }
                // Parse attributes
                else if (inAttributeSection)
                {
                    // Stop parsing attributes when we hit a blank line or new section
                    if (string.IsNullOrWhiteSpace(trimmed) || !char.IsDigit(trimmed[0]))
                    {
                        inAttributeSection = false;
                        continue;
                    }

                    var parts = WhitespaceRegex.Split(trimmed);
                    if (parts.Length >= 10 && int.TryParse(parts[0], out int id))
                    {
                        var attribute = new SmartAttribute
                        {
                            Id = id,
                            Name = parts[1],
                            Value = int.TryParse(parts[3], out int val) ? val : 0,
                            Worst = int.TryParse(parts[4], out int worst) ? worst : 0,
                            Threshold = int.TryParse(parts[5], out int thresh) ? thresh : 0,
                            RawValue = parts.Length > 9 ? string.Join(" ", parts, 9, parts.Length - 9) : ""
                        };

                        smartInfo.Attributes.Add(attribute);

                        // Extract special values
                        if (attribute.Name.Contains("Temperature", StringComparison.OrdinalIgnoreCase) &&
                            int.TryParse(attribute.RawValue.Split(' ')[0], out int temp))
                        {
                            smartInfo.Temperature = temp;
                        }
                        else if (attribute.Name.Contains("Power_On_Hours", StringComparison.OrdinalIgnoreCase) &&
                                 long.TryParse(attribute.RawValue, out long hours))
                        {
                            smartInfo.PowerOnHours = hours;
                        }
                        else if (attribute.Name.Contains("Power_Cycle_Count", StringComparison.OrdinalIgnoreCase) &&
                                 long.TryParse(attribute.RawValue, out long cycles))
                        {
                            smartInfo.PowerCycleCount = cycles;
                        }
                    }
                }
            }

            return smartInfo;
        }
    }

    // JSON models for lsblk output
    /// <summary>
    /// Represents the root object of the JSON output produced by the <c>lsblk</c> command.
    /// </summary>
    internal class LsblkOutput
    {
        /// <summary>
        /// Gets or sets the collection of top-level block devices returned by <c>lsblk</c> in the
        /// <c>"blockdevices"</c> array.
        /// </summary>
        public List<LsblkDevice>? Blockdevices { get; set; }
    }

    /// <summary>
    /// Represents a single block device entry from the JSON output of the <c>lsblk</c> command.
    /// </summary>
    internal class LsblkDevice
    {
        /// <summary>
        /// Gets or sets the device name (for example, <c>sda</c> or <c>sda1</c>), corresponding to the
        /// <c>"name"</c> field in <c>lsblk</c> JSON output.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the device type (for example, <c>disk</c>, <c>part</c>, or <c>rom</c>),
        /// corresponding to the <c>"type"</c> field.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the size of the device in bytes, corresponding to the <c>"size"</c> field.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the mount point of the device, if any, corresponding to the
        /// <c>"mountpoint"</c> field.
        /// </summary>
        public string? Mountpoint { get; set; }

        /// <summary>
        /// Gets or sets the filesystem type on the device, such as <c>ext4</c> or <c>ntfs</c>,
        /// corresponding to the <c>"fstype"</c> field.
        /// </summary>
        public string? Fstype { get; set; }

        /// <summary>
        /// Gets or sets the filesystem UUID associated with the device, corresponding to the
        /// <c>"uuid"</c> field.
        /// </summary>
        public string? Uuid { get; set; }

        /// <summary>
        /// Gets or sets the filesystem label assigned to the device, corresponding to the
        /// <c>"label"</c> field.
        /// </summary>
        public string? Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the device is removable,
        /// corresponding to the <c>"rm"</c> field.
        /// </summary>
        public bool Rm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the device is read-only,
        /// corresponding to the <c>"ro"</c> field.
        /// </summary>
        public bool Ro { get; set; }

        /// <summary>
        /// Gets or sets the device model string reported by the system, corresponding to the
        /// <c>"model"</c> field.
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// Gets or sets the device serial number, if available, corresponding to the
        /// <c>"serial"</c> field.
        /// </summary>
        public string? Serial { get; set; }

        /// <summary>
        /// Gets or sets the collection of child devices for this block device, such as partitions
        /// or logical volumes, corresponding to the nested <c>"children"</c> array.
        /// </summary>
        public List<LsblkDevice>? Children { get; set; }
    }
}
