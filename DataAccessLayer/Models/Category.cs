using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU24_PRN212_SE1717_Group3.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]

        public int Id { get; set; }

        public string? Name { get; set; }

      
    }
}
