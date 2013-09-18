using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class DispatchAllocation
    {
        public DispatchAllocation()
        {
            this.Dispatches = new List<Dispatch>();
        }
        [Key]
        public System.Guid DispatchAllocationID { get; set; }
        public int PartitionID { get; set; }
        public int HubID { get; set; }
        public Nullable<int> StoreID { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Round { get; set; }
        public Nullable<int> DonorID { get; set; }
        public Nullable<int> ProgramID { get; set; }
        public int CommodityID { get; set; }
        public string RequisitionNo { get; set; }
        public string BidRefNo { get; set; }
        public Nullable<System.DateTime> ContractStartDate { get; set; }
        public Nullable<System.DateTime> ContractEndDate { get; set; }
        public Nullable<int> Beneficiery { get; set; }
        public decimal Amount { get; set; }
        public int Unit { get; set; }
        public Nullable<int> TransporterID { get; set; }
        public int FDPID { get; set; }
        public Nullable<int> ShippingInstructionID { get; set; }
        public Nullable<int> ProjectCodeID { get; set; }
        public bool IsClosed { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual ICollection<Dispatch> Dispatches { get; set; }
        public virtual FDP FDP { get; set; }
        public virtual Program Program { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual ProjectCode ProjectCode { get; set; }
        public virtual ShippingInstruction ShippingInstruction { get; set; }
        public virtual Transporter Transporter { get; set; }
    }
}
