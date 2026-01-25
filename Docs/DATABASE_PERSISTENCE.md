# Database Persistence Guide

## Overview

MingYue uses SQLite as its database. To ensure user data persists across application restarts, the database file must be stored in a location that is not deleted when the application is restarted or redeployed.

## Issue: Users Disappearing After Logout/Restart

If you experience users disappearing after logging out and attempting to log back in, this is caused by the database file being recreated or not persisted properly.

## Configuration

### Production Environment

In production, the database is configured to be stored in `/var/lib/mingyue/mingyue.db`. This is defined in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=/var/lib/mingyue/mingyue.db"
  }
}
```

**Important:** Make sure the directory `/var/lib/mingyue/` exists and the application has write permissions to it. The application will automatically create the directory if it doesn't exist.

### Development Environment

In development, the database is stored in the project directory as `mingyue.db`. This is defined in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=mingyue.db"
  }
}
```

**Note:** The `.gitignore` file excludes `*.db`, `*.db-shm`, and `*.db-wal` files to prevent committing development data to the repository.

## Docker Deployment

When deploying with Docker, ensure the database directory is mounted as a volume:

```yaml
version: '3.8'
services:
  mingyue:
    image: mingyue:latest
    volumes:
      - mingyue-data:/var/lib/mingyue
    ports:
      - "5000:5000"

volumes:
  mingyue-data:
```

Or with docker run:

```bash
docker run -d \
  -p 5000:5000 \
  -v mingyue-data:/var/lib/mingyue \
  mingyue:latest
```

## Systemd Service

When running as a systemd service, ensure the service has permissions to create and write to `/var/lib/mingyue/`:

```ini
[Service]
User=mingyue
Group=mingyue
ExecStart=/usr/bin/dotnet /opt/mingyue/MingYue.dll
WorkingDirectory=/opt/mingyue
# Ensure database directory exists
ExecStartPre=/bin/mkdir -p /var/lib/mingyue
ExecStartPre=/bin/chown mingyue:mingyue /var/lib/mingyue
```

## Manual Setup

If running the application manually:

```bash
# Create database directory
sudo mkdir -p /var/lib/mingyue
sudo chown $USER:$USER /var/lib/mingyue

# Run the application
dotnet run
```

## Backup and Restore

### Backup

```bash
# Stop the application first
sudo cp /var/lib/mingyue/mingyue.db /backup/location/mingyue-$(date +%Y%m%d).db
```

### Restore

```bash
# Stop the application first
sudo cp /backup/location/mingyue-20260125.db /var/lib/mingyue/mingyue.db
# Restart the application
```

## Troubleshooting

### Users disappear after logout

**Cause:** Database file is not persisted between application restarts.

**Solution:** 
1. Check that `/var/lib/mingyue/` directory exists
2. Verify the application has write permissions to the directory
3. Check logs for database migration errors
4. If using Docker, ensure the volume is properly mounted

### Permission denied errors

**Cause:** Application doesn't have write permissions to database directory.

**Solution:**
```bash
sudo chown -R $USER:$USER /var/lib/mingyue
# Or if running as a service:
sudo chown -R mingyue:mingyue /var/lib/mingyue
```

### Database locked errors

**Cause:** Multiple instances of the application trying to access the same database.

**Solution:** Ensure only one instance of the application is running at a time.

## Database Migrations

The application automatically applies database migrations on startup. When upgrading to a new version:

1. Backup your existing database
2. Start the new version
3. Migrations will apply automatically
4. If migrations fail, the application will log errors - restore from backup and report the issue
