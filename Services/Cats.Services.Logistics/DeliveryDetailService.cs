

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.Logistics
{

    public class DeliveryDetailService : IDeliveryDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DeliveryDetailService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddDeliveryDetail(DeliveryDetail deliveryDetail)
        {
            _unitOfWork.DeliveryDetailRepository.Add(deliveryDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDeliveryDetail(DeliveryDetail deliveryDetail)
        {
            _unitOfWork.DeliveryDetailRepository.Edit(deliveryDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDeliveryDetail(DeliveryDetail deliveryDetail)
        {
            if (deliveryDetail == null) return false;
            _unitOfWork.DeliveryDetailRepository.Delete(deliveryDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DeliveryDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DeliveryDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<DeliveryDetail> GetAllDeliveryDetail()
        {
            return _unitOfWork.DeliveryDetailRepository.GetAll();
        }
        public DeliveryDetail FindById(Guid id)
        {
            return _unitOfWork.DeliveryDetailRepository.FindById(id);
        }
        public List<DeliveryDetail> FindBy(Expression<Func<DeliveryDetail, bool>> predicate)
        {
            return _unitOfWork.DeliveryDetailRepository.FindBy(predicate);
        }
       public IEnumerable<DeliveryDetail> Get(
            Expression<Func<DeliveryDetail, bool>> filter = null,
            Func<IQueryable<DeliveryDetail>, IOrderedQueryable<DeliveryDetail>> orderBy = null,
            string includeProperties = "")
       {
           return _unitOfWork.DeliveryDetailRepository.Get(filter, orderBy, includeProperties);
       }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


