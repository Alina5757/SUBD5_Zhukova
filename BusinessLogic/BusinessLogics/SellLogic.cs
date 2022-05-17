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
    public class SellLogic : ISellLogic
    {
        private readonly ISellStorage _sellStorage;

        public SellLogic(ISellStorage sellStorage)
        {
            _sellStorage = sellStorage;
        }

        public List<SellViewModel> Read(SellBindingModel model)
        {
            if (model == null)
            {
                return _sellStorage.GetFullList();
            }

            if (model.SellId != null && model.SellId != 0)
            {
                return new List<SellViewModel> { _sellStorage.GetElement(model) };
            }

            return _sellStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(SellBindingModel model)
        {
            var element = _sellStorage.GetElement(new SellBindingModel
            {
                SellId = model.SellId
            });
            if (element != null && element.SellId != model.SellId)
            {
                throw new Exception("Уже есть такое ID");
            }
            else
            {
                _sellStorage.Insert(model);
            }
        }

        public void Delete(SellBindingModel model)
        {
            var element = _sellStorage.GetElement(new SellBindingModel
            {
                SellId = model.SellId
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _sellStorage.Delete(model);
        }
    }
}
