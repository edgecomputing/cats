

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
        public Delivery FindById(Guid id)
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



        public decimal GetFDPDelivery(int transportOrderId, int fdpId)
        {
            var dispatchAllocation =
               _unitOfWork.DispatchAllocationRepository.FindBy(
                   m => m.TransportOrderID == transportOrderId && m.FDPID == fdpId).FirstOrDefault();
            if (dispatchAllocation != null)
            {
                var dispatch =
                    _unitOfWork.DispatchRepository.FindBy(
                        m => m.DispatchAllocationID == dispatchAllocation.DispatchAllocationID).FirstOrDefault();
                if (dispatch != null)
                {
                    var delivery =
                        _unitOfWork.DeliveryRepository.FindBy(m => m.DispatchID == dispatch.DispatchID).FirstOrDefault();
                    if (delivery!=null)
                    {
                        return delivery.DeliveryDetails.Sum(m => m.ReceivedQuantity*10);//return Delivered amount in quintal
                    }
                   
                }

            }
            return 0;
        }

        public int? GetDonorID(int id)
        {
            var gift = _unitOfWork.GiftCertificateRepository.FindBy(m => m.ShippingInstructionID == id).FirstOrDefault();
            if (gift == null) return null;
            return gift.DonorID;
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }





    }
}


