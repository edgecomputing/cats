using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public interface IStatusService
    {
        bool AddStatus(Status status);
        bool DeleteStatus(Status status);
        bool DeleteById(int id);
        bool EditStatus(Status status);
        Status FindById(int id);
        List<Status> GetAllStatus();
        List<Status> FindBy(Expression<Func<Status, bool>> predicate);

        IEnumerable<Status> Get(
             Expression<Func<Status, bool>> filter = null,
             Func<IQueryable<Status>, IOrderedQueryable<Status>> orderBy = null,
             string includeProperties = "");
    }
}
