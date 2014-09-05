using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public class TransportBidQuotationHeaderService : ITransportBidQuotationHeaderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransportBidQuotationHeaderService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddTransportBidQuotationHeader(TransportBidQuotationHeader item)
        {
            _unitOfWork.TransportBidQuotationHeaderRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }

        public bool UpdateTransportBidQuotationHeader(TransportBidQuotationHeader item)
        {
            if (item == null) return false;
            _unitOfWork.TransportBidQuotationHeaderRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteTransportBidQuotationHeader(TransportBidQuotationHeader item)
        {
            if (item == null) return false;
            _unitOfWork.TransportBidQuotationHeaderRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var item = _unitOfWork.TransportBidQuotationHeaderRepository.FindById(id);
            return DeleteTransportBidQuotationHeader(item);
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
        public IEnumerable<TransportBidQuotationHeader> Get(System.Linq.Expressions.Expression<Func<TransportBidQuotationHeader, bool>> filter = null,
                                    Func<IQueryable<TransportBidQuotationHeader>, IOrderedQueryable<TransportBidQuotationHeader>> orderBy = null,
                                    string includeProperties = "")
        {
            return _unitOfWork.TransportBidQuotationHeaderRepository.Get(filter, orderBy, includeProperties);
        }
        //public 
        public string GetStatus(int id)
        {
            var priceQuotation =
                _unitOfWork.TransportBidQuotationHeaderRepository.FindBy(b => b.TransportBidQuotationHeaderID == id).FirstOrDefault();
            if (priceQuotation!=null)
            {
                if (priceQuotation.Status == 1)
                    return "Draft";
                if (priceQuotation.Status == 2)
                    return "Approved";
                return "Winner Generated";
            }
            return "";
        }

        

    }
}