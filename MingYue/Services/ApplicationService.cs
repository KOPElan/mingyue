using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;

namespace MingYue.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IDbContextFactory<MingYueDbContext> _dbFactory;
        private readonly ILogger<ApplicationService> _logger;

        public ApplicationService(
            IDbContextFactory<MingYueDbContext> dbFactory,
            ILogger<ApplicationService> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
        }

        public async Task<List<Application>> GetAllApplicationsAsync()
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                return await context.Applications
                    .AsNoTracking()
                    .OrderBy(a => a.Order)
                    .ThenBy(a => a.Title)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all applications");
                return new List<Application>();
            }
        }

        public async Task<Application?> GetApplicationByIdAsync(int id)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                return await context.Applications
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting application by ID {Id}", id);
                return null;
            }
        }

        public async Task<Application?> GetApplicationByAppIdAsync(string appId)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                return await context.Applications
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.AppId == appId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting application by AppId {AppId}", appId);
                return null;
            }
        }

        public async Task<Application> CreateApplicationAsync(Application application)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();

                // Generate unique AppId if not provided
                if (string.IsNullOrEmpty(application.AppId))
                {
                    application.AppId = Guid.NewGuid().ToString();
                }

                // Set timestamps
                application.CreatedAt = DateTime.UtcNow;
                application.UpdatedAt = DateTime.UtcNow;

                // Get max order and set new order
                var maxOrder = await context.Applications.MaxAsync(a => (int?)a.Order) ?? 0;
                application.Order = maxOrder + 1;

                context.Applications.Add(application);
                await context.SaveChangesAsync();

                _logger.LogInformation("Application {Title} created with ID {Id}", 
                    application.Title, application.Id);

                return application;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating application {Title}", application.Title);
                throw;
            }
        }

        public async Task<Application?> UpdateApplicationAsync(Application application)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();

                var existingApp = await context.Applications.FindAsync(application.Id);
                if (existingApp == null)
                {
                    _logger.LogWarning("Application with ID {Id} not found for update", application.Id);
                    return null;
                }

                // Update properties
                existingApp.Title = application.Title;
                existingApp.Url = application.Url;
                existingApp.Icon = application.Icon;
                existingApp.IconColor = application.IconColor;
                existingApp.Description = application.Description;
                existingApp.Category = application.Category;
                existingApp.IsVisible = application.IsVisible;
                existingApp.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync();

                _logger.LogInformation("Application {Title} updated", application.Title);

                return existingApp;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating application {Id}", application.Id);
                return null;
            }
        }

        public async Task<bool> DeleteApplicationAsync(int id)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();

                var application = await context.Applications.FindAsync(id);
                if (application == null)
                {
                    _logger.LogWarning("Application with ID {Id} not found for deletion", id);
                    return false;
                }

                context.Applications.Remove(application);
                await context.SaveChangesAsync();

                _logger.LogInformation("Application {Title} deleted", application.Title);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting application {Id}", id);
                return false;
            }
        }

        public async Task<bool> ReorderApplicationsAsync(List<int> applicationIds)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();

                for (int i = 0; i < applicationIds.Count; i++)
                {
                    var app = await context.Applications.FindAsync(applicationIds[i]);
                    if (app != null)
                    {
                        app.Order = i;
                        app.UpdatedAt = DateTime.UtcNow;
                    }
                }

                await context.SaveChangesAsync();

                _logger.LogInformation("Applications reordered successfully");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reordering applications");
                return false;
            }
        }
    }
}
