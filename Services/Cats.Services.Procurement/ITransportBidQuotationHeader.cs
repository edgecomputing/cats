using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Procurement
{
   public interface ITransportBidQuotationHeader
    {
        bool AddTransportBidQuotationHeader(TransportBidQuotationHeader transportBidQuotationHeader);
        bool UpdateTransportBidQuotationHeader(TransportBidQuotationHeader transportBidQuotationHeader);

        bool DeleteTransportBidQuotationHeader(TransportBidQuotationHeader transportBidQuotationHeader);
        bool DeleteById(int id);

        TransportBidQuotationHeader FindById(int id);
        List<TransportBidQuotationHeader> GetAllTransportBidQuotationHeader();
        List<TransportBidQuotationHeader> FindBy(Expression<Func<TransportBidQuotationHeader, bool>> predicate);

        IEnumerable<TransportBidQuotationHeader> Get(
            System.Linq.Expressions.Expression<Func<TransportBidQuotationHeader, bool>> filter = null,
            Func<IQueryable<TransportBidQuotationHeader>, IOrderedQueryable<TransportBidQuotationHeader>> orderBy = null,
            string includeProperties = "");
    }
}
