

      using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
      using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IRegionalRequestService
   {
       
       bool AddReliefRequistion(RegionalRequest reliefRequistion);
       bool DeleteReliefRequistion(RegionalRequest reliefRequistion);
       bool DeleteById(int id);
       bool EditReliefRequistion(RegionalRequest reliefRequistion);
       RegionalRequest FindById(int id);
       List<RegionalRequest> GetAllReliefRequistion();
       List<RegionalRequest> FindBy(Expression<Func<RegionalRequest, bool>> predicate);

 IEnumerable<RegionalRequest> Get(
           Expression<Func<RegionalRequest, bool>> filter = null,
           Func<IQueryable<RegionalRequest>, IOrderedQueryable<RegionalRequest>> orderBy = null,
           string includeProperties = "");

        List<int?> GetZonesFoodRequested(int requestId);
   }
}


      

