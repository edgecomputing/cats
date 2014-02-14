using System;
using System.Collections.Generic;

namespace Cats.Models
{
    public partial class HRD
    {

        public HRD()
        {
            this.Contributions=new List<Contribution>();
            this.HrdDonorCovarages=new List<HrdDonorCoverage>();
        }
        public int HRDID { get; set; }
        public int Year { get; set; }
        public Nullable<int> SeasonID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public Nullable<int> CreatedBY { get; set; }
        public int RationID { get; set; }
        //public Nullable<int> NeedAssessmentID { get; set; }
        public Nullable<int> Status { get; set; }
        public int PlanID { get; set; }
        public Nullable<System.Guid> TransactionGroupID { get; set; }
        public virtual TransactionGroup TransactionGroup { get; set; }
        public virtual Ration Ration { get; set; }
        public virtual ICollection<HRDDetail> HRDDetails { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        //public virtual NeedAssessment NeedAssessment { get; set; }
        public virtual Season Season { get; set; }
        public virtual ICollection<Contribution> Contributions { get; set; }
        public virtual Plan Plan { get; set;}
        public virtual ICollection<HrdDonorCoverage> HrdDonorCovarages { get; set; } 

    }
}
