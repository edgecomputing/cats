using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class RequestAllocationViewModel
    {
        public int RequisitionId{ get; set; }
        public string Commodity { get; set; }
        public decimal Amount { get; set; }
        public string RegionName { get; set; }
        public string ZoneName { get; set; }
        public List<SIAllocation> SIAllocations {get;set;}
    }
    public class SIAllocation
    {
        public int ShippingInstructionId { get; set; }
        public string ShippingInstructionCode { get; set; }
        public double AllocatedAmount { get; set; }
    }
}