using E_commerce.Models.Enums;
namespace E_commerce.Models.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
    public class PlaceOrderDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int ShippingDetailsId { get; set; }
    }
    public class PlaceOrderRequestDto
    {
        public int ShippingDetailsId { get; set; }

        // Add more properties here in the future, e.g.
        // public string PromoCode { get; set; }
        // public PaymentDetailsDto PaymentDetails { get; set; }
    }

}
