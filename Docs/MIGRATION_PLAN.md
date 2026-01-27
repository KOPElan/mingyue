# MingYue 迁移计划文档
## QingFeng → MingYue 功能迁移与重构计划

**文档版本**: 1.0  
**创建日期**: 2026-01-21  
**项目目标**: 将 KOPElan/qingfeng 仓库的所有功能迁移到 MingYue，并根据最佳实践进行重构优化

---

## 目录
1. [项目概述](#1-项目概述)
2. [当前状态分析](#2-当前状态分析)
3. [迁移策略](#3-迁移策略)
4. [详细迁移计划](#4-详细迁移计划)
5. [重构优化要点](#5-重构优化要点)
6. [技术栈对比](#6-技术栈对比)
7. [风险评估](#7-风险评估)
8. [时间规划](#8-时间规划)

---

## 1. 项目概述

### 1.1 项目背景
MingYue（明月）是一个基于 .NET 10.0 和 Blazor Server 的家庭服务器门户项目，旨在提供统一的服务器管理界面。本项目计划迁移并实现 QingFeng（清风）仓库中的所有功能，同时进行代码重构以符合最佳实践。

### 1.2 项目目标
- ✅ 迁移 QingFeng 的所有核心功能到 MingYue
- ✅ 遵循 Fluent UI Blazor 最佳实践
- ✅ 优化代码质量和性能
- ✅ 统一代码风格和架构模式
- ✅ 提供完整的文档和测试覆盖

### 1.3 QingFeng 项目特性总结

QingFeng 是一款功能完善的家庭服务器主页，具备以下功能：

**核心功能模块**:
1. **个性化主页**: 自定义快捷方式、Dock 栏、应用管理
2. **系统监控**: CPU、内存、磁盘、网络实时监控
3. **磁盘管理**: 本地磁盘挂载、网络磁盘（SMB/NFS）、电源管理
4. **共享目录管理**: Samba/CIFS 和 NFS 共享配置
5. **Docker 管理**: 容器和镜像管理
6. **文件管理器**: 完整的文件操作、预览、上传下载
7. **Anydrop**: 跨设备文件传输和消息分享
8. **用户认证**: 登录、权限管理
9. **计划任务**: 定时任务调度和执行历史
10. **本地化**: 多语言支持
11. **系统设置**: 全局配置管理
12. **通知服务**: 实时通知系统，支持多种类型通知（信息、成功、警告、错误）

---

## 2. 当前状态分析

### 2.1 MingYue 现有实现

**已实现的功能**:
- ✅ 基础架构（Blazor Server + FluentUI）
- ✅ SQLite 数据库和 EF Core
- ✅ 系统监控服务（CPU、内存、网络、磁盘）
- ✅ 文件管理服务（完整的 CRUD 操作）
- ✅ 磁盘管理服务（挂载、网络磁盘、电源管理）
- ✅ 共享目录管理服务（初步实现）
- ✅ Docker 服务（基础功能）

**数据库架构**:
- ✅ 已激活: `FavoriteFolder`（收藏夹）
- 🔄 已定义但未激活:
  - `Application`, `DockItem`, `SystemSetting`
  - `User`, `ScheduledTask`, `ScheduledTaskExecutionHistory`
  - `AnydropMessage`, `AnydropAttachment`
  - `FileIndex`, `Thumbnail`
  - `Notification`（新增）


**UI 组件**:
- ✅ MainLayout（主布局）
- ✅ Home（主页仪表板）
- ✅ Error/NotFound 页面
- ✅ 重连模态框

### 2.2 QingFeng 完整功能清单

**Services (37 个服务)**:
```
核心服务:
- SystemMonitorService ✅ (MingYue已有)
- FileManagerService ✅ (MingYue已有)
- DiskManagementService ✅ (MingYue已有)
- ShareManagementService ✅ (MingYue已有)
- DockerService ✅ (MingYue已有，需增强)

待迁移服务:
- AuthenticationService ❌
- AuthenticationStateService ❌
- ApplicationService ❌
- DockItemService ❌
- SystemSettingService ✅ (Phase 3 完成, 2026-01-26)
- LocalizationService ❌
- NetworkManagementService ❌
- AnydropService ✅ (Phase 3 完成, 2026-01-26)
- FileIndexService ✅ (Phase 3 完成, 2026-01-26, 与ScheduledTask集成)
- FileUploadService ❌
- ThumbnailService ❌
- ScheduledTaskService ✅ (Phase 3 完成, 2026-01-26)
- ScheduledTaskExecutionHistoryService ✅ (Phase 3 完成, 2026-01-26, 已集成到ScheduledTaskService)
- ScheduledTaskExecutorService ✅ (Phase 3 完成, 2026-01-26, 含安全加固)
- NotificationService ✅ (Phase 3 完成, 2026-01-26)
```

**Components/Pages (17 个页面)**:
```
已迁移:
- Home.razor ✅ (基础版本)
- SystemMonitor.razor ✅ (基础版本)
- Error.razor ✅
- NotFound.razor ✅

待迁移:
- Login.razor ❌
- InitialSetup.razor ❌
- AppManagement.razor ❌
- DockManagement.razor ❌
- Docker.razor ❌ (需增强)
- FileManager.razor ❌ (需完整UI)
- DiskManagement.razor ❌ (需完整UI)
- ShareManagement.razor ❌ (需完整UI)
- Anydrop.razor ✅ (Phase 3 完成, 2026-01-26, 含聊天式UI重新设计)
- ScheduledTasks.razor ✅ (Phase 3 完成, 2026-01-26, 含5种任务类型)
- Settings.razor ✅ (Phase 3 完成, 2026-01-26, 5个类别)
- UserManagement.razor ❌
- NotificationPanel.razor ✅ (Phase 3 完成, 2026-01-26, 集成到MainLayout)
- NotificationTest.razor ✅ (Phase 3 完成, 2026-01-26, 测试页面)
```

**Endpoints (API 层)**:
```
待迁移:
- AuthenticationEndpoints ❌
- ApplicationEndpoints ❌
- DockItemEndpoints ❌
- AnydropEndpoints ✅ (Phase 3 完成, 2026-01-26, 使用Service直接调用)
- ScheduledTaskEndpoints ✅ (Phase 3 完成, 2026-01-26, 使用Service直接调用)
- SystemSettingEndpoints ✅ (Phase 3 完成, 2026-01-26, 使用Service直接调用)
- NotificationEndpoints ✅ (Phase 3 完成, 2026-01-26, 使用Service直接调用)
```

已有但需增强:
- SystemMonitorEndpoints ✅
- FileManagerEndpoints ✅
- DiskManagementEndpoints ✅
- ShareManagementEndpoints ✅
- DockerEndpoints ✅
```

**Models (数据模型)**:
```
待迁移约 30+ 个模型类和 DTO
```

---

## 3. 迁移策略

### 3.1 迁移原则

1. **渐进式迁移**: 按功能模块逐步迁移，确保每个阶段都可独立测试
2. **重构优先**: 不直接复制代码，而是根据 MingYue 的最佳实践重构
3. **FluentUI 优先**: 使用 FluentUI Blazor 组件替代 QingFeng 的 MudBlazor/Bootstrap
4. **测试驱动**: 每个功能迁移后立即编写单元测试
5. **文档同步**: 同步更新相关文档

### 3.2 优先级划分

**P0 - 核心基础设施** (第1-2周) ✅ **已完成 (2026-01-27)**:
- 用户认证系统 (含用户管理)
- 数据库架构完善
- 基础 UI 布局和导航

**P1 - 主要功能** (第3-5周) ✅ **已完成 (2026-01-27)**:
- 应用和 Dock 管理
- 完整的文件管理器 UI (支持图片/PDF/文本预览)
- Docker 管理增强
- 磁盘和共享管理 UI

**P2 - 高级功能** (第6-8周):
- Anydrop 文件传输
- 计划任务
- 系统设置
- 通知服务

**P3 - 优化和完善** (第9-10周):
- 本地化支持
- 性能优化
- 文档和测试
- UI/UX 优化

### 3.3 技术栈调整

| 组件 | QingFeng | MingYue | 迁移策略 |
|------|----------|---------|----------|
| UI 框架 | MudBlazor → FluentUI | FluentUI ✅ | 保持 FluentUI |
| .NET 版本 | .NET 10.0 | .NET 10.0 | 无需调整 |
| 数据库 | SQLite + EF Core | SQLite + EF Core | 无需调整 |
| 身份认证 | Custom + ProtectedLocalStorage | 待实现 | 迁移实现 |
| 文件预览 | PDF.js, Mammoth.js, SheetJS | 待实现 | 迁移实现 |

---

## 4. 详细迁移计划

### ~~Phase 1: 核心基础设施 (第1-2周)~~ ✅ **已完成 (2026-01-27)**

#### 1.1 用户认证系统
**目标**: 实现完整的用户登录、注册、权限管理

**已完成内容**:
- ✅ `AuthenticationService.cs` - 用户认证逻辑 (含 BCrypt 哈希)
- ✅ `AuthenticationStateService.cs` - 认证状态管理
- ✅ `Login.razor` - 登录页面
- ✅ `InitialSetup.razor` - 初始化设置页面 (首个用户自动设为 Admin)
- ✅ `UserManagement.razor` - 用户管理页面 (角色修改、删除用户)
- ✅ `AuthenticationEndpoints.cs` - 认证 API (通过 Service 直接调用)

**数据库变更**:
```csharp
// 启用 User 表
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "User"; // Admin/User
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}
```

**重构要点**:
- ✅ 密码哈希使用 BCrypt
- ✅ 实现基于 Service 的认证状态管理
- ✅ 遵循安全最佳实践

**验收标准**:
- [x] 用户可以注册和登录
- [x] 密码安全存储（哈希+盐）
- [x] 会话管理和超时
- [x] 初次运行引导设置管理员账户
- [x] 用户权限控制（Admin/User）

#### 1.2 应用和 Dock 管理
**目标**: 实现个性化主页的核心功能

**迁移内容**:
- `ApplicationService.cs` - 应用管理服务
- `DockItemService.cs` - Dock 项管理服务
- `AppManagement.razor` - 应用管理页面
- `DockManagement.razor` - Dock 管理页面
- `ApplicationEndpoints.cs` - 应用 API
- `DockItemEndpoints.cs` - Dock API

**数据库变更**:
```csharp
// 启用 Application 和 DockItem 表
public class Application
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsVisible { get; set; } = true;
}

public class DockItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsPinned { get; set; } = true;
}
```

**重构要点**:
- 使用 FluentUI 的 DataGrid 组件
- 拖放排序功能
- 图标选择器组件
- 表单验证

**验收标准**:
- [ ] 添加、编辑、删除应用
- [ ] 固定/取消固定到 Dock
- [ ] 拖拽排序
- [ ] 从 Docker 容器快速创建应用
- [ ] 图标自定义

#### 1.3 主页重构
**目标**: 实现完整的个性化主页

**迁移内容**:
- 增强 `Home.razor` - 添加应用网格、Dock 栏
- 快捷方式管理
- 系统信息卡片

**重构要点**:
- 使用 FluentUI 的响应式网格布局
- 卡片式设计
- 实时系统监控小部件
- 美观的图标和动画

**验收标准**:
- [ ] 显示所有应用（网格或列表视图）
- [ ] Dock 栏显示固定应用
- [ ] 系统监控摘要卡片
- [ ] 快速搜索和过滤
- [ ] 响应式设计（移动端适配）

---

### ~~Phase 2: 主要功能迁移 (第3-5周)~~ ✅ **已完成 (2026-01-27)**

#### 2.1 文件管理器完整 UI
**目标**: 实现功能完善的文件管理界面

**已完成内容**:
- ✅ `FileManager.razor` - 完整文件管理器页面
- ✅ 文件预览组件（图片、文本、PDF）
- ✅ 文件上传组件（支持分块上传、大文件、进度）
- ✅ `FileUploadService.cs` - 文件上传服务
- ✅ `ThumbnailService.cs` - 缩略图服务 (文件系统驱动)
- ✅ `FileIndexService.cs` - 文件索引服务 (文件系统驱动)

**数据库变更**:
- ✅ 已实现，并优化为文件系统驱动以提升性能

**重构要点**:
- ✅ 使用 FluentUI 的文件选择器和 Grid
- ✅ 基础 PDF 和文本预览
- ✅ 拖拽分块上传 (Dropzone.js)
- ✅ 虚拟化长列表

**验收标准**:
- [x] 文件浏览（网格/列表视图）
- [x] 文件操作（创建、删除、重命名、复制、移动）
- [x] 文件搜索（支持通配符和索引）
- [x] 文件上传（多文件、大文件、进度）
- [x] 文件下载
- [x] 文件预览（图片、文本、PDF）
- [x] 缩略图生成和缓存 (性能优化完成)
- [x] 面包屑导航
- [x] 文件夹收藏

#### 2.2 Docker 管理增强
**目标**: 提供完整的 Docker 容器和镜像管理

**迁移内容**:
- 增强 `DockerService.cs` - 添加更多 Docker 操作
- 完善 `Docker.razor` - 容器和镜像管理界面
- Docker 日志查看
- 快速创建快捷方式

**重构要点**:
- 使用 FluentUI 的选项卡和表格组件
- 实时状态更新
- 日志流式显示

**验收标准**:
- [ ] 容器列表（状态、端口、镜像）
- [ ] 容器操作（启动、停止、重启、删除）
- [ ] 镜像列表
- [ ] 镜像操作（删除、拉取）
- [ ] 容器日志查看
- [ ] 从容器创建应用快捷方式

#### 2.3 磁盘和共享管理 UI
**目标**: 提供友好的磁盘和共享管理界面

**迁移内容**:
- `DiskManagement.razor` - 磁盘管理页面
- `ShareManagement.razor` - 共享管理页面
- 挂载向导
- 网络磁盘配置

**重构要点**:
- 使用 FluentUI 的向导组件
- 表单验证
- 操作确认对话框

**验收标准**:
- [ ] 磁盘列表（本地磁盘和网络磁盘）
- [ ] 挂载/卸载操作
- [ ] 临时/永久挂载
- [ ] 网络磁盘挂载向导（SMB/NFS）
- [ ] 磁盘电源管理（休眠、APM）
- [ ] 共享目录列表（Samba/NFS）
- [ ] 添加/编辑/删除共享
- [ ] 服务重启

---

### ~~Phase 3: 高级功能迁移 (第6-8周)~~ ✅ **已完成 (2026-01-26)**

**完成状态**: 🎉 所有 Phase 3 功能已实现并通过安全审查

**实现详情**: 参见 [Phase 3 实现总结](../PHASE3_IMPLEMENTATION_SUMMARY.md)

#### 3.1 Anydrop 文件传输 ✅
**目标**: 实现跨设备文件和消息分享

**已完成内容** (2026-01-26):
- ✅ `AnydropService.cs` - Anydrop 服务（含事件驱动更新）
- ✅ `Anydrop.razor` - Anydrop 页面（**聊天式UI重新设计**）
- ✅ 使用 Service 直接调用（无需单独 API endpoints）
- ✅ 数据库表: AnydropMessage, AnydropAttachment

**实现亮点**:
- 🎨 **聊天式UI**: 两栏布局（左侧消息列表，右侧聊天气泡）
- 🔍 **实时搜索**: 搜索消息内容、设备名、附件名
- 🏷️ **快速筛选**: 全部/未读/文件/今天
- 📅 **日期分组**: 智能分组显示（今天/昨天/星期/日期）
- 🔄 **实时更新**: 事件驱动的消息同步
- 🔐 **竞态保护**: SemaphoreSlim 防止并发问题
- 📁 **文件处理**: 拖拽上传（10文件×100MB），类型图标，自动清理

**验收标准** - 全部完成:
- [x] 发送文本消息
- [x] 发送文件附件
- [x] 接收和查看消息
- [x] 下载附件
- [x] 删除消息
- [x] 未读标记
- [x] 设备识别（基于MAC地址）
- [x] 搜索和筛选功能
- [x] 聊天式UI

#### 3.2 计划任务 ✅
**目标**: 实现定时任务调度系统

**已完成内容** (2026-01-26):
- ✅ `ScheduledTaskService.cs` - 任务管理服务（含批量操作优化）
- ✅ `ScheduledTaskExecutorService.cs` - 后台执行服务（**含安全加固**）
- ✅ `ScheduledTasks.razor` - 计划任务页面（含5种任务类型）
- ✅ 使用 Service 直接调用（无需单独 API endpoints）
- ✅ 数据库表: ScheduledTask, ScheduledTaskExecutionHistory
- ✅ **新增**: Cronos (v0.11.1) 依赖用于Cron解析

**实现亮点**:
- 🔒 **安全加固**: 命令注入防护、解释器白名单、SSRF防护、路径遍历防护
- 📋 **5种任务类型**:
  - **Command**: Shell命令执行（含验证和转义）
  - **Script**: 脚本执行（白名单解释器：bash, sh, python3, node）
  - **HTTP**: HTTP请求（SSRF防护，私有IP阻断）
  - **FileIndex**: 文件索引（路径遍历防护）
  - **AnydropMigration**: 消息清理（批处理，输入验证）
- 📝 **上下文帮助**: 每种任务类型显示JSON配置示例
- ⚡ **性能优化**: 批量SQL删除，资源正确释放
- 🔄 **后台服务**: IHostedService每分钟检查待执行任务

**验收标准** - 全部完成:
- [x] 创建/编辑/删除任务
- [x] Cron 表达式编辑器（含示例）
- [x] 手动执行任务
- [x] 启用/禁用任务
- [x] 查看执行历史
- [x] 查看任务输出和错误
- [x] 5种任务类型（含新增的FileIndex和AnydropMigration）
- [x] 安全防护（命令注入、SSRF、路径遍历）

#### 3.3 系统设置 ✅
**目标**: 提供全局配置管理

**已完成内容** (2026-01-26):
- ✅ `SystemSettingService.cs` - 系统设置服务（含批量删除优化）
- ✅ `Settings.razor` - 设置页面（5个类别）
- ✅ 使用 Service 直接调用（无需单独 API endpoints）
- ✅ 数据库表: SystemSetting（已存在，已启用）

**实现亮点**:
- 📂 **5个设置类别**: General, Appearance, Security, FileManagement, Docker
- 🎯 **智能保存**: 下拉框/复选框自动保存，文本/数字手动保存
- 📤 **导入/导出**: JSON格式导入导出所有设置
- 🔄 **重置功能**: 批量SQL删除优化，一键恢复默认值
- 🔐 **访问控制**: Admin用户专属
- ⚡ **性能优化**: DeleteAllSettingsAsync使用ExecuteSqlRaw批量删除

**验收标准** - 全部完成:
- [x] 查看和编辑设置（5个类别，17个设置项）
- [x] 设置分类组织
- [x] 导入/导出JSON
- [x] 重置为默认值
- [x] 访问权限控制（Admin only）
- [x] 自动保存（下拉框/复选框）
- [x] 性能优化（批量操作）

#### 3.4 通知服务 ✅
**目标**: 提供实时通知系统

**已完成内容** (2026-01-26):
- ✅ `NotificationService.cs` - 通知服务（含事件驱动更新）
- ✅ `NotificationPanel.razor` - 通知面板（**完整UI集成**）
- ✅ `NotificationTest.razor` - 通知测试页面
- ✅ **MainLayout集成**: 通知图标+徽章+面板
- ✅ 使用 Service 直接调用（无需单独 API endpoints）
- ✅ 数据库表: Notifications

**实现亮点**:
- 🔔 **实时徽章**: 显示未读数量（>99显示"99+"）
- 🎨 **滑出面板**: 点击图标打开通知列表
- 🌈 **颜色编码**: 4种通知类型（信息/成功/警告/错误）
- ⏱️ **相对时间**: 中文时间显示（刚刚、5分钟前等）
- ✅ **批量操作**: 全部已读、清除已读
- 🔗 **动作URL**: 点击通知跳转到相关页面
- 🔐 **竞态保护**: SemaphoreSlim防止并发问题
- ⚡ **异步优化**: 正确的async/await，无fire-and-forget

**验收标准** - 全部完成:
- [x] 创建通知（4种类型）
- [x] 显示未读徽章
- [x] 通知面板UI
- [x] 标记已读/删除
- [x] 批量操作
- [x] 点击导航到ActionUrl
- [x] 实时更新
- [x] 竞态条件防护

---

**Phase 3 总结**:
- ✅ **代码行数**: ~6,500行（含UI、服务、安全加固）
- ✅ **文件数**: 21个文件创建/修改
- ✅ **安全审查**: 31个问题已解决
- ✅ **构建状态**: 成功（0错误，5个预存警告）
- ✅ **数据库迁移**: 已应用
- ✅ **生产就绪**: 是


- 分类组织设置
- 实时预览

**验收标准**:
- [ ] 常规设置（语言、时区）
- [ ] 外观设置（主题、颜色）
- [ ] 安全设置（会话超时）
- [ ] 文件管理设置（上传大小限制）
- [ ] Docker 设置（连接地址）
- [ ] 设置导入/导出

#### 3.4 通知服务
**目标**: 实现实时通知系统

**迁移内容**:
- `NotificationService.cs` - 通知服务
- `INotificationService.cs` - 通知服务接口
- `Notification` 数据模型
- 通知面板组件（MainLayout 集成）
- `NotificationEndpoints.cs` - 通知 API

**数据库变更**:
```csharp
// 启用 Notification 表
public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = "Info"; // Info, Success, Warning, Error
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public string? ActionUrl { get; set; }
    public string? Icon { get; set; }
}
```

**重构要点**:
- 使用 FluentUI 的徽章和对话框组件
- 事件驱动的实时更新（OnNotificationChanged 事件）
- 未读计数器自动更新
- 相对时间显示（刚刚、N分钟前等）
- 批量操作（全部已读、清除已读）

**验收标准**:
- [ ] 创建通知（支持多种类型：Info/Success/Warning/Error）
- [ ] MainLayout 显示未读计数徽章
- [ ] 通知面板展示所有通知
- [ ] 标记单条通知为已读
- [ ] 标记所有通知为已读
- [ ] 删除单条通知
- [ ] 批量删除已读通知
- [ ] 通知带操作链接（可选）
- [ ] 实时更新通知列表
- [ ] 相对时间格式化

---

### Phase 4: 优化和完善 (第9-10周)

#### 4.1 本地化支持
**目标**: 实现多语言界面

**迁移内容**:
- `LocalizationService.cs` - 本地化服务
- Resources 文件（中文、英文）
- 语言切换

**重构要点**:
- 使用 ASP.NET Core 本地化中间件
- 资源文件管理
- 前端字符串国际化

**验收标准**:
- [ ] 中文界面（默认）
- [ ] 英文界面
- [ ] 语言切换功能
- [ ] 所有页面和组件本地化

#### 4.2 网络管理
**目标**: 实现网络接口管理

**迁移内容**:
- `NetworkManagementService.cs` - 网络管理服务
- 网络设置页面

**重构要点**:
- 网络状态监控
- 静态 IP 配置

**验收标准**:
- [ ] 网络接口列表
- [ ] 网络统计信息
- [ ] 静态 IP 配置（可选）

#### 4.3 性能优化
**目标**: 提升应用性能和用户体验

**优化项**:
- [ ] 数据库查询优化（索引、异步）
- [ ] 长列表虚拟化
- [ ] 图片懒加载和缩略图
- [ ] 静态资源压缩和缓存
- [ ] SignalR 连接优化
- [ ] 内存使用优化

#### 4.4 测试和文档
**目标**: 完善测试和文档

**测试**:
- [ ] 单元测试（Services）
- [ ] 组件测试（使用 bUnit）
- [ ] 集成测试（API Endpoints）
- [ ] 端到端测试（可选）

**文档**:
- [ ] API 文档
- [ ] 用户指南
- [ ] 开发者文档
- [ ] 部署文档

---

## 5. 重构优化要点

### 5.1 代码风格统一

**遵循 CODE_STYLE.md**:
- 使用 file-scoped namespaces
- PascalCase 公共成员，camelCase 私有字段（带下划线）
- Async 方法以 `Async` 结尾
- 使用 `ArgumentNullException.ThrowIfNull`

**示例**:
```csharp
namespace MingYue.Services;

public class ApplicationService : IApplicationService
{
    private readonly ILogger<ApplicationService> _logger;
    private readonly IDbContextFactory<MingYueDbContext> _dbContextFactory;

    public ApplicationService(
        ILogger<ApplicationService> logger,
        IDbContextFactory<MingYueDbContext> dbContextFactory)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(dbContextFactory);
        
        _logger = logger;
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Application>> GetAllAsync()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Applications
            .OrderBy(a => a.Order)
            .ToListAsync();
    }
}
```

### 5.2 FluentUI 最佳实践

**遵循 FluentUI-Blazor-Guide.md**:

1. **服务注册**:
```csharp
builder.Services.AddFluentUIComponents(options =>
{
    options.UseTooltipServiceProvider = true;
    options.ValidateClassNames = true;
});
```

2. **布局提供器**:
```razor
<FluentDesignTheme StorageName="mingyue-theme" />
<FluentToastProvider />
<FluentDialogProvider />
<FluentTooltipProvider />
<FluentMessageBarProvider />
```

3. **组件使用**:
```razor
<!-- 按钮 -->
<FluentButton Appearance="Appearance.Accent" OnClick="HandleSubmit">
    保存
</FluentButton>

<!-- 数据表格 -->
<FluentDataGrid Items="@applications" Virtualize="true">
    <PropertyColumn Property="@(a => a.Name)" Sortable="true" />
    <PropertyColumn Property="@(a => a.Url)" />
    <TemplateColumn Title="操作">
        <FluentButton OnClick="@(() => EditApp(context))">编辑</FluentButton>
    </TemplateColumn>
</FluentDataGrid>

<!-- 表单 -->
<EditForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <DataAnnotationsValidator />
    <FluentTextField @bind-Value="model.Name" Label="名称" Required="true" />
    <FluentValidationMessage For="@(() => model.Name)" />
    <FluentButton Type="ButtonType.Submit" Loading="@isSubmitting">提交</FluentButton>
</EditForm>
```

### 5.3 安全性加固

**密码存储**:
```csharp
// 使用 BCrypt 或 PBKDF2
public string HashPassword(string password)
{
    return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
}

public bool VerifyPassword(string password, string hash)
{
    return BCrypt.Net.BCrypt.Verify(password, hash);
}
```

**输入验证**:
```csharp
// 文件路径验证
public bool IsPathSafe(string path)
{
    var normalizedPath = Path.GetFullPath(path);
    var allowedRoot = Path.GetFullPath("/");
    return normalizedPath.StartsWith(allowedRoot);
}

// Shell 命令参数验证
public string EscapeShellArgument(string argument)
{
    return $"\"{argument.Replace("\"", "\\\"")}\"";
}
```

**权限检查**:
```csharp
// 检查用户权限
public async Task<bool> HasPermissionAsync(string userId, string permission)
{
    using var context = await _dbContextFactory.CreateDbContextAsync();
    var user = await context.Users.FindAsync(userId);
    return user?.Role == "Admin" || user?.Permissions.Contains(permission) == true;
}
```

### 5.4 性能优化

**数据库查询**:
```csharp
// 使用异步和投影
public async Task<List<ApplicationDto>> GetApplicationsAsync()
{
    using var context = await _dbContextFactory.CreateDbContextAsync();
    return await context.Applications
        .AsNoTracking()
        .Select(a => new ApplicationDto
        {
            Id = a.Id,
            Name = a.Name,
            Url = a.Url,
            Icon = a.Icon
        })
        .ToListAsync();
}
```

**缓存策略**:
```csharp
// 缩略图缓存
private readonly MemoryCache _thumbnailCache = new MemoryCache(new MemoryCacheOptions
{
    SizeLimit = 100 // 限制缓存条目数
});

public async Task<byte[]> GetThumbnailAsync(string filePath)
{
    if (_thumbnailCache.TryGetValue(filePath, out byte[] cachedThumbnail))
    {
        return cachedThumbnail;
    }

    var thumbnail = await GenerateThumbnailAsync(filePath);
    _thumbnailCache.Set(filePath, thumbnail, new MemoryCacheEntryOptions
    {
        Size = 1,
        SlidingExpiration = TimeSpan.FromMinutes(30)
    });

    return thumbnail;
}
```

**虚拟化**:
```razor
<!-- 使用 Virtualize 组件处理长列表 -->
<FluentDataGrid Items="@items" Virtualize="true" ItemSize="50">
    <!-- 列定义 -->
</FluentDataGrid>
```

### 5.5 错误处理

**OperationResult 模式**:
```csharp
public enum ResultCode
{
    Success,
    NotFound,
    Conflict,
    ValidationError,
    Forbidden,
    InternalError
}

public class OperationResult
{
    public bool Success { get; init; }
    public ResultCode Code { get; init; }
    public string Message { get; init; } = string.Empty;
}

public class OperationResult<T> : OperationResult
{
    public T? Data { get; init; }
}

// 使用示例
public async Task<OperationResult<Application>> CreateApplicationAsync(Application app)
{
    try
    {
        ArgumentNullException.ThrowIfNull(app);
        
        if (string.IsNullOrWhiteSpace(app.Name))
        {
            return new OperationResult<Application>
            {
                Success = false,
                Code = ResultCode.ValidationError,
                Message = "应用名称不能为空"
            };
        }

        using var context = await _dbContextFactory.CreateDbContextAsync();
        context.Applications.Add(app);
        await context.SaveChangesAsync();

        return new OperationResult<Application>
        {
            Success = true,
            Code = ResultCode.Success,
            Data = app
        };
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "创建应用失败");
        return new OperationResult<Application>
        {
            Success = false,
            Code = ResultCode.InternalError,
            Message = "创建应用时发生错误"
        };
    }
}
```

---

## 6. 技术栈对比

| 类别 | QingFeng | MingYue | 迁移策略 |
|------|----------|---------|----------|
| **框架** | .NET 10.0 | .NET 10.0 | ✅ 保持 |
| **Blazor** | Server | Server | ✅ 保持 |
| **UI 库** | MudBlazor → FluentUI | FluentUI | ✅ 保持 FluentUI |
| **数据库** | SQLite | SQLite | ✅ 保持 |
| **ORM** | EF Core 10.0 | EF Core 10.0 | ✅ 保持 |
| **认证** | Custom + ProtectedLocalStorage | 无 | 🔄 迁移 |
| **Docker** | Docker.DotNet | Docker.DotNet | 🔄 需安装 |
| **终端** | xterm.js + SignalR | 无 | 🔄 迁移 |
| **文件预览** | PDF.js, Mammoth.js, SheetJS | 无 | 🔄 迁移 |
| **定时任务** | Custom IHostedService | 无 | 🔄 迁移 |
| **本地化** | ASP.NET Core Localization | 无 | 🔄 迁移 |

---

## 7. 风险评估

### 7.1 技术风险

| 风险项 | 影响 | 可能性 | 缓解措施 |
|--------|------|--------|----------|
| FluentUI 组件功能不足 | 中 | 低 | 必要时自定义组件或混合使用 |
| 性能问题（大量文件） | 高 | 中 | 虚拟化、分页、缓存 |
| SignalR 连接稳定性 | 中 | 中 | 重连机制、心跳检测 |
| 跨平台兼容性 | 中 | 低 | 特性检测、平台抽象 |
| 安全漏洞 | 高 | 中 | 代码审查、渗透测试 |

### 7.2 进度风险

| 风险项 | 影响 | 可能性 | 缓解措施 |
|--------|------|--------|----------|
| 功能范围蔓延 | 高 | 中 | 严格按优先级，P0-P3 分阶段 |
| 技术难点超预期 | 中 | 中 | 预留缓冲时间，及时求助 |
| 测试不充分 | 高 | 高 | 每阶段完成后立即测试 |

---

## 8. 时间规划

### 8.1 里程碑

| 阶段 | 时间 | 主要交付物 | 验收标准 |
|------|------|-----------|----------|
| **Phase 1** | 第1-2周 | 用户认证、应用/Dock管理、主页重构 | 可登录、管理应用、个性化主页 |
| **Phase 2** | 第3-5周 | 文件管理器UI、Docker增强、磁盘/共享UI | 完整文件操作、Docker管理、磁盘管理 |
| **Phase 3** | 第6-8周 | Anydrop、计划任务、系统设置、通知服务 | 文件传输、定时任务、系统配置 |
| **Phase 4** | 第9-10周 | 本地化、网络管理、优化、测试、文档 | 多语言、性能优化、完整文档 |

### 8.2 每周计划（示例：第1周）

| 日期 | 任务 | 产出 |
|------|------|------|
| Day 1 | 设计认证架构、创建 User 数据模型 | 数据库迁移、User 表 |
| Day 2 | 实现 AuthenticationService | 密码哈希、登录验证 |
| Day 3 | 实现 Login.razor 和 InitialSetup.razor | 登录界面、初始化界面 |
| Day 4 | 实现 AuthenticationEndpoints | 登录/注册 API |
| Day 5 | 测试认证流程、修复 Bug | 可用的认证系统 |
| Day 6-7 | 代码审查、文档、准备下周任务 | 代码提交、文档更新 |

### 8.3 关键路径

```
认证系统 → 应用管理 → 主页重构 → 文件管理器 → Docker管理 
                                                ↓
                           通知服务 ← 计划任务 ← Anydrop
                                ↓
                           优化测试 → 发布
```

---

## 9. 迁移检查清单

### 9.1 Phase 1 检查清单
- [x] User 数据模型和数据库迁移
- [x] AuthenticationService 实现
- [x] Login.razor 页面
- [x] InitialSetup.razor 页面
- [x] UserManagement.razor 页面
- [x] AuthenticationEndpoints API (通过 Service 整合)
- [x] 密码安全测试（哈希、盐值）
- [x] Application 和 DockItem 数据模型
- [x] ApplicationService 和 DockItemService
- [x] AppManagement.razor 和 DockManagement.razor
- [x] ApplicationEndpoints 和 DockItemEndpoints (通过 Service 整合)
- [x] Home.razor 重构（应用网格、Dock栏）
- [x] 拖拽排序功能
- [x] 图标选择器
- [ ] 单元测试（认证、应用服务）- 待加强
- [x] 文档更新

### 9.2 Phase 2 检查清单
- [x] FileIndex 和 Thumbnail 性能优化实现
- [x] FileUploadService 和 ThumbnailService
- [x] FileIndexService
- [x] FileManager.razor 完整UI
- [x] 文件预览组件（图片、文本、PDF）
- [x] 文件上传组件（拖拽、多文件、进度、分块）
- [x] 缩略图生成和缓存
- [x] DockerService 增强
- [x] Docker.razor 完整UI
- [x] 容器日志查看
- [x] DiskManagement.razor 完整UI
- [x] ShareManagement.razor 完整UI
- [x] 挂载向导
- [ ] 单元测试（文件、Docker、磁盘服务）- 待加强
- [x] 文档更新

### 9.3 Phase 3 检查清单
- [ ] AnydropMessage 和 AnydropAttachment 数据模型
- [ ] AnydropService
- [ ] Anydrop.razor 页面
- [ ] AnydropEndpoints API
- [ ] ScheduledTask 和 ExecutionHistory 数据模型
- [ ] ScheduledTaskService 和 ExecutorService
- [ ] ScheduledTasks.razor 页面
- [ ] Cron 表达式解析
- [ ] SystemSetting 数据模型
- [ ] SystemSettingService
- [ ] Settings.razor 页面
- [ ] Notification 数据模型
- [ ] NotificationService
- [ ] 通知面板组件
- [ ] NotificationEndpoints API
- [ ] 单元测试（Anydrop、任务、设置、通知服务）
- [ ] 文档更新

### 9.4 Phase 4 检查清单
- [ ] LocalizationService
- [ ] 资源文件（中文、英文）
- [ ] 所有页面本地化
- [ ] 语言切换功能
- [ ] NetworkManagementService
- [ ] 网络设置页面
- [ ] 数据库查询优化
- [ ] 虚拟化长列表
- [ ] 图片懒加载
- [ ] 静态资源压缩
- [ ] 内存优化
- [ ] 单元测试覆盖率 >70%
- [ ] 组件测试（bUnit）
- [ ] 集成测试
- [ ] API 文档
- [ ] 用户指南
- [ ] 开发者文档
- [ ] 部署文档
- [ ] 性能测试报告

---

## 10. 成功标准

### 10.1 功能完整性
- ✅ QingFeng 所有核心功能已迁移
- ✅ 所有功能经过测试验证
- ✅ 无重大 Bug

### 10.2 代码质量
- ✅ 遵循 CODE_STYLE.md 规范
- ✅ 遵循 FluentUI-Blazor-Guide.md 最佳实践
- ✅ 单元测试覆盖率 >70%
- ✅ 代码审查通过

### 10.3 性能指标
- ✅ 首页加载时间 <2秒
- ✅ 文件列表（1000+文件）渲染时间 <1秒
- ✅ 内存占用 <500MB（正常使用）
- ✅ 数据库查询响应时间 <100ms

### 10.4 用户体验
- ✅ 响应式设计（桌面、平板、手机）
- ✅ 操作流畅，无明显卡顿
- ✅ 错误提示友好
- ✅ 支持中英文界面

### 10.5 文档完整性
- ✅ 迁移计划文档 ✅（本文档）
- ✅ API 文档
- ✅ 用户指南
- ✅ 开发者文档
- ✅ README 更新

---

## 11. 附录

### 11.1 参考文档
- [CODE_STYLE.md](./CODE_STYLE.md) - MingYue 代码规范
- [FluentUI-Blazor-Guide.md](./FluentUI-Blazor-Guide.md) - FluentUI 最佳实践
- [QingFeng README](https://github.com/KOPElan/qingfeng/blob/main/README.md)
- [QingFeng MIGRATION_STATUS](https://github.com/KOPElan/qingfeng/blob/main/MIGRATION_STATUS.md)

### 11.2 相关链接
- [Fluent UI Blazor 官方文档](https://www.fluentui-blazor.net/)
- [ASP.NET Core Blazor 文档](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Entity Framework Core 文档](https://learn.microsoft.com/en-us/ef/core/)
- [SignalR 文档](https://learn.microsoft.com/en-us/aspnet/core/signalr/)

### 11.3 工具和库
- **UI**: Microsoft.FluentUI.AspNetCore.Components 4.13.2+
- **数据库**: Microsoft.EntityFrameworkCore.Sqlite 10.0.1+
- **Docker**: Docker.DotNet
- **密码哈希**: BCrypt.Net-Next
- **定时任务**: NCrontab（Cron表达式解析）
- **文件预览**: PDF.js, Mammoth.js, SheetJS

---

## 变更记录

| 版本 | 日期 | 作者 | 变更说明 |
|------|------|------|----------|
| 1.0 | 2026-01-21 | MingYue Team | 初始版本 |

---

**文档所有者**: MingYue Development Team  
**审核人**: To be assigned  
**最后更新**: 2026-01-21
