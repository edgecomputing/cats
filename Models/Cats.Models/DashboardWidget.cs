using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class DashboardWidget
    {
        public DashboardWidget(int dashboardWidgetID)
        {
            this.UserDashboardPreferences = new List<UserDashboardPreference>();
        }

        public int DashboardWidgetID { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Source { get; set; }
        public virtual ICollection<UserDashboardPreference> UserDashboardPreferences { get; set; }
    }
}
