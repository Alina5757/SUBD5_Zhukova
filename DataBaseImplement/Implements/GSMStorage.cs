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
    public class GSMStorage : IGSMStorage
    {
        public List<GSMViewModel> GetFullList()
        {
            using var context = new Database();
            return context.GSMs
            .Select(CreateModel)
            .ToList();
        }

        public List<GSMViewModel> GetFilteredList(GSMBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Database();
            return context.GSMs
            .Where(rec => rec.GSMName.Equals(model.GSMName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public GSMViewModel GetElement(GSMBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Database();
            var gsm = context.GSMs
            .FirstOrDefault(rec => rec.GSMName == model.GSMName || rec.GSMId == model.GSMId);
            return gsm != null ? CreateModel(gsm) : null;
        }

        public void Insert(GSMBindingModel model)
        {
            using var context = new Database();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                GSM gsm_to_add = CreateModel(model, new GSM(), context);
                context.GSMs.Add(gsm_to_add);
                context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(GSMBindingModel model)
        {
            using var context = new Database();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.GSMs.FirstOrDefault(rec => rec.GSMId == model.GSMId);
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

        public void Delete(GSMBindingModel model)
        {
            using var context = new Database();
            GSM element = context.GSMs.FirstOrDefault(rec => rec.GSMId == model.GSMId);
            if (element != null)
            {
                context.GSMs.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static GSM CreateModel(GSMBindingModel model, GSM gsm, Database context)
        {
            gsm.GSMName = model.GSMName;        
            return gsm;
        }

        private static GSMViewModel CreateModel(GSM gsm)
        {
            return new GSMViewModel
            {
                GSMId = gsm.GSMId,
                GSMName = gsm.GSMName                
            };
        }
    }
}
