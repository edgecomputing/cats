using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Constant
{
  public enum WORKFLOW
  {
      REGIONAL_REQUEST=1,
      RELIEF_REQUISITION=2,
      TRANSPORT_REQUISITION=3,
      TRANSPORT_ORDER=4,
      HRD=5

      
  }

    public enum RegionalRequestStatus
    {
        Draft=1,
        Approved=2, 
        Closed=3,
        FederalApproved=4
     

    }

    public  enum ReliefRequisitionStatus
    {
        Draft=1,
        Approved=2,
        HubAssigned=3,
        ProjectCodeAssigned=4,
        TransportRequisitionCreated=5,
        TransportOrderCreated=6,
    }
    public enum TransportRequisitionStatus
    {
        Draft=1,
        Approved=2,
        Closed=3

    }

    public enum TransportOrderStatus
    {
        Draft=1,
        Approved=2,
        Signed=3,
        Closed=4
    }
    public enum HRDStatus
    {
        Draft=1,
        Approved=2,
        Published=3
    }
}
