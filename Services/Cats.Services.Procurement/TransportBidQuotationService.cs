using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public class TransportBidQuotationService : ITransportBidQuotationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransportBidQuotationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddTransportBidQuotation(TransportBidQuotation item)
        {
            _unitOfWork.TransportBidQuotationRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateTransportBidQuotation(TransportBidQuotation item)
        {
            if (item == null) return false;
            _unitOfWork.TransportBidQuotationRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteTransportBidQuotation(TransportBidQuotation item)
        {
            if (item == null) return false;
            _unitOfWork.TransportBidQuotationRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.TransportBidQuotationRepository.FindById(id);
            return DeleteTransportBidQuotation(item);
        }
        public TransportBidQuotation FindById(int id)
        {
            return _unitOfWork.TransportBidQuotationRepository.FindById(id);
        }
        public List<TransportBidQuotation> GetAllTransportBidQuotation()
        {
            return _unitOfWork.TransportBidQuotationRepository.GetAll();

        }
        public List<TransportBidQuotation> FindBy(Expression<Func<TransportBidQuotation, bool>> predicate)
        {
            return _unitOfWork.TransportBidQuotationRepository.FindBy(predicate);

        }
        public IEnumerable<TransportBidQuotation> Get(System.Linq.Expressions.Expression<Func<TransportBidQuotation, bool>> filter = null,
                                    Func<IQueryable<TransportBidQuotation>, IOrderedQueryable<TransportBidQuotation>> orderBy = null,
                                    string includeProperties = "")
        {
            return _unitOfWork.TransportBidQuotationRepository.Get(filter, orderBy, includeProperties);
        }
        //public 
    }
}