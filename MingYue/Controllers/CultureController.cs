using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MingYue.Controllers
{
    /// <summary>
    /// Controller for handling culture/language changes via cookie
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CultureController : ControllerBase
    {
        private readonly ILogger<CultureController> _logger;
        private readonly RequestLocalizationOptions _localizationOptions;

        public CultureController(
            ILogger<CultureController> logger,
            IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _logger = logger;
            _localizationOptions = localizationOptions.Value;
        }

        /// <summary>
        /// Set the culture cookie for localization
        /// </summary>
        /// <param name="culture">Culture code (e.g., "zh-CN", "en-US")</param>
        /// <param name="redirectUri">URI to redirect to after setting culture</param>
        [HttpGet("set")]
        public IActionResult SetCulture(string culture, string redirectUri = "/")
        {
            if (string.IsNullOrEmpty(culture))
            {
                return BadRequest("Culture is required");
            }

            // Validate culture against configured supported cultures
            var supportedCultures = _localizationOptions.SupportedCultures?
                .Select(c => c.Name)
                .ToArray() ?? Array.Empty<string>();
            
            if (!supportedCultures.Contains(culture))
            {
                return BadRequest($"Culture '{culture}' is not supported");
            }

            // Set the culture cookie
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax,
                    Secure = true
                }
            );

            _logger.LogInformation("Culture set to {Culture}", culture);

            // Redirect to the specified URI
            if (!string.IsNullOrEmpty(redirectUri) && Url.IsLocalUrl(redirectUri))
            {
                return LocalRedirect(redirectUri);
            }

            return LocalRedirect("/");
        }
    }
}
