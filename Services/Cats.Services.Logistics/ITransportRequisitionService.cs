using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Models.ViewModels;

namespace Cats.Services.Logistics
{
    public interface ITransportRequisitionService : IDisposable
    {
        bool AddTransportRequisition(TransportRequisition transportRequisition);

        bool DeleteTransportRequisition(TransportRequisition transportRequisition);
        bool DeleteById(int id);
        bool EditTransportRequisition(TransportRequisition transportRequisition);
        TransportRequisition FindById(int id);
        List<TransportRequisition> GetAllTransportRequisition();
        List<TransportRequisition> FindBy(Expression<Func<TransportRequisition, bool>> predicate);

        IEnumerable<TransportRequisition> Get(
                   Expression<Func<TransportRequisition, bool>> filter = null,
                   Func<IQueryable<TransportRequisition>, IOrderedQueryable<TransportRequisition>> orderBy = null,
                   string includeProperties = "");
        TransportRequisition CreateTransportRequisition(List<int> reliefRequisitions);
        IEnumerable<RequisitionToDispatch> GetRequisitionToDispatch();
        bool ApproveTransportRequisition(int id);
    }
}


