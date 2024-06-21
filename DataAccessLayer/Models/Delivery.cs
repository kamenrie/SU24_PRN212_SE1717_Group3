using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("Delivery")]
    public class Delivery
    {
        [Key]
        public int Id { get; set; }

        public string? Type { get; set; }

        public double? Price { get; set; }

        public int? Day { get; set; }
    }
}
