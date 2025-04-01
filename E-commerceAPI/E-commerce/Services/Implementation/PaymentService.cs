using E_commerce.Models.Domains;
using E_commerce.Models.Enums;
using E_commerce.Repositories.Interface;
using E_commerce.Services.Interface;

namespace E_commerce.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Payment> ProcessPaymentAsync(int userId, int orderId, decimal amount, PaymentMethod method)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null || order.UserId != userId)
                throw new InvalidOperationException("Invalid order.");

            if (order.TotalAmount != amount)
                throw new InvalidOperationException("Payment amount mismatch.");

            var payment = new Payment
            {
                UserId = userId,
                OrderId = orderId,
                Amount = amount,
                PaymentMethod = method,
                PaymentStatus = PaymentStatus.Pending,
                PaymentDate = DateTime.UtcNow
            };

            var createdPayment = await _paymentRepository.CreatePaymentAsync(payment);

            // Optional: Update order status if payment is successful
            order.Status = OrderStatus.Processing;
            await _orderRepository.UpdateOrderAsync(order);

            return createdPayment;
        }

        public async Task<Payment?> GetPaymentByIdAsync(int paymentId)
        {
            return await _paymentRepository.GetPaymentByIdAsync(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId)
        {
            return await _paymentRepository.GetPaymentsByUserIdAsync(userId);
        }
    }

}
