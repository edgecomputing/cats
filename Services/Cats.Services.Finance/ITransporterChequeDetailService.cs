using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Finance
{
    public interface ITransporterChequeDetailService : IDisposable
    {
        bool AddTransporterChequeDetail(TransporterChequeDetail transporterChequeDetail);
        bool DeleteTransporterChequeDetail(TransporterChequeDetail transporterChequeDetail);
        bool DeleteById(int id);
        bool EditTransporterChequeDetail(TransporterChequeDetail transporterChequeDetail);
        TransporterChequeDetail FindById(int id);
        List<TransporterChequeDetail> GetAllTransporterChequeDetail();
        List<TransporterChequeDetail> FindBy(Expression<Func<TransporterChequeDetail, bool>> predicate);
        IEnumerable<TransporterChequeDetail> Get(
                   Expression<Func<TransporterChequeDetail, bool>> filter = null,
                   Func<IQueryable<TransporterChequeDetail>, IOrderedQueryable<TransporterChequeDetail>> orderBy = null,
                   string includeProperties = "");
    }
}
