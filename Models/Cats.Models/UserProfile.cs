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

            //this.ReliefRequisitions = new List<ReliefRequisition>();
            //this.ReliefRequisitions1 = new List<ReliefRequisition>();
           // this.HubAllocations = new List<HubAllocation>();
            //this.TransportRequisitions = new List<TransportRequisition>();
            //this.TransportRequisitions1=new List<TransportRequisition>();
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
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string DefaultTheme { get; set; }

        //public virtual ICollection<ReliefRequisition> ReliefRequisitions { get; set; }
        //public virtual ICollection<ReliefRequisition> ReliefRequisitions1 { get; set; }
        public virtual ICollection<HubAllocation> HubAllocations { get; set; }
        //public virtual ICollection<TransportRequisition> TransportRequisitions { get; set; }
        //public virtual ICollection<TransportRequisition> TransportRequisitions1 { get; set; }
    }
}
