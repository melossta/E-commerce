//namespace E_commerce.Services.Implementation
//{
//    using E_commerce.Data;
//    using E_commerce.Models.Domains;
//    using E_commerce.Models.DTOs;
//    using E_commerce.Models.JunctionTables;
//    using E_commerce.Services.Interface;
//    using E_commerce.Data;
//    using E_commerce.Models.Domains;
//    using E_commerce.Models.DTOs;
//    //using E_commerce.Repositories.Implementation;
//    using E_commerce.Repositories.Interface;
//    using Microsoft.EntityFrameworkCore;
//    using System.Security.Cryptography;
//    using System.Text;

//    namespace HippAdministrata.Services.Implementation
//    {
//        public class UserService : IUserService
//        {
//            private readonly IUserRepository _userRepository;
//            private readonly ApplicationDbContext _dbContext;

//            public UserService(IUserRepository userRepository, ApplicationDbContext dbContext)
//            {
//                _userRepository = userRepository;
//                _dbContext = dbContext;
//            }

//            public async Task<List<UserDTO>> GetAllUsersAsync()
//            {
//                var users = await _userRepository.GetAllUsersAsync();
//                return users.Select(user => new UserDTO
//                {
//                    UserId = user.UserId,
//                    Name = user.Name,
//                    Email = user.Email,
//                    Roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList()
//                }).ToList();
//            }

//            public async Task<UserDTO?> GetUserByIdAsync(int id)
//            {
//                var user = await _userRepository.GetUserByIdAsync(id);
//                if (user == null) return null;

//                return new UserDTO
//                {
//                    UserId = user.UserId,
//                    Name = user.Name,
//                    Email = user.Email,
//                    Roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList()
//                };
//            }

//            public async Task<bool> AddUserAsync(User model, List<string> roleNames)
//            {
//                var roles = await _dbContext.Roles.Where(r => roleNames.Contains(r.RoleName)).ToListAsync();

//                var user = new User
//                {
//                    Name = model.Name,
//                    PasswordHash = HashPassword(model.PasswordHash),
//                    Email = model.Email,
//                    UserRoles = roles.Select(role => new UserRole { Role = role }).ToList()
//                };

//                await _userRepository.AddUserAsync(user);
//                return true;
//            }

//            public async Task<bool> DeleteUserAsync(int id)
//            {
//                var user = await _userRepository.GetUserByIdAsync(id);
//                if (user == null) return false;

//                await _userRepository.DeleteUserAsync(user);
//                return true;
//            }

//            public async Task<bool> ChangeUserRolesAsync(int userId, List<string> newRoles)
//            {
//                var user = await _dbContext.Users.Include(u => u.UserRoles)
//                                                 .ThenInclude(ur => ur.Role)
//                                                 .FirstOrDefaultAsync(u => u.UserId == userId);
//                if (user == null) return false;

//                var roles = await _dbContext.Roles.Where(r => newRoles.Contains(r.RoleName)).ToListAsync();
//                if (!roles.Any()) return false;

//                if (user.UserRoles == null)
//                {
//                    user.UserRoles = new List<UserRole>();
//                }
//                else
//                {
//                    user.UserRoles.Clear();
//                }

//                //user.UserRoles.AddRange(roles.Select(role => new UserRole { RoleId = role.RoleId, UserId = user.UserId }));
//                user.UserRoles = roles.Select(role => new UserRole { UserId = user.UserId, RoleId = role.RoleId }).ToList();



//                await _userRepository.UpdateUserAsync(user);
//                return true;
//            }

//            private string HashPassword(string password)
//            {
//                using var sha256 = SHA256.Create();
//                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//                return Convert.ToBase64String(hashedBytes);
//            }
//        }
//    }

//}
using E_commerce.Data;
using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Repositories.Interface;
using E_commerce.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using E_commerce.Models.JunctionTables;

namespace E_commerce.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _dbContext;

        public UserService(IUserRepository userRepository, ApplicationDbContext dbContext)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(user => new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList(), // Fetch multiple roles
                Email = user.Email
            }).ToList();
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            return new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList()
            };
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<bool> AddUserAsync(User model, List<int> roleIds)
        {
            var user = new User
            {
                Name = model.Name,
                PasswordHash = model.PasswordHash,
                Email = model.Email,
                UserRoles = roleIds.Select(roleId => new UserRole { RoleId = roleId }).ToList() // Assign multiple roles
            };

            await _userRepository.AddUserAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteUserAsync(user);
            return true;
        }

        public async Task<bool> ChangeUserRolesAsync(int userId, List<int> newRoleIds)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            // Remove old roles
            user.UserRoles.Clear();

            // Assign new roles
            user.UserRoles = newRoleIds.Select(roleId => new UserRole { UserId = userId, RoleId = roleId }).ToList();

            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}

