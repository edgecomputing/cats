using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class WorkflowStatus
    {
        public int StatusID { get; set; }
        public int WorkflowID { get; set; }
        public string Description { get; set; }
        public virtual Workflow Workflow { get; set; }
       // RegionalPSNPPlan
    }
}
