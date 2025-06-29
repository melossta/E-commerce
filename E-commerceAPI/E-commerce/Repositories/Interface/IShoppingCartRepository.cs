using E_commerce.Models.Domains;
using System.Threading.Tasks;

namespace E_commerce.Repositories.Interface
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetCartByUserIdAsync(int userId);
        Task AddToCartAsync(int userId, int productId, int quantity);
        Task UpdateCartItemAsync(int cartItemId, int quantity);
        Task RemoveCartItemAsync(int cartItemId);
        Task ClearCartAsync(int userId);
        Task SaveChangesAsync();
    }
}
