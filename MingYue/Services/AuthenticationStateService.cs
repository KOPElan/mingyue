using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MingYue.Models;

namespace MingYue.Services
{
    /// <summary>
    /// Service for managing authentication state across the application
    /// </summary>
    public class AuthenticationStateService
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly ILogger<AuthenticationStateService> _logger;
        private const string UserSessionKey = "CurrentUser";

        public AuthenticationStateService(
            ProtectedSessionStorage sessionStorage,
            ILogger<AuthenticationStateService> logger)
        {
            _sessionStorage = sessionStorage;
            _logger = logger;
        }

        public event Action? OnAuthenticationStateChanged;

        private UserDto? _currentUser;

        /// <summary>
        /// Get the current authenticated user
        /// </summary>
        public UserDto? CurrentUser => _currentUser;

        /// <summary>
        /// Check if user is authenticated
        /// </summary>
        public bool IsAuthenticated => _currentUser != null;

        /// <summary>
        /// Check if user is admin
        /// </summary>
        public bool IsAdmin => _currentUser?.Role == "Admin";

        /// <summary>
        /// Set the current user after successful login
        /// </summary>
        public async Task SetCurrentUserAsync(UserDto user)
        {
            _currentUser = user;
            await _sessionStorage.SetAsync(UserSessionKey, user);
            _logger.LogInformation("User {Username} authenticated", user.Username);
            NotifyAuthenticationStateChanged();
        }

        /// <summary>
        /// Load user from session storage
        /// </summary>
        public async Task<bool> TryRestoreSessionAsync()
        {
            try
            {
                var result = await _sessionStorage.GetAsync<UserDto>(UserSessionKey);
                if (result.Success && result.Value != null)
                {
                    _currentUser = result.Value;
                    _logger.LogInformation("Session restored for user {Username}", _currentUser.Username);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error restoring session");
            }

            return false;
        }

        /// <summary>
        /// Logout current user
        /// </summary>
        public async Task LogoutAsync()
        {
            var username = _currentUser?.Username ?? "Unknown";
            _currentUser = null;
            await _sessionStorage.DeleteAsync(UserSessionKey);
            _logger.LogInformation("User {Username} logged out", username);
            NotifyAuthenticationStateChanged();
        }

        /// <summary>
        /// Notify subscribers that authentication state has changed
        /// </summary>
        private void NotifyAuthenticationStateChanged()
        {
            OnAuthenticationStateChanged?.Invoke();
        }
    }
}
