using Contracts.BindingModels;
using Contracts.StoragesContracts;
using Contracts.ViewModels;
using DataBaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseImplement.Implements
{
    public class WorkerGSMStorage : IWorkerGSMStorage
    {
        public List<WorkerGSMViewModel> GetFullList()
        {
            using var context = new Database();
            return context.WorkerGSMs
            .Select(CreateModel)
            .ToList();
        }

        public List<WorkerGSMViewModel> GetFilteredList(WorkerGSMBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Database();
            return context.WorkerGSMs
            .Where(rec => rec.WorkerId.Equals(model.WorkerId) || rec.GSMId == model.GSMId)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public WorkerGSMViewModel GetElement(WorkerGSMBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Database();
            var workergsm = context.WorkerGSMs
            .FirstOrDefault(rec => rec.WorkerId == model.WorkerId && rec.GSMId == model.GSMId);
            return workergsm != null ? CreateModel(workergsm) : null;
        }

        public void Insert(WorkerGSMBindingModel model)
        {
            using var context = new Database();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                WorkerGSM workergsm_to_add = CreateModel(model, new WorkerGSM(), context);
                context.WorkerGSMs.Add(workergsm_to_add);
                context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(WorkerGSMBindingModel model)
        {
            using var context = new Database();
            WorkerGSM element = context.WorkerGSMs.FirstOrDefault(rec => rec.WorkerId == model.WorkerId && rec.GSMId == model.GSMId);
            if (element != null)
            {
                context.WorkerGSMs.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static WorkerGSM CreateModel(WorkerGSMBindingModel model, WorkerGSM workergsm, Database context)
        {
            workergsm.GSMId = model.GSMId;
            workergsm.WorkerId = model.WorkerId;
            return workergsm;
        }

        private static WorkerGSMViewModel CreateModel(WorkerGSM workergsm)
        {
            using var context = new Database();
            string nameworker = Convert.ToString(context.Workers.FirstOrDefault(rec => rec.WorkerId == workergsm.WorkerId).WorkerName);
            string namegsm = Convert.ToString(context.GSMs.FirstOrDefault(rec => rec.GSMId == workergsm.GSMId).GSMName);
            return new WorkerGSMViewModel
            {
                WorkerId = workergsm.WorkerId,
                WorkerName = nameworker,
                GSMId = workergsm.GSMId,
                GSMName = namegsm
            };
        }
    }
}
