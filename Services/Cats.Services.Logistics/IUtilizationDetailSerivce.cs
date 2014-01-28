using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Logistics
{
   public interface IUtilizationDetailSerivce
    {

        bool AddDetailDistribution(UtilizationDetail DetailDistribution);
        bool DeleteDetailDistribution(UtilizationDetail DetailDistribution);
        bool DeleteById(int id);
        bool EditDetailDistribution(UtilizationDetail DetailDistribution);
        UtilizationDetail FindById(Guid id);
        List<UtilizationDetail> GetAllDetailDistribution();
        List<UtilizationDetail> FindBy(Expression<Func<UtilizationDetail, bool>> predicate);
        IEnumerable<UtilizationDetail> Get(
            Expression<Func<UtilizationDetail, bool>> filter = null,
            Func<IQueryable<UtilizationDetail>, IOrderedQueryable<UtilizationDetail>> orderBy = null,
            string includeProperties = "");

    }
}
