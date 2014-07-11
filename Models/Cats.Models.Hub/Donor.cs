using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class Donor
    {
        public Donor()
        {
            this.GiftCertificates = new List<GiftCertificate>();
            this.ReceiptAllocations = new List<ReceiptAllocation>();
        
        }
        [Key]
        public int DonorID { get; set; }
        public string Name { get; set; }
        public string DonorCode { get; set; }
        public bool IsResponsibleDonor { get; set; }
        public bool IsSourceDonor { get; set; }
        public string LongName { get; set; }
        public virtual ICollection<GiftCertificate> GiftCertificates { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; }
      
    }
}
