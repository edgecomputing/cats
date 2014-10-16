using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Administration
{
    public interface IUserActivityService : IDisposable
    {
       
        Audit FindById(int id);
       
        List<Audit> GetAllUserActivity();
        List<Audit> FindBy(Expression<Func<Audit, bool>> predicate);
        IEnumerable<Audit> Get(Expression<Func<Audit, bool>> filter = null,
                             Func<IQueryable<Audit>, IOrderedQueryable<Audit>> orderBy = null,
                             string includeProperties = "");


    }   
}
