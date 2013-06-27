
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

        bool AddRegionalRequestDetail(RegionalRequestDetail regionalRequestDetail);
        bool DeleteRegionalRequestDetail(RegionalRequestDetail regionalRequestDetail);
        bool DeleteById(int id);
        bool EditRegionalRequestDetail(RegionalRequestDetail regionalRequestDetail);
        RegionalRequestDetail FindById(int id);
        List<RegionalRequestDetail> GetAllRegionalRequestDetail();
        List<RegionalRequestDetail> FindBy(Expression<Func<RegionalRequestDetail, bool>> predicate);
        bool Save();

    }
}


