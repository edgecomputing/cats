using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
    public partial class UserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Disabled { get; set; }
        public bool LoggedInInd { get; set; }
        public Nullable<System.DateTime> LogginDate { get; set; }
        public Nullable<System.DateTime> LogOutDate { get; set; }
        public int FailedAttempts { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GrandFatherName { get; set; }
        public string Email { get; set; }
        public Nullable<int> CaseTeam { get; set; }
        public string LanguageCode { get; set; }
        public string DatePreference { get; set; }
        public string Keyboard { get; set; }
        public string PreferedWeightMeasurment { get; set; }
        public string DefaultTheme { get; set; }
        public bool ActiveInd { get; set; }
        public bool LockedInInd { get; set; }
        public int NumberOfLogins { get; set; }
        //  public byte[] UserSID { get; set; }
        public int UserProfileID { get; set; }
        public int? RegionID { get; set; }
        public bool RegionalUser { get; set; }
        public int? DefaultHub { get; set; }
        public bool IsAdmin { get; set; }
        //public int? ProgramId  { get; set; }
        public string FullName
        {
            get { return string.Format("{0} {1} {2}", FirstName, LastName, GrandFatherName); }
        }
    }
}