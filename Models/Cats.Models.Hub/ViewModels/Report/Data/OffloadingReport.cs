using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs.ViewModels.Report.Data
{
    public class OffloadingReportMain : BaseReport
    {
        public List<OffloadingReport> reports
        {
            get;
            set;
        }
    }

    public class OffloadingReport
    {
        public string Region { get; set; }
        public string ContractNumber { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Program { get; set; }
        public string Round { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public List<OffloadingDetail> OffloadingDetails { get; set; }

    }

    public class OffloadingDetail
    {
        public string RequisitionNumber { get; set; }
        public string Product { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public string Destination { get; set; }
        public decimal Allocation { get; set; }
        public decimal Dispatched { get; set; }
        public decimal Remaining { get; set; }
        public string Transporter { get; set; }
        public string Donor { get; set; }
    }
}
