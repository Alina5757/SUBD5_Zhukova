using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ViewModels
{
    public class GSMViewModel
    {
        [DisplayName("ID")]
        public int GSMId { get; set; }

        [DisplayName("Название ГСМ")]
        public string GSMName { get; set; }

    }
}
