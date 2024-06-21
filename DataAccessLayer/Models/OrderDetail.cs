using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int SizeId { get; set; }

        [ForeignKey("SizeId")]
        public Size? Size {  get; set; } 

        [ForeignKey("OrderId")]
        public  Order? Order { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        public int? Amount { get; set; }

        public double? Subtotal { get; set; }

    }
}
