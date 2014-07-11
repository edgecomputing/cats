using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Disabled { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public string[] Roles { get; set; }
    }
}
