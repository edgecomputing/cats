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
    }
}
