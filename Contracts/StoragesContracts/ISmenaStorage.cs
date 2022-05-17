using Contracts.BindingModels;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StoragesContracts
{
    public interface ISmenaStorage
    {
        List<SmenaViewModel> GetFullList();

        List<SmenaViewModel> GetFilteredList(SmenaBindingModel model);

        SmenaViewModel GetElement(SmenaBindingModel model);

        void Insert(SmenaBindingModel model);

        void Delete(SmenaBindingModel model);
    }
}
