using MingYue.Models;

namespace MingYue.Services
{
    public interface ISystemSettingService
    {
        /// <summary>
        /// Get all settings
        /// </summary>
        Task<List<SystemSetting>> GetAllSettingsAsync();

        /// <summary>
        /// Get settings by category
        /// </summary>
        Task<List<SystemSetting>> GetSettingsByCategoryAsync(string category);

        /// <summary>
        /// Get setting by key
        /// </summary>
        Task<SystemSetting?> GetSettingByKeyAsync(string key);

        /// <summary>
        /// Get setting value by key
        /// </summary>
        Task<string?> GetSettingValueAsync(string key, string? defaultValue = null);

        /// <summary>
        /// Set or update a setting
        /// </summary>
        Task SetSettingAsync(string key, string value, string category = "General", string? description = null);

        /// <summary>
        /// Delete a setting
        /// </summary>
        Task DeleteSettingAsync(string key);

        /// <summary>
        /// Export all settings as JSON
        /// </summary>
        Task<string> ExportSettingsAsync();

        /// <summary>
        /// Import settings from JSON
        /// </summary>
        Task ImportSettingsAsync(string json);
    }
}
