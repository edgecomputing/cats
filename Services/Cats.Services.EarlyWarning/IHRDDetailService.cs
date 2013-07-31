using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IHRDDetailService : IDisposable
    {
        bool AddHRDDetail(HRDDetail hrdDetail);
        bool DeleteHRDDetail(HRDDetail hrdDetail);
        bool DeleteById(int id);
        bool EditHRDDetail(HRDDetail hrdDetail);
        HRDDetail FindById(int id);
        List<HRDDetail> GetAllHRDDetail();
        List<HRDDetail> FindBy(Expression<Func<HRDDetail, bool>> predicate);

        IEnumerable<HRDDetail> Get(
                   Expression<Func<HRDDetail, bool>> filter = null,
                   Func<IQueryable<HRDDetail>, IOrderedQueryable<HRDDetail>> orderBy = null,
                   string includeProperties = "");
    }
}
