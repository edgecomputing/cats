

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.Logistics
{

    public class DistributionDetailService : IDeliveryDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DistributionDetailService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddDistributionDetail(DeliveryDetail distributionDetail)
        {
            _unitOfWork.DistributionDetailRepository.Add(distributionDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDistributionDetail(DeliveryDetail distributionDetail)
        {
            _unitOfWork.DistributionDetailRepository.Edit(distributionDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDistributionDetail(DeliveryDetail distributionDetail)
        {
            if (distributionDetail == null) return false;
            _unitOfWork.DistributionDetailRepository.Delete(distributionDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DistributionDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DistributionDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<DeliveryDetail> GetAllDistributionDetail()
        {
            return _unitOfWork.DistributionDetailRepository.GetAll();
        }
        public DeliveryDetail FindById(Guid id)
        {
            return _unitOfWork.DistributionDetailRepository.FindById(id);
        }
        public List<DeliveryDetail> FindBy(Expression<Func<DeliveryDetail, bool>> predicate)
        {
            return _unitOfWork.DistributionDetailRepository.FindBy(predicate);
        }
       public IEnumerable<DeliveryDetail> Get(
            Expression<Func<DeliveryDetail, bool>> filter = null,
            Func<IQueryable<DeliveryDetail>, IOrderedQueryable<DeliveryDetail>> orderBy = null,
            string includeProperties = "")
       {
           return _unitOfWork.DistributionDetailRepository.Get(filter, orderBy, includeProperties);
       }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


