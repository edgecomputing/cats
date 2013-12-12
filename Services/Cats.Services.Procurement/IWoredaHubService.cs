using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public interface IWoredaHubService : IDisposable
    {
        bool AddWoredaHub(WoredaHub woredaHub);
        bool DeleteWoredaHub(WoredaHub woredaHub);
        bool DeleteById(int id);
        bool EditWoredaHub(WoredaHub woredaHub);
        WoredaHub FindById(int id);
        List<WoredaHub> GetAllWoredaHub();
        List<WoredaHub> FindBy(Expression<Func<WoredaHub, bool>> predicate);
        IEnumerable<WoredaHub> Get(
                   Expression<Func<WoredaHub, bool>> filter = null,
                   Func<IQueryable<WoredaHub>, IOrderedQueryable<WoredaHub>> orderBy = null,
                   string includeProperties = "");
    }
}
