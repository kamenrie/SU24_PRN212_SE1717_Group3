using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("MyDiscount")]
    public class MyDiscount
    {
       public int? DiscountId { get; set; }

        public int? AccountId { get; set; }

        [ForeignKey("DiscountId")]
        public Discount? Discount { get; set; }
        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
    }
}
