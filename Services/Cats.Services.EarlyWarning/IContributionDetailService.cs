using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IContributionDetailService:IDisposable
    {
        bool AddContributionDetail(ContributionDetail contributionDetail);
        bool DeleteContributionDetail(ContributionDetail contributionDetail);
        bool DeleteById(int id);
        bool EditContributionDetail(ContributionDetail contributionDetail);
        ContributionDetail FindById(int id);
        List<ContributionDetail> GetAllContributionDetail();
        List<ContributionDetail> FindBy(Expression<Func<ContributionDetail, bool>> predicate);
        IEnumerable<ContributionDetail> Get(
                   Expression<Func<ContributionDetail, bool>> filter = null,
                   Func<IQueryable<ContributionDetail>, IOrderedQueryable<ContributionDetail>> orderBy = null,
                   string includeProperties = "");
    }
}
