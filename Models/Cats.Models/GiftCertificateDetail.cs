using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class GiftCertificateDetail
    {
        public GiftCertificateDetail()
        {
            this.ReceiptAllocations = new List<ReceiptAllocation>();
            this.ReceiptPlans = new List<ReceiptPlan>();
        }
        [Key]
        public int GiftCertificateDetailID { get; set; }
        public int PartitionID { get; set; }
        public int TransactionGroupID { get; set; }
        public int GiftCertificateID { get; set; }
        public int CommodityID { get; set; }
        public decimal WeightInMT { get; set; }
        public string BillOfLoading { get; set; }
        public int AccountNumber { get; set; }
        public decimal EstimatedPrice { get; set; }
        public decimal EstimatedTax { get; set; }
        public int YearPurchased { get; set; }
        public int DFundSourceID { get; set; }
        public int DCurrencyID { get; set; }
        public int DBudgetTypeID { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual Detail Detail { get; set; }
        public virtual Detail Detail1 { get; set; }
        public virtual Detail Detail2 { get; set; }
        public virtual GiftCertificate GiftCertificate { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; }
        public virtual ICollection<ReceiptPlan> ReceiptPlans { get; set; }
    }
}
