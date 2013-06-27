
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IRegionalRequestDetailService
    {

        bool AddReliefRequisitionDetail(RegionalRequestDetail Entity);
        bool DeleteReliefRequisitionDetail(RegionalRequestDetail Entity);
        bool DeleteById(int id);
        bool EditReliefRequisitionDetail(RegionalRequestDetail Entity);
        RegionalRequestDetail FindById(int id);
        List<RegionalRequestDetail> GetAllReliefRequisitionDetail();
        List<RegionalRequestDetail> FindBy(Expression<Func<RegionalRequestDetail, bool>> predicate);

        bool Save();
    }
}


      

