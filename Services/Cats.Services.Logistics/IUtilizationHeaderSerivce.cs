using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface IUtilizationHeaderSerivce
    {

        bool AddHeaderDistribution(UtilizationHeader HeaderDistribution);
        bool DeleteHeaderDistribution(UtilizationHeader HeaderDistribution);
        bool DeleteById(int id);
        bool EditHeaderDistribution(UtilizationHeader HeaderDistribution);
        UtilizationHeader FindById(int id);
        List<UtilizationHeader> GetAllHeaderDistribution();
        List<UtilizationHeader> FindBy(Expression<Func<UtilizationHeader, bool>> predicate);

        IEnumerable<UtilizationHeader> Get(
            Expression<Func<UtilizationHeader, bool>> filter = null,
            Func<IQueryable<UtilizationHeader>, IOrderedQueryable<UtilizationHeader>> orderBy = null,
            string includeProperties = "");

        ReliefRequisition GetReliefRequisitions(int requisitionId);
        List<ReliefRequisition> GetRequisitions(int regionId, int status);
        RegionalPSNPPlan  GetPSNPPlanRequisitions(int regionId, int status);
        RegionalPSNPPlan  GetPSNPPlan(int planId);
    }
}
