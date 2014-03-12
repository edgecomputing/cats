using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class TransporterPerformanceViewModel
    {
        public int TransporterOrderID { get; set; }
        public string TransportOrderNumber { get; set; }
        public int TransporterID { get; set; }
        public string TransporterName { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal PickedUpSofar { get; set; }
        public string StartDate { get; set; }
        public int NoOfDaysToComplete { get; set; }
        public int ElapsedDays { get; set; }
        public decimal Percentage
        {
            get
            {
                return (PickedUpSofar/TotalQuantity)*100;
            }
        }
        public string ContractNumber { get; set; }
    }
    public class DispatchAllocationViewModel
    {
        public decimal DispatchedSoFar { get; set; }
    }
}