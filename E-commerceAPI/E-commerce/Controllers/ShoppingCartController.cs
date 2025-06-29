using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Repositories.Interface;
using E_commerce.Services.Implementation;
using E_commerce.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IUserRepository _userRepository;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IUserRepository userRepository)
        {
            _shoppingCartService = shoppingCartService;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));
            var cart = await _shoppingCartService.GetCartAsync(userId);
            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            var cartDto = new CartDto
            {
                UserId = cart.UserId,
                CartItems = cart.CartItems.Select(ci => new CartItemDto
                {
                    CartItemId = ci.CartItemId,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product?.Name,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.TotalPrice // Map TotalPrice
                                               // Map other Product properties if needed
                }).ToList()
            };

            return Ok(cartDto);
        }

        //[HttpGet("{userId}")]
        //public async Task<IActionResult> GetCartByUserId(int userId)
        //{
        //    var cart = await _shoppingCartService.GetCartAsync(userId);
        //    var userExists = await _userRepository.UserExistsAsync(userId);
        //    if (cart == null)
        //    {
        //        return NotFound("Cart not found");
        //    }
        //    else if (!userExists)
        //    {
        //       return NotFound("User not found");
        //    }

        //    var cartDto = new CartDto
        //    {
        //        UserId = cart.UserId,
        //        CartItems = cart.CartItems.Select(ci => new CartItemDto
        //        {
        //            CartItemId = ci.CartItemId,
        //            ProductId = ci.ProductId,
        //            ProductName = ci.Product?.Name,
        //            Quantity = ci.Quantity,
        //            TotalPrice = ci.TotalPrice // Map TotalPrice
        //                                       // Map other Product properties if needed
        //        }).ToList()
        //    };

        //    return Ok(cartDto);
        //}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            var userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                return NotFound("User not found");
            }

            var cart = await _shoppingCartService.GetCartAsync(userId);
            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            var cartDto = new CartDto
            {
                UserId = cart.UserId,
                CartItems = cart.CartItems.Select(ci => new CartItemDto
                {
                    CartItemId = ci.CartItemId,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product?.Name,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.TotalPrice
                }).ToList()
            };

            return Ok(cartDto); // Even if CartItems is empty, return 200
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO request)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (userIdClaim == null) return Unauthorized("User ID missing from token.");
            int userId = int.Parse(userIdClaim);

            try
            {
                await _shoppingCartService.AddToCartAsync(userId, request.ProductId, request.Quantity);
                return Ok(new { message = "Item added to cart" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("add")]
        //public async Task<IActionResult> AddToCart( int productId, int quantity)
        //{
        //    var userId = int.Parse(User.FindFirstValue("UserId"));
        //    var userExists = await _userRepository.UserExistsAsync(userId);
        //    if(!userExists)
        //    {
        //        return NotFound("User not found");
        //    }    

        //    try
        //    {
        //        await _shoppingCartService.AddToCartAsync(userId, productId, quantity);
        //        return Ok(new {message="Item Added to the Cart"});

        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpPost("add")]
        //public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        //{
        //    try
        //    {
        //        // Check if the user exists
        //        var userExists = await _userService.UserExistsAsync(userId);
        //        if (!userExists)
        //        {
        //            return NotFound("User  not found");
        //        }

        //        // Proceed to add the item to the cart
        //        await _shoppingCartService.AddToCartAsync(userId, productId, quantity);
        //        return Ok("Item added to cart");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut("update/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, int quantity)
        {
            try
            {
                await _shoppingCartService.UpdateCartItemAsync(cartItemId, quantity);
                return Ok(new { message = "Shopping cart updated successfully." });
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
            return Ok(new { message = "Cart item removed." });
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _shoppingCartService.ClearCartAsync(userId);
            return Ok(new { message = "Cart Cleared" });
        }
    }
}
