using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class Commodity
    {
        public Commodity()
        {
            this.DispatchAllocations = new List<DispatchAllocation>();
            this.Commodity1 = new List<Commodity>();
            this.DispatchDetails = new List<DispatchDetail>();
            this.GiftCertificateDetails = new List<GiftCertificateDetail>();
            this.OtherDispatchAllocations = new List<OtherDispatchAllocation>();
            this.ReceiptAllocations = new List<ReceiptAllocation>();
            this.ReceiveDetails = new List<ReceiveDetail>();
            this.Transactions = new List<Transaction>();
            this.Transactions1 = new List<Transaction>();
        }
        [Key]
        public int CommodityID { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string NameAM { get; set; }
        public string CommodityCode { get; set; }
        public int CommodityTypeID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public virtual ICollection<DispatchAllocation> DispatchAllocations { get; set; }
        public virtual ICollection<Commodity> Commodity1 { get; set; }
        public virtual Commodity Commodity2 { get; set; }
        public virtual ICollection<DispatchDetail> DispatchDetails { get; set; }
        public virtual ICollection<GiftCertificateDetail> GiftCertificateDetails { get; set; }
        public virtual ICollection<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }
        public virtual CommodityType CommodityType { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; }
        public virtual ICollection<ReceiveDetail> ReceiveDetails { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Transaction> Transactions1 { get; set; }
    }
}
