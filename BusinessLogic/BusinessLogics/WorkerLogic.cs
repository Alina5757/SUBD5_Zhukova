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
    public class WorkerLogic : IWorkerLogic
    {
        private readonly IWorkerStorage _workerStorage;

        public WorkerLogic(IWorkerStorage workerStorage)
        {
            _workerStorage = workerStorage;
        }

        public List<WorkerViewModel> Read(WorkerBindingModel model)
        {
            if (model == null)
            {
                return _workerStorage.GetFullList();
            }

            if (model.WorkerId != null && model.WorkerId != 0)
            {
                return new List<WorkerViewModel> { _workerStorage.GetElement(model) };
            }

            return _workerStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(WorkerBindingModel model)
        {
            var element = _workerStorage.GetElement(new WorkerBindingModel
            {
                WorkerName = model.WorkerName
            });
            if (element != null && element.WorkerId != model.WorkerId)
            {
                throw new Exception("Уже есть работник с таким ФИО");
            }
            if (model.WorkerId != null && model.WorkerId != 0)
            {
                _workerStorage.Update(model);
            }
            else
            {
                _workerStorage.Insert(model);
            }
        }

        public void Delete(WorkerBindingModel model)
        {
            var element = _workerStorage.GetElement(new WorkerBindingModel
            {
                WorkerId = model.WorkerId
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _workerStorage.Delete(model);
        }
    }
}
