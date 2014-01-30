
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface IDeliveryService
    {

        bool AddDistribution(Delivery distribution);
        bool DeleteDistribution(Delivery distribution);
        bool DeleteById(int id);
        bool EditDistribution(Delivery distribution);
        Delivery FindById(int id);
        List<Delivery> GetAllDistribution();
        List<Delivery> FindBy(Expression<Func<Delivery, bool>> predicate);

        IEnumerable<Delivery> Get(
            Expression<Func<Delivery, bool>> filter = null,
            Func<IQueryable<Delivery>, IOrderedQueryable<Delivery>> orderBy = null,
            string includeProperties = "");

   

    }
}


