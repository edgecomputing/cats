using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;

namespace Cats.Services.Logistics
{
    public class TransportRequisitionService : ITransportRequisitionService
    {
        private IUnitOfWork _unitOfWork;

        public TransportRequisitionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }



        public List<Models.TransportRequisition> CreateTransportRequisitions(List<int> reliefRequisitions)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
