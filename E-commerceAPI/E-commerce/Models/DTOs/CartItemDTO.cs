namespace E_commerce.Models.DTOs
{
    public class CartDto
    {
        public int UserId { get; set; } // Or ShoppingCartId, depending on your needs
        public List<CartItemDto> CartItems { get; set; }
    }
    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; } 
    }
}
