using E_commerce.Models.Enums;

namespace E_commerce.Models.DTOs
{
    //public class PaymentDTO
    //{
    //    public int PaymentId { get; set; }
    //    public int OrderId { get; set; }
    //    public int UserId { get; set; }  // ✅ Kept only UserId for efficiency
    //    public decimal Amount { get; set; }
    //    public PaymentMethod PaymentMethod { get; set; } // ✅ Enum for payment method
    //    public PaymentStatus PaymentStatus { get; set; } // ✅ Only payment-related status
    //    public DateTime PaymentDate { get; set; }
    //}
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }

        // Safe payment details
        public string LastFourDigits { get; set; }
        public string BillingAddress { get; set; }
        public string TransactionId { get; set; }
    }


}
