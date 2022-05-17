using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BindingModels
{
    public class SellBindingModel
    {
        public int SellId { get; set; }
        public int WorkerId { get; set; }
        public int GSMId { get; set; }
        public double SellValue { get; set; }
        public DateTime SellDate { get; set; }
    }
}
