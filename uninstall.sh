#!/bin/bash

# MingYue Uninstallation Script
# This script removes MingYue and its configuration

# Note: Not using 'set -e' to ensure uninstallation continues even if individual steps fail

# Color codes for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Configuration variables (must match install.sh)
APP_NAME="mingyue"
APP_USER="mingyue"
APP_GROUP="mingyue"
INSTALL_DIR="/opt/mingyue"
DATA_DIR="/var/lib/mingyue"
LOG_DIR="/var/log/mingyue"
SERVICE_NAME="mingyue.service"

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

# Stop and disable service
stop_service() {
    print_info "Stopping and disabling $SERVICE_NAME..."
    
    if systemctl is-active --quiet $SERVICE_NAME 2>/dev/null; then
        systemctl stop $SERVICE_NAME || print_warn "Failed to stop service, continuing..."
        print_info "Service stopped"
    else
        print_warn "Service is not running"
    fi
    
    if systemctl is-enabled --quiet $SERVICE_NAME 2>/dev/null; then
        systemctl disable $SERVICE_NAME || print_warn "Failed to disable service, continuing..."
        print_info "Service disabled"
    else
        print_warn "Service is not enabled"
    fi
}

# Remove service file
remove_service() {
    print_info "Removing systemd service file..."
    
    if [ -f "/etc/systemd/system/$SERVICE_NAME" ]; then
        rm -f "/etc/systemd/system/$SERVICE_NAME"
        systemctl daemon-reload
        print_info "Service file removed"
    else
        print_warn "Service file not found"
    fi
}

# Remove application files
remove_app_files() {
    print_info "Removing application files..."
    
    if [ -d "$INSTALL_DIR" ]; then
        rm -rf "$INSTALL_DIR"
        print_info "Application directory removed: $INSTALL_DIR"
    else
        print_warn "Application directory not found: $INSTALL_DIR"
    fi
}

# Remove data and logs
remove_data() {
    local REMOVE_DATA=false
    
    if [ -d "$DATA_DIR" ] || [ -d "$LOG_DIR" ]; then
        read -p "Do you want to remove data and logs? (y/N) " -n 1 -r
        echo
        if [[ $REPLY =~ ^[Yy]$ ]]; then
            REMOVE_DATA=true
        fi
    fi
    
    if [ "$REMOVE_DATA" = true ]; then
        if [ -d "$DATA_DIR" ]; then
            rm -rf "$DATA_DIR"
            print_info "Data directory removed: $DATA_DIR"
        fi
        
        if [ -d "$LOG_DIR" ]; then
            rm -rf "$LOG_DIR"
            print_info "Log directory removed: $LOG_DIR"
        fi
    else
        print_warn "Data and logs preserved"
        [ -d "$DATA_DIR" ] && print_info "Data directory: $DATA_DIR"
        [ -d "$LOG_DIR" ] && print_info "Log directory: $LOG_DIR"
    fi
}

# Remove sudoers configuration
remove_sudoers() {
    print_info "Removing sudoers configuration..."
    
    if [ -f "/etc/sudoers.d/mingyue" ]; then
        rm -f "/etc/sudoers.d/mingyue"
        print_info "Sudoers configuration removed"
    else
        print_warn "Sudoers configuration not found"
    fi
}

# Remove user and group
remove_user() {
    local REMOVE_USER=false
    
    read -p "Do you want to remove the $APP_USER user and group? (y/N) " -n 1 -r
    echo
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        REMOVE_USER=true
    fi
    
    if [ "$REMOVE_USER" = true ]; then
        if id "$APP_USER" &>/dev/null; then
            userdel -r $APP_USER 2>/dev/null || userdel $APP_USER 2>/dev/null || print_warn "Failed to remove user $APP_USER"
            if ! id "$APP_USER" &>/dev/null; then
                print_info "User $APP_USER removed"
            fi
        else
            print_warn "User $APP_USER not found"
        fi
        
        if getent group "$APP_GROUP" &>/dev/null; then
            if groupdel $APP_GROUP 2>/dev/null; then
                print_info "Group $APP_GROUP removed"
            else
                print_warn "Failed to remove group $APP_GROUP (may still have members or be in use by running processes)"
            fi
        else
            print_warn "Group $APP_GROUP not found"
        fi
    else
        print_warn "User and group preserved"
    fi
}

# Display post-uninstallation information
display_info() {
    echo ""
    echo "========================================"
    print_info "Uninstallation completed!"
    echo "========================================"
    echo ""
    
    if [ -d "$DATA_DIR" ] || [ -d "$LOG_DIR" ]; then
        echo "Preserved directories:"
        [ -d "$DATA_DIR" ] && echo "  Data: $DATA_DIR"
        [ -d "$LOG_DIR" ] && echo "  Logs: $LOG_DIR"
        echo ""
        echo "To remove these manually:"
        [ -d "$DATA_DIR" ] && echo "  sudo rm -rf $DATA_DIR"
        [ -d "$LOG_DIR" ] && echo "  sudo rm -rf $LOG_DIR"
        echo ""
    fi
}

# Main uninstallation process
main() {
    echo ""
    echo "========================================"
    echo "  MingYue Uninstallation Script"
    echo "========================================"
    echo ""
    
    check_root
    
    # Prompt for confirmation
    print_warn "This will uninstall MingYue from your system."
    read -p "Are you sure you want to continue? (y/N) " -n 1 -r
    echo
    if [[ ! $REPLY =~ ^[Yy]$ ]]; then
        print_info "Uninstallation cancelled"
        exit 0
    fi
    
    echo ""
    
    stop_service
    remove_service
    remove_app_files
    remove_data
    remove_sudoers
    remove_user
    display_info
}

# Run main uninstallation
main
