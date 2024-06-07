using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU24_PRN212_SE1717_Group3.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]

        public int Roleid { get; set; }

        public string? Name { get; set; }


    }
}
