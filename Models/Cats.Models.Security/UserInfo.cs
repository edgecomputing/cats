using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public byte[] DBUserSid { get; set; }
        public int UserProfileID { get; set; }
        public string UILanguage { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Disabled { get; set; }
        public bool ActiveInd { get; set; }
        public bool LoggedInInd { get; set; }
        public DateTime LogInDate { get; set; }
        public DateTime LogOutDate { get; set; }
        public int FailedAttempts { get; set; }
        public bool LockedInInd { get; set; }
        public string PreferedWeightMeasurment { get; set; }
        public string Calendar { get; set; }
        public string KeyBoardLanguage { get; set; }
    }
}
