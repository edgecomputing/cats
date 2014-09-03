using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public class TransporterPaymentRequestService : ITransporterPaymentRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransporterPaymentRequestService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            this._unitOfWork.Dispose();
        }

        public bool AddTransporterPaymentRequest(TransporterPaymentRequest transporterPaymentRequest)
        {
            _unitOfWork.TransporterPaymentRequestRepository.Add(transporterPaymentRequest);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteTransporterPaymentRequest(TransporterPaymentRequest transporterPaymentRequest)
        {
            if (transporterPaymentRequest == null) return false;
            _unitOfWork.TransporterPaymentRequestRepository.Delete(transporterPaymentRequest);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TransporterPaymentRequestRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TransporterPaymentRequestRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditTransporterPaymentRequest(TransporterPaymentRequest transporterPaymentRequest)
        {
            _unitOfWork.TransporterPaymentRequestRepository.Edit(transporterPaymentRequest);
            _unitOfWork.Save();
            return true;
        }

        public TransporterPaymentRequest FindById(int id)
        {
            return _unitOfWork.TransporterPaymentRequestRepository.FindById(id);
        }

        public List<TransporterPaymentRequest> GetAllTransporterPaymentRequest()
        {
            return _unitOfWork.TransporterPaymentRequestRepository.GetAll();
        }

        public List<TransporterPaymentRequest> FindBy(Expression<Func<TransporterPaymentRequest, bool>> predicate)
        {
            return _unitOfWork.TransporterPaymentRequestRepository.FindBy(predicate);
        }

        public IEnumerable<TransporterPaymentRequest> Get(
            Expression<Func<TransporterPaymentRequest, bool>> filter = null, 
            Func<IQueryable<TransporterPaymentRequest>, 
            IOrderedQueryable<TransporterPaymentRequest>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.TransporterPaymentRequestRepository.Get(filter, orderBy, includeProperties);
        }
    }
}
