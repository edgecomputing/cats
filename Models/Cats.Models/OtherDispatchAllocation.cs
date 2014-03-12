using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class OtherDispatchAllocation
    {
        public OtherDispatchAllocation()
        {
            this.Dispatches = new List<Dispatch>();
        }
        [Key]
        public System.Guid OtherDispatchAllocationID { get; set; }
        public int PartitionID { get; set; }
        public System.DateTime AgreementDate { get; set; }
        public int CommodityID { get; set; }
        public System.DateTime EstimatedDispatchDate { get; set; }
        public int HubID { get; set; }
        public int ToHubID { get; set; }
        public int ProgramID { get; set; }
        public int UnitID { get; set; }
        public decimal QuantityInUnit { get; set; }
        public decimal QuantityInMT { get; set; }
        public int ReasonID { get; set; }
        public string ReferenceNumber { get; set; }
        public string Remark { get; set; }
        public int ShippingInstructionID { get; set; }
        public int ProjectCodeID { get; set; }
        public Nullable<int> TransporterID { get; set; }
        public bool IsClosed { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual ICollection<Dispatch> Dispatches { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual Hub Hub1 { get; set; }
        public virtual Program Program { get; set; }
        public virtual ProjectCode ProjectCode { get; set; }
        public virtual ShippingInstruction ShippingInstruction { get; set; }
        public virtual Transporter Transporter { get; set; }
    }
}
