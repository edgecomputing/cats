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
            this.ReliefRequisitions = new List<ReliefRequisition>();
            this.Transactions = new List<Transaction>();
            this.TransportRequisitions = new List<TransportRequisition>();
           // this.HrdPlans=new List<HRDPlan>();
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
        public virtual ICollection<ReliefRequisition> ReliefRequisitions { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<TransportRequisition> TransportRequisitions { get; set; }
        public virtual ICollection<Plan> Plans { get; set; } 
    }
}
