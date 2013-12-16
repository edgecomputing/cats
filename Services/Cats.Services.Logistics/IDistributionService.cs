
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface IDistributionService
    {

        bool AddDistribution(Distribution distribution);
        bool DeleteDistribution(Distribution distribution);
        bool DeleteById(int id);
        bool EditDistribution(Distribution distribution);
        Distribution FindById(int id);
        List<Distribution> GetAllDistribution();
        List<Distribution> FindBy(Expression<Func<Distribution, bool>> predicate);

        IEnumerable<Distribution> Get(
            Expression<Func<Distribution, bool>> filter = null,
            Func<IQueryable<Distribution>, IOrderedQueryable<Distribution>> orderBy = null,
            string includeProperties = "");

   

    }
}


