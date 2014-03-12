
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
        IEnumerable<RegionalRequestDetail> Get(
                 Expression<Func<RegionalRequestDetail, bool>> filter = null,
                 Func<IQueryable<RegionalRequestDetail>, IOrderedQueryable<RegionalRequestDetail>> orderBy = null,
                 string includeProperties = "");

        bool AddRegionalRequestDetailWithBeneficiary(RegionalRequestDetail regionalRequestDetail);
        bool AddRequestDetailCommodity(int commodityId, int requestId);
        bool AddAllCommodity(int regionalRequestID);
        bool DeleteRequestDetailCommodity(int commodityId, int requestId);
        bool UpdateRequestDetailCommodity(int commodityId, int requestCommodityId);
        bool AddCommodityFdp(RegionalRequestDetail requestDetail);
        bool Save();

    }
}


