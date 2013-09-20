

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.Administration
{

    public class CommoditySourceService : ICommoditySourceService
    {
        private readonly IUnitOfWork _unitOfWork;


        public CommoditySourceService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddCommoditySource(CommoditySource commoditySource)
        {
            _unitOfWork.CommoditySourceRepository.Add(commoditySource);
            _unitOfWork.Save();
            return true;

        }
        public bool EditCommoditySource(CommoditySource commoditySource)
        {
            _unitOfWork.CommoditySourceRepository.Edit(commoditySource);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteCommoditySource(CommoditySource commoditySource)
        {
            if (commoditySource == null) return false;
            _unitOfWork.CommoditySourceRepository.Delete(commoditySource);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.CommoditySourceRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.CommoditySourceRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<CommoditySource> GetAllCommoditySource()
        {
            return _unitOfWork.CommoditySourceRepository.GetAll();
        }
        public CommoditySource FindById(int id)
        {
            return _unitOfWork.CommoditySourceRepository.FindById(id);
        }
        public List<CommoditySource> FindBy(Expression<Func<CommoditySource, bool>> predicate)
        {
            return _unitOfWork.CommoditySourceRepository.FindBy(predicate);
        }
        #endregion
       
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


