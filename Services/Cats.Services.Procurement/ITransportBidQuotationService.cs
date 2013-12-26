using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public interface ITransportBidQuotationService
    {
        bool AddTransportBidQuotation(TransportBidQuotation item);

       
        bool UpdateTransportBidQuotation(TransportBidQuotation item);

        bool DeleteTransportBidQuotation(TransportBidQuotation item);
        bool DeleteById(int id);

        TransportBidQuotation FindById(int id);
        List<TransportBidQuotation> GetAllTransportBidQuotation();
        List<TransportBidQuotation> FindBy(Expression<Func<TransportBidQuotation, bool>> predicate);

        IEnumerable<TransportBidQuotation> Get(
            System.Linq.Expressions.Expression<Func<TransportBidQuotation, bool>> filter = null,
            Func<IQueryable<TransportBidQuotation>, IOrderedQueryable<TransportBidQuotation>> orderBy = null,
            string includeProperties = "");
    }
}