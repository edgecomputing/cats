using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs.ViewModels.Report.Data
{
    public class FreeStockReport : BaseReport
    {
        public List<FreeStockProgram> Programs
        {
            get;
            set;
        }
    }

    public class FreeStockProgram
    {
        public string Name { get; set; }
        public IEnumerable<FreeStockStatusRow> Details { get; set; }
    }

    public class FreeStockStatusRow
    {
        public string SINumber { get; set; }
        public string Vessel { get; set; }
        public string Product { get; set; }
        public string Project { get; set; }
        public string Donor { get; set; }
        public string Program { get; set; }
        public decimal Allocation { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal Transported { get; set; }
        public decimal Dispatched { get; set; }
        public decimal PhysicalStock { get; set; }
        public decimal FreeStock { get; set; }
        public string Remark { get; set; }
    }
}
