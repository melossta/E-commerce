using E_commerce.Models.Domains;
using E_commerce.Repositories.Interface;
using E_commerce.Services.Interface;
using System;
using System.Threading.Tasks;

namespace E_commerce.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCart> GetCartAsync(int userId)
        {
            return await _shoppingCartRepository.GetCartByUserIdAsync(userId);
        }

        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            await _shoppingCartRepository.AddToCartAsync(userId, productId, quantity);
        }

        public async Task UpdateCartItemAsync(int cartItemId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            await _shoppingCartRepository.UpdateCartItemAsync(cartItemId, quantity);
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            await _shoppingCartRepository.RemoveCartItemAsync(cartItemId);
        }

        public async Task ClearCartAsync(int userId)
        {
            await _shoppingCartRepository.ClearCartAsync(userId);
        }
    }
}
