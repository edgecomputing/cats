using System;
using System.Collections.Generic;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.Procurement
{
    class BidWinnerService:IBidWinnerService
    {
         private readonly  IUnitOfWork _unitOfWork;


         public BidWinnerService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
        public bool AddBidWinner(BidWinner bidWinner)
        {
            _unitOfWork.BidWinnerRepository.Add(bidWinner);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteBidWinner(BidWinner bidWinner)
        {
            if (bidWinner == null) return false;
            _unitOfWork.BidWinnerRepository.Delete(bidWinner);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.BidWinnerRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.BidWinnerRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditBidWinner(BidWinner bidWinner)
        {
            _unitOfWork.BidWinnerRepository.Edit(bidWinner);
            _unitOfWork.Save();
            return true;
        }

        public BidWinner FindById(int id)
        {
            return _unitOfWork.BidWinnerRepository.FindById(id);
        }

        public List<BidWinner> GetAllBidWinner()
        {
            return _unitOfWork.BidWinnerRepository.GetAll();
        }

        public List<BidWinner> FindBy(System.Linq.Expressions.Expression<Func<BidWinner, bool>> predicate)
        {
            return _unitOfWork.BidWinnerRepository.FindBy(predicate);
        }

        public IEnumerable<BidWinner> Get(System.Linq.Expressions.Expression<Func<BidWinner, bool>>
            filter = null, Func<IQueryable<BidWinner>, IOrderedQueryable<BidWinner>> 
            orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.BidWinnerRepository.Get(filter, orderBy, includeProperties);
        }

        public bool Save()
        {
            _unitOfWork.Save();
            return true;
        }
    }
}
