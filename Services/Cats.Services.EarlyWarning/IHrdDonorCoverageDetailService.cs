using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
   public interface IHrdDonorCoverageDetailService:IDisposable
    {
        bool AddHrdDonorCoverageDetail(HrdDonorCoverageDetail hrdDonorCoverageDetail);
        bool DeleteHrdDonorCoverageDetail(HrdDonorCoverageDetail hrdDonorCoverageDetail);
        bool DeleteById(int id);
        bool EditHrdDonorCoverageDetail(HrdDonorCoverageDetail hrdDonorCoverageDetail);
        HrdDonorCoverageDetail FindById(int id);
        List<HrdDonorCoverageDetail> GetAllHrdDonorCoverageDetail();
        List<HrdDonorCoverageDetail> FindBy(Expression<Func<HrdDonorCoverageDetail, bool>> predicate);

        IEnumerable<HrdDonorCoverageDetail> Get(
             Expression<Func<HrdDonorCoverageDetail, bool>> filter = null,
             Func<IQueryable<HrdDonorCoverageDetail>, IOrderedQueryable<HrdDonorCoverageDetail>> orderBy = null,
             string includeProperties = "");
    }
}
