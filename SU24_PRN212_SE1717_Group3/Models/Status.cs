using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU24_PRN212_SE1717_Group3.Models
{
    [Table("Status")]
    public class Status
    {
        [Key]

        public int Id { get; set; }

        public string? Type { get; set; }
    }
}
