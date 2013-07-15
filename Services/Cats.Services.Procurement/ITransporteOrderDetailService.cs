using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public interface ITransportOrderDetailService
    {

        bool AddTransportOrderDetail(TransportOrderDetail transportOrderDetail);
        bool DeleteTransportOrderDetail(TransportOrderDetail transportOrderDetail);
        bool DeleteById(int id);
        bool EditTransportOrderDetail(TransportOrderDetail transportOrderDetail);
        TransportOrderDetail FindById(int id);
        List<TransportOrderDetail> GetAllTransportOrderDetail();
        List<TransportOrderDetail> FindBy(Expression<Func<TransportOrderDetail, bool>> predicate);
        IEnumerable<TransportOrderDetail> Get(
                  Expression<Func<TransportOrderDetail, bool>> filter = null,
                  Func<IQueryable<TransportOrderDetail>, IOrderedQueryable<TransportOrderDetail>> orderBy = null,
                  string includeProperties = "");

    }
}