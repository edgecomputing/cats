using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class Donor
    {
        public Donor()
        {
           
          
            this.ReliefRequisitionDetails = new List<ReliefRequisitionDetail>();
            this.TransportOrderDetails = new List<TransportOrderDetail>();
            this.GiftCertificates = new List<GiftCertificate>();
            this.Contributions=new List<Contribution>();
            //this.Contributions1=new List<Contribution>();
        }

        public int DonorID { get; set; }
        public string Name { get; set; }
        public string DonorCode { get; set; }
        public bool IsResponsibleDonor { get; set; }
        public bool IsSourceDonor { get; set; }
        public string LongName { get; set; }
        public virtual ICollection<GiftCertificate> GiftCertificates { get; set; }
        public virtual ICollection<TransportOrderDetail> TransportOrderDetails { get; set; }
  
        public virtual ICollection<ReliefRequisitionDetail> ReliefRequisitionDetails { get; set; }
        public virtual ICollection<Contribution> Contributions { get; set; }
        //public virtual ICollection<Contribution> Contributions1 { get; set; }
    }
}
