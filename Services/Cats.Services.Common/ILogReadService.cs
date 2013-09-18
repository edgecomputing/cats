using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Services.Common
{
    public interface ILogReadService : IDisposable
    {
        List<Cats.Models.Log> GetAllLog();
        List<Cats.Models.Log> FindBy(Expression<Func<Cats.Models.Log, bool>> predicate);
        IEnumerable<Cats.Models.Log> Get(
                   Expression<Func<Cats.Models.Log, bool>> filter = null,
                   Func<IQueryable<Cats.Models.Log>, IOrderedQueryable<Cats.Models.Log>> orderBy = null,
                   string includeProperties = "");
    }
}
