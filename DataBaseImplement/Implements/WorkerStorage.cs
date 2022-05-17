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
    public class WorkerStorage : IWorkerStorage
    {
        public List<WorkerViewModel> GetFullList()
        {
            using var context = new Database();
            return context.Workers
            .Select(CreateModel)
            .ToList();
        }

        public List<WorkerViewModel> GetFilteredList(WorkerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Database();
            return context.Workers
            .Where(rec => rec.WorkerName.Equals(model.WorkerName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public WorkerViewModel GetElement(WorkerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Database();
            var worker = context.Workers
            .FirstOrDefault(rec => rec.WorkerId == model.WorkerId);
            return worker != null ? CreateModel(worker) : null;
        }

        public void Insert(WorkerBindingModel model)
        {
            using var context = new Database();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Worker worker_to_add = CreateModel(model, new Worker(), context);
                context.Workers.Add(worker_to_add);
                context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        public void Update(WorkerBindingModel model)
        {
            using var context = new Database();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Workers.FirstOrDefault(rec => rec.WorkerId == model.WorkerId);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        public void Delete(WorkerBindingModel model)
        {
            using var context = new Database();
            Worker element = context.Workers.FirstOrDefault(rec => rec.WorkerId == model.WorkerId);
            if (element != null)
            {
                context.Workers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static Worker CreateModel(WorkerBindingModel model, Worker worker, Database context)
        {
            worker.WorkerName = model.WorkerName;
            return worker;
        }

        private static WorkerViewModel CreateModel(Worker worker)
        {
            using var context = new Database();
            return new WorkerViewModel
            {
                WorkerId = worker.WorkerId,
                WorkerName = worker.WorkerName
            };
        }
    }
}
