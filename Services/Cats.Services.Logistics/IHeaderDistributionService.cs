using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface IHeaderHeaderDistributionService
    {

        bool AddHeaderDistribution(HeaderDistribution HeaderDistribution);
        bool DeleteHeaderDistribution(HeaderDistribution HeaderDistribution);
        bool DeleteById(int id);
        bool EditHeaderDistribution(HeaderDistribution HeaderDistribution);
        HeaderDistribution FindById(int id);
        List<HeaderDistribution> GetAllHeaderDistribution();
        List<HeaderDistribution> FindBy(Expression<Func<HeaderDistribution, bool>> predicate);

        IEnumerable<HeaderDistribution> Get(
            Expression<Func<HeaderDistribution, bool>> filter = null,
            Func<IQueryable<HeaderDistribution>, IOrderedQueryable<HeaderDistribution>> orderBy = null,
            string includeProperties = "");

        List<ReliefRequisitionDetail> GetReliefRequisitions(int regionId);
        List<RegionalPSNPPlanDetail> GetPSNPPlan(int regionId);
    }
}
