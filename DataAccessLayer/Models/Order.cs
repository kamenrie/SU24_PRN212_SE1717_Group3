using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]

        public int Id { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "numeric(18, 2)")]
        public double? Total { get; set; }

        public DateTime? OrderDate { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        [ForeignKey("AccountId")]
        public Account? Account { get; set; }

        [ForeignKey("DiscountId")]
        public Discount? Discount { get; set; }

        [ForeignKey("ShippingId")]
        public ShippingInformation? ShippingInformation { get; set; }
    }
}
