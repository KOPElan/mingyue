# MingYue (明月) - 家庭服务器门户

> 🌙 一款基于 .NET 10.0 和 FluentUI Blazor 开发的现代化家庭服务器管理平台

[![License](https://img.shields.io/badge/license-GPL--3.0-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-blue.svg)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)

## 📖 项目简介

MingYue（明月）是一个功能强大的家庭服务器门户系统，旨在为用户提供统一、美观、高效的服务器管理界面。本项目正在从 [KOPElan/qingfeng](https://github.com/KOPElan/qingfeng) 仓库迁移所有功能，并采用 Microsoft FluentUI Blazor 组件库进行现代化重构。

### 🎯 项目愿景

- **统一管理**: 一个入口管理所有家庭服务器功能
- **现代化界面**: 采用 FluentUI 设计语言，提供流畅美观的用户体验
- **高性能**: 基于 .NET 10.0 和 Blazor Server，确保快速响应
- **可扩展**: 模块化设计，易于扩展新功能
- **安全可靠**: 遵循安全最佳实践，保护用户数据

## ✨ 功能特性

### 🚀 当前已实现

- ✅ **系统监控**: 实时监控 CPU、内存、磁盘、网络状态
- ✅ **文件管理**: 完整的文件操作服务（浏览、上传、下载、搜索）
- ✅ **磁盘管理**: 本地磁盘挂载、网络磁盘（SMB/NFS）、电源管理
- ✅ **共享管理**: Samba/NFS 共享目录配置
- ✅ **Docker 服务**: 容器和镜像基础管理（需完善 UI）

### 🔧 计划迁移功能

详细迁移计划请参阅 [迁移计划文档](Docs/MIGRATION_PLAN.md)

#### Phase 1: 核心基础设施 (第1-2周)
- 🔄 **用户认证**: 登录、注册、权限管理
- 🔄 **应用管理**: 自定义应用快捷方式
- 🔄 **Dock 管理**: 固定常用应用到 Dock 栏
- 🔄 **个性化主页**: 可定制的主页布局

#### Phase 2: 主要功能 (第3-5周)
- 🔄 **文件管理器 UI**: 完整的文件浏览、预览界面
  - 图片、文本、PDF、Word、Excel 预览
  - 拖拽上传、批量操作
  - 缩略图和文件索引
- 🔄 **Docker 管理增强**: 完整的容器和镜像管理界面
- 🔄 **磁盘/共享管理 UI**: 友好的配置向导

#### Phase 3: 高级功能 (第6-8周)
- 🔄 **Web 终端**: 基于 xterm.js 的浏览器终端
- 🔄 **Anydrop**: 跨设备文件传输和消息分享
- 🔄 **计划任务**: Cron 定时任务调度
- 🔄 **系统设置**: 全局配置管理
- 🔄 **通知服务**: 实时通知系统，支持多种类型通知

#### Phase 4: 优化完善 (第9-10周)
- 🔄 **本地化**: 多语言支持（中文/英文）
- 🔄 **网络管理**: 网络接口配置
- 🔄 **性能优化**: 虚拟化、缓存、懒加载
- 🔄 **测试和文档**: 完整的测试覆盖和用户文档

## 🛠️ 技术栈

- **框架**: [.NET 10.0](https://dotnet.microsoft.com/)
- **UI**: [Blazor Server](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
- **组件库**: [Microsoft FluentUI Blazor](https://www.fluentui-blazor.net/) 4.13.2
- **数据库**: SQLite + Entity Framework Core 10.0
- **依赖注入**: ASP.NET Core DI
- **实时通信**: SignalR（用于终端和实时监控）

## 📦 快速开始

### 前置要求

- **.NET SDK 10.0** 或更高版本
- **操作系统**: Linux（主要支持）、Windows、macOS
- **（可选）Docker**: 用于容器管理功能
- **（Linux）系统工具**:
  - `lsblk`, `mount`, `umount` - 磁盘管理
  - `hdparm` - 磁盘电源管理（可选）
  - `cifs-utils`, `nfs-common` - 网络磁盘支持（可选）
  - `samba`, `nfs-kernel-server` - 共享服务（可选）

### 安装依赖（Ubuntu/Debian）

```bash
# 基础系统工具
sudo apt-get update
sudo apt-get install util-linux hdparm

# 网络磁盘支持
sudo apt-get install cifs-utils nfs-common

# 共享服务（可选）
sudo apt-get install samba samba-common-bin nfs-kernel-server
```

### 运行应用

```bash
# 克隆仓库
git clone https://github.com/KOPElan/mingyue.git
cd mingyue/MingYue

# 还原依赖
dotnet restore

# 运行应用
dotnet run

# 或指定端口运行
dotnet run --urls "http://0.0.0.0:5000"
```

访问 `http://localhost:5000` 查看应用。

### 构建和发布

```bash
# 构建项目
dotnet build

# 发布为独立应用
dotnet publish -c Release -o ./publish
```

## 📂 项目结构

```
MingYue/
├── Components/              # Blazor 组件
│   ├── Layout/             # 布局组件（MainLayout, NavMenu）
│   ├── Pages/              # 页面组件（Home, SystemMonitor 等）
│   └── Dialog/             # 对话框组件
├── Services/               # 业务逻辑服务
│   ├── ISystemMonitorService.cs
│   ├── SystemMonitorService.cs
│   ├── IFileManagementService.cs
│   ├── FileManagerService.cs
│   ├── IDiskManagementService.cs
│   ├── DiskManagementService.cs
│   └── ...
├── Models/                 # 数据模型和 DTO
├── Data/                   # 数据库上下文
│   ├── MingYueDbContext.cs
│   └── MingYueDbContextFactory.cs
├── Utilities/              # 工具类
├── wwwroot/                # 静态资源（CSS, JS, 图片）
├── appsettings.json        # 应用配置
└── Program.cs              # 应用入口

Docs/                       # 文档目录
├── CODE_STYLE.md           # 代码规范
├── FluentUI-Blazor-Guide.md # FluentUI 最佳实践
└── MIGRATION_PLAN.md       # 迁移计划文档
```

## 📚 文档

- [代码规范与最佳实践](Docs/CODE_STYLE.md)
- [FluentUI Blazor 开发规范](Docs/FluentUI-Blazor-Guide.md)
- [迁移计划文档](Docs/MIGRATION_PLAN.md)

## 🤝 贡献指南

我们欢迎任何形式的贡献！在提交 Pull Request 之前，请：

1. Fork 本仓库
2. 创建你的特性分支 (`git checkout -b feature/AmazingFeature`)
3. 遵循 [代码规范](Docs/CODE_STYLE.md)
4. 提交你的更改 (`git commit -m 'Add some AmazingFeature'`)
5. 推送到分支 (`git push origin feature/AmazingFeature`)
6. 打开一个 Pull Request

### 开发建议

- 阅读 [CODE_STYLE.md](Docs/CODE_STYLE.md) 了解代码规范
- 阅读 [FluentUI-Blazor-Guide.md](Docs/FluentUI-Blazor-Guide.md) 了解 UI 组件最佳实践
- 使用依赖注入管理服务
- 编写单元测试（目标覆盖率 >70%）
- 使用有意义的 commit 消息

## 🔒 安全注意事项

- 本应用设计用于内网环境，建议不要直接暴露到公网
- 如需公网访问，请配置反向代理（如 Nginx）和 HTTPS
- 定期更新依赖包以修复安全漏洞
- 磁盘挂载和系统命令需要 sudo 权限，请谨慎配置

## 📄 许可证

本项目采用 [GPL-3.0 许可证](LICENSE)。

## 🙏 致谢

本项目基于 [KOPElan/qingfeng](https://github.com/KOPElan/qingfeng) 进行功能迁移和重构，感谢原项目的开发工作。

## 📞 联系方式

- **问题反馈**: [GitHub Issues](https://github.com/KOPElan/mingyue/issues)
- **功能建议**: [GitHub Discussions](https://github.com/KOPElan/mingyue/discussions)

---

**注**: 本项目正在积极开发中，部分功能尚未完成。详细的开发进度请参阅 [迁移计划文档](Docs/MIGRATION_PLAN.md)。
