using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Adjustments = new List<Adjustment>();
            this.Receives = new List<Receive>();
            this.SessionAttempts = new List<SessionAttempt>();
            this.SessionHistories = new List<SessionHistory>();
            this.UserHubs = new List<UserHub>();
            this.UserRoles = new List<UserRole>();
        }
       [Key]
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
        //public int? DefaultHub { get; set; }

        public bool Disabled { get; set; }
        public string[] Roles { get; set; }

        public string FullName { get { return string.Format("{0} {1} {2}", FirstName, LastName, GrandFatherName); } }

        public virtual ICollection<Adjustment> Adjustments { get; set; }
        public virtual ICollection<Receive> Receives { get; set; }
        public virtual ICollection<SessionAttempt> SessionAttempts { get; set; }
        public virtual ICollection<SessionHistory> SessionHistories { get; set; }
        public virtual ICollection<UserHub> UserHubs { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
