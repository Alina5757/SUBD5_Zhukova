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
    public class SmenaLogic : ISmenaLogic
    {
        private readonly ISmenaStorage _smenaStorage;

        public SmenaLogic(ISmenaStorage smenaStorage)
        {
            _smenaStorage = smenaStorage;
        }

        public List<SmenaViewModel> Read(SmenaBindingModel model)
        {
            if (model == null)
            {
                return _smenaStorage.GetFullList();
            }

            if (model.SmenaNumber != 0 && model.SmenaDate.Year != 1 && model.WorkerId != 0)
            {
                return new List<SmenaViewModel> { _smenaStorage.GetElement(model) };
            }

            return _smenaStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(SmenaBindingModel model)
        {
            var element = _smenaStorage.GetElement(new SmenaBindingModel
            {
                SmenaDate = model.SmenaDate,
                SmenaNumber = model.SmenaNumber,
                WorkerId = model.WorkerId
            });
            if (element != null)
            {
                throw new Exception("Уже есть такое назначение на смену");
            }
            else
            {
                _smenaStorage.Insert(model);
            }
        }

        public void Delete(SmenaBindingModel model)
        {
            var element = _smenaStorage.GetElement(new SmenaBindingModel
            {
                SmenaDate = model.SmenaDate,
                SmenaNumber = model.SmenaNumber,
                WorkerId = model.WorkerId
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _smenaStorage.Delete(model);
        }
    }
}
