using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using MingYue.Components;
using MingYue.Data;
using MingYue.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();
builder.Services.AddHttpClient();

// Add SQLite database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=mingyue.db";
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
builder.Services.AddScoped<AuthenticationStateService>();
var app = builder.Build();

// Apply database migrations automatically
using (var scope = app.Services.CreateScope())
{
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MingYueDbContext>>();
    using var context = await dbContextFactory.CreateDbContextAsync();
    await context.Database.MigrateAsync();
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

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
