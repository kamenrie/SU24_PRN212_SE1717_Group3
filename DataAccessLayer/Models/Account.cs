using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        [ForeignKey("ProfileId")]
        public Profile? Profile { get; set; }

        [ForeignKey("ShopId")]
        public Shop? Shop { get; set; }
    }
}
