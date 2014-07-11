using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
    public partial class UserAccount
    {
        public int UserAccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Disabled { get; set; }
        public bool LoggedIn { get; set; }
        public Nullable<System.DateTime> LogginDate { get; set; }
        public Nullable<System.DateTime> LogOutDate { get; set; }
        public int FailedAttempts { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GrandFatherName { get; set; }
        public string Email { get; set; }
        public Nullable<int> CaseTeam { get; set; }
        public string LanguageCode { get; set; }
        public string Calendar { get; set; }
        public string Keyboard { get; set; }
        public string PreferedWeightMeasurment { get; set; }
        public string DefaultTheme { get; set; }
        public virtual UserPreference UserPreference { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        [NotMapped]
        public string Role { get; set; }
        [NotMapped]
        public string[] Roles { get; set; }

        public UserAccount()
        {
            //this.UserPreference = new UserPreference();
            //this.UserProfile = new UserProfile();
        }
        
        
    }
}
