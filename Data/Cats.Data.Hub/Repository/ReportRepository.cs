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
            /*
                    select * from 
       (
       SELECT Year(GiftDate) Year, Month(GiftDate) Month,  SUBSTRING (DATENAME(Month, (GiftDate)),0, 4)  + ' ' + CAST( Year(GiftDate) as nvarchar) MonthName,
             * case when c.ParentID is null then c.CommodityID else c.ParentID end as CommodityID, SUM(WeightInMT) Weight 
               from GiftCertificate g join GiftCertificateDetail d on g.GiftCertificateID = d.GiftCertificateID join Commodity c on d.CommodityID = c.CommodityID 
               group by SUBSTRING(DATENAME(Month, (GiftDate)),0 , 4) + ' ' + cast( Year(GiftDate) as nvarchar) ,case when c.ParentID is null then c.CommodityID else c.ParentID end, Year(GiftDate), MONTH(GiftDate) 
		
           ) as PivotData
           PIVOT
           (
           Sum(Weight)
           For CommodityID in 	
                ( [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12] )
                ) as PivotTable
               Order By Year,Month
       END

            */
            //(from gift in _context.GiftCertificates select  new {gift.GiftDate.Year,gift.GiftDate.Month, gift.GiftDate.Day, gift. })}
            return _context.GetMonthlySummary();
        }

        public System.Data.Objects.ObjectResult<RPT_Distribution_Result> RPT_Distribution(int hubId)
        {
            return _context.RPT_Distribution(hubId);
        }

        public System.Data.Objects.ObjectResult<RPT_Distribution_Result> RPT_ReceiptReport(int hubID, DateTime sTime,
                                                                                           DateTime eTime)
        {
            return _context.RPT_ReceiptReport(hubID, sTime, eTime);
        }

        public System.Data.Objects.ObjectResult<RPT_Distribution_Result> RPT_Offloading(int hubID, DateTime sTime,
                                                                                        DateTime eTime)
        {
            return _context.RPT_Offloading(hubID, sTime, eTime);
        }

        public IEnumerable<RPT_Distribution_Result> util_GetDispatchedAllocationFromSI(int hubId, int sis)
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
                     select
                         new RPT_Distribution_Result
                             {
                                 Quantity = transaction.QuantityInMT,
                                 QuantityInUnit = transaction.QuantityInUnit
                             }
                    );

            return new List<RPT_Distribution_Result>()
            {
                new RPT_Distribution_Result()
                    {Quantity = x.Sum(t => t.Quantity), QuantityInUnit = x.Sum(t => t.QuantityInUnit)}
            };
    }

        //public class DispatchedQuantityFromSI
        //{
        //    public decimal? Quantity { get; set; }
        //    public decimal? QuantityInMT { get; set; }
        //}
        //public DispatchedQuantityFromSI GetDispatchedAllocationFromSi(int hubId, int sis)
        //{


        //    var x = (from item in _context.DispatchAllocations
        //             join dispatch in _context.Dispatches on item.DispatchAllocationID equals
        //                 dispatch.DispatchAllocationID
        //             join dispatchDetail in _context.DispatchDetails on dispatch.DispatchID equals
        //                 dispatchDetail.DispatchID
        //             join transaction in _context.Transactions on dispatchDetail.TransactionGroupID equals
        //                 transaction.TransactionGroupID

        //             where
        //                 (item.ShippingInstructionID == sis && item.IsClosed && transaction.LedgerID == 9 &&
        //                  dispatch.HubID == hubId)
        //             select new RPT_Distribution_Result { Quantity = transaction.QuantityInMT, QuantityInUnit = transaction.QuantityInUnit }
        //            );
        //    return new DispatchedQuantityFromSI()
        //               {Quantity = x.Sum(t => t.Quantity), QuantityInMT = x.Sum(t => t.QuantityInMT)};


        //}
        
        public System.Data.Objects.ObjectResult<BinCardReport> RPT_BinCardNonFood(int hubID, int? StoreID,
                                                                                  int? CommodityID, string ProjectID)
        {

        var y = (
                from item in _context.Dispatches
                join dispatchDetail in _context.DispatchDetails on item.DispatchID equals dispatchDetail.DispatchID
                join fdp in _context.FDPs on item.FDPID equals fdp.FDPID
                join @group in _context.TransactionGroups on dispatchDetail.TransactionGroupID equals @group.TransactionGroupID
                join transaction1 in _context.Transactions on @group.TransactionGroupID equals transaction1.TransactionGroupID
                join code in _context.ProjectCodes on transaction1.ProjectCodeID equals code.ProjectCodeID
                join instruction in _context.ShippingInstructions on transaction1.ShippingInstructionID equals instruction.ShippingInstructionID
                join transporter1 in _context.Transporters on item.TransporterID equals transporter1.TransporterID
                where transaction1.HubID == hubID && transaction1.StoreID == StoreID
                //in (from com in Commodity where ParentId==CommodityID || CommodityID==CommodityID select CommodityID)
                //)

                select new
                {
                    Transporter = transporter1.Name,
                    transporter1.NameAM,
                    item.DriverName,
                    item.PlateNo_Prime,
                    Date = item.DispatchDate,
                    Identification = item.GIN,
                    ToFrom = fdp.Name,
                    Project = code.Value,
                    SINumber = instruction.Value,
                    transaction1.QuantityInMT,
                    //Dispatched = null
                }

                );
             
            return _context.RPT_BinCardNonFood(hubID, StoreID, CommodityID, ProjectID);
        }


        public IEnumerable<BinCardReport> RPT_BinCard(int hubID, int? StoreID, int? CommodityID, string ProjectID)
        {
            /*    select * from 
            (
                select [Ordering] = 1,tr.Name Transporter, tr.NameAM TransporterAM, r.DriverName,r.PlateNo_Prime, r.ReceiptDate [Date],
                  * r.GRN as Identification, d.Name as ToFrom, pc.Value Project,si.Value SINumber, t.QuantityInMT Received, Dispatched = null from Receive
                  * r left join Donor d on r.ResponsibleDonorID = d.DonorID 
                  * join ReceiveDetail rc on r.ReceiveID = rc.ReceiveID 
                  * join TransactionGroup tg on tg.TransactionGroupID = rc.TransactionGroupID 
                  * join [Transaction] t on t.TransactionGroupID = tg.TransactionGroupID and t.QuantityInMT >= 0 
                  * join ProjectCode pc on t.ProjectCodeID = pc.ProjectCodeID 
                  * join ShippingInstruction si on si.ShippingInstructionID = t.ShippingInstructionID 
                  * join Transporter tr on r.TransporterID = tr.TransporterID
                  * where t.HubID = @warehouse and t.StoreID = @store 
                  * and rc.CommodityID in ( select CommodityID from Commodity where ParentID = @commodity or CommodityID = @commodity) and pc.ProjectCodeID = @project
                union
                select [Ordering] = 2,tr.Name Transporter, tr.NameAM TransporterAM, d.DriverName,d.PlateNo_Prime, d.DispatchDate [Date],
                  * d.GIN as Identification,f.Name as ToFrom, pc.Value Project,SINumber = null, Received = null, t.QuantityInMT Dispatched   from Dispatch d 
                  * join DispatchDetail dc on d.DispatchID = dc.DispatchID left 
                  * join FDP f on  d.FDPID = f.FDPID join TransactionGroup tg on tg.TransactionGroupID = dc.TransactionGroupID 
                  * join [Transaction] t on t.TransactionGroupID = tg.TransactionGroupID and t.QuantityInMT >= 0 
                  * join ProjectCode pc on t.ProjectCodeID = pc.ProjectCodeID 
                  * join ShippingInstruction si on si.ShippingInstructionID = t.ShippingInstructionID 
                  * join Transporter tr on d.TransporterID = tr.TransporterID 
                  * where t.HubID = @warehouse 
                  * and t.StoreID = @store 
                  * and pc.ProjectCodeID = @project 
                  * and t.ParentCommodityID in 
                  * ( select CommodityID from Commodity where ParentID = @commodity or CommodityID = @commodity)
            ) as Transactions
            order by Transactions.Date asc, Ordering*/


            var y = (
                        from item in _context.Dispatches
                        join dispatchDetail in _context.DispatchDetails on item.DispatchID equals dispatchDetail.DispatchID
                        join fdp in _context.FDPs on item.FDPID equals fdp.FDPID
                        join @group in _context.TransactionGroups on dispatchDetail.TransactionGroupID equals @group.TransactionGroupID
                        join transaction1 in _context.Transactions on @group.TransactionGroupID equals transaction1.TransactionGroupID
                        join code in _context.ProjectCodes on transaction1.ProjectCodeID equals code.ProjectCodeID
                        join instruction in _context.ShippingInstructions on transaction1.ShippingInstructionID equals instruction.ShippingInstructionID
                        join transporter1 in _context.Transporters on item.TransporterID equals transporter1.TransporterID
                        where transaction1.HubID == hubID && transaction1.StoreID==StoreID
                        //in (from com in Commodity where ParentId==CommodityID || CommodityID==CommodityID select CommodityID)
                        //)
                        select new  BinCardReport()
                            {
                                Transporter = transporter1.Name,
                                TransporterAM= transporter1.NameAM,
                                DriverName = item.DriverName,
                                PlateNoPrime=  item.PlateNo_Prime,
                                Date = item.DispatchDate,
                                Identification=item.GIN,
                                ToFrom = fdp.Name,
                                Project = code.Value,
                                SINumber = instruction.Value,
                                Dispatched = transaction1.QuantityInMT,
                                Received = null
                            }
                    );

            var x = (
                        from bin in _context.Receives
                        join donor in _context.Donors on bin.ResponsibleDonorID equals donor.DonorID
                        join receiveDetail in _context.ReceiveDetails on bin.ReceiveID equals receiveDetail.ReceiveID
                        join transactionGroup in _context.TransactionGroups on receiveDetail.TransactionGroupID equals transactionGroup.TransactionGroupID
                        join transaction in _context.Transactions on transactionGroup.TransactionGroupID equals transaction.TransactionGroupID
                        join projectCode in _context.ProjectCodes on transaction.ProjectCodeID equals projectCode.ProjectCodeID
                        join shippingInstruction in _context.ShippingInstructions on transaction.ShippingInstructionID equals shippingInstruction.ShippingInstructionID
                        join transporter in _context.Transporters on bin.TransporterID equals transporter.TransporterID

                        where transaction.HubID == hubID && transaction.StoreID == StoreID
                        
                        select new BinCardReport()
                            {
                                Transporter = transporter.Name,
                                TransporterAM= transporter.NameAM,
                                DriverName = bin.DriverName,
                                PlateNoPrime = bin.PlateNo_Prime,
                                Date = bin.ReceiptDate,
                                Identification=bin.GRN,
                                ToFrom = donor.Name,
                                Project = projectCode.Value,
                                SINumber = shippingInstruction.Value,
                                Dispatched=null,
                                Received = transaction.QuantityInMT
                            }

                    );

             var z = x.Union(y).OrderBy(d => d.Date);

             var r = (from each in z

                      select new BinCardReport()
                         {
                             Transporter = each.Transporter,
                             TransporterAM = each.TransporterAM,
                             DriverName = each.DriverName,
                             PlateNoPrime = each.PlateNoPrime,
                             Date = each.Date,
                             Identification = each.Identification,
                             ToFrom = each.ToFrom,
                             Project = each.Project,
                             SINumber =  each.SINumber,
                             Dispatched = each.Dispatched,
                             Received = each.Received,
                         }
                    ).ToList();
            return r;
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
            /*
             select *,StoreID = 0 from 
            (select s.Number as StoreNo,
             * pc.Value as Project,
             * si.Value as SINumber, 
             * SUM(t.QuantityInMT) as Balance 
             * 
             * from [Transaction] t 
             * join ShippingInstruction si on t.ShippingInstructionID = si.ShippingInstructionID 
             * join ProjectCode pc on t.ProjectCodeID = pc.ProjectCodeID 
             * join Store s on t.StoreID = s.StoreID where t.HubID = @Warehouse and (t.ParentCommodityID = @commodity) 
             * and LedgerID in (2) group by s.StoreID, s.Number, si.Value,pc.Value ) as PivotData
		            PIVOT
		            (
		            SUM(Balance)
		            For StoreNo in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20])
		            ) AS PivotTable
             */

            //(
            //    from transaction in _context.Transactions
            //    join shippingInstruction in _context.ShippingInstructions on transaction.ShippingInstructionID equals
            //        shippingInstruction.ShippingInstructionID
            //    join projectCode in _context.ProjectCodes on transaction.ProjectCodeID equals projectCode.ProjectCodeID

            //    join store in _context.Stores on transaction.StoreID equals store.StoreID
            //    where (transaction.HubID == hubID && transaction.ParentCommodityID == commodityID)
            //    select new {transaction.HubID} as 
            //}
            //)
           

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