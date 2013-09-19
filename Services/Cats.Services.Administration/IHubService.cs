using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Administration
{
    public interface IHubService : IDisposable
    {
        bool AddHub(Models.Hub hub);
        bool DeleteHub(Models.Hub hub);
        bool DeleteById(int id);
        bool EditHub(Models.Hub hub);
        Models.Hub FindById(int id);
        List<Models.Hub> GetAllHub();
        List<Models.Hub> FindBy(Expression<Func<Models.Hub, bool>> predicate);
        
        List<Models.Hub> GetAllWithoutId(int hubId);
        List<Models.Hub> GetOthersHavingSameOwner(Models.Hub hub);
        List<Models.Hub> GetOthersWithDifferentOwner(Models.Hub hub);
    }
}
