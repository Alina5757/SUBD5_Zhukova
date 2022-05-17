using System;
using Contracts.BindingModels;
using Contracts.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessLogicsContracts
{
    public interface ISmenaLogic
    {
        List<SmenaViewModel> Read(SmenaBindingModel model);

        void CreateOrUpdate(SmenaBindingModel model);

        void Delete(SmenaBindingModel model);
    }
}
