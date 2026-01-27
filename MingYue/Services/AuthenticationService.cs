using Microsoft.EntityFrameworkCore;
using MingYue.Data;
using MingYue.Models;
using BCrypt.Net;

namespace MingYue.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDbContextFactory<MingYueDbContext> _dbFactory;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly int _bcryptWorkFactor;

        public AuthenticationService(
            IDbContextFactory<MingYueDbContext> dbFactory,
            ILogger<AuthenticationService> logger,
            IConfiguration configuration)
        {
            _dbFactory = dbFactory;
            _logger = logger;
            _configuration = configuration;
            _bcryptWorkFactor = _configuration.GetValue<int>("Security:BCryptWorkFactor", 12);
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        public async Task<AuthenticationResponse> RegisterAsync(RegisterRequest request)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();

                // Use database transaction to prevent race condition for first admin user
                using var transaction = await context.Database.BeginTransactionAsync();

                try
                {
                    // Check if username already exists
                    var existingUser = await context.Users
                        .FirstOrDefaultAsync(u => u.Username == request.Username);

                    if (existingUser != null)
                    {
                        await transaction.RollbackAsync();
                        return new AuthenticationResponse
                        {
                            Success = false,
                            Message = "Username already exists"
                        };
                    }

                    // Hash password using BCrypt with configurable work factor
                    var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: _bcryptWorkFactor);

                    // Determine role (first user is admin) - within transaction to prevent race condition
                    var hasUsers = await context.Users.AnyAsync();
                    var role = hasUsers ? "User" : "Admin";

                    var user = new User
                    {
                        Username = request.Username,
                        PasswordHash = passwordHash,
                        Role = role,
                        CreatedAt = DateTime.UtcNow
                    };

                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    _logger.LogInformation("User {Username} registered successfully with role {Role}", 
                        request.Username, role);

                    return new AuthenticationResponse
                    {
                        Success = true,
                        Message = "User registered successfully",
                        User = new UserDto
                        {
                            Id = user.Id,
                            Username = user.Username,
                            Role = user.Role,
                            CreatedAt = user.CreatedAt,
                            LastLoginAt = user.LastLoginAt
                        }
                    };
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user {Username}", request.Username);
                return new AuthenticationResponse
                {
                    Success = false,
                    Message = "An error occurred during registration"
                };
            }
        }

        /// <summary>
        /// Login an existing user
        /// </summary>
        public async Task<AuthenticationResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();

                var user = await context.Users
                    .FirstOrDefaultAsync(u => u.Username == request.Username);

                if (user == null)
                {
                    _logger.LogWarning("Login attempt with non-existent username: {Username}", 
                        request.Username);
                    return new AuthenticationResponse
                    {
                        Success = false,
                        Message = "Invalid username or password"
                    };
                }

                // Verify password using BCrypt
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    _logger.LogWarning("Failed login attempt for user: {Username}", request.Username);
                    return new AuthenticationResponse
                    {
                        Success = false,
                        Message = "Invalid username or password"
                    };
                }

                // Update last login time
                user.LastLoginAt = DateTime.UtcNow;
                await context.SaveChangesAsync();

                _logger.LogInformation("User {Username} logged in successfully", request.Username);

                return new AuthenticationResponse
                {
                    Success = true,
                    Message = "Login successful",
                    User = new UserDto
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Role = user.Role,
                        CreatedAt = user.CreatedAt,
                        LastLoginAt = user.LastLoginAt
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for user {Username}", request.Username);
                return new AuthenticationResponse
                {
                    Success = false,
                    Message = "An error occurred during login"
                };
            }
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                return await context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by ID {UserId}", userId);
                return null;
            }
        }

        /// <summary>
        /// Get user by username
        /// </summary>
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                return await context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Username == username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by username {Username}", username);
                return null;
            }
        }

        /// <summary>
        /// Check if any users exist in the database
        /// </summary>
        public async Task<bool> HasUsersAsync()
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                return await context.Users.AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if users exist");
                return false;
            }
        }

        /// <summary>
        /// Update last login time for a user
        /// </summary>
        public async Task<bool> UpdateLastLoginAsync(int userId)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                var user = await context.Users.FindAsync(userId);
                
                if (user == null)
                    return false;

                user.LastLoginAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating last login for user {UserId}", userId);
                return false;
            }
        }

        /// <summary>
        /// Retrieves all users in the system
        /// </summary>
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                return await context.Users
                    .AsNoTracking()
                    .OrderByDescending(u => u.CreatedAt)
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        Username = u.Username,
                        Role = u.Role,
                        CreatedAt = u.CreatedAt,
                        LastLoginAt = u.LastLoginAt
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                return new List<UserDto>();
            }
        }

        /// <summary>
        /// Updates a user's role
        /// </summary>
        public async Task<bool> UpdateUserRoleAsync(int userId, string newRole)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                var user = await context.Users.FindAsync(userId);

                if (user == null)
                    return false;

                user.Role = newRole;
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating role for user {UserId}", userId);
                return false;
            }
        }

        /// <summary>
        /// Deletes a user by their unique identifier
        /// </summary>
        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                await using var context = await _dbFactory.CreateDbContextAsync();
                var user = await context.Users.FindAsync(userId);

                if (user == null)
                    return false;

                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {UserId}", userId);
                return false;
            }
        }
    }
}
