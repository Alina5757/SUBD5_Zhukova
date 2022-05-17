using System.ComponentModel.DataAnnotations;

namespace DataBaseImplement.Models
{
    public class WorkerGSM
    {
        [Required]
        public int WorkerId { get; set; }

        [Required]
        public int GSMId { get; set; }

        public virtual Worker Worker { get; set; }
        public virtual GSM GSM { get; set; }
    }
}
