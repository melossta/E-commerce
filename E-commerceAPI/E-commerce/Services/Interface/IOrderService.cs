using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerce.Services.Interface
{
    public interface IOrderService
    {
        Task<OrderDto> PlaceOrderAsync(int userId);
        //Task<Order?> GetOrderByIdAsync(int orderId);
        Task<OrderDto?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId);
        //Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
    }
}
