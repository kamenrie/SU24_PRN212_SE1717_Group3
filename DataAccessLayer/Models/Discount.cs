﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("Discount")]
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        
        public string? Name { get; set; }

        public string? Description { get; set; }

        public double? Percent {  get; set; }

        public bool? Validity { get; set; }

        public int? Quantity { get; set; }


    }
}
