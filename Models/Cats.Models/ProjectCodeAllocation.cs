using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Models
{
    public class ProjectCodeAllocation
    {
        
        public int? ProjectCodeAllocationID { get; set; }
        public int HubAllocationID { get; set; }
        public Nullable<int> ProjectCodeID { get; set; }
        public Nullable<int> Amount_FromProject { get; set; }
        public Nullable<int> SINumberID { get; set; }
        public Nullable<int> Amount_FromSI { get; set; }
        public int AllocatedBy { get; set; }
        public System.DateTime AlloccationDate { get; set; }

        //public ProjectCodeAllocationInput Input { get; set; }
        public virtual ProjectCode ProjectCode { get; set; }
        public virtual ShippingInstruction ShippingInstruction { get; set; }
        //public virtual UserProfile UserProfile { get; set; }
        //public virtual HubAllocation HubAllocation { get; set; }
        
    }

}
