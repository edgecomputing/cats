
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface IDeliveryDetailService
    {

        bool AddDeliveryDetail(DeliveryDetail deliveryDetail);
        bool DeleteDeliveryDetail(DeliveryDetail deliveryDetail);
        bool DeleteById(int id);
        bool EditDeliveryDetail(DeliveryDetail deliveryDetail);
        DeliveryDetail FindById(Guid id);
        List<DeliveryDetail> GetAllDeliveryDetail();
        List<DeliveryDetail> FindBy(Expression<Func<DeliveryDetail, bool>> predicate);
        IEnumerable<DeliveryDetail> Get(
            Expression<Func<DeliveryDetail, bool>> filter = null,
            Func<IQueryable<DeliveryDetail>, IOrderedQueryable<DeliveryDetail>> orderBy = null,
            string includeProperties = "");

    }
}


