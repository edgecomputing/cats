using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class ProjectCodeAllocation
    {
        public int? RequisitionNumber { get; set; }
        public int? ProjectCodeAllocationID { get; set; }
        public int HubAllocationID { get; set; }
        public Nullable<int> ProjectCodeID { get; set; }
        public Nullable<int> Amount_Project { get; set; }
        public Nullable<int> ShippingInstructionID { get; set; }
        public Nullable<int> Amount_SI { get; set; }
        public int AllocatedBy { get; set; }
        public string Hub { get; set; }
        public System.DateTime AlloccationDate { get; set; }
        public ProjectCodeAllocationInput Input { get; set; }
        public class ProjectCodeAllocationInput
        {
            public int? ProjectCodeAllocationID { get; set; }
            public int HubAllocationID { get; set; }
            public int Number { get; set; }
            public Nullable<int> ProjectCodeID { get; set; }
            public Nullable<int> Amount_FromProject { get; set; }
            public Nullable<int> SINumberID { get; set; }
            public Nullable<int> Amount_FromSI { get; set; }
            public int AllocatedBy { get; set; }
            public System.DateTime AlloccationDate { get; set; }
        }
    }
}