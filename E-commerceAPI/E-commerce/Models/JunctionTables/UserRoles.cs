using E_commerce.Models.Domains;

namespace E_commerce.Models.JunctionTables
{
    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }

}
