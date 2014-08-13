using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.Hub.UnitWork;
using Cats.Services.Hub.Interfaces;
using Cats.Data.Hub;
using Cats.Models.Hubs;
//using Cats.Helpers;

namespace Cats.Services.Hub
{
    public class StockStatusService : IStockStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramService _programService;
        private readonly ITransactionService _transactionService;

        public StockStatusService(IUnitOfWork unitOfWork,
                                   IProgramService programService,
                                   ITransactionService transactionService)
        {
            _unitOfWork = unitOfWork;
            _programService = programService;
            _transactionService = transactionService;
        }

        public IOrderedEnumerable<HubView> GetHubs()
        {
            var hubs = _unitOfWork.HubRepository.GetAll();
            return hubs.Select(hub => new HubView
            {
                Name = hub.Name + " " + hub.HubOwner.Name,
                HubId = hub.HubID
            }).OrderBy(e => e.Name);
        }

        public IEnumerable<ProgramView> GetPrograms()
        {
            var programs = _unitOfWork.ProgramRepository.GetAll();

            return programs.Select(program => new ProgramView
                {
                    Name = program.Name,
                    ProgramId = program.ProgramID
                }
            );
        }

        //TODO: use the date parameter in the query. 
        public List<HubFreeStockView> GetFreeStockStatusD(int hub, int program, DateTime date)
        {
            string query =
                string.Format(
                    @"SELECT GOH.CommodityID, GOH.QuantityInMT PhysicalStock, Commited.QuantityInMT Commited, (GOH.QuantityInMT - ISNULL(Commited.QuantityInMT,0) ) FreeStock, Commodity.Name CommodityName FROM 

                                                (SELECT SUM(QuantityInMT) QuantityInMT, CommodityID 
	                                                FROM [Transaction]
	                                                WHERE LedgerID = {0} and HubID  = {2} and ProgramID = {3} 
	                                                GROUP BY CommodityID) GOH
	                                                LEFT JOIN
		                                                (SELECT ABS(SUM(QuantityInMT)) QuantityInMT, CommodityID 
			                                                FROM [Transaction]
			                                                WHERE LedgerID = {1} and HubID  = {2} and ProgramID = {3} AND TransactionDate < =  {4}
			                                                GROUP BY CommodityID) Commited

			                                                ON GOH.CommodityID = Commited.CommodityID
			                                                JOIN Commodity on Commodity.CommodityID = GOH.CommodityID", Cats.Models.Ledger.Constants.GOODS_ON_HAND, Cats.Models.Ledger.Constants.COMMITED_TO_FDP, hub, program, "'" + date.AddDays(1).ToString(CultureInfo.InvariantCulture) + "'");
            
            return _unitOfWork.Database.SqlQuery<HubFreeStockView>(query).ToList();
        }

        public List<HubFreeStockView> GetFreeStockStatus(int hub, int program, string date)
        {
            DateTime _date = Convert.ToDateTime(date);
            return GetFreeStockStatusD(hub, program, _date);
        }

        public List<HubFreeStockSummaryView> GetStockSummary(int program, string date)
        {
            DateTime _date = Convert.ToDateTime(date);
            return GetStockSummaryD(program, _date);
          
        }

        public List<SummaryFreeAndPhysicalStockModel> GetFreeAndPhysicalStockSummary()
        {


            string query = string.Format(@"SELECT GOH.CommodityID, GOH.ProgramID, GOH.HubID, GOH.QuantityInMT PhysicalStock, Commited.QuantityInMT Commited, (GOH.QuantityInMT - ISNULL(Commited.QuantityInMT,0) ) FreeStock,
                                                Commodity.Name CommodityName, Program.Name Program,Hub.Name HubName FROM 

                                                (SELECT SUM(QuantityInMT) QuantityInMT, CommodityID, ProgramID, HubID
	                                                FROM [Transaction]
	                                                WHERE LedgerID = {0} AND  TransactionDate < =  GETDATE()
	                                                GROUP BY CommodityID,ProgramID,HubID) GOH
	                                                LEFT JOIN
		                                                (SELECT ABS(SUM(QuantityInMT)) QuantityInMT, CommodityID, ProgramID, HubID
			                                                FROM [Transaction]
			                                                WHERE LedgerID = {1} AND TransactionDate < =  GETDATE()
			                                                GROUP BY CommodityID,ProgramID,HubID) Commited

			                                                ON GOH.CommodityID = Commited.CommodityID
			                                                JOIN Commodity on Commodity.CommodityID = GOH.CommodityID
															JOIN Program on Program.ProgramID = GOH.ProgramID
															JOIN Hub on Hub.HubID = GOH.HubID",Cats.Models.Ledger.Constants.GOODS_ON_HAND,Cats.Models.Ledger.Constants.COMMITED_TO_FDP);
            return _unitOfWork.Database.SqlQuery<SummaryFreeAndPhysicalStockModel>(query).ToList();



           
        }

      public List<HubFreeStockSummaryView> GetStockSummaryD(int program, DateTime date)
        {

            string query = string.Format(@"SELECT  GOH.HubID, GOH.QuantityInMT TotalPhysicalStock, (GOH.QuantityInMT - ISNULL(Commited.QuantityInMT,0) ) TotalFreestock, 
                                                Hub.Name HubName FROM 

                                                (SELECT SUM(QuantityInMT) QuantityInMT, ProgramID, HubID
	                                                FROM [Transaction]
	                                                WHERE LedgerID = {0}  AND ProgramID = {3}
	                                                GROUP BY ProgramID,HubID) GOH
	                                                LEFT JOIN
		                                                (SELECT ABS(SUM(QuantityInMT)) QuantityInMT, ProgramID, HubID
			                                                FROM [Transaction]
			                                                WHERE LedgerID = {1}  AND TransactionDate < =  {2} AND ProgramID = {3}
			                                                GROUP BY ProgramID,HubID) Commited

			                                                ON GOH.HubID = Commited.HubID
			                                               														
															JOIN Hub on Hub.HubID = GOH.HubID
															JOin Program on Program.ProgramID =GOH.ProgramID
															", Cats.Models.Ledger.Constants.GOODS_ON_HAND, Cats.Models.Ledger.Constants.COMMITED_TO_FDP, "'"  + date.AddDays(1).ToString(CultureInfo.InvariantCulture) + "'", program);
            return _unitOfWork.Database.SqlQuery<HubFreeStockSummaryView>(query).ToList();

        }

      public List<HubFreeStockSummaryView> GetStockSummaryHubDahsBoard(int hubId, DateTime date)
      {

          string query = string.Format(@"SELECT  GOH.HubID, SUM(GOH.QuantityInMT) TotalPhysicalStock, SUM(GOH.QuantityInMT - ISNULL(Commited.QuantityInMT,0) ) TotalFreestock, 
                                                Hub.Name HubName FROM 

                                                (SELECT SUM(QuantityInMT) QuantityInMT, ProgramID, HubID
	                                                FROM [Transaction]
	                                                WHERE LedgerID = {0}  AND HubID = {3}
	                                                GROUP BY ProgramID,HubID) GOH
	                                                LEFT JOIN
		                                                (SELECT ABS(SUM(QuantityInMT)) QuantityInMT, ProgramID, HubID
			                                                FROM [Transaction]
			                                                WHERE LedgerID = {1}  AND TransactionDate < =  {2} AND HubID = {3}
			                                                GROUP BY ProgramID,HubID) Commited

			                                                ON GOH.HubID = Commited.HubID
			                                               														
															JOIN Hub on Hub.HubID = GOH.HubID
															JOin Program on Program.ProgramID =GOH.ProgramID
                                                            Group by GOH.HubID,Hub.Name
															", Cats.Models.Ledger.Constants.GOODS_ON_HAND, Cats.Models.Ledger.Constants.COMMITED_TO_FDP, "'" + date.AddDays(1).ToString(CultureInfo.InvariantCulture) + "'", hubId);
          return _unitOfWork.Database.SqlQuery<HubFreeStockSummaryView>(query).ToList();

      }

      public List<VWCommodityReceived> GetReceivedCommodity (Expression<Func<VWCommodityReceived, bool>> filter = null)
      {
          return _unitOfWork.VWCommodityReceived.Get(filter,null,string.Empty).ToList();
      }
 
      public List<VWFreePhysicalStock> GetSummaryFreePhysicalStock(Expression<Func<VWFreePhysicalStock, bool>> filter = null)
      {
          return _unitOfWork.VWFreePhysicalStock.Get(filter, null, string.Empty).ToList();
      }

        public List<VWDispatchCommodity> GetDispatchedCommodity(
            Expression<Func<VWDispatchCommodity, bool>> filter = null)

      {
          return _unitOfWork.VWDispatchCommodity.Get(filter, null, string.Empty).ToList();
      }

        public List<VWCarryOver> GetCarryOverStock(Expression<Func<VWCarryOver, bool>> filter = null)
      {
          return _unitOfWork.VWCarryOver.Get(filter, null, string.Empty).ToList();
      }
      public List<VWTransferredStock> GetTransferredStock(Expression<Func<VWTransferredStock, bool>> filter = null)
      {
          return _unitOfWork.VWTransferredStock.Get(filter, null, string.Empty).ToList();

      }
     public List<HubDispatchAllocationViewModel> GetHubDispatchAllocation(int program, DateTime date)

    {
        string query = string.Format(@" SELECT  GOH.HubID, GOH.QuantityInMT TotalPhysicalStock, (GOH.QuantityInMT - ISNULL(Commited.QuantityInMT,0) ) TotalFreestock, (ISNULL(DispatchedQuantity.QuantityInMT,0)) Dispatched,
                                                Hub.Name HubName ,(GOH.QuantityInMT -ISNULL(DispatchedQuantity.QuantityInMT,0)) Remaining
												FROM 

                                                (SELECT SUM(QuantityInMT) QuantityInMT, ProgramID, HubID
	                                                FROM  [CatsMaster].[dbo].[Transaction]
	                                                WHERE LedgerID = {0}  AND ProgramID = {4}
	                                                GROUP BY ProgramID,HubID) GOH
	                                                LEFT JOIN
		                                                (SELECT ABS(SUM(QuantityInMT)) QuantityInMT, ProgramID, HubID
			                                                FROM  [CatsMaster].[dbo].[Transaction]
			                                                WHERE LedgerID = {1}  AND TransactionDate < =  {3} AND ProgramID = {4}
			                                                GROUP BY ProgramID,HubID) Commited
															  ON GOH.HubID = Commited.HubID
													LEFt JOIN (SELECT ABS(SUM(QuantityInMT)) QuantityInMT, ProgramID, HubID
			                                                FROM  [CatsMaster].[dbo].[Transaction]
			                                                WHERE LedgerID = {2}  AND TransactionDate < =  {3} AND ProgramID = {4}
															 GROUP BY ProgramID,HubID) DispatchedQuantity

			                                                ON GOH.HubID = DispatchedQuantity.HubID

															JOIN [CatsMaster].[dbo].Hub on Hub.HubID = GOH.HubID
															", Cats.Models.Ledger.Constants.GOODS_ON_HAND, Cats.Models.Ledger.Constants.COMMITED_TO_FDP, Cats.Models.Ledger.Constants.GOODS_IN_TRANSIT, "'" + date.AddDays(1).ToString(CultureInfo.InvariantCulture) + "'", program);
        return _unitOfWork.Database.SqlQuery<HubDispatchAllocationViewModel>(query).ToList();
    }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}