# Migration Guide: Path Configuration Updates

This document provides a quick reference for migrating from the old scattered directory structure to the new unified structure under `/srv/mingyue`.

## Quick Summary

### What Changed?

**Old Structure (Scattered):**
```
/opt/mingyue/                    # Application
/var/lib/mingyue/mingyue.db      # Database
~/.mingyue-cache/                # Cache (user home!)
/var/log/mingyue/                # Logs
```

**New Structure (Unified):**
```
/opt/mingyue/                    # Application
/srv/mingyue/
├── data/mingyue.db              # Database
└── cache/                       # Cache
# Logs: systemd journal only
```

### Environment Variables Added

- `MINGYUE_DATA_DIR` - Database and data directory (default: `/srv/mingyue/data`)
- `MINGYUE_CACHE_DIR` - Cache directory (default: `/srv/mingyue/cache`)

## For New Installations

Simply run the updated `install.sh` script. It will automatically:
1. Create the unified directory structure under `/srv/mingyue`
2. Set up environment variables in the systemd service
3. Configure proper permissions

```bash
sudo ./install.sh
```

## For Existing Installations

### Option 1: Fresh Install (Recommended for Testing)

1. Backup your data:
   ```bash
   sudo cp /var/lib/mingyue/mingyue.db ~/mingyue-backup.db
   ```

2. Uninstall old version:
   ```bash
   sudo ./uninstall.sh
   ```

3. Install new version:
   ```bash
   sudo ./install.sh
   ```

4. Restore database if needed:
   ```bash
   sudo cp ~/mingyue-backup.db /srv/mingyue/data/mingyue.db
   sudo chown mingyue:mingyue /srv/mingyue/data/mingyue.db
   ```

### Option 2: In-Place Migration

1. Stop the service:
   ```bash
   sudo systemctl stop mingyue
   ```

2. Create new directory structure:
   ```bash
   sudo mkdir -p /srv/mingyue/{data,cache}
   sudo chown -R mingyue:mingyue /srv/mingyue
   ```

3. Move existing data:
   ```bash
   # Move database
   sudo mv /var/lib/mingyue/* /srv/mingyue/data/ 2>/dev/null || true
   
   # Move cache if it exists
   sudo mv /home/mingyue/.mingyue-cache/* /srv/mingyue/cache/ 2>/dev/null || true
   sudo mv ~/.mingyue-cache/* /srv/mingyue/cache/ 2>/dev/null || true
   ```

4. Update systemd service:
   ```bash
   sudo nano /etc/systemd/system/mingyue.service
   ```
   
   Update the Environment section to:
   ```ini
   Environment=DOTNET_ENVIRONMENT=Production
   Environment=ASPNETCORE_ENVIRONMENT=Production
   Environment=MINGYUE_DATA_DIR=/srv/mingyue/data
   Environment=MINGYUE_CACHE_DIR=/srv/mingyue/cache
   ```
   
   And update ReadWritePaths:
   ```ini
   ReadWritePaths=/srv/mingyue
   ```
   
   Remove this line if present:
   ```ini
   Environment="ConnectionStrings__DefaultConnection=Data Source=/var/lib/mingyue/mingyue.db"
   ```

5. Reload and restart:
   ```bash
   sudo systemctl daemon-reload
   sudo systemctl start mingyue
   ```

6. Verify it works:
   ```bash
   sudo systemctl status mingyue
   sudo journalctl -u mingyue -n 50
   ```

7. Clean up old directories (only after verifying everything works):
   ```bash
   sudo rm -rf /var/lib/mingyue
   sudo rm -rf /var/log/mingyue
   sudo rm -rf ~/.mingyue-cache
   sudo rm -rf /home/mingyue/.mingyue-cache
   ```

## Verifying the Migration

After migration, verify that:

1. Service is running:
   ```bash
   sudo systemctl status mingyue
   ```

2. Database is in the new location:
   ```bash
   ls -la /srv/mingyue/data/mingyue.db
   ```

3. Cache directory exists and is writable:
   ```bash
   ls -la /srv/mingyue/cache/
   ```

4. Application is accessible:
   ```bash
   curl http://localhost:5000
   ```

5. Check logs for any errors:
   ```bash
   sudo journalctl -u mingyue -n 100
   ```

## Troubleshooting

### Service fails to start

Check permissions:
```bash
sudo chown -R mingyue:mingyue /srv/mingyue
sudo chmod 755 /srv/mingyue
sudo chmod 755 /srv/mingyue/data
sudo chmod 755 /srv/mingyue/cache
```

### Database not found

Verify environment variable:
```bash
systemctl show mingyue.service --property=Environment
```

Should show:
```
Environment=MINGYUE_DATA_DIR=/srv/mingyue/data ...
```

### Cache errors

Check if cache directory is writable:
```bash
sudo -u mingyue touch /srv/mingyue/cache/test.txt
sudo -u mingyue rm /srv/mingyue/cache/test.txt
```

## Customizing Paths

If you want to use a different location (e.g., on a separate disk):

1. Create your custom directory:
   ```bash
   sudo mkdir -p /mnt/storage/mingyue/{data,cache}
   sudo chown -R mingyue:mingyue /mnt/storage/mingyue
   ```

2. Update systemd service environment variables:
   ```bash
   sudo nano /etc/systemd/system/mingyue.service
   ```
   
   Set:
   ```ini
   Environment=MINGYUE_DATA_DIR=/mnt/storage/mingyue/data
   Environment=MINGYUE_CACHE_DIR=/mnt/storage/mingyue/cache
   ReadWritePaths=/mnt/storage/mingyue
   ```

3. Reload and restart:
   ```bash
   sudo systemctl daemon-reload
   sudo systemctl restart mingyue
   ```

## Support

For detailed configuration options, see:
- `CONFIGURATION.md` - Comprehensive configuration guide
- `INSTALL.md` - Installation documentation

For issues:
- Check logs: `sudo journalctl -u mingyue -n 100`
- GitHub Issues: https://github.com/KOPElan/mingyue/issues
