# MingYue 日志管理和审计指南 / Logging and Auditing Guide

本文档说明 MingYue 如何进行日志记录、日志审计和历史回查。
This document explains how MingYue handles logging, auditing, and historical log review.

---

## 日志系统概述 / Logging System Overview

MingYue 使用 **systemd journal** 进行日志管理，而不是传统的文件日志。这是 Linux 系统服务的标准做法，提供了更强大的日志管理能力。

MingYue uses **systemd journal** for log management instead of traditional file-based logging. This is the standard practice for Linux system services and provides more powerful log management capabilities.

### 为什么选择 systemd journal？/ Why systemd journal?

**优势 / Advantages:**

1. **集中管理** / Centralized Management
   - 所有系统和服务日志统一管理
   - All system and service logs are managed centrally
   - 无需担心日志文件轮转和磁盘空间
   - No need to worry about log rotation and disk space

2. **结构化存储** / Structured Storage
   - 日志带有元数据（时间戳、优先级、服务名等）
   - Logs include metadata (timestamp, priority, service name, etc.)
   - 支持精确的字段查询
   - Supports precise field-based queries

3. **自动持久化** / Automatic Persistence
   - 日志自动持久化到磁盘
   - Logs are automatically persisted to disk
   - 系统重启后日志不丢失
   - Logs survive system reboots

4. **安全性** / Security
   - 日志文件防篡改
   - Log files are tamper-resistant
   - 基于权限的访问控制
   - Permission-based access control

---

## 日志查看 / Viewing Logs

### 基本查看命令 / Basic Viewing Commands

```bash
# 查看 MingYue 最新日志（实时）
# View MingYue latest logs (real-time)
sudo journalctl -u mingyue -f

# 查看最近 100 条日志
# View last 100 log entries
sudo journalctl -u mingyue -n 100

# 查看所有 MingYue 历史日志
# View all MingYue historical logs
sudo journalctl -u mingyue

# 从头开始查看日志
# View logs from the beginning
sudo journalctl -u mingyue --no-pager
```

### 按时间范围查询 / Query by Time Range

```bash
# 查看今天的日志
# View today's logs
sudo journalctl -u mingyue --since today

# 查看昨天的日志
# View yesterday's logs
sudo journalctl -u mingyue --since yesterday --until today

# 查看最近 1 小时的日志
# View logs from the last 1 hour
sudo journalctl -u mingyue --since "1 hour ago"

# 查看特定日期的日志
# View logs from a specific date
sudo journalctl -u mingyue --since "2024-01-20" --until "2024-01-21"

# 查看特定时间范围的日志
# View logs from a specific time range
sudo journalctl -u mingyue --since "2024-01-20 14:30:00" --until "2024-01-20 15:30:00"
```

### 按优先级过滤 / Filter by Priority

```bash
# 仅查看错误日志
# View only error logs
sudo journalctl -u mingyue -p err

# 查看警告及以上级别日志
# View warning and above level logs
sudo journalctl -u mingyue -p warning

# 优先级列表：emerg, alert, crit, err, warning, notice, info, debug
# Priority levels: emerg, alert, crit, err, warning, notice, info, debug
```

---

## 日志导出和归档 / Log Export and Archiving

### 导出为文本文件 / Export to Text File

```bash
# 导出所有 MingYue 日志到文件
# Export all MingYue logs to a file
sudo journalctl -u mingyue > /tmp/mingyue-logs.txt

# 导出特定时间范围的日志
# Export logs from a specific time range
sudo journalctl -u mingyue --since "2024-01-01" --until "2024-01-31" > /tmp/mingyue-january-2024.txt
```

### 导出为 JSON 格式（便于分析）/ Export to JSON Format (for Analysis)

```bash
# 导出为 JSON 格式
# Export to JSON format
sudo journalctl -u mingyue -o json > /tmp/mingyue-logs.json

# 导出为 JSON-Pretty 格式（便于阅读）
# Export to JSON-Pretty format (human-readable)
sudo journalctl -u mingyue -o json-pretty > /tmp/mingyue-logs-pretty.json
```

### 持续归档策略 / Continuous Archiving Strategy

```bash
# 创建每日日志备份脚本
# Create daily log backup script
cat > /usr/local/bin/backup-mingyue-logs.sh <<'EOF'
#!/bin/bash
DATE=$(date +%Y-%m-%d)
BACKUP_DIR="/srv/mingyue/log-archives"
mkdir -p "$BACKUP_DIR"

# 导出前一天的日志
# Export previous day's logs
journalctl -u mingyue --since yesterday --until today > "$BACKUP_DIR/mingyue-$DATE.log"

# 可选：压缩日志
# Optional: compress logs
gzip "$BACKUP_DIR/mingyue-$DATE.log"

# 可选：删除 90 天前的归档
# Optional: delete archives older than 90 days
find "$BACKUP_DIR" -name "mingyue-*.log.gz" -mtime +90 -delete
EOF

chmod +x /usr/local/bin/backup-mingyue-logs.sh

# 使用 cron 每天执行
# Run daily with cron
# 添加到 crontab: 0 1 * * * /usr/local/bin/backup-mingyue-logs.sh
# Add to crontab: 0 1 * * * /usr/local/bin/backup-mingyue-logs.sh
```

---

## 日志持久化配置 / Log Persistence Configuration

systemd journal 默认持久化日志。可以配置日志保留策略：

systemd journal persists logs by default. You can configure log retention policies:

```bash
# 编辑 journald 配置
# Edit journald configuration
sudo nano /etc/systemd/journald.conf
```

关键配置参数 / Key configuration parameters:

```ini
[Journal]
# 日志存储位置（persistent = 持久化到磁盘）
# Log storage location (persistent = persist to disk)
Storage=persistent

# 日志最大占用磁盘空间（例如 1G）
# Maximum disk space for logs (e.g., 1G)
SystemMaxUse=1G

# 单个日志文件最大大小
# Maximum size of a single log file
SystemMaxFileSize=100M

# 日志保留时间（例如保留 90 天）
# Log retention time (e.g., keep for 90 days)
MaxRetentionSec=90day
```

应用配置 / Apply configuration:

```bash
sudo systemctl restart systemd-journald
```

---

## 审计功能 / Audit Features

### 1. 日志完整性验证 / Log Integrity Verification

systemd journal 支持日志签名验证（Forward Secure Sealing）：

systemd journal supports log signature verification (Forward Secure Sealing):

```bash
# 查看日志文件状态
# View log file status
sudo journalctl --header

# 验证日志完整性（如果启用了 FSS）
# Verify log integrity (if FSS is enabled)
sudo journalctl --verify
```

### 2. 精确的时间戳 / Precise Timestamps

所有日志都带有微秒级时间戳：

All logs include microsecond-precision timestamps:

```bash
# 显示详细时间戳
# Show detailed timestamps
sudo journalctl -u mingyue -o short-precise
```

### 3. 用户操作审计 / User Operation Audit

如果需要审计特定用户的操作，可以在应用代码中添加审计日志：

For auditing specific user operations, you can add audit logs in the application code:

```csharp
// 示例：在关键操作处添加审计日志
// Example: Add audit logs for critical operations
_logger.LogWarning("AUDIT: User {Username} performed {Action} at {Timestamp}", 
    username, action, DateTime.UtcNow);
```

然后可以通过关键字搜索审计日志：

Then search for audit logs by keywords:

```bash
# 搜索包含 "AUDIT" 的日志
# Search for logs containing "AUDIT"
sudo journalctl -u mingyue | grep AUDIT

# 搜索特定用户的审计日志
# Search for audit logs of a specific user
sudo journalctl -u mingyue | grep "AUDIT.*User.*admin"
```

### 4. 导出到外部审计系统 / Export to External Audit Systems

可以将日志转发到外部系统（如 Elasticsearch、Splunk 等）：

You can forward logs to external systems (like Elasticsearch, Splunk, etc.):

```bash
# 使用 journald 的远程日志功能
# Use journald's remote logging feature
sudo systemctl enable systemd-journal-upload
sudo systemctl start systemd-journal-upload
```

或使用 rsyslog 转发：

Or use rsyslog for forwarding:

```bash
# 安装 rsyslog
# Install rsyslog
sudo apt-get install rsyslog

# 配置转发规则
# Configure forwarding rules
sudo nano /etc/rsyslog.d/50-mingyue.conf
```

添加配置 / Add configuration:

```
# 转发 mingyue 日志到远程服务器
# Forward mingyue logs to remote server
if $programname == 'mingyue' then @@remote-log-server:514
```

---

## 高级查询示例 / Advanced Query Examples

### 复杂过滤 / Complex Filtering

```bash
# 查找包含特定关键字的错误日志
# Find error logs containing specific keywords
sudo journalctl -u mingyue -p err | grep "database"

# 查看特定时间段的警告和错误
# View warnings and errors in a specific time period
sudo journalctl -u mingyue -p warning --since "2024-01-20 10:00" --until "2024-01-20 11:00"

# 统计错误数量
# Count number of errors
sudo journalctl -u mingyue -p err --since today | wc -l
```

### 性能分析 / Performance Analysis

```bash
# 查看启动时间
# View startup time
sudo journalctl -u mingyue | grep "Application started"

# 查看服务重启记录
# View service restart records
sudo journalctl -u mingyue | grep -E "Started|Stopped"

# 分析日志输出速率
# Analyze log output rate
sudo journalctl -u mingyue --since "1 hour ago" | wc -l
```

---

## 日志查看工具 / Log Viewing Tools

### 1. 命令行工具 / Command-line Tools

```bash
# journalctl - 标准工具
# journalctl - standard tool
sudo journalctl -u mingyue

# journalctl + jq - JSON 分析
# journalctl + jq - JSON analysis
sudo journalctl -u mingyue -o json | jq '.MESSAGE'

# journalctl + less - 分页查看
# journalctl + less - paginated viewing
sudo journalctl -u mingyue --no-pager | less
```

### 2. Web 界面工具 / Web Interface Tools

如果需要 Web 界面查看日志，可以安装：

For web interface log viewing, you can install:

- **Cockpit** - Red Hat 官方的 Web 管理界面
- **Grafana Loki** - 日志聚合和可视化
- **Graylog** - 日志管理平台

安装 Cockpit 示例 / Cockpit installation example:

```bash
sudo apt-get install cockpit
sudo systemctl enable --now cockpit.socket
# 访问 https://server-ip:9090
# Access https://server-ip:9090
```

---

## 对比文件日志的优势 / Advantages Over File-based Logging

| 功能特性 / Feature | systemd journal | 文件日志 / File Logs |
|-------------------|-----------------|---------------------|
| 结构化查询 / Structured Queries | ✅ 支持 / Yes | ❌ 需要额外工具 / Needs extra tools |
| 时间范围查询 / Time Range Queries | ✅ 原生支持 / Native | ❌ 需要手动过滤 / Manual filtering |
| 日志轮转 / Log Rotation | ✅ 自动 / Automatic | ❌ 需要配置 / Needs configuration |
| 持久化 / Persistence | ✅ 自动 / Automatic | ❌ 需要配置 / Needs configuration |
| 优先级过滤 / Priority Filtering | ✅ 原生支持 / Native | ❌ 需要自定义 / Custom needed |
| 防篡改 / Tamper-resistant | ✅ 支持签名 / Supports signing | ❌ 容易修改 / Easily modified |
| 磁盘空间管理 / Disk Space Management | ✅ 自动限制 / Auto-limited | ❌ 需要监控 / Needs monitoring |
| 系统集成 / System Integration | ✅ 完全集成 / Fully integrated | ❌ 独立管理 / Separate management |

---

## 满足审计合规要求 / Meeting Audit Compliance Requirements

systemd journal 满足大多数审计和合规要求：

systemd journal meets most audit and compliance requirements:

### ✅ 支持的审计需求 / Supported Audit Requirements

1. **日志完整性** / Log Integrity
   - 使用 Forward Secure Sealing 防篡改
   - Tamper-resistant with Forward Secure Sealing

2. **时间戳准确性** / Timestamp Accuracy
   - 微秒级精确时间戳
   - Microsecond-precision timestamps

3. **日志持久化** / Log Persistence
   - 自动持久化到磁盘，重启不丢失
   - Auto-persisted to disk, survives reboots

4. **访问控制** / Access Control
   - 基于 Linux 用户权限管理
   - Based on Linux user permissions

5. **日志导出** / Log Export
   - 支持多种格式导出（文本、JSON 等）
   - Supports multiple export formats (text, JSON, etc.)

6. **长期归档** / Long-term Archiving
   - 可配置保留策略或定期导出
   - Configurable retention or periodic export

7. **审计追踪** / Audit Trail
   - 完整的操作历史记录
   - Complete operation history

---

## 常见问题 / FAQ

### Q1: 如何长期保存日志？/ How to preserve logs long-term?

**A:** 使用定期导出脚本（见上文"持续归档策略"），将日志导出为文件并存储到安全位置。

**A:** Use periodic export scripts (see "Continuous Archiving Strategy" above) to export logs as files and store them in a secure location.

### Q2: 日志会占用太多磁盘空间吗？/ Will logs consume too much disk space?

**A:** 不会。可以在 `/etc/systemd/journald.conf` 中配置 `SystemMaxUse` 限制最大磁盘使用量。

**A:** No. You can configure `SystemMaxUse` in `/etc/systemd/journald.conf` to limit maximum disk usage.

### Q3: 如何查找特定操作的日志？/ How to find logs for a specific operation?

**A:** 使用 `grep` 或 `journalctl` 的过滤功能结合时间范围查询。

**A:** Use `grep` or `journalctl`'s filtering features combined with time range queries.

### Q4: 可以集成到现有的日志管理系统吗？/ Can it integrate with existing log management systems?

**A:** 可以。systemd journal 可以通过 rsyslog、fluentd 等工具转发到 Elasticsearch、Splunk 等系统。

**A:** Yes. systemd journal can forward to Elasticsearch, Splunk, etc. via rsyslog, fluentd, and other tools.

### Q5: 如何确保日志不被篡改？/ How to ensure logs are not tampered with?

**A:** 启用 Forward Secure Sealing (FSS) 功能，日志文件会被加密签名。

**A:** Enable Forward Secure Sealing (FSS) feature for cryptographically signed log files.

```bash
# 启用 FSS
# Enable FSS
sudo journalctl --setup-keys
```

---

## 推荐的日志管理流程 / Recommended Log Management Workflow

1. **日常监控** / Daily Monitoring
   ```bash
   sudo journalctl -u mingyue -f
   ```

2. **定期审计** / Regular Auditing
   ```bash
   # 每周审计一次错误日志
   # Weekly audit of error logs
   sudo journalctl -u mingyue -p err --since "7 days ago"
   ```

3. **月度归档** / Monthly Archiving
   ```bash
   # 每月导出日志到归档目录
   # Monthly export to archive directory
   sudo journalctl -u mingyue --since "30 days ago" > /srv/mingyue/archives/$(date +%Y-%m).log
   ```

4. **年度备份** / Annual Backup
   ```bash
   # 每年备份所有历史日志
   # Annual backup of all historical logs
   sudo journalctl -u mingyue > /backup/mingyue-logs-$(date +%Y).log
   ```

---

## 总结 / Summary

systemd journal **完全可以满足日志回查和审计需求**，并且在很多方面优于传统文件日志：

systemd journal **fully meets log review and audit requirements**, and surpasses traditional file-based logging in many ways:

- ✅ 支持精确的时间范围查询
- ✅ 支持结构化字段查询
- ✅ 支持日志完整性验证
- ✅ 自动持久化和轮转管理
- ✅ 可导出为多种格式进行归档
- ✅ 可集成到外部审计系统

如有特殊的审计需求，可以通过定期导出和外部系统集成来实现。

For special audit requirements, they can be achieved through periodic exports and external system integration.
