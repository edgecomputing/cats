
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Cats.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> FindBy(Expression<Func<T, bool>> predicate);
        bool Add(T entity);
        bool Delete(T entity);
        bool Edit(T entity);
        T FindById(int id);


    }
}
