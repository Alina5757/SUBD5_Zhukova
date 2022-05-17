using Contracts.BindingModels;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StoragesContracts
{
    public interface IWorkerStorage
    {
        List<WorkerViewModel> GetFullList();

        List<WorkerViewModel> GetFilteredList(WorkerBindingModel model);

        WorkerViewModel GetElement(WorkerBindingModel model);

        void Insert(WorkerBindingModel model);

        void Update(WorkerBindingModel model);

        void Delete(WorkerBindingModel model);
    }
}
