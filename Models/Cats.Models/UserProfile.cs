using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class UserProfile
    {

        public UserProfile()
        {

            
            this.Hrds=new List<HRD>();
            this.NeedAssessments = new List<NeedAssessment>();
            this.NeedAssessments1 = new List<NeedAssessment>();
            this.DonationPlanHeaders = new List<DonationPlanHeader>();
        }

        public int UserProfileID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GrandFatherName { get; set; }
        public bool ActiveInd { get; set; }
        public bool LoggedInInd { get; set; }
        public Nullable<System.DateTime> LogginDate { get; set; }
        public Nullable<System.DateTime> LogOutDate { get; set; }
        public int FailedAttempts { get; set; }
        public bool LockedInInd { get; set; }
        public string LanguageCode { get; set; }
        public string DatePreference { get; set; }
        public string PreferedWeightMeasurment { get; set; }
        public string Keyboard { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string DefaultTheme { get; set; }

        public virtual ICollection<DonationPlanHeader> DonationPlanHeaders { get; set; }

        public int? DefaultHub { get; set; }


        public virtual ICollection<WoredaStockDistribution> UtilizationHeaders { get; set; }
        public virtual ICollection<HRD> Hrds { get; set; }
        public virtual ICollection<NeedAssessment> NeedAssessments { get; set; }
        public virtual ICollection<NeedAssessment> NeedAssessments1 { get; set; }
       
        
    }
}
