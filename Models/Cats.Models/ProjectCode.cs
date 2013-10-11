using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class ProjectCode
    {
        public int ProjectCodeID { get; set; }
        public string Value { get; set; }
        public virtual ICollection<ProjectCodeAllocation> ProjectCodeAllocations { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public ProjectCode()
        {
            this.ProjectCodeAllocations = new List<ProjectCodeAllocation>();
            this.Transactions = new List<Transaction>();
        }

    }
}
