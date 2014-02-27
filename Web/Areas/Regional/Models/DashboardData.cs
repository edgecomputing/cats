using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Regional.Models
{
    public class DashboardData
    {
        public int ApprovedRequests { get; set; }
        public int PendingRequests { get; set; }
        public int HubAssignedRequests { get; set; }
        public int ApprovedRequisitions { get; set; }
        public int PendingRequisitions { get; set; }
        public int HubAssignedRequisitions { get; set; }
        public int IncomingDispatches { get; set; }
        public int IncomingCommodity { get; set; }
        public int Above18 { get; set; }
        public int Below5 { get; set; }
        public int Bet5And8 { get; set; }
        public int Female { get; set; }
        public int Male { get; set; }
    }
}