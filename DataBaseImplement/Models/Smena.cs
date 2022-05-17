using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataBaseImplement.Models
{
    public class Smena
    {
        [Required]
        public DateTime SmenaDate { get; set; }

        [Required]
        public int SmenaNumber { get; set; }

        [Required]
        public int WorkerId { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
