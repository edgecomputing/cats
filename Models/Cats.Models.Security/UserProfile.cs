using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
    public partial class UserProfile
    {
        public int UserAccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GrandFatherName { get; set; }
        public string Email { get; set; }
        public Nullable<int> CaseTeam { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
