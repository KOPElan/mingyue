using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

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

        public CultureController(ILogger<CultureController> logger)
        {
            _logger = logger;
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

            // Validate culture
            var supportedCultures = new[] { "zh-CN", "en-US" };
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
                    SameSite = SameSiteMode.Lax
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
