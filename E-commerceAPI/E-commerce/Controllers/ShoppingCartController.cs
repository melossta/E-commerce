using E_commerce.Models.Domains;
using E_commerce.Services.Implementation;
using E_commerce.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _shoppingCartService.GetCartAsync(userId);
            return cart == null ? NotFound("Cart not found") : Ok(cart);
        }

        //[HttpPost("add")]
        //public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        //{
        //    try
        //    {
        //        await _shoppingCartService.AddToCartAsync(userId, productId, quantity);
        //        return Ok("Item added to cart");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        {
            try
            {
                // Check if the user exists
                var userExists = await _userService.UserExistsAsync(userId);
                if (!userExists)
                {
                    return NotFound("User  not found");
                }

                // Proceed to add the item to the cart
                await _shoppingCartService.AddToCartAsync(userId, productId, quantity);
                return Ok("Item added to cart");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, int quantity)
        {
            try
            {
                await _shoppingCartService.UpdateCartItemAsync(cartItemId, quantity);
                return Ok("Cart item updated");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            await _shoppingCartService.RemoveCartItemAsync(cartItemId);
            return Ok("Cart item removed");
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _shoppingCartService.ClearCartAsync(userId);
            return Ok("Cart cleared");
        }
    }
}
