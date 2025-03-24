using E_commerce.Models.Domains;

namespace E_commerce.Models.Requests
{
    public class RegisterRequest
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
    }
}
