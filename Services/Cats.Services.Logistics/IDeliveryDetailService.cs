
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

        bool AddDistributionDetail(DeliveryDetail distributionDetail);
        bool DeleteDistributionDetail(DeliveryDetail distributionDetail);
        bool DeleteById(int id);
        bool EditDistributionDetail(DeliveryDetail distributionDetail);
        DeliveryDetail FindById(Guid id);
        List<DeliveryDetail> GetAllDistributionDetail();
        List<DeliveryDetail> FindBy(Expression<Func<DeliveryDetail, bool>> predicate);
        IEnumerable<DeliveryDetail> Get(
            Expression<Func<DeliveryDetail, bool>> filter = null,
            Func<IQueryable<DeliveryDetail>, IOrderedQueryable<DeliveryDetail>> orderBy = null,
            string includeProperties = "");

    }
}


