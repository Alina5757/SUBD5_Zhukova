using Contracts.BindingModels;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessLogicsContracts
{
    public interface IWorkerLogic
    {
        List<WorkerViewModel> Read(WorkerBindingModel model);

        void CreateOrUpdate(WorkerBindingModel model);

        void Delete(WorkerBindingModel model);
    }
}
