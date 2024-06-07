using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU24_PRN212_SE1717_Group3.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        public  Order? Order { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        public int ? Amount { get; set; }

        public double ? Subtotal { get; set; }

    }
}
