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
    public class SmenaStorage : ISmenaStorage
    {
        public List<SmenaViewModel> GetFullList()
        {
            using var context = new Database();
            return context.Smenas
            .Select(CreateModel)
            .ToList();
        }

        public List<SmenaViewModel> GetFilteredList(SmenaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Database();
            if (model.WorkerId != 0 && model.SmenaDate.Year != 1 && model.SmenaNumber != 0)
            {
                return context.Smenas
            .Where(rec => rec.WorkerId.Equals(model.WorkerId) && rec.SmenaDate.Equals(model.SmenaDate) && rec.SmenaNumber.Equals(model.SmenaNumber))
            .ToList().Select(CreateModel).ToList();
            }
            if (model.WorkerId != 0 && model.SmenaDate.Year != 1)
            {
                return context.Smenas
            .Where(rec => rec.WorkerId.Equals(model.WorkerId) && rec.SmenaDate.Equals(model.SmenaDate))
            .ToList().Select(CreateModel).ToList();
            }
            if (model.WorkerId != 0 && model.SmenaNumber != 0)
            {
                return context.Smenas
            .Where(rec => rec.WorkerId.Equals(model.WorkerId) && rec.SmenaNumber.Equals(model.SmenaNumber))
            .ToList().Select(CreateModel).ToList();
            }
            if (model.SmenaDate.Year != 1 && model.SmenaNumber != 0)
            {
                return context.Smenas
            .Where(rec => rec.SmenaDate.Equals(model.SmenaDate) && rec.SmenaNumber.Equals(model.SmenaNumber))
            .ToList().Select(CreateModel).ToList();
            }
            if (model.WorkerId != 0)
            {
                return context.Smenas
            .Where(rec => rec.WorkerId.Equals(model.WorkerId))
            .ToList().Select(CreateModel).ToList();
            }
            if (model.SmenaDate.Year != 1)
            {
                return context.Smenas
            .Where(rec => rec.SmenaDate.Equals(model.SmenaDate))
            .ToList().Select(CreateModel).ToList();
            }
            if (model.SmenaNumber != 0)
            {
                return context.Smenas
            .Where(rec => rec.SmenaNumber.Equals(model.SmenaNumber))
            .ToList().Select(CreateModel).ToList();
            }
            return null;
        }

        public SmenaViewModel GetElement(SmenaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Database();
            var smena = context.Smenas
            .FirstOrDefault(rec => rec.SmenaDate == model.SmenaDate && rec.SmenaNumber == model.SmenaNumber && rec.WorkerId == model.WorkerId);
            return smena != null ? CreateModel(smena) : null;
        }

        public void Insert(SmenaBindingModel model)
        {
            using var context = new Database();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Smena smena_to_add = CreateModel(model, new Smena(), context);
                context.Smenas.Add(smena_to_add);
                context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        public void Delete(SmenaBindingModel model)
        {
            using var context = new Database();
            Smena element = context.Smenas.FirstOrDefault(rec => rec.SmenaDate == model.SmenaDate && 
            rec.SmenaNumber == model.SmenaNumber && rec.WorkerId == model.WorkerId);
            if (element != null)
            {
                context.Smenas.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static Smena CreateModel(SmenaBindingModel model, Smena smena, Database context)
        {
            smena.SmenaDate = model.SmenaDate;
            smena.SmenaNumber = model.SmenaNumber;
            smena.WorkerId = model.WorkerId;
            return smena;
        }

        private static SmenaViewModel CreateModel(Smena smena)
        {
            using var context = new Database();
            string nameWorker = context.Workers.FirstOrDefault(rec => rec.WorkerId == smena.WorkerId).WorkerName;
            return new SmenaViewModel
            {
                SmenaNumber = smena.SmenaNumber,
                SmenaDate = smena.SmenaDate,
                WorkerId = smena.WorkerId,
                WorkerName = nameWorker
            };
        }
    }
}
