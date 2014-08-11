using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels.Dashboard
{
    public class RecentRequests
    {
        public int RegionalRequestID { get; set; }
        public string RequestNumber { get; set; }
        public int Month { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
        public int Beneficiaries { get; set; }
        public decimal Amount { get; set; }
    }

    public class RecentRequisitions
    {
        public int RequisitionID { get; set; }
        public string RequisitionNo { get; set; }
        public int BenficiaryNo { get; set; }
        public decimal Amount { get; set; }
        public int? Status { get; set; }
        public DateTime? RequestedDate { get; set; }
        public string Name { get; set; }
        
    }

    public class RegionalRequestAllocationChange
    {
        public int RegionalRequestID { get; set; }
        public string RequestNumber { get; set; }
        public int Month { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
        public int Beneficiaries { get; set; }
        public decimal RequestedAmount { get; set; }
        public decimal AllocatedAmount { get; set; }
    }
}
