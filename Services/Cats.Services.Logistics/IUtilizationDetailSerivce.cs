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

        bool AddDetailDistribution(WoredaStockDistributionDetail DetailDistribution);
        bool DeleteDetailDistribution(WoredaStockDistributionDetail DetailDistribution);
        bool DeleteById(int id);
        bool EditDetailDistribution(WoredaStockDistributionDetail DetailDistribution);
        WoredaStockDistributionDetail FindById(Guid id);
        List<WoredaStockDistributionDetail> GetAllDetailDistribution();
        List<WoredaStockDistributionDetail> FindBy(Expression<Func<WoredaStockDistributionDetail, bool>> predicate);
        IEnumerable<WoredaStockDistributionDetail> Get(
            Expression<Func<WoredaStockDistributionDetail, bool>> filter = null,
            Func<IQueryable<WoredaStockDistributionDetail>, IOrderedQueryable<WoredaStockDistributionDetail>> orderBy = null,
            string includeProperties = "");

    }
}
