using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class Receive
    {
        public Receive()
        {
            this.ReceiveDetails = new List<ReceiveDetail>();
        }
        [Key]
        public System.Guid ReceiveID { get; set; }
        public int PartitionID { get; set; }
        public int HubID { get; set; }
        public string GRN { get; set; }
        public int CommodityTypeID { get; set; }
        public Nullable<int> SourceDonorID { get; set; }
        public Nullable<int> ResponsibleDonorID { get; set; }
        public int TransporterID { get; set; }
        public string PlateNo_Prime { get; set; }
        public string PlateNo_Trailer { get; set; }
        public string DriverName { get; set; }
        public string WeightBridgeTicketNumber { get; set; }
        public Nullable<decimal> WeightBeforeUnloading { get; set; }
        public Nullable<decimal> WeightAfterUnloading { get; set; }
        public System.DateTime ReceiptDate { get; set; }
        public int UserProfileID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string WayBillNo { get; set; }
        public int CommoditySourceID { get; set; }
        public string Remark { get; set; }
        public string VesselName { get; set; }
        public string ReceivedByStoreMan { get; set; }
        public string PortName { get; set; }
        public string PurchaseOrder { get; set; }
        public string SupplierName { get; set; }
        public Nullable<System.Guid> ReceiptAllocationID { get; set; }
        public virtual CommoditySource CommoditySource { get; set; }
        public virtual CommodityType CommodityType { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual Donor Donor1 { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual ReceiptAllocation ReceiptAllocation { get; set; }
        public virtual Transporter Transporter { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<ReceiveDetail> ReceiveDetails { get; set; }
    }
}
