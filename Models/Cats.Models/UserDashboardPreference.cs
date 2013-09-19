using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class UserDashboardPreference
    {
        public int? UserDashboardPreferenceID { get; set; }
        public int UserID { get; set; }
        public int DashboardWidgetID { get; set; }
        public int OrderNo { get; set; }
        public virtual DashboardWidget DashboardWidget { get; set; }
        public virtual User User { get; set; }
    }
}
