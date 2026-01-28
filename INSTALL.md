# MingYue Installation Guide

This guide explains how to manually build and install MingYue on Linux systems.

## Using GitHub Actions (Recommended)

The easiest way to get a Linux release build is through GitHub Actions:

1. Go to the [Actions tab](https://github.com/KOPElan/mingyue/actions) in the repository
2. Select "Build Linux Release" workflow
3. Click "Run workflow"
4. Enter a version number (e.g., `1.0.0`) or use the default
5. Download the generated artifact `mingyue-linux-x64-*.tar.gz`

## Manual Installation from Release Package

### Prerequisites

- Linux operating system (Ubuntu/Debian/CentOS/RHEL/Fedora)
- Root/sudo access
- Internet connection for downloading dependencies

### Installation Steps

1. **Download the release package** (from GitHub Actions artifacts)

2. **Extract the package**
   ```bash
   tar -xzf mingyue-linux-x64-1.0.0.tar.gz
   cd mingyue-linux-x64-1.0.0
   ```

3. **Run the installation script**
   ```bash
   sudo ./install.sh
   ```

The installation script will:
- Install required system dependencies (Samba, NFS, disk utilities)
- Create a dedicated `mingyue` user
- Set up directories (`/opt/mingyue`, `/var/lib/mingyue`, `/var/log/mingyue`)
- Configure sudo permissions for necessary system commands
- Create and enable a systemd service
- Start the application automatically

4. **Access the application**
   ```
   http://localhost:5000
   ```
   or
   ```
   http://<your-server-ip>:5000
   ```

## Manual Build from Source

If you prefer to build from source:

### Prerequisites

- .NET SDK 10.0 or higher
- Git

### Build Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/KOPElan/mingyue.git
   cd mingyue
   ```

2. **Build for Linux**
   ```bash
   dotnet publish MingYue/MingYue.csproj \
     -c Release \
     -r linux-x64 \
     --self-contained true \
     -p:PublishSingleFile=true \
     -o ./publish/linux-x64
   ```

3. **Copy installation scripts**
   ```bash
   cp install.sh uninstall.sh ./publish/linux-x64/
   cd ./publish/linux-x64/
   ```

4. **Run installation**
   ```bash
   sudo ./install.sh
   ```

## Post-Installation

### Firewall Configuration

If you have a firewall enabled, you'll need to allow traffic on port 5000:

**Ubuntu/Debian (ufw):**
```bash
sudo ufw allow 5000/tcp
sudo ufw reload
```

**CentOS/RHEL/Fedora (firewalld):**
```bash
sudo firewall-cmd --permanent --add-port=5000/tcp
sudo firewall-cmd --reload
```

### Service Management

```bash
# Check service status
sudo systemctl status mingyue

# Start service
sudo systemctl start mingyue

# Stop service
sudo systemctl stop mingyue

# Restart service
sudo systemctl restart mingyue

# View logs
sudo journalctl -u mingyue -f
```

### Directories

- **Installation**: `/opt/mingyue`
- **Data**: `/var/lib/mingyue` (database, uploaded files, etc.)
  - The SQLite database is configured to be stored at `/var/lib/mingyue/mingyue.db`
- **Logs**: `/var/log/mingyue`

### Configuration

Edit the systemd service file to customize settings:
```bash
sudo nano /etc/systemd/system/mingyue.service
```

After making changes, reload and restart:
```bash
sudo systemctl daemon-reload
sudo systemctl restart mingyue
```

### Port Configuration

By default, MingYue runs on port 5000. To change it:

1. Edit `/etc/systemd/system/mingyue.service`
2. Modify the `ExecStart` line:
   ```
   ExecStart=/opt/mingyue/MingYue --urls "http://0.0.0.0:YOUR_PORT"
   ```
3. Reload and restart:
   ```bash
   sudo systemctl daemon-reload
   sudo systemctl restart mingyue
   ```

## Uninstallation

To remove MingYue, use the provided uninstall script:

```bash
sudo ./uninstall.sh
```

The script will:
- Stop and disable the MingYue service
- Remove the systemd service file
- Remove application files from `/opt/mingyue`
- Optionally remove data and logs (prompts for confirmation)
- Remove sudoers configuration
- Optionally remove the mingyue user and group (prompts for confirmation)

If you don't have the uninstall script, you can remove MingYue manually:

```bash
# Stop and disable service
sudo systemctl stop mingyue
sudo systemctl disable mingyue

# Remove service file
sudo rm /etc/systemd/system/mingyue.service
sudo systemctl daemon-reload

# Remove application files
sudo rm -rf /opt/mingyue

# Remove sudoers configuration
sudo rm /etc/sudoers.d/mingyue

# (Optional) Remove data and logs
sudo rm -rf /var/lib/mingyue
sudo rm -rf /var/log/mingyue

# (Optional) Remove user
sudo userdel mingyue
```

## Troubleshooting

### Service fails to start

Check logs:
```bash
sudo journalctl -u mingyue -n 50
```

### Permission issues

Ensure the mingyue user has proper permissions:
```bash
sudo chown -R mingyue:mingyue /opt/mingyue
sudo chown -R mingyue:mingyue /var/lib/mingyue
```

### Port already in use

Change the port in the systemd service file (see Port Configuration above).

## Security Considerations

- MingYue is designed for internal network use
- For internet access, use a reverse proxy (Nginx/Apache) with HTTPS
- Regularly update dependencies for security patches
- **IMPORTANT**: The installation grants the `mingyue` user significant system privileges:
  - Sudo permissions for system commands (mount, systemctl, etc.)
- Review and restrict `/etc/sudoers.d/mingyue` based on your security requirements
- Configure firewall rules appropriately to limit access
- Consider using SELinux or AppArmor for additional security layers

## Support

- Issues: [GitHub Issues](https://github.com/KOPElan/mingyue/issues)
- Discussions: [GitHub Discussions](https://github.com/KOPElan/mingyue/discussions)
