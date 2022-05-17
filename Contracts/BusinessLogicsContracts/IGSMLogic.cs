using Contracts.BindingModels;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessLogicsContracts
{
    public interface IGSMLogic
    {
        List<GSMViewModel> Read(GSMBindingModel model);

        void CreateOrUpdate(GSMBindingModel model);

        void Delete(GSMBindingModel model);
    }
}
