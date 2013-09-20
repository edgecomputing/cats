
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.Administration
{
    public interface ICommodityService:IDisposable
    {

        bool AddCommodity(Commodity commodity);
        bool DeleteCommodity(Commodity commodity);
        bool DeleteById(int id);
        bool EditCommodity(Commodity commodity);
        Commodity FindById(int id);
        List<Commodity> GetAllCommodity();
        List<Commodity> GetCommonCommodity();
        List<Commodity> FindBy(Expression<Func<Commodity, bool>> predicate);

        IEnumerable<Commodity> Get(
             Expression<Func<Commodity, bool>> filter = null,
             Func<IQueryable<Commodity>, IOrderedQueryable<Commodity>> orderBy = null,
             string includeProperties = "");

        IEnumerable<Unit> GetAllUnit(); 
        int GetCommoidtyId(string commodityName);
    }
}


