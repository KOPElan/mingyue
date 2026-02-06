### Install Fluent UI Blazor .NET Templates

Source: https://www.fluentui-blazor.net/CodeSetup

Command to install the Fluent UI Blazor .NET templates, enabling the use of pre-configured project templates.

```bash
dotnet new install Microsoft.FluentUI.AspNetCore.Templates
```

--------------------------------

### Quick Start: Fluent UI Components in Blazor

Source: https://www.fluentui-blazor.net/CodeSetup

Example of using Fluent UI components like FluentCard and FluentButton in a Blazor view.

```razor
<FluentCard>
  <h2>Hello World!</h2>
  <FluentButton Appearance="@Appearance.Accent">Click Me</FluentButton>
</FluentCard>
```

--------------------------------

### Install Fluent UI Blazor EF Core DataGrid Adapter (dotnet CLI)

Source: https://www.fluentui-blazor.net/CodeSetup

Command to install the necessary NuGet package for integrating the FluentDataGrid component with Entity Framework Core in a Blazor application.

```bash
dotnet add package Microsoft.FluentUI.AspNetCore.Components.DataGrid.EntityFrameworkAdapter
```

--------------------------------

### Simple Fluent UI Blazor Card and Button Example

Source: https://www.fluentui-blazor.net/index

Example demonstrating the usage of FluentCard and FluentButton components from the Fluent UI Blazor library. It shows how to set component properties like Width, Height, and Appearance.

```razor
@using Microsoft.FluentUI.AspNetCore.Components.

<FluentCard Width="350px" Height="250px">
    <h2>Hello World!</h2>
    <FluentButton Appearance="@Appearance.Accent">Click Me</FluentButton>
</FluentCard>
            Copy
```

--------------------------------

### Add Fluent UI Icons NuGet Package

Source: https://www.fluentui-blazor.net/ProjectSetup

Installs the Microsoft.Fast.Components.FluentUI.Icons NuGet package into your project. This package provides access to Fluent UI System Icons.

```bash
dotnet add package Microsoft.Fast.Components.FluentUI.Icons

```

--------------------------------

### Include Fluent UI Blazor Stylesheet

Source: https://www.fluentui-blazor.net/CodeSetup

Example of how to include Fluent UI Blazor styles in the project's HTML or Razor files. Replace {PROJECT_NAME} with your project's root namespace or assembly name.

```html
<link href="{PROJECT_NAME}.styles.css" rel="stylesheet" />
```

--------------------------------

### Transparent Fluent UI Blazor Overlay Example

Source: https://www.fluentui-blazor.net/Overlay

Illustrates how to create a transparent overlay. This example focuses on the basic functionality of showing and hiding a transparent overlay with a progress ring, controlled by a button click.

```Razor
<FluentButton Appearance="Appearance.Accent" @onclick="() => visible = !visible">Show Overlay</FluentButton>

<FluentOverlay @bind-Visible=@visible OnClose="HandleOnClose">
    <FluentProgressRing />
</FluentOverlay>

@code {
    bool visible = false;

    protected void HandleOnClose()
    {
        DemoLogger.WriteLine("Transparent overlay closed");
    }
}
```

--------------------------------

### Install Fluent UI Blazor NuGet Package (CLI)

Source: https://www.fluentui-blazor.net/index

Command to install the main Microsoft.FluentUI.AspNetCore.Components NuGet package using the .NET CLI. This is a prerequisite for using the Fluent UI Blazor components in a project.

```bash
dotnet add package Microsoft.FluentUI.AspNetCore.ComponentsCopy
```

--------------------------------

### Create a new Fluent UI Blazor Project

Source: https://www.fluentui-blazor.net/CodeSetup

Command to create a new standard Blazor project with Fluent UI components pre-configured.

```bash
dotnet new fluentblazor --name MyApplication
```

--------------------------------

### FluentAnchor Examples with Icons (Razor)

Source: https://www.fluentui-blazor.net/Anchor

Demonstrates various ways to use the FluentAnchor component in Razor, including different appearances (neutral and hypertext) and icon placements (start, end, and within content). It shows how to define icons using the IconStart/IconEnd parameters or by embedding FluentIcon components.

```razor
<h4>Neutral appearance</h4>
<div style="display: flex; align-items: center; gap: 10px; margin-bottom: 1em;">
    <FluentAnchor Href="#" IconStart="@(new Icons.Regular.Size16.Globe())">
        Icon at start
    </FluentAnchor>

    <FluentAnchor Href="#" IconEnd="@(new Icons.Regular.Size16.Globe())">
        Icon at end
    </FluentAnchor>

    <FluentAnchor Href="#">
        <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Success" Slot="start" />
        Icon at start in content
    </FluentAnchor>

    <FluentAnchor Href="#">
        Icon at end in content
        <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Success" Slot="end" />
    </FluentAnchor>
</div>
<h4>Hypertext appearance</h4>
<p>
    By default, the margin between the icon and the link is equal to the margin that is used for the other
    appearances. If you find that margin to large, you can add the following to your CSS:<br/><br />
    <CodeSnippet>
        /* for icon at the start */
        fluent-anchor[appearance="hypertext"]::part(start) {
            margin-inline-end: calc(var(--design-unit) * 1px);
        }
        /* for icon at the end */
        fluent-anchor[appearance="hypertext"]::part(end) {
        margin-inline-start: calc(var(--design-unit) * 1px);
        }
    </CodeSnippet>
    <br />
    <em>Do not use these styles if there is no icon being displayed with a hypertext. It will cause the hypertext to get a margin at the start/end.</em>
</p>
<div style="display: flex; align-items: center; gap: 10px; margin-bottom: 1em;">
    <FluentAnchor Appearance="@Appearance.Hypertext" Href="#" IconStart="@(new Icons.Regular.Size16.Globe())">
        Icon at start
    </FluentAnchor>

    <FluentAnchor Appearance="@Appearance.Hypertext" Href="#" IconEnd="@(new Icons.Regular.Size16.Globe())">
        Icon at end
    </FluentAnchor>

    <FluentAnchor Appearance="@Appearance.Hypertext" Href="#">
        <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Success" Slot="start" />
        Icon at start in content
    </FluentAnchor>

    <FluentAnchor Appearance="@Appearance.Hypertext" Href="#">
        Icon at start in content
        <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Success" Slot="end" />
    </FluentAnchor>
</div>

<div style="display: flex; align-items: center; gap: 10px; margin-top: 1em;">
    With icon in default slot:
        <FluentAnchor Href="#" aria-label="test">
            <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" />
        </FluentAnchor>

</div>
```

--------------------------------

### Install OData DataGrid Adapter (CLI)

Source: https://www.fluentui-blazor.net/datagrid

Command to install the NuGet package required for integrating the FluentDataGrid component with OData data sources. This package facilitates efficient asynchronous query resolution for OData feeds.

```bash
dotnet add package Microsoft.FluentUI.AspNetCore.Components.DataGrid.ODataAdapter
```

--------------------------------

### Create a new Fluent UI Blazor WASM Project

Source: https://www.fluentui-blazor.net/CodeSetup

Command to create a new Blazor WebAssembly standalone project with Fluent UI components pre-configured.

```bash
dotnet new fluentblazorwasm --name MyApplication
```

--------------------------------

### Add Fluent UI Emojis NuGet Package

Source: https://www.fluentui-blazor.net/ProjectSetup

Installs the Microsoft.Fast.Components.FluentUI.Emojis NuGet package into your project. This package is required to use Fluent UI Emojis.

```bash
dotnet add package Microsoft.Fast.Components.FluentUI.Emojis

```

--------------------------------

### Add Fluent UI Blazor NuGet Package

Source: https://www.fluentui-blazor.net/CodeSetup

Command to add the main Fluent UI Blazor component NuGet package to an existing project.

```bash
dotnet add package Microsoft.FluentUI.AspNetCore.Components
```

--------------------------------

### FluentAppBar Component API

Source: https://www.fluentui-blazor.net/AppBar

This section details the properties, event callbacks, and methods available for the FluentAppBar component.

```APIDOC
## FluentAppBar Class

### Description
Represents the main application bar component, providing a container for navigation and actions.

### Parameters
#### Path Parameters
None

#### Query Parameters
None

#### Request Body
None

### Parameters
- **AppsOverflow** (IEnumerable<IAppBarItem>) - Gets all app items with `IAppBarItem.Overflow` assigned to True.
- **ChildContent** (RenderFragment?) - Gets or sets the content to display (the app bar items, `FluentAppBarItem`).
- **Items** (IEnumerable<IAppBarItem>) - Gets or sets the collections of app bar items. Use either this or ChildContent to define the content of the app bar.
- **Orientation** (Orientation) - Defaults to Vertical. Gets or sets the `Orientation` of the app bar.
- **PopoverShowSearch** (bool) - Defaults to True. Gets or sets if the popover shows the search box.

### EventCallbacks
- **PopoverVisibilityChanged** (EventCallback<bool>) - Event to be called when the visibility of the popover changes.

### Methods
#### OverflowRaisedAsync
- **Parameters**: string value
- **Type**: Task
- **Description**: Handles overflow events.

### Request Example
```razor
<FluentStack Orientation="Orientation.Vertical" Style="height: 330px;">
    <FluentAppBar Style="height: 100%;">
        <FluentAppBarItem Href="/" Text="Home" />
        <FluentAppBarItem Href="/AppBar" Text="AppBar" />
    </FluentAppBar>
</FluentStack>
```

### Response
#### Success Response (200)
Not applicable for component documentation.

#### Response Example
Not applicable for component documentation.
```

--------------------------------

### Full Screen Fluent UI Blazor Overlay Example

Source: https://www.fluentui-blazor.net/Overlay

Provides an example of a full-screen overlay that covers the entire viewport. It includes options for background color, opacity, and prevents scrolling while visible. The overlay contains a progress ring and is controlled by a button.

```Razor
<FluentButton Appearance="Appearance.Accent" @onclick="() => visible = !visible">Show Overlay</FluentButton>

<FluentOverlay @bind-Visible=@visible Opacity="0.4" BackgroundColor="#e8f48c" FullScreen="true" OnClose="HandleOnClose" PreventScroll=true>
    <FluentProgressRing />
</FluentOverlay>

@code {
    bool visible = false;

    protected void HandleOnClose()
    {
        DemoLogger.WriteLine("Full screen overlay closed");
    }
}
```

--------------------------------

### Include Fluent UI Blazor Reboot Stylesheet

Source: https://www.fluentui-blazor.net/CodeSetup

Manual inclusion of the Reboot stylesheet for Fluent UI Blazor components, providing a consistent baseline for site development.

```html
<link href="_content/Microsoft.FluentUI.AspNetCore.Components/css/reboot.css" rel="stylesheet" />
```

--------------------------------

### Fluent UI Blazor Breadcrumb Examples

Source: https://www.fluentui-blazor.net/Breadcrumb

Illustrates how to construct a complete breadcrumb navigation using the Fluent UI Blazor component. Examples show a default breadcrumb, one with custom separators between items, and another utilizing start icons for each item.

```Razor
<FluentBreadcrumb>
    <FluentBreadcrumbItem Href="#">
        Breadcrumb item 1
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem Href="#">
        Breadcrumb item 2
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem>Breadcrumb item 3</FluentBreadcrumbItem>
</FluentBreadcrumb>
```

```Razor
<FluentBreadcrumb>
    <FluentBreadcrumbItem Href="#">
        Breadcrumb item 1
        <FluentIcon Value="@(new Icons.Regular.Size20.ChevronDoubleRight())" Color="@Color.Neutral" Slot="separator" />
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem Href="#">
        Breadcrumb item 2
        <FluentIcon Value="@(new Icons.Regular.Size20.ChevronDoubleRight())" Color="@Color.Neutral" Slot="separator" />
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem>Breadcrumb item 3</FluentBreadcrumbItem>
</FluentBreadcrumb>
```

```Razor
<FluentBreadcrumb>
    <FluentBreadcrumbItem Href="#">
         Breadcrumb item 1
        <FluentIcon Value="@(new Icons.Regular.Size16.Home())" Color="@Color.Neutral" Slot="start" />
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem Href="#">
        Breadcrumb item 2
        <FluentIcon Value="@(new Icons.Regular.Size16.Clipboard())" Color="@Color.Neutral" Slot="start" />
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem>
        Breadcrumb item 3
        <FluentIcon Value="@(new Icons.Regular.Size16.Money())" Color="@Color.Neutral" Slot="start" />
    </FluentBreadcrumbItem>
</FluentBreadcrumb>
```

--------------------------------

### Register Fluent UI Services

Source: https://www.fluentui-blazor.net/CodeSetup

Register the necessary Fluent UI services in the Program.cs file for your Blazor application.

```csharp
builder.Services.AddFluentUIComponents();
```

--------------------------------

### FluentIcon Examples with Various Properties and Components

Source: https://www.fluentui-blazor.net/Icon

This comprehensive example showcases the versatility of the `<FluentIcon>` component within a Blazor `<FluentStack>`. It demonstrates rendering different icon sizes and styles, applying custom colors (e.g., `Color.Error`, `Color.Success`, or a named color like 'red'), displaying an icon from a URL, and converting icon objects to markup strings. It also shows how to integrate icons into `<FluentButton>` components using `IconStart` and `IconEnd` slots, with and without color modifications.

```razor
<FluentStack VerticalAlignment="VerticalAlignment.Center">
    <FluentIcon Value="@(new Icons.Regular.Size24.Save())" Title="Save" />
    <FluentIcon Value="@(new Icons.Regular.Size24.Open())" Title="Open" Color="Color.Error" />
    <FluentIcon Value="@(new Icons.Regular.Size24.Album())" />

    <FluentIcon Value="@Icon.FromImageUrl("_content/FluentUI.Demo.Shared/images/BlazorLogo.png")" Width="24px"/>

    @(new Icons.Regular.Size20.Add().ToMarkup())
    @(new Icons.Regular.Size20.Airplane().ToMarkup("16px", "blue"))

    <FluentButton IconStart="@(new Icons.Regular.Size24.ArrowCircleLeft())">
        Back
    </FluentButton>

    <FluentButton IconEnd="@(new Icons.Regular.Size24.ArrowCircleRight().WithColor(Color.Success))">
        Next
    </FluentButton>

    <FluentButton IconEnd="@(new Icons.Regular.Size24.DismissCircle().WithColor("red"))">
        Exit
    </FluentButton>

    <FluentButton Appearance="Appearance.Accent"
                  IconEnd="@(new Icons.Regular.Size24.Alert().WithColor(Color.Fill))">
        Next
    </FluentButton>

</FluentStack>
```

--------------------------------

### Add Fluent UI Blazor Icons and Emoji Packages

Source: https://www.fluentui-blazor.net/CodeSetup

Commands to add optional NuGet packages for Fluent UI Blazor icons and emoji support.

```bash
dotnet add package Microsoft.FluentUI.AspNetCore.Components.Icons
dotnet add package Microsoft.FluentUI.AspNetCore.Components.Emoji
```

--------------------------------

### Customized FluentProfileMenu in Razor

Source: https://www.fluentui-blazor.net/ProfileMenu

Illustrates a customized FluentProfileMenu component in Razor, allowing for custom content within the start, header, body, and footer sections. This example highlights advanced styling and content projection capabilities. It depends on the FluentUI Blazor library.

```Razor
<FluentStack HorizontalAlignment="@HorizontalAlignment.End"
             VerticalAlignment="@VerticalAlignment.Center"
             Style="height: 48px; background: var(--neutral-layer-4); padding-inline-end: 10px; ">
    <FluentProfileMenu Initials="BG"
                       Style="--fluent-profile-menu-hover: var(--neutral-stroke-focus); padding: 4px;">
        <StartTemplate>
            Bill Gates
        </StartTemplate>
        <HeaderTemplate>
            <FluentLabel Typo="@Typography.Header">Login</FluentLabel>
        </HeaderTemplate>
        <ChildContent>
            <div style="width: 250px; height: 80px">
                <FluentLabel Typo="@Typography.Header" Style="font-weight: bold;">Bill Gates</FluentLabel>
                <FluentLabel>bill.gates@microsoft.com</FluentLabel>
            </div>
        </ChildContent>
        <FooterTemplate>
            <FluentStack>
                <FluentSpacer />
                <FluentAnchor Appearance="@Appearance.Hypertext"
                              Href="https://microsoft.com"
                              Target="_blank">About</FluentAnchor>
            </FluentStack>
        </FooterTemplate>
    </FluentProfileMenu>
</FluentStack>
```

--------------------------------

### Install Fluent UI Icons NuGet Package

Source: https://www.fluentui-blazor.net/IconsAndEmoji

Installs the necessary NuGet package for using Fluent UI System Icons in your Blazor project. This is a prerequisite for utilizing the `FluentIcon` component with system icons.

```bash
dotnet add package Microsoft.FluentUI.AspNetCore.Components.Icons
```

--------------------------------

### FluentLayout Component Example in Razor

Source: https://www.fluentui-blazor.net/Layout

Demonstrates the usage of the FluentLayout component to structure a typical web page, including optional header, navigation menu, body content, and footer. It utilizes FluentSwitch components to control the visibility of each layout section. This example requires FluentUI Blazor libraries.

```Razor
<FluentSwitch Style="width: 150px;" @bind-Value="ShowHeader">Header</FluentSwitch>
<FluentSwitch Style="width: 150px;" @bind-Value="ShowNavMenu">NavMenu</FluentSwitch>
<FluentSwitch Style="width: 150px;" @bind-Value="ShowBodyContent">BodyContent</FluentSwitch>
<FluentSwitch Style="width: 150px;" @bind-Value="ShowFooter">Footer</FluentSwitch>

<FluentLayout Style="margin-top: 10px;">
    @if (ShowHeader)
    {
        <FluentHeader>
            Some Header Text
            <FluentSpacer />
            Aligned to the end
        </FluentHeader>
    }
    
    <FluentStack Orientation="Orientation.Horizontal" Width="100%">
        @if (ShowNavMenu)
        {
            <FluentNavMenu Width="250">
                <FluentNavLink Icon="@(new Icons.Regular.Size24.Home())">Item 1</FluentNavLink>
                <FluentNavLink Icon="@(new Icons.Regular.Size24.Cloud())">Item 2</FluentNavLink>
                <FluentNavGroup Title="Item 3">
                    <FluentNavLink Icon="@(new Icons.Regular.Size24.LeafOne())">Item 3.1</FluentNavLink>
                    <FluentNavLink Icon="@(new Icons.Regular.Size24.LeafTwo())">Item 3.2</FluentNavLink>
                </FluentNavGroup>
                <FluentNavLink Icon="@(new Icons.Regular.Size24.CalendarAgenda())" Disabled="true">Item 4</FluentNavLink>
            </FluentNavMenu>
        }

        @if (ShowBodyContent)
        {
            <FluentBodyContent>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lorem lacus.
                Ut id leo non enim feugiat ultrices. Proin vulputate volutpat urna nec iaculis.
                Integer dui lacus, fermentum sit amet aliquet in, scelerisque vitae dui.
                Nulla fringilla sagittis orci eu consectetur. Fusce eget dolor non lectus placerat
                tincidunt. Pellentesque aliquam non odio ac porttitor. Nam finibus mattis sagittis.
                Ut hendrerit porttitor massa in aliquam. Duis laoreet fringilla feugiat.
                Sed maximus, urna in fringilla posuere, enim urna bibendum justo, vel molestie nibh orci nec lectus.
                Etiam a varius justo. Aenean nisl ante, interdum eget vulputate eget, iaculis ut massa.
                Suspendisse maximus sed purus id molestie. Lorem ipsum dolor sit amet, consectetur adipiscing elit.
            </FluentBodyContent>
        }
    </FluentStack>

    @if (ShowFooter)
    {
        <FluentFooter Style="@('height: 40px; color: white; text-align: center;')">
            Footer Text
        </FluentFooter>
    }
</FluentLayout>

@code {
    bool ShowHeader = true;
    bool ShowNavMenu = true;
    bool ShowBodyContent = true;
    bool ShowFooter = true;
}
```

--------------------------------

### FluentInputFile Component Documentation

Source: https://www.fluentui-blazor.net/InputFile

This section details the FluentInputFile component, its parameters, event callbacks, and methods, along with an example of a disabled component.

```APIDOC
## FluentInputFile Component

### Description
The `FluentInputFile` component is used for handling file uploads in Blazor applications. It supports various configurations for file selection, size limits, drag-and-drop, and progress tracking.

### Parameters

#### Path Parameters
None

#### Query Parameters
None

#### Request Body
None

### Request Example

```razor
<FluentInputFile Id="my-file-uploader-disabled"
                 Disabled="true"
                 Style="height: 300px; border: 1px dashed var(--accent-fill-rest);">
    <ChildContent>
        <label for="my-file-uploader">
            <FluentIcon Value="@(new Icons.Regular.Size24.CircleLine())" />
        </label>

        <div>
            This component is disabled. You cannot drag files here,
            or <label for="my-file-uploader-disabled">browse</label>
            for them.
            <br />
        </div>

    </ChildContent>
</FluentInputFile>

@code
{

}
```

### Response

#### Success Response (200)
N/A

#### Response Example
N/A

### FluentInputFile Class Details

#### Parameters

*   **Accept** (string) - Gets or sets the filter for what file types the user can pick from the file input dialog box. Example: '.gif, .jpg, .png, .doc', 'audio/*', 'video/*', 'image/*'. See https://developer.mozilla.org/en-US/docs/Web/HTML/Attributes/accept for more information.
*   **AnchorId** (string) - Gets or sets the identifier of the source component clickable by the end user.
*   **BufferSize** (uint) - Gets or sets the size of buffer to read bytes from uploaded file (in bytes). Default value is 10 KiB.
*   **ChildContent** (RenderFragment?) - Gets or sets the child content of the component.
*   **Disabled** (bool) - Disables the form control, ensuring it doesn't participate in form submission. Default value is False.
*   **DragDropZoneVisible** (bool) - Gets or sets a value indicating whether the Drag/Drop zone is visible. Default is True.
*   **MaximumFileCount** (int) - To select multiple files, set the maximum number of files allowed to be uploaded. Default value is 10.
*   **MaximumFileSize** (long) - Gets or sets the maximum size of a file to be uploaded (in bytes). Default value is 10 MiB.
*   **Mode** (InputFileMode) - Gets or sets the type of file reading. For SaveToTemporaryFolder, use `FluentInputFileEventArgs.LocalFile` to retrieve the file. For Buffer, use `FluentInputFileEventArgs.Buffer` to retrieve bytes. For Stream, use `FluentInputFileEventArgs.Stream` to have full control over retrieving the file.
*   **Multiple** (bool) - To enable multiple file selection and upload, set the Multiple property to true. Set `FluentInputFile.MaximumFileCount` to change the number of allowed files. Default value is False.
*   **ProgressPercent** (int) - Gets or sets the current global value of the percentage of a current upload. Default value is 0.
*   **ProgressTemplate** (RenderFragment<ProgressFileDetails>?) - Gets or sets the progress content of the component.
*   **ProgressTitle** (string) - Gets the current label display when an upload is in progress.

#### Event Callbacks

*   **OnCompleted** (EventCallback<IEnumerable<FluentInputFileEventArgs>>) - Raised when all files are completely uploaded.
*   **OnFileCountExceeded** (EventCallback<int>) - Raised when the `FluentInputFile.MaximumFileCount` is exceeded. The return parameter specifies the total number of files that were attempted for upload.
*   **OnFileError** (EventCallback<FluentInputFileEventArgs>) - Raised when a file raised an error. Not yet used.
*   **OnFileUploaded** (EventCallback<FluentInputFileEventArgs>) - Raised when a file is completely uploaded.
*   **OnInputFileChange** (EventCallback<InputFileChangeEventArgs>) - Use the native event raised by the InputFile component. If you use this event, the `FluentInputFile.OnFileUploaded` will never be raised.
*   **OnProgressChange** (EventCallback<FluentInputFileEventArgs>) - Raised when a progression step is updated. You can use `FluentInputFile.ProgressPercent` and `FluentInputFile.ProgressTitle` to have more detail on the progression.
*   **ProgressPercentChanged** (EventCallback<int>) - Gets or sets a callback that updates the `FluentInputFile.ProgressPercent`.

#### Methods

*   **DisposeAsync** () - Returns `ValueTask`.
*   **ShowFilesDialogAsync** () - Opens the dialog-box to select files. Use `FluentInputFile.AnchorId` instead to specify the ID of the button (for example) on which the user should click. ⚠️ This method doesn't work on Safari and iOS.

### Known Issues
Starting with .NET 6, the `InputFile` component does not work in Server-Side Blazor applications using Autofac IoC containers. This issue is being tracked here: aspnetcore#38842. Enable HubOptions DisableImplicitFromServicesParameters in program/startup to workaround this issue.

```csharp
builder.Services
    .AddServerSideBlazor()
    .AddHubOptions(opt =>
    {
        opt.DisableImplicitFromServicesParameters = true;
    });
```
```

--------------------------------

### FluentAnchoredRegion: Start & End Positioning (Razor)

Source: https://www.fluentui-blazor.net/AnchoredRegion

Shows how to position multiple FluentAnchoredRegions relative to a single anchor, demonstrating `HorizontalPosition.Start` and `HorizontalPosition.End`. This example uses `AxisPositioningMode.Locktodefault` and `AxisScalingMode.Content`.

```Razor
<div id="viewport-se" style="position:relative;height:400px;width:400px;background: var(--neutral-layer-4);overflow:auto;resize:both;">
    <FluentButton Appearance=Appearance.Neutral id="anchor-se" style="margin-left:100px;margin-top:100px">anchor</FluentButton>
    <FluentAnchoredRegion Anchor="anchor-se" Viewport="viewport-se"
                          HorizontalPositioningMode="AxisPositioningMode.Locktodefault"
                          HorizontalDefaultPosition="HorizontalPosition.Start"
                          HorizontalScaling="AxisScalingMode.Content"
                          VerticalPositioningMode="AxisPositioningMode.Uncontrolled"
                          VerticalDefaultPosition="VerticalPosition.Unset"
                          VerticalScaling="AxisScalingMode.Content"
                          AutoUpdateMode="AutoUpdateMode.Anchor">
        <div style="height:100px;width:100px;background:var(--highlight-bg);" />
    </FluentAnchoredRegion>
    <FluentAnchoredRegion Anchor="anchor-se" Viewport="viewport-se"
                          HorizontalPositioningMode="AxisPositioningMode.Locktodefault"
                          HorizontalDefaultPosition="HorizontalPosition.End"
                          HorizontalScaling="AxisScalingMode.Content"
                          VerticalPositioningMode="AxisPositioningMode.Uncontrolled"
                          VerticalDefaultPosition="VerticalPosition.Unset"
                          VerticalScaling="AxisScalingMode.Content"
                          AutoUpdateMode="AutoUpdateMode.Anchor">
        <div style="height:100px;width:100px;background:var(--neutral-layer-2);" />
    </FluentAnchoredRegion>
</div>
```

--------------------------------

### FluentAppBar with OnClick Event Handling in Razor

Source: https://www.fluentui-blazor.net/AppBar

This example demonstrates how to create a FluentAppBar with multiple FluentAppBarItems. Each item can have an Href for navigation or an OnClick event handler. The code defines static methods for generating icons and event handlers for logging clicks or showing dialogs. It requires the `FluentUI.AspNetCore.Components` package and `IDialogService` for showing dialogs.

```razor
@inject IDialogService DialogService

<FluentStack Orientation="Orientation.Vertical" Style="height: 330px;">
    <FluentAppBar Style="height: 100%;">
        <FluentAppBarItem Href="/"
                          Match="NavLinkMatch.All"
                          IconRest="HomeIcon()"
                          IconActive="HomeIcon(active: true)"
                          Text="Home"
                          OnClick="HandleOnClick" />
        <FluentAppBarItem Href="/AppBar"
                          IconRest="AppBarIcon()"
                          IconActive="AppBarIcon(active: true)"
                          Text="AppBar"
                          OnClick="HandleOnClick" />
        <FluentAppBarItem IconRest="WhatsNewIcon()"
                          IconActive="WhatsNewIcon(active: true)"
                          Text="What's New"
                          OnClick="ShowSuccessAsync" />
        <FluentAppBarItem Href="@(null)"
                          IconRest="IconsIcon()"
                          IconActive="IconsIcon(active: true)"
                          Text="Icons"
                          OnClick="ShowWarningAsync" />
        <FluentAppBarItem Href="/Dialog"
                          IconRest="DialogIcon()"
                          IconActive="DialogIcon(active: true)"
                          Text="Dialog"
                          OnClick="HandleOnClick" />
    </FluentAppBar>
</FluentStack>

@code {
    private static Icon HomeIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.Home()
               : new Icons.Regular.Size24.Home();

    private static Icon AppBarIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.AppsList()
               : new Icons.Regular.Size24.AppsList();

    private static Icon WhatsNewIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.Info()
               : new Icons.Regular.Size24.Info();

    private static Icon IconsIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.Symbols()
               : new Icons.Regular.Size24.Symbols();

    private static Icon DialogIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.AppGeneric()
               : new Icons.Regular.Size24.AppGeneric();


    private void HandleOnClick(IAppBarItem item)
    {

        DemoLogger.WriteLine($"Clicked {item.Text}!");
    }

    private async Task ShowSuccessAsync(IAppBarItem item)
    {
        var dialog = await DialogService.ShowSuccessAsync($"You clicked {item.Text}");
        var result = await dialog.Result;
    }

    private async Task ShowWarningAsync(IAppBarItem item)
    {
        var dialog = await DialogService.ShowWarningAsync($"Are you sure? {item.Text}");
        var result = await dialog.Result;
    }
}

```

--------------------------------

### HTML Example for FluentDesignTheme Web Component

Source: https://www.fluentui-blazor.net/DesignTheme

Examples of using the fluent-design-theme web component directly in HTML to set the mode (dark/light) and primary color before Blazor initializes. Supports predefined color names or hex codes.

```html
<fluent-design-theme mode="dark" primary-color="word" />
<fluent-design-theme mode="light" primary-color="#ff0000" />
```

--------------------------------

### FluentSearch: FocusAsync Example

Source: https://www.fluentui-blazor.net/Search

Demonstrates how to programmatically focus the FluentSearch component using the FocusAsync method. This is useful for guiding user interaction after a specific event, like a button click. It requires a reference to the FluentSearch component. The output is the FluentSearch component being focused.

```Razor
<h4>Autofocus</h4>
<p>Commented out to prevent page actually jumping to this location. See example code below for implementation.</p>
@*<FluentSearch Autofocus="true">autofocus</FluentSearch>*@

<h4>Focus Async</h4>
<div style="display:flex">
    <FluentButton @onclick="() => focusTest!.FocusAsync()">FocusAsync</FluentButton>
    <FluentSearch @ref=focusTest @bind-Value=value></FluentSearch>
</div>

@code {
    FluentSearch? focusTest;
    string? value;
}
```

--------------------------------

### Fluent UI Blazor MessageBar Intents Example

Source: https://www.fluentui-blazor.net/MessageBar

This example demonstrates various MessageBar intents and configurations without using the `MessageBarService`. It showcases different `MessageIntent` values (Info, Success, Warning, Error, Custom) and how to set a custom icon and color. It also shows how to create a non-dismissible message bar.

```Razor
<FluentStack Orientation="@Orientation.Vertical" VerticalGap="8">
    <FluentMessageBar Title="Descriptive title">
        Message providing information to the user with actionable insights.
    </FluentMessageBar>

    <FluentMessageBar Title="Descriptive title" Intent="@MessageIntent.Success">
        Message providing information to the user with actionable insights.
    </FluentMessageBar>

    <FluentMessageBar Title="Descriptive title" Intent="@MessageIntent.Warning">
        Message providing information to the user with actionable insights.
    </FluentMessageBar>

    <FluentMessageBar Title="Descriptive title" Intent="@MessageIntent.Error">
        Message providing information to the user with actionable insights.
    </FluentMessageBar>
    
    <FluentMessageBar Title="Descriptive title" Intent="@MessageIntent.Custom" Icon="@(new Icons.Filled.Size20.LeafTwo())" IconColor="Color.Neutral">
        Message providing information to the user with actionable insights.
    </FluentMessageBar>

    <FluentMessageBar Title="Descriptive title" Intent="@MessageIntent.Error" AllowDismiss="false">
        Message that is not dismissible.
    </FluentMessageBar>
</FluentStack>

```

--------------------------------

### Register EF Core DataGrid Adapter Service (C#)

Source: https://www.fluentui-blazor.net/CodeSetup

C# code snippet demonstrating how to register the Entity Framework Core DataGrid adapter service in the `Program.cs` file of a Blazor application after initializing Fluent UI components.

```csharp
builder.Services.AddDataGridEntityFrameworkAdapter();
```

--------------------------------

### Add Fluent UI Component Providers

Source: https://www.fluentui-blazor.net/CodeSetup

Add component providers for Toasts, Dialogs, Tooltips, Message Bars, and Menus at the end of your MainLayout.razor file.

```razor
<FluentToastProvider />
<FluentDialogProvider />
<FluentTooltipProvider />
<FluentMessageBarProvider />
<FluentMenuProvider />
```

--------------------------------

### FluentBreadcrumb with Start, End, and Separator Icons (Razor)

Source: https://www.fluentui-blazor.net/Breadcrumb

This example demonstrates using FluentBreadcrumb to display breadcrumbs with icons at the start, end, and a custom separator. It utilizes the 'start', 'end', and 'separator' slots for icon placement. Dependencies include FluentBreadcrumb, FluentBreadcrumbItem, and FluentIcon components.

```Razor
<FluentBreadcrumb>
    <FluentBreadcrumbItem Href="#">
        Breadcrumb item 1
        <FluentIcon Value="@(new Icons.Regular.Size16.Home())" Color="@Color.Neutral" Slot="start" />
        <FluentIcon Value="@(new Icons.Regular.Size16.Home())" Color="@Color.Neutral" Slot="end" />
        <FluentIcon Value="@(new Icons.Regular.Size20.ChevronDoubleRight())" Color="@Color.Neutral" Slot="separator" />
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem Href="#">
        Breadcrumb item 2
        <FluentIcon Value="@(new Icons.Regular.Size16.Clipboard())" Color="@Color.Neutral" Slot="start" />
        <FluentIcon Value="@(new Icons.Regular.Size16.Clipboard())" Color="@Color.Neutral" Slot="end" />
        <FluentIcon Value="@(new Icons.Regular.Size20.ChevronDoubleRight())" Color="@Color.Neutral" Slot="separator" />
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem>
        Breadcrumb item 3
        <FluentIcon Value="@(new Icons.Regular.Size16.Money())" Color="@Color.Neutral" Slot="end" />
        <FluentIcon Value="@(new Icons.Regular.Size16.Money())" Color="@Color.Neutral" Slot="start" />
    </FluentBreadcrumbItem>
</FluentBreadcrumb>
```

--------------------------------

### Register OData DataGrid Adapter (C#)

Source: https://www.fluentui-blazor.net/datagrid

Code snippet for registering the OData DataGrid adapter in the application's service collection. This setup step is required after installing the adapter package to enable OData integration with the FluentDataGrid.

```csharp
builder.Services.AddDataGridODataAdapter();
```

--------------------------------

### Custom PullToRefresh Templates (Razor)

Source: https://www.fluentui-blazor.net/PullToRefresh

Illustrates how to customize the PullToRefresh component's appearance using templates for pulling, releasing, and completion states. This example uses plain text templates and shows how to hide the initial tip until the action starts.

```razor
<FluentPullToRefresh Direction="@PullDirection.Down"
                     OnRefreshAsync="OnRefreshAsync"
                     ShowStaticTip="false"
                     Style="height: 400px; width: 400px; ">
    <PullingTemplate>
        Pull to refresh
    </PullingTemplate>
    <ReleaseTemplate>
        Release to update
    </ReleaseTemplate>
    <CompletedTemplate>
        Update completed
    </CompletedTemplate>

    <ChildContent>
        <FluentListbox Height="300px" Items="@Enumerable.Range(1, ItemsCount).Select(i => $"Item {i}")" />
    </ChildContent>
</FluentPullToRefresh>


@code {
    int RefreshCount = 0;
    int ItemsCount = 2;

    public async Task<bool> OnRefreshAsync()
    {
        RefreshCount++;
        DemoLogger.WriteLine($"Pull down refresh count {RefreshCount}");
        await Task.Delay(1000);
        ItemsCount += 3;
        return true;
    }
}
```

--------------------------------

### Default Fluent UI Blazor Overlay Example

Source: https://www.fluentui-blazor.net/Overlay

Demonstrates a default overlay with a standard background color and opacity. It includes configuration options for alignment and justification, triggered by a button. The overlay contains a progress ring and handles close events.

```Razor
<FluentSelect Items=@(Enum.GetValues<JustifyContent>()) OptionValue="@(c => c.ToAttributeValue())" TOption="JustifyContent" Position="SelectPosition.Below" @bind-SelectedOption="@justification" />

<FluentSelect Items=@(Enum.GetValues<Align>()) OptionValue="@(c => c.ToAttributeValue())" TOption="Align" Position="SelectPosition.Below" @bind-SelectedOption="@alignment" />
<br />
<br />


<FluentButton Appearance="Appearance.Accent" @onclick="() => visible = !visible">Show Overlay</FluentButton>

<FluentOverlay @bind-Visible=@visible Transparent="false" Alignment="@alignment" Justification="@justification" OnClose="HandleOnClose">
    <FluentProgressRing />
</FluentOverlay>

@code {
    bool visible = false;
    JustifyContent justification = JustifyContent.Center;
    Align alignment = Align.Center;

    protected void HandleOnClose()
    {
        DemoLogger.WriteLine("Overlay closed");
    }
}
```

--------------------------------

### Fluent Blazor Text Field: With Start and End Icons

Source: https://www.fluentui-blazor.net/TextField

Demonstrates how to integrate icons within the FluentTextField component, both at the start and end positions of the input field. This enhances usability and visual appeal. The examples use Razor syntax.

```Razor
<h4>With start</h4>
<FluentTextField @bind-Value=value1>
    <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Slot="start" Color="Color.Neutral" />
</FluentTextField>

<h4>With end</h4>
<FluentTextField @bind-Value=value2                                                                                                                                                                                                                                                                                                                                                                                                       >
    <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="end" />
</FluentTextField>

@code {
    string? value1, value2;
}
```

--------------------------------

### FluentAppBarItem Component API

Source: https://www.fluentui-blazor.net/AppBar

This section outlines the properties and event callbacks for the FluentAppBarItem component, used to define individual items within the AppBar.

```APIDOC
## FluentAppBarItem Class

### Description
Represents an individual item within the FluentAppBar, typically containing an icon, text, and navigation link.

### Parameters
#### Path Parameters
None

#### Query Parameters
None

#### Request Body
None

### Parameters
- **ChildContent** (RenderFragment?) - Gets or sets the content to be shown.
- **Count** (int?) - Defaults to 0. Gets or sets the count to show on the item with a `FluentCounterBadge`.
- **Href** (string?) - Gets or sets the URL for this item.
- **IconActive** (Icon?) - Gets or sets the Icon to use when the item is hovered/selected/active.
- **IconRest** (Icon) - Gets or sets the Icon to use when the item is not hovered/selected/active.
- **Match** (NavLinkMatch) - Defaults to Prefix. Gets or sets how the link should be matched. Defaults to `Microsoft.AspNetCore.Components.Routing.NavLinkMatch.Prefix`.
- **Overflow** (bool?) - If this app is outside of visible app bar area.
- **Text** (string) - Gets or sets the text to show under the icon.
- **Tooltip** (string?) - Gets or sets the tooltip to show when the item is hovered.

### EventCallbacks
- **OnClick** (EventCallback<IAppBarItem>) - The callback to invoke when the item is clicked. Can be Horizontal or Vertical.

### Request Example
```razor
<FluentAppBar>
    <FluentAppBarItem Href="/" Text="Home" OnClick="HandleOnClick" />
    <FluentAppBarItem IconRest="Icons.Regular.Size24.Home()" IconActive="Icons.Filled.Size24.Home()" Text="Home" OnClick="HandleOnClick" />
</FluentAppBar>
```

### Response
#### Success Response (200)
Not applicable for component documentation.

#### Response Example
Not applicable for component documentation.
```

--------------------------------

### FluentLabel Component: Typography Examples

Source: https://www.fluentui-blazor.net/Label

Demonstrates the use of the FluentLabel component with various predefined typography styles from the Typography enum. Each label displays a different text format as defined by the enum.

```Razor
<FluentLabel Typo="Typography.Body"> Example 'Body' label </FluentLabel>
<FluentLabel Typo="Typography.Subject"> Example 'Subject' label </FluentLabel>
<FluentLabel Typo="Typography.Header"> Example 'Header' label </FluentLabel>
<FluentLabel Typo="Typography.PaneHeader"> Example 'PaneHeader' label </FluentLabel>
<FluentLabel Typo="Typography.EmailHeader"> Example 'EmailHeader' label </FluentLabel>
<FluentLabel Typo="Typography.PageTitle"> Example 'PageTitle' label </FluentLabel>
<FluentLabel Typo="Typography.HeroTitle"> Example 'HeroTitle' label </FluentLabel>
<FluentLabel Typo="Typography.H1"> Example 'H1' label </FluentLabel>
<FluentLabel Typo="Typography.H2"> Example 'H2' label </FluentLabel>
<FluentLabel Typo="Typography.H3"> Example 'H3' label </FluentLabel>
<FluentLabel Typo="Typography.H4"> Example 'H4' label </FluentLabel>
<FluentLabel Typo="Typography.H5"> Example 'H5' label </FluentLabel>
<FluentLabel Typo="Typography.H6"> Example 'H6' label </FluentLabel>
```

--------------------------------

### Breadcrumb with Start, End, and Custom Separator Icons

Source: https://www.fluentui-blazor.net/Breadcrumb

This example illustrates a more complex breadcrumb configuration, featuring icons for the start and end of each item, as well as a custom icon to act as the separator between items. This allows for highly customized visual navigation flows.

```APIDOC
## FluentBreadcrumb with Start, End, and Custom Separator Icons

### Description
This example showcases advanced customization of the `FluentBreadcrumb` component. It demonstrates using icons for the start and end of each `FluentBreadcrumbItem`, and also how to define a custom icon for the separator between items, providing a unique visual design.

### Razor Component
```razor
<FluentBreadcrumb>
    <FluentBreadcrumbItem Href="#">
        Breadcrumb item 1
        <FluentIcon Value="@(new Icons.Regular.Size16.Home())" Color="@Color.Neutral" Slot="start" />
        <FluentIcon Value="@(new Icons.Regular.Size16.Home())" Color="@Color.Neutral" Slot="end" />
        <FluentIcon Value="@(new Icons.Regular.Size20.ChevronDoubleRight())" Color="@Color.Neutral" Slot="separator" />
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem Href="#">
        Breadcrumb item 2
        <FluentIcon Value="@(new Icons.Regular.Size16.Clipboard())" Color="@Color.Neutral" Slot="start" />
        <FluentIcon Value="@(new Icons.Regular.Size16.Clipboard())" Color="@Color.Neutral" Slot="end" />
        <FluentIcon Value="@(new Icons.Regular.Size20.ChevronDoubleRight())" Color="@Color.Neutral" Slot="separator" />
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem>
        Breadcrumb item 3
        <FluentIcon Value="@(new Icons.Regular.Size16.Money())" Color="@Color.Neutral" Slot="end" />
        <FluentIcon Value="@(new Icons.Regular.Size16.Money())" Color="@Color.Neutral" Slot="start" />
    </FluentBreadcrumbItem>
</FluentBreadcrumb>
```
```

--------------------------------

### Razor MultiSplitter Layout Example

Source: https://www.fluentui-blazor.net/MultiSplitter

This example demonstrates how to use the FluentMultiSplitter component to create a complex layout with multiple resizable and collapsible panes. It includes event handlers for resize and collapse/expand actions, showcasing dynamic layout adjustments.

```razor
<FluentMultiSplitter OnResize="@OnResizeHandler" Height="150px" Style="border: 1px dashed var(--accent-fill-rest);">
    <FluentMultiSplitterPane Size="20%" Min="50px" Max="70%">
        Left Menu
    </FluentMultiSplitterPane>
    <FluentMultiSplitterPane Size="50%">
        <FluentMultiSplitter OnResize="@OnResizeHandler" OnExpand="@OnCollapseExpand" OnCollapse="@OnCollapseExpand" Orientation="Orientation.Vertical">
            <FluentMultiSplitterPane Collapsible="true">
                Main Content
            </FluentMultiSplitterPane>
            <FluentMultiSplitterPane Collapsible="true">
                Console log
            </FluentMultiSplitterPane>
        </FluentMultiSplitter>
    </FluentMultiSplitterPane>
    <FluentMultiSplitterPane Size="30%">
        Properties
    </FluentMultiSplitterPane>
</FluentMultiSplitter>

@code
{
    void OnResizeHandler(FluentMultiSplitterResizeEventArgs e)
    {
        DemoLogger.WriteLine($"Pane {e.PaneIndex} Resize (New size {e.NewSize})");
    }

    void OnCollapseExpand(FluentMultiSplitterEventArgs e)
    {
        bool willCollapse = !e.Pane.Collapsed;
        DemoLogger.WriteLine($"Pane {e.PaneIndex} {(willCollapse ? "collapsed" : "expanded")}");
    }
}

```

--------------------------------

### FluentOption with Icons in Blazor

Source: https://www.fluentui-blazor.net/Option

Shows how to enhance FluentOption components in Blazor by including icons. This example demonstrates adding icons to the start, end, or both slots of an option within a FluentSelect component.

```Razor
<h4>With icons</h4>
<FluentSelect Value=2 TOption="int">
    <FluentOption Value=1>
        <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="start" />
        <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="end" />
        This option has an icon in its start and end slots.
    </FluentOption>
    <FluentOption Value=2>
        <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="start" />
        This option has an icon in its start slot.
    </FluentOption>
    <FluentOption Value=3>
        <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="end" />
        This option has an icon in its end slot.
    </FluentOption>
</FluentSelect>
```

--------------------------------

### FluentRating Default Example with Blazor

Source: https://www.fluentui-blazor.net/Rating

This example demonstrates the default configuration of the FluentRating component. It allows users to rate a product out of 5 stars, with dynamic label updates based on hover and selection. It also includes a button to clear the rating.

```razor
<FluentStack VerticalAlignment="VerticalAlignment.Center">
    <FluentRating Max="5"
                  Label="Rate me!"
                  @bind-Value="@SelectedValue"
                  OnHoverValueChanged="@(i => HoverValue = i)"
                  AllowReset="true"/>
    <FluentLabel>@LabelText</FluentLabel>
</FluentStack>

<FluentButton Appearance="Appearance.Lightweight"
              OnClick="@((e) => SelectedValue = 0)">
    Clear Rating
</FluentButton>

@code
{
    int SelectedValue = 0;
    int? HoverValue = null;

    private string LabelText => (HoverValue ?? SelectedValue) switch
    {
        1 => "Very bad",
        2 => "Bad",
        3 => "Sufficient",
        4 => "Good",
        5 => "Awesome!",
        _ => "Rate our product!"
    };
}
```

--------------------------------

### FluentPersona with Text Position (Razor)

Source: https://www.fluentui-blazor.net/Persona

Demonstrates the FluentPersona component with the text aligned to the start. This example showcases the `TextPosition` property set to `TextPosition.Start` to alter the layout of the name relative to the avatar.

```Razor
<FluentPersona Name="Bill Gates"
               Status="PresenceStatus.Available"
               StatusSize="PresenceBadgeSize.Small"
               StatusTitle="He is available"
               Image="@DataSource.SamplePicture"
               ImageSize="50px"
               TextPosition="TextPosition.Start">
</FluentPersona>
```

--------------------------------

### Add IIS Reboot CSS for .NET 9

Source: https://www.fluentui-blazor.net/CodeSetup

Manually add the reboot CSS for IIS hosting in .NET 9 projects by including a link tag in App.razor, index.html, or _Layout.cshtml.

```razor
<link href="@Assets["_content/Microsoft.FluentUI.AspNetCore.Components/css/reboot.css"]" rel="stylesheet" />
```

--------------------------------

### Configure Fluent UI Services in Program.cs

Source: https://www.fluentui-blazor.net/UpgradeGuide

Updates the `AddFluentUIComponents` call in `Program.cs`. The `IconConfiguration` and `EmojiConfiguration` options are removed. The `HostingModel` should be set based on the `BlazorHostingModel` enumeration.

```csharp
builder.Services.AddFluentUIComponents(options =>
{
    options.HostingModel = {see remark below};
});
```

--------------------------------

### Align Enumeration Changes v2 to v3

Source: https://www.fluentui-blazor.net/UpgradeGuide

Highlights the change in the Align enumeration values from 'Left' and 'Right' to 'Start' and 'End' for improved consistency, especially in RTL applications.

```csharp
The `Align` enumeration values `Left` and `Right` have been changed to `Start` and `End`.
```

--------------------------------

### FluentOverflow Horizontal Customized Example (Razor)

Source: https://www.fluentui-blazor.net/Overflow

This example demonstrates a horizontally configured FluentOverflow component. It displays a list of items, allows adding and removing items, and customizes the 'more' button and overflow content. It utilizes FluentBadge and FluentTooltip for enhanced UI.

```Razor
<div style="background-color: var(--neutral-layer-4); overflow: auto; resize: horizontal; padding: 10px;">
    <FluentOverflow OnOverflowRaised="OverflowHandler" Style="width: 100%; border: 1px solid darkgray; padding: 2px; margin-bottom: 4px;">
        <ChildContent>
            @foreach (var item in Items)
            {
                <FluentOverflowItem><FluentBadge>@item</FluentBadge></FluentOverflowItem>
            }
        </ChildContent>
        <MoreButtonTemplate>
            <FluentBadge Style="min-width: 32px; max-width:32px;">
                @($"+{context.ItemsOverflow.Count()}")
            </FluentBadge>
        </MoreButtonTemplate>
        <OverflowTemplate>
            <FluentTooltip Anchor="@context.IdMoreButton" UseTooltipService="false">
                @foreach (var item in context.ItemsOverflow)
                {
                    <div style="margin: 5px;">@item.Text</div>
                }
            </FluentTooltip>
        </OverflowTemplate>
    </FluentOverflow>
</div>

<FluentButton @onclick="AddNewItemClick">Add</FluentButton>
<FluentButton @onclick="RemoveLastItemClick">Remove</FluentButton>

@code
{
    static string[] Catalog = new[] { "Blazor", "WPF", "Microsoft", "#Framework",
                                      "Electron", "WinForms", "MAUI", "Fluent Reality",
                                      "Office", "Installation", "Azure", "DevOps" };

    List<string> Items = new List<string>() { Catalog[0], Catalog[1] };

    void OverflowHandler(IEnumerable<FluentOverflowItem> items)
    {
        var text = String.Join("; ", items.Select(i => i.Text));

    }

    void AddNewItemClick()
    {
        var index = new Random().NextInt64(Catalog.Length);
        Items.Add(Catalog[index]);
    }

    void RemoveLastItemClick()
    {
        Items.RemoveAt(Items.Count-1);
    }
}

```

--------------------------------

### Install Fluent UI Emoji NuGet Package

Source: https://www.fluentui-blazor.net/IconsAndEmoji

Installs the NuGet package required for integrating Fluent Emoji into your Blazor application. This package makes the Fluent Emoji collection available for use within your components.

```bash
dotnet add package Microsoft.FluentUI.AspNetCore.Components.Emoji
```

--------------------------------

### Default SplashScreen Implementation (C#)

Source: https://www.fluentui-blazor.net/SplashScreen

C# code for the default splash screen example in Fluent UI Blazor. It demonstrates how to configure and show a splash screen using DialogService, including simulating tasks and updating content asynchronously. It also shows how to handle the dialog result.

```csharp
// ------------------------------------------------------------------------
// This file is licensed to you under the MIT License.
// ------------------------------------------------------------------------

using global::Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace FluentUI.Demo.Shared.Pages.SplashScreen.Examples;

public partial class DialogSplashScreenDefault
{
    private IDialogReference? _dialog;

    private async Task OpenSplashDefaultAsync()
    {
        DemoLogger.WriteLine($"Open default SplashScreen for 4 seconds");
        DialogParameters<SplashScreenContent> parameters = new()
        {
            Content = new()
            {
                DisplayTime = 0,    // See Task.Delay below
                Title = "Core components",
                SubTitle = "Microsoft Fluent UI Blazor library",
                LoadingText = "Loading...",
                Message = (MarkupString)"some <i>extra</i> text <strong>here</strong>",
                Logo = FluentSplashScreen.LOGO,
            },
            PreventDismissOnOverlayClick = true,
            Modal = false,
            Width = "640px",
            Height = "480px",
        };
        _dialog = await DialogService.ShowSplashScreenAsync(parameters);

        var splashScreen = (SplashScreenContent)_dialog.Instance.Content;

        // Simulate a first task
        await Task.Delay(2000);

        // Update the splash screen content and simulate a second task
        splashScreen.UpdateLabels(loadingText: "Second task...");
        await Task.Delay(2000);

        await _dialog.CloseAsync();

        DialogResult result = await _dialog.Result;
        await HandleDefaultSplashAsync(result);
    }

    private void OpenSplashDefault()
    {
        DemoLogger.WriteLine($"Open default SplashScreen for 4 seconds");
        DialogParameters<SplashScreenContent> parameters = new()
        {
            Content = new()
            {
                Title = "Core components",
                SubTitle = "Microsoft Fluent UI Blazor library",
                LoadingText = "Loading...",
                Message = (MarkupString)"some <i>extra</i> text <strong>here</strong>",
                Logo = FluentSplashScreen.LOGO,
            },
            Width = "640px",
            Height = "480px",
            Modal = true,
        };
        DialogService.ShowSplashScreen(this, HandleDefaultSplashAsync, parameters);
    }

    private async Task HandleDefaultSplashAsync(DialogResult result)
    {
        await Task.Run(() => DemoLogger.WriteLine($"Default splash closed"));
    }
}
```

--------------------------------

### Register HttpClient for Blazor Server

Source: https://www.fluentui-blazor.net/CodeSetup

Register a default HttpClient before adding Fluent UI services when running a Blazor Server application.

```csharp
builder.Services.AddHttpClient();
```

--------------------------------

### FluentSearch: Appearance and Validation Examples

Source: https://www.fluentui-blazor.net/Search

Illustrates different visual appearances (Filled) and validation states (Required, Disabled, ReadOnly) for the FluentSearch component. These examples show how to configure the component for various input scenarios, including setting labels, placeholders, and disabling user input. The output includes styled and functional FluentSearch inputs.

```Razor
<h4>Default</h4>
<FluentSearch @bind-Value=value Appearance="FluentInputAppearance.Filled"></FluentSearch>
<FluentSearch @bind-Value=value Appearance="FluentInputAppearance.Filled">label</FluentSearch>

<h4>Placeholder</h4>
<FluentSearch @bind-Value=value Appearance="FluentInputAppearance.Filled" Placeholder="Placeholder"></FluentSearch>

<!-- Required -->
<h4>Required</h4>
<FluentSearch @bind-Value=value Appearance="FluentInputAppearance.Filled" Required="true"></FluentSearch>

<!-- Disabled -->
<h4>Disabled</h4>
<FluentSearch @bind-Value=value Appearance="FluentInputAppearance.Filled" Disabled="true"></FluentSearch>
<FluentSearch @bind-Value=value Appearance="FluentInputAppearance.Filled" Disabled="true">label</FluentSearch>
<FluentSearch @bind-Value=value Appearance="FluentInputAppearance.Filled" Disabled="true" Placeholder="placeholder"></FluentSearch>

<!-- Read only -->
<h4>Read only</h4>
<FluentSearch @bind-Value=value Appearance="FluentInputAppearance.Filled" ReadOnly="true"></FluentSearch>
<FluentSearch @bind-Value=value Appearance="FluentInputAppearance.Filled" ReadOnly="true">label</FluentSearch>
@code {
    string? value;
}
```

--------------------------------

### Fluent UI Blazor Breadcrumb Item Examples

Source: https://www.fluentui-blazor.net/Breadcrumb

Demonstrates various configurations for a single breadcrumb item using Fluent UI Blazor. This includes default items, items with custom separators, and items with start and end icons. The FluentBreadcrumbItem component wraps individual links in the breadcrumb trail.

```Razor
<FluentBreadcrumbItem Href="#">
    Breadcrumb item
</FluentBreadcrumbItem>
```

```Razor
<FluentBreadcrumbItem Href="#">
    Breadcrumb item
    <FluentIcon Value="@(new Icons.Regular.Size20.ChevronDoubleRight())" Color="@Color.Neutral" Slot="separator" />
</FluentBreadcrumbItem>
```

```Razor
<FluentBreadcrumbItem Href="#">
    Breadcrumb item
    <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="start" />
</FluentBreadcrumbItem>
```

```Razor
<FluentBreadcrumbItem Href="#">
    Breadcrumb item
    <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="end" />
</FluentBreadcrumbItem>
```

```Razor
<FluentBreadcrumbItem Href="#">
    Breadcrumb item
    <FluentIcon Value="@(new Icons.Regular.Size20.ChevronDoubleRight())" Color="@Color.Neutral" Slot="separator" />
    <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="end" />
    <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="start" />
</FluentBreadcrumbItem>
```

--------------------------------

### Add Fluent UI Namespace Import

Source: https://www.fluentui-blazor.net/CodeSetup

Import the Fluent UI Blazor components namespace in your _Imports.razor file to use them in your application.

```razor
@using Microsoft.FluentUI.AspNetCore.Components
```

--------------------------------

### Default Tooltip Example in Razor

Source: https://www.fluentui-blazor.net/Tooltip

Demonstrates a basic Fluent UI Blazor tooltip attached to an icon. It shows how to anchor the tooltip to an element using an ID and provides a simple dismiss handler.

```razor
<FluentIcon Id="myFirstButton" Icon="Icons.Regular.Size24.Notepad" />

<FluentTooltip Anchor="myFirstButton" OnDismissed="OnDismiss">
    Hello World <br />
    It is a <i>small</i> tooltip.
</FluentTooltip>

@code {
    private void OnDismiss()
    {
        DemoLogger.WriteLine("Tooltip dismissed!");
    }
}
```

--------------------------------

### Timed Fluent UI Blazor Overlay Example

Source: https://www.fluentui-blazor.net/Overlay

Shows a timed overlay that automatically hides after a specified duration (3 seconds in this example). It uses similar configuration to the default overlay but includes logic to manage visibility over time. The overlay also contains a progress ring.

```Razor
<FluentSelect Items=@(Enum.GetValues<JustifyContent>()) OptionValue="@(c => c.ToAttributeValue())" TOption="JustifyContent" Position="SelectPosition.Below" @bind-SelectedOption="@justification" />

<FluentSelect Items=@(Enum.GetValues<Align>()) OptionValue="@(c => c.ToAttributeValue())" TOPTION="Align" Position="SelectPosition.Below" @bind-SelectedOption="@alignment" />
<br />
<br />


<FluentButton Appearance="Appearance.Accent" @onclick="HandleOnOpen">Show Overlay</FluentButton>

<FluentOverlay @bind-Visible=@visible Opacity="0.4" Alignment="@alignment" Justification="@justification" OnClose="HandleOnClose">
    <FluentProgressRing />
</FluentOverlay>

@code {
    bool visible = false;
    JustifyContent justification = JustifyContent.Center;
    Align alignment = Align.Center;

    protected void HandleOnClose()
    {
        DemoLogger.WriteLine("Overlay closed");
    }

    protected async Task HandleOnOpen()
    {
        visible = true;
        DemoLogger.WriteLine("Overlay opened");
        await Task.Delay(3000);
        visible = false;
    }
}
```

--------------------------------

### Displaying Preformatted Code Blocks in HTML

Source: https://www.fluentui-blazor.net/Reboot

This snippet shows how to use the `<pre>` tag for multi-line code. It emphasizes escaping angle brackets for proper rendering and customizes margin properties for consistent spacing. This is crucial for displaying code examples within web pages.

```html
<p>Sample text here...</p>
<p>And another line of sample text here...</p>
```

--------------------------------

### Displaying Sample Program Output with HTML <samp> Tag

Source: https://www.fluentui-blazor.net/Reboot

Shows how to use the `<samp>` tag for indicating sample output from a computer program. This helps differentiate program-generated text from narrative text in documentation.

```html
This text is meant to be treated as sample output from a computer program.
```

--------------------------------

### Blazor Accordion Default Example with Slots and Templates

Source: https://www.fluentui-blazor.net/Accordion

Demonstrates the default behavior of the Fluent Blazor Accordion, showcasing the use of 'start' and 'end' slots for adding content like icons to accordion item headers, and the HeaderTemplate for custom header rendering. It also includes event handling for item changes.

```Razor
<FluentAccordion ActiveId="@activeId" OnAccordionItemChange="HandleOnAccordionItemChange">
    <FluentAccordionItem Heading="Panel one">
        <FluentIcon Value="@(new Icons.Regular.Size20.Globe())" Color="@Color.Neutral" Slot="start" />
        Panel one content, using the 'start' slot for extra header content (in this case an icon)
    </FluentAccordionItem>
    <FluentAccordionItem Heading="Panel two">
        <div slot="end">
            #end#
        </div>
        Panel two content, using the 'end' slot for extra header content
    </FluentAccordionItem>
    <FluentAccordionItem Expanded="true" Heading="Panel three">
        Panel three content
    </FluentAccordionItem>
    <FluentAccordionItem Expanded="true">
        <HeadingTemplate>
            Panel <span style="color:red">Four</span>
        </HeadingTemplate>
        <ChildContent>
            Panel four content
        </ChildContent>
    </FluentAccordionItem>
</FluentAccordion>

<p>Last changed accordion item: @(changed?.Heading ?? "item with HeaderTemplate"), Expanded: @changed?.Expanded</p>


@code {
    string activeId = "accordion-1";

    FluentAccordionItem? changed;

    private void HandleOnAccordionItemChange(FluentAccordionItem item)
    {
        changed = item;
    }
}
```

--------------------------------

### Fluent Blazor Text Field: Full Width and Placeholder

Source: https://www.fluentui-blazor.net/TextField

Shows how to make the FluentTextField component occupy the full width of its container and how to set a placeholder text to guide user input. These examples are implemented using Razor syntax for Blazor.

```Razor
<h4>Full Width</h4>
<FluentTextField @bind-Value=value1 style="width: 100%;"></FluentTextField>

<h4>Placeholder</h4>
<FluentTextField @bind-Value=value2 Placeholder="Placeholder"></FluentTextField>

@code {
    string? value1, value2;
}
```

--------------------------------

### Import Fluent UI Namespace for Icons

Source: https://www.fluentui-blazor.net/ProjectSetup

Adds the necessary using statement to your `_Imports.razor` file to make Fluent UI components and icon definitions available in your Blazor application.

```razor
@using Microsoft.Fast.Components.FluentUI

```

--------------------------------

### Blazor Component Code Section

Source: https://www.fluentui-blazor.net/datagrid-typical

Defines the C# backend logic for a Blazor component, including state management for a data grid, pagination, filtering, sorting, and asynchronous data loading.

```csharp
@code {
    FluentDataGrid<Country>? grid;
    bool _clearItems = false;
    IQueryable<Country>? items;
    PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
    string nameFilter = string.Empty;
    int minMedals;
    int maxMedals = 130;

    ColumnResizeLabels resizeLabels = ColumnResizeLabels.Default with
    {
        DiscreteLabel = "Width (+/- 10px)",
        ResetAriaLabel = "Restore"
    };

    GridSort<Country> rankSort = GridSort<Country>
        .ByDescending(x => x.Medals.Gold)
        .ThenDescending(x => x.Medals.Silver)
        .ThenDescending(x => x.Medals.Bronze);

    Func<Country, string?> rowClass = x => x.Name.StartsWith("A") ? "highlighted" : null;
    Func<Country, string?> rowStyle = x => x.Name.StartsWith("Au") ? "background-color: var(--highlight-bg)" : null;

    //IQueryable<Country>? FilteredItems => items?.Where(x => x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase));

    IQueryable<Country>? FilteredItems
    {
        get
        {
            var result = items?.Where(c => c.Medals.Total <= maxMedals);

            if (result is not null && !string.IsNullOrEmpty(nameFilter))
            {
                result = result.Where(c => c.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase));
            }

            if (result is not null && minMedals > 0)
            {
                result = result.Where(c => c.Medals.Total >= minMedals);
            }

            return result;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        items = (await Data.GetCountriesAsync()).AsQueryable();
    }

    private void HandleCountryFilter(ChangeEventArgs args)
    {
        if (args.Value is string value)
        {
            nameFilter = value;
        }
    }

    private void HandleClear()
    {
        if (string.IsNullOrWhiteSpace(nameFilter))
        {
            nameFilter = string.Empty;
        }
    }

    private async Task HandleCloseFilterAsync(KeyboardEventArgs args)
    {
        if (args.Key == "Escape")
        {
            nameFilter = string.Empty;
        }
        if (args.Key == "Enter" && grid is not null)
        {
            await grid.CloseColumnOptionsAsync();
        }
    }

    private async Task ToggleItemsAsync()
    {
        if (_clearItems)
        {
            items = null;
        }
        else
        {
            items = (await Data.GetCountriesAsync()).AsQueryable();
        }
    }
}
```

--------------------------------

### Default SplashScreen Example (Razor)

Source: https://www.fluentui-blazor.net/SplashScreen

Razor markup for triggering the default splash screen in Fluent UI Blazor. It includes buttons to open the splash screen synchronously and asynchronously, utilizing the injected IDialogService.

```razor
@inject IDialogService DialogService

<FluentButton @onclick="@OpenSplashDefault" Appearance="Appearance.Accent">
    Open splash screen
</FluentButton>

<FluentButton @onclick="@OpenSplashDefaultAsync" Appearance="Appearance.Accent">
    Open splash screen (async)
</FluentButton>
```

--------------------------------

### Include Fluent UI Blazor Script in App.razor

Source: https://www.fluentui-blazor.net/CodeSetup

Manual inclusion of the Fluent UI Web Components script in the App.razor file, typically for .NET 8 SSR projects.

```html
<script src="_content/Microsoft.FluentUI.AspNetCore.Components/Microsoft.FluentUI.AspNetCore.Components.lib.module.js" type="module" async></script>
```

--------------------------------

### Fluent Blazor Nested Menu Example

Source: https://www.fluentui-blazor.net/Menu

Shows how to create a nested menu structure using Fluent Blazor components. A switch controls the visibility of the main menu, which contains items that expand to reveal sub-menu items. This example requires the Fluent UI Blazor library.

```Razor
<FluentSwitch @bind-Value="@open">Show</FluentSwitch>
<FluentMenu @bind-Open="@open">
    <FluentMenuItem Label="Menu item 1">
        <MenuItems>
            <FluentMenuItem>Menu item 1.1</FluentMenuItem>
            <FluentMenuItem>Menu item 1.2</FluentMenuItem>
            <FluentMenuItem>Menu item 1.3</FluentMenuItem>
        </MenuItems>
    </FluentMenuItem>
    <FluentMenuItem Label="Menu item 2">
        <MenuItems>
            <FluentMenuItem>Menu item 2.1</FluentMenuItem>
            <FluentMenuItem>Menu item 2.2</FluentMenuItem>
            <FluentMenuItem>Menu item 2.3</FluentMenuItem>
        </MenuItems>
    </FluentMenuItem>
    <FluentMenuItem Label="Menu item 3">
        <MenuItems>
            <FluentMenuItem>Menu item 3.1</FluentMenuItem>
            <FluentMenuItem>Menu item 3.2</FluentMenuItem>
            <FluentMenuItem>Menu item 3.3</FluentMenuItem>
        </MenuItems>
    </FluentMenuItem>
</FluentMenu>

@code {
    bool open = false;
}

```

--------------------------------

### Configure Fluent Design Theme and UI Elements in Blazor

Source: https://www.fluentui-blazor.net/DesignTheme

This Razor code example shows how to configure the FluentDesignTheme component, binding its mode and office color properties to C# variables. It also demonstrates using Fluent UI components like FluentGrid, FluentSelect, and FluentButton to create a theme selection interface and display example content.

```razor
<FluentDesignTheme @bind-Mode="@Mode"
                   @bind-OfficeColor="@OfficeColor"
                   OnLoaded="@OnLoaded"
                   OnLuminanceChanged="@OnLuminanceChanged"
                   StorageName="theme" />

<div style="min-height: 250px;">
    <FluentGrid>
        <FluentGridItem>
            <FluentSelect Label="Theme"
                          Width="250px"
                          Items="@(Enum.GetValues<DesignThemeModes>())"
                          @bind-SelectedOption="@Mode" />
        </FluentGridItem>

        <FluentGridItem>
            <FluentSelect Label="Color"
                          Items="@(Enum.GetValues<OfficeColor>().Select(i => (OfficeColor?)i))"
                          Height="200px"
                          Width="250px"
                          @bind-SelectedOption="@OfficeColor">
                <OptionTemplate>
                    <FluentStack>
                        <FluentIcon Value="@(new Icons.Filled.Size20.RectangleLandscape())"
                                    Color="Color.Custom"
                                    CustomColor="@(@context.ToAttributeValue() != "default" ? context.ToAttributeValue() : "#036ac4" )" />
                        <FluentLabel>@context</FluentLabel>
                    </FluentStack>
                </OptionTemplate>
            </FluentSelect>
            <FluentButton Appearance="Appearance.Accent" OnClick="PickRandomColor">Feeling lucky?</FluentButton>
        </FluentGridItem>
    </FluentGrid>

    <FluentStack Style="margin: 30px 0px; padding: 30px; border: 1px solid var(--accent-fill-rest);">
        <FluentIcon Value="@(new Icons.Regular.Size24.Airplane())" />
        <FluentLabel>Example of content</FluentLabel>
        <FluentButton Appearance="Appearance.Outline">Outline button</FluentButton>
        <FluentButton Appearance="Appearance.Accent">Accent button</FluentButton>
    </FluentStack>
</div>

@code
{
    public DesignThemeModes Mode { get; set; }



    public OfficeColor? OfficeColor { get; set; }

    void OnLoaded(LoadedEventArgs e)
    {
        DemoLogger.WriteLine($"Loaded: {(e.Mode == DesignThemeModes.System ? "System" : "")} {(e.IsDark ? "Dark" : "Light")}");
    }

    void OnLuminanceChanged(LuminanceChangedEventArgs e)
    {
        DemoLogger.WriteLine($"Changed: {(e.Mode == DesignThemeModes.System ? "System" : "")} {(e.IsDark ? "Dark" : "Light")}");
    }

    void PickRandomColor()
    {
        OfficeColor = OfficeColorUtilities.GetRandom();
    }
}
```

--------------------------------

### CSS for Flags and Search Box

Source: https://www.fluentui-blazor.net/datagrid-typical

Provides CSS styles for common UI elements. The '.flag' class ensures flags are uniformly sized and centered, while '.search-box' styles a search input field for consistent width and appearance.

```css
/* Ensure all the flags are the same size, and centered */
.flag {
    height: 1rem;
    margin-top: 4px;
    border: 1px solid var(--neutral-layer-3);
}
.search-box {
    min-width: 250px;
    width: 100%;
}

    .search-box fluent-search {
        width: 100%;
    }
```

--------------------------------

### FluentTabs: No Active Indicator (Razor)

Source: https://www.fluentui-blazor.net/Tabs

Demonstrates how to create a FluentTabs component in Razor that disables the active indicator. This example shows a basic setup with multiple tabs and their content. It utilizes the `ShowActiveIndicator` attribute set to `false`.

```Razor
<h4>No active indicator</h4>
<FluentTabs ShowActiveIndicator=false ActiveTabId="TabTwo">
    <FluentTab Id="TabOne" Label="Tab one">
        Tab one content. This is for testing.
    </FluentTab>
    <FluentTab Id="TabTwo" Label="Tab two">
        Tab two content. This is for testing.
    </FluentTab>
    <FluentTab Id="TabThree" Label="Tab three">
        Tab three content. This is for testing.
    </FluentTab>
</FluentTabs>
```

--------------------------------

### Fluent Blazor Search Component States and Styling

Source: https://www.fluentui-blazor.net/Search

This example demonstrates various states and styling options for the Fluent Blazor Search component. It shows how to apply full width styling, set placeholder text, enforce required input, disable the component, and configure it as read-only. These examples are useful for customizing the user experience and form validation.

```razor
<h4>Full Width</h4>
<FluentSearch @bind-Value=value style="width: 100%;"></FluentSearch>

<h4>Placeholder</h4>
<FluentSearch @bind-Value=value Placeholder="Placeholder"></FluentSearch>

<h4>Required</h4>
<FluentSearch @bind-Value=value Required="true"></FluentSearch>

<h4>Disabled</h4>
<FluentSearch @bind-Value=value Disabled="true"></FluentSearch>
<FluentSearch @bind-Value=value Disabled="true">label</FluentSearch>
<FluentSearch @bind-Value=value Disabled="true" Placeholder="placeholder"></FluentSearch>

<h4>Read only</h4>
<FluentSearch @bind-Value=value ReadOnly="true"></FluentSearch>
<FluentSearch @bind-Value=value ReadOnly="true">label</FluentSearch>
@code {
    string? value;
}
```

--------------------------------

### Styling HTML Blockquote Element

Source: https://www.fluentui-blazor.net/Reboot

Describes the default margin styling for blockquotes (`1em 40px`) and provides an example of its usage for quoting text and attributing the source.

```markdown
> A well-known quote, contained in a blockquote element. 
Someone famous in Source Title
```

--------------------------------

### Use Start and End Slots in FluentToolbar (Razor)

Source: https://www.fluentui-blazor.net/Toolbar

Illustrates the use of 'start' and 'end' slots within FluentToolbar to position FluentIcon components. This allows for custom elements at the beginning and end of the toolbar.

```Razor
<FluentToolbar id="toolbar-start-end-slots" Orientation="Orientation.Horizontal">
    <FluentIcon Value="@(new Icons.Filled.Size16.Globe())" Slot="start" Color="Color.Accent" />
    <FluentIcon Value="@(new Icons.Filled.Size16.Globe())" Slot="end" Color="Color.Accent" />
</FluentToolbar>
<FluentToolbar id="toolbar-start-end-slots-vertical" Orientation="Orientation.Vertical">
    <FluentIcon Value="@(new Icons.Filled.Size16.Globe())" Slot="start" Color="Color.Accent" />
    <FluentIcon Value="@(new Icons.Filled.Size16.Globe())" Slot="end" Color="Color.Accent" />
</FluentToolbar>
```

--------------------------------

### Custom Background Color Fluent UI Blazor Overlay Example

Source: https://www.fluentui-blazor.net/Overlay

Demonstrates customizing the background color of the overlay. This example sets a specific hex color ('#e8f48c') and opacity for the overlay, which contains a progress ring and is managed by a button.

```Razor
<FluentButton Appearance="Appearance.Accent" @onclick="() => visible = !visible">Show Overlay</FluentButton>

<FluentOverlay @bind-Visible=@visible Opacity="0.6" BackgroundColor="#e8f48c" OnClose="HandleOnClose">
    <FluentProgressRing />
</FluentOverlay>

@code {
    bool visible = false;

    protected void HandleOnClose()
    {
        DemoLogger.WriteLine("Custom background color overlay closed");
    }
}
```

--------------------------------

### Fluent Blazor Menu with Service Provider Example

Source: https://www.fluentui-blazor.net/Menu

This Razor code demonstrates how to use the FluentMenu component with the FluentMenuProvider. It shows how to link a button to open a menu using an ID and an Anchor tag. The menu is rendered at the provider's location, allowing it to appear above other components. This example utilizes UseMenuService="true" (or it can be omitted as it's the default).

```razor
<FluentStack>

    <FluentCard>
        <p>Click this button to open a Menu.</p>
        <FluentButton Id="btnOpenMenuProvider1"
                      Appearance="Appearance.Accent"
                      OnClick="@(() => open1 = !open1)">
            Open menu
        </FluentButton>

        <!-- UseMenuService="false" -->
        <FluentMenu UseMenuService="false"
                    Anchor="btnOpenMenuProvider1"
                    @bind-Open="open1"
                    VerticalThreshold="170">
            <FluentMenuItem>Menu item 1</FluentMenuItem>
            <FluentMenuItem>Menu item 2</FluentMenuItem>
        </FluentMenu>
    </FluentCard>

    <FluentCard>
        <p>Click this button to open a Menu.</p>
        <FluentButton Id="btnOpenMenuProvider2"
                      Appearance="Appearance.Accent"
                      OnClick="@(() => open2 = !open2)">
            Open menu
        </FluentButton>

        <!-- UseMenuService="true" (or undefined) -->
        <FluentMenu UseMenuService="true"
                    Anchor="btnOpenMenuProvider2"
                    @bind-Open="open2"
                    VerticalThreshold="170">
            <FluentMenuItem>Menu item 1</FluentMenuItem>
            <FluentMenuItem>Menu item 2</FluentMenuItem>
        </FluentMenu>
    </FluentCard>

</FluentStack>

@code {
    bool open1 = false;
    bool open2 = false;
}
```

--------------------------------

### FluentAutocomplete Component Example in Razor

Source: https://www.fluentui-blazor.net/Autocomplete

This example demonstrates the default usage of the FluentAutocomplete component in Blazor. It showcases how to bind selected options, handle asynchronous search, and configure auto-height based on the MaxAutoHeight attribute. Dependencies include a DataSource service for fetching country data.

```Razor
@inject DataSource Data

<FluentAutocomplete TOption="Country"
                    AutoComplete="off"
                    Autofocus="true"
                    Label="Select a country"
                    Width="250px"
                    MaxAutoHeight="@(AutoHeight ? "200px" : null)"
                    Placeholder="Select countries"
                    OnOptionsSearch="@OnSearchAsync"
                    OptionDisabled="@(e => e.Code == "au")"
                    MaximumSelectedOptions="5"
                    OptionText="@(item => item.Name)"
                    @bind-SelectedOptions="@SelectedItems" />

<p>
    <b>Selected</b>: @(String.Join(" - ", SelectedItems.Select(i => i.Name)))
</p>
<p>
    <FluentSwitch @bind-Value="@AutoHeight" Label="Auto Height" /><br />
    When the <code>MaxAutoHeight</code> attribute is set, the component adapts its height in relation to the selected elements.
</p>
@code
{
    bool AutoHeight = false;
    IEnumerable<Country> SelectedItems = Array.Empty<Country>();

    private async Task OnSearchAsync(OptionsSearchEventArgs<Country> e)
    {
        var allCountries = await Data.GetCountriesAsync();
        e.Items = allCountries.Where(i => i.Name.StartsWith(e.Text, StringComparison.OrdinalIgnoreCase))
                              .OrderBy(i => i.Name);
    }
}

```

--------------------------------

### Horizontal and Vertical Tabs Layout in Razor

Source: https://www.fluentui-blazor.net/Tabs

This snippet showcases how to configure FluentUI Blazor Tabs for both horizontal and vertical layouts. It demonstrates using slots for custom content at the start and end of the tab bar, and setting the `Orientation` property to switch between layouts. The `ActiveTabId` property is used to pre-select a tab. This example is purely presentational.

```Razor
<h4>Horizontal</h4>
<FluentTabs ActiveTabId="tab-2" >
    <div slot="start">
        <FluentButton>1</FluentButton>
        <FluentButton>2</FluentButton>
        <FluentButton>3</FluentButton>
    </div>
    <div slot="end">
        <FluentButton>1</FluentButton>
        <FluentButton>2</FluentButton>
        <FluentButton>3</FluentButton>
    </div>
    <FluentTab Id="tab-1" Label="Tab one">
    Tab one content. This is for testing.
    </FluentTab>
    <FluentTab Id="tab-2" Label="Tab two">
        Tab two content. This is for testing.
    </FluentTab>
    <FluentTab Id="tab-3" Label="Tab three">
        Tab three content. This is for testing.
    </FluentTab>
</FluentTabs>

<h4>Vertical</h4>
<FluentTabs Orientation="Orientation.Vertical" ActiveTabId="tab-v3">
    <div slot="start">
        <FluentButton>1</FluentButton>
        <FluentButton>2</FluentButton>
        <FluentButton>3</FluentButton>
    </div>
    <div slot="end">
        <FluentButton>1</FluentButton>
        <FluentButton>2</FluentButton>
        <FluentButton>3</FluentButton>
    </div>
    <FluentTab Id="tab-v1" Label="Tab one">
        Tab one content. This is for testing.
    </FluentTab>
    <FluentTab Id="tab-v2" Label="Tab two">
        Tab two content. This is for testing.
    </FluentTab>
    <FluentTab Id="tab-v3" Label="Tab three">
        Tab three content. This is for testing.
    </FluentTab>
</FluentTabs>

```

--------------------------------

### Default and Disabled Checkbox Examples in Blazor

Source: https://www.fluentui-blazor.net/Checkbox

Demonstrates the basic usage of Fluent Blazor Checkbox for horizontal and vertical layouts. Includes an example of a disabled checkbox. Requires Fluent UI Blazor component library.

```Razor
<h4>Horizontal</h4>
<FluentStack>
    <FluentCheckbox @bind-Value="@value1" Label="Apples" />
    <FluentCheckbox @bind-Value="@value2" Disabled="true" Label="Bananas (disabled)" />
    <FluentCheckbox @bind-Value="@value3" Label="Oranges" />
</FluentStack>

<br />
<br />

<h4>Vertical</h4>
<FluentStack Orientation="Orientation.Vertical">
    <FluentCheckbox @bind-Value="@value1">Apples</FluentCheckbox>
    <FluentCheckbox @bind-Value="@value2" Disabled="true">Bananas (disabled)</FluentCheckbox>
    <FluentCheckbox @bind-Value="@value3">Oranges</FluentCheckbox>
</FluentStack>

@code {
    bool value1 = true;
    bool value2 = true;
    bool value3;
}
```

--------------------------------

### FluentAppBar with Search and Orientation Control (Razor)

Source: https://www.fluentui-blazor.net/AppBar

This Razor component demonstrates the FluentAppBar with a search popover and adjustable orientation (vertical/horizontal). It utilizes FluentStack and FluentSwitch components for layout and user interaction. The code dynamically sets styles based on the orientation and includes various FluentAppBarItems for navigation.

```Razor
@{
    string stylevalue = $"background-color: var(--neutral-layer-3); overflow: auto; resize: {(_vertical ? \"vertical; width: 86px; height: 320px;\" : \"horizontal;width: 440px; height: 68px;\")}  padding: 10px;";
}
<FluentStack Orientation="Orientation.Vertical" Style="height: 100%;">
    <FluentStack Orientation="Orientation.Horizontal">
    <FluentSwitch @bind-Value="_showSearch" CheckedMessage="Show" UncheckedMessage="Hide" Label="Show search in popover" />
    <FluentSwitch @bind-Value="_vertical" @bind-Value:after="@HandleOrientationChanged" CheckedMessage="Vertical" UncheckedMessage="Horizontal" Label="Orientation" />
    </FluentStack>
    <div style="@stylevalue">
        @{
            var wh = _vertical ? "height: 100%; width: 100%;" : "";
        }
        <FluentAppBar Orientation="@(_vertical ? Orientation.Vertical : Orientation.Horizontal)" Style="@(" background-color: var(--neutral-layer-2);")" PopoverVisibilityChanged="HandlePopover" PopoverShowSearch="@_showSearch">

            <FluentAppBarItem Href="/AppBarDefault"
                                Match="NavLinkMatch.All"
                                IconRest="ResourcesIcon()"
                                IconActive="ResourcesIcon(active: true)"
                                Text="Resources" />
            <FluentAppBarItem Href="/AppBar"
                                IconRest="ConsoleLogsIcon()"
                                IconActive="ConsoleLogsIcon(active: true)"
                                Text="Console Logs" />
            <FluentAppBarItem Href="/StructuredLogs"
                                IconRest="StructuredLogsIcon()"
                                IconActive="StructuredLogsIcon(active: true)"
                                Text="Logs"
                                Tooltip="Structured Logs"
                                Count="4"/>
            <FluentAppBarItem Href="/Traces"
                                IconRest="TracesIcon()"
                                IconActive="TracesIcon(active: true)"
                                Text="Traces" />
            <FluentAppBarItem Href="/Metrics"
                                IconRest="MetricsIcon()"
                                IconActive="MetricsIcon(active: true)"
                                Text="Metrics" />
            <FluentAppBarItem Href="/AppBarPage"
                                IconRest="ResourcesIcon()"
                                IconActive="ResourcesIcon(active: true)"
                                Text="Resources 2" />
            <FluentAppBarItem Href="/AppBar"
                                IconRest="ConsoleLogsIcon()"
                                IconActive="ConsoleLogsIcon(active: true)"
                                Text="Console Logs 2" />
            <FluentAppBarItem Href="/StructuredLogs"
                                IconRest="StructuredLogsIcon()"
                                IconActive="StructuredLogsIcon(active: true)"
                                Text="Structured Logs 2" />
            <FluentAppBarItem Href="/Traces"
                                IconRest="TracesIcon()"
                                IconActive="TracesIcon(active: true)"
                                Text="Traces 2" />
            <FluentAppBarItem Href="/Metrics"
                                IconRest="MetricsIcon()"
                                IconActive="MetricsIcon(active: true)"
                                Text="Metrics 2" />
            <FluentAppBarItem Href="/AppBarPage"
                                IconRest="ResourcesIcon()"
                                IconActive="ResourcesIcon(active: true)"
                                Text="Resources 3" />
            <FluentAppBarItem Href="/AppBar"
                                IconRest="ConsoleLogsIcon()"
                                IconActive="ConsoleLogsIcon(active: true)"
                                Text="Console Logs 3" />
            <FluentAppBarItem Href="/StructuredLogs"
                                IconRest="StructuredLogsIcon()"
                                IconActive="StructuredLogsIcon(active: true)"
                                Text="Structured Logs 3" />
            <FluentAppBarItem Href="/Traces"
                                IconRest="TracesIcon()"
                                IconActive="TracesIcon(active: true)"
                                Text="Traces 3" />
            <FluentAppBarItem Href="/Metrics"
                                IconRest="MetricsIcon()"
                                IconActive="MetricsIcon(active: true)"
                                Text="Metrics 3" />
        </FluentAppBar>
    </div>
</FluentStack>

@code {
    private bool _vertical = true;
    private bool _showSearch = true;

    private static Icon ResourcesIcon(bool active = false) =>

```

--------------------------------

### Fluent UI Blazor Overflow: VisibleOnLoad=false

Source: https://www.fluentui-blazor.net/Overflow

This example demonstrates using the `VisibleOnLoad` parameter set to false for the FluentOverflow component. This hides overflow indicators initially, requiring the user to interact or resize to see them, useful for dynamic content loading.

```Razor
<div style="background-color: var(--neutral-layer-4); overflow: auto; resize: horizontal; padding: 10px;">
    <FluentOverflow Style="width: 100%;" VisibleOnLoad="false">
        <FluentOverflowItem><FluentBadge>Blazor</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Microsoft</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Azure</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>DevOps</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Framework</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Office</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Installation</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Blazor</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Microsoft</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Azure</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>DevOps</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Framework</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Office</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Installation</FluentBadge></FluentOverflowItem>
    </FluentOverflow>
</div>
```

--------------------------------

### Fluent UI Blazor Overflow: Basic Usage with Badges

Source: https://www.fluentui-blazor.net/Overflow

This example demonstrates the basic usage of the FluentOverflow component with FluentBadge items. When the container width is insufficient, an ellipsis will appear, indicating overflow. The first element is always displayed.

```Razor
<div style="background-color: var(--neutral-layer-4); overflow: auto; resize: horizontal; padding: 10px;">
    <FluentOverflow Style="width: 100%;">
        <FluentOverflowItem><FluentBadge>Blazor</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Microsoft</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Azure</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>DevOps</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Framework</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Office</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Installation</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Blazor</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Microsoft</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Azure</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>DevOps</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Framework</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Office</FluentBadge></FluentOverflowItem>
        <FluentOverflowItem><FluentBadge>Installation</FluentBadge></FluentOverflowItem>
    </FluentOverflow>
</div>
```

--------------------------------

### Fluent UI Blazor Overflow: Fixed Ellipsis Item

Source: https://www.fluentui-blazor.net/Overflow

This example shows how to configure a specific FluentOverflowItem to display an ellipsis (...) when overflow occurs, using the `FixedOverflowItem.Ellipsis` property. Other items will wrap or be hidden as needed.

```Razor
<div style="background-color: var(--neutral-layer-4); overflow: auto; resize: horizontal; padding: 10px;">
    <FluentOverflow Style="width: 100%;">
        <FluentOverflowItem Fixed="OverflowItemFixed.Ellipsis">Aspire;</FluentOverflowItem>
        <FluentOverflowItem>Blazor;</FluentOverflowItem>
        <FluentOverflowItem>Microsoft;</FluentOverflowItem>
        <FluentOverflowItem>Azure;</FluentOverflowItem>
        <FluentOverflowItem>DevOps</FluentOverflowItem>
    </FluentOverflow>
</div>
```

--------------------------------

### Blazor Sortable List with Disabled Sorting and Dropping

Source: https://www.fluentui-blazor.net/SortableList

This example demonstrates how to configure Fluent UI Blazor Sortable Lists to disable sorting within a list and prevent items from being dropped onto another list. The `Sort="false"` parameter disables internal sorting, and `Drop="false"` on a specific list prevents items from being accepted by it. This setup is useful for unidirectional data flow or controlled list interactions.

```Razor
// To disable sorting within a list, set the Sort parameter to false:
// <FluentSortableList Sort="false" ...>

// To disable dropping items onto a list, set the Drop parameter to false:
// <FluentSortableList Drop="false" ...>

// Example demonstrating unidirectional drag-and-drop:
// List 1 allows sorting and dropping.
// List 2 allows sorting but not dropping from List 1.
// This would require separate Razor markup or logic to enforce.
// The provided text describes the concept but not a direct code snippet for this specific scenario.
// For illustration, consider the conceptual application:

/*
<FluentGrid Justify="JustifyContent.FlexStart" Spacing="3">
    <FluentGridItem xs="12" sm="6">
        <h5>List 1 (Sortable, Droppable)</h5>
        <FluentSortableList Id="list1" Group="mygroup" OnUpdate="@SortList1" Items="@items1" Context="item">
            <ItemTemplate>@item.Name</ItemTemplate>
        </FluentSortableList>
    </FluentGridItem>
    <FluentGridItem xs="12" sm="6">
        <h5>List 2 (Sortable, Not Droppable from List 1)</h5>
        <FluentSortableList Id="list2" Group="mygroup" Sort="true" Drop="false" OnUpdate="@SortList2" Items="@items2" Context="item">
            <ItemTemplate>@item.Name</ItemTemplate>
        </FluentSortableList>
    </FluentGridItem>
</FluentGrid>

@code {
    // ... (Item class and item list definitions as before) ...

    // ... (SortList1 and SortList2 methods, potentially modified if Drop="false" is used) ...

    // Note: When Drop="false" on a target list, the OnRemove event on the source list
    // will still fire if the drag operation is initiated, but the item won't be added
    // to the target list. The source list's state remains unchanged in this case.
}
*/

```

--------------------------------

### Fluent UI Blazor DatePicker and TimePicker Examples

Source: https://www.fluentui-blazor.net/DateTime

Provides various examples of using FluentDatePicker and FluentTimePicker components in Blazor. It showcases default usage with @bind-Value, and demonstrates manual binding with conversion methods for different nullable and non-nullable DateTime, DateOnly, and TimeOnly types.

```razor
<!-- Default usage -->
<FluentDatePicker @bind-Value="@MyDate0" />
<FluentTimePicker @bind-Value="@MyTime0" />
<b>Date:</b> @(MyDate0?.ToString("yyyy-MM-dd"))
<b>Time:</b> @(MyTime0?.ToString("HH:mm"))
<br />

<!-- Using conversion methods -->
<FluentDatePicker Value="@MyDate1" ValueChanged="@(e => MyDate1 = e.ToDateTime())" />
<FluentTimePicker Value="@MyTime1" ValueChanged="@(e => MyTime1 = e.ToDateTime())" />
<b>Date:</b> @(MyDate1.ToString("yyyy-MM-dd"))
<b>Time:</b> @(MyTime1.ToString("HH:mm"))
<br />

<FluentDatePicker Value="@MyDate2.ToDateTimeNullable()" ValueChanged="@(e => MyDate2 = e.ToDateOnlyNullable())" />
<FluentTimePicker Value="@MyTime2.ToDateTimeNullable()" ValueChanged="@(e => MyTime2 = e.ToTimeOnlyNullable())" />
<b>Date:</b> @(MyDate2?.ToString("yyyy-MM-dd"))
<b>Time:</b> @(MyTime2?.ToString("HH:mm"))
<br />

<FluentDatePicker Value="@MyDate3.ToDateTime()" ValueChanged="@(e => MyDate3 = e.ToDateOnly())" />
<FluentTimePicker Value="@MyTime3.ToDateTime()" ValueChanged="@(e => MyTime3 = e.ToTimeOnly())" />
<b>Date:</b> @(MyDate3.ToString("yyyy-MM-dd"))
<b>Time:</b> @(MyTime3.ToString("HH:mm"))
<br />

@code
{
    private DateTime? MyDate0 = DateTime.Now;
    private DateTime MyDate1 = DateTime.Now;
    private DateOnly? MyDate2 = DateOnly.FromDateTime(DateTime.Now);
    private DateOnly MyDate3 = DateOnly.FromDateTime(DateTime.Now);

    private DateTime? MyTime0 = DateTime.Now;
    private DateTime MyTime1 = DateTime.Now;
    private TimeOnly? MyTime2 = TimeOnly.FromDateTime(DateTime.Now);
    private TimeOnly MyTime3 = TimeOnly.FromDateTime(DateTime.Now);
}
```

--------------------------------

### Blazor Hybrid WebView Script Setup for Fluent UI

Source: https://www.fluentui-blazor.net/BlazorHybrid

This code snippet demonstrates the necessary script tags to include Fluent UI's `initializersLoader.webview.js` before `blazor.webview.js` in Blazor Hybrid applications. This workaround addresses issues with automatic web-components script import and custom event handler loading in WebView environments for .NET 8 and below.

```html
<script app-name="{NAME OF YOUR APP}" src="./_content/Microsoft.FluentUI.AspNetCore.Components/js/initializersLoader.webview.js"></script>
<script src="_framework/blazor.webview.js"></script>
```

--------------------------------

### FluentNavMenu Component

Source: https://www.fluentui-blazor.net/NavMenu

Documentation for the FluentNavMenu component, including its parameters and event callbacks.

```APIDOC
## FluentNavMenu Component

### Description
The `FluentNavMenu` component provides a navigation structure with options for databinding, sub-items, and icons.

### Parameters
#### Path Parameters
None

#### Query Parameters
None

#### Request Body
None

### Parameters
#### Path Parameters
None

#### Query Parameters
None

#### Request Body
None

### Parameters

- **ChildContent** (RenderFragment?) - Description: Slot for the navigation menu's content.
- **CollapsedChildNavigation** (bool) - Default: False - Description: Gets or sets whether a menu with all child links is shown for `FluentNavGroup`s when the navigation menu is collapsed.
- **Collapsible** (bool) - Default: False - Description: Gets or sets whether or not the menu can be collapsed.
- **CustomToggle** (bool) - Default: False - Description: Gets or sets if a custom toggle for showing/hiding the menu is used. This is primarily intended to be used in a mobile view.
- **Expanded** (bool) - Default: True - Description: Controls the expanded state of the navigation menu.
- **ExpanderContent** (RenderFragment?) - Description: Gets or sets the content to be rendered for the collapse icon when the menu is collapsible. The default icon will be used if this is not specified.
- **Margin** (string?) - Description: Adjust the vertical spacing between navlinks.
- **Title** (string?) - Default: 'Navigation menu' - Description: Gets or sets the title of the navigation menu using the aria-label attribute.
- **Width** (int?) - Description: Gets or sets the width of the menu (in pixels).

### EventCallbacks
#### Success Response (200)
- **ExpandedChanged** (EventCallback<bool>) - Description: Event callback for when the `FluentNavMenu.Expanded` property changes.

### Request Example
```razor
<FluentNavMenu @bind-Expanded=MenuExpanded Title="Databound demo">
    <FluentNavGroup Id="Group1" Title="Item 1" @bind-Expanded=Item1Expanded Icon="@(new Icons.Regular.Size24.LeafOne())">
        <FluentNavLink Icon="@(new Icons.Regular.Size24.LeafOne())">Item 1.1</FluentNavLink>
        <FluentNavLink Icon="@(new Icons.Regular.Size24.LeafTwo())">Item 1.2</FluentNavLink>
    </FluentNavGroup>
    <FluentNavGroup Id="Group2" Title="Item 2" @bind-Expanded=Item2Expanded Icon="@(new Icons.Regular.Size24.LeafTwo())">
        <FluentNavLink Icon="@(new Icons.Regular.Size24.LeafOne())">Item 2.1</FluentNavLink>
        <FluentNavLink Icon="@(new Icons.Regular.Size24.LeafTwo())">Item 2.2</FluentNavLink>
    </FluentNavGroup>
</FluentNavMenu>

@code {
    bool MenuExpanded = true;
    bool Item1Expanded = true;
    bool Item2Expanded = true;
}
```

### Response
#### Success Response (200)
None

#### Response Example
None
```

--------------------------------

### Fluent UI Blazor Wizard: Default Configuration and Step Control

Source: https://www.fluentui-blazor.net/Wizard

Demonstrates the basic setup of a Fluent UI Blazor Wizard with default step positioning and sequence options. Includes controls for changing step position (Top/Left) and wizard step sequence type.

```Razor
@inject IDialogService DialogService

<FluentStack VerticalAlignment="VerticalAlignment.Center">
    <FluentSwitch @bind-Value="@IsTop"
                  Style="margin: 30px;"
                  Label="Step position"
                  UncheckedMessage="Left"
                  CheckedMessage="Top" />

    WizardStepSequence:
    <FluentSelect Width="150px"
                  Items="@(Enum.GetValues<WizardStepSequence>())"
                  @bind-SelectedOption="@StepSequence" />
</FluentStack>

<FluentWizard StepperPosition="@(IsTop ? StepperPosition.Top : StepperPosition.Left)"
              StepSequence="@StepSequence"
              DisplayStepNumber="@(WizardStepStatus.Current | WizardStepStatus.Next)"
              Border="WizardBorder.Outside"
              StepTitleHiddenWhen="@GridItemHidden.XsAndDown"
              Height="300px"
              OnFinish="@OnFinishedAsync">
    <Steps>
        <FluentWizardStep Label="Intro"
                          OnChange="@OnStepChange">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ut nisi eget dolor semper
            luctus vitae a nulla. Cras semper eros sed lacinia tincidunt. Mauris dignissim ullamcorper dolor,
            ut blandit dui ullamcorper faucibus. Interdum et malesuada fames ac ante ipsum.
        </FluentWizardStep>
        <FluentWizardStep Label="Get started"
                          Summary="Begin the tasks"
                          OnChange="@OnStepChange">
            Maecenas sed justo ac sapien venenatis ullamcorper. Sed maximus nunc non venenatis euismod.
            Fusce vel porta ex, imperdiet molestie nisl. Vestibulum eu ultricies mauris, eget aliquam quam.
        </FluentWizardStep>
        <FluentWizardStep Disabled="true"
                          Label="Disabled step"
                          Summary="This step is disabled"
                          OnChange="@OnStepChange">
            Nunc dignissim tortor eget lacus porta tristique. Nunc in posuere dui. Cras ligula ex,
            ullamcorper in gravida in, euismod vitae purus. Lorem ipsum dolor sit amet, consectetur
            adipiscing elit. Aliquam at velit leo. Suspendisse potenti. Cras dictum eu augue in laoreet.
        </FluentWizardStep>
        <FluentWizardStep Label="Set budget"
                          Summary="Identify the best price"
                          IconPrevious="@(new Icons.Filled.Size24.Star())"
                          IconCurrent="@(new Icons.Filled.Size24.StarEmphasis())"
                          IconNext="@(new Icons.Regular.Size24.Star())"
                          DisplayStepNumber="false"
                          OnChange="@OnStepChange">
            Phasellus quis augue convallis, congue velit ac, aliquam ex. In egestas porttitor massa
            aliquet porttitor. Donec bibendum faucibus urna vitae elementum. Phasellus vitae efficitur
            turpis, eget molestie ipsum.
            <FluentSelect Items="@(Enumerable.Range(10, 80).Select(i => i.ToString()))"
                          Style="min-width: 70px;"
                          Height="300px" />
        </FluentWizardStep>
        <FluentWizardStep Label="Summary"
                          OnChange="@OnStepChange">
            Ut iaculis sed magna efficitur tempor. Vestibulum est erat, imperdiet in diam ac,

```

--------------------------------

### Update FluentIcon Component Syntax

Source: https://www.fluentui-blazor.net/UpgradeGuide

Demonstrates the new syntax for using the `FluentIcon` component. Icons are now specified using a structured `Icons.[IconVariant].[IconSize].[IconName]()` format.

```razor
<FluentIcon Value="@(new @(Icons.Regular.Size24.Save)())" />
```

--------------------------------

### Configure RTL Direction with FluentDesignTheme

Source: https://www.fluentui-blazor.net/CodeSetup

Configure the design theme for Right-To-Left languages using the Direction design token and FluentDesignTheme component in a Blazor layout.

```razor
@* MainRtlLayout.razor *@

@using Microsoft.FluentUI.AspNetCore.Components.DesignTokens
@inject Direction DirectionDesignToken
@inherits LayoutComponentBase
...
@Body
...
<FluentDesignTheme Direction="@Direction" />
@code {
    LocalizationDirection Direction { get; set; }
    protected override async Task OnAfterRenderAsync(bool f)
    {
        await base.OnAfterRenderAsync(f);
        if(!f)
            return;
        await DirectionDesignToken.WithDefault("rtl");
        Direction = LocalizationDirection.RightToLeft;
        StateHasChanged();
    }
}
```

--------------------------------

### Automate FluentIcon Component Renaming (Find and Replace)

Source: https://www.fluentui-blazor.net/UpgradeGuide

Provides regular expressions for Visual Studio's Find and Replace functionality to automate the migration of older `<FluentIcon>` component syntax to the new format. This requires backing up the project before execution.

```regex
Search: <FluentIcon Name="?@?FluentIcons\.(?<name>[^"]+)"? Size="?@?IconSize\.(?<size>[^"]+)"? Variant="?@?IconVariant\.(?<variant>[^"]+)"? Color="?@?Color\.(?<color>[^"]+)"? Slot="?(?<slot>[^"]+)"? />
Replace by: <FluentIcon Value="@(new Icons.${variant}.${size}.${name}())" Color="@Color.${color}" Slot="${slot}" />
```

--------------------------------

### Fluent UI Blazor Circular Badge Examples

Source: https://www.fluentui-blazor.net/Badge

Illustrates how to render the FluentBadge component in a circular shape using the 'Circular' property set to true. Two examples show circular badges with Accent and Neutral appearances.

```Razor
<FluentBadge Circular=true Appearance="Appearance.Accent">
    Circular 
</FluentBadge>

<FluentBadge Circular=true Appearance="Appearance.Neutral">
    Circular
</FluentBadge>
```

--------------------------------

### FluentStack Default Layout Example (Razor)

Source: https://www.fluentui-blazor.net/Stack

Demonstrates the basic usage of the FluentStack component with default settings. It renders child divs with a dotted border to visualize the stack's boundaries.

```Razor
@namespace FluentUI.Demo.Shared

<FluentStack Style="border: 1px dotted var(--accent-fill-rest)">
    <div class="demopanel">This is some content displayed in a div</div>
    <div class="demopanel">This is some content displayed in a div</div>
</FluentStack>
```

--------------------------------

### Fluent UI Blazor Grid with Breakpoints and Spacing

Source: https://www.fluentui-blazor.net/Grid

This example demonstrates the Fluent UI Blazor Grid component with various breakpoint configurations (xs, sm) and spacing options. It includes interactive controls for JustifyContent and Spacing, along with event handling for breakpoint changes. The component adapts its layout based on screen size, making it suitable for responsive web design.

```Razor
<FluentStack Style="margin-bottom: 24px;" VerticalAlignment="VerticalAlignment.Center">
    <FluentLabel>Justify</FluentLabel>
    <FluentSelect Items=@(Enum.GetValues<JustifyContent>()) 
                  OptionValue="@(c => c.ToAttributeValue())" 
                  TOption="JustifyContent" 
                  Position="SelectPosition.Below" 
                  @bind-SelectedOption="@Justification" />

    <FluentLabel>Spacing</FluentLabel>
    <FluentSlider @bind-Value="@Spacing" Min="0" Max="10" Step="1" Style="max-width: 300px; margin-top: 18px;" />
</FluentStack>

<FluentGrid Spacing="@Spacing" OnBreakpointEnter="@OnBreakpointEnterHandler" AdaptiveRendering="true" Justify="@Justification" Style="background-color: var(--neutral-layer-3); padding: 4px; ">
    <FluentGridItem xs="12">
        <div class="card">
            xs="12"
        </div>
    </FluentGridItem>
    <FluentGridItem xs="12" sm="6" HiddenWhen="GridItemHidden.SmAndDown">
        <div class="card">
            xs="12" sm="6" Hidden="SmAndDown"
        </div>
    </FluentGridItem>
    <FluentGridItem xs="12" sm="6">
        <div class="card">
            xs="12" sm="6"
        </div>
    </FluentGridItem>
    <FluentGridItem xs="6" sm="3">
        <div class="card">
            xs="6" sm="3"
        </div>
    </FluentGridItem>
    <FluentGridItem xs="6" sm="3">
        <div class="card">
            xs="6" sm="3"
        </div>
    </FluentGridItem>
    <FluentGridItem xs="6" sm="3">
        <div class="card">
            xs="6" sm="3"
        </div>
    </FluentGridItem>
    <FluentGridItem xs="6" sm="3">
        <div class="card">
            xs="6" sm="3"
        </div>
    </FluentGridItem>
    <FluentGridItem xs="3">
        <div class="card">
            xs="3"
        </div>
    </FluentGridItem>
</FluentGrid>

@code
{
    JustifyContent Justification = JustifyContent.FlexStart;
    int Spacing = 3;

    void OnBreakpointEnterHandler(GridItemSize size)
    {
        DemoLogger.WriteLine($"Page Size: {size}");
    }
}
```

--------------------------------

### Render Fluent UI Blazor DataGrid with Sortable Columns and Pagination

Source: https://www.fluentui-blazor.net/datagrid-get-started

This Razor component code snippet demonstrates how to render a Fluent DataGrid with sortable columns and two synchronized paginators. It defines a 'Person' record, populates the grid with sample data, and configures pagination with 2 items per page. Dependencies include the Fluent UI Blazor library.

```Razor
<FluentPaginator State="@pagination" SummaryTemplate="@template" />

<FluentDataGrid Items="@people" Pagination="@pagination">
    <PropertyColumn Property="@(p => p.PersonId)" Sortable="true" />
    <PropertyColumn Property="@(p => p.Name)" Sortable="true" />
    <PropertyColumn Property="@(p => p.BirthDate)" Format="yyyy-MM-dd" Sortable="true" />
</FluentDataGrid>

<FluentPaginator State="@pagination" />

@code {
    PaginationState pagination = new PaginationState() { ItemsPerPage = 2 };

    record Person(int PersonId, string Name, DateOnly BirthDate);

    IQueryable<Person> people = new[]
    {
        new Person(10895, "Jean Martin", new DateOnly(1985, 3, 16)),
        new Person(10944, "António Langa", new DateOnly(1991, 12, 1)),
        new Person(11203, "Julie Smith", new DateOnly(1958, 10, 10)),
        new Person(11205, "Nur Sari", new DateOnly(1922, 4, 27)),
        new Person(11898, "Jose Hernandez", new DateOnly(2011, 5, 3)),
        new Person(12130, "Kenji Sato", new DateOnly(2004, 1, 9)),
    }.AsQueryable();

    private RenderFragment template = @<span />;
}

```

--------------------------------

### FluentTextArea Basic Usage and States in Razor

Source: https://www.fluentui-blazor.net/TextArea

Demonstrates the usage of the FluentTextArea component in Razor, showcasing default appearance, placeholders, required fields, disabled states, and read-only configurations. It includes examples of setting labels and initial values through `@bind-Value`.

```Razor
<h4>Default</h4>
<FluentTextArea @bind-Value=value1 Appearance="FluentInputAppearance.Filled"></FluentTextArea>
<FluentTextArea @bind-Value=value2 Appearance="FluentInputAppearance.Filled">
    <span>label</span>
</FluentTextArea>

<h4>Placeholder</h4>
<FluentTextArea @bind-Value=value3 Appearance="FluentInputAppearance.Filled" Placeholder="Placeholder"></FluentTextArea>

<!-- Required -->
<h4>Required</h4>
<FluentTextArea @bind-Value=value4 Appearance="FluentInputAppearance.Filled" Required="true"></FluentTextArea>

<!-- Disabled -->
<h4>Disabled</h4>
<FluentTextArea @bind-Value=value5 Appearance="FluentInputAppearance.Filled" Disabled="true"></FluentTextArea>
<FluentTextArea @bind-Value=value6 Appearance="FluentInputAppearance.Filled" Disabled="true">
    <span>label</span>
</FluentTextArea>
<FluentTextArea @bind-Value=value7 Appearance="FluentInputAppearance.Filled" Disabled="true" Placeholder="placeholder"></FluentTextArea>

<!-- Read only -->
<h4>Read only</h4>
<FluentTextArea @bind-Value=value8 Appearance="FluentInputAppearance.Filled" ReadOnly="true">
    label
</FluentTextArea>
<FluentTextArea @bind-Value=value9 Appearance="FluentInputAppearance.Filled" ReadOnly="true">
    label
</FluentTextArea>
@code {
    string? value1, value2, value3, value4, value5, value6, value7, value8="Read only text area", value9 = "Read only text area";
}
```

--------------------------------

### Render FluentEmoji Component

Source: https://www.fluentui-blazor.net/IconsAndEmoji

Example of rendering a specific emoji using the `FluentEmoji` component. It demonstrates how to instantiate an emoji object and pass it to the `Value` property.

```razor
<FluentEmoji Value="@(new Emojis.PeopleBody.Color.Default.Artist())" />
```

--------------------------------

### Log Popover Visibility Change (C#)

Source: https://www.fluentui-blazor.net/AppBar

This method logs the visibility change of a popover to the console. It takes a boolean 'visible' parameter and uses 'DemoLogger.WriteLine' to output the status. No specific dependencies are required beyond the 'DemoLogger' class.

```csharp
private void HandlePopover(bool visible) => DemoLogger.WriteLine($"Popover visibility changed to {visible}");
```

--------------------------------

### Fluent Blazor Text Field: Input Types

Source: https://www.fluentui-blazor.net/TextField

Provides examples of using the FluentTextField component with various input types, such as password, email, telephone, URL, color picker, and search. This allows for specialized input handling and validation. Examples are in Razor syntax.

```Razor
<!-- Password -->
<FluentTextField @bind-Value=password Type="TextFieldType.Password" Placeholder="Password"></FluentTextField>

<!-- Email (with spellcheck) -->
<FluentTextField @bind-Value=email Type="TextFieldType.Email" Placeholder="Email (with spellcheck)"></FluentTextField>

<!-- Telephone number -->
<FluentTextField @bind-Value=phone Type="TextFieldType.Telephone" Placeholder="Telephone"></FluentTextField>

<!-- Url -->
<FluentTextField @bind-Value=url Type="TextFieldType.Url" Placeholder="Url"></FluentTextField>

<!-- Text with InputMode -->
<FluentTextField @bind-Value=textWithInputMode InputMode="InputMode.numeric" Placeholder="Text with InputMode"></FluentTextField>

<!-- Color picker -->
<FluentTextField @bind-Value=color Type="TextFieldType.Color" Placeholder="Color picker"></FluentTextField>

<!-- Search input -->
<FluentTextField @bind-Value=search Type="TextFieldType.Search" Placeholder="Search"></FluentTextField>

@code {
    string? password, email, phone, url, textWithInputMode, color, search;
}
```

--------------------------------

### FluentLabel Component: Color Styling Examples

Source: https://www.fluentui-blazor.net/Label

Illustrates how to apply colors to the FluentLabel component using predefined Color enum values, a custom color string via CustomColor, or a CSS style string via the Style parameter. The Style parameter takes precedence over CustomColor.

```Razor
<FluentLabel Typo="Typography.Header" Color="@Color.Warning"> A 'Header' using Color.Warning</FluentLabel>
<FluentLabel Typo="Typography.Body" Color="@Color.Disabled"> A 'Body' label using Color.Disabled</FluentLabel>
<FluentLabel Typo="Typography.Body" Color="@Color.Custom" CustomColor="deepskyblue"> A 'Body' label using a custom color through the CustomColor parameter. Just specify a valid color string value.</FluentLabel>
<FluentLabel Typo="Typography.Body" Style="color: chocolate;"> A 'Body' label using a custom color through the Style parameter. In this case a valid CSS color specification needs to be provided.</FluentLabel>
<FluentLabel Typo="Typography.Body" Style="color: burlywood;" Color="@Color.Custom" CustomColor="burlywood"> When specifying both <code>CustomColor</code> and <code>Style</code>, the latter wins.</FluentLabel>
```

--------------------------------

### Handle Dialog Result Callback (C#)

Source: https://www.fluentui-blazor.net/DialogService

Provides an example implementation of a method (HandleIt) to process the DialogResult received from a dialog. It demonstrates checking for cancellation and accessing data returned by the dialog.

```csharp
private async Task HandleIt(DialogResult result) {
    if (result.Cancelled) {
        //Handle the cancellation
        return;
    }
    if (result.Data is not null) {
        //Handle the data
    }
    //Handle closing the dialog
    await Task.Run(() => ...);
}
```

--------------------------------

### FluentTextArea Default Example - Razor

Source: https://www.fluentui-blazor.net/TextArea

Demonstrates the default usage of the FluentTextArea component with and without a label. It utilizes Razor syntax and binds the textarea's value to a string variable.

```Razor
<p>Without label: <FluentTextArea @bind-Value=value1 AriaLabel="Without label"></FluentTextArea></p>
<p>
    <FluentTextArea @bind-Value=value2 Label="With label:" />
</p>
@code {
    string? value1, value2;
}
```

--------------------------------

### Blazor Slider: Vertical Orientation Example

Source: https://www.fluentui-blazor.net/Slider

A basic example of the Fluent Blazor Slider component configured for vertical orientation. It includes labels and sets specific height and width via inline styles. The component binds to an integer value.

```Razor
<FluentSlider Orientation="Orientation.Vertical" Min="0" Max="100" Step="10" @bind-Value=value style="height: 300px; width: 50px;">
    <FluentSliderLabel Position="0">0&#8451;</FluentSliderLabel>
    <FluentSliderLabel Position="10">10&#8451;</FluentSliderLabel>
    <FluentSliderLabel Position="90">90&#8451;</FluentSliderLabel>
    <FluentSliderLabel Position="100">100&#8451;</FluentSliderLabel>
</FluentSlider>
@code {
    int value=50;
}
```

--------------------------------

### FluentSelect with Option<T> Items in Razor

Source: https://www.fluentui-blazor.net/Select

Illustrates using FluentSelect with lists of Option<T> items. The first example shows how to pre-select an item using an OptionSelected delegate with Option<string>. The second example demonstrates disabling an item using OptionDisabled with Option<int>.

```Razor
<!-- Example with Option<string> -->
<FluentSelect Label="Select a number"
              Items="@new List<Option<string>> { new("One", "1"), new("Two", "2"), new("Three", "3") }"
              OptionSelected="@((opt) => SelectedStringValue = opt.Value)"
              @bind-Value="@SelectedStringValue" />
<p>Selected Value: @SelectedStringValue</p>

<!-- Example with Option<int> -->
<FluentSelect Label="Select an item"
              Items="@new List<Option<int>> { new("First", 1, disabled: true), new("Second", 2), new("Third", 3) }"
              OptionDisabled="@((opt) => opt.Value == 1)"
              @bind-Value="@SelectedIntValue" />
<p>Selected Value: @SelectedIntValue</p>
<p>Selected Item (strongly typed): @SelectedIntItem?.Text</p>
<p>Value: (@SelectedIntItem?.Value) (Type: @SelectedIntItem?.Value.GetType().Name)</p>

@code {
    string? SelectedStringValue;
    Option<int>? SelectedIntItem;
    int? SelectedIntValue;
}
```

--------------------------------

### Remove Reboot CSS Import from app.css

Source: https://www.fluentui-blazor.net/CodeSetup

Remove the import statement for the Fluent UI reboot CSS from the app.css file to avoid conflicts.

```css
@import '_content/Microsoft.FluentUI.AspNetCore.Components/css/reboot.css';
```

--------------------------------

### Fluent Blazor AppBar with Ordered App List and Search

Source: https://www.fluentui-blazor.net/AppBar

This Razor component demonstrates the Fluent Blazor AppBar. It defines a custom 'FluentCustomAppBarItem' class with an 'Order' property to manage the display sequence of applications. The AppBar is configured to use this custom item and allows toggling a search input within its popover.

```razor
<FluentStack Orientation="Orientation.Vertical" Style="height: 100%;">
    <FluentSwitch @bind-Value="_showSearch" CheckedMessage="Show" UncheckedMessage="Hide" Label="Show search in popover" />
    <div style="background-color: var(--neutral-layer-3); overflow: auto; resize: vertical; height: 356px; width: 500px; padding: 10px;">
        <FluentAppBar
                      Items="@_apps.OrderBy(a => a.Order)"
                      Style="width: 68px; height: 100%; background-color: var(--neutral-layer-2);"
                      PopoverVisibilityChanged="HandlePopover"
                      PopoverShowSearch="@_showSearch">
        </FluentAppBar>
    </div>
</FluentStack>


@code {
    private class FluentCustomAppBarItem : FluentAppBarItem
    {
        public int Order { get; set; }
    }

    private List<FluentCustomAppBarItem> _apps => new List<FluentCustomAppBarItem>
    {
        new FluentCustomAppBarItem { Order = 15, IconRest = ResourcesIcon(), IconActive = ResourcesIcon(active: true), Text = "Aaaaa", Href = "/AppBarDefault" },
        new FluentCustomAppBarItem { Order = 14, IconRest = ResourcesIcon(), IconActive = ResourcesIcon(active: true), Text = "Bbbbb", Href = "/AppBar" },
        new FluentCustomAppBarItem { Order = 13, IconRest = ResourcesIcon(), IconActive = ResourcesIcon(active: true), Text = "Ccccc" },
        new FluentCustomAppBarItem { Order = 12, IconRest = ConsoleLogsIcon(), IconActive = ConsoleLogsIcon(active: true), Text = "Ddddd" },
        new FluentCustomAppBarItem { Order = 11, IconRest = ConsoleLogsIcon(), IconActive = ConsoleLogsIcon(active: true), Text = "Eeeee" },
        new FluentCustomAppBarItem { Order = 10, IconRest = ConsoleLogsIcon(), IconActive = ConsoleLogsIcon(active: true), Text = "Fffff" },
        new FluentCustomAppBarItem { Order = 9, IconRest = StructuredLogsIcon(), IconActive = StructuredLogsIcon(active: true), Text = "Ggggg", Count = 4 },
        new FluentCustomAppBarItem { Order = 8, IconRest = StructuredLogsIcon(), IconActive = StructuredLogsIcon(active: true), Text = "Hhhhh" },
        new FluentCustomAppBarItem { Order = 7, IconRest = StructuredLogsIcon(), IconActive = StructuredLogsIcon(active: true), Text = "Iiiii" },
        new FluentCustomAppBarItem { Order = 6, IconRest = TracesIcon(), IconActive = TracesIcon(active: true), Text = "Jjjjj" },
        new FluentCustomAppBarItem { Order = 5, IconRest = TracesIcon(), IconActive = TracesIcon(active: true), Text = "Kkkkk" },
        new FluentCustomAppBarItem { Order = 4, IconRest = TracesIcon(), IconActive = TracesIcon(active: true), Text = "Lllll" },
        new FluentCustomAppBarItem { Order = 3, IconRest = MetricsIcon(), IconActive = MetricsIcon(active: true), Text = "Mmmmm" },
        new FluentCustomAppBarItem { Order = 2, IconRest = MetricsIcon(), IconActive = MetricsIcon(active: true), Text = "Nnnnn" },
        new FluentCustomAppBarItem { Order = 1, IconRest = MetricsIcon(), IconActive = MetricsIcon(active: true), Text = "Ooooo" }
    };

    private bool _showSearch = true;

    private static Icon ResourcesIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.AppFolder() 
               : new Icons.Regular.Size24.AppFolder();

    private static Icon ConsoleLogsIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.SlideText() 
               : new Icons.Regular.Size24.SlideText();

    private static Icon StructuredLogsIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.SlideTextSparkle() 
               : new Icons.Regular.Size24.SlideTextSparkle();

    private static Icon TracesIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.GanttChart() 
               : new Icons.Regular.Size24.GanttChart();

    private static Icon MetricsIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.ChartMultiple() 
               : new Icons.Regular.Size24.ChartMultiple();

    private void HandlePopover(bool visible) => DemoLogger.WriteLine($"Popover visibility changed to {visible}");
}

```

--------------------------------

### DialogHelper Class Methods

Source: https://www.fluentui-blazor.net/Dialog

API documentation for the DialogHelper class, detailing methods for creating and showing dialogs programmatically.

```APIDOC
## DialogHelper Class Methods

### Description

Provides utility methods for creating and displaying dialogs using the `IDialogService`.

### Method

`From<TDialog>`

### Endpoint

Not Applicable (Class Method)

### Parameters

#### Path Parameters

None

#### Query Parameters

None

#### Request Body

None

### Request Example

None

### Response

None

#### Success Response (200)

None

#### Response Example

None

#### Parameters

- **TDialog**: The type of the dialog component to create.

### Method

`ShowDialogAsync<TDialog, TData>`

### Endpoint

Not Applicable (Class Method)

### Parameters

#### Path Parameters

None

#### Query Parameters

None

#### Request Body

None

### Request Example

None

### Response

None

#### Success Response (200)

None

#### Response Example

None

#### Parameters

- **svc** (IDialogService) - The dialog service instance.
- **dialogHelper** (DialogHelper<TDialog>) - An instance of DialogHelper for the specified dialog type.
- **data** (TData) - The data to pass to the dialog.
- **parameters** (DialogParameters) - Additional parameters for the dialog.

### Method

`ShowDialogAsync<TDialog, TData>`

### Endpoint

Not Applicable (Class Method)

### Parameters

#### Path Parameters

None

#### Query Parameters

None

#### Request Body

None

### Request Example

None

### Response

None

#### Success Response (200)

None

#### Response Example

None

#### Parameters

- **dialogHelper** (DialogHelper<TDialog>) - An instance of DialogHelper for the specified dialog type.
- **data** (TData) - The data to pass to the dialog.
- **svc** (IDialogService) - The dialog service instance.
- **parameters** (DialogParameters) - Additional parameters for the dialog.
```

--------------------------------

### FluentOverlay Component API

Source: https://www.fluentui-blazor.net/Overlay

This section details the parameters, event callbacks, and methods available for the FluentOverlay component.

```APIDOC
## FluentOverlay Component API

### Description

The FluentOverlay component provides a way to display an overlay with customizable interactivity, background, and full-screen behavior.

### Parameters

#### Properties

- **Alignment** (Align) - Optional - Gets or sets the alignment of the content to a `Align` value. Defaults to Align.Center.
- **BackgroundColor** (string?) - Optional - Gets or sets the background color. Needs to be formatted as an HTML hex color string (#rrggbb or #rgb). Default NeutralBaseColor token value (#808080).
- **ChildContent** (RenderFragment?) - Optional - Represents the content to be displayed within the overlay.
- **Dismissable** (bool) - Optional - Gets or sets a value indicating if the overlay can be dismissed by clicking on it. Default is true.
- **FullScreen** (bool) - Optional - Gets or sets a value indicating whether the overlay is shown full screen or bound to the containing element. Default is False.
- **Interactive** (bool) - Optional - Gets or sets a value indicating whether the overlay is interactive, except for the element with the specified `FluentOverlay.InteractiveExceptId`. In other words, the elements below the overlay remain usable (mouse-over, click) and the overlay will close when clicked. Default is False.
- **InteractiveExceptId** (string?) - Optional - Gets or sets the HTML identifier of the element that is not interactive when the overlay is shown. This property is ignored if `FluentOverlay.Interactive` is false.
- **Justification** (JustifyContent) - Optional - Gets or sets the justification of the content to a `JustifyContent` value. Defaults to JustifyContent.Center.
- **Opacity** (double?) - Optional - Gets or sets the opacity of the overlay. Default is 0.4.
- **PreventScroll** (bool) - Optional - Prevents scrolling when the overlay is active. Default is False.
- **Transparent** (bool) - Optional - Gets or set if the overlay is transparent. Default is True.
- **Visible** (bool) - Optional - Gets or sets a value indicating whether the overlay is visible. Default is False.

#### EventCallbacks

- **OnClose** (EventCallback<MouseEventArgs>) - Callback for when the overlay is closed.
- **VisibleChanged** (EventCallback<bool>) - Callback for when overlay visibility changes.

#### Methods

- **DisposeAsync** () - ValueTask - Disposes the overlay.
- **OnCloseHandlerAsync** (MouseEventArgs e) - Task - Handles the overlay close event.
- **OnCloseInteractiveAsync** (MouseEventArgs e) - Task - Handles the interactive overlay close event.
```

--------------------------------

### Stack Alignment Renaming

Source: https://www.fluentui-blazor.net/UpgradeGuide

Notes the renaming of `StackHorizontalAlignment` and `StackVerticalAlignment` to `HorizontalAlignment` and `VerticalAlignment` to accommodate broader usage across multiple components.

```csharp
`StackHorizontalAlignment`/`StackVerticalAlignment` have been renamed to `HorizontalAlignment`/`VerticalAlignment`.
```

--------------------------------

### Fluent Blazor RadioGroup States (Readonly, Disabled)

Source: https://www.fluentui-blazor.net/RadioGroup

Demonstrates controlling the interactive state of a FluentRadioGroup. The 'Read only' example prevents user selection while displaying options, and the 'Disabled' example completely disables the group and its options. Both use Blazor data binding.

```Razor
<h4>Read only</h4>
<div style="display: flex; flex-direction: column; margin-top: 12px;">
    <FluentRadioGroup ReadOnly=true Label="readonly radio group" Name="office" @bind-Value=value1>
        <FluentRadio Value=@("word") Label="Word" />
        <FluentRadio Value=@("excel") Label="Excel" />
    </FluentRadioGroup>
</div>

<h4>Disabled</h4>
<div style="display: flex; flex-direction: column; margin-top: 12px;">
    <FluentRadioGroup Disabled=true Label="Disabled radio group" Name="cars" @bind-Value=value2>
        <FluentRadio Value=@("lambo") Label="Lamborghini" />
        <FluentRadio Value=@("ferari") Label="Ferari" />
    </FluentRadioGroup>
</div>
@code {
    string? value1, value2;
}
```

--------------------------------

### C# Form Data Models and Initialization

Source: https://www.fluentui-blazor.net/Drag

Defines the data structures for Form, FormRow, FormColumn, and FormElement, and initializes a test form with columns and elements. This setup is crucial for the drag and drop functionality.

```csharp
public class Form
{
    public int FormId { get; set; }
    public List<FormRow> Rows { get; set; } = [];
}

public class FormRow
{
    public int RowId { get; set; }
    public List<FormColumn> Columns { get; set; } = [];
}

public class FormColumn
{
    public int ColumnId { get; set; }
    public List<FormElement> Elements { get; set; } = [];
}

public class FormElement
{
    public int ElementId { get; set; }
}
```

```csharp
var columns = new List<FormColumn>()
{
    new FormColumn { ColumnId = 0 },
    new FormColumn { ColumnId = 1 },
    new FormColumn { ColumnId = 2 },
    new FormColumn { ColumnId = 3 },
    new FormColumn { ColumnId = 4 },
    new FormColumn { ColumnId = 5 },
    new FormColumn { ColumnId = 6 },
    new FormColumn { ColumnId = 7 },
    new FormColumn { ColumnId = 8 }
};

var elementMap = new Dictionary<int, int>
{
    { 0, 1 },
    { 1, 1 },
    { 2, 0 },
    { 3, 2 },
    { 4, 2 },
    { 5, 1 },
    { 6, 0 },
    { 7, 1 },
    { 8, 0 }
};

int elementIdCounter = 1;
foreach (var column in columns)
{
    if (elementMap.TryGetValue(column.ColumnId, out int count))
    {
        for (int i = 0; i < count; i++)
        {
            column.Elements.Add(new FormElement
                {
                    ElementId = elementIdCounter++
                });
        }
    }
}

var rows = new List<FormRow>()
{
    new FormRow(), new FormRow(), new FormRow(), new FormRow(), new FormRow(), new FormRow()
};

rows[0].Columns.AddRange(new[] { columns[0], columns[1] });
rows[1].Columns.Add(columns[2]);
rows[2].Columns.Add(columns[3]);
rows[3].Columns.AddRange(new[] { columns[4], columns[5] });
rows[4].Columns.AddRange(new[] { columns[6], columns[7] });
rows[5].Columns.Add(columns[8]);

_testForm.Rows.AddRange(rows);
```

--------------------------------

### FluentSlider Component Configuration

Source: https://www.fluentui-blazor.net/datagrid-typical

Configures a FluentSlider component to select a maximum value, with defined steps and labels for specific positions. It binds to the 'maxMedals' variable and updates immediately.

```html
<FluentSlider Label="@($"Max ({maxMedals})")" Min="0" Max="150" Step="1" Orientation="Orientation.Horizontal" @bind-Value=maxMedals Immediate="true" Style="width: 100%;">
    <FluentSliderLabel Position="0">0</FluentSliderLabel>
    <FluentSliderLabel Position="50">50</FluentSliderLabel>
    <FluentSliderLabel Position="100">100</FluentSliderLabel>
    <FluentSliderLabel Position="150">150</FluentSliderLabel>
</FluentSlider>
```

--------------------------------

### Fluent DataGrid with Pagination, Sorting, Filtering, and Resizing (Razor)

Source: https://www.fluentui-blazor.net/datagrid-typical

This Razor code defines a FluentDataGrid component configured for typical usage. It includes in-memory data, pagination, sorting, filtering capabilities, and column resizing. Tooltips are enabled for most columns, with specific handling for TemplateColumns and PropertyColumns.

```Razor
@inject DataSource Data

<p>To test set ResizeType on the DataGrid to either DataGridResizeType.Discrete or DataGridResizeType.Exact</p>
<p>Remove the parameter completely to get the original behavior</p>
<p>Use ResizeColumnOnAllRows="false" to limit column resizing to header cells only (default is true for all rows)</p>

<div style="height: 380px; overflow-x:auto; display:flex;">
    <FluentDataGrid @ref="grid"
                    Items="@FilteredItems"
                    ResizableColumns=true
                    ResizeType="DataGridResizeType.Discrete"
                    GridTemplateColumns="0.3fr 1fr 0.2fr 0.2fr 0.2fr 0.2fr"
                    Pagination="@pagination"
                    RowClass="@rowClass"
                    RowStyle="@rowStyle"
                    HeaderCellAsButtonWithMenu="true"
                    ColumnResizeLabels="@resizeLabels">
        <TemplateColumn Tooltip="true" TooltipText="@(c => "Flag of " + c.Name)" Title="Rank" SortBy="@rankSort" Align="Align.Center" InitialSortDirection="SortDirection.Ascending" IsDefaultSortColumn=true>
            <img class="flag" src="_content/FluentUI.Demo.Shared/flags/@(context.Code).svg" alt="Flag of @(context.Code)" />
        </TemplateColumn>
        <PropertyColumn Property="@(c => c.Name)" Sortable="true" Filtered="!string.IsNullOrWhiteSpace(nameFilter)" Tooltip="true" Title="Name of the country">
            <ColumnOptions>
                <div class="search-box">
                    <FluentSearch Autofocus=true @bind-Value=nameFilter @oninput="HandleCountryFilter" @onkeydown="HandleCloseFilterAsync" @bind-Value:after="HandleClear" Placeholder="Country name..." Style="width: 100%;" Label="Filter" />
                </div>
            </ColumnOptions>
        </PropertyColumn>
        <PropertyColumn Property="@(c => c.Medals.Gold)" Sortable="true" Align="Align.Start" Tooltip="true" TooltipText="@(c => "That is " + c.Medals.Gold + " x GOLD!!")" />
        <PropertyColumn Property="@(c => c.Medals.Silver)" Sortable="true" Align="Align.Center" Tooltip="true" />
        <PropertyColumn Property="@(c => c.Medals.Bronze)" Sortable="false" Align="Align.End" />
        <PropertyColumn Property="@(c => c.Medals.Total)" Sortable="true" Filtered="@(minMedals != 0 || maxMedals != 130)" Align="Align.End" Tooltip="true">
            <ColumnOptions>
                <div style="width: 100%; height: 150px;">
                    <FluentSlider Label="@($"Min ({minMedals})")" Min="0" Max="150" Step="1" Orientation="Orientation.Horizontal" @bind-Value=minMedals Immediate="true" Style="width: 100%;">
                        <FluentSliderLabel Position="0">0</FluentSliderLabel>
                        <FluentSliderLabel Position="50">50</FluentSliderLabel>
                        <FluentSliderLabel Position="100">100</FluentSliderLabel>
                        <FluentSliderLabel Position="150">150</FluentSliderLabel>
                    </FluentSlider>
                    <br /><br />

```

--------------------------------

### FluentPaginator Component Usage

Source: https://www.fluentui-blazor.net/datagrid-typical

Integrates the FluentPaginator component, controlled by the 'pagination' state object. This component is used to manage pagination for data displayed in a FluentDataGrid or similar components.

```html
<FluentPaginator State="@pagination" />
```

--------------------------------

### FluentDialogProvider Class Methods

Source: https://www.fluentui-blazor.net/Dialog

Documentation for the methods available in the FluentDialogProvider class.

```APIDOC
## FluentDialogProvider Class

### Description
Provides dialog related services.

### Methods

#### DismissAll

##### Description
Dismisses all currently open dialogs.

##### Method
`void`

#### DisposeAsync

##### Description
Disposes the dialog provider asynchronously.

##### Method
`ValueTask`

```

--------------------------------

### Get Total Food Recall Results Count using .NET

Source: https://www.fluentui-blazor.net/datagrid-remote-data

This C# code snippet retrieves the total number of food recall results from the FDA API. It uses HttpClient to make a GET request and deserializes the 'Meta.Results.Total' field from the JSON response.

```csharp
// Display the number of results just for information. This is completely separate from the grid.
numResults = (await Http.GetFromJsonAsync<FoodRecallQueryResult>("https://api.fda.gov/food/enforcement.json"))!.Meta.Results.Total;
```

--------------------------------

### FluentDataGrid Parameter Changes v2 to v3

Source: https://www.fluentui-blazor.net/UpgradeGuide

Details parameter name changes in the FluentDataGrid component from v2 to v3, aligning with QuickGrid naming conventions for better integration with .NET 8.

```csharp
Parameter name changes from v2:
  * RowsData -> Items
  * RowsDataProvider -> ItemsProvider
  * RowsDataSize -> ItemSize
  * RowsDataKey -> ItemKey
```

--------------------------------

### Sorting by Column Key with ColumnKeyGridSort in Blazor

Source: https://www.fluentui-blazor.net/datagrid-custom-gridsort

This example demonstrates sorting in a Blazor DataGrid when the sorting criteria are not directly mapped to properties on the data model. It uses ColumnKeyGridSort to define sorting logic based on keys within a dictionary. This is useful for datasets with dynamic or non-standard property names.

```razor
<FluentDataGrid Items="@_gridData" ResizableColumns=true GridTemplateColumns="0.5fr 0.5fr">
    <TemplateColumn Sortable="true" Title="First Name" SortBy="_firstNameSort">
        @context.Properties["firstname"]
    </TemplateColumn>

    <TemplateColumn Sortable="true" Title="Last Name" SortBy="_lastNameSort">
        @context.Properties["lastname"]
    </TemplateColumn>
</FluentDataGrid>

@code {
    private ColumnKeyGridSort<GridRow> _firstNameSort = new ColumnKeyGridSort<GridRow>(
        "firstname",
        (queryable, sortAscending) =>
        {
            if (sortAscending)
            {
                return queryable.OrderBy(x => x.Properties["firstname"]);
            }
            else
            {
                return queryable.OrderByDescending(x => x.Properties["firstname"]);
            }
        }
    );

    private ColumnKeyGridSort<GridRow> _lastNameSort = new ColumnKeyGridSort<GridRow>(
        "lastname",
        (queryable, sortAscending) =>
        {
            if (sortAscending)
            {
                return queryable.OrderBy(x => x.Properties["lastname"]);
            }
            else
            {
                return queryable.OrderByDescending(x => x.Properties["lastname"]);
            }
        }
    );

    private static readonly IQueryable<GridRow> _gridData = new GridRow[] {
        new(new Dictionary<string, string>{ { "firstname", "Tom" }, { "lastname", "Cruise" } }),
        new(new Dictionary<string, string>{ { "firstname", "Dolly" }, { "lastname", "Parton" } }),
        new(new Dictionary<string, string>{ { "firstname", "Nicole" }, { "lastname", "Kidmon" } }),
        new(new Dictionary<string, string>{ { "firstname", "James" }, { "lastname", "Bond" } }),
    }.AsQueryable();

    public record GridRow(Dictionary<string, string> Properties);
}

```

--------------------------------

### FluentAnchor Simple Examples (Razor)

Source: https://www.fluentui-blazor.net/Anchor

Demonstrates basic FluentAnchor usage in Razor, including default anchors, anchors with targets, aria-labels, and custom click actions. It shows how to create links with and without href attributes, utilizing the Appearance.Hypertext option for inline text links.

```Razor
<p>Default</p>
<FluentAnchor Href="#" title="Anchor tooltip">Anchor</FluentAnchor>

<p>With target</p>
<FluentAnchor Href="https://microsoft.com" Target="_blank">Anchor</FluentAnchor>

<p>With aria-label</p>
<FluentAnchor Href="#" aria-label="Anchor with aria-label"></FluentAnchor>


<p>With custom action</p>
<FluentAnchor Href="#" Appearance="Appearance.Hypertext" OnClick="HandleClick">Link with no Href</FluentAnchor>

@code{
    private void HandleClick() => DemoLogger.WriteLine("Anchor clicked");
}
```

--------------------------------

### Tab Orientation Example

Source: https://www.fluentui-blazor.net/Tabs

Demonstrates how to configure FluentTabs with a vertical orientation and control the active tab.

```APIDOC
## POST /websites/fluentui-blazor_net

### Description
This endpoint is not directly exposed as an API. The provided content is a Razor code snippet demonstrating the usage of FluentTabs and FluentTab components within a Blazor application.

### Method
N/A (This is a Razor code example, not an HTTP endpoint)

### Endpoint
N/A

### Parameters
#### Path Parameters
- None

#### Query Parameters
- None

#### Request Body
- None

### Request Example
```razor
<h4>No active indicator - Vertical</h4>
<FluentTabs ShowActiveIndicator=false ActiveTabId="tab3" Orientation="Orientation.Vertical" Style="height: 250px;">
    <FluentTab Id="TabOne" Label="Tab one">
        Tab one content. This is for testing.
    </FluentTab>
    <FluentTab Id="TabTwo" Label="Tab two">
        Tab two content. This is for testing.
    </FluentTab>
    <FluentTab Id="TabThree" Label="Tab three">
        Tab three content. This is for testing.
    </FluentTab>
    <FluentTab id="tab4" Disabled=true Label="Tab four">
        Tab four content. This is for testing.
    </FluentTab>
</FluentTabs>
```

### Response
#### Success Response (200)
- N/A

#### Response Example
- N/A
```

--------------------------------

### FluentSwitch for Toggling Items

Source: https://www.fluentui-blazor.net/datagrid-typical

Implements a FluentSwitch component that allows users to toggle between clearing and restoring all items. It uses two-way binding to '_clearItems' and triggers the 'ToggleItemsAsync' method after the value changes.

```html
<FluentSwitch @bind-Value="@_clearItems"
              @bind-Value:after="ToggleItemsAsync"
              UncheckedMessage="Clear all results"
              CheckedMessage="Restore all results">
</FluentSwitch>
```

--------------------------------

### Custom Sort Order with GridSort in Blazor

Source: https://www.fluentui-blazor.net/datagrid-custom-gridsort

This example shows how to define custom sorting logic for a Blazor DataGrid using the GridSort class. It allows for sorting by multiple properties, including maintaining ascending order within groups. Dependencies include Fluent DataGrid components and a GridRow data model.

```razor
<FluentDataGrid Items="@_gridData" ResizableColumns=true GridTemplateColumns="0.5fr 0.5fr">
    <PropertyColumn Sortable="true" Property="x => x.Number" Title="Rank" />

    <TemplateColumn Sortable="true" SortBy="groupRank" Title="Group">
        @context.Group
    </TemplateColumn>

</FluentDataGrid>

<p>Keep numbers always sorted ascending inside the group when sorting by group</p>
<FluentDataGrid Items="@_gridData" ResizableColumns=true GridTemplateColumns="0.5fr 0.5fr">

    <PropertyColumn Sortable="true" Property="x => x.Number" Title="Rank" />
    <PropertyColumn Property="x => x.Group" SortBy="groupRankNumberAlwaysAscending" Title="Group" />

</FluentDataGrid>



@code {
    GridSort<GridRow> groupRank = GridSort<GridRow>
            .ByAscending(x => x.Group)
            .ThenAscending(x => x.Number);

    GridSort<GridRow> groupRankNumberAlwaysAscending = GridSort<GridRow>
        .ByAscending(x => x.Group)
        .ThenAlwaysAscending(x => x.Number);

    private static readonly IQueryable<GridRow> _gridData = new GridRow[] {
        new(2, "B"),
        new(1, "A"),
        new(4, "B"),
        new(3, "A")
    }.AsQueryable();

    public class GridRow(int number, string group)
    {
        public int Number { get; } = number;
        public string Group { get; } = group;
    }
}

```

--------------------------------

### MenuButton with Dictionary Items

Source: https://www.fluentui-blazor.net/MenuButton

This example demonstrates how to create a FluentMenuButton using a Dictionary to supply menu items. It also shows how to handle menu item selection and update the accent color based on the selected item. Requires AccentBaseColor service.

```Razor
@inject AccentBaseColor AccentBaseColor

<FluentMenuButton @ref=menubutton Text="Select brand color" Items="@items" OnMenuChanged="HandleOnMenuChanged"></FluentMenuButton>

@code {
    private FluentMenuButton menubutton = new();

    private Dictionary<string, string> items = new Dictionary<string, string>()
    {
        {"0078D4","Windows"},
        {"D83B01","Office"},
        {"464EB8","Teams"},
        {"107C10","Xbox"},
        {"8661C5","Visual Studio"},
        {"F2C811","Power BI"},
        {"0066FF","Power Automate"},
        {"742774","Power Apps"},
        {"0B556A","Power Virtual Agents"}
    };

    private async Task HandleOnMenuChanged(MenuChangeEventArgs args)
    {
        await AccentBaseColor.SetValueFor(menubutton.Button!.Element, $"#{args.Id}".ToSwatch());
    }

}
```

--------------------------------

### FluentDivider: Default and Role Examples (Razor)

Source: https://www.fluentui-blazor.net/Divider

Demonstrates the default usage of the FluentDivider component and how to apply 'Presentation' and 'Separator' ARIA roles for accessibility. This component wraps the native `<fluent-divider>` web component.

```Razor
<h4>Default</h4>
<FluentDivider></FluentDivider>
<br />
<h4>Role="Presentation"</h4>
<p>
    <FluentStack Orientation="Orientation.Vertical">
        <span>before divider</span>
        <FluentDivider Style="width: 100%;" Role="DividerRole.Presentation"></FluentDivider>
        <span>after divider</span>
    </FluentStack>
</p>

<h4>Role="Separator"</h4>
<p>
    <FluentStack Orientation="Orientation.Vertical">
        <span>before divider</span>
        <FluentDivider Style="width: 100%;" Orientation=Orientation.Horizontal Role="DividerRole.Separator"></FluentDivider>
        <span>after divider</span>
    </FluentStack>
</p>
```

--------------------------------

### FluentRadio Aria Label vs. Visible Label (Razor)

Source: https://www.fluentui-blazor.net/Radio

Shows the difference between using a visible label and an aria-label for the Fluent UI Blazor Radio component. This example requires the Fluent UI Blazor library and demonstrates accessibility considerations.

```csharp
<h4>Visual vs audio label</h4>
<FluentRadioGroup @bind-Value=value1>
    <FluentRadio Label="Visible label" />
</FluentRadioGroup>

<div style="display: flex; flex-direction: column; margin-top: 12px;">
    <FluentRadioGroup @bind-Value="value2" Label="Outside label">
        <FluentRadio Label="label1"></FluentRadio>
    </FluentRadioGroup>
</div>
@code {
    string? value1, value2;
}
```

--------------------------------

### Fluent Blazor Navigation Menu Examples

Source: https://www.fluentui-blazor.net/NavMenu

This snippet showcases different ways to create navigation menus using Fluent Blazor components. It includes a main navigation menu with nested groups and icons, a menu with only icons, and a simple text-based menu. The `FluentNavMenu` component supports `FluentNavLink` and `FluentNavGroup` for creating hierarchical navigation structures. Event handling for clicks is also demonstrated.

```Razor
<h2>Navigation Examples</h2>

<FluentStack Orientation="Orientation.Horizontal" >
    <div style="background: var(--neutral-layer-3); display: flex; padding: 10px;">
        <FluentNavMenu @bind-Expanded="@expanded" Width="250" Title="Custom navigation menu">
            <FluentNavLink Icon="@(new Icons.Regular.Size20.Home())" Href="/" Match="NavLinkMatch.All">Home</FluentNavLink>
            <FluentNavLink Href="/NavMenu">Item 2</FluentNavLink>
            <FluentNavGroup OnClick="OnClick" Title="Item 3" Icon="@(new Icons.Regular.Size20.EarthLeaf())">
                <TitleTemplate><h3>Item 3</h3></TitleTemplate>
                <ChildContent>
                    <FluentNavLink OnClick="OnClick" Icon="@(new Icons.Regular.Size20.LeafOne())">Item 3.1</FluentNavLink>
                    <FluentNavLink OnClick="OnClick" Icon="@(new Icons.Regular.Size20.LeafTwo())">Item 3.2</FluentNavLink>
                </ChildContent>
            </FluentNavGroup>
            <FluentNavLink Icon="@(new Icons.Regular.Size20.CalendarAgenda())" Disabled="true" Href="https://microsoft.com">Item 4</FluentNavLink>
            <FluentNavLink Icon="@(new Icons.Regular.Size20.Home())" Disabled="true">Item 5</FluentNavLink>
            <FluentNavGroup Expanded="true" Title="Item 6 Item 6 Item 6 Item 6 Item 6" Icon="@(new Icons.Regular.Size20.EarthLeaf())">
                <FluentNavLink Icon="@(new Icons.Regular.Size20.LeafOne())">Item 6.1</FluentNavLink>
                <FluentNavLink Icon="@(new Icons.Regular.Size20.LeafTwo())">Item 6.2</FluentNavLink>
                <FluentNavGroup Expanded="true" Title="Item 6.3" Icon="@(new Icons.Regular.Size20.EarthLeaf())">
                    <FluentNavLink Icon="@(new Icons.Regular.Size20.LeafOne())">Item 6.3.1 Item 6.3.1 Item 6.3.1</FluentNavLink>
                    <FluentNavLink Icon="@(new Icons.Regular.Size20.LeafTwo())">Item 6.3.2</FluentNavLink>
                    <FluentNavGroup Expanded="true" Title="Item 6.3.3 Item 6.3.3 Item 6.3.3" Icon="@(new Icons.Regular.Size20.EarthLeaf())">
                        <FluentNavLink Icon="@(new Icons.Regular.Size20.LeafOne())">Item 6.3.3.1</FluentNavLink>
                        <FluentNavLink Disabled="true" Icon="@(new Icons.Regular.Size20.LeafTwo())">Item 6.3.3.2</FluentNavLink>
                        <FluentNavGroup Disabled="true" Expanded="true" Title="Item 6.3.3.3" Icon="@(new Icons.Regular.Size20.EarthLeaf())">
                            <FluentNavLink Icon="@(new Icons.Regular.Size20.LeafOne())">Item 6.3.3.3.1</FluentNavLink>
                            <FluentNavLink Icon="@(new Icons.Regular.Size20.LeafTwo())">Item 6.3.3.3.2</FluentNavLink>
                        </FluentNavGroup>
                    </FluentNavGroup>
                </FluentNavGroup>
            </FluentNavGroup>
        </FluentNavMenu>
    </div>

    @code
    {
        bool expanded = true;
    }

    <!-- Menu with icons -->
    <FluentNavMenu>
        <FluentNavLink OnClick="OnClick" Icon="@(new Icons.Regular.Size24.Home())" >Item 1</FluentNavLink>
        <FluentNavLink OnClick="OnClick" >Item 2</FluentNavLink>
        <FluentNavLink OnClick="OnClick" Icon="@(new Icons.Regular.Size24.LeafOne())" >Item 3</FluentNavLink>
        <FluentNavLink OnClick="OnClick" >Item 4</FluentNavLink>
    </FluentNavMenu>

    <!-- Menu simple -->
    <FluentNavMenu>
        <FluentNavLink OnClick="OnClick" >Item 1</FluentNavLink>
        <FluentNavLink OnClick="OnClick" >Item 2</FluentNavLink>
        <FluentNavLink OnClick="OnClick" >Item 3</FluentNavLink>
        <FluentNavLink OnClick="OnClick" >Item 4</FluentNavLink> 
    </FluentNavMenu>

</FluentStack>
@code
{
    void OnClick(MouseEventArgs e)
    {
        DemoLogger.WriteLine("NavMenu item clicked");
    }
}

```

--------------------------------

### FluentNavMenu Keep Previous Code

Source: https://www.fluentui-blazor.net/UpgradeGuide

Instructions for retaining previous FluentNavMenu code structure when upgrading. This involves renaming the component to `FluentNavMenuTree`.

```razor
To keep your previous menu code:
  * Rename `FluentNavMenu` to `FluentNavMenuTree`
```

--------------------------------

### MenuButton with Manually Supplied FluentMenuItems

Source: https://www.fluentui-blazor.net/MenuButton

This example shows how to configure a FluentMenuButton by manually adding FluentMenuItem components. This approach allows for finer control over individual menu items, such as disabling specific options. Requires AccentBaseColor service.

```Razor
@inject AccentBaseColor AccentBaseColor

<FluentMenuButton @ref=menubuttonm Text="Select brand color" OnMenuChanged="HandleOnMenuChanged">
    <FluentMenuItem Id="0078D4">Windows</FluentMenuItem>
    <FluentMenuItem Id="D83B01" Disabled="true">Office</FluentMenuItem>
    <FluentMenuItem Id="464EB8">Teams</FluentMenuItem>
    <FluentMenuItem Id="107C10" Disabled="true">Xbox</FluentMenuItem>
    <FluentMenuItem Id="8661C5">Visual Studio</FluentMenuItem>
    <FluentMenuItem Id="F2C811" Disabled="true">Power BI</FluentMenuItem>
    <FluentMenuItem Id="0066FF">Power Automate</FluentMenuItem>
    <FluentMenuItem Id="742774" Disabled="true">Power Apps</FluentMenuItem>
    <FluentMenuItem Id="0B556A">Power Virtual Agents</FluentMenuItem>

</FluentMenuButton>

@code {
    private FluentMenuButton menubuttonm = new();

    private async Task HandleOnMenuChanged(MenuChangeEventArgs args)
    {
        await AccentBaseColor.SetValueFor(menubuttonm.Button!.Element, $"#{args.Id}".ToSwatch());
    }

}
```

--------------------------------

### FluentTreeView - With several nested items (expanded)

Source: https://www.fluentui-blazor.net/TreeView

Demonstrates how to use the FluentTreeView component with multiple nested items, showing an example where all items are initially expanded.

```APIDOC
## FluentTreeView - With several nested items (expanded)

### Description
This example showcases a `FluentTreeView` with deeply nested `FluentTreeItem` components. The `InitiallyExpanded="true"` attribute is used on several parent items to ensure they are open by default, revealing their child items.

### Method
N/A (UI Component Example)

### Endpoint
N/A (UI Component Example)

### Parameters
N/A (UI Component Example)

### Request Example
```html
<FluentTreeView>
    <FluentTreeItem InitiallyExpanded="true">
        Root item
        <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="start" />
        <FluentTreeItem InitiallyExpanded="true">
            Nested Root item 1
            <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="start" />
            <FluentTreeItem>
                <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="start" />
                Nested item 4
            </FluentTreeItem>
            <FluentTreeItem>
                
                Nested item 5
            </FluentTreeItem>
        </FluentTreeItem>
        <FluentTreeItem>
            Nested item 2
            
        </FluentTreeItem>
        <FluentTreeItem>
            Nested item 3
            <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="start" />
        </FluentTreeItem>
    </FluentTreeItem>
</FluentTreeView>
```

### Response
N/A (UI Component Example)

### Response Example
N/A (UI Component Example)
```

--------------------------------

### Fluent Blazor Search with Start and End Icons

Source: https://www.fluentui-blazor.net/Search

This snippet illustrates how to add icons to the Fluent Blazor Search component, positioned either at the start or the end of the input field. It uses the FluentIcon component and specifies the slot attribute to control the icon's placement. This enhances the visual appeal and usability of the search input.

```razor
<h4>With start</h4>
<FluentSearch @bind-Value=value>
    <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="start" />
</FluentSearch>

<h4>With end</h4>
<FluentSearch @bind-Value=value>
    <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="end" />
</FluentSearch>
@code {
    string? value;
}
```

--------------------------------

### Razor Combobox Examples: Default, Pre-selected, Placeholder

Source: https://www.fluentui-blazor.net/Combobox

Demonstrates the default usage of the FluentCombobox component in Razor, including how to set a placeholder, pre-select an item, and bind values. It relies on a `DataSource` for items and handles selected values using string variables.

```Razor
@inject DataSource Data

<h4>Select the best song from the list or type your own</h4>
<p>
    Selected hit: @hit
</p>
<FluentCombobox Placeholder="Make a selection..." Label="Best song" Autofocus="true" Items="@Data.Hits" @bind-Value="@hit" Height="200px" Immediate ImmediateDelay="100" />

<h4>Pre-selected option</h4>
<FluentCombobox AriaLabel="Pre-selected option" Items="@Data.Names" @bind-Value="@name" Height="200px" />
<p>
    Selected name: @name
</p>

<h4>With Placeholder</h4>
<FluentCombobox Placeholder="Please select a size" Items="@Data.Sizes" @bind-Value="@size" Height="200px" />
<p>
    Selected size: @size
</p>

@code {
    string? hit = "";
    string? name = "Nur Sari";
    string? size = "";
}
```

--------------------------------

### FluentTooltip Component Configuration

Source: https://www.fluentui-blazor.net/Tooltip

This section details the configurable parameters for the FluentTooltip component, allowing customization of its behavior, appearance, and content.

```APIDOC
## FluentTooltip Component

### Description

The FluentTooltip component displays informative text when a user hovers over or focuses on an associated element. It offers extensive customization options for positioning, delay, content, and accessibility.

### Parameters

#### Path Parameters

None

#### Query Parameters

None

#### Request Body

*   **Anchor** (string) - Required - Gets or sets the control identifier associated with the tooltip.
*   **AriaLabel** (string?) - Optional - Gets or sets the text used on aria-label attribute.
*   **AutoUpdateMode** (AutoUpdateMode?) - Optional - Controls when the tooltip updates its position. Default is `anchor` (updates on anchor resize). `Auto` updates on scroll/resize events. Corresponds to `anchored-region auto-update-mode`.
*   **ChildContent** (RenderFragment?) - Optional - Gets or sets the content to be rendered inside the component.
*   **Delay** (int?) - Optional - Gets or sets the delay (in milliseconds). Default is 300.
*   **HideTooltipOnCursorLeave** (bool?) - Optional - Gets or sets the value indicating whether the library should close the tooltip if the cursor leaves the anchor and the tooltip. By default, the tooltip closes if the cursor leaves the anchor, but not the tooltip itself. This behavior can be configured globally using `LibraryConfiguration.HideTooltipOnCursorLeave`.
*   **HorizontalViewportLock** (bool) - Optional - Gets or sets whether the horizontal viewport is locked. Default is `False`.
*   **MaxWidth** (string?) - Optional - Gets or sets the maximum width of the tooltip panel.
*   **Position** (TooltipPosition?) - Optional - Gets or sets the tooltip's position. See `TooltipPosition`. If not set, the tooltip uses the best position automatically.
*   **UseTooltipService** (bool) - Optional - Use `ITooltipService` to create the tooltip if the service is injected. If `FluentTooltip.ChildContent` is dynamic, set this to `false`. Default is `True`.
*   **VerticalViewportLock** (bool) - Optional - Gets or sets whether the vertical viewport is locked. Default is `False`.
*   **Visible** (bool) - Optional - Gets or sets a value indicating whether the tooltip is visible. Default is `False`.

### EventCallbacks

*   **OnDismissed** (EventCallback<EventArgs>) - Callback for when the tooltip is dismissed.

### Request Example

```html
<FluentTooltip Anchor="someElementId" Content="This is a tooltip message.">
    Hover over me
</FluentTooltip>
```

### Response

#### Success Response (200)

This component does not have a direct success response in the typical API sense, as it's a UI component. Its rendering and behavior are controlled by its parameters and framework integration.

#### Response Example

*(N/A - UI Component)*

### Tooltip Positions

*   Top
*   Bottom
*   Left
*   Right
*   Start
*   End
```

--------------------------------

### Implement IKeyCodeListener Interface (C#)

Source: https://www.fluentui-blazor.net/KeyCode

This example shows how to implement the IKeyCodeListener and IDisposableAsync interfaces to capture key events. By registering the component itself as a listener, it can directly handle key events. This approach is suitable for components that need to react to keyboard input.

```csharp
public partial MyPage : IKeyCodeListener, IDisposableAsync
{
    [Inject]
    private IKeyCodeService KeyCodeService { get; set; }

    protected override void OnInitialized()
    {
        KeyCodeService.RegisterListener(this);
    }

    public async Task OnKeyDownAsync(FluentKeyCodeEventArgs args) => { // ... }

    public ValueTask DisposeAsync()
    {
        KeyCodeService.UnregisterListener(this);
        return ValueTask.CompletedTask;
    }
}
```

--------------------------------

### FluentMessageBarProvider Class Parameters

Source: https://www.fluentui-blazor.net/MessageBar

Detailed documentation for the parameters of the FluentMessageBarProvider class.

```APIDOC
## FluentMessageBarProvider Class Parameters

### Description
This section details the configurable parameters for the `FluentMessageBarProvider` component.

### Method
N/A

### Endpoint
N/A

### Parameters
#### Path Parameters
N/A

#### Query Parameters
N/A

#### Request Body
##### `ClearAfterNavigation`
- **Type**: `bool`
- **Default**: `False`
- **Description**: Clear all (shown and stored) messages when the user navigates to a new page.

##### `MaxMessageCount`
- **Type**: `int?`
- **Default**: `5`
- **Description**: Maximum number of messages displayed. The rest are stored in memory to be displayed when a shown message is closed. Default value is 5. Set a value equal to or less than zero to display all messages for this `FluentMessageBarProvider.Section` (or all categories if not set).

##### `NewestOnTop`
- **Type**: `bool`
- **Default**: `True`
- **Description**: Display the newest messages on top (true) or on bottom (false).

##### `Section`
- **Type**: `string?`
- **Default**: `null`
- **Description**: Display only messages for this section.

##### `Type`
- **Type**: `MessageType`
- **Default**: `MessageBar`
- **Description**: Displays messages as a single line (with the message only) or as a card (with the detailed message).

### Request Example
N/A

### Response
N/A
```

--------------------------------

### FluentSearch: Minlength and Maxlength Examples

Source: https://www.fluentui-blazor.net/Search

Shows how to set minimum and maximum length constraints for the FluentSearch component using the Minlength and Maxlength attributes. This helps in enforcing input data quality by limiting the number of characters users can enter. The output demonstrates FluentSearch inputs with enforced character limits.

```Razor
<h4>Minlength</h4>
<FluentSearch @bind-Value=value Minlength ="4">Minlength</FluentSearch>

<h4>Maxlength</h4>
<FluentSearch @bind-Value=value Maxlength ="4">Maxlength</FluentSearch>
@code {
    string? value;
}
```

--------------------------------

### Use Fluent UI System Icon in Blazor

Source: https://www.fluentui-blazor.net/ProjectSetup

Renders a Fluent UI System Icon within a Blazor component. It demonstrates how to specify the icon variant, size, and name using the FluentIcon component.

```razor
<FluentIcon Value="@(new @(Icons.Regular.Size24.Save)())" />

```

--------------------------------

### Generate Fluent UI Blazor Icons (C#)

Source: https://www.fluentui-blazor.net/AppBar

These methods generate Fluent UI Blazor icons based on a boolean 'active' parameter. They utilize 'Icons.Filled' and 'Icons.Regular' namespaces to select the appropriate icon variant. The output is an 'Icon' object.

```csharp
private static Icon ResourcesIcon(bool active = false) =>
        active ? new Icons.Filled.Size16.AppFolder() 
               : new Icons.Regular.Size16.AppFolder();

    private static Icon ConsoleLogsIcon(bool active = false) =>
        active ? new Icons.Filled.Size16.SlideText() 
               : new Icons.Regular.Size16.SlideText();

    private static Icon StructuredLogsIcon(bool active = false) =>
        active ? new Icons.Filled.Size16.SlideTextSparkle() 
               : new Icons.Regular.Size16.SlideTextSparkle();

    private static Icon TracesIcon(bool active = false) =>
        active ? new Icons.Filled.Size16.GanttChart() 
               : new Icons.Regular.Size16.GanttChart();

    private static Icon MetricsIcon(bool active = false) =>
        active ? new Icons.Filled.Size16.ChartMultiple() 
               : new Icons.Regular.Size16.ChartMultiple();
```

--------------------------------

### Add Fluent UI Components to Service Collection (C#)

Source: https://www.fluentui-blazor.net/WhatsNew-Archive

This snippet demonstrates how to add Fluent UI components to the service collection in Program.cs. It utilizes a configuration object generated from icon and emoji settings. This setup is necessary for the system to recognize available icons and emojis.

```csharp
LibraryConfiguration config = new(ConfigurationGenerator.GetIconConfiguration(), ConfigurationGenerator.GetEmojiConfiguration());
builder.Services.AddFluentUIComponents(config);
```

--------------------------------

### Fluent Blazor Splitter with Fractional Unit Sizing

Source: https://www.fluentui-blazor.net/Splitter

This example illustrates using the Fluent UI Blazor Splitter component with CSS fractional units ('fr') for defining panel sizes. This method offers more flexible and responsive panel sizing compared to fixed percentages.

```Razor
<FluentSplitter Orientation="Orientation.Horizontal" Panel1Size="0.3fr" Panel2Size="0.7fr">
    <Panel1>
        <div class="demopanel">
            <h5>Panel 1</h5>
            <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                Eget dolor morbi non arcu risus quis varius. Turpis tincidunt id aliquet risus feugiat in ante. Eros donec ac odio tempor orci
                dapibus ultrices in iaculis. Sit amet justo donec enim diam vulputate ut. Morbi blandit cursus risus at ultrices mi tempus. Sed
                ullamcorper morbi tincidunt ornare massa eget egestas. Mi eget mauris pharetra et ultrices neque. Sit amet porttitor eget dolor
                morbi non arcu risus quis. Tempus egestas sed sed risus pretium quam vulputate dignissim. Diam vel quam elementum pulvinar. Enim
                nulla aliquet porttitor lacus luctus accumsan. Convallis tellus id interdum velit laoreet id donec ultrices. Dui faucibus in ornare
                quam viverra orci sagittis.
            </p>
        </div>
    </Panel1>
    <Panel2>
        <div class="demopanel">
            <h5>Panel 2</h5>
            <p>
                Neque laoreet suspendisse interdum consectetur libero id faucibus nisl tincidunt. Suspendisse faucibus interdum posuere lorem ipsum
                dolor sit amet. Imperdiet sed euismod nisi porta lorem mollis aliquam. Malesuada proin libero nunc consequat interdum. Amet nisl purus
                in mollis nunc sed id semper risus. Nunc sed augue lacus viverra vitae congue eu. Fermentum dui faucibus in ornare quam viverra. Ut eu
                sem integer vitae. Interdum velit laoreet id donec ultrices tincidunt arcu non. Pellentesque dignissim enim sit amet. Scelerisque purus
                semper eget duis at.
            </p>
        </div>
    </Panel2>
</FluentSplitter>
```

--------------------------------

### FluentTextArea Filled Appearance Options - Razor

Source: https://www.fluentui-blazor.net/TextArea

Demonstrates the 'filled' visual appearance for the FluentTextArea component, including examples with default label, placeholder, required, and disabled states.

```Razor
<h4>Default</h4>
<p>label</p>
<h4>Placeholder</h4>
<p>Placeholder</p>
<h4>Required</h4>
<p>Required</p>
<h4>Disabled</h4>
<p>label</p>
```

--------------------------------

### Show Dialog and Get Result via IDialogReference (C#)

Source: https://www.fluentui-blazor.net/DialogService

Demonstrates using DialogService.ShowDialogAsync to display a dialog and retrieve its result. It shows how to configure dialog parameters, await the dialog's completion, and process the DialogResult, including accessing data and checking for cancellation.

```csharp
DialogParameters<SimplePerson> parameters = new() {
    Title = $"Hello {simplePerson.Firstname}",
    PrimaryAction = "Yes",
    PrimaryActionEnabled = false,
    SecondaryAction = "No",
    Width = "500px",
    Height = "500px",
    Content = simplePerson,
    TrapFocus = _trapFocus,
    Modal = _modal,
};

IDialogReference dialog = await DialogService.ShowDialogAsync<SimpleDialog, SimplePerson>(parameters);
DialogResult? result = await dialog.Result;

if (result.Data is not null) {
    SimplePerson? simplePerson = result.Data as SimplePerson;
    Console.WriteLine($"Dialog closed by {simplePerson?.Firstname} {simplePerson?.Lastname} ({simplePerson?.Age}) - Canceled: {result.Cancelled}");
}
else {
    Console.WriteLine($"[DialogService] Dialog closed - Canceled: {result.Cancelled}");
}
```

--------------------------------

### Implement Drag and Drop with Fluent DragContainer (Razor)

Source: https://www.fluentui-blazor.net/Drag

This example demonstrates how to use the FluentDragContainer and FluentDropZone components in Razor to implement drag-and-drop functionality. It logs drag events using a DemoLogger. Ensure the necessary Blazor components are available in your project.

```Razor
<FluentDragContainer TItem="string"
                     OnDragEnd="@(e => DemoLogger.WriteLine($"{e.Source.Id} drag ended"))"
                     OnDragEnter="@(e => DemoLogger.WriteLine($"{e.Source.Id} is entered in  {e.Target.Id}"))"
                     OnDragLeave="@(e => DemoLogger.WriteLine($"{e.Source.Id} has left {e.Target.Id}"))"
                     OnDropEnd="@(e => DemoLogger.WriteLine($"{e.Source.Id} dropped in {e.Target.Id}"))">
    <FluentStack>
        <FluentDropZone Id="Item1" Draggable="true" Droppable="true">
            <div style="width: 50px; height: 50px; background-color: pink;">
                Item 1
            </div>
        </FluentDropZone>
        <FluentDropZone Id="Item2" Draggable="true" Droppable="true">
            <div style="width: 50px; height: 50px; background-color: lightgreen;">
                Item 2
            </div>
        </FluentDropZone>
    </FluentStack>
</FluentDragContainer>
```

--------------------------------

### FluentNavMenu Upgrade from v3 to v4

Source: https://www.fluentui-blazor.net/UpgradeGuide

Instructions for upgrading FluentNavMenu component when moving from v3 to v4. This involves changing tag names, parameter names, and event handlers to align with the new component structure.

```razor
To upgrade your previous menu code:
  * Change all occurrences of `<FluentNavMenuLink>` to `<FluentNavLink>`
  * Change `FluentNavMenuLink` from a self-closing tag to a tag with a closing tag
  * Move the `FluentNavMenuLink` `Text` parameter content to in between the opening and closing tag
  * Change any `@onclick` occurrences to `OnClick`
  * Change all occurrences of `FluentNavMenuGroup` to `FluentNavGroup`
  * Replace the `Text` parameter with `Title`
```

--------------------------------

### DataGrid Column Headers with DataAnnotations in Razor

Source: https://www.fluentui-blazor.net/datagrid-header-generation

Configures DataGrid column headers using System.ComponentModel.DataAnnotations.DisplayAttribute on class properties. This is a declarative approach for defining display names. It requires the `System.ComponentModel.DataAnnotations` namespace.

```Razor
@using System.ComponentModel.DataAnnotations
<FluentDataGrid Items="@people">
    <PropertyColumn Property="@(p => p.PersonId)" Sortable="true" />
    <PropertyColumn Property="@(p => p.Name)" Sortable="true" />
    <PropertyColumn Property="@(p => p.BirthDate)" Format="yyyy-MM-dd" Sortable="true" />
</FluentDataGrid>

@code {
    public class Person
    {
        public Person(int personId, string name, DateOnly birthDate)
        {
            PersonId = personId;
            Name = name;
            BirthDate = birthDate;
        }

        [Display(Name="Identity")]
        public int PersonId { get; set; }

        [Display(Name = "Full _name")]
        public string Name { get; set; }

        [Display(Name = "Birth date")]
        public DateOnly BirthDate { get; set; }
    }

    IQueryable<Person> people = new[]
    {
        new Person(10895, "Jean Martin", new DateOnly(1985, 3, 16)),
        new Person(10944, "António Langa", new DateOnly(1991, 12, 1)),
        new Person(11203, "Julie Smith", new DateOnly(1958, 10, 10)),
        new Person(11205, "Nur Sari", new DateOnly(1922, 4, 27)),
        new Person(11898, "Jose Hernandez", new DateOnly(2011, 5, 3)),
        new Person(12130, "Kenji Sato", new DateOnly(2004, 1, 9)),
    }.AsQueryable();
}
```

--------------------------------

### Large Tooltip with Custom Max Width and Delay

Source: https://www.fluentui-blazor.net/Tooltip

Illustrates a Fluent UI Blazor tooltip with a specified maximum width and a delay before appearing. This example uses a long text content for the tooltip and positions it at the bottom.

```razor
<FluentIcon Id="myLongText" Icon="Icons.Regular.Size24.Notepad" />

<FluentTooltip Anchor="myLongText"
               Delay="300"
               MaxWidth="400px"
               Position="TooltipPosition.Bottom">@content</FluentTooltip>

@code {
    string content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                     "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                     "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut " +
                     "aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in " +
                     "voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint " +
                     "occaecat cupidatat non proident, sunt in culpa qui officia " +
                     "deserunt mollit anim id est laborum";

}
```

--------------------------------

### Fluent Blazor AppBar with Smaller Icons and Search

Source: https://www.fluentui-blazor.net/AppBar

This Razor component demonstrates how to configure the Fluent UI Blazor AppBar to use smaller icons and include a search functionality within a popover. It utilizes CSS variables for icon sizing and conditional rendering for the search bar based on user interaction.

```Razor
<style>
    .fluent-appbar.smallicons {
    --appbar-item-size: 58;
    }
</style>
@{ 
    string stylevalue = $"background-color: var(--neutral-layer-3); overflow: auto; resize: {(_vertical ? \"vertical; width: 78px; height: 300px;\" : \"horizontal; width: 380px; height: 58px;\")}  padding: 10px;";
}
<FluentStack Orientation="Orientation.Vertical" Style="height: 100%;">
    <FluentStack Orientation="Orientation.Horizontal">
    <FluentSwitch @bind-Value="_showSearch" CheckedMessage="Show" UncheckedMessage="Hide" Label="Show search in popover" />
    <FluentSwitch @bind-Value="_vertical" CheckedMessage="Vertical" UncheckedMessage="Horizontal" Label="Orientation" />
    </FluentStack>
    <div class="smallicons" style="@stylevalue">
        @{
            var wh = _vertical ? "height: 100%; width: 58px;" : "width: 100%;";
        }
        <FluentAppBar Orientation="@(_vertical ? Orientation.Vertical : Orientation.Horizontal)" Class="smallicons" Style="@($"{wh} background-color: var(--neutral-layer-2);")" PopoverVisibilityChanged="HandlePopover" PopoverShowSearch="@_showSearch">

            <FluentAppBarItem Href="/AppBarDefault"
                                Match="NavLinkMatch.All"
                                IconRest="ResourcesIcon()"
                                IconActive="ResourcesIcon(active: true)"
                                Text="Resources" />
            <FluentAppBarItem Href="/AppBar"
                                IconRest="ConsoleLogsIcon()"
                                IconActive="ConsoleLogsIcon(active: true)"
                                Text="Console Logs" />

            <FluentAppBarItem Href="/StructuredLogs"
                                IconRest="StructuredLogsIcon()"
                                IconActive="StructuredLogsIcon(active: true)"
                                Text="Logs"
                                Tooltip="Structured Logs"/>
            <FluentAppBarItem Href="/Traces"
                                IconRest="TracesIcon()"
                                IconActive="TracesIcon(active: true)"
                                Text="Traces" />
            <FluentAppBarItem Href="/Metrics"
                                IconRest="MetricsIcon()"
                                IconActive="MetricsIcon(active: true)"
                                Text="Metrics" />
            <FluentAppBarItem Href="/AppBarPage"
                                IconRest="ResourcesIcon()"
                                IconActive="ResourcesIcon(active: true)"
                                Text="Resources 2" />
            <FluentAppBarItem Href="/AppBar"
                                IconRest="ConsoleLogsIcon()"
                                IconActive="ConsoleLogsIcon(active: true)"
                                Text="Console Logs 2" />

            <FluentAppBarItem Href="/StructuredLogs"
                                IconRest="StructuredLogsIcon()"
                                IconActive="StructuredLogsIcon(active: true)"
                                Text="Structured Logs 2" />
            <FluentAppBarItem Href="/Traces"
                                IconRest="TracesIcon()"
                                IconActive="TracesIcon(active: true)"
                                Text="Traces 2" />
            <FluentAppBarItem Href="/Metrics"
                                IconRest="MetricsIcon()"
                                IconActive="MetricsIcon(active: true)"
                                Text="Metrics 2" />
            <FluentAppBarItem Href="/AppBarPage"
                                IconRest="ResourcesIcon()"
                                IconActive="ResourcesIcon(active: true)"
                                Text="Resources 3" />
            <FluentAppBarItem Href="/AppBar"
                                IconRest="ConsoleLogsIcon()"
                                IconActive="ConsoleLogsIcon(active: true)"
                                Text="Console Logs 3" />

            <FluentAppBarItem Href="/StructuredLogs"
                                IconRest="StructuredLogsIcon()"
                                IconActive="StructuredLogsIcon(active: true)"
                                Text="Structured Logs 3" />
            <FluentAppBarItem Href="/Traces"
                                IconRest="TracesIcon()"
                                IconActive="TracesIcon(active: true)"
                                Text="Traces 3" />
            <FluentAppBarItem Href="/Metrics"
                                IconRest="MetricsIcon()"
                                IconActive="MetricsIcon(active: true)"
                                Text="Metrics 3" />
        </FluentAppBar>
    </div>
</FluentStack>


@code {
    private bool _vertical = true;
    private bool _showSearch = true;

    // Placeholder for icon methods, replace with actual icon implementations
    private string ResourcesIcon(bool active = false) => active ? "icon-resources-active" : "icon-resources";
    private string ConsoleLogsIcon(bool active = false) => active ? "icon-console-active" : "icon-console";
    private string StructuredLogsIcon(bool active = false) => active ? "icon-structured-active" : "icon-structured";
    private string TracesIcon(bool active = false) => active ? "icon-traces-active" : "icon-traces";
    private string MetricsIcon(bool active = false) => active ? "icon-metrics-active" : "icon-metrics";

    private void HandlePopover(PopoverVisibilityEventArgs args) {
        // Implement logic for popover visibility changes if needed
    }
}
```

--------------------------------

### FluentAnchoredRegion: RTL Start & End Positioning (Razor)

Source: https://www.fluentui-blazor.net/AnchoredRegion

Demonstrates how to use FluentAnchoredRegion to position content relative to an anchor element within a viewport, specifically configured for Right-to-Left (RTL) layouts. It shows two regions, one anchored to the start and another to the end of the anchor, within a scrollable and resizable div. The 'dir="rtl"' attribute on the viewport is crucial for correct RTL behavior. Inputs include anchor and viewport IDs, and positioning modes. Outputs are visually positioned child elements.

```Razor
<div id="viewport-rtl-se" dir="rtl" style="position:relative;height:400px;width:400px;background: var(--neutral-layer-4);overflow:auto;resize:both;">
    <FluentButton Appearance=Appearance.Neutral id="anchor-rtl-se" style="margin-right:100px;margin-top:100px">anchor</FluentButton>
    <FluentAnchoredRegion Anchor="anchor-rtl-se" Viewport="viewport-rtl-se"
                          HorizontalPositioningMode="AxisPositioningMode.Locktodefault"
                          HorizontalDefaultPosition="HorizontalPosition.Start"
                          HorizontalScaling="AxisScalingMode.Content"
                          VerticalPositioningMode="AxisPositioningMode.Uncontrolled"
                          VerticalDefaultPosition="VerticalPosition.Unset"
                          VerticalScaling="AxisScalingMode.Content"
                          AutoUpdateMode="AutoUpdateMode.Anchor">
        <div style="height:100px;width:100px;background:var(--highlight-bg);" />
    </FluentAnchoredRegion>
    <FluentAnchoredRegion Anchor="anchor-rtl-se" Viewport="viewport-rtl-se"
                          HorizontalPositioningMode="AxisPositioningMode.Locktodefault"
                          HorizontalDefaultPosition="HorizontalPosition.End"
                          HorizontalScaling="AxisScalingMode.Content"
                          VerticalPositioningMode="AxisPositioningMode.Uncontrolled"
                          VerticalDefaultPosition="VerticalPosition.Unset"
                          VerticalScaling="AxisScalingMode.Content"
                          AutoUpdateMode="AutoUpdateMode.Anchor">
        <div style="height:100px;width:100px;background:var(--neutral-layer-2);" />
    </FluentAnchoredRegion>
</div>
```

--------------------------------

### Importing Icons and Emoji in Blazor

Source: https://www.fluentui-blazor.net/WhatsNew-Before412

This code snippet demonstrates how to add the necessary `@using` statements to your `_Imports.razor` file to correctly import the Icons and Emoji namespaces. This is required starting from version 4.11.0 of the Fluent UI Blazor packages.

```razor
@using Icons = Microsoft.FluentUI.AspNetCore.Components.Icons
@* add line below only if you are using the Emoji package *@
@using Emoji = Microsoft.FluentUI.AspNetCore.Components.Emoji
```

--------------------------------

### Toast Showing Methods

Source: https://www.fluentui-blazor.net/ToastService

This section covers the methods used to display different types of toasts, including custom, download, error, event, info, mention, progress, success, and upload toasts. It also details a generic method for showing toasts with custom parameters.

```APIDOC
## Toast Showing Methods

### Description
These methods are used to display various types of toasts to the user. You can show predefined types like error, success, or info, as well as custom toasts with specific content and actions.

### Methods

*   **`ShowCommunicationToast(ToastParameters<CommunicationToastContent> parameters)`**
    *   **Description:** Shows a communication toast with the provided parameters.
    *   **Parameters:**
        *   `parameters` (ToastParameters<CommunicationToastContent>) - Required. The parameters for the communication toast.
    *   **Returns:** void

*   **`ShowConfirmationToast(ToastParameters<ConfirmationToastContent> parameters)`**
    *   **Description:** Shows a confirmation toast with the provided parameters.
    *   **Parameters:**
        *   `parameters` (ToastParameters<ConfirmationToastContent>) - Required. The parameters for the confirmation toast.
    *   **Returns:** void

*   **`ShowCustom(string title, int? timeout, string topAction, EventCallback<ToastResult>? callback, (Icon Value, Color Color)? icon)`**
    *   **Description:** Shows a simple custom confirmation toast. It displays an icon, title, and a close button or action.
    *   **Parameters:**
        *   `title` (string) - Required. The title text for the toast.
        *   `timeout` (int?) - Optional. The duration in milliseconds before the toast automatically closes.
        *   `topAction` (string) - Optional. Text for an action button at the top of the toast.
        *   `callback` (EventCallback<ToastResult>?) - Optional. An event callback triggered when the toast is interacted with or closed.
        *   `icon` ((Icon Value, Color Color)?) - Optional. The icon to display, along with its color.
    *   **Returns:** void

*   **`ShowDownload(string title, int? timeout, string topAction, EventCallback<ToastResult>? callback)`**
    *   **Description:** Shows a simple download confirmation toast. It displays an icon, title, and a close button or action.
    *   **Parameters:**
        *   `title` (string) - Required. The title text for the toast.
        *   `timeout` (int?) - Optional. The duration in milliseconds before the toast automatically closes.
        *   `topAction` (string) - Optional. Text for an action button at the top of the toast.
        *   `callback` (EventCallback<ToastResult>?) - Optional. An event callback triggered when the toast is interacted with or closed.
    *   **Returns:** void

*   **`ShowError(string title, int? timeout, string topAction, EventCallback<ToastResult>? callback)`**
    *   **Description:** Shows a simple error confirmation toast. It displays an icon, title, and a close button or action.
    *   **Parameters:**
        *   `title` (string) - Required. The title text for the toast.
        *   `timeout` (int?) - Optional. The duration in milliseconds before the toast automatically closes.
        *   `topAction` (string) - Optional. Text for an action button at the top of the toast.
        *   `callback` (EventCallback<ToastResult>?) - Optional. An event callback triggered when the toast is interacted with or closed.
    *   **Returns:** void

*   **`ShowEvent(string title, int? timeout, string topAction, EventCallback<ToastResult>? callback)`**
    *   **Description:** Shows a simple event confirmation toast. It displays an icon, title, and a close button or action.
    *   **Parameters:**
        *   `title` (string) - Required. The title text for the toast.
        *   `timeout` (int?) - Optional. The duration in milliseconds before the toast automatically closes.
        *   `topAction` (string) - Optional. Text for an action button at the top of the toast.
        *   `callback` (EventCallback<ToastResult>?) - Optional. An event callback triggered when the toast is interacted with or closed.
    *   **Returns:** void

*   **`ShowInfo(string title, int? timeout, string topAction, EventCallback<ToastResult>? callback)`**
    *   **Description:** Shows a simple information confirmation toast. It displays an icon, title, and a close button or action.
    *   **Parameters:**
        *   `title` (string) - Required. The title text for the toast.
        *   `timeout` (int?) - Optional. The duration in milliseconds before the toast automatically closes.
        *   `topAction` (string) - Optional. Text for an action button at the top of the toast.
        *   `callback` (EventCallback<ToastResult>?) - Optional. An event callback triggered when the toast is interacted with or closed.
    *   **Returns:** void

*   **`ShowMention(string title, int? timeout, string topAction, EventCallback<ToastResult>? callback)`**
    *   **Description:** Shows a simple mention confirmation toast. It displays an icon, title, and a close button or action.
    *   **Parameters:**
        *   `title` (string) - Required. The title text for the toast.
        *   `timeout` (int?) - Optional. The duration in milliseconds before the toast automatically closes.
        *   `topAction` (string) - Optional. Text for an action button at the top of the toast.
        *   `callback` (EventCallback<ToastResult>?) - Optional. An event callback triggered when the toast is interacted with or closed.
    *   **Returns:** void

*   **`ShowProgress(string title, int? timeout, string topAction, EventCallback<ToastResult>? callback)`**
    *   **Description:** Shows a simple progress confirmation toast. It displays an icon, title, and a close button or action.
    *   **Parameters:**
        *   `title` (string) - Required. The title text for the toast.
        *   `timeout` (int?) - Optional. The duration in milliseconds before the toast automatically closes.
        *   `topAction` (string) - Optional. Text for an action button at the top of the toast.
        *   `callback` (EventCallback<ToastResult>?) - Optional. An event callback triggered when the toast is interacted with or closed.
    *   **Returns:** void

*   **`ShowProgressToast(ToastParameters<ProgressToastContent> parameters)`**
    *   **Description:** Shows a progress toast with the provided parameters.
    *   **Parameters:**
        *   `parameters` (ToastParameters<ProgressToastContent>) - Required. The parameters for the progress toast.
    *   **Returns:** void

*   **`ShowSuccess(string title, int? timeout, string topAction, EventCallback<ToastResult>? callback)`**
    *   **Description:** Shows a simple success confirmation toast. It displays an icon, title, and a close button or action.
    *   **Parameters:**
        *   `title` (string) - Required. The title text for the toast.
        *   `timeout` (int?) - Optional. The duration in milliseconds before the toast automatically closes.
        *   `topAction` (string) - Optional. Text for an action button at the top of the toast.
        *   `callback` (EventCallback<ToastResult>?) - Optional. An event callback triggered when the toast is interacted with or closed.
    *   **Returns:** void

*   **`ShowToast(ToastIntent intent, string title, int? timeout, string topAction, EventCallback<ToastResult>? callback)`**
    *   **Description:** Shows a toast using the supplied parameters.
    *   **Parameters:**
        *   `intent` (ToastIntent) - Required. The intent of the toast.
        *   `title` (string) - Required. The title text for the toast.
        *   `timeout` (int?) - Optional. The duration in milliseconds before the toast automatically closes.
        *   `topAction` (string) - Optional. Text for an action button at the top of the toast.
        *   `callback` (EventCallback<ToastResult>?) - Optional. An event callback triggered when the toast is interacted with or closed.
    *   **Returns:** void

*   **`ShowToast<T, TData>(ToastParameters<TData> parameters)`**
    *   **Description:** Shows a toast with a specified component type and data.
    *   **Parameters:**
        *   `parameters` (ToastParameters<TData>) - Required. The parameters for the toast, including data.
    *   **Returns:** void

*   **`ShowToast<TContent>(Type component, ToastParameters parameters, TContent content)`**
    *   **Description:** Shows a toast with a specified component type as the body, passing the given parameters and content.
    *   **Parameters:**
        *   `component` (Type) - Required. The type of the Blazor component to use for the toast body.
        *   `parameters` (ToastParameters) - Required. The parameters for the toast.
        *   `content` (TContent) - Required. The content to pass to the toast component.
    *   **Returns:** void

*   **`ShowUpload(string title, int? timeout, string topAction, EventCallback<ToastResult>? callback)`**
    *   **Description:** Shows a simple upload confirmation toast. It displays an icon, title, and a close button or action.
    *   **Parameters:**
        *   `title` (string) - Required. The title text for the toast.
        *   `timeout` (int?) - Optional. The duration in milliseconds before the toast automatically closes.
        *   `topAction` (string) - Optional. Text for an action button at the top of the toast.
        *   `callback` (EventCallback<ToastResult>?) - Optional. An event callback triggered when the toast is interacted with or closed.
    *   **Returns:** void

```

--------------------------------

### Use Fluent UI Emoji in Blazor

Source: https://www.fluentui-blazor.net/ProjectSetup

Renders a Fluent UI Emoji within a Blazor component. It shows how to specify the emoji group, style, skintone, and name using the FluentEmoji component.

```razor
<FluentEmoji Value="@(new Emojis.PeopleBody.Color.Default.Artist())" />

```

--------------------------------

### Blazor Example for FluentDesignTheme Component

Source: https://www.fluentui-blazor.net/DesignTheme

Illustrates how to configure the FluentDesignTheme Blazor component to set the application's mode (e.g., Dark, Light) and color (e.g., Word, CustomColor). Uses enums for mode and predefined or custom colors.

```razor
<FluentDesignTheme Mode="DesignThemeModes.Dark" OfficeColor="OfficeColor.Word" />
<FluentDesignTheme Mode="DesignThemeModes.Light" CustomColor="#ff0000" />
```

--------------------------------

### FluentAnchoredRegion: Size to Anchor (Razor)

Source: https://www.fluentui-blazor.net/AnchoredRegion

Demonstrates how FluentAnchoredRegion scales to the anchor's size when positioned dynamically relative to a viewport. This example uses `AxisScalingMode.Anchor` for both vertical and horizontal scaling.

```Razor
<div id="viewport-anchor-sized" style="position:relative;height:400px;width:400px;background: var(--neutral-layer-4);overflow:auto;resize:both;">
    <FluentAnchoredRegion Anchor="anchor-anchor-sized" Viewport="viewport-anchor-sized"
                          VerticalPositioningMode="AxisPositioningMode.Dynamic"
                          VerticalScaling="AxisScalingMode.Anchor"
                          HorizontalPositioningMode="AxisPositioningMode.Dynamic"
                          HorizontalScaling="AxisScalingMode.Anchor"
                          HorizontalInset="false"
                          HorizontalDefaultPosition="HorizontalPosition.Unset"
                          VerticalDefaultPosition="VerticalPosition.Unset"
                          AutoUpdateMode="AutoUpdateMode.Anchor">
        <div style="height:100%;width:100%;background:var(--highlight-bg);" />
    </FluentAnchoredRegion>
    <FluentButton Appearance=Appearance.Neutral id="anchor-anchor-sized" style="margin-left:100px;margin-top:100px">anchor</FluentButton>
</div>
```

--------------------------------

### Remove RCL Target Configuration

Source: https://www.fluentui-blazor.net/UpgradeGuide

Shows the code snippet that needs to be removed from the project file (`.csproj`) when using the library from a Razor Class Library (RCL) for version 2. This target is no longer necessary.

```xml
<Target Name="DisableAnalyzers" BeforeTargets="CoreCompile">
	<ItemGroup>
		<Analyzer Remove="@(Analyzer)" Condition="'%(Filename)' == 'Microsoft.Fast.Components.FluentUI.Configuration'" />
	</ItemGroup>
</Target>
```

--------------------------------

### Fluent UI Blazor Message Service Example (Razor)

Source: https://www.fluentui-blazor.net/MessageBar

This Razor component demonstrates how to use the `IMessageService` and `IDialogService` to display various message types. It includes functions for adding messages to the top bar, notification center, and dialogs, as well as handling non-dismissible messages and clearing all alerts. Dependencies include `IMessageService`, `IDialogService`, and Fluent UI Blazor components.

```razor
@inject IMessageService MessageService
@inject IDialogService DialogService
@using FluentUI.Demo.Shared.Pages.Dialog.Examples;

<FluentButton OnClick=@AddInTopBar Appearance="Appearance.Accent">Add on top</FluentButton>
<FluentButton OnClick=@AddInNotificationCenter Appearance="Appearance.Accent">Add in Notification Center</FluentButton>
<FluentButton OnClick=@AddInDialog Appearance="Appearance.Accent">Add in a dialog</FluentButton>
<FluentButton OnClick=@AddNonDismissibleMessage Appearance="Appearance.Accent">Add non-dismissible on top</FluentButton>

<FluentButton Appearance="Appearance.Neutral" OnClick=@((e) => MessageService.Clear())>Clear all alerts</FluentButton>

@code
{
    SimplePerson simplePerson = new()
        {
            Firstname = "Dan",
            Lastname = "Sanderson",
            Age = 42,
        };

    ActionLink<Message> link = new()
        {
            Text = "Learn more",
            Href = "https://bing.com",
            OnClick = (e) => { DemoLogger.WriteLine($"Message 'learn more' clicked"); return Task.CompletedTask; }
        };

    int counter = 0;

    async Task AddInTopBar()
    {
        var message = $"Simple message #{counter++}";
        var type = Enum.GetValues<MessageIntent>()[counter % 4];
        await MessageService.ShowMessageBarAsync(message, type, App.MESSAGES_TOP);
    }

    void AddInNotificationCenter()
    {
        MessageService.ShowMessageBar(options =>
        {
            options.Intent = Enum.GetValues<MessageIntent>()[counter % 4];
            options.Title = $"Simple message #{counter++}";
            options.Body = MessageBarSamples.OneRandomMessage;
            options.Link = link;
            options.Timestamp = DateTime.Now;
            options.Section = App.MESSAGES_NOTIFICATION_CENTER;
        });
    }


    async Task AddInDialog()
    {
        MessageService.ShowMessageBar(options =>
        {
            options.Intent = Enum.GetValues<MessageIntent>()[counter % 4];
            options.Title = $"Simple message #{counter++}";
            options.Body = MessageBarSamples.OneRandomMessage;
            options.Link = link;
            options.Timestamp = DateTime.Now;
            options.Section = App.MESSAGES_DIALOG;
        });

        await OpenDialogAsync();
    }

    private async Task OpenDialogAsync()
    {
        DialogParameters<SimplePerson> parameters = new()
            {
                Title = $"Hi {simplePerson.Firstname}!",
                PrimaryAction = "Yes",
                PrimaryActionEnabled = false,
                SecondaryAction = "No",
                Width = "500px",
                Height = "500px",
                Content = simplePerson,
                TrapFocus = true,
                Modal = true,
            };

        IDialogReference dialog = await DialogService.ShowDialogAsync<SimpleDialog>(simplePerson, parameters);
        DialogResult? result = await dialog.Result;
    }

    private async Task AddNonDismissibleMessage()
    {
        var message = $"Simple non-dismissible message #{counter++}";
        var type = Enum.GetValues<MessageIntent>()[counter % 4];
        await MessageService.ShowMessageBarAsync(options =>
        {
            options.Title = message;
            options.Intent = type;
            options.Section = App.MESSAGES_TOP;
            options.AllowDismiss = false;
        });
    }
}

```

--------------------------------

### Define Custom Icon Class

Source: https://www.fluentui-blazor.net/IconsAndEmoji

Provides an example of how to create a custom icon by defining a static class that inherits from the `Icon` base class. This allows you to embed custom SVG content directly into your application.

```csharp
public static class MyIcons
{
    public class SettingsEmail : Icon { public SettingsEmail() : base("SettingsEmail", IconVariant.Regular, IconSize.Size20, "<svg width=\"20\" height=\"19\" viewBox=\"0 0 20 19\" fill=\"none\" xmlns=\"http://www.w3.org/2000/svg\"><path d=\"M15.6251 2.5H4.37508L4.2214 2.50428C2.79712 2.58396 1.66675 3.76414 1.66675 5.20833V13.125L1.67103 13.2787C1.75071 14.7029 2.93089 15.8333 4.37508 15.8333H9.76425C9.91725 15.4818 10.1354 15.1606 10.4087 14.8873L10.7126 14.5833H4.37508L4.25547 14.5785C3.50601 14.5177 2.91675 13.8902 2.91675 13.125V6.97833L9.709 10.5531L9.78908 10.5883C9.95267 10.647 10.135 10.6353 10.2912 10.5531L17.0834 6.9775V9.17258C17.5072 9.14483 17.9362 9.21517 18.3334 9.38358V5.20833L18.3292 5.05465C18.2494 3.63038 17.0693 2.5 15.6251 2.5ZM4.37508 3.75H15.6251L15.7447 3.75483C16.4942 3.81568 17.0834 4.44319 17.0834 5.20833V5.565L10.0001 9.29375L2.91675 5.56583V5.20833L2.92158 5.08873C2.98242 4.33926 3.60994 3.75 4.37508 3.75ZM15.9167 10.5579L10.9979 15.4766C10.7112 15.7633 10.5077 16.1227 10.4093 16.5162L10.0279 18.0418C9.86208 18.7052 10.4631 19.3062 11.1265 19.1403L12.6521 18.7588C13.0455 18.6605 13.4048 18.4571 13.6917 18.1703L18.6103 13.2516C19.3542 12.5078 19.3542 11.3018 18.6103 10.5579C17.8665 9.814 16.6605 9.814 15.9167 10.5579Z\" fill=\"#212121\"/></svg>", IconVariant.Regular, IconSize.Size20, "<svg width=\"20\" height=\"19\" viewBox=\"0 0 20 19\" fill=\"none\" xmlns=\"http://www.w3.org/2000/svg\"><path d=\"M15.6251 2.5H4.37508L4.2214 2.50428C2.79712 2.58396 1.66675 3.76414 1.66675 5.20833V13.125L1.67103 13.2787C1.75071 14.7029 2.93089 15.8333 4.37508 15.8333H9.76425C9.91725 15.4818 10.1354 15.1606 10.4087 14.8873L10.7126 14.5833H4.37508L4.25547 14.5785C3.50601 14.5177 2.91675 13.8902 2.91675 13.125V6.97833L9.709 10.5531L9.78908 10.5883C9.95267 10.647 10.135 10.6353 10.2912 10.5531L17.0834 6.9775V9.17258C17.5072 9.14483 17.9362 9.21517 18.3334 9.38358V5.20833L18.3292 5.05465C18.2494 3.63038 17.0693 2.5 15.6251 2.5ZM4.37508 3.75H15.6251L15.7447 3.75483C16.4942 3.81568 17.0834 4.44319 17.0834 5.20833V5.565L10.0001 9.29375L2.91675 5.56583V5.20833L2.92158 5.08873C2.98242 4.33926 3.60994 3.75 4.37508 3.75ZM15.9167 10.5579L10.9979 15.4766C10.7112 15.7633 10.5077 16.1227 10.4093 16.5162L10.0279 18.0418C9.86208 18.7052 10.4631 19.3062 11.1265 19.1403L12.6521 18.7588C13.0455 18.6605 13.4048 18.4571 13.6917 18.1703L18.6103 13.2516C19.3542 12.5078 19.3542 11.3018 18.6103 10.5579C17.8665 9.814 16.6605 9.814 15.9167 10.5579Z\" fill=\"#212121\"/></svg>" ) { } }
```

--------------------------------

### Fluent Blazor Button with Icons and Loading

Source: https://www.fluentui-blazor.net/Button

Illustrates how to add icons to Fluent Blazor Buttons, both at the start and end of the button text, and within the default slot. It also shows how to combine icons with the loading state.

```Razor
<p>With icon at Start or End</p>
<FluentStack>
    <FluentButton IconStart="@(new Icons.Regular.Size16.Globe())">
        Button
    </FluentButton>
    <FluentButton IconStart="@(new Icons.Regular.Size16.Globe())"
                  Appearance="Appearance.Accent">
        Button
    </FluentButton>
    <FluentButton IconEnd="@(new Icons.Regular.Size16.Globe())">
        Button
    </FluentButton>
    <FluentButton IconEnd="@(new Icons.Regular.Size16.Globe())"
                  Appearance="Appearance.Accent">
        Button
    </FluentButton>
</FluentStack>

<p>With icon in default slot</p>
<FluentButton IconEnd="@(new Icons.Regular.Size16.Globe())"
              Title="Globe"
              OnClick="@(() => DemoLogger.WriteLine("Button clicked"))" />

<p>With icon in the content. By doing it this way, it is possible to specify a <code>Color</code> for the icon.</p>
<FluentButton>
    <FluentIcon Value="@(new Icons.Regular.Size32.Globe())" Color="Color.Error" Slot="start" />
    Button
</FluentButton>

<p>With icon and loading</p>
<FluentStack>
    <FluentButton IconStart="@(new Icons.Regular.Size16.ArrowClockwise())"
                  Loading="@loading"
                  OnClick="@StartLoadingAsync">
        Refresh
    </FluentButton>
    <FluentButton IconStart="@(new Icons.Regular.Size16.ArrowClockwise())"
                  Appearance="Appearance.Accent"
                  Loading="@loading"
                  OnClick="@StartLoadingAsync">
        Refresh
    </FluentButton>
</FluentStack>

@code {

    bool loading = false;

    async Task StartLoadingAsync()
    {
        loading = true;
        await DataSource.WaitAsync(2000, () => loading = false);
    }
}
```

--------------------------------

### Fluent Wizard Component with Event Handling (Razor)

Source: https://www.fluentui-blazor.net/Wizard

Demonstrates the Fluent UI Blazor Wizard component, including defining steps, handling step change events, and managing the completion action. Requires the Fluent UI Blazor library.

```csharp
<FluentWizard IsTop="IsTop" StepSequence="StepSequence" OnStepChange="OnStepChange" OnFinished="OnFinishedAsync">
    <Steps>
        <FluentWizardStep Label="Intro">
            aliquam tempus sapien. Nam rutrum mi at enim mattis, non mollis diam molestie.
            Cras sodales dui libero, sit amet cursus sapien elementum ac. Nulla euismod nisi sem.
        </FluentWizardStep>
        <FluentWizardStep Label="Get Started">
            
        </FluentWizardStep>
        <FluentWizardStep Label="Set budget">
            
        </FluentWizardStep>
        <FluentWizardStep Label="Summary">
            
        </FluentWizardStep>
    </Steps>
</FluentWizard>

@code
{
    bool IsTop = false;
    WizardStepSequence StepSequence = WizardStepSequence.Linear;

    void OnStepChange(FluentWizardStepChangeEventArgs e)
    {
        DemoLogger.WriteLine($"Go to step {e.TargetLabel} (#{e.TargetIndex})");
    }

    async Task OnFinishedAsync()
    {
        await DialogService.ShowInfoAsync("Wizard completed");
    }
}
```

--------------------------------

### Fluent UI Blazor v2 Icon/Emoji Project Configuration

Source: https://www.fluentui-blazor.net/UpgradeGuide

Shows the deprecated `<PropertyGroup>` configuration in `.csproj` files for managing icon and emoji assets in Fluent UI Blazor v2. This configuration should be removed when upgrading to v3.

```xml
<PropertyGroup>
	<PublishFluentIconAssets>true</PublishFluentIconAssets>
	<FluentIconSizes>10,12,16,20,24,28,32,48</FluentIconSizes>
	<FluentIconVariants>Filled,Regular</FluentIconVariants>
	<PublishFluentEmojiAssets>true</PublishFluentEmojiAssets>
	<FluentEmojiGroups>Activities,Animals_Nature,Flags,Food_Drink,Objects,People_Body,Smileys_Emotion,Symbols,Travel_Places</FluentEmojiGroups>
	<FluentEmojiStyles>Color,Flat,HighContrast</FluentEmojiStyles>
</PropertyGroup>
```

--------------------------------

### FluentRating Component Example in Razor

Source: https://www.fluentui-blazor.net/Rating

This Razor component demonstrates how to use the FluentRating component with various configuration options. It includes FluentStack for layout and FluentSelect/FluentNumberField for controlling component properties. The @code block handles the component's logic and state management.

```Razor
<h4>Example</h4>

<FluentStack VerticalAlignment="VerticalAlignment.Center" Style="margin-left: 32px;">
    <FluentRating Max="@_maxValue"
                  @bind-Value="@_value"
                  IconFilled="@_iconFilled"
                  IconOutline="@_iconOutline"
                  ReadOnly="@_readOnly"
                  IconColor="@_iconColor"
                  Disabled="@_disabled" ,
                  AllowReset="@_allowReset" />

    <FluentLabel>Value: @_value</FluentLabel>
</FluentStack>

<hr />

<FluentStack HorizontalGap="100">
    <div>
        <FluentSelect Label="ReadOnly"
                      @bind-SelectedOption="@_readOnly"
                      Style="@FixedWidth"
                      Items="@(new[] { true, false })" />

        <FluentSelect Label="AllowReset"
                      @bind-SelectedOption="@_allowReset"
                      Style="@FixedWidth"
                      Items="@(new[] { true, false })" />

        <FluentSelect Label="Disabled"
                      @bind-SelectedOption="@_disabled"
                      Style="@FixedWidth"
                      Items="@(new[] { true, false })" />
    </div>
    <div>
        <FluentNumberField TValue="int"
                           Label="Max Value"
                           Style="@FixedWidth"
                           Min="1"
                           Max="20"
                           @bind-Value="@_maxValue" />

        <FluentSelect Label="Color"
                      @bind-SelectedOption="@_iconColor"
                      Style="@FixedWidth"
                      Items="@(Enum.GetValues<Color>().Where(i => i != Color.Custom))"
                      OptionValue="@(i => i.ToAttributeValue())" />

        <FluentSelect Label="Icons"
                      TOption="string"
                      SelectedOptionChanged="@SetIcon"
                      Style="@FixedWidth"
                      Items="IconsName" />
    </div>
</FluentStack>

@code
{
    readonly string FixedWidth = "max-width: 100px; min-width: 100px; margin: 10px;";
    readonly List<string> IconsName = ["Heart", "Star", "Alert", "PersonCircle"];
    readonly Icon[] IconsFilled = new Icon[]
    {
        new Icons.Filled.Size20.Heart(),
        new Icons.Filled.Size20.Star(),
        new Icons.Filled.Size20.Alert(),
        new Icons.Filled.Size20.PersonCircle(),
    };
    readonly Icon[] IconsOutline = new Icon[]
    {
        new Icons.Regular.Size20.Heart(),
        new Icons.Regular.Size20.Star(),
        new Icons.Regular.Size20.Alert(),
        new Icons.Regular.Size20.PersonCircle(),
    };

    bool _readOnly = false;
    bool _disabled = false;
    bool _allowReset = false;
    int _maxValue = 10;
    int _value = 2;
    Color _iconColor = Color.Error;

    Icon _iconFilled = new Icons.Filled.Size20.Star();
    Icon _iconOutline = new Icons.Regular.Size20.Star();
    

    void SetIcon(string? name)
    {
        var index = name is null ? 0 : IconsName.IndexOf(name);

        _iconFilled = IconsFilled[index];
        _iconOutline = IconsOutline[index];
    }

    protected override void OnParametersSet() => SetIcon(null);
}
```

--------------------------------

### FluentFooter Component Usage in Blazor

Source: https://www.fluentui-blazor.net/Footer

This Blazor code snippet demonstrates how to use the FluentFooter component to create a footer bar. It includes the Footer start text, a FluentSpacer for layout, and Footer end text. The component can be styled using standard CSS.

```razor
<FluentFooter>
    Footer start text
    <FluentSpacer />
    Footer end text
</FluentFooter>
```

--------------------------------

### Create Manual FluentDataGrid in Razor

Source: https://www.fluentui-blazor.net/datagrid-manual

This snippet shows how to manually define a data grid in Razor, setting up headers and rows directly. It uses `FluentDataGrid` with `DisplayMode.Table` and `GenerateHeaderOption.None` for custom header control. This method offers more flexibility than declarative approaches for specific layout and sorting needs.

```razor
<FluentDataGrid id="manualGrid" GenerateHeader="GenerateHeaderOption.None" DisplayMode="DataGridDisplayMode.Table" TGridItem="string" role="grid" Style="width: 600px;">
    <FluentDataGridRow RowType="DataGridRowType.Header" >
        <FluentDataGridCell GridColumn=1 CellType="DataGridCellType.ColumnHeader" Style="width: 50%;">Column 1</FluentDataGridCell>
        <FluentDataGridCell GridColumn=2 CellType="DataGridCellType.ColumnHeader" Style="width: 50%;">Column 2</FluentDataGridCell>
    </FluentDataGridRow>
    <FluentDataGridRow>
        <FluentDataGridCell GridColumn=1>1.1</FluentDataGridCell>
        <FluentDataGridCell GridColumn=2>1.2</FluentDataGridCell>
    </FluentDataGridRow>
    <FluentDataGridRow>
        <FluentDataGridCell GridColumn=1>2.1</FluentDataGridCell>
        <FluentDataGridCell GridColumn=2> 2.2</FluentDataGridCell>
    </FluentDataGridRow>
</FluentDataGrid>
```

--------------------------------

### Three-State Checkbox Examples in Blazor

Source: https://www.fluentui-blazor.net/Checkbox

Illustrates the three-state functionality of Fluent Blazor Checkbox, including indeterminate states. Shows how to bind to both the value and check state. Requires Fluent UI Blazor component library.

```Razor
<FluentStack Style="margin: 20px;">
    <FluentCheckbox Id="check1" ThreeState="true" @bind-Value="@value1" @bind-CheckState="@state1" Label="ThreeState = true" Style="min-width: 250px;" />
    <div>
        Value = @value1 - CheckState = @(state1?.ToString() ?? "null (Indeterminate)")
    </div>
</FluentStack>

<FluentStack Style="margin: 20px;">
    <FluentCheckbox Id="check2" ThreeState="false" @bind-Value="@value2" Label="ThreeState = false" Style="min-width: 250px;" />
    <div>
        Value = @value2
    </div>
</FluentStack>

<FluentStack Style="margin: 20px;">
    <FluentCheckbox Id="check3" ThreeState="true" @bind-Value="@value3" @bind-CheckState="@state3" ShowIndeterminate="false" Label="ShowIndeterminate = false" Style="min-width: 250px;" />
    <div>
        Value = @value3 - CheckState = @(state3?.ToString() ?? "null (Indeterminate)")
    </div>
</FluentStack>

@code {
    bool value1, value2, value3;
    bool? state1 = false, state3 = null;
}
```

--------------------------------

### Customize Column Resize Labels in Blazor

Source: https://www.fluentui-blazor.net/datagrid

Example of customizing the labels and icons for column resizing in a FluentDataGrid. This allows for custom text and the removal of icons for specific resize actions.

```csharp
ColumnResizeLabels resizeLabels = ColumnResizeLabels.Default with
{
    DiscreteLabel = "Abcd efg",
    ResetAriaLabel = "hij klm",
    Icon = null
};

```

--------------------------------

### FluentMessageBar Class Parameters

Source: https://www.fluentui-blazor.net/MessageBar

Detailed documentation for the parameters of the FluentMessageBar class.

```APIDOC
## FluentMessageBar Class Parameters

### Description
This section details the configurable parameters for the `FluentMessageBar` component, used for displaying individual messages.

### Method
N/A

### Endpoint
N/A

### Parameters
#### Path Parameters
N/A

#### Query Parameters
N/A

#### Request Body
##### `AllowDismiss`
- **Type**: `bool`
- **Default**: `True`
- **Description**: Gets or sets the ability to dismiss the notification. Default is true.

##### `ChildContent`
- **Type**: `RenderFragment?`
- **Default**: `null`
- **Description**: Gets or sets the message to be shown when not using the MessageService methods.

##### `Content`
- **Type**: `Message`
- **Default**: `null`
- **Description**: Gets or sets the actual message instance shown in the message bar.

##### `FadeIn`
- **Type**: `bool`
- **Default**: `True`
- **Description**: Gets or sets the fade in animation for the MessageBar. Default is true.

##### `Icon`
- **Type**: `Icon?`
- **Default**: `Preview`
- **Description**: Gets or sets the icon to show in the message bar based on the intent of the message. See `FluentMessageBar.Icon` for more details.

##### `IconColor`
- **Type**: `Color?`
- **Default**: `Accent`
- **Description**: Gets or sets the color of the icon. Only applied when intent is MessageBarIntent.Custom. Default is Color.Accent.

##### `Intent`
- **Type**: `MessageIntent?`
- **Default**: `Info`
- **Description**: Gets or sets the intent of the message bar. Default is MessageIntent.Info. See `MessageIntent` for more details.

##### `Timestamp`
- **Type**: `DateTime?`
- **Default**: `DateTime.Now`
- **Description**: Gets or sets the time on which the message was created. Default is DateTime.Now. Only used when MessageType is Notification.

##### `Title`
- **Type**: `string?`
- **Default**: `null`
- **Description**: Gets or sets the title. Most important info to be shown in the message bar.

##### `Type`
- **Type**: `MessageType`
- **Default**: `MessageBar`
- **Description**: Gets or sets the type of message bar. Default is MessageType.MessageBar. See `MessageType` for more details.

##### `Visible`
- **Type**: `bool`
- **Default**: `True`
- **Description**: Gets or sets the visibility of the message bar. Default is true.

### Request Example
N/A

### Response
N/A
```

--------------------------------

### Implement FluentPaginator with Custom Templates in Blazor

Source: https://www.fluentui-blazor.net/datagrid-custom-comparer-sort

This code demonstrates how to use FluentPaginator in Blazor to display custom summary and pagination text. It binds to a PaginationState object to show the total item count and current page information.

```blazor
<FluentPaginator State="@pagination">
    <SummaryTemplate>
        There are <strong>@(pagination.TotalItemCount ?? 0)</strong> rows
    </SummaryTemplate>
    <PaginationTextTemplate>
        This is page <strong>@(pagination.CurrentPageIndex + 1)</strong> out of a total of <strong>@(pagination.LastPageIndex + 1)</strong> pages
    </PaginationTextTemplate>
</FluentPaginator>

@code {
    PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
}
```

--------------------------------

### Add FluentUI Emojis Namespace

Source: https://www.fluentui-blazor.net/IconsAndEmoji

This code snippet shows the necessary `@using` directive to import the FluentUI emoji namespace for use in Blazor components. This is required starting from v4.11.0.

```csharp
@using Emojis = Microsoft.FluentUI.AspNetCore.Components.Emojis
```

--------------------------------

### FluentComponentBase Id Parameter Addition

Source: https://www.fluentui-blazor.net/UpgradeGuide

Explains the addition of the `Id` parameter to the `FluentComponentBase` class, which is used to set the `id` attribute on the root element. The `Id` parameter now only creates a value if explicitly assigned.

```csharp
The `FluentComponentBase` class now has an `Id` parameter. This `Id` parameter is used to set the `id` attribute on the root element of the component. Previously, a value was always created for the `Id` in `FluentInputBase`. Now, a value is only created for components that need an id. You can assign a value to the `Id` parameter yourself if needed.
```

--------------------------------

### Update Core Namespace in Blazor Projects

Source: https://www.fluentui-blazor.net/UpgradeGuide

Details the primary namespace change from Microsoft.Fast.Components.FluentUI to Microsoft.FluentUI.AspNetCore.Components for version 4.0.0. This requires updating all 'using' statements in code files and the _Imports.razor file to reflect the new namespace.

```csharp
using Microsoft.FluentUI.AspNetCore.Components
```

--------------------------------

### FluentProgressRing: Custom Size Variations

Source: https://www.fluentui-blazor.net/ProgressRing

Shows how to apply custom sizes to FluentProgressRing components using inline styles for both determinate and indeterminate states. The examples include rings with increasing pixel dimensions.

```Razor
<h4>Custom Sizes</h4>
<FluentStack>
    <FluentProgressRing Min="0" Max="100" Value="20"></FluentProgressRing>
    <FluentProgressRing Min="0" Max="100" Value="40" style="width: 42px; height: 42px;"></FluentProgressRing>
    <FluentProgressRing Min="0" Max="100" Value="60" style="width: 62px; height: 62px;"></FluentProgressRing>
    <FluentProgressRing Min="0" Max="100" Value="80" style="width: 82px; height: 82px;"></FluentProgressRing>
    <FluentProgressRing Min="0" Max="100" Value="100" style="width: 102px; height: 102px;"></FluentProgressRing>
</FluentStack>
<FluentStack>
    <FluentProgressRing></FluentProgressRing>
    <FluentProgressRing style="width: 42px; height: 42px;"></FluentProgressRing>
    <FluentProgressRing style="width: 62px; height: 62px;"></FluentProgressRing>
    <FluentProgressRing style="width: 82px; height: 82px;"></FluentProgressRing>
    <FluentProgressRing style="width: 102px; height: 102px;"></FluentProgressRing>
</FluentStack>
```

--------------------------------

### FluentTextArea States (Required, Disabled, ReadOnly) - Razor

Source: https://www.fluentui-blazor.net/TextArea

Demonstrates how to set various states for the FluentTextArea component, including 'Required', 'Disabled', and 'ReadOnly' attributes. Includes examples with and without content.

```Razor
<h4>Required</h4>
<FluentTextArea @bind-Value=value1 Required="true"></FluentTextArea>

<h4>Disabled</h4>
<FluentTextArea @bind-Value=value2 Disabled="true"></FluentTextArea>
<FluentTextArea @bind-Value=value3 Disabled="true">
    <span>label</span>
</FluentTextArea>
<FluentTextArea @bind-Value=value4 Disabled="true" Placeholder="placeholder"></FluentTextArea>

<h4>Read only</h4>
<FluentTextArea @bind-Value=value5 ReadOnly="true"></FluentTextArea>
<FluentTextArea @bind-Value=value6 ReadOnly="true">label</FluentTextArea>

@code {
    string? value1, value2, value3, value4, value5 = "Readonly text area", value6 = "Readonly text area";
}
```

--------------------------------

### Handle UI Events (C#)

Source: https://www.fluentui-blazor.net/AppBar

These C# methods demonstrate how to handle user interface events within a Blazor component. 'HandlePopover' logs changes in popover visibility, while 'HandleOrientationChanged' logs changes in the application's orientation (vertical or horizontal). These are essential for responding to user interactions and device changes.

```csharp
private void HandlePopover(bool visible) => DemoLogger.WriteLine($"Popover visibility changed to {visible}");

    private void HandleOrientationChanged()
    {
        DemoLogger.WriteLine($"Orientation changed to {{(_vertical ? "vertical" : "horizontal") }}");
    }
```

--------------------------------

### Blazor - Simple Sortable List Usage

Source: https://www.fluentui-blazor.net/SortableList

Example of using `FluentSortableList` in a Blazor component. It defines a list of items and uses `ItemTemplate` to render each item. The `OnUpdate` event is wired to the `SortList` method to handle reordering.

```razor
<FluentGrid Justify="JustifyContent.FlexStart" Spacing="3">
    <FluentGridItem xs="12" sm="6">
        <FluentSortableList Items="items" OnUpdate="@SortList">
            <ItemTemplate>@context.Name</ItemTemplate>
        </FluentSortableList>
    </FluentGridItem>
</FluentGrid>

@code {

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public bool Disabled { get; set; } = false;
    }

    public List<Item> items = Enumerable.Range(1, 10).Select(i => new Item { Id = i, Name = $"Item {i}" }).ToList();


    private void SortList(FluentSortableListEventArgs args)
    {
        if (args is null || args.OldIndex == args.NewIndex)
        {
            return;
        }

        var oldIndex = args.OldIndex;
        var newIndex = args.NewIndex;

        var items = this.items;
        var itemToMove = items[oldIndex];
        items.RemoveAt(oldIndex);

        if (newIndex < items.Count)
        {
            items.Insert(newIndex, itemToMove);
        }
        else
        {
            items.Add(itemToMove);
        }
    }
}
```

--------------------------------

### Fluent Toolbar Vertical Orientation (Razor)

Source: https://www.fluentui-blazor.net/Toolbar

This example demonstrates how to configure the Fluent Toolbar to display in a vertical orientation. It includes a slotted label and several buttons arranged vertically.

```Razor
<FluentToolbar id="toolbar-vertical-orientation" Orientation="Orientation.Vertical">
    <span slot="label">Span Label</span>
    <FluentButton>One</FluentButton>
    <FluentButton>Two</FluentButton>
    <FluentButton>Three</FluentButton>
</FluentToolbar>
```

--------------------------------

### Fluent UI Blazor Grid Without Breakpoints

Source: https://www.fluentui-blazor.net/Grid

This example showcases the Fluent UI Blazor Grid component used without explicit breakpoints, allowing items to wrap to the next line. It demonstrates how to set a minimum width for a grid item and how the component manages layout when space is constrained. This is useful for scenarios where content flow is more important than fixed screen-size adaptations.

```Razor
<FluentGrid Justify="JustifyContent.FlexEnd"
            Style="background-color: var(--neutral-layer-3); overflow: hidden; resize: horizontal; padding: 4px;">
    <FluentGridItem Style="min-width: 200px;">
        <FluentLabel>
            Views must be setup in the Admin Portal to use this client application hosted by my company.
        </FluentLabel>
    </FluentGridItem>
    <FluentGridItem Justify="JustifyContent.FlexEnd" Gap="10px">
        <FluentButton Appearance="Appearance.Neutral">Setup</FluentButton>
        <FluentButton Appearance="Appearance.Neutral">Documentation</FluentButton>
    </FluentGridItem>
</FluentGrid>
```

--------------------------------

### MenuButton with Different Button Appearances

Source: https://www.fluentui-blazor.net/MenuButton

This example illustrates various appearance options for the FluentMenuButton component, including Neutral, Lightweight, Outline, Stealth, and With Icon. It utilizes FluentStack for vertical arrangement and demonstrates setting different ButtonAppearance values and adding an icon.

```Razor
<FluentStack Orientation="Orientation.Vertical">
    <span>
        Neutral: <FluentMenuButton @ref=menubutton ButtonAppearance="@Appearance.Neutral" Text="Select an item" Items="@items"></FluentMenuButton>
    </span>
    <span>
        Lightweight: <FluentMenuButton @ref=menubutton ButtonAppearance="@Appearance.Lightweight" Text="Select an item" Items="@items"></FluentMenuButton>
    </span>
    <span>
        Outline: <FluentMenuButton @ref=menubutton ButtonAppearance="@Appearance.Outline" Text="Select an item" Items="@items"></FluentMenuButton>
    </span>
    <span>
        Stealth: <FluentMenuButton @ref=menubutton ButtonAppearance="@Appearance.Stealth" Text="Select an item" Items="@items"></FluentMenuButton>
    </span>
    <span>
        With Icon: <FluentMenuButton @ref=menubutton ButtonAppearance="@Appearance.Stealth" Text="Select an item" IconStart="new Icons.Regular.Size16.Globe()" Items="@items"></FluentMenuButton>
    </span>
</FluentStack>
@code {

    private FluentMenuButton menubutton = new();
    private Dictionary<string, string> items = new Dictionary<string, string>()
    {
        {"1","Item 1"},
        {"2","Item 2"},
        {"3","Item 3"},
        {"4","Item 4"},
    };
}
```

--------------------------------

### Show Confirmation Toasts with Fluent UI Blazor

Source: https://www.fluentui-blazor.net/Toast

This snippet demonstrates how to use the IToastService to display various types of confirmation toasts in a Blazor application. It includes examples for success, warning, error, info, progress, upload, download, event, mention, and custom toasts, utilizing Fluent UI components and icons.

```razor
@inject IToastService ToastService

@inject NavigationManager NavigationManager

<FluentStack Orientation="Orientation.Vertical" VerticalGap="10">
    <FluentStack Wrap=true>
        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowSuccess(\"Success confirmation.\"))">
            <FluentIcon Value="@(new Icons.Filled.Size20.CheckmarkCircle())" Color="@Color.Success" Slot="start" />
            Show Success
        </FluentButton>
        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowWarning(\"Warning confirmation.\"))">
            <FluentIcon Value="@(new Icons.Filled.Size20.Warning())" Color="@Color.Warning" Slot="start" />
            Show Warning
        </FluentButton>
        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowError(\"Error confirmation.\"))">
            <FluentIcon Value="@(new Icons.Filled.Size20.DismissCircle())" Color="@Color.Error" Slot="start" />
            Show Error
        </FluentButton>
        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowInfo(\"Info confirmation.\"))">
            <FluentIcon Value="@(new Icons.Filled.Size20.Info())" Color="@Color.Info" Slot="start" />
            Show Info
        </FluentButton>
        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowProgress(\"Progress confirmation.\"))">
            <FluentIcon Value="@(new Icons.Regular.Size20.Flash())" Color="@Color.Neutral" Slot="start" />
            Show progress
        </FluentButton>
        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowUpload(\"Upload confirmation.\"))">
            <FluentIcon Value="@(new Icons.Regular.Size20.ArrowUpload())" Color="@Color.Neutral" Slot="start" />
            Show upload
        </FluentButton>
        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowDownload(\"Download confirmation.\"))">
            <FluentIcon Value="@(new Icons.Regular.Size20.ArrowDownload())" Color="@Color.Neutral" Slot="start" />
            Show download
        </FluentButton>
        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowEvent(\"Event confirmation.\"))">
            <FluentIcon Value="@(new Icons.Regular.Size20.CalendarLtr())" Color="@Color.Neutral" Slot="start" />
            Show event
        </FluentButton>
        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowMention(\"Mention confirmation.\"))">
            <FluentIcon Value="@(new Icons.Regular.Size20.Person())" Color="@Color.Neutral" Slot="start" />
            Show mention
        </FluentButton>
        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowCustom(\"Custom confirmation.\", null, null, null, (new Icons.Regular.Size24.Delete(), Color.Accent)))">
            <FluentIcon Value="@(new Icons.Regular.Size20.Delete())" Color="@Color.Neutral" Slot="start" />
            Show custom
        </FluentButton>
    </FluentStack>
    <FluentStack Wrap=true>
        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowCustom(\"Confirmation without an icon\"))">
            Without icon
        </FluentButton>

        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowSuccess(\"Success confirmation with action.\", null, \"Action\", EventCallback.Factory.Create<ToastResult>(this, HandleTopAction)))">
            With action
        </FluentButton>

        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowInfo(\"Info confirmation custom settings.\", timeout: 10000))">
            Custom timeout
        </FluentButton>

        <FluentButton Appearance=Appearance.Neutral @onclick="@(() => ToastService.ShowSuccess(\"Success confirmation with a lot of text to see what a toast looks like when it's really big.\"))">
            <FluentIcon Value="@(new Icons.Filled.Size20.CheckmarkCircle())" Color="@Color.Success" Slot="start" />
            Long Success
        </FluentButton>
    </FluentStack>
</FluentStack>

@code {
    private void HandleTopAction(ToastResult result)
    {
        DemoLogger.WriteLine($"Toast clicked");
    }
}

```

--------------------------------

### RenderFragment for Dialog Content in Fluent UI Blazor

Source: https://www.fluentui-blazor.net/Dialog

Displays dialog content using a RenderFragment, allowing for dynamic UI elements within the dialog. This example uses a FluentTextField bound to a string variable and demonstrates how to open and interact with the dialog.

```Razor
@inject IDialogService _dialogService

<FluentButton OnClick="OpenDialog" Appearance="Appearance.Accent">Open dialog</FluentButton>

@code {
    private async Task OpenDialog()
    {
        var text = string.Empty;
        var dialogInstance = await _dialogService.ShowDialogAsync(@<div>
        <FluentTextField @bind-Value=text Label="Enter a value:" />
    </div>
    , new DialogParameters
    {
        Title = "Render Fragment Content",
    });

        var result = await dialogInstance.Result;
        if (!result.Cancelled)
        {
            await _dialogService.ShowSuccessAsync($"You entered: {text}", "Success");
        }
    }
}
```

--------------------------------

### FluentDialog Component API

Source: https://www.fluentui-blazor.net/Dialog

API documentation for the FluentDialog component, outlining its configurable parameters for appearance and behavior.

```APIDOC
## FluentDialog Component API

### Description

Provides a modal dialog experience with customizable content, dismiss buttons, and visibility.

### Method

Not Applicable (Component)

### Endpoint

Not Applicable (Component)

### Parameters

#### Path Parameters

None

#### Query Parameters

None

#### Request Body

None

### Request Example

None

### Response

None

#### Success Response (200)

None

#### Response Example

None

#### Parameters

- **ChildContent** (RenderFragment?) - Gets or sets the content to be rendered inside the component.
- **ShowDismiss** (bool?) - When true, shows the dismiss button in the header. If defined, this value will replace the one defined in the `DialogParameters`.
- **ShowDismissTooltip** (bool?) - When true, shows the 'Close' button tooltip in the header. Default is True.
- **TabIndex** (int?) - Allows developers to make elements sequentially focusable and determine their relative ordering for navigation (usually with the Tab key). Default is 0.
- **Visible** (bool) - When true, the header is visible. Default is True.
```

--------------------------------

### Create a FluentSwitch Toggle in Blazor

Source: https://www.fluentui-blazor.net/datagrid-custom-comparer-sort

This snippet shows how to implement a FluentSwitch component in Blazor, allowing users to toggle a setting. It uses @bind-Value to bind the switch's state to a boolean variable and displays a label.

```blazor
<FluentSwitch @bind-Value="altcolor" Label="Alternative hover color"></FluentSwitch>

@code {
    bool altcolor = false;
}
```

--------------------------------

### FluentToolbar Example with Various Controls in Razor

Source: https://www.fluentui-blazor.net/Toolbar

This snippet demonstrates the usage of the FluentToolbar component in Razor, showcasing various Fluent UI Blazor components such as FluentButton, FluentRadioGroup, FluentMenuButton, FluentSelect, and FluentCheckbox. It also includes dynamic styling and interaction handling.

```Razor
@inject FillColor FillColor

@inject BaseLayerLuminance BaseLayerLuminance

<FluentToolbar id="toolbar-fluent-components">
    <FluentButton Appearance="Appearance.Accent">Accent Button</FluentButton>
    <FluentButton Appearance="Appearance.Stealth">Stealth Button</FluentButton>
    <FluentRadioGroup @bind-Value=value1>
        <FluentRadio Value=@("one") Checked="true">One</FluentRadio>
        <FluentRadio Value=@("two")>Two</FluentRadio>
        <FluentRadio Value=@("three")>Three</FluentRadio>
    </FluentRadioGroup>
    <FluentMenuButton @ref=menubutton Text="Select brand color" Items="@items" OnMenuChanged="HandleOnMenuChanged"></FluentMenuButton>

    <FluentButton>Button</FluentButton>

    <FluentInputLabel ForId="s1" Orientation="Orientation.Horizontal">Select something</FluentInputLabel>
    <FluentSelect Id="s1" Class="below outline" @bind-Value="@comboboxValue" TOption="string">
        <FluentOption id="option-15">Option 1</FluentOption>
        <FluentOption id="option-16">Second option</FluentOption>
        <FluentOption id="option-17">Option 3</FluentOption>
    </FluentSelect>

    <FluentCheckbox @bind-Value=check1>Checkbox</FluentCheckbox>
</FluentToolbar>


<div style="padding:10px">
    <FluentToolbar id="toolbar-fluent-components-two" style="width:100%" @ref=Toolbar>
        <FluentRadioGroup @bind-Value=value2>
            <FluentRadio Value=@("one") Checked="true">Add</FluentRadio>
            <FluentRadio Value=@("two")>Open</FluentRadio>
            <FluentRadio Value=@("three")>Copy</FluentRadio>
            <FluentRadio Value=@("four")>Export</FluentRadio>
            <FluentRadio Value=@("five")>Automate</FluentRadio>
        </FluentRadioGroup>
        <FluentButton Appearance="Appearance.Stealth">Stealth</FluentButton>
        <FluentButton Appearance="Appearance.Accent">Refresh</FluentButton>
        <FluentBadge>21 items</FluentBadge>

        <FluentMenuButton @ref=menubutton
                          Text="Select brand color"
                          Items="@items"
                          OnMenuChanged="HandleOnMenuChanged"
                          style="margin: auto 16px;"
                          slot="end"></FluentMenuButton>
        <FluentRadioGroup @bind-Value=value3 slot="end">
            <FluentRadio>Filter</FluentRadio>
            <FluentRadio>
                <FluentTextField @bind-Value=text1 Placeholder="Search"></FluentTextField>
            </FluentRadio>
        </FluentRadioGroup>
    </FluentToolbar>
</div>

@code {
    string? comboboxValue;
    FluentToolbar? Toolbar;
    string? value1 = "one", value2 = "two", value3;
    bool check1;
    string? text1;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await FillColor.SetValueFor(Toolbar!.Element, "#333");
            await BaseLayerLuminance.SetValueFor(Toolbar!.Element, (float)0.15);

            StateHasChanged();
        }
    }

    private FluentMenuButton menubutton = new();

    private Dictionary<string, string> items = new Dictionary<string, string>()
    {
        {"0078D4","Windows"},
        {"D83B01","Office"},
        {"464EB8","Teams"},
        {"107C10","Xbox"},
        {"8661C5","Visual Studio"},
        {"F2C811","Power BI"},
        {"0066FF","Power Automate"},
        {"742774","Power Apps"},
        {"0B556A","Power Virtual Agents"}
    };

    private void HandleOnMenuChanged(MenuChangeEventArgs args)
    {


    }
}
```

--------------------------------

### Fluent Blazor RadioGroup Default Example

Source: https://www.fluentui-blazor.net/RadioGroup

Demonstrates the default usage of the FluentRadioGroup component in Blazor, allowing selection between numeric and string values. It includes binding the selected value and displaying its type. Dependencies include the Fluent UI Blazor library.

```Razor
<FluentRadioGroup Name="numbers" @bind-Value=value1 Label="Numbers">
    <FluentRadio Value="1">One</FluentRadio>
    <FluentRadio Value="2">Two</FluentRadio>
</FluentRadioGroup>
<p>Selected: @value1 (@value1?.GetType())</p>

<FluentRadioGroup Name="strings" @bind-Value=value2 Label="Strings">
    <FluentRadio Value="@("one")">One</FluentRadio>
    <FluentRadio Value="@("two")">Two</FluentRadio>
</FluentRadioGroup>
<p>Selected: @value2 (@value2?.GetType())</p>

@code 
{
    int? value1;
    string? value2;
}
```

--------------------------------

### Configure DataGrid Column Headers as Menu Buttons in Razor

Source: https://www.fluentui-blazor.net/datagrid-menu-header

This snippet shows how to enable menu-like behavior for column headers in a Fluent DataGrid by setting `HeaderCellAsButtonWithMenu` to `true`. It defines a `Person` record and populates the grid with sample data. This requires the Fluent UI Blazor component library.

```csharp
<FluentDataGrid Items="@people" ResizableColumns="true" ResizeType="DataGridResizeType.Exact" HeaderCellAsButtonWithMenu="true">
    <PropertyColumn Property="@(p => p.PersonId)" Sortable="true" />
    <PropertyColumn Property="@(p => p.Name)" Sortable="true" />
    <PropertyColumn Property="@(p => p.BirthDate)" Format="yyyy-MM-dd" Sortable="true" />
</FluentDataGrid>

@code {
    record Person(int PersonId, string Name, DateOnly BirthDate);

    IQueryable<Person> people = new[]
    {
        new Person(10895, "Jean Martin", new DateOnly(1985, 3, 16)),
        new Person(10944, "António Langa", new DateOnly(1991, 12, 1)),
        new Person(11203, "Julie Smith", new DateOnly(1958, 10, 10)),
        new Person(11205, "Nur Sari", new DateOnly(1922, 4, 27)),
        new Person(11898, "Jose Hernandez", new DateOnly(2011, 5, 3)),
        new Person(12130, "Kenji Sato", new DateOnly(2004, 1, 9)),
    }.AsQueryable();
}
```

--------------------------------

### FluentDialog Class

Source: https://www.fluentui-blazor.net/Dialog

Documentation for the FluentDialog component, including its parameters, event callbacks, and methods.

```APIDOC
## FluentDialog Class

### Description
Represents a dialog window in the Fluent UI Blazor component library.

### Parameters

#### Path Parameters
N/A

#### Query Parameters
N/A

#### Request Body
N/A

#### Component Parameters

- **AriaDescribedby** (string?) - Gets or sets the id of the element describing the dialog.
- **AriaLabel** (string?) - Gets or sets the label surfaced to assistive technologies.
- **AriaLabelledby** (string?) - Gets or sets the id of the element labeling the dialog.
- **ChildContent** (RenderFragment?) - Used when not calling the `DialogService` to show a dialog.
- **Hidden** (bool) - Gets or sets a value indicating whether the dialog is hidden. Defaults to `False`.
- **Instance** (DialogInstance) - Gets or sets the instance containing the programmatic API for the dialog.
- **Modal** (bool?) - Gets or sets a value indicating whether the element is modal. When modal, user mouse interaction will be limited to the contents of the element by a modal overlay. Clicks on the overlay will cause the dialog to emit a 'dismiss' event.
- **PreventScroll** (bool) - Prevents scrolling outside of the dialog while it is shown. Defaults to `True`.
- **TrapFocus** (bool?) - Gets or sets a value indicating whether that the dialog should trap focus.

### EventCallbacks

- **HiddenChanged** (EventCallback<bool>) - The event callback invoked when `FluentDialog.Hidden` change.
- **OnDialogResult** (EventCallback<DialogResult>) - The event callback invoked to return the dialog result.

### Methods

#### CancelAsync

##### Description
Closes the dialog with a cancel result.

##### Method
`Task`

#### CancelAsync<T>

##### Description
Closes the dialog with a cancel result, specifying a return value of type T.

##### Method
`Task`

#### CloseAsync

##### Description
Closes the dialog.

##### Method
`Task`

#### CloseAsync

##### Description
Closes the dialog with a specified `DialogResult`.

##### Method
`Task`

#### CloseAsync<T>

##### Description
Closes the dialog with an OK result, specifying a return value of type T.

##### Method
`Task`

#### Hide

##### Description
Hides the dialog.

##### Method
`void`

#### Show

##### Description
Shows the dialog.

##### Method
`void`

#### TogglePrimaryActionButton

##### Description
Toggles the enabled state of the primary action button.

##### Parameters
- **isEnabled** (bool) - Whether the primary action button should be enabled.

##### Method
`void`

#### ToggleSecondaryActionButton

##### Description
Toggles the enabled state of the secondary action button.

##### Parameters
- **isEnabled** (bool) - Whether the secondary action button should be enabled.

##### Method
`void`

```

--------------------------------

### Razor Example: Interactive FluentOverlay Control

Source: https://www.fluentui-blazor.net/Overlay

This Razor component demonstrates controlling a FluentOverlay's interactivity. It uses FluentSwitch components to toggle `Interactive` and `InteractiveExceptId` properties, affecting when the overlay closes. The overlay's visibility is managed by a `FluentButton`, and an incrementing counter is displayed within the overlay.

```html
<FluentStack>
    <FluentSwitch @bind-Value="@interactive" Label="Interactive" />
    <FluentSwitch @bind-Value="@interactiveExceptId" Label="Exception Zone (my-zone)" Disabled="@(!interactive)" />
    <FluentSwitch @bind-Value="@fullScreen" Label="Full screen" />
</FluentStack>

<FluentButton Appearance="Appearance.Accent"
              Style="margin: 24px 0px;"
              @onclick="() => visible = !visible">
    Show Overlay
</FluentButton>

<FluentStack VerticalAlignment="VerticalAlignment.Center">
    <FluentButton OnClick="@(e => counter++)">Increment</FluentButton>
    <FluentLabel>Counter: @counter</FluentLabel>
</FluentStack>

<FluentOverlay @bind-Visible=@visible
               Opacity="0.4"
               BackgroundColor="#e8f48c"
               FullScreen="@fullScreen"
               Interactive="@interactive"
               InteractiveExceptId="@(interactiveExceptId ? "my-zone" : null)"
               OnClose="HandleOnClose"
               PreventScroll=true>
    @if (interactive)
    {
        <div id="my-zone">
            <p>Non-interactive zone</p>
            <FluentProgressRing />
        </div>
    }
    else
    {
        <FluentProgressRing />
    }
</FluentOverlay>

<style>
    #my-zone {
        background-color: var(--neutral-base-color);
        width: 200px;
        height: 160px;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }
    #my-zone p {
            color: var(--neutral-foregrond-rest);
    }
</style>

@code {
    bool visible = false;
    bool interactive = true;
    bool interactiveExceptId = true;
    bool fullScreen = true;
    int counter = 0;

    protected void HandleOnClose()
    {
        DemoLogger.WriteLine("Overlay closed");
    }
}
```

--------------------------------

### Fluent Blazor Menu: Menu with Separator

Source: https://www.fluentui-blazor.net/Menu

This example demonstrates how to include a separator within a Fluent Blazor Menu using the `FluentDivider` component. The menu is controlled by a `FluentSwitch`. This allows for visual grouping of menu items.

```Razor
<FluentSwitch @bind-Value="@open">Show</FluentSwitch>
<FluentMenu @bind-Open="@open">
    <FluentMenuItem>Menu item 1</FluentMenuItem>
    <FluentMenuItem>Menu item 2</FluentMenuItem>
    <FluentDivider />
    <FluentMenuItem>Menu item 3</FluentMenuItem>
</FluentMenu>

@code {
    bool open = false;
}
```

--------------------------------

### Dynamic Scaling of Regions via Offset (Razor)

Source: https://www.fluentui-blazor.net/AnchoredRegion

This example demonstrates dynamic scaling of a FluentAnchoredRegion using an offset. Similar to scaling via updates, it employs `AxisPositioningMode.Dynamic` and `AxisScalingMode.Fill`, but the scaling is influenced by an offset rather than direct content updates.

```Razor
<div id="viewport-scaling-offset" style="height:400px;width:400px;background: var(--neutral-layer-4);overflow:auto;position:relative;resize:both;">
    <FluentButton Appearance=Appearance.Neutral id="anchor-scaling-offset" style="margin-left:100px;margin-top:100px">anchor</FluentButton>
    <FluentAnchoredRegion id="region-scaling-offset" Anchor="anchor-scaling-offset" Viewport="viewport-scaling-offset"
                          VerticalPositioningMode="AxisPositioningMode.Dynamic"
                          VerticalScaling="AxisScalingMode.Fill"
                          HorizontalPositioningMode="AxisPositioningMode.Dynamic"
                          HorizontalScaling="AxisScalingMode.Fill">
        <div style="height:40px;width:60px;background:var(--highlight-bg);" />
    </FluentAnchoredRegion>
</div>
```

--------------------------------

### Interactive FluentSearch with Live Filtering in Blazor

Source: https://www.fluentui-blazor.net/Search

An interactive FluentSearch example in Blazor that demonstrates live filtering of a list of US states as the user types. It uses `@bind-Value:after` to trigger search logic on input, and `FluentListbox` to display results. Requires `FluentSearch`, `FluentListbox`, and associated C# logic for data handling and filtering.

```Razor
<FluentSearch @ref=searchTest 
              @bind-Value="@searchValue"
              @bind-Value:after=HandleSearchInput
              Placeholder="Search for State" />
<br />
<FluentListbox aria-label="search results" Items=@searchResults TOption="string" SelectedOptionChanged="@(e => searchValue = (e != defaultResultsText ? e : string.Empty) )"  />

<p>
    You searched for: @searchValue
</p>

@code {
    FluentSearch? searchTest;
    string? searchValue = string.Empty;

    List<string> searchData = new() {
        "Alabama",
        "Alaska",
        "Arizona",
        "Arkansas",
        "California",
        "Colorado",
        "Connecticut",
        "Delaware",
        "Florida",
        "Georgia",
        "Hawaii",
        "Idaho",
        "Illinois",
        "Indiana",
        "Iowa",
        "Kansas",
        "Kentucky",
        "Louisiana",
        "Maine",
        "Maryland",
        "Massachussets",
        "Michigain",
        "Minnesota",
        "Mississippi",
        "Missouri",
        "Montana",
        "Nebraska",
        "Nevada",
        "New Hampshire",
        "New Jersey",
        "New Mexico",
        "New York",
        "North Carolina",
        "North Dakota",
        "Ohio",
        "Oklahoma",
        "Oregon",
        "Pennsylvania",
        "Rhode Island",
        "South Carolina",
        "South Dakota",
        "Texas",
        "Tennessee",
        "Utah",
        "Vermont",
        "Virginia",
        "Washington",
        "Wisconsin",
        "West Virginia",
        "Wyoming"
    };

    List<string> searchResults = defaultResults();

    static string defaultResultsText = "no results";
    static List<string> defaultResults() {
        return new() { defaultResultsText };
    }

    void HandleSearchInput() {
        if (string.IsNullOrWhiteSpace(searchValue)) {
            searchResults = defaultResults();
            searchValue = string.Empty;
        } else {
            string searchTerm = searchValue.ToLower();

            if (searchTerm.Length > 0) {
                List<string> temp = searchData.Where(str => str.ToLower().Contains(searchTerm)).Select(str => str).ToList();
                if (temp.Count() > 0) {
                    searchResults = temp;
                }
            }
        }
    }
}
```

--------------------------------

### FluentNavGroup Component

Source: https://www.fluentui-blazor.net/NavMenu

Documentation for the FluentNavGroup component, which is part of the FluentNavMenu structure.

```APIDOC
## FluentNavGroup Component

### Description
The `FluentNavGroup` component represents a group of navigation links within a `FluentNavMenu`, supporting expansion and icons.

### Method
N/A

### Endpoint
N/A

### Parameters
#### Path Parameters
None

#### Query Parameters
None

#### Request Body
None

### Parameters

- **Id** (string) - Description: Unique identifier for the navigation group.
- **Title** (string) - Description: The title displayed for this navigation group.
- **Icon** (Icon?) - Description: The icon to display for the navigation group.
- **ChildContent** (RenderFragment?) - Description: Slot for the navigation links within this group.
- **Expanded** (bool) - Description: Controls the expanded state of the navigation group.

### Request Example
```razor
<FluentNavGroup Id="Group1" Title="Item 1" @bind-Expanded=Item1Expanded Icon="@(new Icons.Regular.Size24.LeafOne())">
    <FluentNavLink Icon="@(new Icons.Regular.Size24.LeafOne())">Item 1.1</FluentNavLink>
    <FluentNavLink Icon="@(new Icons.Regular.Size24.LeafTwo())">Item 1.2</FluentNavLink>
</FluentNavGroup>
```

### Response
#### Success Response (200)
None

#### Response Example
None
```

--------------------------------

### FluentNumberField Examples in Razor

Source: https://www.fluentui-blazor.net/NumberField

Demonstrates the usage of the FluentNumberField component in Razor syntax. It showcases binding integer and nullable integer values, setting labels, and handling the 'oninput' event for real-time updates. The component supports different appearances like 'Filled'.

```Razor
<p>
    <FluentNumberField @bind-Value="exampleInt" Label="Integer" />
    <br />
    Example int: @exampleInt
    <br />
    Minimum value: @(int.MinValue); Maximum value: @(int.MaxValue)
</p>
<p>
    <FluentNumberField @bind-Value="exampleNullableInt" Label="Nullable integer" />
    <br />
    Example nullable int: @exampleNullableInt
    <br />
    Minimum value: @(int.MinValue); Maximum value: @(int.MaxValue)
</p>
<p>
    Same as above but bound to oninput event 
    <br />
    <FluentNumberField @bind-Value="exampleInt2"
                       Appearance="FluentInputAppearance.Filled"
                       @oninput="@(e => Int32.TryParse(e.Value?.ToString(), out exampleInt2))"
                       Label="Integer" />
    <br />
    Example int: @exampleInt2
</p>
<p>
    Nullable int bound to oninput event 
    <br />
    <FluentNumberField @bind-Value="exampleNullableInt2"
                       Appearance="FluentInputAppearance.Filled"
                       @oninput="@(e => exampleNullableInt2 = int.TryParse(e.Value?.ToString(), out int tmp) ? (int?)tmp : null)"
                       Label="Nullable integer" />
    <br />
    Example nullable int: @exampleNullableInt2
</p>

@code {
    int exampleInt { get; set; } = 123;
    private int? exampleNullableInt = null;
    private int exampleInt2 = 345;
    private int? exampleNullableInt2 = null;
}
```

--------------------------------

### Indicating User Keyboard Input with HTML <kbd> Tag

Source: https://www.fluentui-blazor.net/Reboot

Illustrates the use of the `<kbd>` tag to represent input typically entered via a keyboard. This is useful for showing commands, shortcuts, or user input within documentation or tutorials.

```html
To switch directories, type `cd` followed by the name of the directory.
To edit settings, press ``ctrl` + `,``
```

--------------------------------

### Generate Icons Based on Active State (C#)

Source: https://www.fluentui-blazor.net/AppBar

This C# code defines static methods to generate Fluent UI icons. It takes a boolean 'active' parameter to determine whether to return a 'Filled' or 'Regular' version of the icon. These methods are useful for dynamically changing icon appearances based on component state.

```csharp
private static Icon AppFolderIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.AppFolder()
               : new Icons.Regular.Size24.AppFolder();

    private static Icon ConsoleLogsIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.SlideText()
               : new Icons.Regular.Size24.SlideText();

    private static Icon StructuredLogsIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.SlideTextSparkle()
               : new Icons.Regular.Size24.SlideTextSparkle();

    private static Icon TracesIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.GanttChart()
               : new Icons.Regular.Size24.GanttChart();

    private static Icon MetricsIcon(bool active = false) =>
        active ? new Icons.Filled.Size24.ChartMultiple()
               : new Icons.Regular.Size24.ChartMultiple();
```

--------------------------------

### RenderFragment as Dialog

Source: https://www.fluentui-blazor.net/Dialog

Shows how to display a dialog using only a RenderFragment and the IDialogService.

```APIDOC
## RenderFragment as Dialog

### Description
This example demonstrates how to display a dialog using only a `RenderFragment`. It utilizes the `IDialogService` to show a dialog with a `FluentTextField` for user input.

### Method
N/A (Component Rendering with Service Interaction)

### Endpoint
N/A (Component Rendering with Service Interaction)

### Parameters
N/A

### Request Example
```html
@inject IDialogService _dialogService

<FluentButton OnClick="OpenDialog" Appearance="Appearance.Accent">Open dialog</FluentButton>

@code {
    private async Task OpenDialog()
    {
        var text = string.Empty;
        var dialogInstance = await _dialogService.ShowDialogAsync(@<div>
        <FluentTextField @bind-Value=text Label="Enter a value:" />
    </div>
    , new DialogParameters
    {
        Title = "Render Fragment Content",
    });

        var result = await dialogInstance.Result;
        if (!result.Cancelled)
        {
            await _dialogService.ShowSuccessAsync($"You entered: {text}", "Success");
        }
    }
}
```

### Response
N/A (Component Rendering with Service Interaction)

```

--------------------------------

### Use Custom Image as Fluent UI Icon in Blazor

Source: https://www.fluentui-blazor.net/ProjectSetup

Renders a Fluent UI Icon using a custom image URL. The `Icon.FromImageUrl` method is used to specify the image source, and the width can be set.

```razor
<FluentIcon Value="@(Icon.FromImageUrl("/Blazor.png"))" Width="32px" />

```

--------------------------------

### Handle Content Updates and Raise Event (Blazor)

Source: https://www.fluentui-blazor.net/Lab/TableOfContents

This Blazor component example shows how to receive a cascaded EventCallback and invoke it when content is converted, such as after a MarkdownSection component renders. This triggers the TableOfContents refresh mechanism.

```razor
@page "/CodeSetup"

<MarkdownSection FromAsset="./_content/FluentUI.Demo.Shared/docs/CodeSetup.md" OnContentConverted="RefreshTableOfContents" />

@code {

    [CascadingParameter]
    public EventCallback OnRefreshTableOfContents { get; set; }

    private async Task RefreshTableOfContents()
    {
        await OnRefreshTableOfContents.InvokeAsync();
    }
}

```

--------------------------------

### FluentProgressRing: Paused State Example

Source: https://www.fluentui-blazor.net/ProgressRing

Demonstrates the 'Paused' state for the FluentProgressRing component. The first ring is a determinate progress ring with a value of 75 that is visually paused. The second ring is an indeterminate ring that is also paused.

```Razor
<h4>Paused</h4>
<FluentStack>
    <FluentProgressRing Paused="true" Min="0" Max="100" Value="75"></FluentProgressRing>
    <FluentProgressRing Paused="true"></FluentProgressRing>
</FluentStack>
```

--------------------------------

### Styling HTML <summary> Element for Interactivity

Source: https://www.fluentui-blazor.net/Reboot

Details the styling of the `<summary>` element, resetting its default `cursor` from `text` to `pointer` to indicate that it is clickable and interactive.

```html
Some details
More info about the details.
Even more details
Here are even more details about the details.
```

--------------------------------

### FluentSelect Default Example in Razor

Source: https://www.fluentui-blazor.net/Select

Demonstrates the default usage of the FluentSelect component in Razor. It binds to a strongly typed Person object and a string value, allowing selection from a list of people. Includes placeholders for selected values and persons.

```Razor
@inject DataSource Data
<FluentSelect TOption="Person"
              Label="Select a person"
              Items="@Data.People.WithVeryLongName()"
              Id="people-listbox"
              Width="200px"
              Height="250px"
              Placeholder="Make a selection..."
              OptionValue="@(p => p.PersonId.ToString())"
              OptionText="@(p => p.LastName + ", " + p.FirstName)"
              OptionTitle="@(p => p.LastName.Length + p.FirstName.Length > 20 ? p.LastName : null)"
              @bind-Value="@SelectedValue"
              @bind-SelectedOption="@SelectedPerson" />

<p>
    Selected value: @SelectedValue <br />
    Selected person (strongly typed): @SelectedPerson?.ToString()
</p>

@code
{
    Person? SelectedPerson;
    string? SelectedValue;
}
```

--------------------------------

### Define CSS for Centered Flag Images in Blazor

Source: https://www.fluentui-blazor.net/datagrid-custom-comparer-sort

This CSS code snippet defines a '.flag' class to ensure all flag images have a consistent height and are centered. It also applies a subtle border to the flags.

```css
/* Ensure all the flags are the same size, and centered */
.flag {
    height: 1rem;
    margin: auto;
    border: 1px solid var(--neutral-layer-3);
}
```

--------------------------------

### Workaround for Autofac IoC Issue in Blazor Server

Source: https://www.fluentui-blazor.net/InputFile

Provides a code snippet for a workaround to an issue with the InputFile component in Blazor Server applications using Autofac IoC containers starting with .NET 6. This involves configuring HubOptions.

```csharp
builder.Services
    .AddServerSideBlazor()
    .AddHubOptions(opt =>
    {
        opt.DisableImplicitFromServicesParameters = true;
    });
```

--------------------------------

### Implement FocusAsync on FluentNumberField (Razor)

Source: https://www.fluentui-blazor.net/NumberField

Shows how to programmatically focus a FluentNumberField using the `FocusAsync` method. This is achieved by referencing the component using `@ref` and calling `FocusAsync` on a button click event. The example includes commented-out autofocus for demonstration purposes.

```Razor
<h4>Autofocus</h4>
<p>Commented out to prevent page actually jumping to this location. See example code on 'Razor'-tab for implementation.</p>
@*<FluentNumberField TValue="int?" Autofocus="true">autofocus</FluentNumberField>*@

<h4>Focus Async</h4>
<p style="display:flex">
    <FluentButton @onclick="() => focusTest!.FocusAsync()">FocusAsync</FluentButton>
    <FluentNumberField @ref=focusTest @bind-Value=value></FluentNumberField>
</p>

@code {
    FluentNumberField<int?>? focusTest;
    int? value;
}
```

--------------------------------

### FluentBreadcrumb with End Icons (Razor)

Source: https://www.fluentui-blazor.net/Breadcrumb

This example shows how to add icons to the end of each FluentBreadcrumbItem. It utilizes the 'end' slot for positioning the icons. Dependencies include FluentBreadcrumb, FluentBreadcrumbItem, and FluentIcon components.

```Razor
<FluentBreadcrumb>
    <FluentBreadcrumbItem Href="#">
        Breadcrumb item 1
        <FluentIcon Value="@(new Icons.Regular.Size16.Home())" Color="@Color.Neutral" Slot="end" />
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem Href="#">
        Breadcrumb item 2
        <FluentIcon Value="@(new Icons.Regular.Size16.Clipboard())" Color="@Color.Neutral" Slot="end" />
    </FluentBreadcrumbItem>
    <FluentBreadcrumbItem>
        Breadcrumb item 3
        <FluentIcon Value="@(new Icons.Regular.Size16.Money())" Color="@Color.Neutral" Slot="end" />
    </FluentBreadcrumbItem>
</FluentBreadcrumb>
```

--------------------------------

### Display Async Message Boxes using DialogService in Blazor

Source: https://www.fluentui-blazor.net/MessageBox

This snippet shows how to invoke various asynchronous message box methods from the DialogService in Blazor. It includes examples for success, warning, error, information, and confirmation dialogs, demonstrating how to display predefined messages and handle the asynchronous results.

```razor
@inject IDialogService DialogService


<FluentStack>
    <FluentButton OnClick="@ShowSuccessAsync" Appearance="Appearance.Accent">Success</FluentButton>
    <FluentButton OnClick="@ShowWarningAsync" Appearance="Appearance.Accent">Warning</FluentButton>
    <FluentButton OnClick="@ShowErrorAsync" Appearance="Appearance.Accent">Error</FluentButton>
    <FluentButton OnClick="@ShowInformationAsync" Appearance="Appearance.Accent">Information</FluentButton>
    <FluentButton OnClick="@ShowConfirmationAsync" Appearance="Appearance.Accent">Confirmation</FluentButton>
    <FluentButton OnClick="@ShowMessageBoxLongAsync" Appearance="Appearance.Accent">Long message</FluentButton>
    <FluentButton OnClick="@ShowMessageBoxAsync" Appearance="Appearance.Accent">Custom message</FluentButton>
</FluentStack>

<p>
    Last result: @(canceled == null ? "" : (canceled == true ? "❌ Canceled" : "✅ OK"))
</p>

@code
{
    bool? canceled;

    private async Task ShowSuccessAsync()
    {
        var dialog = await DialogService.ShowSuccessAsync("The action was a success");
        var result = await dialog.Result;
        canceled = result.Cancelled;
    }

    private async Task ShowWarningAsync()
    {
        var dialog = await DialogService.ShowWarningAsync("This is your final warning");
        var result = await dialog.Result;
        canceled = result.Cancelled;
    }

    private async Task ShowErrorAsync()
    {
        var dialog = await DialogService.ShowErrorAsync("This is an error");
        var result = await dialog.Result;
        canceled = result.Cancelled;
    }

    private async Task ShowInformationAsync()
    {
        var dialog = await DialogService.ShowInfoAsync("This is a message");
        var result = await dialog.Result;
        canceled = result.Cancelled;
    }

    private async Task ShowConfirmationAsync()
    {
        var dialog = await DialogService.ShowConfirmationAsync("Are you <strong>sure</strong> you want to delete this item? <br /><br />This will also remove any linked items");
        var result = await dialog.Result;
        canceled = result.Cancelled;
    }

    private async Task ShowMessageBoxLongAsync()
    {
        var dialog = await DialogService.ShowInfoAsync("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
        var result = await dialog.Result;
        canceled = result.Cancelled;
    }

    private async Task ShowMessageBoxAsync()
    {
        var dialog = await DialogService.ShowMessageBoxAsync(new DialogParameters<MessageBoxContent>()
           {
                Content = new()
                {
                    Title = "My title",
                    MarkupMessage = new MarkupString("My <strong>customized</strong> message"),
                    Icon = new Icons.Regular.Size24.Games(),
                    IconColor = Color.Success,
                },
                PrimaryAction = "Plus",
                SecondaryAction = "Minus",
                Width = "300px",
            });
        var result = await dialog.Result;
        canceled = result.Cancelled;
    }
}

```

--------------------------------

### FluentAnchoredRegion: RTL Fill (Razor)

Source: https://www.fluentui-blazor.net/AnchoredRegion

Illustrates the behavior of FluentAnchoredRegion in a Right-to-Left (RTL) layout, using `AxisScalingMode.Fill` to make the region fill available space. The example includes a `dir="rtl"` attribute on the viewport.

```Razor
<div id="viewport-rtl-fill" dir="rtl" style="position:relative;height:400px;width:400px;background: var(--neutral-layer-4);overflow:auto;resize:both;">
    <FluentButton Appearance=Appearance.Neutral id="anchor-rtl-fill" style="margin-right:100px;margin-top:100px">anchor</FluentButton>
    <FluentAnchoredRegion Anchor="anchor-rtl-fill" Viewport="viewport-rtl-fill"
                          VerticalPositioningMode="AxisPositioningMode.Dynamic"
                          VerticalScaling="AxisScalingMode.Fill"
                          HorizontalPositioningMode="AxisPositioningMode.Dynamic"
                          HorizontalScaling="AxisScalingMode.Fill"
                          HorizontalDefaultPosition="HorizontalPosition.Unset"
                          VerticalDefaultPosition="VerticalPosition.Unset"
                          AutoUpdateMode="AutoUpdateMode.Anchor">
        <div style="height:100%;width:100%;background:var(--highlight-bg);" />
    </FluentAnchoredRegion>
</div>
```

--------------------------------

### Blazor Virtualized Data Grid Implementation

Source: https://www.fluentui-blazor.net/datagrid-virtualize

This Blazor code demonstrates how to implement a virtualized data grid using FluentDataGrid. It enables virtualization, defines columns, and includes logic for simulating data loading, clearing, and restoring items. Ensure that each rendered row has a consistent, known height for proper virtualization.

```Razor
<div style="height: 400px; max-width: 800px; overflow-y: scroll;">
    <FluentDataGrid @ref="grid" Items=@items Virtualize="true" DisplayMode="DataGridDisplayMode.Table" Style="width: 100%;" ItemSize="54" GenerateHeader="@GenerateHeaderOption.Sticky">
        <ChildContent>
            <PropertyColumn Width="25%" Property="@(c => c.Item1)" Sortable="true" />
            <PropertyColumn Width="25%" Property="@(c => c.Item2)" />
            <PropertyColumn Width="25%" Property="@(c => c.Item3)" Align="Align.Center" />
            <PropertyColumn Width="25%" Property="@(c => c.Item4)" Align="Align.End" />
        </ChildContent>
        <EmptyContent>
            <FluentIcon Value="@(new Icons.Filled.Size24.Crown())" Color="@Color.Accent" />&nbsp; Nothing to see here. Carry on!
        </EmptyContent>
        <LoadingContent>
            <FluentStack Orientation="Orientation.Vertical" HorizontalAlignment="HorizontalAlignment.Center">
                Loading...<br />
                <FluentProgress Width="240px" />
            </FluentStack>
        </LoadingContent>
    </FluentDataGrid>
</div>
<br />
<FluentSwitch @ref="_clearToggle"
              @bind-Value="@_clearItems"
              @bind-Value:after="ToggleItems"
              UncheckedMessage="Clear all results"
              CheckedMessage="Restore all results">
</FluentSwitch>
<FluentButton OnClick="SimulateDataLoading">Simulate data loading</FluentButton>

@code {
    FluentDataGrid<SampleGridData>? grid;
    FluentSwitch? _clearToggle;

    bool _clearItems = false;
    public record SampleGridData(string Item1, string Item2, string Item3, string Item4);

    IQueryable<SampleGridData>? items = Enumerable.Empty<SampleGridData>().AsQueryable();

    private IQueryable<SampleGridData> GenerateSampleGridData(int size)
    {
        SampleGridData[] data = new SampleGridData[size];

        for (int i = 0; i < size; i++)
        {
            data[i] = new SampleGridData($"value {i}-1", $"value {i}-2", $"value {i}-3", $"value {i}-4");
        }
        return data.AsQueryable();
    }
    protected override void OnInitialized()
    {
        items = GenerateSampleGridData(5000);
    }

    private void ToggleItems()
    {
        if (_clearItems)
        {
            items = null;
        }
        else
        {
            items = GenerateSampleGridData(5000);
        }
    }

    private async Task SimulateDataLoading()
    {
        _clearItems = false;

        items = null;
        grid?.SetLoadingState(true);

        await Task.Delay(1500);

        items = GenerateSampleGridData(5000);
        grid?.SetLoadingState(false);
    }
}
```

--------------------------------

### Manipulating Design Tokens Programmatically in Blazor

Source: https://www.fluentui-blazor.net/DesignTokens

Demonstrates how to programmatically set, delete, or get values for Fluent UI Blazor design tokens using JavaScript interop. This approach requires an ElementReference and should be performed after the component has rendered (e.g., in OnAfterRenderAsync).

```csharp
using Microsoft.AspNetCore.Components;

public partial class MyComponent : ComponentBase
{
    [Parameter] public ElementReference MyElement { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Example: Set a value for a design token
            await NeutralFill.SetValueFor(MyElement, "#0000FF");

            // Example: Get a value for a design token
            var color = await NeutralForeground.GetValueFor(MyElement);

            // Example: Delete a value for a design token
            await NeutralStroke.DeleteValueFor(MyElement);

            // Example: Set a default value for the design system
            BaseLayerLuminance.WithDefault(0.2f);
        }
    }
}
```

--------------------------------

### Use Custom Icon with FluentIcon Component

Source: https://www.fluentui-blazor.net/IconsAndEmoji

Illustrates how to render a custom icon defined in your code using the `FluentIcon` component. The `Value` parameter accepts an instance of your custom icon class, for example, `new MyIcons.SettingsEmail()`.

```razor
<FluentIcon Value="@(new MyIcons.SettingsEmail())" />
```

--------------------------------

### SimpleDialog Component Usage

Source: https://www.fluentui-blazor.net/Dialog

Demonstrates how to use a SimpleDialog component by inheriting from IDialogContentComponent<T> and rendering it directly on a page.

```APIDOC
## SimpleDialog Component Usage

### Description
This example shows the `SimpleDialog` component being rendered directly in the page. For a component to be usable in a dialog, it just needs to inherit from `IDialogContentComponent<T>`.

### Method
N/A (Component Rendering)

### Endpoint
N/A (Component Rendering)

### Parameters
N/A

### Request Example
```html
<SimpleDialog Content="@(new SimplePerson(){Firstname = "Jan", Lastname = "Burke", Age = 18})" />
```

### Response
N/A (Component Rendering)

```

--------------------------------

### FluentOption States (Disabled, Selected) in Blazor

Source: https://www.fluentui-blazor.net/Option

Illustrates how to configure the states of a FluentOption component in Blazor. It shows examples for disabling an option using the 'Disabled' attribute and pre-selecting an option using the 'Selected' attribute.

```Razor
<h2>Disabled</h2>
<FluentSelect TOption=string>
    <FluentOption Disabled="true">This option is disabled.</FluentOption>
</FluentSelect>
<h2>Selected</h2>
<FluentSelect TOption=string>
    <FluentOption Selected="true">This option is selected by default.</FluentOption>
</FluentSelect>
```

--------------------------------

### Fetching Remote Data with Blazor DataGrid ItemsProvider

Source: https://www.fluentui-blazor.net/datagrid-remote-data

This example demonstrates how to configure the FluentDataGrid component to fetch data remotely using the ItemsProvider delegate. It maps the grid's request parameters (startIndex, count) to the external API's query parameters (skip, limit) and returns the data along with the total item count for proper functioning of pagination and virtualization.

```csharp
@using Microsoft.FluentUI.AspNetCore.Components

@inject HttpClient Http
@inject NavigationManager NavManager

<div style="height: 434px; overflow:auto;" tabindex="-1">
    <FluentDataGrid
                    ItemsProvider="foodRecallProvider"
                    OnRowDoubleClick="@(()=>DemoLogger.WriteLine("Row double clicked!"))"
                    Virtualize="true"
                    DisplayMode="DataGridDisplayMode.Table"
                    ItemSize="46"
                    GenerateHeader="GenerateHeaderOption.Sticky"
                    TGridItem="FoodRecall" >
        <PropertyColumn Title="ID" Property="@(c => c!.Event_Id)" />
        <PropertyColumn Property="@(c => c!.State)" Style="color: #af5f00 ;" />
        <PropertyColumn Property="@(c => c!.City)" />
        <PropertyColumn Title="Company" Property="@(c => c!.Recalling_Firm)" Tooltip="true"/>
        <PropertyColumn Property="@(c => c!.Status)" />
        <TemplateColumn Title="Actions" Align="@Align.End">
            <FluentButton aria-label="Edit item" IconEnd="@(new Icons.Regular.Size16.Edit())" OnClick="@(() => DemoLogger.WriteLine("Edit clicked"))" />
            <FluentButton aria-label="Delete item" IconEnd="@(new Icons.Regular.Size16.Delete())" OnClick="@(() => DemoLogger.WriteLine("Delete clicked"))" />
        </TemplateColumn>
    </FluentDataGrid>
</div>

<p>Total: <strong>@numResults results found</strong></p>

@code {
    GridItemsProvider<FoodRecall> foodRecallProvider = default!;
    int? numResults;

    protected override async Task OnInitializedAsync()
    {
        // Define the GridRowsDataProvider. Its job is to convert QuickGrid's GridRowsDataProviderRequest into a query against
        // an arbitrary data soure. In this example, we need to translate query parameters into the particular URL format
        // supported by the external JSON API. It's only possible to perform whatever sorting/filtering/etc is supported
        // by the external API.
        foodRecallProvider = async req =>
        {
            var url = NavManager.GetUriWithQueryParameters("https://api.fda.gov/food/enforcement.json", new Dictionary<string, object?>
                        {
                { "skip", req.StartIndex },
                { "limit", req.Count },

```

--------------------------------

### Disable Fluent UI Blazor Class Name Validation

Source: https://www.fluentui-blazor.net/CodeSetup

Configuration in Program.cs to disable class name validation for Fluent UI Blazor components, useful when using frameworks like Tailwind CSS.

```csharp
builder.Services.AddFluentUIComponents(options =>
{
    options.ValidateClassNames = false;
});
```

--------------------------------

### FluentDivider: Vertical Divider Examples (Razor)

Source: https://www.fluentui-blazor.net/Divider

Illustrates how to create vertical dividers using the FluentDivider component by setting the 'Orientation' property to 'Orientation.Vertical'. It also shows the application of 'Presentation' and 'Separator' ARIA roles for these vertical dividers.

```Razor
<h4>Role="Presentation"</h4>

<FluentStack Orientation="Orientation.Horizontal" VerticalAlignment="VerticalAlignment.Center">
    <span>before divider</span>
    <FluentDivider Style="height: 50px;" Role="DividerRole.Presentation" Orientation="Orientation.Vertical"></FluentDivider>
    <span>after divider</span>
</FluentStack>

<h4>Role="Separator"</h4>

<FluentStack Orientation="Orientation.Horizontal" VerticalAlignment="VerticalAlignment.Center">
    <span>before divider</span>
    <FluentDivider Style="height: 50px;" Role="DividerRole.Separator" Orientation="Orientation.Vertical"></FluentDivider>
    <span>after divider</span>
</FluentStack>
```

--------------------------------

### FluentPersona with Icon Instead of Image (Razor)

Source: https://www.fluentui-blazor.net/Persona

Illustrates using an icon as a placeholder for the user's image, suitable for representing anonymous users. This example generates a person icon using `Icons.Regular.Size48.Person()` and converts it to a Data URI.

```Razor
<FluentPersona Name="Anonymous"
               Status="PresenceStatus.Offline"
               StatusSize="PresenceBadgeSize.Small"
               StatusTitle="Anonymous user is offline"
               Image="@(new Icons.Regular.Size48.Person().ToDataUri(size: "25px", color: "white"))"
               ImageSize="50px">
</FluentPersona>
```

--------------------------------

### Razor Combobox: Selection from Option<T> items

Source: https://www.fluentui-blazor.net/Combobox

Shows how to use FluentCombobox with a list of `Option<T>` items in Razor. This example demonstrates selecting an item and displaying its value and text, including strongly typed item details. The `OptionSelected` parameter is used to set an initial selection.

```Razor
<!-- Example assumes a setup where Option<string> items are provided and OptionSelected is configured -->
<!-- The actual item source and OptionSelected delegate configuration would be in the @code block or another component -->

<h4>From list of Option<string> items</h4>
<p>
    Second item in the list is initially selected through the `OptionSelected` (Func delegate) parameter.
</p>
<FluentCombobox Items="@OptionItems" @bind-Value="@selectedValue" OptionSelected="@((option) => HandleOptionSelected(option))" />

<p>Selected Value: @selectedValue</p>
<p>
    Selected Item (strongly typed):   
</p>
<p>
    Value: @selectedItem.Value (Type: @selectedItem.GetType().FullName)   
</p>
<p>Text: @selectedItem.Text

@code {
    // Assuming OptionItems is a List<Option<string>> and appropriately populated
    public List<Option<string>> OptionItems { get; set; } = new List<Option<string>>
    {
        new Option<string> { Value = "1", Text = "One" },
        new Option<string> { Value = "2", Text = "Two" },
        new Option<string> { Value = "3", Text = "Three" }
    };

    private string? selectedValue;
    private Option<string> selectedItem = new Option<string>();

    protected override void OnInitialized() {
        // Example of pre-selecting an item
        selectedValue = "2"; 
        // Find the corresponding Option object for display
        selectedItem = OptionItems.FirstOrDefault(o => o.Value == selectedValue) ?? new Option<string>();
    }

    private void HandleOptionSelected(Option<string> option) {
        selectedValue = option.Value;
        selectedItem = option;
    }
}

```

--------------------------------

### Fluent UI Blazor MessageBar as Notification Example

Source: https://www.fluentui-blazor.net/MessageBar

This snippet illustrates using the `FluentMessageBar` component as a notification. It configures the message bar with a title, timestamp, specific styling (including a border for demonstration), a success intent, and sets the type to `MessageType.Notification`. This allows for displaying time-sensitive information like operation confirmations.

```Razor
<FluentMessageBar Title="Delete operation"
                  Timestamp="@(DateTime.Now.AddHours(-1))"
                  Style="width: 500px; border: 1px solid var(--accent-fill-rest)"
                  Intent="MessageIntent.Success"
                  Type="@MessageType.Notification">
    Successfully deleted <i>'XYZ-blazor.pdf'</i>
</FluentMessageBar>

```

--------------------------------

### Configure and Show Panels with DialogParameters (C#)

Source: https://www.fluentui-blazor.net/Panel

This C# code defines methods to open panels using `IDialogService.ShowPanel`. It shows how to configure `DialogParameters` for both right-aligned (modal, dismiss button) and left-aligned (non-modal, no dismiss button, custom width) panels. It also includes a `HandlePanelAsync` method to process the results returned from the dialog.

```csharp
// ------------------------------------------------------------------------
// This file is licensed to you under the MIT License.
// ------------------------------------------------------------------------

using FluentUI.Demo.Shared.SampleData;
using Microsoft.FluentUI.AspNetCore.Components;

namespace FluentUI.Demo.Shared.Pages.Panel.Examples;

public partial class DialogPanel
{
    private readonly SimplePerson simplePerson = new()
    {
        Firstname = "Steve",
        Lastname = "Roth",
        Age = 42,
    };

    private void OpenPanelRight()
    {
        DemoLogger.WriteLine($"Open right panel");

#pragma warning disable CS0618 // Type or member is obsolete
        DialogService.ShowPanel<SimplePanel, SimplePerson>(new DialogParameters<SimplePerson>()
        {
            Content = simplePerson,
            Alignment = HorizontalAlignment.Right,
            Title = $"Hello {simplePerson.Firstname}",
            OnDialogResult = DialogService.CreateDialogCallback(this, HandlePanelAsync),
            PrimaryAction = "Yes",
            SecondaryAction = "No",
        });
#pragma warning restore CS0618 // Type or member is obsolete
    }

    private void OpenPanelLeft()
    {
        DemoLogger.WriteLine($"Open left panel");
        DialogParameters<SimplePerson> parameters = new()
        {
            Content = simplePerson,
            Title = $"Hello {simplePerson.Firstname}",
            OnDialogResult = DialogService.CreateDialogCallback(this, HandlePanelAsync),
            Alignment = HorizontalAlignment.Left,
            Modal = false,
            ShowDismiss = false,
            PrimaryAction = "Maybe",
            SecondaryAction = "Cancel",
            Width = "300px",
        };
#pragma warning disable CS0618 // Type or member is obsolete
        DialogService.ShowPanel<SimplePanel, SimplePerson>(parameters);
#pragma warning restore CS0618 // Type or member is obsolete
    }

    private async Task HandlePanelAsync(DialogResult result)
    {
        if (result.Cancelled)
        {
            await Task.Run(() => DemoLogger.WriteLine($"Panel cancelled"));
            return;
        }

        if (result.Data is not null)
        {
            var simplePerson = result.Data as SimplePerson;
            await Task.Run(() => DemoLogger.WriteLine($"Panel closed by {simplePerson?.Firstname} {simplePerson?.Lastname} ({{simplePerson?.Age}})"));
            return;
        }

        await Task.Run(() => DemoLogger.WriteLine($"Panel closed"));
    }
}
```

--------------------------------

### FluentSkeleton: Shimmer Effect with CSS (Razor & CSS)

Source: https://www.fluentui-blazor.net/Skeleton

Shows how to apply a shimmering effect to FluentSkeleton components using the Shimmer attribute. This example also includes isolated CSS for managing spacing within a FluentCard, demonstrating advanced styling.

```Razor
<div>
    <FluentCard Class="card-padding">
        <FluentSkeleton Shape="SkeletonShape.Circle" Shimmer="true"></FluentSkeleton>
        <FluentSkeleton Height="10px" Shimmer="true"></FluentSkeleton>
        <FluentSkeleton Height="10px" Shimmer="true"></FluentSkeleton>
        <FluentSkeleton Height="10px" Shimmer="true"></FluentSkeleton>
        <FluentSkeleton Width="75px" Height="30px" Shimmer="true"></FluentSkeleton>
    </FluentCard>
</div>
```

```CSS
::deep .card-padding fluent-skeleton:not(:first-child) {
    margin-top: 10px;
}

::deep .card-padding fluent-skeleton:last-child {
    margin-top: 20px;
    margin-bottom: 10px;
}
```

--------------------------------

### FluentToastProvider Class Parameters

Source: https://www.fluentui-blazor.net/ToastService

Configuration options for the FluentToastProvider component, controlling the appearance and behavior of toasts.

```APIDOC
## FluentToastProvider Class Parameters

### Description
Configuration options for the FluentToastProvider component, controlling the appearance and behavior of toasts.

### Parameters

#### Class Parameters

- **MaxToastCount** (int) - Optional - Gets or sets the maximum number of toasts that can be shown at once. Default is 4.
- **Position** (ToastPosition) - Optional - Gets or sets the position on screen where the toasts are shown. See `ToastPosition`. Default is `ToastPosition.TopRight`.
- **RemoveToastsOnNavigation** (bool) - Optional - Gets or sets whether to remove toasts when the user navigates to a new page. Default is true.
- **ShowCloseButton** (bool) - Optional - Gets or sets whether to show a close button on a toast. Default is true.
- **Timeout** (int) - Optional - Gets or sets the number of milliseconds a toast remains visible. Default is 7000 (7 seconds). Note: This parameter is obsolete and will be removed in a future version.
```

--------------------------------

### Razor Popover with Header, Body, and Footer

Source: https://www.fluentui-blazor.net/Popover

This example demonstrates a basic Popover component in Razor, connected to buttons using AnchorId. It includes customizable Header, Body, and Footer sections. The visibility is controlled by boolean variables. It relies on FluentButton, FluentSpacer, FluentStack, FluentCheckbox, and FluentTextField components.

```Razor
<div style="display: flex; width=100%">
    <FluentButton id="myPopoverButtonl" Appearance="Appearance.Accent" @onclick="() => _visibleL = !_visibleL">
        Open Callout 1
    </FluentButton>

    <FluentSpacer />

    <FluentButton id="myPopoverButtonr" Appearance="Appearance.Accent" @onclick="() => _visibleR = !_visibleR">
        Open Callout 2
    </FluentButton>

    <FluentPopover Style="width: 300px;" VerticalThreshold="170" AnchorId="myPopoverButtonl" @bind-Open="_visibleL">
        <Header>Callout Header</Header>
        <Body>
            <FluentStack Orientation="Orientation.Vertical">
                <FluentCheckbox>Item 1</FluentCheckbox>
                <FluentCheckbox>Item 2</FluentCheckbox>
                <FluentTextField tabindex="0"></FluentTextField>
            </FluentStack>
        </Body>
        <Footer>Callout Footer</Footer>
    </FluentPopover>

    <FluentPopover Style="width: 300px;" VerticalThreshold="170" AnchorId="myPopoverButtonr" @bind-Open="_visibleR">
        <Header>Callout Header</Header>
        <Body>
            Callout Body
        </Body>
        <Footer>Callout Footer</Footer>
    </FluentPopover>
</div>

@code {
    bool _visibleL, _visibleR;
}
```

--------------------------------

### FluentRadio States: Checked, Required, Disabled (Razor)

Source: https://www.fluentui-blazor.net/Radio

Illustrates how to set different states for the Fluent UI Blazor Radio component, including checked, required, and disabled states. These examples are intended for use within a FluentRadioGroup and leverage the Fluent UI Blazor library.

```csharp
<h4>Checked</h4>
<FluentRadioGroup @bind-Value=v1 Name="checkedstate">
    <FluentRadio Name="checkedstate" Label="Checked" Value=@("checked") Checked="true"></FluentRadio>
</FluentRadioGroup>

<h4>Required</h4>
<FluentRadioGroup @bind-Value=v2>
    <FluentRadio Value=@("required") AriaLabel="Required" Required="true"></FluentRadio>
</FluentRadioGroup>

<h4>Disabled</h4>
<FluentRadioGroup @bind-Value=v3>
    <FluentRadio Disabled="true" Label="label"></FluentRadio>
    <FluentRadio Disabled="true" Checked="true" Label="checked"></FluentRadio>
</FluentRadioGroup>
@code {
    string? v1, v2, v3;
}
```

--------------------------------

### Listbox from List of Option<T> Items in Razor

Source: https://www.fluentui-blazor.net/Listbox

This example demonstrates creating a Fluent Blazor Listbox populated from a `List<Option<string>>`. It highlights how to pre-select an item using the `OptionSelected` parameter, which accepts a `Func` delegate to determine the initially selected option based on its value.

```razor
@(new List<Option<string>>()
{
    new() { Value = "1", Content = "One" },
    new() { Value = "2", Content = "Two", OptionSelected = true },
    new() { Value = "3", Content = "Three" }
})
```

--------------------------------

### Default FluentRadio and FluentRadioGroup Usage (Razor)

Source: https://www.fluentui-blazor.net/Radio

Demonstrates the basic implementation of a FluentRadio within a FluentRadioGroup, showing how to provide a label or use an aria-label for accessibility. This requires the Fluent UI Blazor library.

```csharp
<p>Without a label: <FluentRadioGroup @bind-Value=value1><FluentRadio AriaLabel="Radio without label"></FluentRadio></FluentRadioGroup></p>
<p>With a label: <FluentRadioGroup @bind-Value=value2><FluentRadio Label="label"></FluentRadio></FluentRadioGroup></p>

@code {
    string? value1,value2;
}
```

--------------------------------

### Blazor Wizard with FluentEditForm for Multi-Step Data Entry and Validation

Source: https://www.fluentui-blazor.net/Wizard

A comprehensive Blazor component demonstrating a multi-step wizard for data entry. It utilizes FluentWizard and FluentEditForm components for each step, integrating DataAnnotationsValidator and FluentValidationSummary for robust form validation. The example includes personal info, address info, budget selection, and a signature step.

```razor
@using System.ComponentModel.DataAnnotations

@inherits Microsoft.AspNetCore.Components.ComponentBase

@inject IDialogService DialogService

<FluentWizard StepperPosition="StepperPosition.Left"
              StepSequence="@WizardStepSequence.Visited"
              DisplayStepNumber="@(WizardStepStatus.Current | WizardStepStatus.Next)"
              Border="WizardBorder.Outside"
              StepTitleHiddenWhen="@GridItemHidden.XsAndDown"
              Height="auto"
              Style="min-height: 300px;"
              OnFinish="@OnFinishedAsync">
    <Steps>
        <FluentWizardStep Label="Personal Info"
                          OnChange="@OnStepChange">
            <FluentEditForm Model="_formData1" FormName="personalInfo" OnValidSubmit="OnValidSubmit" OnInvalidSubmit="OnInvalidSubmit">
                <DataAnnotationsValidator />
                <FluentStack Orientation="Orientation.Vertical">
                    <FluentTextField Placeholder="First Name" @bind-Value="_formData1.FirstName" />
                    <FluentTextField Placeholder="Last Name" @bind-Value="_formData1.LastName" />
                </FluentStack>
                <FluentValidationSummary style="color:darkred" />
            </FluentEditForm>
        </FluentWizardStep>
        <FluentWizardStep Label="Address Info"
                          OnChange="@OnStepChange">
            <FluentEditForm Model="_formData2" FormName="addressInfo" OnValidSubmit="OnValidSubmit" OnInvalidSubmit="OnInvalidSubmit">
                <DataAnnotationsValidator />
                <FluentStack Orientation="Orientation.Vertical">
                    <FluentTextField Placeholder="Address Line 1" @bind-Value="_formData2.AddressLine1" />
                    <FluentTextField Placeholder="Address Line 2" @bind-Value="_formData2.AddressLine2" />
                    <FluentTextField Placeholder="City" @bind-Value="_formData2.City" />
                    <FluentTextField Placeholder="State or Province" @bind-Value="_formData2.StateOrProvince" />
                    <FluentTextField Placeholder="Country" @bind-Value="_formData2.Country" />
                    <FluentTextField Placeholder="Postal Code" @bind-Value="_formData2.PostalCode" />
                </FluentStack>
                <FluentValidationSummary style="color:darkred" />
            </FluentEditForm>
        </FluentWizardStep>
        <FluentWizardStep Label="Set budget"
                          Summary="Identify the best price"
                          IconPrevious="@(new Icons.Filled.Size24.Star())"
                          IconCurrent="@(new Icons.Filled.Size24.StarEmphasis())"
                          IconNext="@(new Icons.Regular.Size24.Star())"
                          DisplayStepNumber="false"
                          OnChange="@OnStepChange">
            Phasellus quis augue convallis, congue velit ac, aliquam ex. In egestas porttitor massa
            aliquet porttitor. Donec bibendum faucibus urna vitae elementum. Phasellus vitae efficitur
            turpis, eget molestie ipsum.
            <FluentSelect Items="@(Enumerable.Range(10, 80).Select(i => i.ToString()))"
                          Style="min-width: 70px;"
                          Height="300px" />
        </FluentWizardStep>
        <FluentWizardStep Label="Finish">
            <FluentEditForm Model="_finishFormData" FormName="finishForm" OnValidSubmit="OnValidSubmit" OnInvalidSubmit="OnInvalidSubmit">
                <DataAnnotationsValidator />
                <FluentStack Orientation="Orientation.Vertical">
                    <FluentTextField Label="Signature" Placeholder="Type your signature" @bind-Value="_finishFormData.Signature" />
                </FluentStack>
                <FluentValidationSummary style="color:darkred" />
            </FluentEditForm>
        </FluentWizardStep>
    </Steps>
</FluentWizard>

<FluentOverlay @bind-Visible=@_overlayIsVisible
               Opacity="0.4"
               Alignment="Align.Center"
               Justification="@JustifyContent.Center">
    <FluentProgressRing />
</FluentOverlay>

@code
{
    private FormData1 _formData1 = new FormData1();

```

--------------------------------

### ShowSplashScreenAsync

Source: https://www.fluentui-blazor.net/DialogService

Shows a splash screen dialog with specified parameters and an optional callback function.

```APIDOC
## POST /dialogs/splashscreen

### Description
Shows a splash screen of the given type with the given parameters. This method can optionally accept a callback function to be executed after the dialog is closed.

### Method
POST

### Endpoint
/dialogs/splashscreen

### Parameters
#### Query Parameters
- **type** (string) - Required - The type of the splash screen component.
- **parameters** (DialogParameters<SplashScreenContent>) - Optional - Parameters for the splash screen dialog.
- **callback** (Func<DialogResult, Task>) - Optional - A callback function to execute when the dialog is closed.

### Request Body
(Not applicable for this endpoint, parameters are passed via query string or method arguments)

### Response
#### Success Response (200)
- **IDialogReference** (object) - A reference to the displayed dialog.

#### Response Example
```json
{
  "dialogReference": {
    "id": "some-dialog-id"
  }
}
```
```

--------------------------------

### Fluent Blazor Text Field: States (Required, Disabled, ReadOnly)

Source: https://www.fluentui-blazor.net/TextField

Illustrates how to configure the state of a FluentTextField component, including making it required, disabled, or read-only. These examples use Razor syntax for Blazor applications.

```Razor
<h4>Required</h4>
<FluentTextField @bind-Value=value1 Required="true"></FluentTextField>

<h4>Disabled</h4>
<FluentTextField @bind-Value=value2 Disabled="true"></FluentTextField>
<FluentTextField @bind-Value=value3 Disabled="true">label</FluentTextField>
<FluentTextField @bind-Value=value4 Disabled="true" Placeholder="placeholder"></FluentTextField>

<h4>Read only</h4>
<FluentTextField @bind-Value=value5 ReadOnly="true"></FluentTextField>
<FluentTextField @bind-Value=value6 ReadOnly="true">label</FluentTextField>

@code{
    string? value1, value2="Disabled", value3="Disabled", value4="Disabled", value5="Read only", value6="Read only";
}
```

--------------------------------

### Dynamic Scaling of Regions via Updates (Razor)

Source: https://www.fluentui-blazor.net/AnchoredRegion

This example shows how to dynamically scale a FluentAnchoredRegion to fill its viewport when changes occur. It uses `AxisPositioningMode.Dynamic` and `AxisScalingMode.Fill` to achieve this behavior, ensuring the region adapts its size based on content updates.

```Razor
<div id="viewport-scaling-update" style="height:400px;width:400px;background: var(--neutral-layer-4);overflow:auto;position:relative;resize:both;">
    <FluentButton Appearance=Appearance.Neutral id="anchor-scaling-update" style="margin-left:100px;margin-top:100px">anchor</FluentButton>
    <FluentAnchoredRegion id="region-scaling-update" Anchor="anchor-scaling-update" Viewport="viewport-scaling-update"
                          VerticalPositioningMode="AxisPositioningMode.Dynamic"
                          VerticalScaling="AxisScalingMode.Fill"
                          HorizontalPositioningMode="AxisPositioningMode.Dynamic"
                          HorizontalScaling="AxisScalingMode.Fill">
        <div style="height:60px;width:60px;background:var(--highlight-bg);" />
    </FluentAnchoredRegion>
</div>
```

--------------------------------

### FluentMainLayout Class Parameters

Source: https://www.fluentui-blazor.net/MainLayout

Details the available parameters for the FluentMainLayout class, including their types, default values, and descriptions.

```APIDOC
## FluentMainLayout Class Parameters

### Description
This section details the configurable parameters for the `FluentMainLayout` component.

### Method
Class Definition / Component Parameters

### Endpoint
N/A

### Parameters
#### Path Parameters
None

#### Query Parameters
None

#### Request Body
None

### Request Example
N/A

### Response
#### Success Response (N/A)
N/A

#### Response Example
N/A

## Parameter Details:

- **Body** (RenderFragment?) - Optional - Gets or sets the content of the body.
- **Header** (RenderFragment?) - Optional - Gets or sets the header content.
- **HeaderHeight** (int?) - Optional - Gets or sets the height of the header (in pixels). Default is 50.
- **NavMenuContent** (RenderFragment?) - Optional - Gets or sets the content of the navigation menu.
- **NavMenuTitle** (string?) - Optional - Gets or sets the title of the navigation menu.
- **NavMenuWidth** (int) - Required - Gets or sets the width of the navigation menu. Default is 200.
- **SubHeader** (RenderFragment?) - Optional - Gets or sets the sub header content.
```

--------------------------------

### Fluent Toolbar Different Label Types (Razor)

Source: https://www.fluentui-blazor.net/Toolbar

This example showcases three ways to associate labels with a Fluent Toolbar: using a slotted label, an external label linked via 'for', and an invisible label using 'aria-label'.

```Razor
<p class="heavy">Slotted (span) label</p>
<FluentToolbar id="toolbar-slotted-label">
    <span slot="label">Span Label</span>
    <FluentButton>One</FluentButton>
    <FluentButton>Two</FluentButton>
    <FluentButton>Three</FluentButton>
</FluentToolbar>

<p class ="heavy">External label</p>
<label id="toolbar-label" for="toolbar-external-label">External label</label>
<FluentToolbar aria-labelledby="toolbar-label" id="toolbar-external-label">
    <FluentButton>One</FluentButton>
    <FluentButton>Two</FluentButton>
    <FluentButton>Three</FluentButton>
</FluentToolbar>

<p class="heavy">Invisible label</p>
<FluentToolbar id="toolbar-invisible-label" aria-label="Invisible label">
    <FluentButton>One</FluentButton>
    <FluentButton>Two</FluentButton>
    <FluentButton>Three</FluentButton>
</FluentToolbar>
```

--------------------------------

### Default Horizontal Scroll Component in Razor

Source: https://www.fluentui-blazor.net/HorizontalScroll

Demonstrates the default implementation of the FluentHorizontalScroll component in Razor. It includes event handlers for scroll start and end, logging scroll information. Requires the ILogger service for logging.

```razor
@inject ILogger<HorizontalScrollDefault> logger;
    
<FluentHorizontalScroll Speed="600" Easing=ScrollEasing.EaseInOut @onscrollstart=OnHorizontalScrollStart @onscrollend=OnHorizontalScrollEnd>
    <FluentCard>
        Card number 1
        <FluentButton Appearance="Appearance.Neutral">A button</FluentButton>
    </FluentCard>
    <FluentCard>Card number 2</FluentCard>
    <FluentCard>Card number 3</FluentCard>
    <FluentCard>Card number 4</FluentCard>
    <FluentCard>Card number 5</FluentCard>
    <FluentCard>Card number 6</FluentCard>
    <FluentCard>Card number 7</FluentCard>
    <FluentCard>Card number 8</FluentCard>
    <FluentCard>Card number 9</FluentCard>
    <FluentCard>Card number 10</FluentCard>
    <FluentCard>Card number 11</FluentCard>
    <FluentCard>Card number 12</FluentCard>
    <FluentCard>Card number 13</FluentCard>
    <FluentCard>Card number 14</FluentCard>
    <FluentCard>Card number 15</FluentCard>
    <FluentCard>Card number 16</FluentCard>
</FluentHorizontalScroll>

@code {
    private void OnHorizontalScrollStart(HorizontalScrollEventArgs args)
    {
        logger.LogInformation($"{args.Scroll}");
    }

    private void OnHorizontalScrollEnd(HorizontalScrollEventArgs args)
    {
        logger.LogInformation($"{args.Scroll}");
    }
}
```

--------------------------------

### Add Using Statement for Fluent UI Extension Methods

Source: https://www.fluentui-blazor.net/UpgradeGuide

Instructs users to add a new using statement to their _Imports.razor file and C# files to resolve potential conflicts with extension methods. This change, introduced in v4.7.0, moves extension methods to the 'Extensions' sub-namespace within Microsoft.FluentUI.AspNetCore.Components.

```csharp
using Microsoft.FluentUI.AspNetCore.Components.Extensions
```

--------------------------------

### Register Tooltip Service in Program.cs

Source: https://www.fluentui-blazor.net/Tooltip

Registers the ITooltipService with global options in the application's service collection. This is essential for global tooltip configurations and enabling click-event tooltips.

```csharp
builder.Services.AddScoped<ITooltipService, TooltipService>();
```

--------------------------------

### FluentMainLayout Component Usage

Source: https://www.fluentui-blazor.net/MainLayout

Demonstrates the basic usage of the FluentMainLayout component in a Razor file, showcasing how to define header, subheader, body, and navigation menu content.

```APIDOC
## FluentMainLayout Component

### Description
The `FluentMainLayout` component provides a flexible structure for creating page layouts in Blazor applications. It allows for distinct sections such as a header, subheader, main body, and a navigation menu.

### Method
Razor Component

### Endpoint
N/A (Client-side component)

### Parameters
#### Path Parameters
None

#### Query Parameters
None

#### Request Body
None

### Request Example
```html
<div style="height: 500px;">
    <FluentMainLayout NavMenuTitle="Navigation menu">
        <Header><h3>Fluent UI Components</h3></Header>
        <SubHeader><p>A subheader here</p></SubHeader>
        <Body>
            Lorum ipsum...
        </Body>
        <NavMenuContent>
            <FluentNavLink Href="/" Text="Home">Home</FluentNavLink>
        </NavMenuContent>
    </FluentMainLayout>
</div>
```

### Response
#### Success Response (N/A)
This component renders UI elements directly.

#### Response Example
N/A
```

--------------------------------

### Fluent Blazor Menu: Simple Context Menu on Right-Click

Source: https://www.fluentui-blazor.net/Menu

This example shows how to create a context menu that appears when a specific text area is right-clicked. It uses the `Trigger` attribute set to `MouseButton.Right` on the `FluentMenu` component and specifies the `Anchor` element. The `OnMenuChange` event handler logs the selected item.

```Razor
<div id="myId4" class="demopanel">
    Right-click on this text <br />
    to open a Context Menu.
</div>

<FluentMenu Anchor="myId4" Trigger="MouseButton.Right" Anchored="false" @onmenuchange=OnMenuChange>
    <FluentMenuItem Label="Item 1" OnClick="@((e) => DemoLogger.WriteLine("Item 1 Clicked"))" />

    <FluentMenuItem Label="Item 2">
        <MenuItems>
            <FluentMenuItem Label="Item 2.1">
                <MenuItems>
                    <FluentMenuItem Label="Item 2.1.1" OnClick="@((e) => DemoLogger.WriteLine("Item 2.1.1 Clicked"))" />
                    <FluentMenuItem Label="Item 2.1.2" OnClick="@((e) => DemoLogger.WriteLine("Item 2.1.2 Clicked"))" />
                </MenuItems>
            </FluentMenuItem>
            <FluentMenuItem Label="Item 2.2" OnClick="@((e) => DemoLogger.WriteLine("Item 2.2 Clicked"))" />
        </MenuItems>
    </FluentMenuItem>

    <FluentMenuItem Label="Item 3" OnClick="@((e) => DemoLogger.WriteLine("Item 3 Clicked"))" />
</FluentMenu>

<p>@status</p>

@code {
 
    private string status = "";

    private void OnMenuChange(MenuChangeEventArgs args)
    {
        if (args is not null && args.Value is not null)
            status = $"Item \"{args.Value}\" clicked";
    }
}
```

--------------------------------

### Fluent DataGrid with EmptyContent and LoadingContent in Razor

Source: https://www.fluentui-blazor.net/datagrid-loading-and-empty-content

This Razor code snippet illustrates how to implement custom content for loading and empty states within a FluentDataGrid component. It utilizes the ChildContent, PropertyColumn, and various alignment/sorting properties for data display. The surrounding div provides scrollable behavior for the grid.

```Razor
<div style="height: 400px; overflow-y: scroll;">
    <FluentDataGrid @ref="grid" Items=@items GridTemplateColumns="1fr 1fr 1fr 1fr">
        <ChildContent>
            <PropertyColumn Property="@(c => c.Item1)" Sortable="true" />
            <PropertyColumn Property="@(c => c.Item2)" />
            <PropertyColumn Property="@(c => c.Item3)" Align="Align.Center" />

```

--------------------------------

### Implement Custom String Comparison for Sorting in Blazor

Source: https://www.fluentui-blazor.net/datagrid-custom-comparer-sort

This Blazor code defines a custom comparer 'StringLengthComparer' that implements IComparer<string> to sort strings based on their length. This can be used with FluentDataGrid for custom sorting scenarios.

```blazor
public class StringLengthComparer : IComparer<string>
{
    public static readonly StringLengthComparer Instance = new StringLengthComparer();

    public int Compare(string? x, string? y)
    {
        if (x is null)
        {
            return y is null ? 0 : -1;
        }

        if (y is null)
        {
            return 1;
        }

        return x.Length.CompareTo(y.Length);
    }
}
```

--------------------------------

### Fluent Blazor Button Appearances and Loading State

Source: https://www.fluentui-blazor.net/Button

Demonstrates various visual appearances of the Fluent Blazor Button component, including Accent, Lightweight, Outline, Stealth, and Colored. It also shows how to implement a loading state for buttons.

```Razor
<FluentStack HorizontalGap="10">
    <FluentButton>Button</FluentButton>
    <FluentButton Appearance="Appearance.Accent">Accent</FluentButton>
    <FluentButton Appearance="Appearance.Lightweight">Lightweight</FluentButton>
    <FluentButton Appearance="Appearance.Outline">Outline</FluentButton>
    <FluentButton Appearance="Appearance.Stealth">Stealth</FluentButton>
    <FluentButton BackgroundColor="var(--highlight-bg)" Color="var(--info)">Colored</FluentButton>
    <FluentButton Loading="@loading" OnClick="@StartLoadingAsync" Appearance="Appearance.Accent">Loading</FluentButton>
</FluentStack>

@code {

    bool loading = false;

    async Task StartLoadingAsync()
    {
        loading = true;
        await DataSource.WaitAsync(2000, () => loading = false);
    }
}
```

--------------------------------

### FluentNumberField Examples for Various Numeric Types in Blazor

Source: https://www.fluentui-blazor.net/NumberField

Demonstrates the usage of FluentNumberField for different numeric types including long, short, float, double, decimal, unsigned short, unsigned integer, and unsigned long. It shows how to bind values and display ranges.

```Razor
<p>
    <FluentNumberField @bind-Value="exampleLong">Long</FluentNumberField>
    <br/>
    Example long: @exampleLong
    <br/>
    Minimum value: @(MinValue); Maximum value: @(MaxValue)
</p>
<p>
    <FluentNumberField @bind-Value="shortMin">Short</FluentNumberField>
    <br/>
    Minimum value: @(short.MinValue); Maximum value: @(short.MaxValue)
</p>

<p>
    <FluentNumberField @bind-Value="@exampleFloat">Float</FluentNumberField>
    <br/>
    Example float: @exampleFloat
    <br/>
    Minimum value: @(MinValue); Maximum value: @(MaxValue)
</p>
<p>
    <FluentNumberField Step=0.25 @bind-Value="@exampleFloat2">Float</FluentNumberField>
    <br/>
    Example float: @exampleFloat2 (step=0.25)
    <br/>
    Minimum value: @(MinValue); Maximum value: @(MaxValue)
</p>
<p>
    <FluentNumberField @bind-Value="@exampleDouble">Double</FluentNumberField>
    <br/>
    Example double: @exampleDouble
    <br/>
    Minimum value: @(MinValue); Maximum value: @(MaxValue)
</p>
<p>
    <FluentNumberField @bind-Value="@exampleDecimal">Decimal</FluentNumberField>
    <br/>
    Example decimal: @exampleDecimal
    <br/>
    Minimum value: @(MinValue); Maximum value: @(MaxValue)
</p>

<p>
    <FluentNumberField @bind-Value="@exampleUshort">Unsigned short</FluentNumberField>
    Example unsigned short: @exampleUshort
    <br/>
    Minimum value: @(ushort.MinValue); Maximum value: @(ushort.MaxValue)
</p>

<p>
    <FluentNumberField @bind-Value="@exampleUint">Unsigned integer</FluentNumberField>
    Example unsigned integer: @exampleUint
    <br/>
    Minimum value: @(uint.MinValue); Maximum value: @(uint.MaxValue)
</p>

<p>
    <FluentNumberField @bind-Value="@exampleUlong">Unsigned long</FluentNumberField>
    Example unsigned long: @exampleUlong
    <br/>
    Minimum value: @(ulong.MinValue); Maximum value: @(MaxValue)
</p>

@code {
    short shortMin = short.MinValue;
    long exampleLong { get; set; } = 9999999997;
    float exampleFloat { get; set; } = 123.45f;
    float exampleFloat2 { get; set; } = 123.45f;
    double exampleDouble { get; set; } = 456.32d;
    decimal exampleDecimal { get; set; } = Decimal.One / 3;
    ushort exampleUshort { get; set; }
    uint exampleUint { get; set; }
    ulong exampleUlong { get; set; }

    private const long MaxValue = 9999999999;
    private const long MinValue = -9999999999;
}
```

--------------------------------

### Slow Scroll Horizontal Component in Razor

Source: https://www.fluentui-blazor.net/HorizontalScroll

Demonstrates a FluentHorizontalScroll component configured for slower scrolling by setting a lower 'Speed' value. This example uses default easing and shows a series of cards within the scrollable area.

```razor
<FluentHorizontalScroll Speed=" 200" Easing=ScrollEasing.EaseInOut>
    <FluentCard>Card number 1</FluentCard>
    <FluentCard>Card number 2</FluentCard>
    <FluentCard>Card number 3</FluentCard>
    <FluentCard>Card number 4</FluentCard>
    <FluentCard>Card number 5</FluentCard>
    <FluentCard>Card number 6</FluentCard>
    <FluentCard>Card number 7</FluentCard>
    <FluentCard>Card number 8</FluentCard>
    <FluentCard>Card number 9</FluentCard>
    <FluentCard>Card number 10</FluentCard>
    <FluentCard>Card number 11</FluentCard>
    <FluentCard>Card number 12</FluentCard>
    <FluentCard>Card number 13</FluentCard>
    <FluentCard>Card number 14</FluentCard>
    <FluentCard>Card number 15</FluentCard>
    <FluentCard>Card number 16</FluentCard>
</FluentHorizontalScroll>
```

--------------------------------

### Fluent Combobox with Long List and Forced Positioning (Razor)

Source: https://www.fluentui-blazor.net/Combobox

This example demonstrates a FluentCombobox with a large number of options, simulating a long list. It also shows how to force the dropdown position to appear either above or below the input field using the 'Position' attribute.

```Razor
<h4>With long list</h4>
<FluentCombobox Id="combobox-with-long-list" Autocomplete="ComboboxAutocomplete.Both" @bind-Value="@comboboxValue" TOption="string">
    <FluentOption>Alabama</FluentOption>
    <FluentOption>Alaska</FluentOption>
    <FluentOption>Arizona</FluentOption>
    <FluentOption>Arkansas</FluentOption>
    <FluentOption>California</FluentOption>
    <FluentOption>Colorado</FluentOption>
    <FluentOption>Connecticut</FluentOption>
    <FluentOption>Delaware</FluentOption>
    <FluentOption>Florida</FluentOption>
    <FluentOption>Georgia</FluentOption>
    <FluentOption>Hawaii</FluentOption>
    <FluentOption>Idaho</FluentOption>
    <FluentOption>Illinois</FluentOption>
    <FluentOption>Indiana</FluentOption>
    <FluentOption>Iowa</FluentOption>
    <FluentOption>Kansas</FluentOption>
    <FluentOption>Kentucky</FluentOption>
    <FluentOption>Louisiana</FluentOption>
    <FluentOption>Maine</FluentOption>
    <FluentOption>Maryland</FluentOption>
    <FluentOption>Massachussets</FluentOption>
    <FluentOption>Michigain</FluentOption>
    <FluentOption>Minnesota</FluentOption>
    <FluentOption>Mississippi</FluentOption>
    <FluentOption>Missouri</FluentOption>
    <FluentOption>Montana</FluentOption>
    <FluentOption>Nebraska</FluentOption>
    <FluentOption>Nevada</FluentOption>
    <FluentOption>New Hampshire</FluentOption>
    <FluentOption>New Jersey</FluentOption>
    <FluentOption>New Mexico</FluentOption>
    <FluentOption>New York</FluentOption>
    <FluentOption>North Carolina</FluentOption>
    <FluentOption>North Dakota</FluentOption>
    <FluentOption>Ohio</FluentOption>
    <FluentOption>Oklahoma</FluentOption>
    <FluentOption>Oregon</FluentOption>
    <FluentOption>Pennsylvania</FluentOption>
    <FluentOption>Rhode Island</FluentOption>
    <FluentOption>South Carolina</FluentOption>
    <FluentOption>South Dakota</FluentOption>
    <FluentOption>Texas</FluentOption>
    <FluentOption>Tennessee</FluentOption>
    <FluentOption>Utah</FluentOption>
    <FluentOption>Vermont</FluentOption>
    <FluentOption>Virginia</FluentOption>
    <FluentOption>Washington</FluentOption>
    <FluentOption>Wisconsin</FluentOption>
    <FluentOption>West Virginia</FluentOption>
    <FluentOption>Wyoming</FluentOption>
</FluentCombobox>

<h4>With forced position above</h4>
<FluentCombobox Id="combobox-with-forced-position-above" Position="SelectPosition.Above" @bind-Value="@comboboxValue2" TOption="string">
    <FluentOption>Position forced above</FluentOption>
    <FluentOption>Option Two</FluentOption>
</FluentCombobox>

<h4>with forced position below</h4>
<FluentCombobox Id="combobox-with-forced-position-below" Position="SelectPosition.Below" @bind-Value="@comboboxValue3" TOption="string">
    <FluentOption>Position forced below</FluentOption>
    <FluentOption>Option Two</FluentOption>
</FluentCombobox>

@code {
    string? comboboxValue;
    string? comboboxValue2 ="Position forced above" ;
    string? comboboxValue3 = "Position forced below";

}
```

--------------------------------

### FluentDialog Methods

Source: https://www.fluentui-blazor.net/Dialog

This section lists the methods available for the FluentDialog component, providing functionality for managing dialog data and state.

```APIDOC
## FluentDialog Methods

This section lists the methods available for the FluentDialog component, providing functionality for managing dialog data and state.

### Methods

Name| Parameters| Type| Description
---|---|---|---
`Add`| string parameterName
Object value|
| void|
`Get<T>`| string parameterName|
| T?|
`GetDictionary`| | Dictionary<string, Object>|
`GetEnumerator`| | IEnumerator<KeyValuePair<string, Object>>|
`TryGet<T>`| string parameterName|
| T?
```

--------------------------------

### TreeView with Unlimited Items and Dynamic Loading (Razor)

Source: https://www.fluentui-blazor.net/TreeView

This example shows how to implement a Fluent TreeView with dynamically loaded items, allowing for an unlimited number of nested elements. It handles item expansion and loading states, useful for large or asynchronous data.

```Razor
<FluentTreeView Items="@Items" @bind-SelectedItem="@SelectedItem" />

<div>
    Selected Item: @SelectedItem?.Text
</div>

@code
{
    private ITreeViewItem? SelectedItem;
    private IEnumerable<ITreeViewItem>? Items = new List<ITreeViewItem>();

    protected override void OnInitialized()
    {
        Items = GetItems();
    }

    private Task OnExpandedAsync(TreeViewItemExpandedEventArgs e)
    {
        if (e.Expanded)
        {
            e.CurrentItem.Items = GetItems();
        }
        else
        {
            // Remove sub-items and add a "Fake" item to simulate the [+]
            e.CurrentItem.Items = TreeViewItem.LoadingTreeViewItems;
        }

        return Task.CompletedTask;
    }

    private IEnumerable<ITreeViewItem> GetItems()
    {
        var nbItems = Random.Shared.Next(3, 9);

        return Enumerable.Range(1, nbItems).Select(i => new TreeViewItem() 
            {
                Text = $"Item {Random.Shared.Next(1, 9999)}",
                OnExpandedAsync = OnExpandedAsync,
                Items = TreeViewItem.LoadingTreeViewItems, // "Fake" sub-item to simulate the [+] 
            }).ToArray();
    }
}
```

--------------------------------

### Render FluentIcon Component with Specific Icon

Source: https://www.fluentui-blazor.net/Icon

This example shows how to render a Fluent UI icon using the `<FluentIcon>` component in a Blazor application. It utilizes the `Value` property to specify the desired icon, such as `Icons.Regular.Size24.Bookmark()`. Using the `Value` property is recommended to prevent icons from being removed during the .NET publication process when trimming is enabled.

```razor
<FluentIcon Value="@(new Icons.Regular.Size24.Bookmark())" />
```

--------------------------------

### Fluent Blazor Menu: Open Menu with Button Click

Source: https://www.fluentui-blazor.net/Menu

This example demonstrates how to open and close a Fluent Blazor Menu by clicking a button. It uses a `@bind-Open` directive to control the menu's visibility and `@onclick` for the button. The `OnMenuChange` event handler updates a status message when a menu item is clicked.

```Razor
<p>Click this button to open a Menu.</p>
<FluentButton id="btnOpen1" Appearance="Appearance.Accent" @onclick="@(() => open = !open)">
    Open menu
</FluentButton>

<FluentMenu Anchor="btnOpen1" @bind-Open="open" @onmenuchange=OnMenuChange VerticalThreshold="170">
    <FluentMenuItem OnClick="@((e) => DemoLogger.WriteLine("Item 1 Clicked"))">
        Menu item 1
    </FluentMenuItem>
    <FluentMenuItem OnClick="@((e) => DemoLogger.WriteLine("Item 2 Clicked"))"
                   Checked="true">
        Menu item 2 Checked
    </FluentMenuItem>
    <FluentMenuItem OnClick="@((e) => DemoLogger.WriteLine("Item 3 Clicked"))"
                   Disabled="true">
        Menu item 3 Disabled
    </FluentMenuItem>
    <FluentMenuItem OnClick="@((e) => DemoLogger.WriteLine("Item 4 Clicked"))">
        <span slot="start"><FluentIcon Value="@(new Icons.Regular.Size24.ClipboardPaste())" Color="Color.Neutral" Slot="start" /></span>
        Menu item 4 with Icon
    </FluentMenuItem>
</FluentMenu>

<p>@status</p>

@code {
    bool open = false;
    private string status = "";

    private void OnMenuChange(MenuChangeEventArgs args)
    {
        if (args is not null && args.Value is not null)
            status = $"Item \"{args.Value}\" clicked";
    }
}
```

--------------------------------

### Applying Inset Positioning to FluentAnchoredRegion (Razor)

Source: https://www.fluentui-blazor.net/AnchoredRegion

This example illustrates how to apply inset positioning to a FluentAnchoredRegion, creating a margin between the region and its anchor. It uses `AxisPositioningMode.Dynamic` with `VerticalInset="true"` and `HorizontalInset="true"` to achieve this effect.

```Razor
<div id="viewport-inset" style="position:relative;height:400px;width:400px;background: var(--neutral-layer-4);overflow:auto;resize:both;">
    <FluentButton Appearance=Appearance.Neutral id="anchor-inset" style="margin-left:100px;margin-top:100px">anchor</FluentButton>
    <FluentAnchoredRegion Anchor="anchor-inset" Viewport="viewport-inset"
                          VerticalPositioningMode="AxisPositioningMode.Dynamic"
                          VerticalInset="true"
                          HorizontalPositioningMode="AxisPositioningMode.Dynamic"
                          HorizontalInset="true">
        <div style="height:100px;width:100px;background:var(--highlight-bg);;opacity:.5" />
    </FluentAnchoredRegion>
</div>
```

--------------------------------

### Configure FluentDataGrid with PropertyColumn and Sorting in Blazor

Source: https://www.fluentui-blazor.net/datagrid-custom-comparer-sort

This snippet configures a FluentDataGrid with a PropertyColumn for 'Total' medals, enabling sorting and alignment. It also demonstrates custom sorting logic using GridSort for countries by gold, silver, and bronze medals.

```blazor
<PropertyColumn Property="@(c => c.Medals.Total)" Sortable="true" Align="Align.End" />
    </FluentDataGrid>
</div>

@code {
    GridSort<Country> rankSort = GridSort<Country>
        .ByDescending(x => x.Medals.Gold)
        .ThenDescending(x => x.Medals.Silver)
        .ThenDescending(x => x.Medals.Bronze);

    // Uncomment line below when using the TemplateColumn example for the country _name
    //GridSort<Country> nameSort = GridSort<Country>.ByAscending(x => x.Name, StringLengthComparer.Instance);
}
```

--------------------------------

### Fluent Blazor RadioGroup with External Label

Source: https://www.fluentui-blazor.net/RadioGroup

Shows how to associate an external label with a FluentRadioGroup using the 'aria-labelledby' attribute for improved accessibility. This example also demonstrates vertical orientation for the radio buttons. Requires Fluent UI Blazor.

```Razor
<h4>With label outside group</h4>
<label id="label1">Outside label</label>
<FluentRadioGroup Required="true" aria-labelledby="label1" @bind-Value=fruit Name="fruits" Orientation="Orientation.Vertical">
    <FluentRadio Value="@("apples")">Apples</FluentRadio>
    <FluentRadio Value="@("oranges")">Oranges</FluentRadio>
    <FluentRadio Value="@("bananas")">Bananas</FluentRadio>
    <FluentRadio Value="@("kiwi")">Kiwi</FluentRadio>
    <FluentRadio Value="@("grapefruit")">Grapefruit</FluentRadio>
    <FluentRadio Value="@("mango")">Mango</FluentRadio>
    <FluentRadio Value="@("blueberries")">Blueberries</FluentRadio>
    <FluentRadio Value="@("strawberries")">Strawberries</FluentRadio>
    <FluentRadio Value="@("pineapple")">Pineapple</FluentRadio>
</FluentRadioGroup>


<p>Your favorite fruit: @fruit!</p>

@code
{
    string? fruit = "apples";
}
```

--------------------------------

### Fluent Blazor Tabs: Default, Vertical, Deferred Loading, and Events

Source: https://www.fluentui-blazor.net/Tabs

This Razor component demonstrates the usage of Fluent Blazor's FluentTabs and FluentTab components. It includes examples of default tabs, vertical orientation, deferred loading of content with a progress ring, and handling tab change events.

```razor
<FluentSelect Label="Size"
              Items="@(Enum.GetValues<TabSize>())"
              @bind-SelectedOption="@size" />

<FluentSwitch @bind-Value="@DeferredLoading">Use deferred loading</FluentSwitch>
<p>
If checked, the contents of Tab two and three will be loaded after 1 second of processing (to simulate a long running process). <br />
</p>

<FluentDivider />

<FluentTabs @bind-ActiveTabId="@activeid" OnTabChange="HandleOnTabChange" Size="@size" > 
    <FluentTab Label="Tab one" Icon="@(new Icons.Regular.Size24.LeafOne())" Id="tab-1">
        Tab one content. This is for testing.
    </FluentTab>
    <FluentTab Label="Tab two" Id="tab-2" DeferredLoading="@DeferredLoading">
        <LoadingContent>
            <FluentProgressRing />
        </LoadingContent>
        <Content>
        @{
            if (DeferredLoading)
            {
                Thread.Sleep(1000);
            }
        }
        Tab two content. This is for testing.
        </Content>
    </FluentTab>
    <FluentTab Label="Tab three" Id="tab-3" DeferredLoading="@DeferredLoading">
        @{
            if (DeferredLoading)
            {
                Thread.Sleep(1000);
            }
        }
        Tab three content. This is for testing.
    </FluentTab>

</FluentTabs>

<p>Active tab changed to: @changedto?.Label</p>

<h4>Vertical</h4>
<FluentTabs Orientation="Orientation.Vertical" Style="height: 200px;" Size="@size">
    <FluentTab Label="Tab one" Id="tab-v1">
        Tab one content. This is for testing.
    </FluentTab>
    <FluentTab Label="Tab two" Id="tab-v2">
        Tab two content. This is for testing.
    </FluentTab>
    <FluentTab Label="Tab three" Id="tab-v3">
        Tab three content. This is for testing.
    </FluentTab>
</FluentTabs>

@code {
    bool DeferredLoading = false;

    TabSize size;

    string? activeid = "tab-3";
    FluentTab? changedto;

    private void HandleOnTabChange(FluentTab tab)
    {
        changedto = tab;
    }
}
```

--------------------------------

### FluentSplashScreen Class Parameters

Source: https://www.fluentui-blazor.net/SplashScreen

Parameters for the FluentSplashScreen component.

```APIDOC
## FluentSplashScreen Class Parameters

### Description
Parameters for configuring the FluentSplashScreen component, including its content and dialog settings.

### Parameters

- **Content** (SplashScreenContent) - Optional - Configuration object for the splash screen content.
- **Dialog** (FluentDialog) - Optional - The associated FluentDialog component.

### Request Example
```json
{
  "Content": {
    "Title": "My App",
    "LoadingText": "Starting up..."
  },
  "Dialog": { ... }
}
```

### Response
#### Success Response (200)
- **None** - This class is a component and does not have a direct response.

#### Response Example
```json
{
  "message": "SplashScreen component rendered."
}
```
```

--------------------------------

### Clear Toasts by Intent in Fluent UI Blazor

Source: https://www.fluentui-blazor.net/Toast

Provides examples of clearing specific toasts based on their intent in Fluent UI Blazor. Methods like `ToastManager.ClearIntent()` and specific type clearing methods (e.g., `ClearSuccessToasts()`) are used.

```razor
@inject IToastService ToastService

<FluentStack Wrap=true>
    <FluentButton Appearance=Appearance.Neutral @onclick="ClearIntent">Clear specific intent</FluentButton>

    <FluentButton Appearance=Appearance.Neutral @onclick="ClearSuccess">
        <FluentIcon Value="@(new Icons.Filled.Size20.CheckmarkCircle())" Color="@Color.Success" Slot="start" />
        Clear success
    </FluentButton>

    <FluentButton Appearance=Appearance.Neutral @onclick="ClearWarning">
        <FluentIcon Value="@(new Icons.Filled.Size20.Warning())" Color="@Color.Warning" Slot="start" />
        Clear warning
    </FluentButton>

    <FluentButton Appearance=Appearance.Neutral @onclick="ClearError">
        <FluentIcon Value="@(new Icons.Filled.Size20.DismissCircle())" Color="@Color.Error" Slot="start" />
        Clear error
    </FluentButton>

    <FluentButton Appearance=Appearance.Neutral @onclick="ClearInfo">
        <FluentIcon Value="@(new Icons.Filled.Size20.Info())" Color="@Color.Info" Slot="start" />
        Clear info
    </FluentButton>

    <FluentButton Appearance=Appearance.Neutral @onclick="ClearProgress">
        <FluentIcon Value="@(new Icons.Regular.Size20.Flash())" Color="@Color.Neutral" Slot="start" />
        Clear progress
    </FluentButton>

    <FluentButton Appearance=Appearance.Neutral @onclick="ClearUpload">
        <FluentIcon Value="@(new Icons.Regular.Size20.ArrowUpload())" Color="@Color.Neutral" Slot="start" />
        Clear upload
    </FluentButton>

    <FluentButton Appearance=Appearance.Neutral @onclick="ClearDownload">
        <FluentIcon Value="@(new Icons.Regular.Size20.ArrowDownload())" Color="@Color.Neutral" Slot="start" />
        Clear download
    </FluentButton>

    <FluentButton Appearance=Appearance.Neutral @onclick="ClearEvent">
        <FluentIcon Value="@(new Icons.Regular.Size20.CalendarLtr())" Color="@Color.Neutral" Slot="start" />
        Clear event
    </FluentButton>

    <FluentButton Appearance=Appearance.Neutral @onclick="ClearAvatar">
        <FluentIcon Value="@(new Icons.Regular.Size20.Person())" Color="@Color.Neutral" Slot="start" />
        Clear avatar
    </FluentButton>

    <FluentButton Appearance=Appearance.Neutral @onclick="ClearCustomIntent">
        <FluentIcon Value="@(new Icons.Regular.Size20.Delete())" Color="@Color.Neutral" Slot="start" />
        Clear custom
    </FluentButton>
</FluentStack>

@code {
    private void ClearIntent()
        => ToastService.ClearIntent(ToastIntent.Warning);

    private void ClearSuccess()
        => ToastService.ClearSuccessToasts();

    private void ClearWarning()
        => ToastService.ClearWarningToasts();

    private void ClearError()
        => ToastService.ClearErrorToasts();

    private void ClearInfo()
        => ToastService.ClearInfoToasts();

    private void ClearProgress()
       => ToastService.ClearProgressToasts();

    private void ClearUpload()
        => ToastService.ClearUploadToasts();

    private void ClearDownload()
       => ToastService.ClearDownloadToasts();

    private void ClearEvent()
        => ToastService.ClearEventToasts();

    private void ClearAvatar()
       => ToastService.ClearMentionToasts();

    private void ClearCustomIntent()
        => ToastService.ClearCustomIntentToasts();
}
```

--------------------------------

### Control FluentSelect Position in Blazor

Source: https://www.fluentui-blazor.net/Select

This example demonstrates how to force the FluentSelect dropdown to appear either above or below the select element using the 'Position' parameter. It utilizes the `SelectPosition.Above` and `SelectPosition.Below` enum values for this purpose. The selected value is bound using `@bind-Value`.

```Razor
<h4>Forced position above</h4>
<FluentSelect Position="SelectPosition.Above" @bind-Value="@selectValue" TOption="string">
    <FluentOption>Position forced above</FluentOption>
    <FluentOption>Option Two</FluentOption>
</FluentSelect>

<h4>Forced position below</h4>
<FluentSelect Position="SelectPosition.Below" @bind-Value="@selectValue" TOption="string">
    <FluentOption>Position forced below</FluentOption>
    <FluentOption>Option Two</FluentOption>
</FluentSelect>

@code {
    string? selectValue;
}
```

--------------------------------

### FluentToastProvider Class Methods

Source: https://www.fluentui-blazor.net/ToastService

Methods available for interacting with and managing toasts displayed by the FluentToastProvider.

```APIDOC
## FluentToastProvider Class Methods

### Description
Methods available for interacting with and managing toasts displayed by the FluentToastProvider.

### Methods

#### `ShowWarning`

##### Description
Shows a simple warning confirmation toast. Only shows icon, title and close button or action.

##### Parameters

- **title** (string) - Required - The title of the warning toast.
- **timeout** (int?) - Optional - The duration in milliseconds the toast will be visible.
- **topAction** (string) - Optional - Text for the action button on the toast.
- **callback** (EventCallback<ToastResult>?) - Optional - Callback event when the toast action is triggered or closed.

##### Return Type
void

#### `UpdateToast<TContent>`

##### Description
Updates an existing toast.

##### Parameters

- **id** (string) - Required - The unique identifier of the toast to update.
- **parameters** (ToastParameters<TContent>) - Required - The parameters to update the toast with.

##### Return Type
void

#### `RemoveToast`

##### Description
Removes a specific toast.

##### Parameters

- **toastId** (string) - Required - The unique identifier of the toast to remove.

##### Return Type
void

##### Positions

- BottomLeft
- BottomStart
- BottomCenter
- BottomRight
- BottomEnd
- TopLeft
- TopStart
- TopCenter
- TopRight
- TopEnd
```

--------------------------------

### Slotted Toolbar End Items with Minimal Width (Razor)

Source: https://www.fluentui-blazor.net/Toolbar

This example demonstrates how to use slotted elements at the end of a Fluent Toolbar, such as radio groups for filtering and a search text field. It also shows how to set a minimum width for the toolbar container.

```Razor
<div style="padding:10px;min-width:800px">
    <FluentToolbar id="toolbar-fluent-components-two" style="width:100%">
        <label slot="label">slotted label</label>
        <FluentRadioGroup @bind-Value=value1>
            <FluentRadio Value=@("one") Checked="true">Add</FluentRadio>
            <FluentRadio Value=@("two")>Open</FluentRadio>
            <FluentRadio Value=@("three")>Copy</FluentRadio>
        </FluentRadioGroup>
        <FluentButton Appearance="Appearance.Accent">Refresh</FluentButton>
        <FluentBadge>21 items</FluentBadge>
        <FluentRadioGroup @bind-Value=value2 slot="end">
            <FluentRadio>Filter</FluentRadio>
            <FluentRadio>
                <FluentTextField @bind-Value=text1 Placeholder="Search"></FluentTextField>
            </FluentRadio>
        </FluentRadioGroup>
        <FluentIcon Value="@(new Icons.Filled.Size16.Globe())" Slot="end" Color="Color.Accent" />
    </FluentToolbar>
</div>
@code {
    string? value1, value2, text1;
}
```

--------------------------------

### DataGrid Custom Column Headers with Templates in Razor

Source: https://www.fluentui-blazor.net/datagrid-header-generation

Demonstrates how to set custom column header titles in a DataGrid using the `HeaderCellTitleTemplate` property. This allows for richer header content, including icons and custom text. It requires the `Microsoft.FluentUI.AspNetCore.Components` namespace and potentially other Fluent UI components like `FluentStack` and `FluentIcon`.

```Razor
@using FluentUI.Demo.Shared.Pages.DataGrid.Examples

@using Microsoft.FluentUI.AspNetCore.Components

<FluentDataGrid Items="@people">
    <PropertyColumn Property="@(p => p.PersonId)" Sortable="true" Title="Identity">
        <HeaderCellTitleTemplate>
            <FluentStack Orientation="Orientation.Horizontal"
                         VerticalAlignment="VerticalAlignment.Center">
                <FluentIcon Icon="Icons.Regular.Size20.Person" />
                @context.Title
            </FluentStack>
        </HeaderCellTitleTemplate>
    </PropertyColumn>
    <PropertyColumn Property="@(p => p.Name)" Sortable="true" Title="Full _name" />
    <PropertyColumn Property="@(p => p.BirthDate)" Format="yyyy-MM-dd" Sortable="true">
        <HeaderCellTitleTemplate>
            <FluentStack Orientation="Orientation.Horizontal"
                         VerticalAlignment="VerticalAlignment.Center">
                <FluentIcon Icon="Icons.Regular.Size20.Calendar" />
                Birth date
            </FluentStack>
        </HeaderCellTitleTemplate>
    </PropertyColumn>
</FluentDataGrid>

@code {
    public class Person
    {
        public Person(int personId, string name, DateOnly birthDate)
        {
            PersonId = personId;
            Name = name;
            BirthDate = birthDate;
        }

        public int PersonId { get; set; }

        public string Name { get; set; }

        public DateOnly BirthDate { get; set; }
    }

    IQueryable<Person> people = new[]
    {
        new Person(10895, "Jean Martin", new DateOnly(1985, 3, 16)),
        new Person(10944, "António Langa", new DateOnly(1991, 12, 1)),
        new Person(11203, "Julie Smith", new DateOnly(1958, 10, 10)),
        new Person(11205, "Nur Sari", new DateOnly(1922, 4, 27)),
        new Person(11898, "Jose Hernandez", new DateOnly(2011, 5, 3)),
        new Person(12130, "Kenji Sato", new DateOnly(2004, 1, 9)),
    }.AsQueryable();
}
```

--------------------------------

### Fluent Combobox with Custom Option Template (Razor)

Source: https://www.fluentui-blazor.net/Combobox

This example showcases how to use a custom 'OptionTemplate' within a FluentCombobox to define the display of each option. It includes adding an icon to the end of each option and displaying formatted text, binding to a complex object.

```Razor
@inject DataSource Data

<FluentCombobox Items="@Data.People"
                OptionValue="@(i => i.PersonId.ToString())"
                OptionText="@(i => $"{i.FirstName} ({i.LastName})")"
                @bind-Value="@value"
                @bind-SelectedOption=@Person>
    <OptionTemplate>
        <FluentIcon Value="@(new Icons.Regular.Size16.Person())" Slot="end" OnClick="@(() => DemoLogger.WriteLine($"Icon for {@context.LastName} selected"))" />
        @context.FirstName (@context.LastName)
    </OptionTemplate>
</FluentCombobox>
<p>
    Selected option: @Person <br />
    Selected value: @value
</p>

@code {
    public Person Person { get; set; } = default!;
    public string? value;
}
```

--------------------------------

### Display Communication Toasts with Fluent UI Blazor

Source: https://www.fluentui-blazor.net/Toast

This code snippet demonstrates how to trigger communication toasts using the IToastService in Fluent UI Blazor. It includes examples for success and error intents, with configurable primary and secondary actions, titles, subtitles, details, and timeouts. The `ShowCommunicationToast` method is used, requiring `ToastParameters` with a generic type for content.

```Razor
@inject IToastService ToastService

<FluentButton Appearance=Appearance.Neutral @onclick="@ShowExample1">Communication toast example 1</FluentButton>
<FluentButton Appearance=Appearance.Neutral @onclick="@ShowExample2">Communication toast example 2</FluentButton>


@code {
        private void ShowExample1()
        {
            ToastService.ShowCommunicationToast(new ToastParameters<CommunicationToastContent>()
                {
                    Intent = ToastIntent.Success,
                    Title = "Your dataset is ready",
                    Timeout = 4000,
                    PrimaryAction = "See dataset",
                    OnPrimaryAction = EventCallback.Factory.Create<ToastResult>(this, ClickedPrimary),
                    SecondaryAction = "Get insights",
                    OnSecondaryAction = EventCallback.Factory.Create<ToastResult>(this, ClickedSecondary),
                    Content = new CommunicationToastContent()
                    {
                        Subtitle = "A communication toast subtitle",
                        Details = "Let Power BI help you explore your data.",
                    },
                });
        }

        private void ShowExample2()
        {
            ToastService.ShowCommunicationToast(new ToastParameters<CommunicationToastContent>()
                {
                    Intent = ToastIntent.Error,
                    Title = "File didn't upload to ABC folder",
                    TopCTAType = ToastTopCTAType.Timestamp,
                    Timeout = 8000,
                    PrimaryAction = "Replace",
                    OnPrimaryAction = EventCallback.Factory.Create<ToastResult>(this, ClickedPrimary),
                    SecondaryAction = "Keep both",
                    OnSecondaryAction = EventCallback.Factory.Create<ToastResult>(this, ClickedSecondary),
                    Content = new CommunicationToastContent()
                    {
                        Details = "A file with the same _name already exists.",
                    },
                });
        }


        private void ClickedPrimary(ToastResult result)
        {
            DemoLogger.WriteLine("Clicked primary action");
        }

        private void ClickedSecondary()
        {
            DemoLogger.WriteLine("Clicked secondary action");
        }
}
```

--------------------------------

### PresenceBadge: Showing Various Statuses in Blazor

Source: https://www.fluentui-blazor.net/PresenceBadge

This example illustrates the use of the FluentPresenceBadge component to display a range of predefined statuses: Available, Away, Busy, DoNotDisturb, Offline, OutOfOffice, and Unknown. It utilizes images for visual status indication and includes a case for a badge with no defined status. The default status is Available.

```Razor
<style>
    .face {
        width: 32px;
        height: 32px;
        border: 1px solid silver;
    }
</style>

<FluentPresenceBadge Status="PresenceStatus.Available">
    <img src="@DataSource.ImageFaces[0]" class="face" />
</FluentPresenceBadge>

<FluentSpacer Width="25" />

<FluentPresenceBadge Status="PresenceStatus.Away">
    <img src="@DataSource.ImageFaces[1]" class="face" />
</FluentPresenceBadge>

<FluentSpacer Width="25" />

<FluentPresenceBadge Status="PresenceStatus.Busy">
    <img src="@DataSource.ImageFaces[2]" class="face" />
</FluentPresenceBadge>

<FluentSpacer Width="25" />

<FluentPresenceBadge Status="PresenceStatus.DoNotDisturb">
    <img src="@DataSource.ImageFaces[3]" class="face" />
</FluentPresenceBadge>

<FluentSpacer Width="25" />

<FluentPresenceBadge Status="PresenceStatus.Offline">
    <img src="@DataSource.ImageFaces[4]" class="face" />
</FluentPresenceBadge>

<FluentSpacer Width="25" />

<FluentPresenceBadge Status="PresenceStatus.OutOfOffice">
    <img src="@DataSource.ImageFaces[5]" class="face" />
</FluentPresenceBadge>

<FluentSpacer Width="25" />

<FluentPresenceBadge Status="PresenceStatus.Unknown">
    <img src="@DataSource.ImageFaces[6]" class="face" />
</FluentPresenceBadge>

<FluentSpacer Width="25" />

<FluentPresenceBadge Title="No status defined">
    <img src="@DataSource.ImageFaces[7]" class="face" />
</FluentPresenceBadge>
```

--------------------------------

### Add Icons to FluentNumberField (Razor)

Source: https://www.fluentui-blazor.net/NumberField

Illustrates how to add icons to the FluentNumberField component, either in the start or end slot, using the FluentIcon component. This allows for visual cues or actions associated with the input field. It requires the `Icons.Regular.Size16.Globe()` and `Color.Neutral` for icon and color respectively.

```Razor
<h4>Icons</h4>

<FluentNumberField @bind-Value="value1" Label="With start">
    <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="start" />  
</FluentNumberField>


<FluentNumberField @bind-Value="value2" Label="With end">
    <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="end" />  
</FluentNumberField>

@code {
    int? value1, value2;
}
```

--------------------------------

### Razor Popover with Empty Parts (Header/Body/Footer)

Source: https://www.fluentui-blazor.net/Popover

This Razor example showcases Popovers with selectively omitted Header, Body, or Footer sections. It uses AnchorId to link popovers to buttons and demonstrates control over `AutoFocus` and `HorizontalPosition`. Visibility is managed by boolean variables.

```Razor
<div style="display: flex; width=100%">
    <FluentButton id="myPopoverButtonH" Appearance="Appearance.Accent" @onclick="() => _visibleL = !_visibleL">
        Open Callout 1
    </FluentButton>

    <FluentSpacer />

    <FluentButton id="myPopoverButtonM" Appearance="Appearance.Accent" @onclick="() => _visibleM = !_visibleM">
        Open Callout 2
    </FluentButton>

    <FluentSpacer />

    <FluentButton id="myPopoverButtonF" Appearance="Appearance.Accent" @onclick="() => _visibleR = !_visibleR">
        Open Callout 3
    </FluentButton>

    <FluentPopover Style="width: 300px;" AnchorId="myPopoverButtonH" @bind-Open="_visibleL" AutoFocus="false">
        <Body>
            Callout Body (no header)
        </Body>
        <Footer>Callout Footer</Footer>
    </FluentPopover>

    <FluentPopover Style="width: 300px;" AnchorId="myPopoverButtonM" @bind-Open="_visibleM" HorizontalPosition="HorizontalPosition.Center" AutoFocus="false">
        <Header>Callout Header</Header>
        <Footer>Callout Footer (no body)</Footer>
    </FluentPopover>

    <FluentPopover Style="width: 300px;" AnchorId="myPopoverButtonF" @bind-Open="_visibleR" AutoFocus="false">
        <Header>Callout Header</Header>
        <Body>
            Callout Body (no footer)
        </Body>

    </FluentPopover>
</div>

@code {
    bool _visibleL, _visibleR, _visibleM;
}
```

--------------------------------

### Grid Methods

Source: https://www.fluentui-blazor.net/datagrid

This section details the various methods available for interacting with the Fluent UI Blazor Grid component, including data manipulation, sorting, resizing, and UI control.

```APIDOC
## Grid Methods

### CloseColumnOptionsAsync

#### Description
Closes the `ColumnBase.ColumnOptions` UI that was previously displayed.

### Method
`CloseColumnOptionsAsync`

### Parameters
None

### DisposeAsync

#### Description
Disposes of the grid component's resources asynchronously.

### Method
`DisposeAsync`

### Parameters
None

### OnKeyDownAsync

#### Description
Handles key down events for the grid.

### Method
`OnKeyDownAsync`

### Parameters
- **args** (FluentKeyCodeEventArgs) - The key event arguments.

### RefreshDataAsync

#### Description
Instructs the grid to re-fetch and render the current data from the supplied data source (either `Items` or `ItemsProvider`).

### Method
`RefreshDataAsync`

### Parameters
- **force** (bool) - If true, forces a data refresh even if no changes are detected.

### RemoveSortByColumnAsync (by ColumnBase)

#### Description
Removes the grid's sort on double click for the currently sorted column if it's not a default sort column.

### Method
`RemoveSortByColumnAsync`

### Parameters
- **column** (ColumnBase<TGridItem>) - The column to remove sorting from.

### RemoveSortByColumnAsync (no parameters)

#### Description
Removes the grid's sort on double click for the currently sorted column if it's not a default sort column.

### Method
`RemoveSortByColumnAsync`

### Parameters
None

### ResetColumnWidthsAsync

#### Description
Resets the column widths to their initial values as specified with the `GridTemplateColumns` parameter. If no value is specified, the default value is '1fr' for each column.

### Method
`ResetColumnWidthsAsync`

### Parameters
None

### SetColumnWidthDiscreteAsync

#### Description
Resizes the column width by a discrete amount.

### Method
`SetColumnWidthDiscreteAsync`

### Parameters
- **columnIndex** (int?) - The index of the column to resize.
- **widthChange** (float) - The amount to change the column width by.

### SetColumnWidthExactAsync

#### Description
Resizes the column width to the exact width specified (in pixels).

### Method
`SetColumnWidthExactAsync`

### Parameters
- **columnIndex** (int) - The index of the column to resize.
- **width** (int) - The exact width in pixels.

### SetLoadingState

#### Description
Sets the loading state of the grid.

### Method
`SetLoadingState`

### Parameters
- **loading** (bool?) - The loading state to set.

### ShowColumnOptionsAsync (by ColumnBase)

#### Description
Displays the `ColumnBase.ColumnOptions` UI for the specified column, closing any other column options UI that was previously displayed. If the index is out of range, nothing happens.

### Method
`ShowColumnOptionsAsync`

### Parameters
- **column** (ColumnBase<TGridItem>) - The column to display options for.

### ShowColumnOptionsAsync (by title)

#### Description
Displays the `ColumnBase.ColumnOptions` UI for the specified column, closing any other column options UI that was previously displayed. If the index is out of range, nothing happens.

### Method
`ShowColumnOptionsAsync`

### Parameters
- **title** (string) - The title of the column to display options for.

### ShowColumnOptionsAsync (by index)

#### Description
Displays the `ColumnBase.ColumnOptions` UI for the specified column, closing any other column options UI that was previously displayed. If the index is out of range, nothing happens.

### Method
`ShowColumnOptionsAsync`

### Parameters
- **index** (int) - The index of the column to display options for.

### ShowColumnResizeAsync (by ColumnBase)

#### Description
Displays the column resize UI for the specified column, closing any other column resize UI that was previously displayed.

### Method
`ShowColumnResizeAsync`

### Parameters
- **column** (ColumnBase<TGridItem>) - The column to display resize UI for.

### ShowColumnResizeAsync (by title)

#### Description
Displays the column resize UI for the specified column, closing any other column resize UI that was previously displayed.

### Method
`ShowColumnResizeAsync`

### Parameters
- **title** (string) - The title of the column to display resize UI for.

### ShowColumnResizeAsync (by index)

#### Description
Displays the column resize UI for the specified column, closing any other column resize UI that was previously displayed.

### Method
`ShowColumnResizeAsync`

### Parameters
- **index** (int) - The index of the column to display resize UI for.

### SortByColumnAsync (by ColumnBase)

#### Description
Sorts the grid by the specified column. If the index is out of range, nothing happens.

### Method
`SortByColumnAsync`

### Parameters
- **column** (ColumnBase<TGridItem>) - The column to sort by.
- **direction** (SortDirection) - The sorting direction.

### SortByColumnAsync (by title)

#### Description
Sorts the grid by the specified column. If the index is out of range, nothing happens.

### Method
`SortByColumnAsync`

### Parameters
- **title** (string) - The title of the column to sort by.
- **direction** (SortDirection) - The sorting direction.

### SortByColumnAsync (by index)

#### Description
Sorts the grid by the specified column. If the index is out of range, nothing happens.

### Method
`SortByColumnAsync`

### Parameters
- **index** (int) - The index of the column to sort by.
- **direction** (SortDirection) - The sorting direction.

### UpdateItemsPerPageAsync

#### Description
Updates the `Pagination`s ItemPerPage parameter. Guards the CurrentPageIndex from getting greater than the LastPageIndex.

### Method
`UpdateItemsPerPageAsync`

### Parameters
- **visibleRows** (int) - The number of visible rows per page.
```

--------------------------------

### Razor: FluentSplitter Custom Panel Sizing

Source: https://www.fluentui-blazor.net/Splitter

This example shows how to set custom sizes for panels within a Fluent Blazor Splitter. It covers scenarios where one panel's size is defined, allowing the other to adjust automatically, and also demonstrates setting fixed sizes for both panels, noting the behavior when grid-template-row has a max-content value.

```html
<Panel2>
    <FluentSplitter>
        <Panel2>
            <p>Panel 1 size is set to 25%, no size is set for panel 2. Panel 2 size is therefore automatically set to 75%</p>
        </Panel2>
    </FluentSplitter>
</Panel2>

<Panel2>
    <FluentSplitter>
        <Panel2>
            <p>Panel 2 size is set to 25%, no size is set for panel 1. Panel 1 size is therefore automatically set to 75%</p>
        </Panel2>
    </FluentSplitter>
</Panel2>

<Panel2>
    <FluentSplitter>
        <Panel2>
            <p>Both panel 1 and panel 2 size are set to 25%. Because of the grid-template-row with a max-content value, the total width is set to 50% of the container width.
This sizing will be undone as soon as a resize of the splitter is underway. This can not be changed or adjusted because of the internal working of the splitter.</p>
        </Panel2>
    </FluentSplitter>
</Panel2>
```

--------------------------------

### FluentMenuItem with Standard HTML Elements in Blazor

Source: https://www.fluentui-blazor.net/Menu

An example demonstrating the use of standard HTML div elements with the 'role="menuitem"' attribute within a FluentMenu component in Blazor. This approach bypasses the need for specific FluentUI components for menu items and can be used when icons or complex content are not required.

```razor
<FluentSwitch @bind-Value="@open">Show</FluentSwitch>
<FluentMenu @bind-Open="@open">
    <div role="menuitem">Menu item 1</div>
    <div role="menuitem">Menu item 2</div>
    <div role="menuitem">Menu item 3</div>
</FluentMenu>

@code {
    bool open = false;
}

```

--------------------------------

### Locking Regions to Default Positions with FluentAnchoredRegion (Razor)

Source: https://www.fluentui-blazor.net/AnchoredRegion

This example demonstrates how to lock FluentAnchoredRegion to specific default positions relative to its anchor. It utilizes `AxisPositioningMode.Locktodefault` for both vertical and horizontal positioning, with `VerticalDefaultPosition` and `HorizontalDefaultPosition` attributes to define the anchoring points.

```Razor
<div id="viewport-locked" style="position:relative;height:400px;width:400px;background: var(--neutral-layer-4);overflow:auto; resize:both;">
    <FluentAnchoredRegion Anchor="anchor-locked" Viewport="viewport-locked"
                          VerticalPositioningMode="AxisPositioningMode.Locktodefault"
                          VerticalDefaultPosition="VerticalPosition.Bottom"
                          HorizontalPositioningMode="AxisPositioningMode.Locktodefault"
                          HorizontalDefaultPosition="HorizontalPosition.Right">
        <div style="height:150px;width:150px;background:var(--neutral-layer-2);" />
    </FluentAnchoredRegion>
    <div style="position:relative;height:0;width:0">
        <FluentAnchoredRegion Anchor="anchor-locked" Viewport="viewport-locked"
                              VerticalPositioningMode="AxisPositioningMode.Locktodefault"
                              VerticalDefaultPosition="VerticalPosition.Top"
                              HorizontalPositioningMode="AxisPositioningMode.Locktodefault"
                              HorizontalDefaultPosition="HorizontalPosition.Right">
            <div style="height:100px;width:100px;background:var(--highlight-bg);" />
        </FluentAnchoredRegion>
    </div>
    <FluentAnchoredRegion Anchor="anchor-locked" Viewport="viewport-locked"
                          VerticalPositioningMode="AxisPositioningMode.Locktodefault"
                          VerticalDefaultPosition="VerticalPosition.Top"
                          HorizontalPositioningMode="AxisPositioningMode.Locktodefault"
                          HorizontalDefaultPosition="HorizontalPosition.Left">
        <div style="height:50px;width:50px;background:var(--accent-fill-active);" />
    </FluentAnchoredRegion>
    <div />
    <FluentButton Appearance=Appearance.Neutral id="anchor-locked" style="margin-left:100px;margin-top:150px">anchor</FluentButton>
</div>
```

--------------------------------

### Custom String Length Comparer for FluentDataGrid

Source: https://www.fluentui-blazor.net/datagrid-custom-comparer-sort

Implements a custom comparer for sorting strings by length, used within a FluentDataGrid's PropertyColumn. This allows for sorting based on criteria other than default alphabetical order. It's defined in a separate C# file for reusability.

```csharp
public class StringLengthComparer : IComparer<string>
{
    public static readonly StringLengthComparer Instance = new StringLengthComparer();

    public int Compare(string x, string y)
    {
        if (x == null && y == null)
            return 0;
        if (x == null)
            return -1;
        if (y == null)
            return 1;

        return x.Length.CompareTo(y.Length);
    }
}
```

--------------------------------

### Fluent Blazor Calendar: Default Usage with Different Views

Source: https://www.fluentui-blazor.net/DateTime

Demonstrates the default usage of the FluentCalendar component in Blazor, showcasing its adaptability to display days, months, and years. It includes examples of binding selected values, managing picker months, and disabling specific dates, months, and years using custom functions. This snippet requires the Fluent UI Blazor library.

```Razor
<FluentGrid>
    <FluentGridItem>
        <FluentCalendar DisabledDateFunc="@DisabledDay" @bind-Value="@SelectedDay" @bind-PickerMonth="@PickerDay" Style="height: 250px;" />
        <p>Selected @(SelectedDay?.ToString("yyyy-MM-dd"))</p>
        <p>Panel @(PickerDay.ToString("yyyy-MM-dd"))</p>
    </FluentGridItem>
    <FluentGridItem>
        <FluentCalendar DisabledDateFunc="@DisableMonth" View="CalendarViews.Months" @bind-Value="@SelectedMonth" @bind-PickerMonth="@PickerMonth" Style="height: 250px;" />
        <p>Selected @(SelectedMonth?.ToString("yyyy-MM-dd"))</p>
        <p>Panel @(PickerMonth.ToString("yyyy-MM-dd"))</p>
    </FluentGridItem>
    <FluentGridItem>
        <FluentCalendar DisabledDateFunc="@DisableYear" View="CalendarViews.Years" @bind-Value="@SelectedYear" @bind-PickerMonth="@PickerYear" Style="height: 250px;" />
        <p>Selected @(SelectedYear?.ToString("yyyy-MM-dd"))</p>
        <p>Panel @(PickerYear.ToString("yyyy-MM-dd"))</p>
    </FluentGridItem>
</FluentGrid>

@code
{
    private DateTime? SelectedDay = null;
    private DateTime PickerDay = DateTime.Today;
    private DateTime? SelectedMonth = null;
    private DateTime PickerMonth = DateTime.Today;
    private DateTime? SelectedYear = null;
    private DateTime PickerYear = DateTime.Today;

    private bool DisabledDay(DateTime date) => date.Day == 3 || date.Day == 8 || date.Day == 20;
    private bool DisableMonth(DateTime date) => date.Month == 3 || date.Month == 8;
    private bool DisableYear(DateTime date) => date.Year == 2026 || date.Year == 2027;
}
```

--------------------------------

### FluentDataGrid with Custom Comparer and Styling (Razor)

Source: https://www.fluentui-blazor.net/datagrid-custom-comparer-sort

Configures a FluentDataGrid to use a custom string length comparer for the 'Name' column, enables row hover effects with an alternative color, and demonstrates pagination template usage. It includes dynamic styling for the hover color and resize handle.

```razor
@inject DataSource Data

@if (altcolor)
{
    <style>
        :root {
            --datagrid-hover-color: lightyellow;

        }

        .fluent-data-grid {
            --fluent-data-grid-resize-handle-color: var(--neutral-stroke-rest) !important;
        }
    </style>

}


<FluentToolbar>
    <FluentRadioGroup Name="rt" @bind-Value="@_resizeType" Label="Resize type">
        <FluentRadio Value="@DataGridResizeType.Discrete">Discrete</FluentRadio>
        <FluentRadio Value="@DataGridResizeType.Exact">Exact</FluentRadio>
    </FluentRadioGroup>
    <FluentSpacer Width="25" />
    <FluentCheckbox @bind-Value="@_showActionsMenu" Label="Use menu for column actions" />
    <FluentCheckbox @bind-Value="@_useMenuService" Label="Use service for rendering menu" Disabled="!_showActionsMenu" />
    <FluentCheckbox @bind-Value="@_resizeColumnOnAllRows" Label="Resize column on all rows" />
</FluentToolbar>
<div style="height: 400px; overflow-x:auto; display:flex;">
    <FluentDataGrid Items="@FilteredItems"
                    ResizableColumns=true
                    ResizeType="@_resizeType"
                    ResizeColumnOnAllRows="@_resizeColumnOnAllRows"
                    HeaderCellAsButtonWithMenu="_showActionsMenu"
                    UseMenuService="_useMenuService"
                    Pagination="@pagination"
                    TGridItem="Country"
                    OnRowFocus="HandleRowFocus"
                    GridTemplateColumns="0.2fr 1fr 0.2fr 0.2fr 0.2fr 0.2fr"
                    ShowHover="true">
        <TemplateColumn Title="Rank" Sortable="true"  SortBy="@rankSort" Align="Align.Center">
            <img class="flag" src="_content/FluentUI.Demo.Shared/flags/@(context.Code).svg" alt="Flag of @(context.Code)" />
        </TemplateColumn>
        <PropertyColumn Property="@(c => c.Name)" InitialSortDirection=SortDirection.Descending Sortable="true" IsDefaultSortColumn=true Comparer="@StringLengthComparer.Instance" Filtered="!string.IsNullOrWhiteSpace(nameFilter)">
            <ColumnOptions>
                <div class="search-box">
                    <FluentSearch type="search" Autofocus=true @bind-Value=nameFilter @oninput="HandleCountryFilter" @bind-Value:after="HandleClear" Placeholder="Country name..." />
                </div>
            </ColumnOptions>
        </PropertyColumn>
        <PropertyColumn Property="@(c => c.Medals.Gold)" Sortable="true" Align="Align.Start" />
        <PropertyColumn Property="@(c => c.Medals.Silver)" Sortable="true" Align="Align.Center" />
        <PropertyColumn Property="@(c => c.Medals.Bronze)" Sortable="true" Align="Align.End" />
    </FluentDataGrid>
</div>
```

--------------------------------

### Three-State Checkbox List Management in Blazor

Source: https://www.fluentui-blazor.net/Checkbox

Provides an example of managing a list of checkboxes using the three-state functionality in Blazor. This allows for 'all selected', 'none selected', or 'partially selected' states. Requires Fluent UI Blazor component library and System.Collections.Immutable.

```Razor
@using System.Collections.Immutable
<FluentStack Style="margin: 20px;" Orientation="Orientation.Vertical">
    <FluentCheckbox Label="@($"All ({AreAllTypesVisible?.ToString() ?? "null"})")"
                    ThreeState="true"
                    ShowIndeterminate="false"
                    @bind-CheckState="AreAllTypesVisible" />
    @foreach (string resourceType in _allResourceTypes)
    {
        bool isChecked = _visibleResourceTypes.Contains(resourceType);
        <FluentCheckbox Label="@($"{resourceType} ({isChecked})")"
                        @bind-Value:get="isChecked"
                        @bind-Value:set="c => OnResourceTypeVisibilityChanged(resourceType, c)" />
    }
</FluentStack>


@code {

    private readonly ImmutableArray<string> _allResourceTypes = ["Project", "Executable", "Container"];
    private readonly HashSet<string> _visibleResourceTypes;

    public CheckboxThreeStateList()
    {
        _visibleResourceTypes = new HashSet<string>(_allResourceTypes);
    }

    protected void OnResourceTypeVisibilityChanged(string resourceType, bool isVisible)
    {
        if (isVisible)
        {
            _visibleResourceTypes.Add(resourceType);
        }
        else
        {
            _visibleResourceTypes.Remove(resourceType);
        }
    }

    private bool? AreAllTypesVisible
    {
        get
        {
            return _visibleResourceTypes.SetEquals(_allResourceTypes)
                ? true
                : _visibleResourceTypes.Count == 0
                    ? false
                    : null;
        }
        set
        {
            if (value is true)
            {
                _visibleResourceTypes.UnionWith(_allResourceTypes);
            }
            else if (value is false)
            {
                _visibleResourceTypes.Clear();
            }
        }
    }
}

```

--------------------------------

### Hide Splitter Bar Handle with FluentSwitch

Source: https://www.fluentui-blazor.net/Splitter

Demonstrates hiding the FluentSplitter bar handle by setting the 'BarHandle' property to false, controlled by a FluentSwitch. This example uses Razor syntax and requires the Fluent UI Blazor library. It includes event handling for panel resizing.

```razor
<FluentSwitch CheckedMessage="Bar handle shown" UncheckedMessage="Bar handle hidden" @bind-Value="_barhandle" />
<br />
<br />

<FluentSplitter OnResized=OnResizedHandler BarSize="6" BarHandle="_barhandle" Panel1MinSize="15%" Panel2MinSize="50px">
    <Panel1>
        <div class="demopanel">
            <p>
                Panel 1 -  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                Eget dolor morbi non arcu risus quis varius. Turpis tincidunt id aliquet risus feugiat in ante. Eros donec ac odio tempor orci 
                dapibus ultrices in iaculis. Sit amet justo donec enim diam vulputate ut. Morbi blandit cursus risus at ultrices mi tempus. Sed 
                ullamcorper morbi tincidunt ornare massa eget egestas. Mi eget mauris pharetra et ultrices neque. Sit amet porttitor eget dolor 
                morbi non arcu risus quis. Tempus egestas sed sed risus pretium quam vulputate dignissim. Diam vel quam elementum pulvinar. Enim 
                nulla aliquet porttitor lacus luctus accumsan. Convallis tellus id interdum velit laoreet id donec ultrices. Dui faucibus in ornare 
                quam viverra orci sagittis.
            </p>
        </div>
    </Panel1>
    <Panel2>
        <div class="demopanel">
            <p>
                Panel 2 - Neque laoreet suspendisse interdum consectetur libero id faucibus nisl tincidunt. Suspendisse faucibus interdum posuere lorem ipsum 
                dolor sit amet. Imperdiet sed euismod nisi porta lorem mollis aliquam. Malesuada proin libero nunc consequat interdum. Amet nisl purus
                in mollis nunc sed id semper risus. Nunc sed augue lacus viverra vitae congue eu. Fermentum dui faucibus in ornare quam viverra. Ut eu 
                sem integer vitae. Interdum velit laoreet id donec ultrices tincidunt arcu non. Pellentesque dignissim enim sit amet. Scelerisque purus
                semper eget duis at.
            </p>
        </div>
    </Panel2>
</FluentSplitter>


@code
{
    bool _barhandle = false;

    private void OnResizedHandler(SplitterResizedEventArgs args)
    {
       DemoLogger.WriteLine($"Size changed: {args.Panel1Size}, {args.Panel2Size}");
    }
}
```

--------------------------------

### Styling HTML <address> Element for Contact Info

Source: https://www.fluentui-blazor.net/Reboot

Details the styling of the `<address>` element, resetting `font-style` to `normal`, inheriting `line-height`, and adding `margin-bottom`. This is used for presenting contact information, preserving formatting with `<br>` tags.

```html
**Microsoft Corporation**  
1 Microsoft Way  
Redmond, WA 98052  
P: (425) 882-8080  **Full Name**  
first.last@example.com
```

--------------------------------

### Autocomplete: Control Search Delay with ImmediateDelay in Razor

Source: https://www.fluentui-blazor.net/Autocomplete

This example demonstrates how to use the `ImmediateDelay` property of the `FluentAutocomplete` component to control the delay between user input and the initiation of search operations. Setting `ImmediateDelay` to 0 disables the delay. It takes an integer for the delay in milliseconds and outputs selected options.

```razor
@using System.Globalization
@inject DataSource Data

<FluentStack Orientation="Orientation.Vertical" VerticalGap="10">

    @* Immediate Delay *@
    <FluentNumberField @bind-Value="_immediateDelay"
                       TValue="int"
                       Label="Immediate Delay"
                       Placeholder="Delay"
                       Min="0"
                       Max="2000"
                       Step="100" />

    
    <FluentAutocomplete TOption="CultureInfo"
                        ImmediateDelay="_immediateDelay"
                        AutoComplete="off"
                        Label="Select Culture"
                        Width="250px"
                        OnOptionsSearch="OnSearch"
                        Placeholder="Select countries"
                        MaximumOptionsSearch="int.MaxValue"
                        MaximumSelectedOptions="2"
                        Virtualize="true"
                        OptionText="@(item => $"{item.DisplayName} - {item.Name}")"
                        @bind-SelectedOptions="@SelectedCultures" />

</FluentStack>




<p>
    <b>Selected</b>: @(String.Join(" - ", SelectedCultures.Select(i => i.Name)))
</p>

@code
{
    private int _immediateDelay;

    IEnumerable<CultureInfo> SelectedCultures = Array.Empty<CultureInfo>();
    CultureInfo[] Cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

    private void OnSearch(OptionsSearchEventArgs<CultureInfo> e)
    {
        e.Items = Cultures.Where(culture =>
            culture.Name.Contains(e.Text, StringComparison.OrdinalIgnoreCase) ||
            culture.DisplayName.Contains(e.Text, StringComparison.OrdinalIgnoreCase));
    }
}

```

--------------------------------

### Manual Listbox Configuration in Razor

Source: https://www.fluentui-blazor.net/Listbox

This example demonstrates manually configuring a Fluent Blazor Listbox with various options, including disabled items, items with icons, and pre-selected items. It shows how to bind a string value to the selected item. Note that custom types require overriding `ToString()` or using `OptionText`/`OptionValue`.

```razor
<FluentListbox TOption="string" ValueChanged="@(e => listboxValue = e)">
    <FluentOption Title="With a tooltip">This option has no value</FluentOption>
    <FluentOption Value="Item 1" Disabled="true">This option is disabled</FluentOption>
    <FluentOption Value="Item 2">This option has a value</FluentOption>
    <FluentOption Value="Item 3">
        <FluentIcon Value="@(new Icons.Regular.Size16.Folder())" Slot="start" />
        This option has <b>an icon</b>
    </FluentOption>
    <FluentOption Value="Item 4" Selected=true>
        <div style="display:flex; flex-direction:row">
            <img style="width:20px" src="_content/FluentUI.Demo.Shared/flags/nl.svg" />This option is selected by default
        </div>
    </FluentOption>
</FluentListbox>

<p>Selected: @listboxValue</p>

@code {
    string? listboxValue = "Item 4";
}
```

--------------------------------

### Fluent Blazor Calendar: Date Range Selection

Source: https://www.fluentui-blazor.net/DateTime

This snippet illustrates date range selection using the FluentCalendar component. It utilizes `@bind-SelectedDates` to manage a collection of selected dates, allowing users to pick a start and end date for a range.

```razor
@using Microsoft.FluentUI.AspNetCore.Components.Extensions

<FluentGrid>
    <FluentGridItem>
        <FluentLabel>Range</FluentLabel>
        <FluentCalendar DisabledDateFunc="@DisabledDay"
                        @bind-SelectedDates="@SelectedRange"
                        SelectMode="CalendarSelectMode.Range" />
        <p>Selected days: @DisplaySelectedDays(SelectedRange)</p>
    </FluentGridItem>
</FluentGrid>

@code
{
    private IEnumerable<DateTime> SelectedRange = new List<DateTime>();

    // Disable all days with the day number 3, 8, and 20.
    private bool DisabledDay(DateTime date) => date.Day == 3 || date.Day == 8 || date.Day == 20;

    // Display the selected days in a list.
    private RenderFragment DisplaySelectedDays(IEnumerable<DateTime> days) => builder =>
    {
        builder.OpenElement(0, "ul");
        foreach (var day in days)
        {
            builder.OpenElement(1, "li");
            builder.AddContent(2, day.ToShortDateString());
            builder.CloseElement(); // li
        }
        builder.CloseElement(); // ul
    };
}
```

--------------------------------

### FluentDataGrid with TemplateColumn using Razor

Source: https://www.fluentui-blazor.net/datagrid-template-columns-2

This snippet demonstrates a Fluent UI Blazor DataGrid configured with TemplateColumn for custom cell content. It uses FluentTextField and FluentNumberField within TemplateColumns and includes event handlers for row and cell interactions. The necessary data model and sample data are also defined.

```razor
<FluentDataGrid id="defaultGrid"
                Items=RowsGrid.AsQueryable()
                GridTemplateColumns="1fr 1fr"
                TGridItem=SampleGridData
                OnRowClick="HandleRowClick"
                OnRowFocus="HandleRowFocus"
                OnCellClick="HandleCellClick"
                OnCellFocus="HandleCellFocus"
                RowSize="@DataGridRowSize.Medium">
    <TemplateColumn Title="Name">
        <FluentTextField @bind-Value="@context!.Name" />
    </TemplateColumn>
    <TemplateColumn Title="Age">
        <FluentNumberField @bind-Value="@context!.Age" TValue="int" />
    </TemplateColumn>
</FluentDataGrid>


@code {
    public class SampleGridData
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public SampleGridData(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }



    List<SampleGridData> RowsGrid = new()
    {
        new SampleGridData("Rob", 19 ),
        new SampleGridData("Bob", 20 )
    };

    private void HandleRowClick(FluentDataGridRow<SampleGridData> row)
    {
        DemoLogger.WriteLine($"Row clicked: {row.RowIndex}");
    }

    private void HandleRowFocus(FluentDataGridRow<SampleGridData> row)
    {
        DemoLogger.WriteLine($"Row focused: {row.RowIndex}");
    }

    private void HandleCellClick(FluentDataGridCell<SampleGridData> cell)
    {
        DemoLogger.WriteLine($"Cell clicked: {cell.GridColumn}");
    }

    private void HandleCellFocus(FluentDataGridCell<SampleGridData> cell)
    {
        DemoLogger.WriteLine($"Cell focused : {cell.GridColumn}");
    }
}
```

--------------------------------

### Blazor FluentWizard Implementation

Source: https://www.fluentui-blazor.net/Wizard

This Blazor Razor component demonstrates a fully customizable FluentWizard. It includes styling for wizard steps, custom step templates, dynamic button rendering for navigation (previous, next, first, last, finish), and event handling for step changes and wizard completion. It utilizes the IDialogService for displaying a confirmation message upon finishing.

```Razor
@inject IDialogService DialogService

<style>
    #customized-wizard li[status="current"] > div {
        font-weight: bold;
    }

    #customized-wizard li[disabled] > div {
        color: var(--neutral-stroke-strong-rest);
        opacity: var(--disabled-opacity);
    }
</style>

<FluentWizard @ref="@MyWizard"
              Id="customized-wizard"
              @bind-Value="@Value"
              StepTitleHiddenWhen="@GridItemHidden.XsAndDown"
              OnFinish="OnFinish"
              Height="300px">
    <Steps>
        <FluentWizardStep OnChange="@OnStepChange">
            <StepTemplate>
                <div active="@context.Active">
                    Intro
                </div>
            </StepTemplate>
            <ChildContent>
                <FluentLabel Typo="Typography.Header">Introduction</FluentLabel>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ut nisi eget dolor semper
                luctus vitae a nulla. Cras semper eros sed lacinia tincidunt. Mauris dignissim ullamcorper dolor,
                ut blandit dui ullamcorper faucibus. Interdum et malesuada fames ac ante ipsum.
            </ChildContent>
        </FluentWizardStep>
        <FluentWizardStep OnChange="@OnStepChange">
            <StepTemplate>
                <div active="@context.Active">
                    Get Started
                </div>
            </StepTemplate><ChildContent>
                <FluentLabel Typo="Typography.Header">Get Started</FluentLabel>
                Maecenas sed justo ac sapien venenatis ullamcorper. Sed maximus nunc non venenatis euismod.
                Fusce vel porta ex, imperdiet molestie nisl. Vestibulum eu ultricies mauris, eget aliquam quam.
            </ChildContent>
        </FluentWizardStep>
        <FluentWizardStep OnChange="@OnStepChange">
            <StepTemplate>
                <div active="@context.Active">
                    Set budget
                </div>
            </StepTemplate><ChildContent>
                <FluentLabel Typo="Typography.Header">Set budget</FluentLabel>
                Phasellus quis augue convallis, congue velit ac, aliquam ex. In egestas porttitor massa
                aliquet porttitor. Donec bibendum faucibus urna vitae elementum. Phasellus vitae efficitur
                turpis, eget molestie ipsum.
            </ChildContent>
        </FluentWizardStep>
        <FluentWizardStep OnChange="@OnStepChange">
            <StepTemplate>
                <div active="@context.Active">
                    Summary
                </div>
            </StepTemplate><ChildContent>
                <FluentLabel Typo="Typography.Header">Summary</FluentLabel>
                Ut iaculis sed magna efficitur tempor. Vestibulum est erat, imperdiet in diam ac,
                aliquam tempus sapien. Nam rutrum mi at enim mattis, non mollis diam molestie.
                Cras sodales dui libero, sit amet cursus sapien elementum ac. Nulla euismod nisi sem.
            </ChildContent>
        </FluentWizardStep>
    </Steps>

    <ButtonTemplate>
        @{
            var index = context;
            var lastStepIndex = 3;

            <div>
                @if (index > 0)
                {
                    <FluentButton OnClick="@(() => MyWizard.GoToStepAsync(0))">Go to first page</FluentButton>
                    <FluentButton OnClick="@(() => MyWizard.GoToStepAsync(Value - 1))">Previous</FluentButton>
                }
            </div>
            <FluentSpacer />
            <div>
                @if (index != lastStepIndex)
                {
                    <FluentButton OnClick="@(() => MyWizard.GoToStepAsync(Value + 1))" Appearance="Appearance.Accent">Next</FluentButton>
                    <FluentButton OnClick="@(() => MyWizard.GoToStepAsync(lastStepIndex))" Appearance="Appearance.Accent">Go to last page</FluentButton>
                }
                else
                {
                    <FluentButton OnClick="@(() => MyWizard.FinishAsync())" Appearance="Appearance.Accent">Finish</FluentButton>
                }
            </div>
        }
    </ButtonTemplate>
</FluentWizard>

@code
{
    FluentWizard MyWizard = default!;
    int Value = 0;

    void OnStepChange(FluentWizardStepChangeEventArgs e)
    {
        DemoLogger.WriteLine($"Go to step {e.TargetLabel} (#{e.TargetIndex})");
    }

    async Task OnFinish()
    {
        DemoLogger.WriteLine($"Customized wizard finish clicked.");
        await DialogService.ShowInfoAsync("The wizard has finished.", "Finished Clicked");
    }
}

```

--------------------------------

### Update Blazor Imports for Icons and Emojis

Source: https://www.fluentui-blazor.net/UpgradeGuide

Modifies the _Imports.razor file to correctly reference Icons and Emojis from Microsoft.FluentUI.AspNetCore.Components. This is necessary for versions 4.11.0 and later due to changes in how icon variants and emoji categories are packaged. The provided code adds using statements with aliases for easier access.

```razor
@using Icons = Microsoft.FluentUI.AspNetCore.Components.Icons
@* add line below only if you are using the Emoji package *@
@using Emojis = Microsoft.FluentUI.AspNetCore.Components.Emojis
```

--------------------------------

### Multi-select Tree View with Checkboxes (Razor)

Source: https://www.fluentui-blazor.net/TreeView

This example demonstrates how to implement multi-select functionality in a Fluent Tree View using checkboxes. The `CheckboxHandler` method manages adding or removing items from the `SelectedItems` list, and custom CSS is used to hide the default selection indicator.

```Razor
<FluentTreeView Items="@Items" LazyLoadItems="true" Class="no-selected-indicator">
    <ItemTemplate>
        <FluentCheckbox Value="@(SelectedItems.Contains(context))"
                        ValueChanged="@(e => CheckboxHandler(e, context))"
                        Style="margin-right: 12px;">
            @context.Text
        </FluentCheckbox>
    </ItemTemplate>
</FluentTreeView>

<div>
    <b>Selected items:</b> @(string.Join("; ", SelectedItems.Select(i => i.Text)))
</div>

<style>
    .no-selected-indicator fluent-tree-item[selected]::part(positioning-region) {
        background-color: var(--neutral-fill-stealth-rest);
    }

    .no-selected-indicator fluent-tree-item::part(positioning-region):hover {
        background-color: var(--neutral-fill-stealth-rest);
    }

    .no-selected-indicator fluent-tree-item[selected]::after {
        display: none;
    }
</style>

@code
{
    private int Count = -1;
    private IEnumerable<ITreeViewItem> Items = new List<ITreeViewItem>();
    private List<ITreeViewItem> SelectedItems = new List<ITreeViewItem>();

    protected override void OnInitialized()
    {
        Items = CreateTree(maxLevel: 3, nbItemsPerLevel: 5).Items ?? [];
    }

    // Add or remove item from the selected items list
    private void CheckboxHandler(bool selected, ITreeViewItem item)
    {
        if (selected && !SelectedItems.Contains(item))
        {
            SelectedItems.Add(item);
        }
        else if (!selected && SelectedItems.Contains(item))
        {
            SelectedItems.Remove(item);
        }
    }

    // Recursive method to create tree
    private TreeViewItem CreateTree(int maxLevel, int nbItemsPerLevel, int level = 0)
    {
        Count++;

        var treeItem = new TreeViewItem
            {
                Text = $"Item {Count}",
                Items = level == maxLevel
                              ? null
                              : new List<TreeViewItem>(Enumerable.Range(1, nbItemsPerLevel)
                                                                 .Select(i => CreateTree(maxLevel, nbItemsPerLevel, level + 1))),
            };

        return treeItem;
    }
}

```

--------------------------------

### Open Right and Left Panels with Callbacks (Razor)

Source: https://www.fluentui-blazor.net/Panel

This Razor component demonstrates how to open panels from the right and left sides of the screen using the `IDialogService`. It utilizes `FluentButton` to trigger the panel opening and defines `OpenPanelRight` and `OpenPanelLeft` methods to configure and show the panels with `DialogParameters` and `OnDialogResult` callbacks.

```Razor
@inject IDialogService DialogService

<FluentButton @onclick="@OpenPanelRight" Appearance="Appearance.Accent">
    Open panel (>>)
</FluentButton>

<FluentButton @onclick="@OpenPanelLeft" Appearance="Appearance.Accent">
    Open panel (<<)
</FluentButton>
```

--------------------------------

### FluentPersona with Default and Specific Initials (Razor)

Source: https://www.fluentui-blazor.net/Persona

Shows two ways to display Persona without an image: one where initials are automatically derived from the name and another where specific initials are provided using the `Initials` property. This example uses `FluentStack` to arrange the Personas vertically.

```Razor
<FluentStack>
    <FluentPersona Name="Lydia Bauer"
                   ImageSize="50px"
                   Status="PresenceStatus.Busy"
                   StatusSize="PresenceBadgeSize.Small">
    </FluentPersona>

    <FluentPersona Initials="LB"
                   ImageSize="50px">
    </FluentPersona>
</FluentStack>
```

```Razor
<FluentPersona Name="Lydia Bauer"
               ImageSize="50px"
               Initials="LY">
</FluentPersona>
```

--------------------------------

### Fluent UI Blazor TimePicker with Second Precision

Source: https://www.fluentui-blazor.net/DateTime

This example demonstrates the FluentTimePicker component for selecting time. It shows a basic time picker and another configured to display and allow selection of hours, minutes, and seconds using the 'TimeDisplay.HourMinuteSeconds' option. The selected time is bound to a DateTime variable.

```Razor
<FluentTimePicker @bind-Value="@SelectedValue" Label="Meeting time:" />
<FluentTimePicker @bind-Value="@SelectedValue" Label="With seconds:" TimeDisplay="@TimeDisplay.HourMinuteSeconds" />

<p>Selected Time: @(SelectedValue?.ToString("HH:mm:ss"))</p>

@code
{
    private DateTime? SelectedValue = null;
}
```

--------------------------------

### FluentAnchoredRegion Component API

Source: https://www.fluentui-blazor.net/AnchoredRegion

Detailed documentation for the FluentAnchoredRegion component, including its parameters and available methods for managing anchored regions.

```APIDOC
## FluentAnchoredRegion Component API

### Description

The FluentAnchoredRegion component provides advanced capabilities for positioning and anchoring UI elements relative to other elements on the page. It offers granular control over placement, sizing, and viewport behavior.

### Parameters

#### Path Parameters

*None*

#### Query Parameters

*None*

#### Request Body

*This component does not have a request body as it is a client-side component.*

**Component Parameters:**

- **Anchor** (string?) - Gets or sets the HTML ID of the anchor element this region is positioned relative to. This must be set for the component positioning logic to be active.
- **AutoFocus** (bool) - Gets or sets whether the element should receive the focus when the component is loaded. Default: `False`
- **AutoUpdateMode** (AutoUpdateMode?) - Defines what triggers the anchored region to re-evaluate positioning. Default: `Auto`. In 'anchor' mode only anchor resizes and attribute changes will provoke an update. In 'auto' mode the component also updates because of - any scroll event on the document, window resizes and viewport resizes. See `FluentAnchoredRegion.AutoUpdateMode`.
- **ChildContent** (RenderFragment?) - Gets or sets the content to be rendered inside the component.
- **FixedPlacement** (bool?) - Gets or sets a value indicating whether the region is positioned using css 'position: fixed'. Otherwise the region uses 'position: absolute'. Fixed placement allows the region to break out of parent containers.
- **HorizontalDefaultPosition** (HorizontalPosition?) - Gets or sets the default horizontal position of the region relative to the anchor element. Default: `Unset`. See `HorizontalPosition`.
- **HorizontalInset** (bool) - Gets or sets a value indicating whether the region overlaps the anchor on the horizontal axis. Default: `False` which places the region adjacent to the anchor element.
- **HorizontalPositioningMode** (AxisPositioningMode?) - Sets what logic the component uses to determine horizontal placement. `Locktodefault` forces the default position. `Dynamic` decides placement based on available space. `Uncontrolled` (default) does not control placement on the horizontal axis. See `AxisPositioningMode`.
- **HorizontalScaling** (AxisScalingMode?) - Defines how the width of the region is calculated. Default: `'Content'`. See `AxisScalingMode`.
- **HorizontalThreshold** (int) - How narrow the space allocated to the default position has to be before the widest area is selected for layout. Default: `0`.
- **HorizontalViewportLock** (bool) - Gets or sets a value indicating whether the region remains in the viewport (ie. detaches from the anchor) on the horizontal axis. Default: `False`.
- **Shadow** (ElevationShadow) - Default: `None`.
- **VerticalDefaultPosition** (VerticalPosition?) - Gets or sets the default vertical position of the region relative to the anchor element. Default: `'Unset'`. See `VerticalPosition`.
- **VerticalInset** (bool) - Gets or sets a value indicating whether the region overlaps the anchor on the vertical axis. Default: `False`.
- **VerticalPositioningMode** (AxisPositioningMode?) - Sets what logic the component uses to determine vertical placement. `Locktodefault` forces the default position. `Dynamic` decides placement based on available space. `Uncontrolled` (default) does not control placement on the vertical axis. See `AxisPositioningMode`.
- **VerticalScaling** (AxisScalingMode?) - Defines how the height of the region is calculated. Default: `'Content'`. See `AxisScalingMode`.
- **VerticalThreshold** (int) - How short the space allocated to the default position has to be before the tallest area is selected for layout. Default: `0`.
- **VerticalViewportLock** (bool?) - Gets or sets a value indicating whether the region remains in the viewport (ie. detaches from the anchor) on the vertical axis.
- **Viewport** (string?) - Gets or sets the HTML ID of the viewport element this region is positioned relative to. If unset the parent element of the anchored region is used.

### Methods

- **FocusToNextElementAsync**() - Moves the focus to the next element included in this anchor region only.
- **FocusToOriginalElementAsync**() - Moves the focus to the initial element that triggered the anchor region.

### Enums

**AutoUpdateMode**:
- `Anchor`
- `Auto`

**HorizontalPosition**:
- `Unset`
- `Start`
- `End`
- `Center`

**AxisPositioningMode**:
- `Uncontrolled`
- `Locktodefault`
- `Dynamic`

**AxisScalingMode**:
- `Content`
- `Fill`
- `Anchor`

**ElevationShadow**:
- `None`
- `Tooltip`
- `Flyout`
- `Dialog`

**VerticalPosition**:
- `Unset`
- `Top`
- `Bottom`
- `Center`

### Request Example

*N/A - This is a component, not an API endpoint.*

### Response

#### Success Response (200)

*N/A - This is a component, not an API endpoint.*

#### Response Example

*N/A - This is a component, not an API endpoint.*
```

--------------------------------

### Fluent Blazor RadioGroup in Toolbar

Source: https://www.fluentui-blazor.net/RadioGroup

Illustrates how to embed a FluentRadioGroup within a FluentToolbar to manage navigation options. The example shows a required radio group with radio buttons for 'back', 'forward', and 'refresh', and binds the selected value. Requires Fluent UI Blazor components.

```Razor
<FluentToolbar id="toolbar-fluent-components">
    <FluentButton Appearance="Appearance.Accent">Go</FluentButton>
    <FluentRadioGroup Required="true" Name="navigation" @bind-Value=value>
        <FluentRadio Value=@("back")>back</FluentRadio>
        <FluentRadio Value=@("forward")>forward</FluentRadio>
        <FluentRadio Value=@("refresh")>refresh</FluentRadio>
    </FluentRadioGroup>
    <FluentButton Appearance="Appearance.Accent">Stop</FluentButton>
</FluentToolbar>

@code
{
    string? value;
}
```

--------------------------------

### Manual File Upload with FluentInputFile in Razor

Source: https://www.fluentui-blazor.net/InputFile

This Razor component demonstrates a manual file upload using FluentInputFile. It configures the input for saving to a temporary folder, allows multiple files, sets a maximum file size, and specifies accepted file types. It also includes progress tracking and displays uploaded file information upon completion. Dependencies include Fluent UI Blazor components.

```Razor
<FluentInputFile @ref="@myFileUploader" 
                 DragDropZoneVisible="false"
                 Mode="InputFileMode.SaveToTemporaryFolder"
                 Multiple="true"
                 AnchorId="MyUploadButton"
                 MaximumFileSize="@(100 * 1024 * 1024)"
                 Accept=".mp4, .mov, .avi"
                 OnProgressChange="@(e =>
                     {
                         progressPercent = e.ProgressPercent; 
                         progressTitle = e.ProgressTitle;
                     })"
                 OnCompleted="@OnCompleted" />

<FluentProgress Min="0" Max="100" Visible="@(progressPercent > 0)" Value="@progressPercent" />
<FluentLabel Alignment="HorizontalAlignment.Center">
    @progressTitle
</FluentLabel>

<FluentButton Id="MyUploadButton" Appearance="Appearance.Accent">
    Upload files
</FluentButton>

@if (Files.Any())
{
    <h4>File(s) uploaded:</h4>
    <ul>
        @foreach (var file in Files)
        {
            <li>
                <b>@file.Name</b> 🔹
                @($"{{Decimal.Divide(file.Size, 1024):N}} KB") 🔹
                @file.ContentType 🔹
                @file.LocalFile?.FullName
                @file.ErrorMessage
            </li>
        }
    </ul>
}

@code
{
    FluentInputFile? myFileUploader = default!;
    int? progressPercent;
    string? progressTitle;

    FluentInputFileEventArgs[] Files = Array.Empty<FluentInputFileEventArgs>();

    void OnCompleted(IEnumerable<FluentInputFileEventArgs> files)
    {
        Files = files.ToArray();
        progressPercent = myFileUploader!.ProgressPercent;
        progressTitle = myFileUploader!.ProgressTitle;

        // For the demo, delete these files.
        foreach (var file in Files)
        {
            file.LocalFile?.Delete();
        }
    }
}
```

--------------------------------

### ShowSuccess / ShowSuccessAsync

Source: https://www.fluentui-blazor.net/DialogService

Displays a success message box with a title and a primary action. ShowSuccessAsync returns a dialog reference.

```APIDOC
## POST /messages/success

### Description
Shows a success message box. The `ShowSuccess` method returns `void`, while `ShowSuccessAsync` returns a `Task<IDialogReference>`.

### Method
POST

### Endpoint
/messages/success

### Parameters
#### Query Parameters
- **message** (string) - Required - The message content to display.
- **title** (string) - Optional - The title of the message box.
- **primaryAction** (string) - Optional - The text for the primary action button.

### Request Body
(Not applicable for this endpoint, parameters are passed via query string or method arguments)

### Response
#### Success Response (200)
- **IDialogReference** (object) - (Only for `ShowSuccessAsync`) A reference to the displayed message box.
- **void** - (For `ShowSuccess`) No return value.

#### Response Example
```json
{
  "dialogReference": {
    "id": "success-message-id"
  }
}
```
```

--------------------------------

### Fluent Blazor Menu: Menu with Checkboxes and Radio Buttons

Source: https://www.fluentui-blazor.net/Menu

This example showcases how to implement checkboxes and radio buttons within a Fluent Blazor Menu. It uses `MenuItemRole.MenuItemCheckbox` and `MenuItemRole.MenuItemRadio` for the respective item types. Icons can also be added to checkbox items using the `slot="checkbox-indicator"`.

```Razor
<FluentMenu Open="true" Anchored="false">
    <FluentMenuItem>Menu item 1</FluentMenuItem>
    <FluentMenuItem>Menu item 2</FluentMenuItem>
    <FluentMenuItem>Menu item 3</FluentMenuItem>
    <FluentDivider></FluentDivider>
    <FluentMenuItem Role="MenuItemRole.MenuItemCheckbox">Checkbox 1</FluentMenuItem>
    <FluentMenuItem Role="MenuItemRole.MenuItemCheckbox">
        Checkbox 2
        <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" Slot="checkbox-indicator" />
    </FluentMenuItem>
    <FluentDivider></FluentDivider>
    <FluentMenuItem Role="MenuItemRole.MenuItemRadio">Radio 1.1</FluentMenuItem>
    <FluentMenuItem Role="MenuItemRole.MenuItemRadio">Radio 1.2</FluentMenuItem>
</FluentMenu>
```

--------------------------------

### Adjusting Icon Margin for Hypertext Appearance (CSS)

Source: https://www.fluentui-blazor.net/Anchor

This CSS snippet customizes the margin between an icon and a link when the FluentAnchor component has a 'hypertext' appearance. It targets the ::part(start) and ::part(end) pseudo-elements to reduce the default margin. This should only be applied when an icon is present to avoid unintended margins.

```css
/* for icon at the start */
fluent-anchor[appearance="hypertext"]::part(start) {
    margin-inline-end: calc(var(--design-unit) * 1px);
}
/* for icon at the end */
fluent-anchor[appearance="hypertext"]::part(end) {
margin-inline-start: calc(var(--design-unit) * 1px);
}
```

--------------------------------

### Fluent Blazor Calendar: Multiple Day Selection with Hover Effect

Source: https://www.fluentui-blazor.net/DateTime

This snippet demonstrates multiple day selection with a hover effect that selects a contiguous block of 3 days starting from the hovered date. It uses the `SelectDatesHover` parameter with a custom `Select3Days` function.

```razor
@using Microsoft.FluentUI.AspNetCore.Components.Extensions

<FluentGrid>
    <FluentGridItem>
        <FluentLabel>Multiple with "Select3Days"</FluentLabel>
        <FluentCalendar DisabledDateFunc="@DisabledDay"
                        @bind-SelectedDates="@SelectedDaysCustomized"
                        SelectDatesHover="@Select3Days"
                        SelectMode="CalendarSelectMode.Multiple" />
        <p>Selected days: @DisplaySelectedDays(SelectedDaysCustomized)</p>
    </FluentGridItem>
</FluentGrid>

@code
{
    private IEnumerable<DateTime> SelectedDaysCustomized = new List<DateTime>();

    // Disable all days with the day number 3, 8, and 20.
    private bool DisabledDay(DateTime date) => date.Day == 3 || date.Day == 8 || date.Day == 20;

    // Always select 3 days, from the "clicked" day.
    private IEnumerable<DateTime> Select3Days(DateTime date)
    {
        return Enumerable.Range(0, 3).Select(i => date.AddDays(i));
    }

    // Display the selected days in a list.
    private RenderFragment DisplaySelectedDays(IEnumerable<DateTime> days) => builder =>
    {
        builder.OpenElement(0, "ul");
        foreach (var day in days)
        {
            builder.OpenElement(1, "li");
            builder.AddContent(2, day.ToShortDateString());
            builder.CloseElement(); // li
        }
        builder.CloseElement(); // ul
    };
}
```

--------------------------------

### Configure Fluent UI Blazor Icon Asset Publishing (.csproj)

Source: https://www.fluentui-blazor.net/WhatsNew-Archive

This configuration snippet for the .csproj file enables and specifies which Fluent UI icon assets should be published. It allows control over icon sizes and variants. By default, no icon assets are published unless explicitly enabled.

```xml
<PropertyGroup>
    <PublishFluentIconAssets>true</PublishFluentIconAssets>

    <FluentIconSizes>10,12,16,20,24,28,32,48</FluentIconSizes>

    <FluentIconVariants>Filled,Regular</FluentIconVariants>
</PropertyGroup>
```

--------------------------------

### FluentTreeView with Data Binding (Razor)

Source: https://www.fluentui-blazor.net/TreeView

This example demonstrates data binding with the FluentTreeView component. It uses Blazor's two-way binding (`@bind-Expanded` and `@bind-Selected`) to control the expanded and selected states of tree items, linking them to boolean variables. Checkboxes are used to visually represent and control these states.

```Razor
<FluentStack Orientation="@Orientation.Horizontal">
    <FluentTreeView>
        <FluentTreeItem @bind-Expanded=@FlowersExpanded>
            Flowers
            <FluentTreeItem @bind-Selected=DaisySelected>Daisy</FluentTreeItem>
            <FluentTreeItem @bind-Selected=SunflowerSelected>Sunflower</FluentTreeItem>
            <FluentTreeItem @bind-Selected=RoseSelected>Rose</FluentTreeItem>
        </FluentTreeItem>
        <FluentTreeItem @bind-Expanded=@PlanesExpanded>
            Planes
            <FluentTreeItem>Tomcat</FluentTreeItem>
            <FluentTreeItem>Hawker Harrier</FluentTreeItem>
            <FluentTreeItem>Cesna</FluentTreeItem>
        </FluentTreeItem>
    </FluentTreeView>
    <FluentStack Orientation="@Orientation.Vertical">
        <h2>Expanded</h2>
        <FluentCheckbox @bind-Value=FlowersExpanded>
            <span aria-label="Flowers expanded">Flowers expanded</span>
        </FluentCheckbox>
        <FluentCheckbox @bind-Value=PlanesExpanded>
            <span aria-label="Planes expanded">Planes expanded</span>
        </FluentCheckbox>
    </FluentStack>
    <FluentStack Orientation="@Orientation.Vertical">
        <h2>Selected</h2>
        <FluentCheckbox @bind-Value=DaisySelected Disabled="true">
            <span aria-label="Daisy selected">Daisy selected</span>
        </FluentCheckbox>
        <FluentCheckbox @bind-Value=SunflowerSelected Disabled="true">
            <span aria-label="Sunflower selected">Sunflower selected</span>
        </FluentCheckbox>
        <FluentCheckbox @bind-Value=RoseSelected Disabled="true">
            <span aria-label="Rose selected">Rose selected</span>
        </FluentCheckbox>
    </FluentStack>
</FluentStack>

@code
{
    bool FlowersExpanded = true;
    bool PlanesExpanded = true;

    bool DaisySelected = false;
    bool SunflowerSelected = false;
    bool RoseSelected = false;
}
```

--------------------------------

### Customized Autocomplete with Multiple Selections and Templates (Razor)

Source: https://www.fluentui-blazor.net/Autocomplete

This example demonstrates a highly customized Fluent Autocomplete component in Razor. It supports multiple selections (up to 3), custom templates for labels, selected options (using FluentPersona), and individual options. It also includes a progress indicator that can be toggled and custom header/footer content for the dropdown, including a 'no results found' message.

```Razor
@inject DataSource Data

<FluentAutocomplete Id="my-customized"
                    @ref="ContactList"
                    TOption="Person"
                    Width="100%"
                    Placeholder="search"
                    OnOptionsSearch="@OnSearchAsync"
                    ShowProgressIndicator="@ShowProgressIndicator"
                    MaximumSelectedOptions="3"
                    KeepOpen="true"
                    OptionText="@(item => item.FirstName)"
                    OptionStyle="min-height: 40px;"
                    @bind-SelectedOptions="@SelectedItems">

    <LabelTemplate>
        Select a person
        <FluentIcon Value="@(new Icons.Regular.Size20.Person())" Style="margin: 0 4px;" />
    </LabelTemplate>

    @* Template used with each Selected items *@
    <SelectedOptionTemplate>
        <FluentPersona ImageSize="24px"
                            Image="@context.Picture"
                            Name="@($"{context.FirstName} {context.LastName}")"
                            Style="height: 26px; background: var(--neutral-fill-secondary-hover)"
                            DismissTitle="Remove"
                            Status="PresenceStatus.Available"
                            OnDismissClick="@(async () => await ContactList.RemoveSelectedItemAsync(context))" />
    </SelectedOptionTemplate>

    @* Template used with each Option items *@
    <OptionTemplate>
        <FluentPersona ImageSize="32px"
                            Image="@context.Picture"
                            Status="PresenceStatus.Available"
                            StatusSize="PresenceBadgeSize.Small"
                            Name="@($"{context.FirstName} {context.LastName}")" />
    </OptionTemplate>

    @* Template used when the maximum number of selected items (MaximumSelectedOptions) has been reached *@
    <MaximumSelectedOptionsMessage>
        The maximum number of selected items has been reached.
    </MaximumSelectedOptionsMessage>

    @* Content display at the top of the Popup area *@
    <HeaderContent>
        <FluentLabel Color="Color.Accent"
                     Style="padding: 8px; font-size: 11px; border-bottom: 1px solid var(--neutral-fill-stealth-hover);">
            Suggested contacts
        </FluentLabel>
        <FluentProgress Style="@($"visibility: {(context.InProgress ? "visible" : "collapse")}")" />
    </HeaderContent>

    @* Content display at the bottom of the Popup area *@
    <FooterContent>
        @if (!context.Items.Any())
        {
            <FluentLabel Style="font-size: 11px; text-align: center; width: 200px;">
                No results found
            </FluentLabel>
        }
    </FooterContent>
</FluentAutocomplete>

<p>
    <b>Selected</b>: @(String.Join(" - ", SelectedItems.Select(i => i.LastName)))
</p>

<p>
    <FluentSwitch @bind-Value="@ShowProgressIndicator" Label="ShowProgressIndicator" />
</p>

@code
{
    bool ShowProgressIndicator = false;
    FluentAutocomplete<Person> ContactList = default!;
    IEnumerable<Person> SelectedItems = Array.Empty<Person>();

    private async Task OnSearchAsync(OptionsSearchEventArgs<Person> e)
    {
        if (ShowProgressIndicator)
        {
            await Task.Delay(500); // Simulate a delay for the search operation
        }

        e.Items = Data.People.Where(i => i.LastName.StartsWith(e.Text, StringComparison.OrdinalIgnoreCase) ||
                                         i.FirstName.StartsWith(e.Text, StringComparison.OrdinalIgnoreCase))
                             .OrderBy(i => i.LastName);
    }
}

```

--------------------------------

### Migrate FluentIcon Property using Regex

Source: https://www.fluentui-blazor.net/IconsAndEmoji

Provides a regular expression and replacement pattern for Visual Studio's Find and Replace functionality to migrate from the old `Icon` property to the `Value` property for `FluentIcon` components.

```regex
To search: <FluentIcon Icon="(?<name>[^"]+)"?
To replace by: <FluentIcon Value="@(new ${name}())"
```

--------------------------------

### FluentTabs: Vertical Layout with No Active Indicator (Razor)

Source: https://www.fluentui-blazor.net/Tabs

Illustrates the creation of a vertical FluentTabs component in Razor, also disabling the active indicator and setting a fixed height. This example includes a disabled tab to show its behavior. The `Orientation` attribute is set to `Orientation.Vertical`.

```Razor
<h4>No active indicator - Vertical</h4>
<FluentTabs ShowActiveIndicator=false ActiveTabId="tab3" Orientation="Orientation.Vertical" Style="height: 250px;">
    <FluentTab Id="TabOne" Label="Tab one">
        Tab one content. This is for testing.
    </FluentTab>
    <FluentTab Id="TabTwo" Label="Tab two">
        Tab two content. This is for testing.
    </FluentTab>
    <FluentTab Id="TabThree" Label="Tab three">
        Tab three content. This is for testing.
    </FluentTab>
    <FluentTab id="tab4" Disabled=true Label="Tab four">
        Tab four content. This is for testing.
    </FluentTab>
</FluentTabs>
```

--------------------------------

### Fluent Blazor Menu: Default Menu with Switch Control

Source: https://www.fluentui-blazor.net/Menu

This example displays a default Fluent Blazor Menu controlled by a `FluentSwitch`. The `FluentSwitch`'s value is two-way bound to the `open` boolean property, which in turn controls the `FluentMenu`'s visibility via `@bind-Open`. The `OnMenuChange` event handler updates the status text with the selected item.

```Razor
<FluentSwitch @bind-Value="@open">Show</FluentSwitch>
<FluentMenu @bind-Open="@open" @onmenuchange=OnMenuChange Width="400px">
    <FluentMenuItem>Menu item 1</FluentMenuItem>
    <FluentMenuItem>Menu item 2</FluentMenuItem>
    <FluentMenuItem Disabled="true">Menu item 3 Disabled</FluentMenuItem>
    <FluentMenuItem>Menu item 4</FluentMenuItem>
</FluentMenu>
<p>@status</p>

@code{
    bool open = false;
    private string status = "";
    
    private void OnMenuChange(MenuChangeEventArgs args)
    {
        if (args is not null && args.Value is not null)
            status = $"{args.Value} selected";
    }
}
```

--------------------------------

### SplashScreenContent Class Methods

Source: https://www.fluentui-blazor.net/SplashScreen

Methods available for interacting with the SplashScreenContent.

```APIDOC
## SplashScreenContent Class Methods

### Description
Methods to dynamically update the labels displayed on the splash screen.

### Methods

- **UpdateLabels** (void) - Updates the labels of the splash screen.
  - **loadingText** (string) - The new loading text.
  - **message** (MarkupString?) - The new message.

### Request Example
```json
{
  "method": "UpdateLabels",
  "parameters": {
    "loadingText": "Updating data...",
    "message": "Please wait a moment."
  }
}
```

### Response
#### Success Response (200)
- **None** - This method performs an action and does not return data.

#### Response Example
```json
{
  "status": "Labels updated successfully."
}
```
```

--------------------------------

### Data-Bound Listbox with Custom Objects in Razor

Source: https://www.fluentui-blazor.net/Listbox

This example shows a Fluent Blazor Listbox bound to a list of custom `Person` objects. It utilizes `OptionValue` and `OptionText` to specify how to extract the value and display text from each `Person` object. The selected item and its value are bound using `@bind-Value` and `@bind-SelectedOption`.

```razor
@inject DataSource Data

<FluentListbox TOption="Person"
               Label="Select a person"
               Items="@Data.People"
               Id="people-listbox"
               Height="150px"
               OptionValue="@(p => p.PersonId.ToString())"
               OptionText="@(p => p.LastName + ", " + p.FirstName)"
               @bind-Value="@SelectedValue"
               @bind-SelectedOption="@SelectedItem" />

<p>
    Selected value: @SelectedValue <br />
    Selected item: @SelectedItem?.ToString()
</p>

@code
{
    Person? SelectedItem;
    string? SelectedValue;
}
```

--------------------------------

### FluentListbox with Custom Option Template and Event Handling

Source: https://www.fluentui-blazor.net/Listbox

This example demonstrates using a custom OptionTemplate within FluentListbox to display complex content, including icons and formatted text. It binds the selected item to a Person object and includes an OnClick event handler for an icon within the template, showcasing event propagation and logging. This requires a 'DataSource' and 'Person' class to be defined.

```Razor
@inject DataSource Data

<FluentListbox Items="@Data.People"
               OptionValue="@(i => i.PersonId.ToString())"
               @bind-SelectedOption=@Person>
    <OptionTemplate>
        <FluentIcon Icon="Icons.Regular.Size16.Person" Slot="end" OnClick="@(() => DemoLogger.WriteLine($"Icon for {@context.LastName} selected"))" />
        @context.FirstName (@context.LastName)
    </OptionTemplate>
</FluentListbox>
<p>
    Selected: @Person?.FirstName (@Person?.LastName)
</p>

@code {
    public Person Person { get; set; } = default!;
}
```

--------------------------------

### Autocomplete: Close Dropdown Programmatically with Razor

Source: https://www.fluentui-blazor.net/Autocomplete

This example demonstrates how to close the dropdown of a `FluentAutocomplete` component programmatically using its `CloseDropdownAsync` method. This is useful for scenarios where the dropdown needs to be dismissed based on external actions, such as opening a dialog. It depends on `IDialogService` for showing dialogs and `FluentAutocomplete` for dropdown control.

```razor
@inject DataSource Data


@inject IDialogService DialogService

<FluentAutocomplete @ref=_autocomplete
                    TOption="Country"
                    AutoComplete="off"
                    Label="Select a country"
                    Width="250px"
                    MaxAutoHeight="@(AutoHeight ? "200px" : null)"
                    Placeholder="Select countries"
                    OnOptionsSearch="@OnSearchAsync"
                    OptionDisabled="@(e => e.Code == "au")"
                    MaximumSelectedOptions="5"
                    OptionText="@(item => item.Name)"
                    @bind-SelectedOptions="@SelectedItems">
    <FooterContent>
        <FluentStack Orientation="Orientation.Horizontal"
                     HorizontalAlignment="HorizontalAlignment.Right"
                     Style="padding: 3px;">
            <FluentButton OnClick="OpenDialog">
                Show Dialog
            </FluentButton>
        </FluentStack>
    </FooterContent>
</FluentAutocomplete>

<p>
    <b>Selected</b>: @(String.Join(" - ", SelectedItems.Select(i => i.Name)))
</p>

@code
{
    FluentAutocomplete<Country> _autocomplete = default!;
    bool AutoHeight = false;
    IEnumerable<Country> SelectedItems = Array.Empty<Country>();

    private async Task OnSearchAsync(OptionsSearchEventArgs<Country> e)
    {
        var allCountries = await Data.GetCountriesAsync();
        e.Items = allCountries.Where(i => i.Name.StartsWith(e.Text, StringComparison.OrdinalIgnoreCase))
                              .OrderBy(i => i.Name);
    }

    private async Task OpenDialog()
    {
        await _autocomplete.CloseDropdownAsync();
        await DialogService.ShowInfoAsync("You pressed a button to open a dialog and close the dropdown.");
    }
}

```

--------------------------------

### Fluent Emoji Component with Custom Sizes in Razor

Source: https://www.fluentui-blazor.net/Emoji

Illustrates how to render Fluent Emojis at different predefined and custom sizes using the Width parameter. It includes examples for 16x16, 32x32, 64x64, 128x128, and dynamically set custom sizes based on user input. Requires the Microsoft.FluentUI.AspNetCore.Components.EmojiCopy namespace and a string variable for custom size input.

```Razor
<div style="display: flex; align-items: flex-end; margin-bottom: 0.75em; gap: 10px">
    <span>16x16: </span><FluentEmoji Value="@(new Emojis.FoodDrink.Color.Default.Hamburger())" Width="16px" />
    <span>32x32: </span><FluentEmoji Value="@(new Emojis.FoodDrink.Color.Default.Hamburger())" Width="32px" />
    <span>64x64: </span><FluentEmoji Value="@(new Emojis.FoodDrink.Color.Default.Hamburger())" Width="64px" />
    <span>128x128: </span><FluentEmoji Value="@(new Emojis.FoodDrink.Color.Default.Hamburger())" Width="128px" />
</div>

<FluentDivider Role=DividerRole.Presentation />

<div style="display: flex; align-items: center;  margin-top: 0.75em;">
    <label for="CustomSize">Custom size</label>
    <FluentTextField Id="CustomSize" @bind-Value="@custom" Placeholder="Just 1 number, like 128 or 256"/>
</div>

@if (int.TryParse(custom, out int size) && size > 0)
{
    <div style="display: flex; align-items: flex-end; margin-bottom: 0.75em; gap: 10px">
        <span>@(size)x@(size): </span>
        <FluentEmoji Value="@(new Emojis.FoodDrink.Color.Default.Hamburger())" Width="@($"{size}px")" />
    </div>
}

@code {
    string custom = "";
}
```

--------------------------------

### FluentMenuItem with FluentIcons in Blazor

Source: https://www.fluentui-blazor.net/Menu

Demonstrates how to correctly place FluentIcons within FluentMenuItem components in Blazor. A workaround is provided using span elements with explicit 'start' or 'end' slots to ensure proper indentation, as FluentMenu's internal script may not detect icons added late in the render process.

```razor
@* 
    We can't use the FluentIcon's own Slot parameter here because it will not be detected by FluentMenu's internal script to determine 
    indentation (they are added too late in the render process). Workaround is to define a slot manually and place the FluentIcon in there
*@
<FluentSwitch @bind-Value="@open">Show</FluentSwitch>
<FluentMenu @bind-Open="@open">
    <FluentMenuItem>
        Menu item 1
        <span slot="start" >
            <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" />
        </span>
        <span slot="end">
            <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" />
        </span>
    </FluentMenuItem>
    <FluentMenuItem>
        Menu item 2
        <span slot="start">
            <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" />
        </span>
        <span slot="end">
            <FluentIcon Value="@(new Icons.Regular.Size16.Globe())" Color="@Color.Neutral" />
        </span>
    </FluentMenuItem>
    <FluentMenuItem>
        Menu item 3
        <span slot="start">
            <FluentIcon Value="@(new Icons.Filled.Size16.Globe())" Color="@Color.Accent" />
        </span>
        <span slot="end">
            <FluentIcon Value="@(new Icons.Filled.Size16.Globe())" Color="@Color.Accent" />
        </span>
    </FluentMenuItem>
</FluentMenu>

@code {
    bool open = false;
}

```

--------------------------------

### Fluent Blazor Calendar: Customized Day Rendering

Source: https://www.fluentui-blazor.net/DateTime

Illustrates how to customize the rendering of individual days within the FluentCalendar component using the `DaysTemplate`. This example shows how to apply specific styling (e.g., red color, bold font) to certain days based on conditions, while also demonstrating the functionality to disable specific dates. This snippet is for Blazor applications using Fluent UI.

```Razor
<FluentCalendar DisabledDateFunc="@DisabledDate" @bind-Value="@SelectedValue">
    <DaysTemplate>
        @if (!context.IsInactive &&
            (context.Date.Day == 5 || context.Date.Day == 15))
        {
            <div style="color: red; font-weight: bold;">
                @context.DayNumber
            </div>
        }
        else
        {
            @context.DayNumber
        }
    </DaysTemplate>
</FluentCalendar>
<p>Selected date @(SelectedValue?.ToString("yyyy-MM-dd"))</p>

@code
{
    private DateTime? SelectedValue = null;

    private bool DisabledDate(DateTime date) 
    {
        return (date.Day == 3 ||
                date.Day == 8 ||
                date.Day == 20);
    }
}
```

--------------------------------

### FluentDialogBody Component API

Source: https://www.fluentui-blazor.net/Dialog

API documentation for the FluentDialogBody component, which serves as the main content area for a dialog.

```APIDOC
## FluentDialogBody Component API

### Description

Represents the primary content area within a dialog component.

### Method

Not Applicable (Component)

### Endpoint

Not Applicable (Component)

### Parameters

#### Path Parameters

None

#### Query Parameters

None

#### Request Body

None

### Request Example

None

### Response

None

#### Success Response (200)

None

#### Response Example

None

#### Parameters

- **ChildContent** (RenderFragment?) - Gets or sets the content to be rendered inside the component.
```

--------------------------------

### FluentDialog Properties

Source: https://www.fluentui-blazor.net/Dialog

This section outlines the configurable properties for the FluentDialog component, detailing their types, default values, and descriptions.

```APIDOC
## FluentDialog Properties

This section outlines the configurable properties for the FluentDialog component, detailing their types, default values, and descriptions.

### Properties

Name| Type| Default| Description
---|---|---|---
`Alignment`| HorizontalAlignment | Center| Gets or sets the dialog position: left (full height), right (full height) or screen middle (using Width and Height properties). HorizontalAlignment.Stretch is not supported for this property.
`AriaDescribedby`| string?| | Gets or sets the element that describes the element on which the attribute is set.
`AriaLabel`| string?| | Gets or sets the value that labels an interactive element.
`AriaLabelledby`| string?| | Gets or sets the element that labels the element it is applied to.
`Content`| TContent| | Gets or sets the content to pass to and from the dialog.
`DialogBodyStyle`| string| | Gets or sets the extra styles applied to the Body content.
`DialogType`| DialogType | Dialog| Gets or sets the type of dialog.
`DismissTitle`| string?| Close| Gets or sets the Title of the dismiss button, display in a tooltip. Defaults to 'Close'.
`Height`| string?| | Gets or sets the height of the dialog. Must be a valid CSS height value like '600px' or '3em'. Only used if Alignment is set to 'HorizontalAlignment.Center'.
`Item`| Object| |
`Modal`| bool?| True| Determines if the dialog is modal. Defaults to true. Obscures the area around the dialog.
`PreventDismissOnOverlayClick`| bool| False| Determines if a modal dialog is dismissible by clicking outside the dialog. Defaults to false.
`PreventScroll`| bool| True| Prevents scrolling outside of the dialog while it is shown.
`PrimaryAction`| string?| OK| Gets or sets the text to display for the primary action.
`PrimaryActionEnabled`| bool| True| When true, primary action's button is enabled.
`SecondaryAction`| string?| Cancel| Gets or sets the text to display for the secondary action.
`SecondaryActionEnabled`| bool| True| When true, secondary action's button is enabled.
`ShowDismiss`| bool| True| Gets or sets a value indicating whether show the dismiss button in the header. Defaults to true.
`ShowTitle`| bool| True| Gets or sets a value indicating whether show the title in the header. Defaults to true.
`Title`| string?| | Gets or sets the title of the dialog.
`TitleTypo`| Typography | PaneHeader| Gets or sets the `Typography` style for the title text. Defaults to `Typography.PaneHeader`.
`TrapFocus`| bool?| True| Gets or sets a value indicating whether if dialog should trap focus. Defaults to true.
`ValidateDialogAsync`| Func<Task<bool>>| | Function that is called and awaited before the dialog is closed.
`Visible`| bool| True| Gets or sets if a dialog is visible or not (Hidden).
`Width`| string?| | Gets or sets the width of the dialog. Must be a valid CSS width value like '600px' or '3em'.
```

--------------------------------

### Styling HTML <abbr> Element for Abbreviations

Source: https://www.fluentui-blazor.net/Reboot

Shows basic styling applied to the `<abbr>` element to make HTML abbreviations stand out within paragraph text.

```html
The HTML abbreviation element.
```

--------------------------------

### Refresh Fluent DataGrid Data Programmatically (C#)

Source: https://www.fluentui-blazor.net/datagrid

This C# code snippet demonstrates how to refresh the data in a FluentDataGrid component programmatically. It shows the usage of the `@ref` attribute to get a reference to the grid instance and then calls the `RefreshDataAsync` method within an event handler. This is useful when the underlying data source is updated externally.

```csharp
<FluentDataGrid ... @ref="myGrid">
...
</FluentDataGrid>

@code {
FluentDataGrid<MyDataType> myGrid;

async Task HandleSomeEvent()
{
...

// We can force the grid to reload the current data
await myGrid.RefreshDataAsync();
}
}
```

--------------------------------

### FluentAnchoredRegion: Add to Fixed Region (Razor)

Source: https://www.fluentui-blazor.net/AnchoredRegion

Illustrates how to use FluentAnchoredRegion to place content within a fixed region, ensuring it stays in a consistent position on the screen regardless of scrolling. This example anchors a div with specific styling and content to a fixed position. The `FixedPlacement="true"` property is key here. Inputs include anchor ID, positioning modes, and fixed placement flags. The output is a styled div positioned fixedly.

```Razor
<FluentAnchoredRegion Anchor="anchor-fixed"
                      VerticalPositioningMode="AxisPositioningMode.Dynamic"
                      VerticalScaling="AxisScalingMode.Anchor"
                      VerticalInset="false"
                      HorizontalPositioningMode="AxisPositioningMode.Locktodefault"
                      HorizontalDefaultPosition="HorizontalPosition.Left"
                      FixedPlacement="true"
                      Style="z-index: 11;"
                      Viewport=""
                      HorizontalScaling="AxisScalingMode.Content"
                      VerticalDefaultPosition="VerticalPosition.Unset"
                      AutoUpdateMode="AutoUpdateMode.Anchor">
    <div style="height:100%;width:100%;background:var(--accent-fill-active);z-index:11;">outside &amp; fixed</div>
</FluentAnchoredRegion>
<p>
    No actual output shown here.The result of this example is added to the <FluentAnchor Href="#fixed" Appearance="Appearance.Hypertext">fixed region</FluentAnchor> shown at the top of the examples section
</p>
```

--------------------------------

### FluentDialogFooter Component API

Source: https://www.fluentui-blazor.net/Dialog

API documentation for the FluentDialogFooter component, which allows for custom content and visibility control within a dialog's footer.

```APIDOC
## FluentDialogFooter Component API

### Description

Defines the footer section of a dialog, allowing for custom content and controlling its visibility.

### Method

Not Applicable (Component)

### Endpoint

Not Applicable (Component)

### Parameters

#### Path Parameters

None

#### Query Parameters

None

#### Request Body

None

### Request Example

None

### Response

None

#### Success Response (200)

None

#### Response Example

None

#### Parameters

- **ChildContent** (RenderFragment?) - Gets or sets the content to be rendered inside the component.
- **Visible** (bool) - When true, the footer is visible. Default is True.
```

--------------------------------

### Default TreeView with Event Handling in Razor

Source: https://www.fluentui-blazor.net/TreeView

This Razor code demonstrates the default configuration of the Fluent UI Blazor TreeView component. It includes nested tree items, disabled items, and event handling for selection (`@bind-CurrentSelected`) and expansion changes (`OnExpandedChange`). The example shows how to bind the selected and affected tree items to C# variables.

```razor
<FluentTreeView @bind-CurrentSelected=currentSelected OnExpandedChange="HandleOnExpandedChanged">
    <FluentTreeItem Text="Root item 1">
        <FluentTreeItem Text="Flowers">
            <FluentTreeItem Disabled="true" Text="Daisy" />
            <FluentTreeItem Text="Sunflower" />
            <FluentTreeItem Text="Rose" />
        </FluentTreeItem>
        <FluentTreeItem Text="Nested item 2" />
        <FluentTreeItem Text="Nested item 3" />
    </FluentTreeItem>
    <FluentTreeItem Text="Root item 2">
        <FluentDivider></FluentDivider>
        <FluentTreeItem Text="Flowers">
            <FluentTreeItem Disabled="true" Text="Daisy" />
            <FluentTreeItem Text="Sunflower" />
            <FluentTreeItem Text="Rose" />
        </FluentTreeItem>
        <FluentTreeItem Text="Nested item 2" />
        <FluentTreeItem Text="Nested item 3" />
    </FluentTreeItem>
    <FluentTreeItem Text="Root item 3 - Leaf Erikson" />
</FluentTreeView>

<p>Current selected tree item is @currentSelected?.Text</p>
<p>Most recently expanded/collapsed tree item is @currentAffected?.Text</p>

@code {
    FluentTreeItem? currentSelected;
    FluentTreeItem? currentAffected;

    private void HandleOnExpandedChanged(FluentTreeItem item)
    {
        currentAffected = item;
    }
}
```

--------------------------------

### Fetch and Display Food Recall Data with Blazor DataGrid

Source: https://www.fluentui-blazor.net/datagrid-remote-data

This C# code demonstrates fetching food recall data from an API using Blazor and displaying it in a Fluent UI Blazor DataGrid. It handles query parameter construction, HTTP requests, and updates the UI with the retrieved data, including pagination information. Dependencies include `Microsoft.AspNetCore.Components.WebAssembly.Http` and Fluent UI Blazor components.

```csharp
        filters.Add("sort", s.PropertyName + (s.Direction == SortDirection.Ascending ? ":asc" : ":desc"));
        }

        var url = NavManager.GetUriWithQueryParameters("https://api.fda.gov/food/enforcement.json", filters);

        var response = await Http.GetFromJsonAsync<FoodRecallQueryResult>(url);

        foodRecallItems = response!.Results.AsQueryable();
        await pagination.SetTotalItemCountAsync(response!.Meta.Results.Total);

        loading = false;
        await InvokeAsync(StateHasChanged);

    }

    public void ClearFilters()
    {
        _stateFilter = null;
    }

    public async Task DataGridRefreshDataAsync()
    {
        await dataGrid.RefreshDataAsync(true);
    }
}
```

--------------------------------

### Single Select Autocomplete with Disabled Options (Razor)

Source: https://www.fluentui-blazor.net/Autocomplete

This Razor example shows a Fluent Autocomplete configured for single selection (Multiple=false). It allows users to select a country from a list, with an option to disable specific items (e.g., country code 'au'). The component uses a placeholder and labels for user guidance, and the selected item's name is displayed below.

```Razor
@inject DataSource Data

<FluentAutocomplete TOption="Country"
                    AutoComplete="off"
                    Label="Select a country"
                    Width="250px"
                    Placeholder="Select a country"
                    OnOptionsSearch="@OnSearchAsync"
                    OptionDisabled="@(e => e.Code == "au")"
                    Multiple=false
                    OptionText="@(item => item.Name)"
                    @bind-SelectedOption=SelectedItem />

<p>
    <b>Selected</b>: @(SelectedItem?.Name)
</p>

@code
{
    Country? SelectedItem = null;

    private async Task OnSearchAsync(OptionsSearchEventArgs<Country> e)
    {
        var allCountries = await Data.GetCountriesAsync();
        e.Items = allCountries.Where(i => i.Name.StartsWith(e.Text, StringComparison.OrdinalIgnoreCase))
                              .OrderBy(i => i.Name);
    }
}

```

--------------------------------

### FluentSortableList: Enable fallback behavior with Fallback parameter (Razor)

Source: https://www.fluentui-blazor.net/SortableList

Demonstrates how to use the Fallback parameter in FluentSortableList to disable native HTML5 drag-and-drop behavior and use a custom fallback. This is useful for ensuring consistent drag-and-drop across different browsers or for implementing custom drag logic. The example includes C# code for item definition and list sorting.

```Razor
<FluentGrid Justify="JustifyContent.FlexStart" Spacing="3">
    <FluentGridItem xs="12" sm="6">
        <FluentSortableList Fallback="true" Items="items" OnUpdate="@SortList">
            <ItemTemplate>@context.Name</ItemTemplate>
        </FluentSortableList>
    </FluentGridItem>
</FluentGrid>

@code {

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public bool Disabled { get; set; } = false;
    }

    public List<Item> items = Enumerable.Range(1, 10).Select(i => new Item { Id = i, Name = $"Item {i}" }).ToList();


    private void SortList(FluentSortableListEventArgs args)
    {
        if (args is null || args.OldIndex == args.NewIndex)
        {
            return;
        }

        var oldIndex = args.OldIndex;
        var newIndex = args.NewIndex;

        var items = this.items;
        var itemToMove = items[oldIndex];
        items.RemoveAt(oldIndex);

        if (newIndex < items.Count)
        {
            items.Insert(newIndex, itemToMove);
        }
        else
        {
            items.Add(itemToMove);
        }

        StateHasChanged();
    }
}

```

--------------------------------

### Fluent UI Blazor ReadOnly Calendar with French Culture

Source: https://www.fluentui-blazor.net/DateTime

This example demonstrates a read-only FluentCalendar component configured to use the French culture ('fr-FR'). It also specifies a 'DayFormat.TwoDigit' for displaying days, resulting in a calendar that shows dates like '01', '02', etc. The calendar is not interactive for date selection.

```Razor
<FluentCalendar ReadOnly DayFormat="DayFormat.TwoDigit" Culture="@(System.Globalization.CultureInfo.GetCultureInfo("fr-FR"))" />
```

--------------------------------

### Configure FluentDataGrid for Multiline Cells (Razor)

Source: https://www.fluentui-blazor.net/datagrid-multi-line

This code snippet configures a FluentDataGrid to display cell content that spans multiple lines. It requires the Fluent UI Blazor library and defines a 'Person' record with 'PersonId', 'Name', and 'Description' properties. The 'MultiLine' parameter is set to 'true' on the FluentDataGrid, and 'PropertyColumn' is used for each property, including the 'Description' which contains long text.

```razor
<FluentDataGrid Items="@people" MultiLine="true">
    <PropertyColumn Property="@(p => p.PersonId)" Sortable="true" />
    <PropertyColumn Property="@(p => p.Name)" Sortable="true" />
    <PropertyColumn Property="@(p => p.Description)" Sortable="true" />
</FluentDataGrid>

@code {
    record Person(int PersonId, string Name, string Description);

    IQueryable<Person> people = new[]
    {
        new Person(10895, "Jean Martin", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."),
        new Person(10944, "António Langa", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."),
        new Person(11203, "Julie Smith","Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."),
    }.AsQueryable();
}

```

--------------------------------

### Blazor DialogService with EventCallback and Data Handling

Source: https://www.fluentui-blazor.net/Dialog

This Blazor component demonstrates opening a dialog using DialogService and handling the result via an EventCallback. It allows configuration of modal behavior, focus trapping, and primary/secondary actions. Data is passed to and received from the dialog component.

```csharp
@inject IDialogService DialogService

<div>
    <p>
        When 'Modal' is checked, the dialog can be <em>dismissed</em> by clicking outside of the dialog (anywhere on the overlay). When unchecked,
        the dialog can be <em>dismissed</em> only by the 'ESC' key.<br />The dialog can always be <em>closed</em> by using the 'Close dialog'
        button.
    </p>
    <p>
        When 'Trap focus' is checked, only the elements within the dialog will receive focus. When unchecked, focus will also move outside of the
        dialog.
    </p>
    <FluentCheckbox Name="modal" @bind-Value="_modal">
        Modal
    </FluentCheckbox>
    <FluentCheckbox Name="trap" @bind-Value="_trapFocus">
        Trap focus
    </FluentCheckbox>
</div>
<div style="margin-top: 1rem;">
    <FluentButton OnClick="@OpenDialogAsync" Appearance="Appearance.Accent">
        Open Dialog
    </FluentButton>
</div>

@code {
    private bool _trapFocus = true;
    private bool _modal = true;

    SimplePerson simplePerson = new()
        {
            Firstname = "Steve",
            Lastname = "Roth",
            Age = 39,
        };

    private async Task OpenDialogAsync()
    {
        DemoLogger.WriteLine($"Open dialog centered");

        await DialogService.ShowDialogAsync<SimpleDialog>(simplePerson, new DialogParameters()
            {
                Title = $"Hello {simplePerson.Firstname}",
                OnDialogResult = DialogService.CreateDialogCallback(this, HandleDialog),
                PrimaryAction = "Yes",
                PrimaryActionEnabled = false,
                SecondaryAction = "No",
                Width = "500px",
                Height = "500px",
                TrapFocus = _trapFocus,
                Modal = _modal,
            });
    }

    private async Task HandleDialog(DialogResult result)
    {
        if (result.Cancelled)
        {
            await Task.Run(() => DemoLogger.WriteLine($"Dialog cancelled"));
            return;
        }
        if (result.Data is not null)
        {
            SimplePerson? simplePerson = result.Data as SimplePerson;
            await Task.Run(() => DemoLogger.WriteLine($"Dialog closed by {simplePerson?.Firstname} {simplePerson?.Lastname} ({simplePerson?.Age})"));
            return;
        }

        await Task.Run(() => DemoLogger.WriteLine($"Dialog closed"));

    }
}
```

--------------------------------

### ShowSplashScreenAsync<T>

Source: https://www.fluentui-blazor.net/DialogService

Shows a custom splash screen dialog of a generic type T with specified parameters.

```APIDOC
## POST /dialogs/splashscreen/custom

### Description
Shows a custom splash screen dialog of a generic type `T` with the given parameters. This overload does not explicitly take a callback function, but the dialog itself might handle callbacks internally.

### Method
POST

### Endpoint
/dialogs/splashscreen/custom

### Parameters
#### Query Parameters
- **T** (generic type) - Required - The type of the custom splash screen component.
- **parameters** (DialogParameters<SplashScreenContent>) - Optional - Parameters for the splash screen dialog.

### Request Body
(Not applicable for this endpoint, parameters are passed via query string or method arguments)

### Response
#### Success Response (200)
- **IDialogReference** (object) - A reference to the displayed dialog.

#### Response Example
```json
{
  "dialogReference": {
    "id": "some-custom-dialog-id"
  }
}
```
```

--------------------------------

### Indicating Variables with HTML <var> Tag

Source: https://www.fluentui-blazor.net/Reboot

Demonstrates the use of the `<var>` tag to denote variables in mathematical or programming contexts. This tag helps visually distinguish variables from regular text, improving readability of formulas and code snippets.

```html
y = mx + b
```

--------------------------------

### Razor: FluentHorizontalScroll with External Navigation Buttons

Source: https://www.fluentui-blazor.net/HorizontalScroll

This code snippet demonstrates how to use the FluentHorizontalScroll component in Blazor to create a horizontally scrollable list of FluentCards. It also shows how to implement external buttons to control the scrolling behavior, such as scrolling to the next item, previous item, or a specific item by its index. The example utilizes Blazor's `@code` block to manage the component's reference.

```razor
<FluentHorizontalScroll @ref="_horizontalScroll" Speed="600" Easing=ScrollEasing.EaseInOut>
    <FluentCard>Card number 1</FluentCard>
    <FluentCard>Card number 2</FluentCard>
    <FluentCard>Card number 3</FluentCard>
    <FluentCard>Card number 4</FluentCard>
    <FluentCard>Card number 5</FluentCard>
    <FluentCard>Card number 6</FluentCard>
    <FluentCard>Card number 7</FluentCard>
    <FluentCard>Card number 8</FluentCard>
    <FluentCard>Card number 9</FluentCard>
    <FluentCard>Card number 10</FluentCard>
    <FluentCard>Card number 11</FluentCard>
    <FluentCard>Card number 12</FluentCard>
    <FluentCard>Card number 13</FluentCard>
    <FluentCard>Card number 14</FluentCard>
    <FluentCard>Card number 15</FluentCard>
    <FluentCard>Card number 16</FluentCard>
</FluentHorizontalScroll>

<FluentButton @onclick="() => _horizontalScroll.ScrollToPrevious()">Scroll to Previous</FluentButton>
<FluentButton @onclick="() => _horizontalScroll.ScrollToNext()">Scroll to Next</FluentButton>
<FluentButton @onclick="() => _horizontalScroll.ScrollInView(10)">Scroll to 10</FluentButton>

@code {
    FluentHorizontalScroll _horizontalScroll = default!;
}

```

--------------------------------

### FluentSortableList: Filter items by ItemFilter parameter (Razor)

Source: https://www.fluentui-blazor.net/SortableList

Demonstrates filtering items in a sortable list using the ItemFilter parameter. This parameter accepts a Func<TItem, bool> to exclude specific items from being dragged. The example shows filtering items with 'Disabled' set to true and items with an 'Id' greater than 6. It includes the C# code for item definition and list population.

```Razor
<FluentGrid Justify="JustifyContent.FlexStart" Spacing="3">
    <FluentGridItem xs="12" sm="6">
        <FluentSortableList Id="filter" ItemFilter="@(i => i.Disabled)" Items="items1" OnUpdate="@SortListOne" Context="item">
             <ItemTemplate>@item.Name</ItemTemplate>
         </FluentSortableList>
     </FluentGridItem>

    <FluentGridItem xs="12" sm="6">
        <FluentSortableList Id="filter2" ItemFilter="@(i => i.Id > 6)" Items="items2" OnUpdate="@SortListTwo" Context="item">
            <ItemTemplate>@item.Name</ItemTemplate>
        </FluentSortableList>
    </FluentGridItem>
 </FluentGrid>

@code {
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public bool Disabled { get; set; } = false;
    }

    public List<Item> items1 = Enumerable.Range(1, 10).Select(i => new Item { Id = i, Name = $"Item {i}" }).ToList();
    public List<Item> items2 = Enumerable.Range(1, 10).Select(i => new Item { Id = i, Name = $"Item {i}" }).ToList();

    // on initialized, set a random item in the list to disabled
    protected override void OnInitialized()
    {
        var random = new Random();
        var randomIndex = random.Next(0, items1.Count);
        items1[randomIndex].Disabled = true;
    }


    private void SortListOne(FluentSortableListEventArgs args)
    {
        if (args is null || args.OldIndex == args.NewIndex)
        {
            return;
        }

        var oldIndex = args.OldIndex;
        var newIndex = args.NewIndex;

        var items = this.items1;
        var itemToMove = items1[oldIndex];
        items.RemoveAt(oldIndex);

        if (newIndex < items1.Count)
        {
            items1.Insert(newIndex, itemToMove);
        }
        else
        {
            items1.Add(itemToMove);
        }
    }

    private void SortListTwo(FluentSortableListEventArgs args)
    {
        if (args is null || args.OldIndex == args.NewIndex)
        {
            return;
        }

        var oldIndex = args.OldIndex;
        var newIndex = args.NewIndex;

        var items = this.items2;
        var itemToMove = items2[oldIndex];
        items.RemoveAt(oldIndex);

        if (newIndex < items2.Count)
        {
            items2.Insert(newIndex, itemToMove);
        }
        else
        {
            items2.Add(itemToMove);
        }
    }
}

```

--------------------------------

### Configure FluentSelect with Initial Selections and Disabled Options in Blazor

Source: https://www.fluentui-blazor.net/Select

This experimental code showcases advanced FluentSelect configuration for multiple selections. It uses `OptionSelected` to pre-select items based on a condition (first name starting with 'J') and `OptionDisabled` to disable items based on another condition (birth year > 1990). It demonstrates binding to both a single value and multiple selected options, along with explanations of potential inaccuracies in the single selected value display.

```razor
@inject DataSource Data
<h6>!!Experimental!!</h6>
<p>All people whose first name starts with a "J" are initialy selected through the <code>OptionSelected</code> (Func delegate) parameter.</p>
<p>All people with a birth year greater than 1990 are disabled through the <code>OptionDisabled</code> (Func delegate) parameter.</p>


    <FluentSelect TOption="Person"
                  Label="Select persons"
                  Items="@Data.People"
                  Id="people-listbox2"
                  Multiple="true"
                  OptionValue="@(p => p.PersonId.ToString())"
                  OptionText="@(p => p.LastName + ", " + p.FirstName)"
                  OptionDisabled="@(p => p.BirthDate.Year > 1990 )"
                  OptionSelected="@(p => p.FirstName.StartsWith("J"))"
                  @bind-Value="@SelectedValue"
                  @bind-SelectedOptions="@SelectedOptions" />


<p>
    Selected value: @SelectedValue <br />
    <em>Reflects the value of the first selected option</em><br />
    <em><b>
        This value is not always accurate as a user can potentially deselect all enabled options. A browser will not return the value
        of a disabled item so the last selected non-disabled value will be returned even if it is not selected anymore!
        </b>
    </em>
</p>
<p>
    Selected options: @(SelectedOptions != null ? String.Join(", ", SelectedOptions.Select(i => i.FirstName)) : "")<br />
    <em>Strongly typed enumeration of the selected options</em>. In this case a concatenated string of the first names of the selected persons.
</p>

@code
{
    IEnumerable<Person>? SelectedOptions;
    string? SelectedValue;
}

```

--------------------------------

### General Flipper Usage - Blazor

Source: https://www.fluentui-blazor.net/Flipper

Demonstrates the general usage of the FluentFlipper component in Blazor, including previous, next, aria-hidden, and disabled states. It controls content paging and accessibility.

```razor
<h4>Previous</h4>
<FluentFlipper Direction="FlipperDirection.Previous"></FluentFlipper>

<h4>Next</h4>
<FluentFlipper Direction="FlipperDirection.Next"></FluentFlipper>

<h4>With aria-hidden</h4>
<FluentFlipper AriaHidden="false"></FluentFlipper>

<h4>Disabled</h4>
<FluentFlipper Disabled="true"></FluentFlipper>
```

--------------------------------

### ShowWarning / ShowWarningAsync

Source: https://www.fluentui-blazor.net/DialogService

Displays a warning message box with a title and a primary action. ShowWarningAsync returns a dialog reference.

```APIDOC
## POST /messages/warning

### Description
Shows a warning message box. The `ShowWarning` method returns `void`, while `ShowWarningAsync` returns a `Task<IDialogReference>`.

### Method
POST

### Endpoint
/messages/warning

### Parameters
#### Query Parameters
- **message** (string) - Required - The message content to display.
- **title** (string) - Optional - The title of the message box.
- **primaryAction** (string) - Optional - The text for the primary action button.

### Request Body
(Not applicable for this endpoint, parameters are passed via query string or method arguments)

### Response
#### Success Response (200)
- **IDialogReference** (object) - (Only for `ShowWarningAsync`) A reference to the displayed message box.
- **void** - (For `ShowWarning`) No return value.

#### Response Example
```json
{
  "dialogReference": {
    "id": "warning-message-id"
  }
}
```
```

--------------------------------

### Default FluentProfileMenu in Razor

Source: https://www.fluentui-blazor.net/ProfileMenu

Demonstrates the default configuration of the FluentProfileMenu component in Razor. It includes basic user information such as image, status, name, and email. This snippet requires the FluentUI Blazor library.

```Razor
<FluentStack HorizontalAlignment="@HorizontalAlignment.End"
             VerticalAlignment="@VerticalAlignment.Center"
             Style="height: 48px; background: var(--neutral-layer-4); padding-inline-end: 10px; ">
    <FluentProfileMenu Image="@DataSource.SamplePicture"
                       Status="@PresenceStatus.Available"
                       HeaderLabel="Microsoft"
                       Initials="BG"
                       FullName="Bill Gates"
                       EMail="bill.gates@microsoft.com"
                       PopoverStyle="min-width: 330px;" />
</FluentStack>
```