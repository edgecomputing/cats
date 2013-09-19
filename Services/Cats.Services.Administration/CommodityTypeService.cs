

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.Administration
{

    public class CommodityTypeService : ICommodityTypeService
    {
        private readonly IUnitOfWork _unitOfWork;


        public CommodityTypeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
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

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}



