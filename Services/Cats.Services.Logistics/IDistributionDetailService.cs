
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface IDistributionDetailService
    {

        bool AddDistributionDetail(DistributionDetail distributionDetail);
        bool DeleteDistributionDetail(DistributionDetail distributionDetail);
        bool DeleteById(int id);
        bool EditDistributionDetail(DistributionDetail distributionDetail);
        DistributionDetail FindById(Guid id);
        List<DistributionDetail> GetAllDistributionDetail();
        List<DistributionDetail> FindBy(Expression<Func<DistributionDetail, bool>> predicate);
        IEnumerable<DistributionDetail> Get(
            Expression<Func<DistributionDetail, bool>> filter = null,
            Func<IQueryable<DistributionDetail>, IOrderedQueryable<DistributionDetail>> orderBy = null,
            string includeProperties = "");

    }
}


