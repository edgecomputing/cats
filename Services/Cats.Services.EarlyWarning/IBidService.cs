using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IBidService
    {
        bool AddBid(Bid bid);
        bool DeleteBid(Bid bid);
        bool DeleteById(int id);
        bool EditBid(Bid bid);
        Bid FindById(int id);
        List<Bid> GetAllBid();
        List<Bid> FindBy(Expression<Func<Bid, bool>> predicate);

        IEnumerable<Bid> Get(
             Expression<Func<Bid, bool>> filter = null,
             Func<IQueryable<Bid>, IOrderedQueryable<Bid>> orderBy = null,
             string includeProperties = "");
    }
}
