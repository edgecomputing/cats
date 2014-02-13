using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface IDeliveryReconcileService
    {
        bool AddDeliveryReconcile(DeliveryReconcile deliveryReconcile);
        bool DeleteDeliveryReconcile(DeliveryReconcile deliveryReconcile);
        bool DeleteById(int id);
        bool EditDeliveryReconcile(DeliveryReconcile deliveryReconcile);
        DeliveryReconcile FindById(int id);
        List<DeliveryReconcile> GetAllDeliveryReconciles();
        List<DeliveryReconcile> FindBy(Expression<Func<DeliveryReconcile, bool>> predicate);
        IEnumerable<DeliveryReconcile> Get(
                   Expression<Func<DeliveryReconcile, bool>> filter = null,
                   Func<IQueryable<DeliveryReconcile>, IOrderedQueryable<DeliveryReconcile>> orderBy = null,
                   string includeProperties = "");
    }
}
