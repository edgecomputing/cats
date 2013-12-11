using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Models
{
    public partial class Donor
    {
        public Donor()
        {


            this.ReliefRequisitionDetails = new List<ReliefRequisitionDetail>();
            this.TransportOrderDetails = new List<TransportOrderDetail>();
            this.GiftCertificates = new List<GiftCertificate>();
            this.Contributions = new List<Contribution>();
            this.WoredasByDonors = new List<WoredasByDonor>();
            //this.Contributions1 = new List<Contribution>();
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

        public virtual ICollection<RegionalPSNPPledge> RegionalPSNPPledges { get; set; }
        public virtual ICollection<Contribution> Contributions { get; set; }
        public virtual ICollection<RegionalRequest> RegionalRequests { get; set; }
        //public virtual ICollection<Contribution> Contributions1 { get; set; }

        public virtual ICollection<PromisedContribution> PromisedContributions { get; set; }
        public virtual ICollection<WoredasByDonor> WoredasByDonors { get; set; } 
    }
}
