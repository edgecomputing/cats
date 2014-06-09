using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Administration
{
   public interface ILossReasonService:IDisposable
    {
        bool AddLossReason(LossReason lossReason);
        bool DeleteLossReason(LossReason lossReason);
        bool DeleteById(int id);
        bool EditLossReason(LossReason lossReason);
        LossReason FindById(int id);
        List<LossReason> GetAllLossReason();
        List<LossReason> FindBy(Expression<Func<LossReason, bool>> predicate);

        IEnumerable<LossReason> Get(System.Linq.Expressions.Expression<Func<LossReason, bool>> filter = null,
                             Func<IQueryable<LossReason>, IOrderedQueryable<LossReason>> orderBy = null,
                             string includeProperties = "");
    
    }
}
