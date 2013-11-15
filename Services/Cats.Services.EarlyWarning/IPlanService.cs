using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
   public interface IPlanService:IDisposable
    {
        bool AddPlan(Plan plan);
        bool DeletePlan(Plan plan);
        bool DeleteById(int id);
        bool EditPlan(Plan plan);
        Plan FindById(int id);
        List<Plan> GetAllPlan();
        List<Plan> FindBy(Expression<Func<Plan, bool>> predicate);

        IEnumerable<Plan> Get(
             Expression<Func<Plan, bool>> filter = null,
             Func<IQueryable<Plan>, IOrderedQueryable<Plan>> orderBy = null,
             string includeProperties = "");

       List<Program> GetPrograms();
       void AddNeedAssessmentPlan(NeedAssessment needAssessment);
       void AddPlan(string planName, DateTime startDate, DateTime endDate);
       void AddHRDPlan(string planName, DateTime startDate, DateTime endDate);
       List<NeedAssessment> PlannedNeedAssessment(int planID);
       List<HRD> PlannedHRD(int planID);
    }
}
