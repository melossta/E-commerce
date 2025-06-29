using E_commerce.Models.Enums;

namespace E_commerce.Models.DTOs
{
    public class ProcessPaymentRequestDTO
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }

        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; } // Updated to use the enum
    }
}
