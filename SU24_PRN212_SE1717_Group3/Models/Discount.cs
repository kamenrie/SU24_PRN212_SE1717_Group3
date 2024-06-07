﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU24_PRN212_SE1717_Group3.Models
{
    [Table("Discount")]
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        
        public string ? Name { get; set; }

        public string ? Description { get; set; }

        public string? Precent {  get; set; }

        public Boolean ? Validity { get; set; }

        public int  ? Quantity { get; set; }


    }
}
