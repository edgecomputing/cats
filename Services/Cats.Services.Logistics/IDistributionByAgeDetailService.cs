using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Logistics
{
   public interface IDistributionByAgeDetailService:IDisposable
    {
        bool AddDistributionByAgeDetail(DistributionByAgeDetail distributionByAgeDetail);
        bool DeleteDistributionByAgeDetail(DistributionByAgeDetail distributionByAgeDetail);
        bool DeleteById(int id);
        bool EditDistributionByAgeDetail(DistributionByAgeDetail distributionByAgeDetail);
        DistributionByAgeDetail FindById(int id);
        List<DistributionByAgeDetail> GetAllDistributionByAgeDetail();
        List<DistributionByAgeDetail> FindBy(Expression<Func<DistributionByAgeDetail, bool>> predicate);
    }
}
