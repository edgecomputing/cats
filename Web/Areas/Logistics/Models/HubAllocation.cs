using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class HubAllocation
    {
        public int HubAllocationID { get; set; }
        public int RequisitionID { get; set; }
        public int HubID { get; set; }
        public System.DateTime AllocationDate { get; set; }
        public int AllocatedBy { get; set; }
        public virtual Hub Hub { get; set; }

    }
}