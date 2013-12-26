using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public interface IBidWinnerService
    {
        bool AddBidWinner(BidWinner bidWinner);
        bool DeleteBidWinner(BidWinner bidWinner);
        bool DeleteById(int id);
        bool EditBidWinner(BidWinner bidWinner);
        BidWinner FindById(int id);
        List<BidWinner> GetAllBidWinner();
        List<BidWinner> FindBy(Expression<Func<BidWinner, bool>> predicate);

        IEnumerable<BidWinner> Get(
             Expression<Func<BidWinner, bool>> filter = null,
             Func<IQueryable<BidWinner>, IOrderedQueryable<BidWinner>> orderBy = null,
             string includeProperties = "");

        List<Bid> GetBidsWithWinner();
        bool SignContract(List<BidWinner> bidWinner);
        bool Disqualified(BidWinner bidWinner);
        bool IsRfqGenerated(int bidID);
        bool Save();
    }
}
