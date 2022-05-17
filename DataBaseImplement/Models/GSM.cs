using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseImplement.Models
{
    public class GSM
    {
        [Key]
        public int GSMId { get; set; }

        [Required]
        public string GSMName { get; set; }


        [ForeignKey("GSMId")]
        public virtual List<WorkerGSM> WorkerGSMs { get; set; }

        [ForeignKey("GSMId")]
        public virtual List<Sell> Sells { get; set; }
    }
}
