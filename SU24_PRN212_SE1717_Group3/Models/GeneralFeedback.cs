using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU24_PRN212_SE1717_Group3.Models
{
    [Table("GeneralFeedback")]
    public class GeneralFeedback
    {
        [Key]
        public int Id { get; set; }

        public string ? FeedbackText { get; set; }

        public string? ResponseText { get; set; }

        public DateTime? FeedbackDate { get; set; }

        public DateTime? ResponseDate { get; set; }

        public bool? Ignored { get; set; }

        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
    }
}
