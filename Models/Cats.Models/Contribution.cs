using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public partial class Contribution
    {
        public Contribution()
        {
            this.ContributionDetails = new List<ContributionDetail>();
            this.InKindContributionDetails=new List<InKindContributionDetail>();
        }

        public int ContributionID { get; set; }
        public int DonorID { get; set; }
        public int HRDID { get; set; }
        public int Year { get; set; }
        //public int ImplementingAgency { get; set; }
        public virtual Donor Donor { get; set; }
        //public virtual Donor Donor1 { get; set; }
        public virtual HRD HRD { get; set; }
        public virtual ICollection<ContributionDetail> ContributionDetails { get; set; }
       public virtual ICollection<InKindContributionDetail> InKindContributionDetails{ get; set; }
    }
}
