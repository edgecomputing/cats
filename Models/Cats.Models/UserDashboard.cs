using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class UserDashboard
    {
        public int UserDashboardId { get; set; }
        public int DashboardId { get; set; }
        public int UserId { get; set; }
        public bool Allow { get; set; }
    }
}
