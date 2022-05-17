using System;
using System.ComponentModel.DataAnnotations;

namespace DataBaseImplement.Models
{
    public class Sell
    {
        [Key]
        public int SellId { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [Required]
        public int GSMId { get; set; }

        [Required]
        public double SellValue { get; set; }

        [Required]
        public DateTime SellDate { get; set; }

        public virtual GSM GSM { get; set; }
        public virtual Worker Worker { get; set; } 
    }
}
