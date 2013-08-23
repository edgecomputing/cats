using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface ISeasonService:IDisposable
    {
        bool AddSeason(Season season);
        bool DeleteSeason(Season season);
        bool DeleteById(int id);
        bool EditSeason(Season season);
        Season FindById(int id);
        List<Season> GetAllSeason();
        List<Season> FindBy(Expression<Func<Season, bool>> predicate);
        IEnumerable<Season> Get(
                   Expression<Func<Season, bool>> filter = null,
                   Func<IQueryable<Season>, IOrderedQueryable<Season>> orderBy = null,
                   string includeProperties = "");
    }
}
