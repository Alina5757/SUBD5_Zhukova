using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ViewModels
{
    public class SellViewModel
    {
        [DisplayName("ID продажи")]
        public int SellId { get; set; }

        public int GSMId { get; set; }
        public int WorkerId { get; set; }

        [DisplayName("Рабочий")]
        public string WorkerName { get; set; }

        [DisplayName("ГСМ")]
        public string GSMName { get; set; }

        [DisplayName("Объем")]
        public double SellValue { get; set; }

        [DisplayName("Дата")]
        public DateTime SellDate { get; set; }
    }
}
