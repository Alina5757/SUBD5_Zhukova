using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ViewModels
{
    public class WorkerGSMViewModel
    {
        [DisplayName("ID Рабочего")]
        public int WorkerId { get; set; }

        [DisplayName("ФИО Рабочего")]
        public string WorkerName { get; set; }

        [DisplayName("ID ГСМ")]
        public int GSMId { get; set; }

        [DisplayName("Название ГСМ")]
        public string GSMName { get; set; }
    }
}
