using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Cats.Service;
using Cats.Models;


namespace Cats
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EWDispatchService : IEWDispatchService
    {
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        // Add more operations here and mark them with [OperationContract]

        [OperationContract]
        IEnumerable<AllocationModelDetail> IEWDispatchService.GetAllocationModelDetail()
        {
            Data.UnitWork.IUnitOfWork repo = new Data.UnitWork.UnitOfWork();
            var EW_Data = repo.AllocationModelDetailRepository.GetAll();
            List<AllocationModelDetail> result = new List<Cats.Models.AllocationModelDetail>();
            foreach (var data in EW_Data)
            {
                result.Add(new AllocationModelDetail
                {
                    Beneficiaries=data.Beneficiaries,
                    FDPID=data.FDPID,
                    FDPName=data.FDPName,
                    Region=data.Region,
                    Woreda=data.Woreda,
                    Zone=data.Zone
                });
            }
            return result.AsEnumerable();
        }
    }
}
