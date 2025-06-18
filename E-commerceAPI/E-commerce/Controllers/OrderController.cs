using E_commerce.Models.Domains;
using E_commerce.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using E_commerce.Models.Enums;
using E_commerce.Repositories.Interface;
using E_commerce.Models.DTOs;
namespace E_commerce.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        private readonly IUserRepository _userRepository;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger, IUserRepository userRepository)
        {
            _orderService = orderService;
            _logger = logger;
            _userRepository = userRepository;
        }


        [HttpPost("place-order")]
        [Authorize]
        public async Task<IActionResult> PlaceOrder([FromQuery] int shippingDetailsId)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue("UserId"));
                var order = await _orderService.PlaceOrderAsync(userId, shippingDetailsId);
                return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error placing order");
                return StatusCode(500, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }


        //[HttpPost("place-single")]
        //[Authorize]
        //public async Task<IActionResult> PlaceSingleProductOrder(int userId, int productId, int quantity, int shippingDetailsId)
        //{
        //    var userExists = await _userRepository.UserExistsAsync(userId);
        //    if (!userExists)
        //        return NotFound("User not found");

        //    try
        //    {
        //        var order = await _orderService.PlaceSingleProductOrderAsync(userId, productId, quantity, shippingDetailsId);
        //        return Ok(order);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while placing the order.");
        //    }
        //}

        [HttpPost("place-single")]
        [Authorize]
        public async Task<IActionResult> PlaceSingleProductOrder( int productId, int quantity, int shippingDetailsId)
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));

            try
            {
                var order = await _orderService.PlaceSingleProductOrderAsync(userId, productId, quantity, shippingDetailsId);
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while placing the order.");
            }
        }


        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetOrdersByUserId()
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] OrderStatus status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            return NoContent();
        }
    }
}
