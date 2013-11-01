using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Services.Common;
namespace Cats.Areas.Logistics.Models
{
    public class RequestAllocationViewModel
    {
        public int RequisitionId{ get; set; }
        public int CommodityId { get; set; }
        public string Commodity { get; set; }
        public decimal Amount { get; set; }
        public string RegionName { get; set; }
        public string ZoneName { get; set; }
        public List<SIAllocation> SIAllocations {get;set;}
        public FreeSIPC FreeSIPCCodes { get; set; }
        public int HubAllocationID { get; set; }
        public string HubName { get; set; }
    }
    public class SIAllocation
    {
        public int ShippingInstructionId { get; set; }
        public string ShippingInstructionCode { get; set; }
        public double AllocatedAmount { get; set; }
        public int AllocationId { get; set; }
        public string AllocationType { get; set; }
        
    }
    public class FreeSIPC
    {
        public List<LedgerService.AvailableShippingCodes> FreeSICodes { get; set; }
        public List<LedgerService.AvailableProjectCodes> FreePCCodes { get; set; }
    }
    public class AllocationAction
    {
        public string Action {get;set;}
        public int? AllocationId {get;set;}
        public int RequisitionId {get;set;}
        public int ShippingInstructionId {get;set;}
        public double AllocatedAmount { get; set; }
        public int HubAllocationID { get; set; }
        public string AllocationType { get; set; }
    }
}