using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using MingYue.Components;
using MingYue.Data;
using MingYue.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new System.Globalization.CultureInfo("zh-CN"),
        new System.Globalization.CultureInfo("en-US")
    };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("zh-CN");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    
    // Add cookie-based culture provider as highest priority
    options.RequestCultureProviders.Insert(0, new Microsoft.AspNetCore.Localization.CookieRequestCultureProvider());
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

// Add SQLite database
var dataDir = Environment.GetEnvironmentVariable("MINGYUE_DATA_DIR");
string connectionString;

// Check if connection string is explicitly configured
var configuredConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrWhiteSpace(configuredConnectionString))
{
    // Use the explicitly configured connection string
    connectionString = configuredConnectionString;
}
else if (!string.IsNullOrWhiteSpace(dataDir))
{
    // Build connection string from MINGYUE_DATA_DIR environment variable
    // Ensure the directory exists before creating the database
    if (!Directory.Exists(dataDir))
    {
        Directory.CreateDirectory(dataDir);
    }
    var dbPath = Path.Combine(dataDir, "mingyue.db");
    connectionString = $"Data Source={dbPath}";
}
else
{
    // Default to mingyue.db in the current directory
    connectionString = "Data Source=mingyue.db";
}

// Register DbContextFactory for services that need multiple contexts or custom lifetime management (e.g., FileManagerService)
builder.Services.AddDbContextFactory<MingYueDbContext>(options =>
    options.UseSqlite(connectionString));

// Register application services
builder.Services.AddScoped<ISystemMonitorService, SystemMonitorService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IDockItemService, DockItemService>();
builder.Services.AddScoped<IFileManagementService, FileManagementService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IThumbnailService, ThumbnailService>();
builder.Services.AddScoped<IFileIndexService, FileIndexService>();
builder.Services.AddScoped<IDiskManagementService, DiskManagementService>();
builder.Services.AddScoped<IShareManagementService, ShareManagementService>();
builder.Services.AddScoped<IDockerManagementService, DockerManagementService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ISystemSettingService, SystemSettingService>();
builder.Services.AddScoped<IScheduledTaskService, ScheduledTaskService>();
builder.Services.AddScoped<IAnydropService, AnydropService>();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();
builder.Services.AddScoped<INetworkManagementService, NetworkManagementService>();
builder.Services.AddScoped<AuthenticationStateService>();

// Add background services
builder.Services.AddHostedService<ScheduledTaskExecutorService>();
var app = builder.Build();

// Apply database migrations automatically
using (var scope = app.Services.CreateScope())
{
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MingYueDbContext>>();
    using var context = await dbContextFactory.CreateDbContextAsync();
    await context.Database.MigrateAsync();
}

// Check sudo permissions for mount/umount commands on Linux
if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
{
    using var scope = app.Services.CreateScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    try
    {
        // Check if mount command can be run with sudo without password
        var mountCheckInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "sudo",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        mountCheckInfo.ArgumentList.Add("-n");
        mountCheckInfo.ArgumentList.Add("mount");
        mountCheckInfo.ArgumentList.Add("--version");
        
        using var mountCheckProcess = System.Diagnostics.Process.Start(mountCheckInfo);
        if (mountCheckProcess != null)
        {
            await mountCheckProcess.WaitForExitAsync();
            if (mountCheckProcess.ExitCode != 0)
            {
                logger.LogWarning("Sudo permissions not configured for mount command. Mount operations will fail. Please configure /etc/sudoers.d/mingyue with: {User} ALL=(ALL) NOPASSWD: /usr/bin/mount, /usr/bin/umount", 
                    Environment.UserName);
            }
            else
            {
                logger.LogInformation("Sudo permissions for mount/umount commands verified successfully");
            }
        }
    }
    catch (Exception ex)
    {
        logger.LogWarning(ex, "Could not verify sudo permissions for mount/umount commands. This may cause mount operations to fail.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// Use request localization
var localizationOptions = app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
