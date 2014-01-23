using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Services.Logistics;

namespace Cats.Helpers
{
    public class DistributionHelper
    {
        private static  Cats.Services.Logistics.ISIPCAllocationService _sipcAllocationService;
        private static Cats.Services.Logistics.DistributionDetailService _distributionDetailService;

        public DistributionHelper(ISIPCAllocationService sipcAllocationService, DistributionDetailService distributionDetailService)
        {
            _sipcAllocationService = sipcAllocationService;
            _distributionDetailService = distributionDetailService;
        }

        public static decimal GetAllocated(int requisitionId, int fdiPid)
        {
            try
            {
                var allocatedAmount = _sipcAllocationService.FindBy(s => s.RequisitionDetailID == requisitionId && s.FDPID == fdiPid).Select(p => p.AllocatedAmount).SingleOrDefault();
                return allocatedAmount;
            }
            catch (Exception)
            {

                return 0;
            }
            
        }
        public static decimal GetReceived(string requisitionNo, int fdpId)
        {
            try
            {
                var receivedAtFdp =
               _distributionDetailService.FindBy(
                   r => r.Distribution.RequisitionNo == requisitionNo && r.Distribution.FDPID == fdpId).Sum(
                       r => r.ReceivedQuantity);

                return receivedAtFdp;
            }
            catch (Exception)
            {

                return 0;
            }
           
        }

        public static decimal GetDispatched(int requisitionId,int fdpid)
        {
            return 600;
        }
    }
}