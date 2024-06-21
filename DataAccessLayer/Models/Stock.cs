using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        public int? Quantity { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastEditedDate { get; set; }

       

    }
}
