using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
   public interface IContributionService:IDisposable
    {
        bool AddContribution(Contribution contribution);
        bool DeleteContribution(Contribution contribution);
        bool DeleteById(int id);
        bool EditContribution(Contribution contribution);
        Contribution FindById(int id);
        List<Contribution> GetAllContribution();
        List<Contribution> FindBy(Expression<Func<Contribution, bool>> predicate);
        IEnumerable<Contribution> Get(
                   Expression<Func<Contribution, bool>> filter = null,
                   Func<IQueryable<Contribution>, IOrderedQueryable<Contribution>> orderBy = null,
                   string includeProperties = "");
    }
}
