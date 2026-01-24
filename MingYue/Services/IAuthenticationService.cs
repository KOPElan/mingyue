using MingYue.Models;

namespace MingYue.Services
{
    /// <summary>
    /// Provides authentication and user management services
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Registers a new user with the provided credentials
        /// </summary>
        /// <param name="request">The registration request containing username and password</param>
        /// <returns>An authentication response indicating success or failure</returns>
        Task<AuthenticationResponse> RegisterAsync(RegisterRequest request);

        /// <summary>
        /// Authenticates a user with the provided credentials
        /// </summary>
        /// <param name="request">The login request containing username and password</param>
        /// <returns>An authentication response with user details if successful</returns>
        Task<AuthenticationResponse> LoginAsync(LoginRequest request);

        /// <summary>
        /// Retrieves a user by their unique identifier
        /// </summary>
        /// <param name="userId">The user's unique identifier</param>
        /// <returns>The user if found; otherwise, null</returns>
        Task<User?> GetUserByIdAsync(int userId);

        /// <summary>
        /// Retrieves a user by their username
        /// </summary>
        /// <param name="username">The username to search for</param>
        /// <returns>The user if found; otherwise, null</returns>
        Task<User?> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Checks if any users exist in the system
        /// </summary>
        /// <returns>True if at least one user exists; otherwise, false</returns>
        Task<bool> HasUsersAsync();

        /// <summary>
        /// Updates the last login timestamp for a user
        /// </summary>
        /// <param name="userId">The user's unique identifier</param>
        /// <returns>True if the update was successful; otherwise, false</returns>
        Task<bool> UpdateLastLoginAsync(int userId);
    }
}
