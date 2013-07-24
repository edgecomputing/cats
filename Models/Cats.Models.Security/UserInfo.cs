using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
    public partial class UserInfo
    {
        public int UserAccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Disabled { get; set; }
        public bool LoggedIn { get; set; }
        public Nullable<System.DateTime> LogginDate { get; set; }
        public Nullable<System.DateTime> LogOutDate { get; set; }
        public int FailedAttempts { get; set; }
        public byte[] UserSID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string LanguageCode { get; set; }
        public string Calendar { get; set; }
        public string Keyboard { get; set; }
        public string PreferedWeightMeasurment { get; set; }
        public string DefaultTheme { get; set; }
    }
}
