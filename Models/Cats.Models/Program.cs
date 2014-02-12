using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class Program
    {
        public Program()
        {
            this.DispatchAllocations = new List<DispatchAllocation>();
            this.GiftCertificates = new List<GiftCertificate>();
            this.ReceiptAllocations = new List<ReceiptAllocation>();
            this.RegionalRequests = new List<RegionalRequest>();
            this.DonationPlanHeaders = new List<DonationPlanHeader>();
            this.Transactions = new List<Transaction>();
            this.TransportRequisitions = new List<TransportRequisition>();
            this.LocalPurchases=new List<LocalPurchase>();
            this.LoanReciptPlans=new List<LoanReciptPlan>();
           
            //this.Plans=new List<Plan>();
        }

        public int ProgramID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LongName { get; set; }
        public string ShortCode { get; set; }
        public virtual ICollection<DispatchAllocation> DispatchAllocations { get; set; }
        public virtual ICollection<GiftCertificate> GiftCertificates { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; }
        public virtual ICollection<RegionalRequest> RegionalRequests { get; set; }
        public virtual ICollection<DonationPlanHeader> DonationPlanHeaders { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<TransportRequisition> TransportRequisitions { get; set; }
        public virtual ICollection<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }
        public virtual ICollection<LocalPurchase> LocalPurchases { get; set; }
        public virtual ICollection<LoanReciptPlan> LoanReciptPlans { get; set; } 
       //public virtual ICollection<Plan> Plans { get; set; } 
    }
}
