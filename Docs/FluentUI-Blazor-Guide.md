# Fluent UI Blazor 开发规范与最佳实践

> 本文档基于 Fluent UI Blazor 官方文档汇总，面向 agent 与开发者，用于快速上手、一致性实现与自动化集成。涵盖安装、注册、布局、主题、组件使用、数据/状态管理、表单与验证、可访问性、性能、测试及工程化建议。

---

## 目录
- 安装与服务注册
- 布局与全局提供器
- 主题与设计代币（Design Tokens）
- 常用组件 使用规范
- 数据与状态管理
- 表单与验证
- 对话框、通知与消息栏
- 国际化（RTL）与可访问性
- 样式与类名管理
- 性能优化
- 测试与质量保证
- 工程化建议与示例

---

## 安装与服务注册

- 添加 NuGet 包：

```powershell
dotnet add package Microsoft.FluentUI.AspNetCore.Components
```

- 在 `Program.cs` 中注册服务：

```csharp
builder.Services.AddHttpClient(); // Blazor Server 需先注册
builder.Services.AddFluentUIComponents();

// 可选配置
builder.Services.AddFluentUIComponents(options =>
{
    options.UseTooltipServiceProvider = true;
    options.ServiceLifetime = ServiceLifetime.Scoped; // Server: Scoped, WASM: Singleton 视场景
    options.HideTooltipOnCursorLeave = false;
    options.ValidateClassNames = true; // 如使用 Tailwind 可设为 false
});
```

- 如果使用 DataGrid 与 EF/OData 适配器：

```csharp
builder.Services.AddDataGridEntityFrameworkAdapter();
builder.Services.AddDataGridODataAdapter();
```

---

## 布局与全局提供器

- 将全局组件提供器放在主 Layout（或 `App.razor`）中，保证整个应用可用。常见提供器：

```razor
<FluentDesignTheme StorageName="my-app-theme" />
<FluentToastProvider />
<FluentDialogProvider />
<FluentTooltipProvider />
<FluentMessageBarProvider />
<FluentMenuProvider />

<div class="page">@Body</div>
```

- 若需要手动加载脚本，可在 `App.razor` 引入（通常库会自动注入）：

```html
<script src="/_content/Microsoft.FluentUI.AspNetCore.Components/Microsoft.FluentUI.AspNetCore.Components.lib.module.js" type="module" async></script>
```

---

## 主题与设计代币（Design Tokens）

- 在 Layout 中使用 `FluentDesignTheme` / `FluentDesignSystemProvider` 管理主题与设计代币：

```razor
<FluentDesignTheme @bind-Mode="themeMode" StorageName="my-app-theme" />
<FluentDesignSystemProvider AccentBaseColor="@accentColor" BaseLayerLuminance="@(isDark?0.15f:1.0f)">
  ...
</FluentDesignSystemProvider>
```

- 常用代币：`AccentBaseColor`（主色）、`BaseLayerLuminance`（亮暗层）、`Direction`（RTL）、`BodyFont`、`TypeRamp` 等。
- 使用 `StorageName` 可持久化用户主题偏好（暗/亮、颜色方案）。
- 动态切换主题时在 `OnLoaded` / `OnAfterRenderAsync` 中同步状态，避免首屏闪烁。

---

## 常用组件 使用规范

- 按语义与可访问性使用组件，不要仅靠样式来传达信息。

- 按钮（`FluentButton`）:
  - 使用 `Appearance`（`Accent/Neutral/Outline/Stealth/Lightweight`）区分优先级。
  - 表单提交使用 `Type="ButtonType.Submit"`，异步提交时用 `Loading` 与 `Disabled` 控制。
  - 用 `IconStart` / `IconEnd` 或 `Slot` 放置图标。

- 输入控件:
  - 使用 `EditForm` + `DataAnnotationsValidator`，并使用 `FluentValidationMessage` 显示字段错误。
  - 对于复杂校验可结合 `EditContext` 或 FluentValidation 等库。

- 数据表格（`FluentDataGrid`）:
  - 大数据优先使用 `ItemsProvider` + `Virtualize=true`。
  - 支持直接绑定 `IQueryable`（EF/OData 适配器），服务端进行排序/过滤/分页。
  - 列模板与 `PropertyColumn` 用于自定义渲染与格式化。

- 对话框（`DialogService`）:
  - 使用 `IDialogService.ShowDialogAsync<T>()` 传入 `DialogParameters`。
  - 使用 `TrapFocus=true`、`Modal=true`、`PreventScroll=true` 等提高可访问性。

- 通知/Toast:
  - 注入 `IToastService` 并使用 `ShowSuccess/Warning/Error/Info/Progress`；支持 timeout、topAction 回调。

---

## 数据 与 状态管理

- 推荐把数据获取逻辑放在服务中并注入组件（`HttpClient`/自定义服务）。
- 对于分页/排序/筛选，优先在 API 层完成，组件仅请求所需页数据。
- 在 `FluentDataGrid` 中使用 `ItemsProvider` 返回 `GridItemsProviderResult`，抽象网络层与缓存。

示例 ItemsProvider（伪代码）:

```csharp
dataProvider = async request =>
{
    var response = await Http.GetFromJsonAsync<ApiResponse>($"api/items?skip={request.StartIndex}&take={request.Count}");
    return GridItemsProviderResult.From(response.Items, response.TotalCount);
};
```

---

## 表单 与 验证

- 使用 `EditForm` + `DataAnnotationsValidator` 或 `EditContext` 实现表单验证与提交流。
- 使用 `FluentValidationMessage` 在字段下方显示错误信息。
- 提交步骤：禁用提交按钮、显示 Loading、await 后恢复并展示 `FluentMessageBar` 或 Toast。

表单示例（关键片段）:

```razor
<EditForm Model="@model" OnValidSubmit="@HandleValidSubmit">
  <DataAnnotationsValidator />
  <FluentTextField @bind-Value="model.Email" Label="Email" Required="true" />
  <FluentValidationMessage For="@(()=>model.Email)" />
  <FluentButton Type="ButtonType.Submit" Loading="@isSubmitting">提交</FluentButton>
</EditForm>
```

---

## 对话框、通知与消息栏

- 对话框：优先使用服务化调用，处理返回结果并根据结果更新状态或展示错误。
- Toast：用于短时反馈；Progress Toast 用于展示长期任务进度。
- MessageBar：用于在页面内展示持久提示（成功、警告、错误）。

示例：

```csharp
ToastService.ShowSuccess("已保存", timeout:5000);
```

---

## 国际化（RTL）与可访问性

- RTL: 在 Layout 中使用 `Direction` 设计代币并设置 `LocalizationDirection.RightToLeft`，确保文本/图标/布局正确反转。
- 焦点管理：Modal 对话框设置 `TrapFocus` 并保证可见焦点样式。
- 语义标签与 aria：使用组件的 `Label`、`Title`、`Intent` 等属性，必要时补充 `aria-*`。

可访问性检查要点：键盘可达性、可见焦点、语义化控件、颜色对比度、屏幕阅读器顺序。

---

## 样式 与 类名管理

- 优先通过设计代币调整主题与颜色，避免大量覆盖组件内部样式。
- 当使用 Tailwind 等工具时，可在 `Program.cs` 中 `options.ValidateClassNames = false`。
- 对于需要全局 CSS 的小改动，集中到 `wwwroot/css` 中并命名空间化，避免命名冲突。

---

## 性能优化

- 启用虚拟化（长列表/表格）并设置合适的 `ItemSize`。
- 把排序/筛选/分页下推到后端（API/DB），避免传输全部数据。
- 拆分大组件为小子组件，使用 `@key` 控制列表重用，避免在渲染路径分配新对象或匿名委托。

---

## 测试 与质量保证

- 使用 bUnit 编写组件渲染测试并使用 `.verified.html` 做回归对比：

```csharp
using var ctx = new Bunit.TestContext();
var cut = ctx.RenderComponent<MyButton>(parameters => parameters.Add(p=>p.Width,"100px"));
cut.MarkupMatches("<fluent-button appearance=\"neutral\" style=\"width: 100px;\" />");
```

- 为 `ItemsProvider` 编写集成测试，模拟 HTTP 返回、分页与排序行为。

---

## 工程化 建议（agent 自动化友好）

- 初始化脚本（可在 CI 或模板中执行）：
  - `dotnet add package Microsoft.FluentUI.AspNetCore.Components`
  - 在 `Program.cs` 自动注入 `AddFluentUIComponents()` 与必要适配器
  - 在主 Layout 插入提供器片段

- 示例页面集合：在仓库维护一组小型 Demo 页面（`/docs/demo/buttons`, `/docs/demo/forms`, `/docs/demo/datagrid`），用于手动与自动化测试。
- 主题服务：把主题管理逻辑封装成 `ThemeService` 以便在 agent 中调用或以 UI 控制自动化脚本更新配色/模式。

---

## 快速示例片段

- 注册服务（`Program.cs`）:

```csharp
builder.Services.AddHttpClient();
builder.Services.AddFluentUIComponents();
builder.Services.AddDataGridEntityFrameworkAdapter();
```

- Layout 提供器（`MainLayout.razor`）:

```razor
<FluentDesignTheme StorageName="app-theme" />
<FluentToastProvider />
<FluentDialogProvider />
<FluentTooltipProvider />
<FluentMessageBarProvider />

<div class="page">@Body</div>
```

---

## 后续建议
- 是否需要我：
  - 将该文档提交到仓库并创建 PR？
  - 自动在 `MainLayout.razor` 中注入推荐 provider 并提交更改？
  - 生成 demo 页面与 bUnit 测试样例？

---

版本: 1.0  
来源: 汇总自 Microsoft 官方 `fluentui-blazor` 仓库文档与示例（摘录并重写为实践指南）。
