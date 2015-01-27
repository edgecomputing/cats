using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface ITransporterPaymentRequestService : IDisposable
    {
        bool AddTransporterPaymentRequest(TransporterPaymentRequest transporterPaymentRequest);
        bool DeleteTransporterPaymentRequest(TransporterPaymentRequest transporterPaymentRequest);
        bool DeleteById(int id);
        bool EditTransporterPaymentRequest(TransporterPaymentRequest transporterPaymentRequest);
        TransporterPaymentRequest FindById(int id);
        List<TransporterPaymentRequest> GetAllTransporterPaymentRequest();
        List<TransporterPaymentRequest> FindBy(Expression<Func<TransporterPaymentRequest, bool>> predicate);
        IEnumerable<TransporterPaymentRequest> Get(
                   Expression<Func<TransporterPaymentRequest, bool>> filter = null,
                   Func<IQueryable<TransporterPaymentRequest>, IOrderedQueryable<TransporterPaymentRequest>> orderBy = null,
                   string includeProperties = "");

        bool Reject(TransporterPaymentRequest transporterPaymentRequest);
        int GetFinalState(int parentBusinessProcessID);
    }
}
