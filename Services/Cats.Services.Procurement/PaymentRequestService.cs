using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public class PaymentRequestService : IPaymentRequestService
    {
         private readonly IUnitOfWork _unitOfWork;
         public PaymentRequestService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool Create(PaymentRequest item)
        {
            _unitOfWork.PaymentRequestRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }

        public PaymentRequest FindById(int id)
        {
             var item = _unitOfWork.PaymentRequestRepository.FindById(id);
            return item;
        }
        public List<PaymentRequest> GetAll()
        {
            return _unitOfWork.PaymentRequestRepository.GetAll();
        }

        public List<PaymentRequest> FindBy(Expression<Func<PaymentRequest, bool>> predicate)
        {
             return _unitOfWork.PaymentRequestRepository.FindBy(predicate);
        }

        public bool Update(PaymentRequest item)
        {
             if (item == null) return false;
            _unitOfWork.PaymentRequestRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(PaymentRequest item)
        {
             if (item == null) return false;
            _unitOfWork.PaymentRequestRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.PaymentRequestRepository.FindById(id);
            return Delete(item);
        }

        public IEnumerable<PaymentRequest> Get(System.Linq.Expressions.Expression<Func<PaymentRequest, bool>> filter = null,
                                    Func<IQueryable<PaymentRequest>, IOrderedQueryable<PaymentRequest>> orderBy = null,
                                    string includeProperties = "")
        {
            return _unitOfWork.PaymentRequestRepository.Get(filter, orderBy, includeProperties);
        }
    }
}