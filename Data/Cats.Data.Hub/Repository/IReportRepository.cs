using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Cats.Models.Hub;

namespace DRMFSS.BLL.Interfaces
{
    public interface IReportRepository
    {
        ObjectResult<RPT_MonthGiftSummary_Result> GetMonthlySummary();
        ObjectResult<RPT_Distribution_Result> RPT_Distribution(int hubId);
        ObjectResult<RPT_Distribution_Result> RPT_ReceiptReport(int hubID, DateTime sTime, DateTime eTime);

        ObjectResult<RPT_Distribution_Result> RPT_Offloading(int hubID, DateTime sTime, DateTime eTime);
        ObjectResult<RPT_Distribution_Result> util_GetDispatchedAllocationFromSI(int hubId, int sis);
        ObjectResult<BinCardReport> RPT_BinCardNonFood(int hubID, int? StoreID, int? CommodityID, string ProjectID);
        ObjectResult<BinCardReport> RPT_BinCard(int hubID, int? StoreID, int? CommodityID, string ProjectID);

        ObjectResult<RPT_MonthlyGiftSummary_Result> GetMonthlyGiftSummaryETA();
        ObjectResult<RPT_MonthlyGiftSummary_Result> GetMonthlyGiftSummary();
        ObjectResult<StockStatusReport> RPT_StockStatus(int hubID, int commodityID);
        ObjectResult<StockStatusReport> RPT_StockStatusNonFood(int? hubID, int? commodityID);
        ObjectResult<StatusReportBySI_Result> GetStatusReportBySI(int? hubID);
        ObjectResult<DispatchFulfillmentStatus_Result> GetDispatchFulfillmentStatus(int? hubID);

        ObjectResult<DispatchFulfillmentStatus_Result> GetAllLossAndAdjustmentLog();
    }
}
