using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Models.Enums;

namespace E_commerce.Services.Interface
{
    public interface IPaymentService
    {
        //Task<Payment> ProcessPaymentAsync(int userId, int orderId, decimal amount, PaymentMethod method);
        Task<PaymentDTO> ProcessPaymentAsync(int userId, int orderId, decimal amount, PaymentMethod paymentMethod);
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId);
    }

}
