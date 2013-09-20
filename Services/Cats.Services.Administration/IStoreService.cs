using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Administration
{
   public interface IStoreService:IDisposable
    {
        bool AddStore(Store store);
        bool DeleteStore(Store store);
        bool DeleteById(int id);
        bool EditStore(Store store);
        Store FindById(int id);
        List<Store> GetAllStore();
        List<Store> FindBy(Expression<Func<Store, bool>> predicate);
    }
}
