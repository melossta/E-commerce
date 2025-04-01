using E_commerce.Models.Domains;

namespace E_commerce.Repositories.Interface
{
    public interface IPaymentRepository
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId);
        Task UpdatePaymentAsync(Payment payment);
    }

}
