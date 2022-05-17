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
    public class SellStorage : ISellStorage
    {
        public List<SellViewModel> GetFullList()
        {
            using var context = new Database();
            return context.Sells
            .Select(CreateModel)
            .ToList();
        }

        public List<SellViewModel> GetFilteredList(SellBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Database();
            if (model.WorkerId != 0 )
            {
                return context.Sells
            .Where(rec => rec.WorkerId.Equals(model.WorkerId))
            .ToList().Select(CreateModel).ToList();
            }
            else if (model.GSMId != 0)
            {
                return context.Sells
            .Where(rec => rec.GSMId.Equals(model.GSMId))
            .ToList().Select(CreateModel).ToList();
            }
            else if (model.SellDate.Year != 1)
            {
                return context.Sells
            .Where(rec => rec.SellDate.Equals(model.SellDate))
            .ToList().Select(CreateModel).ToList();
            }
            return null;
        }

        public SellViewModel GetElement(SellBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Database();
            var sell = context.Sells
            .FirstOrDefault(rec => rec.SellId == model.SellId);
            return sell != null ? CreateModel(sell) : null;
        }

        public void Insert(SellBindingModel model)
        {
            using var context = new Database();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Sell sell_to_add = CreateModel(model, new Sell(), context);
                context.Sells.Add(sell_to_add);
                context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        
        public void Delete(SellBindingModel model)
        {
            using var context = new Database();
            Sell element = context.Sells.FirstOrDefault(rec => rec.SellId == model.SellId);
            if (element != null)
            {
                context.Sells.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static Sell CreateModel(SellBindingModel model, Sell sell, Database context)
        {
            sell.SellDate = model.SellDate;
            sell.SellValue = model.SellValue;
            sell.GSMId = model.GSMId;
            sell.WorkerId = model.WorkerId;
            return sell;
        }

        private static SellViewModel CreateModel(Sell sell)
        {
            using var context = new Database();
            string nameGSM = context.GSMs.FirstOrDefault(rec => rec.GSMId == sell.GSMId).GSMName;
            string nameWorker = context.Workers.FirstOrDefault(rec => rec.WorkerId == sell.WorkerId).WorkerName;
            return new SellViewModel
            {
                SellId = sell.SellId,
                SellDate = sell.SellDate,
                SellValue = sell.SellValue,
                GSMId = sell.GSMId,
                WorkerId = sell.WorkerId,
                GSMName = nameGSM,
                WorkerName = nameWorker
            };
        }
    }
}
