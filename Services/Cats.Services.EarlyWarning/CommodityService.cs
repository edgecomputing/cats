

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.EarlyWarning
{

    public class CommodityService : ICommodityService
    {
        private readonly IUnitOfWork _unitOfWork;


        public CommodityService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddCommodity(Commodity commodity)
        {
            _unitOfWork.CommodityRepository.Add(commodity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditCommodity(Commodity commodity)
        {
            _unitOfWork.CommodityRepository.Edit(commodity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteCommodity(Commodity commodity)
        {
            if (commodity == null) return false;
            _unitOfWork.CommodityRepository.Delete(commodity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.CommodityRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.CommodityRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Commodity> GetAllCommodity()
        {
            return _unitOfWork.CommodityRepository.GetAll();
        }

        public List<Commodity> GetCommonCommodity()
        {
            return _unitOfWork.CommodityRepository.FindBy(m=>m.CommodityID==1 && m.CommodityID==2 && m.CommodityID==4 && m.CommodityID==8);
        }

        public Commodity FindById(int id)
        {
            return _unitOfWork.CommodityRepository.FindById(id);
        }
        public List<Commodity> FindBy(Expression<Func<Commodity, bool>> predicate)
        {
            return _unitOfWork.CommodityRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

        public IEnumerable<Commodity> Get(System.Linq.Expressions.
           Expression<Func<Commodity, bool>> filter = null,
           Func<IQueryable<Commodity>, IOrderedQueryable<Commodity>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.CommodityRepository.Get(filter, orderBy, includeProperties);
        }

        public int GetCommoidtyId(string commodityName)
        {
            return  _unitOfWork.CommodityRepository.FindBy(t => t.Name == commodityName).SingleOrDefault().CommodityID;
            
           
        }


        public IEnumerable<Unit> GetAllUnit()
        {
            return _unitOfWork.UnitRepository.GetAll();
        }
    }
}


