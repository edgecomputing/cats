using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
    public partial class UserProfile
    {
        public int UserProfileId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GrandFatherName { get; set; }
        public bool ActiveInd { get; set; }
        public bool LoggedInInd { get; set; }
        public Nullable<DateTime> LogInDate { get; set; }
        public Nullable<DateTime> LogOutDate { get; set; }
        public int FailedAttempts { get; set; }
        public bool LockedInInd { get; set; }
        public string UILanguage { get; set; }
        public string Calendar { get; set; }
        public string KeyboardLanguage { get; set; }
        public string PreferedWeightMeasurement { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string DefaultTheme { get; set; }
        public Nullable<int> CaseTeam { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
