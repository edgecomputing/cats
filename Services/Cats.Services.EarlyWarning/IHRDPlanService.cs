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
        bool AddHRDPlan(HRDPlan hrdPlan);
        bool DeleteHRDPlan(HRDPlan hrdPlan);
        bool DeleteById(int id);
        bool EditHRDPlan(HRDPlan hrdPlan);
        HRDPlan FindById(int id);
        List<HRDPlan> GetAllHRDPlan();
        List<HRDPlan> FindBy(Expression<Func<HRDPlan, bool>> predicate);

        IEnumerable<HRDPlan> Get(
             Expression<Func<HRDPlan, bool>> filter = null,
             Func<IQueryable<HRDPlan>, IOrderedQueryable<HRDPlan>> orderBy = null,
             string includeProperties = "");

       List<Program> GetPrograms();
    }
}
