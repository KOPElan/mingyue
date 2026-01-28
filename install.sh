#!/bin/bash

# MingYue Installation Script
# This script installs and configures MingYue home server portal

set -e

# Color codes for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Configuration variables
APP_NAME="mingyue"
APP_USER="mingyue"
APP_GROUP="mingyue"
INSTALL_DIR="/opt/mingyue"
DATA_DIR="/var/lib/mingyue"
LOG_DIR="/var/log/mingyue"
SERVICE_NAME="mingyue.service"
DEFAULT_PORT=5000

# Print colored messages
print_info() {
    echo -e "${GREEN}[INFO]${NC} $1"
}

print_warn() {
    echo -e "${YELLOW}[WARN]${NC} $1"
}

print_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Check if running as root
check_root() {
    if [ "$EUID" -ne 0 ]; then 
        print_error "Please run this script as root (use sudo)"
        exit 1
    fi
}

# Detect Linux distribution
detect_distro() {
    if [ -f /etc/os-release ]; then
        . /etc/os-release
        DISTRO=$ID
        DISTRO_VERSION=$VERSION_ID
    elif [ -f /etc/lsb-release ]; then
        . /etc/lsb-release
        DISTRO=$DISTRIB_ID
        DISTRO_VERSION=$DISTRIB_RELEASE
    else
        DISTRO="unknown"
    fi
    
    print_info "Detected distribution: $DISTRO $DISTRO_VERSION"
}

# Install system dependencies
install_dependencies() {
    print_info "Installing system dependencies..."
    
    case $DISTRO in
        ubuntu|debian)
            apt-get update
            apt-get install -y \
                util-linux \
                hdparm \
                cifs-utils \
                nfs-common \
                samba \
                samba-common-bin \
                nfs-kernel-server \
                curl \
                ca-certificates
            ;;
            
        centos|rhel|fedora)
            if command -v dnf &> /dev/null; then
                PKG_MGR="dnf"
            else
                PKG_MGR="yum"
            fi
            
            $PKG_MGR install -y \
                util-linux \
                hdparm \
                cifs-utils \
                nfs-utils \
                samba \
                samba-common \
                curl \
                ca-certificates
            ;;
            
        *)
            print_warn "Unsupported distribution: $DISTRO"
            print_warn "Please install dependencies manually:"
            print_warn "- util-linux, hdparm"
            print_warn "- cifs-utils, nfs-common/nfs-utils"
            print_warn "- samba, nfs-kernel-server/nfs-utils"
            ;;
    esac
    
    print_info "Dependencies installed successfully"
}

# Create application user
create_user() {
    print_info "Creating application user: $APP_USER"
    
    # Create group first
    if getent group "$APP_GROUP" &>/dev/null; then
        print_warn "Group $APP_GROUP already exists, skipping..."
    else
        groupadd --system $APP_GROUP
        print_info "Group $APP_GROUP created"
    fi
    
    if id "$APP_USER" &>/dev/null; then
        print_warn "User $APP_USER already exists, skipping..."
    else
        useradd --system --no-create-home --shell /bin/false --gid $APP_GROUP $APP_USER
        print_info "User $APP_USER created"
    fi
    
    # Add user to necessary groups for disk management
    usermod -aG disk $APP_USER || true
}

# Create directories
create_directories() {
    print_info "Creating application directories..."
    
    mkdir -p "$INSTALL_DIR"
    mkdir -p "$DATA_DIR"
    mkdir -p "$LOG_DIR"
    
    # Set ownership
    chown -R $APP_USER:$APP_GROUP "$INSTALL_DIR"
    chown -R $APP_USER:$APP_GROUP "$DATA_DIR"
    chown -R $APP_USER:$APP_GROUP "$LOG_DIR"
    
    # Set permissions
    chmod 755 "$INSTALL_DIR"
    chmod 755 "$DATA_DIR"
    chmod 755 "$LOG_DIR"
    
    print_info "Directories created successfully"
}

# Install application files
install_app() {
    print_info "Installing application files..."
    
    # Get the directory where the script is located
    SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
    
    # Copy all files from script directory to install directory
    if [ -d "$SCRIPT_DIR" ] && [ "$(ls -A "$SCRIPT_DIR")" ]; then
        cp -r "$SCRIPT_DIR"/* "$INSTALL_DIR/"
    else
        print_error "Failed to locate source files"
        exit 1
    fi
    
    # Ensure the main executable is executable
    if [ -f "$INSTALL_DIR/MingYue" ]; then
        chmod +x "$INSTALL_DIR/MingYue"
    elif [ -f "$INSTALL_DIR/mingyue" ]; then
        chmod +x "$INSTALL_DIR/mingyue"
    fi
    
    # Set ownership
    chown -R $APP_USER:$APP_GROUP "$INSTALL_DIR"
    
    print_info "Application files installed to $INSTALL_DIR"
}

# Configure sudoers for required commands
configure_sudoers() {
    print_info "Configuring sudoers for $APP_USER..."
    
    SUDOERS_FILE="/etc/sudoers.d/mingyue"
    
    cat > "$SUDOERS_FILE" <<EOF
# MingYue service commands
# WARNING: These permissions grant significant system access
# Review and restrict based on your security requirements
$APP_USER ALL=(ALL) NOPASSWD: /bin/mount
$APP_USER ALL=(ALL) NOPASSWD: /bin/umount
$APP_USER ALL=(ALL) NOPASSWD: /usr/bin/hdparm
$APP_USER ALL=(ALL) NOPASSWD: /usr/sbin/smartctl
$APP_USER ALL=(ALL) NOPASSWD: /bin/systemctl restart smbd
$APP_USER ALL=(ALL) NOPASSWD: /bin/systemctl restart nmbd
$APP_USER ALL=(ALL) NOPASSWD: /bin/systemctl restart nfs-kernel-server
$APP_USER ALL=(ALL) NOPASSWD: /bin/systemctl restart nfs-server
$APP_USER ALL=(ALL) NOPASSWD: /usr/bin/tee /etc/samba/smb.conf
$APP_USER ALL=(ALL) NOPASSWD: /usr/bin/tee /etc/exports
$APP_USER ALL=(ALL) NOPASSWD: /usr/sbin/exportfs
$APP_USER ALL=(ALL) NOPASSWD: /usr/sbin/exportfs -ra
EOF
    
    chmod 440 "$SUDOERS_FILE"
    
    print_warn "Sudoers configured with elevated privileges"
    print_warn "Review /etc/sudoers.d/mingyue and restrict as needed for your environment"
}

# Create systemd service
create_systemd_service() {
    print_info "Creating systemd service..."
    
    # Determine the executable name
    EXECUTABLE="MingYue"
    if [ ! -f "$INSTALL_DIR/MingYue" ] && [ -f "$INSTALL_DIR/mingyue" ]; then
        EXECUTABLE="mingyue"
    fi
    
    cat > "/etc/systemd/system/$SERVICE_NAME" <<EOF
[Unit]
Description=MingYue Home Server Portal
After=network.target

[Service]
Type=simple
User=$APP_USER
Group=$APP_GROUP
WorkingDirectory=$INSTALL_DIR
ExecStart=$INSTALL_DIR/$EXECUTABLE --urls "http://0.0.0.0:$DEFAULT_PORT"
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=mingyue
Environment=DOTNET_ENVIRONMENT=Production
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=MINGYUE_DATA_DIR=$DATA_DIR

# Security settings
# Note: Some restrictions are relaxed due to application requirements
# Review and tighten based on your security needs
NoNewPrivileges=true
PrivateTmp=true
ProtectSystem=full
ProtectHome=true
ReadWritePaths=$DATA_DIR $LOG_DIR
ProtectKernelTunables=true
ProtectControlGroups=true
RestrictRealtime=true

# Logging
StandardOutput=journal
StandardError=journal

[Install]
WantedBy=multi-user.target
EOF
    
    # Reload systemd
    systemctl daemon-reload
    
    print_info "Systemd service created"
}

# Enable and start service
enable_service() {
    print_info "Enabling and starting $SERVICE_NAME..."
    
    systemctl enable $SERVICE_NAME
    systemctl start $SERVICE_NAME
    
    sleep 2
    
    if systemctl is-active --quiet $SERVICE_NAME; then
        print_info "Service started successfully"
        print_info "Status:"
        systemctl status $SERVICE_NAME --no-pager | head -15
    else
        print_error "Failed to start service"
        print_info "Check logs with: journalctl -u $SERVICE_NAME -n 50"
        exit 1
    fi
}

# Display post-installation information
display_info() {
    echo ""
    echo "========================================"
    print_info "Installation completed successfully!"
    echo "========================================"
    echo ""
    echo "Application installed to: $INSTALL_DIR"
    echo "Data directory: $DATA_DIR"
    echo "Log directory: $LOG_DIR"
    echo ""
    echo "Service name: $SERVICE_NAME"
    echo "Service status: systemctl status $SERVICE_NAME"
    echo "Service logs: journalctl -u $SERVICE_NAME -f"
    echo ""
    echo "Access the application at: http://$(hostname -I | awk '{print $1}'):$DEFAULT_PORT"
    echo "or: http://localhost:$DEFAULT_PORT"
    echo ""
    echo "Note: You may need to open port $DEFAULT_PORT in your firewall:"
    echo "  Ubuntu/Debian: sudo ufw allow $DEFAULT_PORT/tcp"
    echo "  CentOS/RHEL:   sudo firewall-cmd --permanent --add-port=$DEFAULT_PORT/tcp"
    echo "                 sudo firewall-cmd --reload"
    echo ""
    echo "Useful commands:"
    echo "  Start:   sudo systemctl start $SERVICE_NAME"
    echo "  Stop:    sudo systemctl stop $SERVICE_NAME"
    echo "  Restart: sudo systemctl restart $SERVICE_NAME"
    echo "  Status:  sudo systemctl status $SERVICE_NAME"
    echo "  Logs:    sudo journalctl -u $SERVICE_NAME -f"
    echo ""
}

# Main installation process
main() {
    echo ""
    echo "========================================"
    echo "  MingYue Installation Script"
    echo "========================================"
    echo ""
    
    check_root
    detect_distro
    
    # Prompt for confirmation
    read -p "This will install MingYue and its dependencies. Continue? (y/N) " -n 1 -r
    echo
    if [[ ! $REPLY =~ ^[Yy]$ ]]; then
        print_info "Installation cancelled"
        exit 0
    fi
    
    install_dependencies
    create_user
    create_directories
    install_app
    configure_sudoers
    create_systemd_service
    enable_service
    display_info
}

# Run main installation
main
