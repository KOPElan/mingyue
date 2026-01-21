using MingYue.Models;

namespace MingYue.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> RegisterAsync(RegisterRequest request);
        Task<AuthenticationResponse> LoginAsync(LoginRequest request);
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> HasUsersAsync();
        Task<bool> UpdateLastLoginAsync(int userId);
    }
}
