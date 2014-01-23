using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Logistics
{
   public interface IDetailDistributionService
    {

        bool AddDetailDistribution(DetailDistribution DetailDistribution);
        bool DeleteDetailDistribution(DetailDistribution DetailDistribution);
        bool DeleteById(int id);
        bool EditDetailDistribution(DetailDistribution DetailDistribution);
        DetailDistribution FindById(Guid id);
        List<DetailDistribution> GetAllDetailDistribution();
        List<DetailDistribution> FindBy(Expression<Func<DetailDistribution, bool>> predicate);
        IEnumerable<DetailDistribution> Get(
            Expression<Func<DetailDistribution, bool>> filter = null,
            Func<IQueryable<DetailDistribution>, IOrderedQueryable<DetailDistribution>> orderBy = null,
            string includeProperties = "");

    }
}
