# MingYue Configuration Guide

This guide explains how to configure MingYue using environment variables and configuration files.

## Directory Structure

MingYue uses a unified directory structure to keep all application data organized in one location:

```
/opt/mingyue/          # Application installation directory
/srv/mingyue/          # Unified data directory (default)
├── data/              # Database and persistent data
│   └── mingyue.db     # SQLite database
└── cache/             # Thumbnail cache and file indexes
```

**Logs**: Application logs are managed by systemd journal. View them with:
```bash
sudo journalctl -u mingyue -f
```

For comprehensive logging, auditing, and log management information, see [LOGGING_AUDIT_GUIDE.md](LOGGING_AUDIT_GUIDE.md).

## Environment Variables

MingYue supports the following environment variables for path configuration:

### Path Configuration

| Variable | Description | Default (Linux) | Default (Windows) |
|----------|-------------|-----------------|-------------------|
| `MINGYUE_DATA_DIR` | Directory for database and persistent data | `/srv/mingyue/data` | Current directory |
| `MINGYUE_CACHE_DIR` | Directory for cache files (thumbnails, indexes) | `/srv/mingyue/cache` | `%USERPROFILE%\.mingyue-cache` |

**Note**: Logs are managed by systemd journal on Linux and are not configured via environment variables. Use `journalctl -u mingyue` to view logs.

### Database Configuration

| Variable | Description | Default |
|----------|-------------|---------|
| `ConnectionStrings__DefaultConnection` | Full database connection string (overrides MINGYUE_DATA_DIR if set) | Uses MINGYUE_DATA_DIR or defaults to `mingyue.db` in current directory |

**Note**: 
- The double underscore (`__`) in `ConnectionStrings__DefaultConnection` is required for ASP.NET Core configuration hierarchy.
- If `ConnectionStrings__DefaultConnection` is not set, the application will use `MINGYUE_DATA_DIR` to construct the database path as `{MINGYUE_DATA_DIR}/mingyue.db`
- If neither is set, it defaults to `mingyue.db` in the application's working directory

### Application Environment

| Variable | Description | Default |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | ASP.NET Core environment (Development/Production) | `Production` |
| `DOTNET_ENVIRONMENT` | .NET environment | `Production` |

## Configuration Methods

### 1. Systemd Service (Recommended for Linux)

When installed via `install.sh`, environment variables are automatically configured in the systemd service file:

```bash
sudo nano /etc/systemd/system/mingyue.service
```

Example configuration:
```ini
[Service]
Environment=MINGYUE_DATA_DIR=/srv/mingyue/data
Environment=MINGYUE_CACHE_DIR=/srv/mingyue/cache
```

After modifying the service file:
```bash
sudo systemctl daemon-reload
sudo systemctl restart mingyue
```

### 2. Configuration File (appsettings.json)

You can also configure paths in `appsettings.json`, though environment variables take precedence:

```json
{
  "Storage": {
    "CacheDirectory": "/srv/mingyue/cache"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=/srv/mingyue/data/mingyue.db"
  }
}
```

### 3. Environment Variables (Shell)

For manual execution or testing:

```bash
export MINGYUE_CACHE_DIR=/srv/mingyue/cache
export MINGYUE_DATA_DIR=/srv/mingyue/data
./MingYue --urls "http://0.0.0.0:5000"
```

## Configuration Priority

Configuration values are applied in the following order (later sources override earlier ones):

1. Default hardcoded values
2. `appsettings.json`
3. `appsettings.{Environment}.json`
4. Environment variables
5. Command-line arguments

## Customizing Paths

### Example: Using a Different Data Directory

If you want to store all MingYue data on a different drive or partition:

1. Create the directory structure:
```bash
sudo mkdir -p /mnt/data/mingyue/{data,cache}
sudo chown -R mingyue:mingyue /mnt/data/mingyue
```

2. Update the systemd service file:
```bash
sudo nano /etc/systemd/system/mingyue.service
```

3. Modify the environment variables:
```ini
Environment=MINGYUE_DATA_DIR=/mnt/data/mingyue/data
Environment=MINGYUE_CACHE_DIR=/mnt/data/mingyue/cache
```

4. Also update the `ReadWritePaths` directive:
```ini
ReadWritePaths=/mnt/data/mingyue
```

5. Reload and restart:
```bash
sudo systemctl daemon-reload
sudo systemctl restart mingyue
```

### Example: Network Storage

For NFS or CIFS mounted storage:

1. Mount your network storage:
```bash
sudo mount -t nfs server:/export/mingyue /srv/mingyue
```

2. Ensure the mount is persistent in `/etc/fstab`:
```
server:/export/mingyue /srv/mingyue nfs defaults,_netdev 0 0
```

3. Update systemd service to wait for network:
```ini
[Unit]
After=network-online.target
Wants=network-online.target
```

## Security Considerations

1. **Directory Permissions**: Ensure the `mingyue` user has read/write access to all configured directories:
   ```bash
   sudo chown -R mingyue:mingyue /srv/mingyue
   sudo chmod 755 /srv/mingyue
   ```

2. **Database Security**: The SQLite database file should only be accessible by the `mingyue` user:
   ```bash
   sudo chmod 600 /srv/mingyue/data/mingyue.db
   ```

3. **Protected Paths**: When using systemd, ensure `ReadWritePaths` includes all directories MingYue needs to write to.

4. **Avoid User Home Directories**: The default configuration avoids using user home directories on Linux to prevent permission issues and ensure proper service isolation.

## Troubleshooting

### Cache directory permission errors

Check ownership and permissions:
```bash
ls -la /srv/mingyue/cache
sudo chown -R mingyue:mingyue /srv/mingyue/cache
sudo chmod 755 /srv/mingyue/cache
```

### Database connection errors

Verify the database file exists and is accessible:
```bash
ls -la /srv/mingyue/data/mingyue.db
sudo chown mingyue:mingyue /srv/mingyue/data/mingyue.db
```

### Environment variables not taking effect

1. Check the systemd service file:
   ```bash
   systemctl cat mingyue.service
   ```

2. Verify environment variables are loaded:
   ```bash
   sudo systemctl show mingyue.service --property=Environment
   ```

3. Check application logs:
   ```bash
   sudo journalctl -u mingyue -n 100
   ```

### Permission errors (disk mount, file operations, service management)

**For installations using the capability-based approach (recommended):**

If you encounter permission errors when performing system operations (mounting disks, managing shares, restarting services), ensure the systemd service has the required capabilities.

**Solution**: Verify the systemd service has all necessary capabilities:

1. Edit the service file:
   ```bash
   sudo nano /etc/systemd/system/mingyue.service
   ```

2. Verify these lines are present in the `[Service]` section:
   ```ini
   # Required capabilities for system operations
   AmbientCapabilities=CAP_SYS_ADMIN CAP_DAC_OVERRIDE
   NoNewPrivileges=true
   ```

3. Reload and restart the service:
   ```bash
   sudo systemctl daemon-reload
   sudo systemctl restart mingyue
   ```

**What each capability does:**
- `CAP_SYS_ADMIN`: Mount/umount operations, systemctl commands (restart services), exportfs
- `CAP_DAC_OVERRIDE`: Write to system configuration files (/etc/samba/smb.conf, /etc/exports)

**For older installations using sudo (legacy):**

If you encounter "sudo: 已设置'no new privileges'标志" errors, your installation is using the older sudo-based approach which is less secure.

**Recommended upgrade path:**

1. Re-run the installation script to get the new capability-based configuration
2. The old sudoers file (/etc/sudoers.d/mingyue) is no longer needed and can be removed
3. The new approach eliminates sudo entirely, using Linux capabilities instead

**Security benefits of the capability-based approach:**
- ✅ Maintains `NoNewPrivileges=true` security hardening
- ✅ Grants only specific capabilities needed (principle of least privilege)
- ✅ No sudoers configuration required (reduces attack surface)
- ✅ More granular control than blanket sudo access
- ✅ All operations auditable through systemd




## Migration from Old Directory Structure

If you're upgrading from an older version that used scattered directories:

1. Stop the service:
   ```bash
   sudo systemctl stop mingyue
   ```

2. Create the new unified structure:
   ```bash
   sudo mkdir -p /srv/mingyue/{data,cache}
   ```

3. Move existing data:
   ```bash
   # Move database
   sudo mv /var/lib/mingyue/* /srv/mingyue/data/ 2>/dev/null || true
   
   # Move cache (if exists in old location)
   sudo mv /home/mingyue/.mingyue-cache/* /srv/mingyue/cache/ 2>/dev/null || true
   ```

4. Set ownership:
   ```bash
   sudo chown -R mingyue:mingyue /srv/mingyue
   ```

5. Update the systemd service file as described above

6. Restart the service:
   ```bash
   sudo systemctl daemon-reload
   sudo systemctl start mingyue
   ```

7. Clean up old directories (after verifying everything works):
   ```bash
   sudo rm -rf /var/lib/mingyue
   sudo rm -rf /var/log/mingyue
   sudo rm -rf /home/mingyue/.mingyue-cache
   ```
