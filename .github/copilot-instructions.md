
# MingYue 项目 AI 协作指南

> 规范约束详见 .github/instructions/ 目录下相关文件（csharp.instructions.md、blazor.instructions.md、aspnet-rest-apis.instructions.md），所有编码建议以这些文件为准。

## 代码风格
- 统一采用 C# 10+ 语法，Blazor 组件与服务分层，详见 [.github/instructions/csharp.instructions.md] 及 .github/instructions/csharp.instructions.md。
- 命名规范：
	- 公共类型/成员用 PascalCase，私有字段用 _camelCase。
	- 异步方法以 Async 结尾。
- CSS 约束：
	- 禁止在 .razor 文件中直接书写 <style> 或 style 属性的样式表（除极特殊场景的必要内联样式外）。
	- 所有样式应放在同名 .razor.css 文件或全局样式表，优先使用样式表文件。
	- 组件样式用同名 .razor.css 文件，避免样式污染。
- 依赖注入、日志、异常、参数校验、资源本地化等见 [.github/instructions/csharp.instructions.md] 及 .github/instructions/blazor.instructions.md。
- Fluent UI Blazor 组件开发详见 [.github/docs/FluentUI-Blazor-Guide.md]。

## 架构与目录结构
- 主要目录：
	- `Components/`：Blazor 组件（页面、布局、对话框）
	- `Services/`：业务服务（接口+实现，依赖注入）
	- `Models/`：DTO/实体/查询参数/枚举
	- `Data/`：EF Core 上下文与迁移
	- `Resources/`：本地化资源
	- `wwwroot/`：静态资源
- 入口配置见 `Program.cs`，服务注册、数据库连接、FluentUI、HttpClient、控制器、定时任务等均在此集中配置。
- 典型 RESTful API 设计，控制器如 `AnydropController`，服务如 `AnydropService`，模型如 `AnydropModels`。

## 构建与测试
- 依赖还原：
	- `dotnet restore`（主依赖）
	- `npm install`（前端依赖，Dropzone）
- 构建：`dotnet build`
- 运行：`dotnet run` 或 `dotnet run --urls "http://0.0.0.0:5000"`
- 发布：`dotnet publish -c Release -o ./publish`
- 测试：
	- 单元测试推荐 xUnit，组件测试用 bUnit
	- 测试覆盖率目标 >70%
- 数据库迁移自动执行（见 Program.cs），如需手动：`dotnet ef migrations add <name>`、`dotnet ef database update`

## 项目约定
- 服务全部通过依赖注入注册，生命周期按需（Scoped/Singleton/Transient）。
- 用户界面与 API 返回均采用分页、过滤、排序参数（见 `PagedResult<T>`、`AnydropMessageQuery`）。
- 文件上传/下载/批量操作均有严格参数校验与日志。
- 资源字符串集中管理，使用 IStringLocalizer 进行本地化。
- 日志采用 ILogger<T>，避免记录敏感信息。
- 业务异常需记录日志并返回标准错误响应。
- 组件开发优先拆分小型、可复用单元，业务逻辑放服务层。
- API 设计遵循 RESTful，返回统一结构，错误用 ProblemDetails 或标准 JSON。

## 集成点与外部依赖
- UI 组件库：Microsoft FluentUI Blazor（见 [FluentUI-Blazor-Guide.md]）
- 数据库：SQLite，EF Core 10.0
- 实时通信：SignalR
- 文件上传：Dropzone（npm 依赖）
- 依赖注入：ASP.NET Core DI
- HttpClient 用于外部 API/元数据抓取
- 计划任务、通知、磁盘/Docker/网络管理等均有独立服务

## 安全
- 默认仅内网部署，公网需反代+HTTPS。
- 严格校验所有输入，防止命令注入、路径遍历、SSRF 等。
- Linux 下 mount/umount 需配置 sudoers，详见 Program.cs 日志提示。
- 配置项（如数据库连接、密钥）统一放 appsettings.json，不可硬编码。
- 日志避免输出敏感信息。
- 文件上传/下载/删除均有路径与文件名校验，防止越权访问。

## 参考文档
- [FluentUI Blazor 开发规范](.github/docs/FluentUI-Blazor-Guide.md)
- [.github/instructions/csharp.instructions.md]
- [.github/instructions/blazor.instructions.md]
- [.github/instructions/aspnet-rest-apis.instructions.md]

---
如需补充或有疑问，请在 PR 或 Issue 中反馈。
