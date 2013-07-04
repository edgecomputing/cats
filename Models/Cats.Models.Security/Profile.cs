using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
    public partial class Profile
    {
        public int ProfileId { get; set; }
        public System.Guid UserId { get; set; }
        public Nullable<int> UILanguage { get; set; }
        public Nullable<int> KeyboardLanguage { get; set; }
        public Nullable<int> Calendar { get; set; }
    }
}
