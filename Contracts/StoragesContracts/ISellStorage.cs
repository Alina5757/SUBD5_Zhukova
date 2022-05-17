using Contracts.BindingModels;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StoragesContracts
{
    public interface ISellStorage
    {
        List<SellViewModel> GetFullList();

        List<SellViewModel> GetFilteredList(SellBindingModel model);

        SellViewModel GetElement(SellBindingModel model);

        void Insert(SellBindingModel model);

        void Delete(SellBindingModel model);
    }
}
