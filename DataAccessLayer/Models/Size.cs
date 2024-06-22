using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("Size")]
    public class Size
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
