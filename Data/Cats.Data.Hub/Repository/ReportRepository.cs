using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Cats.Models.Hubs;

namespace Cats.Data.Hub.Repository
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
                       // transporter1.NameAM,
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
                        where transaction1.HubID == hubID && transaction1.StoreID == StoreID
                        //in (from com in Commodity where ParentId==CommodityID || CommodityID==CommodityID select CommodityID)
                        //)
                        select new BinCardReport()
                            {
                                Transporter = transporter1.Name,
                                //TransporterAM = transporter1.NameAM,
                                DriverName = item.DriverName,
                                PlateNoPrime = item.PlateNo_Prime,
                                Date = item.DispatchDate,
                                Identification = item.GIN,
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
                                //TransporterAM = transporter.NameAM,
                                DriverName = bin.DriverName,
                                PlateNoPrime = bin.PlateNo_Prime,
                                Date = bin.ReceiptDate,
                                Identification = bin.GRN,
                                ToFrom = donor.Name,
                                Project = projectCode.Value,
                                SINumber = shippingInstruction.Value,
                                Dispatched = null,
                                Received = transaction.QuantityInMT
                            }

                    );

            var z = x.Union(y).OrderBy(d => d.Date);

            var r = (from each in z

                     select new BinCardReport()
                        {
                            Transporter = each.Transporter,
                            DriverName = each.DriverName,
                            PlateNoPrime = each.PlateNoPrime,
                            Date = each.Date,
                            Identification = each.Identification,
                            ToFrom = each.ToFrom,
                            Project = each.Project,
                            SINumber = each.SINumber,
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
        public DataTable RPTStockStatus(int hubID, int commodityID)
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


            var result = (from t in _context.Transactions
                          join shippingInstruction in _context.ShippingInstructions on t.ShippingInstructionID equals
                              shippingInstruction.ShippingInstructionID
                          join projectCode in _context.ProjectCodes on t.ProjectCodeID equals projectCode.ProjectCodeID
                          join store in _context.Stores on t.StoreID equals store.StoreID
                          where t.HubID == hubID && t.ParentCommodityID == commodityID && t.LedgerID == 2
                          group t by new { store.StoreID, store.Number, sivalue = shippingInstruction.Value, projectCode.Value } into g
                          select new
                          {
                              StoreNo = g.Key.Number,
                              Project = g.Key.Value,
                              SINumber = g.Key.sivalue,
                              Balance = g.Sum(t => t.QuantityInMT)
                          });

            if (!result.Any()) return new DataTable();

            var storeMin = result.Min(t => t.StoreNo);
            var storeMax = result.Max(t => t.StoreNo);
            var stores = new List<int>();
            for (int i = storeMin; i < storeMax; i++)
            {
                stores.Add(i);
            }

            var query = result.GroupBy(r => new { r.StoreNo, r.Project, r.SINumber }).Select(storeGroup => new
            {

                StoreNo = storeGroup.Key,
                storeGroup.Key.Project,
                storeGroup.Key.SINumber,
                Values = from lang in stores
                         join ng in storeGroup
                             on lang equals
                             ng.StoreNo into
                             languageGroup
                         select new
                         {
                             Column = lang,
                             Value =
                  languageGroup.Any()
                      ? languageGroup.
                            FirstOrDefault().
                            Balance
                      : 0
                         }
            }).ToList();



            DataTable dt = new DataTable("transpose");

            var col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "Project";
            col.ExtendedProperties.Add("ID", -1);
            dt.Columns.Add(col);

            var col1 = new DataColumn();
            col1.DataType = typeof(string);
            col1.ColumnName = "SINumber";
            col1.ExtendedProperties.Add("ID", -1);
            dt.Columns.Add(col1);

            foreach (var store in stores)
            {
                var col3 = new DataColumn();
                col3.DataType = typeof(decimal);
                col3.ExtendedProperties.Add("ID", store);
                col3.ColumnName = store.ToString();
                dt.Columns.Add(col3);

            }
            foreach (var requestDetail in query)
            {

                var dr = dt.NewRow();

                dr[col] = requestDetail.Project;
                dr[col1] = requestDetail.SINumber;



                foreach (var requestDetailCommodity in requestDetail.Values)
                {

                    DataColumn colx = null;
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (requestDetailCommodity.Column.ToString() ==
                            column.ExtendedProperties["ID"].ToString())
                        {
                            colx = column;
                            break;
                        }
                    }
                    if (colx != null)
                    {
                        dr[colx.ColumnName] = requestDetailCommodity.Value;

                    }
                }
                dt.Rows.Add(dr);
            }




            return dt;

        }

        //public System.Data.Objects.ObjectResult<StockStatusReport> RPT_StockStatus(int hubID, int commodityID)
        //{



        //    return _context.RPT_StockStatus(hubID, commodityID);
        //}

        public System.Data.Objects.ObjectResult<StockStatusReport> RPT_StockStatusNonFood(int? hubID, int? commodityID)
        {
            return _context.RPT_StockStatusNonFood(hubID, commodityID);
        }

        public System.Data.Objects.ObjectResult<StatusReportBySI_Result> GetStatusReportBySI(int? hubID)
        {



            /*
               
	select 
		c.CommodityID, 
		pc.Value as ProjectCode,
		c.Name as CommodityName, 
		uncomm.SINumber,
		isnull(uncomm.UncommitedStock,0) - (ISNULL(commitedToOthers.CommitedBalance, 0) + ISNULL(commitedToFDP.CommitedBalance,0))  UncommitedStock, 
		isnull(commitedToFDP.CommitedBalance,0) + isnull(commitedToOthers.CommitedBalance,0) CommitedBalance, 
		isnull(uncomm.UncommitedStock,0) as TotalStockOnHand, 
		receiveAllocated.AllocatedBalance AllocatedToHub, 
		received.ReceivedBalance, 
		(case when receiveAllocated.AllocatedBalance = 0 then 1 else received.ReceivedBalance / receiveAllocated.AllocatedBalance end) * 100 as PercentageReceived  
	 from  
	 
	(
		
	
	
	left join 
	(
		select raBeforeAggregation.CommodityID, raBeforeAggregation.ShippingInstructionID, sum(raBeforeAggregation.AllocatedBalance) as AllocatedBalance
             * from 
		(select 
			case when (select top 1 c.ParentID from Commodity c where CommodityID = t.CommodityID) is null then t.CommodityID else (select top 1 c.ParentID from Commodity c where c.CommodityID = t.CommodityID) end  as CommodityID ,
			si.ShippingInstructionID , 
			t.QuantityInMT AllocatedBalance 
			
             * from [ReceiptAllocation] t join ShippingInstruction si on t.SINumber = si.Value 
			where t.QuantityInMT > 0 and t.HubID = @Hub 
			) raBeforeAggregation
			group by raBeforeAggregation.ShippingInstructionID, raBeforeAggregation.CommodityID
			 ) receiveAllocated
		on received.CommodityID = receiveAllocated.CommodityID and received.ShippingInstructionID = receiveAllocated.ShippingInstructionID
	
	
	join Commodity c on uncomm.CommodityID = c.CommodityID
	join ProjectCode pc on received.ProjectCodeID = pc.ProjectCodeID

END

            */
            #region       Com
               //( from transaction in _context.Transactions where (transaction.LedgerID==2 && transaction.QuantityInMT > 0 && transaction.HubID==hubID )
               //      group transaction by new {transaction.ShippingInstructionID,CommodityID=transaction.ParentCommodityID,transaction.ProjectCodeID} into received
               //      select new { received.Key.CommodityID,received.Key.ShippingInstructionID ,received.Key.ProjectCodeID, ReceivedBalance=received.Sum(x=>x.QuantityInMT) } )
                     
                    
               //  join t1 in 
               //  (from transaction1 in _context.Transactions join shippingInstruction in _context.ShippingInstructions on transaction1.ShippingInstructionID equals shippingInstruction.ShippingInstructionID  
               //   where transaction1.LedgerID==2 && transaction1.HubID==hubID group transaction1 by new {transaction1.ShippingInstructionID,transaction1.ParentCommodityID,shippingInstruction.Value ,transaction1.ProjectCodeID} into uncomm  
               //   select new {SINumber=uncomm.Key.Value,uncomm.Key.ShippingInstructionID,uncomm.Key.ProjectCodeID ,CommodityID=uncomm.Key.ParentCommodityID,UncommitedStock=uncomm.Sum(x=>x.QuantityInMT) }).ToList() 
               //   on  new {received.Key.CommodityID,received.Key.ShippingInstructionID,received.Key.ProjectCodeID }equals new{t1.CommodityID,t1.ShippingInstructionID,t1.ProjectCodeID} 
            //      join t2 in 
            //    from dispatchAllocation in _context.DispatchAllocations where dispatchAllocation.HubID==hubID && dispatchAllocation.ShippingInstructionID !=null
            //         group dispatchAllocation by new {dispatchAllocation.ShippingInstructionID,dispatchAllocation.CommodityID,dispatchAllocation.ProjectCodeID} into commitedtoFDP
            //         select new
            //{
            //    commitedtoFDP.Key.CommodityID,
            //    commitedtoFDP.Key.ProjectCodeID,
            //    commitedtoFDP.Key.ShippingInstructionID,
            //    CommitedBalance = commitedtoFDP.Sum(x => x.Amount/10)
            //}
        
            //    on new {received.Key.CommodityID,received.Key.ShippingInstructionID,received.Key.ProjectCodeID} equals new {t2.CommodityID,t2.ShippingInstructionID,t2.ProjectCodeID}
            //    )
                //join t3  in
                //(from otherDispatchAllocation in _context.OtherDispatchAllocations where otherDispatchAllocation.HubID==hubID 
                //  group otherDispatchAllocation by new {otherDispatchAllocation.ShippingInstructionID,otherDispatchAllocation.CommodityID,otherDispatchAllocation.ProjectCodeID} into commitedToOthers
                //  select new {commitedToOthers.Key.CommodityID,commitedToOthers.Key.ProjectCodeID,commitedToOthers.Key.ShippingInstructionID,CommitedBalance=commitedToOthers.Sum(x=>x.QuantityInMT)}).ToList()
                //on new {}
#endregion
            
 //(from y  in       (from x in     (from transaction in _context.Transactions where (transaction.LedgerID==2 && transaction.QuantityInMT > 0 && transaction.HubID==hubID )
 //                    group transaction by new {transaction.ShippingInstructionID,CommodityID=transaction.ParentCommodityID,transaction.ProjectCodeID} into received select new {received.Key.CommodityID,received.Key.ShippingInstructionID,received.Key.ProjectCodeID,RecivedBalance=received.Sum(x=>x.QuantityInMT)})
 //            join t1 in 
 //               ( from transaction1 in _context.Transactions join shippingInstruction in _context.ShippingInstructions on transaction1.ShippingInstructionID equals shippingInstruction.ShippingInstructionID  
 //                 where transaction1.LedgerID==2 && transaction1.HubID==hubID group transaction1 by new {transaction1.ShippingInstructionID,transaction1.ParentCommodityID,shippingInstruction.Value ,transaction1.ProjectCodeID} into uncomm select new {SINumber=uncomm.Key.Value,uncomm.Key.ShippingInstructionID,uncomm.Key.ProjectCodeID,CommodityID=uncomm.Key.ParentCommodityID,UncommitedStock=uncomm.Sum(x=>x.QuantityInMT)} )
 //            on new {x.CommodityID,x.ShippingInstructionID,x.ProjectCodeID} equals new {t1.CommodityID,t1.ShippingInstructionID,t1.ProjectCodeID}
 //            join t2 in 
 //               (from dispatchAllocation in _context.DispatchAllocations where dispatchAllocation.HubID==hubID && dispatchAllocation.ShippingInstructionID !=null
 //                    group dispatchAllocation by new {dispatchAllocation.ShippingInstructionID,dispatchAllocation.CommodityID,dispatchAllocation.ProjectCodeID} into commitedtoFDP select new {commitedtoFDP.Key.CommodityID,commitedtoFDP.Key.ProjectCodeID,commitedtoFDP.Key.ShippingInstructionID,CommitedBalance=commitedtoFDP.Sum(x=>x.Amount/10)})    
 //            on new {x.CommodityID,x.ShippingInstructionID,x.ProjectCodeID} equals new {t2.CommodityID,t2.ShippingInstructionID,t2.ProjectCodeID}
 //          join t3  in
 //               (from otherDispatchAllocation in _context.OtherDispatchAllocations where otherDispatchAllocation.HubID==hubID 
 //                 group otherDispatchAllocation by new {otherDispatchAllocation.ShippingInstructionID,otherDispatchAllocation.CommodityID,otherDispatchAllocation.ProjectCodeID} into commitedToOthers select new{commitedToOthers.Key.CommodityID,commitedToOthers.Key.ProjectCodeID,commitedToOthers.Key.ShippingInstructionID,CommitedBalance=commitedToOthers.Sum(x=>x.QuantityInMT)})
 //         on new {}
                   
 //  from receiptAllocation in _context.ReceiptAllocations 
 //           join shippingInstruction in _context.ShippingInstructions
 //           on receiptAllocation.SINumber equals shippingInstruction.Value
 //           where receiptAllocation.QuantityInMT > 0 && receiptAllocation.HubID==hubID
 //           group receiptAllocation by new {receiptAllocation.ShippingInstructionID,receiptAllocation.CommodityID} into receiveAllocated
 //          on new {received.Key.CommodityID,received.Key.ShippingInstructionID} equals new {recivedAllocated.key.CommodityID,recivedAllocated.Key.ShippingInstructionID}
 //          join commodity in _context.Commodity on uncomm.CommodityI equals commodity.CommodityID
 //          join projectCode in _contex.ProjectCode on received.ProjectCodeID equals projectcode.ProjectCodeID

 //       //          select 
 //       //c.CommodityID, 
 //       //pc.Value as ProjectCode,
 //       //c.Name as CommodityName, 
 //       //uncomm.SINumber,
 //       //isnull(uncomm.UncommitedStock,0) - (ISNULL(commitedToOthers.CommitedBalance, 0) + ISNULL(commitedToFDP.CommitedBalance,0))  UncommitedStock, 
 //       //isnull(commitedToFDP.CommitedBalance,0) + isnull(commitedToOthers.CommitedBalance,0) CommitedBalance, 
 //       //isnull(uncomm.UncommitedStock,0) as TotalStockOnHand, 
 //       //receiveAllocated.AllocatedBalance AllocatedToHub, 
 //       //received.ReceivedBalance, 
 //       //(case when receiveAllocated.AllocatedBalance = 0 then 1 else received.ReceivedBalance / receiveAllocated.AllocatedBalance end) * 100 as PercentageReceived  
 //                 )
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