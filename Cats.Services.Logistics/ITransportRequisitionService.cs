using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface  ITransportRequisitionService
    {
        List<TransportRequisition> CreateTransportRequisitions(List<int> reliefRequisitions);
        bool Save();
    }
}
