using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Models.ViewModels.HRD;

namespace Cats.Services.EarlyWarning
{
    public interface IHRDService:IDisposable
    {
        bool AddHRD(HRD hrd);
        bool DeleteHRD(HRD hrd);
        bool DeleteById(int id);
        bool EditHRD(HRD hrd);
        HRD FindById(int id);
        List<HRD> GetAllHRD();
        List<HRD> FindBy(Expression<Func<HRD, bool>> predicate);
        IEnumerable<HRDDetail> GetHRDDetailByHRDID(int hrdID);
        IEnumerable<HRD> Get(
                   Expression<Func<HRD, bool>> filter = null,
                   Func<IQueryable<HRD>, IOrderedQueryable<HRD>> orderBy = null,
                   string includeProperties = "");

        List<Plan> GetPlans();
        Plan GetPlan(int id);
        void PublishHrd(int hrdId);
        bool AddHRDFromAssessment(HRD hrd);
    }
}
