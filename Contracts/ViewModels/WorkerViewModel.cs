using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ViewModels
{
    public class WorkerViewModel
    {
        [DisplayName("ID")]
        public int WorkerId { get; set; }

        [DisplayName("ФИО Рабочего")]
        public string WorkerName { get; set; }

    }
}
