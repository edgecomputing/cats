

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.Logistics;


namespace Cats.Services.Logistics
{

    public class DeliveryService : IDeliveryService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DeliveryService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddDelivery(Delivery delivery)
        {
            _unitOfWork.DeliveryRepository.Add(delivery);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDelivery(Delivery delivery)
        {
            _unitOfWork.DeliveryRepository.Edit(delivery);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDelivery(Delivery delivery)
        {
            if (delivery == null) return false;
            _unitOfWork.DeliveryRepository.Delete(delivery);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DeliveryRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DeliveryRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Delivery> GetAllDelivery()
        {
            return _unitOfWork.DeliveryRepository.GetAll();
        }
        public Delivery FindById(int id)
        {
            return _unitOfWork.DeliveryRepository.FindById(id);
        }
        public List<Delivery> FindBy(Expression<Func<Delivery, bool>> predicate)
        {
            return _unitOfWork.DeliveryRepository.FindBy(predicate);
        }
        public IEnumerable<Delivery> Get(
           Expression<Func<Delivery, bool>> filter = null,
           Func<IQueryable<Delivery>, IOrderedQueryable<Delivery>> orderBy = null,
           string includeProperties = "")
        {
            return _unitOfWork.DeliveryRepository.Get(filter, orderBy, includeProperties);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



       
    }
}


