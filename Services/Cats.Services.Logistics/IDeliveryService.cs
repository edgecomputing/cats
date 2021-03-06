﻿
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

        bool AddDelivery(Delivery distribution);
        bool DeleteDelivery(Delivery distribution);
        bool DeleteById(int id);
        bool EditDelivery(Delivery distribution);
        Delivery FindById(Guid id);
        List<Delivery> GetAllDelivery();
        List<Delivery> FindBy(Expression<Func<Delivery, bool>> predicate);

        IEnumerable<Delivery> Get(
            Expression<Func<Delivery, bool>> filter = null,
            Func<IQueryable<Delivery>, IOrderedQueryable<Delivery>> orderBy = null,
            string includeProperties = "");

        decimal GetFDPDelivery(int transportOrderId, int fdpId);
        int? GetDonorID(string shippingInstruction);

    }
}


