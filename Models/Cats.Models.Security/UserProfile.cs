using System;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Security
{
    public class UserProfile
    {
        public UserProfile()
        
        {
            //this.Adjustments = new List<Adjustment>();
            //this.Receives = new List<Receive>();
            //this.SessionAttempts = new List<SessionAttempt>();
            //this.SessionHistories = new List<SessionHistory>();
            //this.UserHubs = new List<UserHub>();
            //this.UserRoles = new List<UserRole>();
            //try
            //{
            //    this.LanguageCode = this.LanguageCode.ToUpper();
            //    this.Keyboard = this.Keyboard.ToUpper();
            //}
            //catch
            //{
            //}
            
        }

        [Key]
        public int UserProfileID { get; set; }
         [Required]
        public string UserName { get; set; }
         [Required]
        public string Password { get; set; }
         [Required]
        public string FirstName { get; set; }
         [Required]
        public string LastName { get; set; }
        public string GrandFatherName { get; set; }
        public bool ActiveInd { get; set; }
        public bool LoggedInInd { get; set; }
        public DateTime? LogginDate { get; set; }
        public DateTime? LogOutDate { get; set; }
        public int FailedAttempts { get; set; }
        public bool LockedInInd { get; set; }
        public string LanguageCode { get; set; }
        public string DatePreference { get; set; }
        public string PreferedWeightMeasurment { get; set; }
        public string MobileNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string DefaultTheme { get; set; }
        public string Keyboard { get; set; }
        public int? CaseTeam { get; set; }
        public bool? Disabled { get; set; }
        public string[] Roles { get; set; }
        public int? DefaultHub { get; set; }
        public int NumberOfLogins { get; set; }
        public int? RegionID { get; set; }
        //public bool Disabled { get; set; }
        public bool RegionalUser { get; set; }
        public bool IsAdmin { get; set; }
        public string FullName { get { return string.Format("{0} {1} {2}", FirstName, LastName, GrandFatherName); } }
        public int? PartitionId { get; set; }

        //public virtual ICollection<Adjustment> Adjustments { get; set; }
        //public virtual ICollection<Receive> Receives { get; set; }
        //public virtual ICollection<SessionAttempt> SessionAttempts { get; set; }
        //public virtual ICollection<SessionHistory> SessionHistories { get; set; }
        //public virtual ICollection<UserHub> UserHubs { get; set; }
        //public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}