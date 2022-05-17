using Contracts.BindingModels;
using Contracts.BusinessLogicsContracts;
using Contracts.StoragesContracts;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogics
{
    public class WorkerGSMLogic : IWorkerGSMLogic
    {
        private readonly IWorkerGSMStorage _workergsmStorage;

        public WorkerGSMLogic(IWorkerGSMStorage workergsmStorage)
        {
            _workergsmStorage = workergsmStorage;
        }

        public List<WorkerGSMViewModel> Read(WorkerGSMBindingModel model)
        {
            if (model == null)
            {
                return _workergsmStorage.GetFullList();
            }

            if (model.GSMId != 0 && model.WorkerId != 0)
            {
                return new List<WorkerGSMViewModel> { _workergsmStorage.GetElement(model) };
            }

            return _workergsmStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(WorkerGSMBindingModel model)
        {
            var element = _workergsmStorage.GetElement(new WorkerGSMBindingModel
            {
                WorkerId = model.WorkerId,
                GSMId = model.GSMId
            });
            if (element != null)
            {
                throw new Exception("Уже есть такая связь");
            }         
            else
            {
                _workergsmStorage.Insert(model);
            }
        }

        public void Delete(WorkerGSMBindingModel model)
        {
            var element = _workergsmStorage.GetElement(new WorkerGSMBindingModel
            {
                WorkerId = model.WorkerId,
                GSMId = model.GSMId
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _workergsmStorage.Delete(model);
        }
    }
}
