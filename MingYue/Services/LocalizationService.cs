using Microsoft.Extensions.Localization;
using System.Globalization;

using MingYue.Resources;

namespace MingYue.Services
{
    /// <summary>
    /// Service for managing localization and language settings
    /// </summary>
    public class LocalizationService : ILocalizationService, IDisposable
    {
        private readonly IStringLocalizer<Resources.SharedResources> _localizer;
        private readonly ISystemSettingService _systemSettingService;
        private readonly ILogger<LocalizationService> _logger;
        private volatile string _currentCulture = "zh-CN"; // Default to Chinese, volatile for thread-safe reads

        public event EventHandler? CultureChanged;

        private int _initialized = 0; // Use int for Interlocked operations
        private readonly SemaphoreSlim _initSemaphore = new(1, 1);

        public LocalizationService(
            IStringLocalizer<Resources.SharedResources> localizer,
            ISystemSettingService systemSettingService,
            ILogger<LocalizationService> logger)
        {
            _localizer = localizer;
            _systemSettingService = systemSettingService;
            _logger = logger;
        }

        // Lazy initialization to load saved culture
        private async Task InitializeAsync()
        {
            if (Interlocked.CompareExchange(ref _initialized, 0, 0) == 1) return;

            await _initSemaphore.WaitAsync();
            try
            {
                if (Interlocked.CompareExchange(ref _initialized, 0, 0) == 1) return; // Double-check after acquiring lock

                var savedCulture = await _systemSettingService.GetSettingValueAsync("Language");
                if (!string.IsNullOrEmpty(savedCulture))
                {
                    _currentCulture = savedCulture;
                    SetThreadCulture(_currentCulture);
                }
                Interlocked.Exchange(ref _initialized, 1);
            }
            finally
            {
                _initSemaphore.Release();
            }
        }

        public string GetCurrentCulture()
        {
            // Trigger initialization on first access if needed
            if (Interlocked.CompareExchange(ref _initialized, 0, 0) == 0)
            {
                InitializeAsync().GetAwaiter().GetResult();
            }
            return _currentCulture;
        }

        public async Task SetCultureAsync(string culture, Microsoft.JSInterop.IJSRuntime? jsRuntime = null)
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

            // 设置 Cookie 让 RequestLocalizationMiddleware 能识别
            if (jsRuntime is not null)
            {
                try
                {
                    await jsRuntime.InvokeVoidAsync("blazorCulture.set", culture);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to set .AspNetCore.Culture cookie via JSRuntime");
                }
            }

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

        public void Dispose()
        {
            _initSemaphore?.Dispose();
        }
    }

}
