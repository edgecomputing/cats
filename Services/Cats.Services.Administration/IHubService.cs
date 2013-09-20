using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Models;
using System.Threading.Tasks;

namespace Cats.Services.Administration
{
   public interface IHubService:IDisposable
    {
        bool AddHub(Hub hub);
        bool DeleteHub(Hub hub);
        bool DeleteById(int id);
        bool EditHub(Hub hub);
        Hub FindById(int id);
        List<Hub> GetAllHub();
        List<Hub> FindBy(Expression<Func<Hub, bool>> predicate);
    }
}
