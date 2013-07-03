using System;
using System.Collections.Generic;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public class BidService:IBidService
    {
        private readonly  IUnitOfWork _unitOfWork;
      

       public BidService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
        
        public bool AddBid(Bid bid)
        {
            _unitOfWork.BidRepository.Add(bid);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteBid(Bid bid)
        {
            if (bid == null) return false;
            _unitOfWork.BidRepository.Delete(bid);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.BidRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.BidRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditBid(Bid bid)
        {
            _unitOfWork.BidRepository.Edit(bid);
            _unitOfWork.Save();
            return true;
        }

        public Bid FindById(int id)
        {
            return _unitOfWork.BidRepository.FindById(id);
        }

        public List<Bid> GetAllBid()
        {
            return _unitOfWork.BidRepository.GetAll();
        }

        public List<Bid> FindBy(System.Linq.Expressions.Expression<Func<Bid, bool>> predicate)
        {
            return _unitOfWork.BidRepository.FindBy(predicate);
        }

        public IEnumerable<Bid> Get(System.Linq.Expressions.
            Expression<Func<Bid, bool>> filter = null, 
            Func<IQueryable<Bid>, IOrderedQueryable<Bid>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.BidRepository.Get(filter, orderBy, includeProperties);
        }
    }
}
