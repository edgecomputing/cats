using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub
{
    public class RPT_Distribution_Result
    {
        public decimal? QuantityInUnit { get; set; }
        public decimal? Quantity { get; set; }
        public string BidRefNo { get; set; }
        public string ProgramName { get; set; }
        public int Round { get; set; }
        public int PeriodMonth { get; set; }
        public string RegionName { get; set; }
        public int PeriodYear { get; set; }
        public decimal? DispatchedQuantity { get; set; }
        public int? Quarter { get; set; }
        public int RegionID { get; set; }
        public int ProgramID { get; set; }
        public int? BudgetYear { get; set; }
        public decimal? BalanceInMt { get; set; }
        public string CommodityName { get; set; }
        public int? CommodityTypeID { get; set; }
        public string RequisitionNo { get; set; }

        public string ZoneName { get; set; }
        public string WoredaName { get; set; }
        public string FDPName { get; set; }
        public int? AllocatedInMT { get; set; }

        public int? RemainingAmount { get; set; }
        public string TransaporterName { get; set; }
        public string DonorName { get; set; }
    }
}
