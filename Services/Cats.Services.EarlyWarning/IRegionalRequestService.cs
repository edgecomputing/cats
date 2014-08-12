

      using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
      using Cats.Models;
      using Cats.Models.Security;

namespace Cats.Services.EarlyWarning
{
    public interface IRegionalRequestService
   {
       
       bool AddRegionalRequest(RegionalRequest regionalRequest);
       bool DeleteRegionalRequest(RegionalRequest regionalRequest);
       bool DeleteById(int id);
       bool EditRegionalRequest(RegionalRequest regionalRequest);
       RegionalRequest FindById(int id);
       List<RegionalRequest> GetAllRegionalRequest();
       List<RegionalRequest> FindBy(Expression<Func<RegionalRequest, bool>> predicate);

 IEnumerable<RegionalRequest> Get(
           Expression<Func<RegionalRequest, bool>> filter = null,
           Func<IQueryable<RegionalRequest>, IOrderedQueryable<RegionalRequest>> orderBy = null,
           string includeProperties = "");

        List<int?> GetZonesFoodRequested(int requestId);
        List<RegionalRequest> GetSubmittedRequest(int region, int month,int status);
        HRDPSNPPlanInfo PlanToRequest(HRDPSNPPlan plan);

        bool ApproveRequest(int id, Cats.Models.Security.UserInfo userInfo);
        bool RejectRequest(int id, Cats.Models.Security.UserInfo userInfo);
        bool DraftRequest(int id, Models.Security.UserInfo userInfo);
        bool RevertRequestStatus(int id);
   }
}


      

