using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ViewModels
{
    public class SmenaViewModel
    {
        public int WorkerId { get; set; }

        [DisplayName("Рабочий")]
        public string WorkerName { get; set; }

        [DisplayName("Дата")]
        public DateTime SmenaDate { get; set; }

        [DisplayName("Номер смены")]
        public int SmenaNumber { get; set; }
    }
}
