# MingYue 本地化指南 / Localization Guide

## 概述 / Overview

MingYue 使用 .NET 内置的本地化功能，通过 `.resx` 资源文件和 `IStringLocalizer` 接口提供简体中文（zh-CN）和英语（en）的多语言支持。

MingYue uses .NET's built-in localization features with `.resx` resource files and the `IStringLocalizer` interface to provide Simplified Chinese (zh-CN) and English (en) language support.

## 支持的语言 / Supported Languages

- **简体中文** (zh-CN) - 默认语言 / Default Language
- **English** (en)

## 架构 / Architecture

### 资源文件 / Resource Files

位置 / Location: `MingYue/Resources/`

- `SharedResources.cs` - 资源类定义 / Resource class definition
- `SharedResources.zh-CN.resx` - 简体中文资源 / Simplified Chinese resources
- `SharedResources.en.resx` - 英语资源 / English resources

每个资源文件包含 137+ 键值对，覆盖以下类别：

Each resource file contains 137+ key-value pairs covering:
- 通用术语 / Common terms
- 导航菜单 / Navigation menus
- 表单标签 / Form labels
- 操作按钮 / Action buttons
- 消息提示 / Messages
- 页面标题 / Page titles

### 服务层 / Service Layer

**LocalizationService** (`MingYue/Services/LocalizationService.cs`)
- 管理当前语言设置 / Manages current language setting
- 提供可用语言列表 / Provides list of available cultures
- 处理语言切换事件 / Handles culture change events
- 持久化语言设置到数据库 / Persists language preference to database

### 配置 / Configuration

**Program.cs**
```csharp
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new System.Globalization.CultureInfo("zh-CN"),
        new System.Globalization.CultureInfo("en")
    };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("zh-CN");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
```

## 使用方法 / Usage

### 在 Razor 组件中 / In Razor Components

1. **添加必要的引用 / Add necessary usings:**
```csharp
@using MingYue.Resources
@using Microsoft.Extensions.Localization
```

2. **注入 Localizer / Inject the Localizer:**
```csharp
@inject IStringLocalizer<SharedResources> Localizer
```

3. **使用本地化字符串 / Use localized strings:**
```razor
<h2>@Localizer["Welcome"]</h2>
<FluentButton>@Localizer["Save"]</FluentButton>
<FluentLabel>@Localizer["Username"]</FluentLabel>
```

### 在 C# 服务中 / In C# Services

```csharp
public class MyService
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    
    public MyService(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;
    }
    
    public string GetMessage()
    {
        return _localizer["OperationSuccessful"];
    }
}
```

### 带参数的本地化 / Localization with Parameters

```csharp
// 资源文件中 / In resource file:
// "WelcomeUser" = "Welcome, {0}!"

// 使用 / Usage:
@Localizer["WelcomeUser", username]
```

## 添加新的翻译 / Adding New Translations

### 步骤 / Steps

1. **在两个资源文件中添加新键 / Add new key to both resource files:**

   在 `SharedResources.zh-CN.resx` 中:
   ```xml
   <data name="NewFeature" xml:space="preserve">
     <value>新功能</value>
   </data>
   ```

   在 `SharedResources.en.resx` 中:
   ```xml
   <data name="NewFeature" xml:space="preserve">
     <value>New Feature</value>
   </data>
   ```

2. **在组件中使用 / Use in components:**
   ```razor
   @Localizer["NewFeature"]
   ```

### 命名规范 / Naming Conventions

- 使用 PascalCase: `WelcomeMessage`, `SaveButton`
- 使用描述性名称: `ConfirmDeleteMessage` 而不是 `Msg1`
- 为相关键使用前缀: `Docker*`, `Task*`, `Settings*`

## 语言切换 / Language Switching

用户可以通过以下方式切换语言 / Users can switch languages via:

1. **设置页面 / Settings Page** (`/settings`)
   - 选择语言下拉菜单 / Language selection dropdown
   - 自动刷新页面应用更改 / Auto-refresh to apply changes

2. **语言选择器组件 / LanguageSelector Component**
   - 位于主布局导航栏 / Located in main layout navigation
   - 实时切换无需页面刷新 / Real-time switching via forced navigation

## 已本地化的组件 / Localized Components

### 完全本地化 / Fully Localized
- ✅ **Login.razor** - 登录页面 / Login page
- ✅ **InitialSetup.razor** - 初始设置 / Initial setup
- ✅ **Docker.razor** - Docker 管理 / Docker management
- ✅ **ScheduledTasks.razor** - 计划任务 / Scheduled tasks
- ✅ **Home.razor** - 主页 / Home page
- ✅ **FileManager.razor** - 文件管理 / File manager
- ✅ **MainLayout.razor** - 主布局 / Main layout
- ✅ **AppManagement.razor** - 应用管理 / Application management

### 部分本地化 / Partially Localized
- 🔄 **Settings.razor** - 设置页面（60%）/ Settings page (60%)

### 待本地化 / To Be Localized
- ⏳ DiskManagement, ShareManagement, Anydrop
- ⏳ NetworkSettings, UserManagement, DockManagement
- ⏳ Dialog components

## 资源键索引 / Resource Key Index

### 认证 / Authentication
- `Login`, `Logout`, `Username`, `Password`
- `SignIn`, `WelcomeToMingYue`, `PleaseSignInToContinue`
- `EnterYourUsername`, `EnterYourPassword`
- `CreateAdministratorAccount`

### 通用操作 / Common Actions
- `Save`, `Cancel`, `Delete`, `Edit`, `Add`, `Close`
- `Confirm`, `Yes`, `No`, `Search`, `Refresh`
- `Enable`, `Disable`, `Start`, `Stop`, `Restart`
- `Import`, `Export`, `Upload`, `Download`

### 导航 / Navigation
- `Home`, `Settings`, `Management`
- `SystemMonitor`, `FileManager`, `DiskManagement`
- `ShareManagement`, `DockerManagement`, `ScheduledTasks`
- `ApplicationManagement`, `UserManagement`, `DockManagement`

### 消息 / Messages
- `Success`, `Error`, `Warning`, `Info`
- `Loading`, `NoData`, `OperationSuccessful`, `OperationFailed`
- `ConfirmDelete`, `LanguageChanged`, `PleaseRefresh`

## 最佳实践 / Best Practices

1. **始终同时更新两个资源文件 / Always update both resource files**
   - 确保 zh-CN 和 en 文件中的键匹配
   - Ensure keys match in both zh-CN and en files

2. **避免在代码中硬编码字符串 / Avoid hardcoding strings in code**
   ```csharp
   // ❌ 错误 / Wrong
   <h2>系统设置</h2>
   
   // ✅ 正确 / Correct
   <h2>@Localizer["SystemSettings"]</h2>
   ```

3. **使用描述性键名 / Use descriptive key names**
   ```csharp
   // ❌ 避免 / Avoid
   @Localizer["Msg1"]
   
   // ✅ 推荐 / Recommended
   @Localizer["WelcomeMessage"]
   ```

4. **为复杂文本使用参数 / Use parameters for complex text**
   ```csharp
   @Localizer["ItemsSelected", count]
   ```

5. **保持资源文件组织有序 / Keep resource files organized**
   - 使用 XML 注释分组相关键 / Use XML comments to group related keys
   - 按字母顺序排序（非必需） / Sort alphabetically (optional)

## 测试本地化 / Testing Localization

### 手动测试 / Manual Testing

1. 运行应用程序 / Run the application
2. 登录系统 / Log in to the system
3. 访问设置页面 / Navigate to Settings
4. 更改语言为 English
5. 验证所有文本正确显示 / Verify all text displays correctly
6. 切换回简体中文 / Switch back to Simplified Chinese

### 自动化测试建议 / Automated Testing Recommendations

```csharp
[Fact]
public void ResourceKeys_ShouldMatch_BetweenCultures()
{
    var zhKeys = GetResourceKeys("zh-CN");
    var enKeys = GetResourceKeys("en");
    Assert.Equal(zhKeys, enKeys);
}
```

## 故障排除 / Troubleshooting

### 键未找到 / Key Not Found
- **症状 / Symptom**: 页面显示键名而非翻译文本
- **原因 / Cause**: 资源文件中缺少该键
- **解决 / Solution**: 在两个资源文件中添加缺失的键

### 语言未切换 / Language Not Switching
- **症状 / Symptom**: 选择语言后界面未更新
- **原因 / Cause**: 页面未刷新或缓存问题
- **解决 / Solution**: 强制页面重载 (`forceLoad: true`)

### 编译错误 / Build Errors
- **症状 / Symptom**: 编译时提示 Localizer 未注入
- **原因 / Cause**: 缺少必要的引用
- **解决 / Solution**: 添加 `@inject IStringLocalizer<SharedResources> Localizer`

## 资源 / Resources

- [ASP.NET Core Localization](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization)
- [.NET Globalization and Localization](https://docs.microsoft.com/en-us/dotnet/standard/globalization-localization/)
- [Resource File Format (.resx)](https://docs.microsoft.com/en-us/dotnet/framework/resources/creating-resource-files-for-desktop-apps)

## 贡献 / Contributing

添加新翻译或改进现有翻译时，请：
When adding new translations or improving existing ones:

1. 确保翻译准确且符合上下文 / Ensure translations are accurate and contextual
2. 保持术语一致性 / Maintain terminology consistency  
3. 测试两种语言 / Test in both languages
4. 更新本文档（如需要）/ Update this documentation (if needed)

---

最后更新 / Last Updated: 2025-02-02
版本 / Version: 1.0
