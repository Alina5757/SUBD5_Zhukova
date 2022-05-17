using Contracts.BindingModels;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StoragesContracts
{
    public interface IWorkerGSMStorage
    {

        List<WorkerGSMViewModel> GetFullList();

        List<WorkerGSMViewModel> GetFilteredList(WorkerGSMBindingModel model);

        WorkerGSMViewModel GetElement(WorkerGSMBindingModel model);

        void Insert(WorkerGSMBindingModel model);

        void Delete(WorkerGSMBindingModel model);

    }
}
