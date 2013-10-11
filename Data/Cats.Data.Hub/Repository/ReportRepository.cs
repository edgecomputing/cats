using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cats.Data.Hub;
using Cats.Models.Hub;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    public class ReportRepository : IReportRepository
    {
        private HubContext _context;

        public ReportRepository(HubContext context)
        {
            _context = context;
        }



        public System.Data.Objects.ObjectResult<RPT_MonthGiftSummary_Result> GetMonthlySummary()
        {
            return _context.GetMonthlySummary();
        }

        public System.Data.Objects.ObjectResult<RPT_Distribution_Result> RPT_Distribution(int hubId)
        {
            return _context.RPT_Distribution(hubId);
        }

        public System.Data.Objects.ObjectResult<RPT_Distribution_Result> RPT_ReceiptReport(int hubID, DateTime sTime, DateTime eTime)
        {
            return _context.RPT_ReceiptReport(hubID, sTime, eTime);
        }

        public System.Data.Objects.ObjectResult<RPT_Distribution_Result> RPT_Offloading(int hubID, DateTime sTime, DateTime eTime)
        {
            return _context.RPT_Offloading(hubID, sTime, eTime);
        }

        public System.Data.Objects.ObjectResult<RPT_Distribution_Result> util_GetDispatchedAllocationFromSI(int hubId, int sis)
        {
         
            return _context.util_GetDispatchedAllocationFromSI(hubId, sis);
        }
        public class DispatchedQuantityFromSI
        {
            public decimal? Quantity { get; set; }
            public decimal? QuantityInMT { get; set; }
        }
        public DispatchedQuantityFromSI GetDispatchedAllocationFromSi(int hubId, int sis)
        {
            

            var x = (from item in _context.DispatchAllocations
                     join dispatch in _context.Dispatches on item.DispatchAllocationID equals
                         dispatch.DispatchAllocationID
                     join dispatchDetail in _context.DispatchDetails on dispatch.DispatchID equals
                         dispatchDetail.DispatchID
                     join transaction in _context.Transactions on dispatchDetail.TransactionGroupID equals
                         transaction.TransactionGroupID

                     where
                         (item.ShippingInstructionID == sis && item.IsClosed && transaction.LedgerID == 9 &&
                          dispatch.HubID == hubId)
                     select new DispatchedQuantityFromSI { QuantityInMT= transaction.QuantityInMT,Quantity= transaction.QuantityInUnit }
                    );
            return new DispatchedQuantityFromSI()
                       {Quantity = x.Sum(t => t.Quantity), QuantityInMT = x.Sum(t => t.QuantityInMT)};

            
        }
        public System.Data.Objects.ObjectResult<BinCardReport> RPT_BinCardNonFood(int hubID, int? StoreID, int? CommodityID, string ProjectID)
        {
            return _context.RPT_BinCardNonFood(hubID, StoreID, CommodityID, ProjectID);
        }

        public System.Data.Objects.ObjectResult<BinCardReport> RPT_BinCard(int hubID, int? StoreID, int? CommodityID, string ProjectID)
        {
            return _context.RPT_BinCard(hubID, StoreID, CommodityID, ProjectID);
        }

        public System.Data.Objects.ObjectResult<RPT_MonthlyGiftSummary_Result> GetMonthlyGiftSummaryETA()
        {
            return _context.GetMonthlyGiftSummaryETA();
        }

        public System.Data.Objects.ObjectResult<RPT_MonthlyGiftSummary_Result> GetMonthlyGiftSummary()
        {
            return _context.GetMonthlyGiftSummary();
        }

        public System.Data.Objects.ObjectResult<StockStatusReport> RPT_StockStatus(int hubID, int commodityID)
        {
            return _context.RPT_StockStatus(hubID, commodityID);
        }

        public System.Data.Objects.ObjectResult<StockStatusReport> RPT_StockStatusNonFood(int? hubID, int? commodityID)
        {
            return _context.RPT_StockStatusNonFood(hubID, commodityID);
        }

        public System.Data.Objects.ObjectResult<StatusReportBySI_Result> GetStatusReportBySI(int? hubID)
        {
            return _context.GetStatusReportBySI(hubID);
        }

        public System.Data.Objects.ObjectResult<DispatchFulfillmentStatus_Result> GetDispatchFulfillmentStatus(int? hubID)
        {
            return _context.GetDispatchFulfillmentStatus(hubID);
        }

        public System.Data.Objects.ObjectResult<DispatchFulfillmentStatus_Result> GetAllLossAndAdjustmentLog()
        {
            return _context.GetAllLossAndAdjustmentLog();
        }
    }
}
