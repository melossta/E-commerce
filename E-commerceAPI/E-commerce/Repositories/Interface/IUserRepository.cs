using System.Collections.Generic;
using System.Threading.Tasks;
using E_commerce.Models.Domains;

namespace E_commerce.Repositories.Interface
{

    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
    }
}
