using Microsoft.JSInterop;
namespace MingYue.Services
{
    /// <summary>
    /// Service for managing localization and language settings
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Get the current culture code (e.g., "zh-CN", "en-US")
        /// </summary>
        string GetCurrentCulture();

        /// <summary>
        /// Set the current culture
        /// </summary>
        /// <param name="culture">Culture code (e.g., "zh-CN", "en-US")</param>
        Task SetCultureAsync(string culture);
        Task SetCultureAsync(string culture, IJSRuntime? jsRuntime);

        /// <summary>
        /// Get localized string by key
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <returns>Localized string</returns>
        string GetString(string key);

        /// <summary>
        /// Get localized string by key with parameters
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <param name="args">Format parameters</param>
        /// <returns>Localized string</returns>
        string GetString(string key, params object[] args);

        /// <summary>
        /// Get all available cultures
        /// </summary>
        List<CultureInfo> GetAvailableCultures();

        /// <summary>
        /// Event raised when culture changes
        /// </summary>
        event EventHandler? CultureChanged;
    }

    /// <summary>
    /// Culture information for localization
    /// </summary>
    public class CultureInfo
    {
        public string Code { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string NativeName { get; set; } = string.Empty;
    }
}
