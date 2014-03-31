using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Regional.Models
{
    public class DashboardData
    {
        public decimal ApprovedRequests { get; set; }
        public decimal PendingRequests { get; set; }
        public decimal HubAssignedRequests { get; set; }
        public decimal FederalApproved { get; set; }

        public decimal ApprovedRequisitions { get; set; }
        public decimal PendingRequisitions { get; set; }
        public decimal HubAssignedRequisitions { get; set; }
       
        public int IncomingDispatches { get; set; }
        public int IncomingCommodity { get; set; }
        public int Above18 { get; set; }
        public int Below5 { get; set; }
        public int Bet5And8 { get; set; }
        public int Female { get; set; }
        public int Male { get; set; }
    }
}