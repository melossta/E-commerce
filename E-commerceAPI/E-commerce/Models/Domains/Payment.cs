namespace E_commerce.Models.Domains
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int OrderId { get; set; } // Foreign Key
        public Order Order { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = "Credit Card"; // Enum: Credit Card, PayPal, Bank Transfer

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Processing"; // Enum: Processing, Completed, Failed
    }

}
