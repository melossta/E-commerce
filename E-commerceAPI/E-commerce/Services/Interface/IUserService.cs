using System.Collections.Generic;
using System.Threading.Tasks;
using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;

namespace E_commerce.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);

        Task<bool> AddUserAsync(User user, List<int> roleIds);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ChangeUserRolesAsync(int userId, List<int> newRoleIds);
    }
}
