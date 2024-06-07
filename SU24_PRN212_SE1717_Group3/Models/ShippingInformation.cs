using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU24_PRN212_SE1717_Group3.Models
{
    [Table("ShippingInformation")]
    public class ShippingInformation
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? RecipientName { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [ForeignKey("DeliveryId")]
        public Delivery? Delivery { get; set; }
    }
}
