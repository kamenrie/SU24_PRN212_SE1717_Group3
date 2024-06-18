using System.ComponentModel.DataAnnotations.Schema;

namespace SU24_PRN212_SE1717_Group3.Models
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
