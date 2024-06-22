using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("Status")]
    public class Status
    {
        [Key]

        public int Id { get; set; }

        public string? Type { get; set; }
    }
}
