using System.ComponentModel.DataAnnotations.Schema;

namespace SU24_PRN212_SE1717_Group3.Models
{
    [Table("MyDiscount")]
    public class MyDiscount
    {
       public int? Discountid { get; set; }

        public int? Accountid { get; set; }

        [ForeignKey("Discountid")]
        public Discount? Discount { get; set; }
        [ForeignKey("Accountid")]
        public Account? Account { get; set; }
    }
}
