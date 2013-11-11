

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Common;


namespace Cats.Services.Hub
{

    public class CommodityTypeService : ICommodityTypeService
    {
        private readonly IUnitOfWork _unitOfWork;


        public CommodityTypeService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddCommodityType(CommodityType commodityType)
        {
            _unitOfWork.CommodityTypeRepository.Add(commodityType);
            _unitOfWork.Save();
            return true;

        }
        public bool EditCommodityType(CommodityType commodityType)
        {
            _unitOfWork.CommodityTypeRepository.Edit(commodityType);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteCommodityType(CommodityType commodityType)
        {
            if (commodityType == null) return false;
            _unitOfWork.CommodityTypeRepository.Delete(commodityType);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.CommodityTypeRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.CommodityTypeRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<CommodityType> GetAllCommodityType()
        {
            return _unitOfWork.CommodityTypeRepository.GetAll();
        }
        public CommodityType FindById(int id)
        {
            return _unitOfWork.CommodityTypeRepository.FindById(id);
        }
        public List<CommodityType> FindBy(Expression<Func<CommodityType, bool>> predicate)
        {
            return _unitOfWork.CommodityTypeRepository.FindBy(predicate);
        }
        #endregion

        /// <summary>
        /// Gets the name of the commodity by.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        public CommodityType GetCommodityByName(string p)
        {
            return _unitOfWork.CommodityTypeRepository.Get(t => t.Name == p).FirstOrDefault();
           
        }


        public List<CommodityTypeViewModel> GetAllCommodityTypeForReprot()
        {
            var tempCommodityTypes = _unitOfWork.CommodityTypeRepository.GetAll();
            var commodityTypes = (from c in tempCommodityTypes select new CommodityTypeViewModel() { CommodityTypeId = c.CommodityTypeID, CommodityTypeName = c.Name }).ToList();
            commodityTypes.Insert(0, new CommodityTypeViewModel { CommodityTypeName = "All Commodities" });
            return commodityTypes;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


