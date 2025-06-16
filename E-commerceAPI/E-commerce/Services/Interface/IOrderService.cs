using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerce.Services.Interface
{
    public interface IOrderService
    {
        
        Task<OrderDto> PlaceSingleProductOrderAsync(int userId, int productId, int quantity, int shippingDetailsId);

        Task<OrderDto> PlaceOrderAsync(int userId, int shippingDetailsId);
        Task<OrderDto?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);


    }
}
