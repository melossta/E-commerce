using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Models.JunctionTables;
using E_commerce.Repositories;
using E_commerce.Repositories.Interface;
using E_commerce.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce.Models.Enums;
using AutoMapper;

namespace E_commerce.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShippingDetailsRepository _shippingDetailsRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IShoppingCartRepository cartRepository, IProductRepository productRepository,IShippingDetailsRepository shippingDetailsRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _shippingDetailsRepository = shippingDetailsRepository;
            _mapper = mapper;
        }

        //public async Task<OrderDto> PlaceOrderAsync(int userId, int shippingDetailsId)
        //{
        //    var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        //    if (cart == null || !cart.CartItems.Any())
        //        throw new InvalidOperationException("Shopping cart is empty.");

        //    // 🔹 Fetch the shipping details by shippingDetailsId
        //    var shippingDetails = await _shippingDetailsRepository.GetByShippingDetailsIdAsync(shippingDetailsId);
        //    if (shippingDetails == null || shippingDetails.UserId != userId)
        //        throw new InvalidOperationException("Invalid or unauthorized shipping details selected.");

        //    // Create Order
        //    var order = new Order
        //    {
        //        UserId = userId,
        //        ShippingDetailsId = shippingDetails.ShippingDetailsId,
        //        OrderDate = DateTime.UtcNow,
        //        Status = OrderStatus.Pending,
        //        TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
        //        OrderItems = cart.CartItems.Select(ci => new OrderItem
        //        {
        //            ProductId = ci.ProductId,
        //            Quantity = ci.Quantity,
        //            UnitPrice = ci.Product.Price
        //        }).ToList()
        //    };

        //    // Deduct stock
        //    foreach (var cartItem in cart.CartItems)
        //    {
        //        var product = await _productRepository.GetProductByIdAsync(cartItem.ProductId);
        //        if (product.StockQuantity < cartItem.Quantity)
        //            throw new InvalidOperationException($"Not enough stock for product {product.Name}");

        //        product.StockQuantity -= cartItem.Quantity;
        //        await _productRepository.UpdateProductAsync(product);
        //    }

        //    // Save Order and clear cart
        //    var createdOrder = await _orderRepository.CreateOrderAsync(order);
        //    await _cartRepository.ClearCartAsync(cart.ShoppingCartId);
        //    // Convert to DTO before returning
        //    return new OrderDto
        //    {
        //        OrderId = createdOrder.OrderId,
        //        TotalAmount = createdOrder.TotalAmount,
        //        Status = createdOrder.Status,
        //        OrderItems = createdOrder.OrderItems.Select(oi => new OrderItemDto
        //        {
        //            OrderItemId = oi.OrderItemId,
        //            ProductId = oi.ProductId,
        //            Quantity = oi.Quantity,
        //            UnitPrice = oi.UnitPrice
        //        }).ToList()
        //    };
        //}
        public async Task<OrderDto> PlaceOrderAsync(int userId, int shippingDetailsId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null || !cart.CartItems.Any())
                throw new InvalidOperationException("Shopping cart is empty.");

            var shippingDetails = await _shippingDetailsRepository.GetByShippingDetailsIdAsync(shippingDetailsId);
            if (shippingDetails == null)
                throw new InvalidOperationException("Shipping details not found.");

            if (shippingDetails.UserId != userId)
                throw new InvalidOperationException("Unauthorized shipping details selected.");

            var order = new Order
            {
                UserId = userId,
                ShippingDetailsId = shippingDetails.ShippingDetailsId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product.Price
                }).ToList()
            };

            foreach (var cartItem in cart.CartItems)
            {
                var product = await _productRepository.GetProductByIdAsync(cartItem.ProductId);
                if (product.StockQuantity < cartItem.Quantity)
                    throw new InvalidOperationException($"Not enough stock for product {product.Name}");

                product.StockQuantity -= cartItem.Quantity;
                await _productRepository.UpdateProductAsync(product);
            }

            var createdOrder = await _orderRepository.CreateOrderAsync(order);
            await _cartRepository.ClearCartAsync(cart.ShoppingCartId);

            return new OrderDto
            {
                OrderId = createdOrder.OrderId,
                TotalAmount = createdOrder.TotalAmount,
                Status = createdOrder.Status,
                OrderItems = createdOrder.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }



        public async Task<OrderDto> PlaceSingleProductOrderAsync(int userId, int productId, int quantity, int shippingDetailsId)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
                throw new ArgumentException("Product not found.");

            if (product.StockQuantity < quantity)
                throw new ArgumentException("Insufficient stock for product.");

            var shippingDetails = await _shippingDetailsRepository.GetByShippingDetailsIdAsync(shippingDetailsId);
            if (shippingDetails == null || shippingDetails.UserId != userId)
                throw new InvalidOperationException("Invalid or unauthorized shipping details selected.");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                ShippingDetailsId = shippingDetails.ShippingDetailsId,
                Status = OrderStatus.Pending,
                TotalAmount = product.Price * quantity,
                OrderItems = new List<OrderItem>
        {
            new OrderItem
            {
                ProductId = product.ProductId,
                Quantity = quantity,
                UnitPrice = product.Price
            }
        }
            };

            // Deduct stock
            product.StockQuantity -= quantity;
            await _productRepository.UpdateProductAsync(product);

            var createdOrder = await _orderRepository.CreateOrderAsync(order);

            // ✅ Use AutoMapper here
            return _mapper.Map<OrderDto>(createdOrder);
        }






        public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }
        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            return orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            }).ToList();
        }


        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) throw new KeyNotFoundException("Order not found.");

            order.Status = status;
            await _orderRepository.UpdateOrderAsync(order);
        }
    }
}
