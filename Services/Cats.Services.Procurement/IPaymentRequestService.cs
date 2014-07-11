using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using System.Linq.Expressions;
namespace Cats.Services.Procurement
{
    public interface  IPaymentRequestService
    {
        bool Create(PaymentRequest item);

        PaymentRequest FindById(int id);
        List<PaymentRequest> GetAll();
        List<PaymentRequest> FindBy(Expression<Func<PaymentRequest, bool>> predicate);

        bool Update(PaymentRequest item);

        bool Delete(PaymentRequest item);
        bool DeleteById(int id);

        IEnumerable<PaymentRequest> Get(System.Linq.Expressions.Expression<Func<PaymentRequest, bool>> filter = null,
                                        Func<IQueryable<PaymentRequest>, IOrderedQueryable<PaymentRequest>> orderBy =
                                            null,
                                        string includeProperties = "");

    }
}