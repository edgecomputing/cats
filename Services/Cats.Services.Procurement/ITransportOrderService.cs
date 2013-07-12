
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;
using Cats.Models.ViewModels;

namespace Cats.Services.Procurement
{
    public interface ITransportOrderService : IDisposable
    {

        bool AddTransportOrder(TransportOrder transportOrder);
        bool DeleteTransportOrder(TransportOrder transportOrder);
        bool DeleteById(int id);
        bool EditTransportOrder(TransportOrder transportOrder);
        TransportOrder FindById(int id);
        List<TransportOrder> GetAllTransportOrder();
        List<TransportOrder> FindBy(Expression<Func<TransportOrder, bool>> predicate);

        IEnumerable<TransportOrder> Get(
                   Expression<Func<TransportOrder, bool>> filter = null,
                   Func<IQueryable<TransportOrder>, IOrderedQueryable<TransportOrder>> orderBy = null,
                   string includeProperties = "");

        //IEnumerable<RequisitionToDispatch> GetRequisitionToDispatch();
        //IEnumerable<ReliefRequisition> GetProjectCodeAssignedRequisitions();
        IEnumerable<TransportOrder> CreateTransportOrder(IEnumerable<int> requisitions);
        List<vwTransportOrder> GeTransportOrderRpt(int id);
    }
}


