using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class ReceiptAllocation
    {
        public ReceiptAllocation()
        {
            this.Receives = new List<Receive>();
        }
        [Key]
        public System.Guid ReceiptAllocationID { get; set; }
        public int PartitionID { get; set; }
        public bool IsCommited { get; set; }
        public System.DateTime ETA { get; set; }
        public string ProjectNumber { get; set; }
        public Nullable<int> GiftCertificateDetailID { get; set; }
        public int CommodityID { get; set; }
        public string SINumber { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<decimal> QuantityInUnit { get; set; }
        public decimal QuantityInMT { get; set; }
        public int HubID { get; set; }
        public Nullable<int> DonorID { get; set; }
        public int ProgramID { get; set; }
        public int CommoditySourceID { get; set; }
        public bool IsClosed { get; set; }
        public string PurchaseOrder { get; set; }
        public string SupplierName { get; set; }
        public Nullable<int> SourceHubID { get; set; }
        public string OtherDocumentationRef { get; set; }
        public string Remark { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual CommoditySource CommoditySource { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual GiftCertificateDetail GiftCertificateDetail { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual Hub Hub1 { get; set; }
        public virtual Program Program { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<Receive> Receives { get; set; }
    }
}
