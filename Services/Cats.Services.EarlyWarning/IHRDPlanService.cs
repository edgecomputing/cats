using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
   public interface IHRDPlanService:IDisposable
    {
        bool AddHRDPlan(Plan hrdPlan);
        bool DeleteHRDPlan(Plan hrdPlan);
        bool DeleteById(int id);
        bool EditHRDPlan(Plan hrdPlan);
        Plan FindById(int id);
        List<Plan> GetAllHRDPlan();
        List<Plan> FindBy(Expression<Func<Plan, bool>> predicate);

        IEnumerable<Plan> Get(
             Expression<Func<Plan, bool>> filter = null,
             Func<IQueryable<Plan>, IOrderedQueryable<Plan>> orderBy = null,
             string includeProperties = "");

       List<Program> GetPrograms();
    }
}
