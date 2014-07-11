
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IRationService : IDisposable
    {

        bool AddRation(Ration ration);
        bool DeleteRation(Ration ration);
        bool DeleteById(int id);
        bool EditRation(Ration ration);
        Ration FindById(int id);
        List<Ration> GetAllRation();
        List<Ration> FindBy(Expression<Func<Ration, bool>> predicate);
        void SetDefault(int rationId);
        IEnumerable<Ration> Get(
                   Expression<Func<Ration, bool>> filter = null,
                   Func<IQueryable<Ration>, IOrderedQueryable<Ration>> orderBy = null,
                   string includeProperties = "");
    }
}


