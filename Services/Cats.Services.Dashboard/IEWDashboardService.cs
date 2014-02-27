using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Models.Constant;

namespace Cats.Services.Dashboard
{
  public  interface IEWDashboardService:IDisposable
    {
      List<HRD> FindByHrd(Expression<Func<HRD, bool>> predicate);
      List<RationDetail> FindByRationDetail(Expression<Func<RationDetail, bool>> predicate);
      List<RegionalRequest> FindByRequest(Expression<Func<RegionalRequest, bool>> predicate);
      string GetStatusName(WORKFLOW workflow, int statusId);
      List<ReliefRequisition> FindByRequisition(Expression<Func<ReliefRequisition, bool>> predicate);
      List<ReliefRequisition> GetAllReliefRequisition();
      int GetRemainingRequest(int regionID, int planID);
    }
}
