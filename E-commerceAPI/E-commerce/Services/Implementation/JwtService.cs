using E_commerce.Models.Domains;
using E_commerce.Models.Responses;
using E_commerce.Models.Requests;
using E_commerce.Services.Interface;
using E_commerce.Repositories.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using E_commerce.Data;
using E_commerce.Models.JunctionTables;

namespace E_commerce.Services.Implementation
{
    public class JwtService : IJwtService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtService> _logger;
        private readonly ApplicationDbContext _dbContext;
        public JwtService(IUserRepository userRepository, IConfiguration configuration, ILogger<JwtService> logger, ApplicationDbContext dbContext)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
            _dbContext = dbContext;
        }

        public string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList(); // Fetch roles

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.UserId.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // Add each role as a claim
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<LoginResponse> AuthenticateAsync(LoginRequest request)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(request.Email);

                if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                    return null;

                // Get all role names for the user
                var roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList();

                var token = GenerateJwtToken(user);

                return new LoginResponse { Token = token, Roles = roles }; // Return roles as a list of role names
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during authentication.");
                throw;
            }
        }


        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            try
            {
                // Check if a user with the same email already exists
                if (await _userRepository.GetUserByEmailAsync(request.Email) != null)
                    return false;

                // Fetch the role from the database based on role name
                var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == request.Role);
                if (role == null)
                    throw new ArgumentException($"Role '{request.Role}' does not exist.");

                // Create a new user object
                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    PasswordHash = HashPassword(request.Password),
                    UserRoles = new List<UserRole>
            {
                new UserRole
                {
                    Role = role // Assign the role to the UserRoles collection
                }
            }
                };

                // Add the user to the repository
                await _userRepository.AddUserAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during registration.");
                throw;
            }
        }


        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hashOfInput = HashPassword(password);
            return string.Equals(hashOfInput, storedHash);
        }
    }
}
