namespace E_commerce.Models.DTOs
{
    public class ShippingDetailsDto
    {
        public int ShippingDetailsId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
    }

}
