using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseImplement.Models
{
    public class Worker
    {
        [Key]
        public int WorkerId { get; set; }

        [Required]
        public string WorkerName { get; set; }


        [ForeignKey("WorkerId")]
        public virtual List<WorkerGSM> WorkerGSMs { get; set; }

        [ForeignKey("WorkerId")]
        public virtual List<Smena> Smenas { get; set; }

        [ForeignKey("WorkerId")]
        public virtual List<Sell> Sells { get; set; }
    }
}
