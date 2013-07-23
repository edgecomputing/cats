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
        public string UserName { get; set; }
        public byte[] DBUserSid { get; set; }
        public int UserProfileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UILanguage { get; set; }
      //  public string KeyBoardLanguage { get; set; }
        public string Calendar { get; set; }
    }
}
