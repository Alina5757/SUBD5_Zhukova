using System;
using Contracts.BindingModels;
using Contracts.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessLogicsContracts
{
    public interface ISellLogic
    {
        List<SellViewModel> Read(SellBindingModel model);

        void CreateOrUpdate(SellBindingModel model);

        void Delete(SellBindingModel model);
    }
}
