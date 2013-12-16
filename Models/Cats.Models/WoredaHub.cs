using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class WoredaHub
    {
        public WoredaHub()
        {
            this.WoredaHubLinks = new List<WoredaHubLink>();
        }

        public int WoredaHubID { get; set; }
        public int HRDID { get; set; }
        public int PlanID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int Status { get; set; }
        public virtual ICollection<WoredaHubLink> WoredaHubLinks { get; set; }
    }
}
