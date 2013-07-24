using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security
{
    public partial class UserPreference
    {
        public int UserAccountId { get; set; }
        public string LanguageCode { get; set; }
        public string Calendar { get; set; }
        public string Keyboard { get; set; }
        public string PreferedWeightMeasurment { get; set; }
        public string DefaultTheme { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
