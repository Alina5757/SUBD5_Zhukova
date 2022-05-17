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
    public class GSMLogic : IGSMLogic
    {
        private readonly IGSMStorage _gsmStorage;

        public GSMLogic(IGSMStorage gsmStorage)
        {
            _gsmStorage = gsmStorage;
        }

        public List<GSMViewModel> Read(GSMBindingModel model)
        {
            if (model == null)
            {
                return _gsmStorage.GetFullList();
            }

            if (model.GSMId != null && model.GSMId != 0)
            {
                return new List<GSMViewModel> { _gsmStorage.GetElement(model) };
            }

            return _gsmStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(GSMBindingModel model)
        {
            var element = _gsmStorage.GetElement(new GSMBindingModel
            {
                GSMName = model.GSMName
            });
            if (element != null && element.GSMId != model.GSMId)
            {
                throw new Exception("Уже есть ГСМ с таким названием");
            }
            if (model.GSMId != null && model.GSMId != 0)
            {
                _gsmStorage.Update(model);
            }
            else
            {
                _gsmStorage.Insert(model);
            }
        }

        public void Delete(GSMBindingModel model)
        {
            var element = _gsmStorage.GetElement(new GSMBindingModel
            {
                GSMId = model.GSMId
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _gsmStorage.Delete(model);
        }
    }
}
