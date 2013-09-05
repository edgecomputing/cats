using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
   public  interface ICurrencyService:IDisposable
    {

       bool AddCurrency(Currency currency);
       bool DeleteCurrency(Currency currency);
        bool DeleteById(int id);
        bool EditCurrency(Currency currency);
        Currency FindById(int id);
        List<Currency> GetAllCurrency();
        List<Currency> FindBy(Expression<Func<Currency, bool>> predicate);
        IEnumerable<Currency> Get(
                   Expression<Func<Currency, bool>> filter = null,
                   Func<IQueryable<Currency>, IOrderedQueryable<Currency>> orderBy = null,
                   string includeProperties = "");
    }
}
