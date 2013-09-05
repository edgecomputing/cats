
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cats.Localization.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> FindBy(Expression<Func<T, bool>> predicate);
        bool Add(T entity);
        bool Delete(T entity);
        bool Edit(T entity);
        T FindById(int id);
        T FindById(Guid id);

        IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "");
    }
}
