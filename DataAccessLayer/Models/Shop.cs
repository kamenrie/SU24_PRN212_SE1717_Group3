using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("Shop")]
    public class Shop
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
