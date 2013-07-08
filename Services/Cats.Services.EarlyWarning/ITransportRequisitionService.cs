
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface ITransportRequisitionService
    {

        bool AddTransportRequisition(TransportRequisition transportRequisition);
        bool DeleteTransportRequisition(TransportRequisition transportRequisition);
        bool DeleteById(int id);
        bool EditTransportRequisition(TransportRequisition transportRequisition);
        TransportRequisition FindById(int id);
        List<TransportRequisition> GetAllTransportRequisition();
        List<TransportRequisition> FindBy(Expression<Func<TransportRequisition, bool>> predicate);


    }
}


