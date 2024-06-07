using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU24_PRN212_SE1717_Group3.Models
{
    [Table("Delivery")]
    public class Delivery
    {
        [Key]
        public int ID { get; set; }

        public string? Type { get; set; }

        public double? Price { get; set; }

        public DateTime? Date { get; set; }
    }
}
