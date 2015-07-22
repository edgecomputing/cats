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

        public IEnumerable<CommodityView> GetCommodity()
        {
            var commodity = _unitOfWork.CommodityRepository.GetAll();
            return commodity.Select(c => new CommodityView
            {
                Name = c.Name,
                CommodityId = c.CommodityID
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
	                                                WHERE LedgerID = {0} and HubID  = {2} and ProgramID = {3} and ShippingInstructionID IS NOT NULL
	                                                GROUP BY CommodityID) GOH
	                                                LEFT JOIN
		                                                (SELECT ABS(SUM(QuantityInMT)) QuantityInMT, CommodityID 
			                                                FROM [Transaction]
			                                                WHERE LedgerID = {1} and HubID  = {2} and ProgramID = {3} AND TransactionDate < =  {4} and ShippingInstructionID IS NOT NULL
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
	                                                WHERE LedgerID = {0} AND  TransactionDate < =  GETDATE() and ShippingInstructionID IS NOT NULL
	                                                GROUP BY CommodityID,ProgramID,HubID) GOH
	                                                LEFT JOIN
		                                                (SELECT ABS(SUM(QuantityInMT)) QuantityInMT, CommodityID, ProgramID, HubID
			                                                FROM [Transaction]
			                                                WHERE LedgerID = {1} AND TransactionDate < =  GETDATE() and ShippingInstructionID IS NOT NULL
			                                                GROUP BY CommodityID,ProgramID,HubID) Commited

			                                                ON GOH.CommodityID = Commited.CommodityID
			                                                JOIN Commodity on Commodity.CommodityID = GOH.CommodityID
															JOIN Program on Program.ProgramID = GOH.ProgramID
															JOIN Hub on Hub.HubID = GOH.HubID", Cats.Models.Ledger.Constants.GOODS_ON_HAND,Cats.Models.Ledger.Constants.COMMITED_TO_FDP);
            return _unitOfWork.Database.SqlQuery<SummaryFreeAndPhysicalStockModel>(query).ToList();



           
        }

      public List<HubFreeStockSummaryView> GetStockSummaryD(int program, DateTime date)
        {

            string query = string.Format(@"SELECT  GOH.HubID, GOH.QuantityInMT TotalPhysicalStock, (GOH.QuantityInMT - ISNULL(Commited.QuantityInMT,0) ) TotalFreestock, 
                                                Hub.Name HubName FROM 

                                                (SELECT SUM(QuantityInMT) QuantityInMT, ProgramID, HubID
	                                                FROM [Transaction]
	                                                WHERE LedgerID = {0}  AND ProgramID = {3} and ShippingInstructionID IS NOT NULL
	                                                GROUP BY ProgramID,HubID) GOH
	                                                LEFT JOIN
		                                                (SELECT ABS(SUM(QuantityInMT)) QuantityInMT, ProgramID, HubID
			                                                FROM [Transaction]
			                                                WHERE LedgerID = {1}  AND TransactionDate < =  {2} AND ProgramID = {3} and ShippingInstructionID IS NOT NULL
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
	                                                WHERE LedgerID = {0}  AND HubID = {3} and ShippingInstructionID IS NOT NULL
	                                                GROUP BY ProgramID,HubID) GOH
	                                                LEFT JOIN
		                                                (SELECT ABS(SUM(QuantityInMT)) QuantityInMT, ProgramID, HubID
			                                                FROM [Transaction]
			                                                WHERE LedgerID = {1}  AND TransactionDate < =  {2} AND HubID = {3} and ShippingInstructionID IS NOT NULL
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
          string query =
              string.Format(@" SELECT  GOH.HubID, GOH.QuantityInMT TotalPhysicalStock, (GOH.QuantityInMT - ISNULL(Commited.QuantityInMT,0) ) TotalFreestock, (ISNULL(DispatchedQuantity.QuantityInMT,0)) DispatchedAmount,
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
															", Cats.Models.Ledger.Constants.GOODS_ON_HAND,
                            Cats.Models.Ledger.Constants.COMMITED_TO_FDP, Cats.Models.Ledger.Constants.GOODS_IN_TRANSIT,
                            "'" + date.AddDays(1).ToString(CultureInfo.InvariantCulture) + "'", program);
          return _unitOfWork.Database.SqlQuery<HubDispatchAllocationViewModel>(query).ToList();
      }

        public List<StockAdjustmentViewModel> Adjustment(int programId,int hubId,int commodityId, int stockType)
        {
            var ledger = stockType == 0
                             ? Cats.Models.Ledger.Constants.GOODS_ON_HAND
                             : Cats.Models.Ledger.Constants.COMMITED_TO_FDP;

            var query = string.Format("SELECT SUM(QuantityInMT) QuantityInMT,c.Name as commodityName, t.CommodityID ,p.Name as ProgramName,t.ProgramID, h.Name as HubName, s.Value as SINumber, s.ShippingInstructionID,t.ProjectCodeID" + Environment.NewLine + 
	                                                "FROM [Transaction] t inner join ShippingInstruction s on t.ShippingInstructionID = s.ShippingInstructionID" + Environment.NewLine + 
													"JOIN Commodity c on c.CommodityID = t.CommodityID" + Environment.NewLine +
                                                    "JOIN Program p on P.ProgramID = t.ProgramID" + Environment.NewLine + 
													"JOIN Hub H on H.HubID = t.HubID" + Environment.NewLine +
                                                    "WHERE LedgerID = {0} AND H.HubID  = {1} AND P.ProgramID = {2} AND t.CommodityID = {3} " + Environment.NewLine +
                                                    "GROUP BY  t.CommodityID,s.Value,s.ShippingInstructionID,c.Name,p.Name, h.Name,t.ProgramID,t.ProjectCodeID", ledger, hubId, programId, commodityId);

            return _unitOfWork.Database.SqlQuery<StockAdjustmentViewModel>(query).ToList();
        }

        public void SaveAdjustment(StockAdjustmentViewModel viewModel, UserProfile user,int stockType)
        {
            Commodity commodity = _unitOfWork.CommodityRepository.FindById((int)viewModel.CommodityID);
            int ledgerPlus, LedgerMinus;
            if (stockType == 0)
            {
                LedgerMinus = Cats.Models.Ledger.Constants.GOODS_ON_HAND;
                ledgerPlus = Cats.Models.Ledger.Constants.LOSS_IN_TRANSIT;
            }
            else
            {
                LedgerMinus = Cats.Models.Ledger.Constants.COMMITED_TO_FDP;
                ledgerPlus = Cats.Models.Ledger.Constants.PLEDGED_TO_FDP;
            }
           


            Adjustment lossAndAdjustment = new Adjustment();
            TransactionGroup transactionGroup = new TransactionGroup();
            Transaction transactionOne = new Transaction();

            var transactionGroupId = Guid.NewGuid();

            transactionOne.TransactionID = Guid.NewGuid();
            transactionOne.TransactionGroupID = transactionGroupId;
            transactionOne.LedgerID = LedgerMinus;// 2;
            transactionOne.HubOwnerID = user.DefaultHubObj.HubOwner.HubOwnerID;
            //transactionOne.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, user.DefaultHub.Value); // 
            transactionOne.HubID = user.DefaultHub.Value;
            transactionOne.StoreID = viewModel.StoreID;  //
            transactionOne.ProjectCodeID = viewModel.ProjectCodeID;
            transactionOne.ShippingInstructionID = viewModel.ShippingInstructionID;

            transactionOne.ParentCommodityID = (commodity.ParentID == null)
                                                       ? commodity.CommodityID
                                                       : commodity.ParentID.Value;
            transactionOne.CommodityID = viewModel.CommodityID;
            transactionOne.ProgramID = viewModel.ProgramID;
            transactionOne.CommodityGradeID = null; // How did I get this value ? 
            
                transactionOne.QuantityInMT = 0-viewModel.QuantityInMT;
                transactionOne.QuantityInUnit =0- viewModel.QuantityInUnit;
            
          
            transactionOne.UnitID =1;
            transactionOne.TransactionDate = DateTime.Now;



            Transaction transactionTwo = new Transaction();

            transactionTwo.TransactionID = Guid.NewGuid();
            transactionTwo.TransactionGroupID = transactionGroupId;
            transactionTwo.LedgerID = ledgerPlus;// 14;
            transactionTwo.HubOwnerID = user.DefaultHubObj.HubOwnerID;
            //transactionTwo.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, user.DefaultHub.Value); // 
            transactionTwo.HubID = user.DefaultHub.Value;
            transactionTwo.StoreID = viewModel.StoreID;  //
            transactionTwo.ProjectCodeID = viewModel.ProjectCodeID;
            transactionTwo.ShippingInstructionID = viewModel.ShippingInstructionID;
            transactionTwo.ParentCommodityID = (commodity.ParentID == null)
                                                       ? commodity.CommodityID
                                                       : commodity.ParentID.Value;
            transactionTwo.CommodityID = viewModel.CommodityID;
            transactionTwo.ProgramID = viewModel.ProgramID;
            transactionTwo.CommodityGradeID = null; // How did I get this value ? 

            
                transactionTwo.QuantityInMT = viewModel.QuantityInMT;
                transactionTwo.QuantityInUnit = viewModel.QuantityInUnit;
            
         
            transactionTwo.UnitID =1;
            transactionTwo.TransactionDate = DateTime.Now;

            transactionGroup.TransactionGroupID = transactionGroupId;
            transactionGroup.Transactions.Add(transactionOne);
            transactionGroup.Transactions.Add(transactionTwo);


            lossAndAdjustment.PartitionId = 0;
            lossAndAdjustment.AdjustmentID = Guid.NewGuid();
            lossAndAdjustment.TransactionGroupID = transactionGroupId;
            lossAndAdjustment.TransactionGroup = transactionGroup;
            lossAndAdjustment.HubID = user.DefaultHub.Value;
            lossAndAdjustment.AdjustmentReasonID = 7;
            lossAndAdjustment.AdjustmentDirection = "S";
            lossAndAdjustment.AdjustmentDate = DateTime.Now.Date;
            lossAndAdjustment.ApprovedBy = "";
            lossAndAdjustment.Remarks = "Stock take adjustement";
            lossAndAdjustment.UserProfileID = user.UserProfileID;
            lossAndAdjustment.ReferenceNumber = "";
            lossAndAdjustment.StoreManName = "";



            // Try to save this transaction
            try
            {
                _unitOfWork.AdjustmentRepository.Add(lossAndAdjustment);
                _unitOfWork.Save();
            }
            catch (Exception exp)
            {
                // dbTransaction.Rollback();
                //TODO: Save the detail of this exception somewhere
                throw new Exception("The Internal Movement Transaction Cannot be saved. <br />Detail Message :" + exp.Message);
            }
        }

        public void RoolbackAllocation(StockAdjustmentViewModel viewModel, UserProfile user)
        {
            Commodity commodity = _unitOfWork.CommodityRepository.FindById((int)viewModel.CommodityID);



            Adjustment lossAndAdjustment = new Adjustment();
            TransactionGroup transactionGroup = new TransactionGroup();
            Transaction transactionOne = new Transaction();

            var transactionGroupId = Guid.NewGuid();

            transactionOne.TransactionID = Guid.NewGuid();
            transactionOne.TransactionGroupID = transactionGroupId;
            transactionOne.LedgerID = Cats.Models.Ledger.Constants.PLEDGED_TO_FDP;// 2;
            transactionOne.HubOwnerID = user.DefaultHubObj.HubOwner.HubOwnerID;
            //transactionOne.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, user.DefaultHub.Value); // 
            transactionOne.HubID = user.DefaultHub.Value;
            transactionOne.StoreID = viewModel.StoreID;  //
            transactionOne.ProjectCodeID = viewModel.ProjectCodeID;
            transactionOne.ShippingInstructionID = viewModel.ShippingInstructionID;

            transactionOne.ParentCommodityID = (commodity.ParentID == null)
                                                       ? commodity.CommodityID
                                                       : commodity.ParentID.Value;
            transactionOne.CommodityID = viewModel.CommodityID;
            transactionOne.ProgramID = viewModel.ProgramID;
            transactionOne.CommodityGradeID = null; // How did I get this value ? 

            transactionOne.QuantityInMT = 0 - viewModel.QuantityInMT;
            transactionOne.QuantityInUnit = 0 - viewModel.QuantityInUnit;


            transactionOne.UnitID = 1;
            transactionOne.TransactionDate = DateTime.Now;



            Transaction transactionTwo = new Transaction();

            transactionTwo.TransactionID = Guid.NewGuid();
            transactionTwo.TransactionGroupID = transactionGroupId;
            transactionTwo.LedgerID = Cats.Models.Ledger.Constants.COMMITED_TO_FDP;// 14;
            transactionTwo.HubOwnerID = user.DefaultHubObj.HubOwnerID;
            //transactionTwo.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, user.DefaultHub.Value); // 
            transactionTwo.HubID = user.DefaultHub.Value;
            transactionTwo.StoreID = viewModel.StoreID;  //
            transactionTwo.ProjectCodeID = viewModel.ProjectCodeID;
            transactionTwo.ShippingInstructionID = viewModel.ShippingInstructionID;
            transactionTwo.ParentCommodityID = (commodity.ParentID == null)
                                                       ? commodity.CommodityID
                                                       : commodity.ParentID.Value;
            transactionTwo.CommodityID = viewModel.CommodityID;
            transactionTwo.ProgramID = viewModel.ProgramID;
            transactionTwo.CommodityGradeID = null; // How did I get this value ? 


            transactionTwo.QuantityInMT = viewModel.QuantityInMT;
            transactionTwo.QuantityInUnit = viewModel.QuantityInUnit;


            transactionTwo.UnitID = 1;
            transactionTwo.TransactionDate = DateTime.Now;

            transactionGroup.TransactionGroupID = transactionGroupId;
            transactionGroup.Transactions.Add(transactionOne);
            transactionGroup.Transactions.Add(transactionTwo);


            lossAndAdjustment.PartitionId = 0;
            lossAndAdjustment.AdjustmentID = Guid.NewGuid();
            lossAndAdjustment.TransactionGroupID = transactionGroupId;
            lossAndAdjustment.TransactionGroup = transactionGroup;
            lossAndAdjustment.HubID = user.DefaultHub.Value;
            lossAndAdjustment.AdjustmentReasonID = 7;
            lossAndAdjustment.AdjustmentDirection = "A";
            lossAndAdjustment.AdjustmentDate = DateTime.Now.Date;
            lossAndAdjustment.ApprovedBy = "";
            lossAndAdjustment.Remarks = "Stock take adjustement - Allocation";
            lossAndAdjustment.UserProfileID = user.UserProfileID;
            lossAndAdjustment.ReferenceNumber = "";
            lossAndAdjustment.StoreManName = "";



            // Try to save this transaction
            try
            {
                _unitOfWork.AdjustmentRepository.Add(lossAndAdjustment);
                _unitOfWork.Save();
            }
            catch (Exception exp)
            {
                // dbTransaction.Rollback();
                //TODO: Save the detail of this exception somewhere
                throw new Exception("The Internal Movement Transaction Cannot be saved. <br />Detail Message :" + exp.Message);
            }
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}