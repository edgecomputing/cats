using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public interface IBidDetailService
    {
        bool AddBidDetail(BidDetail bidDetail);
        bool DeleteBidDetail(BidDetail bidDetail);
        bool DeleteById(int id);
        bool EditBidDetail(BidDetail bidDetail);
        BidDetail FindById(int id);
        List<BidDetail> GetAllBidDetail();
        List<BidDetail> FindBy(Expression<Func<BidDetail, bool>> predicate);

        IEnumerable<BidDetail> Get(
             Expression<Func<BidDetail, bool>> filter = null,
             Func<IQueryable<BidDetail>, IOrderedQueryable<BidDetail>> orderBy = null,
             string includeProperties = "");
    }
}
