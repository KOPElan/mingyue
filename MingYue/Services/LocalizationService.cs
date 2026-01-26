using Microsoft.Extensions.Localization;
using System.Globalization;

namespace MingYue.Services
{
    /// <summary>
    /// Service for managing localization and language settings
    /// </summary>
    public class LocalizationService : ILocalizationService
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ISystemSettingService _systemSettingService;
        private readonly ILogger<LocalizationService> _logger;
        private string _currentCulture = "zh-CN"; // Default to Chinese

        public event EventHandler? CultureChanged;

        public LocalizationService(
            IStringLocalizer<SharedResources> localizer,
            ISystemSettingService systemSettingService,
            ILogger<LocalizationService> logger)
        {
            _localizer = localizer;
            _systemSettingService = systemSettingService;
            _logger = logger;
            
            // Load saved culture from settings
            Task.Run(async () =>
            {
                var savedCulture = await _systemSettingService.GetSettingValueAsync("Language");
                if (!string.IsNullOrEmpty(savedCulture))
                {
                    _currentCulture = savedCulture;
                    SetThreadCulture(_currentCulture);
                }
            });
        }

        public string GetCurrentCulture()
        {
            return _currentCulture;
        }

        public async Task SetCultureAsync(string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                _logger.LogWarning("Attempted to set null or empty culture");
                return;
            }

            // Validate culture
            var availableCultures = GetAvailableCultures();
            if (!availableCultures.Any(c => c.Code == culture))
            {
                _logger.LogWarning("Attempted to set unsupported culture: {Culture}", culture);
                return;
            }

            _currentCulture = culture;
            SetThreadCulture(culture);

            // Save to settings
            await _systemSettingService.SetSettingAsync("Language", culture, "General", "语言/Language");

            // Raise event
            CultureChanged?.Invoke(this, EventArgs.Empty);

            _logger.LogInformation("Culture changed to: {Culture}", culture);
        }

        public string GetString(string key)
        {
            try
            {
                var localizedString = _localizer[key];
                return localizedString.ResourceNotFound ? key : localizedString.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting localized string for key: {Key}", key);
                return key;
            }
        }

        public string GetString(string key, params object[] args)
        {
            try
            {
                var localizedString = _localizer[key, args];
                return localizedString.ResourceNotFound ? string.Format(key, args) : localizedString.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting localized string for key: {Key}", key);
                return string.Format(key, args);
            }
        }

        public List<Services.CultureInfo> GetAvailableCultures()
        {
            return new List<Services.CultureInfo>
            {
                new Services.CultureInfo
                {
                    Code = "zh-CN",
                    DisplayName = "简体中文",
                    NativeName = "简体中文"
                },
                new Services.CultureInfo
                {
                    Code = "en-US",
                    DisplayName = "English",
                    NativeName = "English"
                }
            };
        }

        private void SetThreadCulture(string culture)
        {
            try
            {
                var cultureInfo = new System.Globalization.CultureInfo(culture);
                System.Globalization.CultureInfo.CurrentCulture = cultureInfo;
                System.Globalization.CultureInfo.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting thread culture to: {Culture}", culture);
            }
        }
    }

    /// <summary>
    /// Shared resources class for localization
    /// </summary>
    public class SharedResources
    {
    }
}
