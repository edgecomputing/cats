using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
   public interface IInkindContributionDetailService:IDisposable
    {
        bool AddInKindContributionDetail(InKindContributionDetail inKindContributionDetail);
        bool DeleteInKindContributionDetail(InKindContributionDetail inKindContributionDetail);
        bool DeleteById(int id);
        bool EditInKindContributionDetail(InKindContributionDetail inKindContributionDetail);
        InKindContributionDetail FindById(int id);
        List<InKindContributionDetail> GetAllInKindContributionDetail();
        List<InKindContributionDetail> FindBy(Expression<Func<InKindContributionDetail, bool>> predicate);
        IEnumerable<InKindContributionDetail> Get(
                   Expression<Func<InKindContributionDetail, bool>> filter = null,
                   Func<IQueryable<InKindContributionDetail>, IOrderedQueryable<InKindContributionDetail>> orderBy = null,
                   string includeProperties = "");
    }
}
