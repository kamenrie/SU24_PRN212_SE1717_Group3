using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double? Price { get; set; }

        public byte[]? Image { get; set; }

        public bool? Availability { get; set; }

        [ForeignKey("ShopId")]

        public Shop? Shop { get; set; }

        
        [ForeignKey("CategoryId")]

        public Category? Category { get; set; }

        [ForeignKey("StockId")]

        public Stock? Stock { get; set; }
    }
}
