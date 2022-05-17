using Contracts.BindingModels;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessLogicsContracts
{
    public interface IWorkerGSMLogic
    {
        List<WorkerGSMViewModel> Read(WorkerGSMBindingModel model);

        void CreateOrUpdate(WorkerGSMBindingModel model);

        void Delete(WorkerGSMBindingModel model);
    }
}
