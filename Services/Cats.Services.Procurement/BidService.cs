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

        public void ActivateBid(int id)
        {
            var approvedbid = _unitOfWork.BidRepository.FindById(id);
            var activeBid = _unitOfWork.BidRepository.FindBy(m => m.StatusID == 2).FirstOrDefault();
            try
            {
                //change the status of bid in to active
                approvedbid.StatusID = 2;
                if (activeBid!=null)
                {
                    activeBid.StatusID = 5;
                    _unitOfWork.Save();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public string AutogenerateBidNo()
        {
            try
            {
                var bids = GetAllBid();
                string maxBidNo = (from bid in bids
                                   select bid.BidNumber).Max();

                var numericoutput = new string(maxBidNo.ToCharArray().Where(char.IsDigit).ToArray());
                int intNumericOutput = int.Parse(numericoutput);
                intNumericOutput = intNumericOutput + 1;

                var stringOutput = new string(maxBidNo.ToCharArray().Where(char.IsLetter).ToArray());
                var newBidNo = stringOutput + "-" + intNumericOutput;
                return newBidNo;

            }
            catch
            {
                return "Bid-001";
            }

        }
        public bool Save()
        {
            _unitOfWork.Save();
            return true;
        }
    }
}
