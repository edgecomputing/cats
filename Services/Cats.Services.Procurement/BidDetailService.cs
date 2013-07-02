using System;
using System.Collections.Generic;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public class BidDetailService : IBidDetailService
    {
        
        private readonly IUnitOfWork _unitOfWork;


        public BidDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool AddBidDetail(BidDetail bidDetail)
        {
            _unitOfWork.BidDetailRepository.Add(bidDetail);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteBidDetail(BidDetail bidDetail)
        {
            if (bidDetail == null) return false;
            _unitOfWork.BidDetailRepository.Delete(bidDetail);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.BidDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.BidDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditBidDetail(BidDetail bidDetail)
        {
            _unitOfWork.BidDetailRepository.Edit(bidDetail);
            _unitOfWork.Save();
            return true;
        }

        public BidDetail FindById(int id)
        {
            return _unitOfWork.BidDetailRepository.FindById(id);
        }

        public List<BidDetail> GetAllBidDetail()
        {
            return _unitOfWork.BidDetailRepository.GetAll();
        }

        public List<BidDetail> FindBy(System.Linq.Expressions.Expression<Func<BidDetail, bool>> predicate)
        {
            return _unitOfWork.BidDetailRepository.FindBy(predicate);
        }

        public IEnumerable<BidDetail> Get(System.Linq.Expressions.
                                              Expression<Func<BidDetail, bool>> filter = null,
                                          Func<IQueryable<BidDetail>, IOrderedQueryable<BidDetail>> orderBy = null,
                                          string includeProperties = "")
        {
            return _unitOfWork.BidDetailRepository.Get(filter, orderBy, includeProperties);
        }
    }
}


