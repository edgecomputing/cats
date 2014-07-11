using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Procurement
{
   public class TransportBidQuotationHeaderHeaderService:ITransportBidQuotationHeader
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransportBidQuotationHeaderHeaderService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool AddTransportBidQuotationHeader(TransportBidQuotationHeader transportBidQuotationHeader)
        {
            _unitOfWork.TransportBidQuotationHeaderRepository.Add(transportBidQuotationHeader);
            _unitOfWork.Save();
            return true;
        }

        public bool UpdateTransportBidQuotationHeader(TransportBidQuotationHeader transportBidQuotationHeader)
        {
            if (transportBidQuotationHeader == null) return false;
            _unitOfWork.TransportBidQuotationHeaderRepository.Edit(transportBidQuotationHeader);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteTransportBidQuotationHeader(TransportBidQuotationHeader transportBidQuotationHeader)
        {
            if (transportBidQuotationHeader == null) return false;
            _unitOfWork.TransportBidQuotationHeaderRepository.Delete(transportBidQuotationHeader);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var transportBidQuotationHeader = _unitOfWork.TransportBidQuotationHeaderRepository.FindById(id);
            return DeleteTransportBidQuotationHeader(transportBidQuotationHeader);
        }

        public TransportBidQuotationHeader FindById(int id)
        {
            return _unitOfWork.TransportBidQuotationHeaderRepository.FindById(id);
        }

        public List<TransportBidQuotationHeader> GetAllTransportBidQuotationHeader()
        {
            return _unitOfWork.TransportBidQuotationHeaderRepository.GetAll();
        }

        public List<TransportBidQuotationHeader> FindBy(Expression<Func<TransportBidQuotationHeader, bool>> predicate)
        {
            return _unitOfWork.TransportBidQuotationHeaderRepository.FindBy(predicate);
        }

        public IEnumerable<TransportBidQuotationHeader> Get(Expression<Func<TransportBidQuotationHeader, bool>> filter = null, Func<IQueryable<TransportBidQuotationHeader>, IOrderedQueryable<TransportBidQuotationHeader>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.TransportBidQuotationHeaderRepository.Get(filter, orderBy, includeProperties);
        }
    }
}
