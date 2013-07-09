using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class ProjectCode
    {
       public ProjectCode()
        {
            this.DispatchAllocations = new List<DispatchAllocation>();
           
            this.TransportRequisitions = new List<TransportRequisition>();
        }

        public int ProjectCodeID { get; set; }
        public string Value { get; set; }
        public virtual ICollection<DispatchAllocation> DispatchAllocations { get; set; }
       
        public virtual ICollection<TransportRequisition> TransportRequisitions { get; set; }
    }
}
