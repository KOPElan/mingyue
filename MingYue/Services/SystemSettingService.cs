using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;
using System.Text.Json;

namespace MingYue.Services
{
    public class SystemSettingService : ISystemSettingService
    {
        private readonly IDbContextFactory<MingYueDbContext> _contextFactory;
        private readonly ILogger<SystemSettingService> _logger;

        public SystemSettingService(IDbContextFactory<MingYueDbContext> contextFactory, ILogger<SystemSettingService> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<SystemSetting>> GetAllSettingsAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.SystemSettings
                    .OrderBy(s => s.Category)
                    .ThenBy(s => s.Key)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all settings");
                return new List<SystemSetting>();
            }
        }

        public async Task<List<SystemSetting>> GetSettingsByCategoryAsync(string category)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.SystemSettings
                    .Where(s => s.Category == category)
                    .OrderBy(s => s.Key)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting settings for category {Category}", category);
                return new List<SystemSetting>();
            }
        }

        public async Task<SystemSetting?> GetSettingByKeyAsync(string key)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.SystemSettings
                    .FirstOrDefaultAsync(s => s.Key == key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting setting {Key}", key);
                return null;
            }
        }

        public async Task<string?> GetSettingValueAsync(string key, string? defaultValue = null)
        {
            try
            {
                var setting = await GetSettingByKeyAsync(key);
                return setting?.Value ?? defaultValue;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting setting value for {Key}", key);
                return defaultValue;
            }
        }

        public async Task SetSettingAsync(string key, string value, string category = "General", string? description = null)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var setting = await context.SystemSettings
                    .FirstOrDefaultAsync(s => s.Key == key);

                if (setting != null)
                {
                    setting.Value = value;
                    setting.UpdatedAt = DateTime.UtcNow;
                    if (!string.IsNullOrEmpty(description))
                    {
                        setting.Description = description;
                    }
                }
                else
                {
                    setting = new SystemSetting
                    {
                        Key = key,
                        Value = value,
                        Category = category,
                        Description = description ?? "",
                        UpdatedAt = DateTime.UtcNow
                    };
                    context.SystemSettings.Add(setting);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting {Key}", key);
                throw;
            }
        }

        public async Task DeleteSettingAsync(string key)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var setting = await context.SystemSettings
                    .FirstOrDefaultAsync(s => s.Key == key);

                if (setting != null)
                {
                    context.SystemSettings.Remove(setting);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting setting {Key}", key);
                throw;
            }
        }

        public async Task DeleteAllSettingsAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                await context.Database.ExecuteSqlRawAsync("DELETE FROM SystemSettings");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting all settings");
                throw;
            }
        }

        public async Task<string> ExportSettingsAsync()
        {
            try
            {
                var settings = await GetAllSettingsAsync();
                var settingsDict = settings.ToDictionary(s => s.Key, s => new
                {
                    s.Value,
                    s.Category,
                    s.Description,
                    s.DataType
                });

                return JsonSerializer.Serialize(settingsDict, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting settings");
                throw;
            }
        }

        public async Task ImportSettingsAsync(string json)
        {
            try
            {
                var settingsDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
                if (settingsDict == null)
                {
                    throw new InvalidOperationException("Invalid settings JSON");
                }

                foreach (var kvp in settingsDict)
                {
                    var key = kvp.Key;
                    var settingObj = kvp.Value;

                    var value = settingObj.GetProperty("Value").GetString() ?? "";
                    var category = settingObj.TryGetProperty("Category", out var cat) ? cat.GetString() : "General";
                    var description = settingObj.TryGetProperty("Description", out var desc) ? desc.GetString() : null;

                    await SetSettingAsync(key, value, category ?? "General", description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing settings");
                throw;
            }
        }
    }
}
