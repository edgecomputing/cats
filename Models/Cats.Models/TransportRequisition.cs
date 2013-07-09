using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class TransportRequisition
    {
        
            public int TransportRequisitionID { get; set; }
            public string TransportRequistionNo { get; set; }
            public int RequisitionID { get; set; }
            public int CommodityID { get; set; }
            public decimal Amount { get; set; }
            public int RegionID { get; set; }
            public int ZoneID { get; set; }
            public int HubID { get; set; }
            public string SINumber { get; set; }
            public Nullable<int> ProjectCode { get; set; }
            public Nullable<int> Status { get; set; }
            public Nullable<int> HubAllocatedBy { get; set; }
            public Nullable<System.DateTime> HubAllocationDate { get; set; }
            public Nullable<int> SIAllocatedBy { get; set; }
            public Nullable<System.DateTime> SIAllocationDate { get; set; }
            public string Remark { get; set; }
            public virtual AdminUnit AdminUnit { get; set; }
            public virtual AdminUnit AdminUnit1 { get; set; }
            public virtual Hub Hub { get; set; }
            public virtual ProjectCode ProjectCode1 { get; set; }
            public virtual UserProfile UserProfile { get; set; }
            public virtual UserProfile UserProfile1 { get; set; }
            public virtual ReliefRequisition ReliefRequisition { get; set; }
        
    }
}
