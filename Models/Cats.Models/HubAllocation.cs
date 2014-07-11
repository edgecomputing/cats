using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class HubAllocation
    {
        public HubAllocation()
        {
            this.ProjectCodeAllocations = new List<ProjectCodeAllocation>();
        }

        public int HubAllocationID { get; set; }
        public string ReferenceNo { get; set; }
        public int RequisitionID { get; set; }
        public int HubID { get; set; }
        public System.DateTime AllocationDate { get; set; }
        public int AllocatedBy { get; set; }
        public virtual Hub Hub { get; set; }
        public int? PartitionId { get; set; }

       // public virtual UserProfile UserProfile { get; set; }
        public virtual ReliefRequisition ReliefRequisition { get; set; }
        public virtual ICollection<ProjectCodeAllocation> ProjectCodeAllocations { get; set; }

    }
}
