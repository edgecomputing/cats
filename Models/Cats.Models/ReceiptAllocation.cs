using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cats.Models;

namespace Cats.Models
{
    public partial class ReceiptAllocation
    {
        public ReceiptAllocation()
        {
            //this.Receives = new List<Receive>();
        }

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

        public virtual Donor Donor { get; set; }

        public virtual Hub Hub { get; set; }
        public virtual Hub Hub1 { get; set; }
        public virtual Program Program { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<Receive> Receives { get; set; }
        public virtual CommoditySource CommoditySource { get; set; }
        public virtual GiftCertificateDetail GiftCertificateDetail { get; set; }







        [NotMapped]
        public bool UserNotAllowedHub { set; get; }

        [NotMapped]
        public Decimal RemainingBalanceInUnit { get; set; }

        [NotMapped]
        public Decimal ReceivedQuantityInUnit
        {
            set { ; }
            get
            {

                if (this.QuantityInUnit == null)
                    return (0 - RemainingBalanceInUnit);
                else
                    return (this.QuantityInUnit.Value - RemainingBalanceInUnit);
            }

        }
        [NotMapped]
        public Decimal RemainingBalanceInMt { set; get; }
        [NotMapped]
        public Decimal ReceivedQuantityInMT
        {
            set { ; }
            get { return this.QuantityInMT - RemainingBalanceInMt; }

        } // { return GetReceivedAlready(this); } 
        [NotMapped]
        public string CommodityName { set; get; }

        public decimal GetReceivedAlready(ReceiptAllocation receiptAllocation)
        {
            decimal sum = 0;
            if (receiptAllocation.Receives != null)
                foreach (Receive r in receiptAllocation.Receives)
                {
                    foreach (ReceiveDetail rd in r.ReceiveDetails)
                    {
                        sum = sum + rd.QuantityInMT;
                    }
                }
            return sum;
        }

        public decimal GetReceivedAlreadyInUnit(ReceiptAllocation receiptAllocation)
        {
            decimal sum = 0;
            if (receiptAllocation.Receives != null)
                foreach (Receive r in receiptAllocation.Receives)
                {
                    foreach (ReceiveDetail rd in r.ReceiveDetails)
                    {
                        sum = sum + rd.QuantityInUnit;
                    }
                }
            return sum;
        }
    }
}
