using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub.ViewModels.Report.Data
{
    public class ReceiveReportMain : BaseReport
    {
        public List<ReceiveReport> receiveReports
        {
            get;
            set;
        }
    }

    public class ReceiveReport
    {
        public List<ReceiveRow> rows
        {
            get;
            set;
        }

        public int BudgetYear { get; set; }
        
    }

    public class ReceiveRow
    {

        public int Quarter
        {
            get;
            set;
        }

        public string Program
        {
            get;
            set;
        }

        public string Product
        {
            get;
            set;
        }

        public string CommodityType
        {
            get;
            set;
        }

        public string MeasurementUnit { get; set; }

        public decimal Quantity { get; set; }

    }
}

