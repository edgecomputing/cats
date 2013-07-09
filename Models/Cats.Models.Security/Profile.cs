using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
    public partial class Profile
    {
        public int UserId { get; set; }
        public Nullable<int> UILanguage { get; set; }
        public Nullable<int> KeyboardLanguage { get; set; }
        public Nullable<int> Calendar { get; set; }
        public virtual User User { get; set; }
    }
}
