using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public interface ITransportBidPlanService
    {
        bool AddTransportBidPlan(TransportBidPlan item);
        bool UpdateTransportBidPlan(TransportBidPlan item);

        bool DeleteTransportBidPlan(TransportBidPlan item);
        bool DeleteById(int id);

        TransportBidPlan FindById(int id);
        List<TransportBidPlan> GetAllTransportBidPlan();
        List<TransportBidPlan> FindBy(Expression<Func<TransportBidPlan, bool>> predicate);
    }
}