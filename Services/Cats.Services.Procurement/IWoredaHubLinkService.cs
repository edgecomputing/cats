using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public interface IWoredaHubLinkService : IDisposable
    {
        bool AddWoredaHubLink(WoredaHubLink woredaHubLink);
        bool DeleteWoredaHubLink(WoredaHubLink woredaHubLink);
        bool DeleteById(int id);
        bool EditWoredaHubLink(WoredaHubLink woredaHubLink);
        WoredaHubLink FindById(int id);
        List<WoredaHubLink> GetAllWoredaHubLink();
        List<WoredaHubLink> FindBy(Expression<Func<WoredaHubLink, bool>> predicate);
        IEnumerable<WoredaHubLink> Get(
                   Expression<Func<WoredaHubLink, bool>> filter = null,
                   Func<IQueryable<WoredaHubLink>, IOrderedQueryable<WoredaHubLink>> orderBy = null,
                   string includeProperties = "");
    }
}
