using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class Workflow
    {
        public Workflow()
        {
            this.WorkflowStatuses = new List<WorkflowStatus>();
        }

        public int WorkflowID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<WorkflowStatus> WorkflowStatuses { get; set; }
    }
}
