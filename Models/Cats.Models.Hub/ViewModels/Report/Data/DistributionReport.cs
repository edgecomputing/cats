using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs.ViewModels.Report.Data
{
    public class DistributionReport : BaseReport
    {
        public List<DistributionRows> Rows
        {
            get;
            set;
        }

    }

    public class DistributionRows
    {
        public int BudgetYear { get; set; }
        public string Program { get; set; }
        public string Region { get; set; }
        public int Quarter { get; set; }
        public string Month { get; set; }
        public decimal DistributedAmount { get; set; }
    }
}
