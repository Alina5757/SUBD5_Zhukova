using Contracts.BindingModels;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StoragesContracts
{
    public interface IGSMStorage
    {
        List<GSMViewModel> GetFullList();

        List<GSMViewModel> GetFilteredList(GSMBindingModel model);

        GSMViewModel GetElement(GSMBindingModel model);

        void Insert(GSMBindingModel model);

        void Update(GSMBindingModel model);

        void Delete(GSMBindingModel model);
    }
}
