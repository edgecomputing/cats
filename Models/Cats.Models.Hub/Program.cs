using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class Program
    {
        public Program()
        {
            this.DispatchAllocations = new List<DispatchAllocation>();
            this.GiftCertificates = new List<GiftCertificate>();
            this.OtherDispatchAllocations = new List<OtherDispatchAllocation>();
            this.ReceiptAllocations = new List<ReceiptAllocation>();
            this.Transactions = new List<Transaction>();
        }
        [Key]
        public int ProgramID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LongName { get; set; }
        public virtual ICollection<DispatchAllocation> DispatchAllocations { get; set; }
        public virtual ICollection<GiftCertificate> GiftCertificates { get; set; }
        public virtual ICollection<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
