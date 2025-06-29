namespace E_commerce.Models.DTOs
{
    public class ShippingDetailsCreateDTO
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
    }
}
