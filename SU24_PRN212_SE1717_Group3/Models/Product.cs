using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SU24_PRN212_SE1717_Group3.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string ? Name { get; set; }

        public string ? Description { get; set; }

        public double ? Price { get; set; }

        public DateTime ? CreatedDate { get; set; }

        public int ? Quantity { get; set; }

        public int ? Size { get; set; }   

        [ForeignKey("ShopId")]

        public Shop ? Shop { get; set; }

        
        [ForeignKey("CategoryId")]

        public Category ? Category { get; set; }
        
    }
}
