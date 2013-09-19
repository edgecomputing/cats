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
        bool AddHub(Models.Hub.Hub hub);
        bool DeleteHub(Models.Hub.Hub hub);
        bool DeleteById(int id);
        bool EditHub(Models.Hub.Hub hub);
        Models.Hub.Hub FindById(int id);
        List<Models.Hub.Hub> GetAllHub();
        List<Models.Hub.Hub> FindBy(Expression<Func<Models.Hub.Hub, bool>> predicate);
        
        List<Models.Hub.Hub> GetAllWithoutId(int hubId);
        List<Models.Hub.Hub> GetOthersHavingSameOwner(Models.Hub.Hub hub);
        List<Models.Hub.Hub> GetOthersWithDifferentOwner(Models.Hub.Hub hub);
    }
}
