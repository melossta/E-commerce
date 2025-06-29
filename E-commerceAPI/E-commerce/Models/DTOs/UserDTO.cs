namespace E_commerce.Models.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; } = new List<string>(); // Multiple roles
        public string Email { get; set; }
    }

}
