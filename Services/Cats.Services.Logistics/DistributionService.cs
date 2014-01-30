

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.Logistics;


namespace Cats.Services.Logistics
{

    public class DistributionService : IDeliveryService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DistributionService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddDistribution(Delivery distribution)
        {
            _unitOfWork.DistributionRepository.Add(distribution);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDistribution(Delivery distribution)
        {
            _unitOfWork.DistributionRepository.Edit(distribution);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDistribution(Delivery distribution)
        {
            if (distribution == null) return false;
            _unitOfWork.DistributionRepository.Delete(distribution);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DistributionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DistributionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Delivery> GetAllDistribution()
        {
            return _unitOfWork.DistributionRepository.GetAll();
        }
        public Delivery FindById(int id)
        {
            return _unitOfWork.DistributionRepository.FindById(id);
        }
        public List<Delivery> FindBy(Expression<Func<Delivery, bool>> predicate)
        {
            return _unitOfWork.DistributionRepository.FindBy(predicate);
        }
        public IEnumerable<Delivery> Get(
           Expression<Func<Delivery, bool>> filter = null,
           Func<IQueryable<Delivery>, IOrderedQueryable<Delivery>> orderBy = null,
           string includeProperties = "")
        {
            return _unitOfWork.DistributionRepository.Get(filter, orderBy, includeProperties);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



       
    }
}


