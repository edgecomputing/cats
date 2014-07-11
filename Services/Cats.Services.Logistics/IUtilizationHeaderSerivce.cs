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

        bool AddHeaderDistribution(WoredaStockDistribution HeaderDistribution);
        bool DeleteHeaderDistribution(WoredaStockDistribution HeaderDistribution);
        bool DeleteById(int id);
        bool EditHeaderDistribution(WoredaStockDistribution HeaderDistribution);
        WoredaStockDistribution FindById(int id);
        List<WoredaStockDistribution> GetAllHeaderDistribution();
        List<WoredaStockDistribution> FindBy(Expression<Func<WoredaStockDistribution, bool>> predicate);

        IEnumerable<WoredaStockDistribution> Get(
            Expression<Func<WoredaStockDistribution, bool>> filter = null,
            Func<IQueryable<WoredaStockDistribution>, IOrderedQueryable<WoredaStockDistribution>> orderBy = null,
            string includeProperties = "");

        
       
        List<ReliefRequisitionDetail> GetReliefRequisitions(int requisitionId);
        List<ReliefRequisition> GetRequisitions(int zoneId, int programId, int planId, int status, int month, int round);
        RegionalPSNPPlan  GetPSNPPlanRequisitions(int regionId, int status);
        RegionalPSNPPlan  GetPSNPPlan(int planId);
    }
}
