using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class DispatchAllocation
    {
        [Key]
      public Guid DispatchAllocationID {get; set;}
        [Required]
      public Int32? PartitionID {get; set;}
      public Int32? HubID {get; set;}
        [Required]
      public int? StoreID {get; set;}
      public int? Year {get; set;}
      public int? Month {get; set;}
      public int? Round {get; set;}
      public int? DonorID {get; set;}
      public int ?ProgramID {get; set;}
      public int CommodityID {get; set;}
      public string RequisitionNo {get; set;}
      public string BidRefNo {get; set;}
      public DateTime? ContractStartDate {get; set;}
      public DateTime? ContractEndDate { get; set; }
      public int? Beneficiery {get; set;}
      public decimal Amount {get; set;}
      public int Unit {get; set;}
      public int? TransporterID {get; set;}
      public int FDPID {get; set;}
      public int? ShippingInstructionID {get; set;}
      public int? ProjectCodeID {get; set;}
      public char IsClosed {get; set;}
    }
}
