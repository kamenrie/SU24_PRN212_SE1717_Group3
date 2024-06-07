using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU24_PRN212_SE1717_Group3.Models
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        public int? Quantity { get; set; }

        public DateTime? DateNow { get; set; }

        [ForeignKey("Orderid")]

        public Order? Drder { get; set; }

    }
}
