using System.ComponentModel.DataAnnotations;
using E_commerce.Models.JunctionTables;

namespace E_commerce.Models.Domains
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }


}
