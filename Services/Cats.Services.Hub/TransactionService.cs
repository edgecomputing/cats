using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Cats.Data.Hub;
using Cats.Data.Hub.UnitWork;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;
using Cats.Models.Hubs.ViewModels.Report;
using Cats.Models.Hubs.ViewModels.Report.Data;
using Cats.Models.Hubs;
using Ledger = Cats.Models.Ledger;


namespace Cats.Services.Hub
{
    public class TransactionService : ITransactionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;
        private readonly IShippingInstructionService _shippingInstructionService;
        private readonly IProjectCodeService _projectCodeService;

        public TransactionService(IUnitOfWork unitOfWork, IAccountService accountService, IShippingInstructionService shippingInstructionService, IProjectCodeService projectCodeService)
        {
            this._unitOfWork = unitOfWork;
            this._accountService = accountService;
            this._shippingInstructionService = shippingInstructionService;
            this._projectCodeService = projectCodeService;

        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _accountService.Dispose();
            _projectCodeService.Dispose();
            _shippingInstructionService.Dispose();
        }


        /// <summary>
        /// Gets the active accounts for ledger.
        /// </summary>
        /// <param name="LedgerID">The ledger ID.</param>
        /// <returns></returns>
        /// 

        public List<Account> GetActiveAccountsForLedger(int LedgerID)
        {
            var accounts =
                _unitOfWork.TransactionRepository.FindBy(t => t.LedgerID == LedgerID).Select(t => t.Account).ToList();

            return accounts;
        }


        /// <summary>
        /// Gets the transactions for ledger.
        /// </summary>
        /// <param name="LedgerID">The ledger ID.</param>
        /// <param name="AccountID">The account ID.</param>
        /// <param name="Commodity">The commodity.</param>
        /// <returns></returns>
        public List<Transaction> GetTransactionsForLedger(int LedgerID, int AccountID, int Commodity)
        {

            var transactions =
                _unitOfWork.TransactionRepository.FindBy(
                    t =>
                    (t.LedgerID == LedgerID && t.AccountID == AccountID) &&
                    (t.CommodityID == Commodity || t.ParentCommodityID == Commodity));

            return transactions;
        }


        /// <summary>
        /// Gets the total receipt allocation.
        /// </summary>
        /// <param name="siNumber">The SI number.</param>
        /// <param name="commodityId"></param>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        public decimal GetTotalReceiptAllocation(int siNumber, int commodityId, int hubId)
        {
            decimal totalAllocation = 0;
            var commodity = _unitOfWork.CommodityRepository.FindById(commodityId);
            if (commodity.ParentID != null)
            {
                commodityId = commodity.ParentID.Value;
            }
            var allocationSum = _unitOfWork.TransactionRepository.FindBy(
                t => t.ShippingInstructionID == siNumber
                     && t.HubID == hubId
                     && (t.ParentCommodityID == commodityId || t.CommodityID == commodityId)
                     && t.LedgerID == Cats.Models.Ledger.Constants.GIFT_CERTIFICATE
                     && t.QuantityInMT > 0
                ).Select(t => t.QuantityInMT).ToList();



            if (allocationSum.Any())
            {
                totalAllocation = allocationSum.Sum();
            }
            return totalAllocation;
        }

        /// <summary>
        /// Gets the total received from receipt allocation.
        /// </summary>
        /// <param name="siNumber">The SI number.</param>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        public decimal GetTotalReceivedFromReceiptAllocation(int siNumber, int commodityId, int hubId)
        {
            decimal totalAllocation = 0;
            var commodity = _unitOfWork.CommodityRepository.FindById(commodityId);
            if (commodity.ParentID != null)
            {
                commodityId = commodity.ParentID.Value;
            }
            var allocationSum = _unitOfWork.TransactionRepository.FindBy(t => t.ShippingInstructionID == siNumber
                                         && t.HubID == hubId
                                       && t.ParentCommodityID == commodityId
                                       && t.LedgerID == Cats.Models.Ledger.Constants.GOODS_ON_HAND
                                       && t.QuantityInMT > 0).Select(t => t.QuantityInMT).ToList();


            if (allocationSum.Any())
            {
                totalAllocation = allocationSum.Sum();
            }
            return totalAllocation;
        }

        /// <summary>
        /// Gets the commodity balance for store.
        /// </summary>
        /// <param name="storeId">The store id.</param>
        /// <param name="parentCommodityId">The parent commodity id.</param>
        /// <param name="si">The SI.</param>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public decimal GetCommodityBalanceForStore(int storeId, int parentCommodityId, int si, int project)
        {
            var balance = _unitOfWork.TransactionRepository.FindBy(t =>
                                                                   t.StoreID == storeId &&
                                                                   t.ParentCommodityID == parentCommodityId &&
                                                                   t.ShippingInstructionID == si &&
                                                                   t.ProjectCodeID == project &&
                                                                   t.LedgerID ==
                                                                  Cats.Models.Ledger.Constants.GOODS_ON_HAND

                ).Select(t => t.QuantityInMT).ToList();

            if (balance.Any())
            {
                return balance.Sum();
            }
            return 0;
        }


        /// <summary>
        /// Gets the commodity balance for stack.
        /// </summary>
        /// <param name="storeId">The store id.</param>
        /// <param name="stack">The stack.</param>
        /// <param name="parentCommodityId">The parent commodity id.</param>
        /// <param name="si">The SI.</param>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public decimal GetCommodityBalanceForStack(int storeId, int stack, int CommodityId, int si, int project)
        {
            var balance = _unitOfWork.TransactionRepository.FindBy(t =>
                                                                   t.StoreID == storeId &&
                                                                   t.CommodityID == CommodityId &&
                                                                   t.ShippingInstructionID == si &&
                                                                   t.ProjectCodeID == project && t.Stack == stack &&
                                                                   t.LedgerID ==
                                                                   Cats.Models.Ledger.Constants.GOODS_ON_HAND

                ).Select(t => t.QuantityInMT).ToList();

            if (balance.Any())
            {
                return balance.Sum();
            }

            return 0;
        }

        public decimal GetCommodityBalanceForStack2(int storeId, int parentCommodityId, int si, int project)
        {
            var balance = _unitOfWork.TransactionRepository.FindBy(t =>
                                                                   t.StoreID == storeId &&
                                                                   t.ParentCommodityID == parentCommodityId &&
                                                                   t.ShippingInstructionID == si &&
                                                                   t.ProjectCodeID == project &&
                                                                   t.LedgerID ==
                                                                   Cats.Models.Ledger.Constants.GOODS_ON_HAND

                ).Select(t => t.QuantityInMT).ToList();

            if (balance.Any())
            {
                return balance.Sum();
            }

            return 0;
        }



        /// <summary>
        /// Saves the receipt transaction.
        /// </summary>
        /// <param name="receiveModels">The receive models.</param>
        /// <param name="user">The user.</param>
        public Boolean SaveReceiptTransaction(ReceiveViewModel receiveModels, UserProfile user)
        {
            // Populate more details of the reciept object 
            // Save it when you are done.

            var receive = receiveModels.GenerateReceive();
            receive.CreatedDate = DateTime.Now;
            receive.HubID = user.DefaultHubObj.HubID;
            receive.UserProfileID = user.UserProfileID;

            int? donorId = receive.SourceDonorID;
            var commType = _unitOfWork.CommodityTypeRepository.FindById(receiveModels.CommodityTypeID);

            // var comms = GenerateReceiveDetail(commodities);


            var transactionGroupId = Guid.NewGuid();

            foreach (ReceiveDetailViewModel c in receiveModels.ReceiveDetails)
            {
                if (commType.CommodityTypeID == 2)//if it's a non food
                {
                    c.ReceivedQuantityInMT = 0;
                    c.SentQuantityInMT = 0;
                }
                TransactionGroup tgroup = new TransactionGroup();
                tgroup.TransactionGroupID = transactionGroupId;
                var receiveDetail = new ReceiveDetail()
                {
                    CommodityID = c.CommodityID,
                    Description = c.Description,
                    SentQuantityInMT = c.SentQuantityInMT.Value,
                    SentQuantityInUnit = c.SentQuantityInUnit.Value,
                    UnitID = c.UnitID,
                    ReceiveID = receive.ReceiveID,
                    ReceiveDetailID = Guid.NewGuid()
                };
                if (c.ReceiveDetailID.HasValue)
                {
                    receiveDetail.ReceiveDetailID = c.ReceiveDetailID.Value;
                }

                receiveDetail.TransactionGroupID = tgroup.TransactionGroupID;
                receiveDetail.TransactionGroup = tgroup;
                receive.ReceiveDetails.Add(receiveDetail);


                #region physical stock movement
                //transaction for goods on hand // previously it was GOODS_ON_HAND_UNCOMMITED
                var transaction = new Transaction();
                transaction.TransactionID = Guid.NewGuid();
                transaction.TransactionGroupID = transactionGroupId;
                transaction.TransactionDate = DateTime.Now;
                transaction.ParentCommodityID = _unitOfWork.CommodityRepository.FindById(c.CommodityID).ParentID ?? c.CommodityID;
                transaction.CommodityID = c.CommodityID;
                transaction.LedgerID = Cats.Models.Ledger.Constants.GOODS_ON_HAND;
                transaction.HubOwnerID = user.DefaultHubObj.HubOwnerID;

                transaction.DonorID = donorId;

                transaction.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, receive.HubID);
                transaction.ShippingInstructionID = _shippingInstructionService.GetSINumberIdWithCreate(receiveModels.SINumber).ShippingInstructionID;

                transaction.ProjectCodeID = _projectCodeService.GetProjectCodeIdWIthCreate(receiveModels.ProjectNumber).ProjectCodeID;
                transaction.HubID = user.DefaultHubObj.HubID;
                transaction.UnitID = c.UnitID;
                if (c.ReceivedQuantityInMT != null) transaction.QuantityInMT = c.ReceivedQuantityInMT.Value;
                if (c.ReceivedQuantityInUnit != null) transaction.QuantityInUnit = c.ReceivedQuantityInUnit.Value;
                if (c.CommodityGradeID != null) transaction.CommodityGradeID = c.CommodityGradeID.Value;

                transaction.ProgramID = receiveModels.ProgramID;
                transaction.StoreID = receiveModels.StoreID;
                transaction.Stack = receiveModels.StackNumber;
                transaction.TransactionGroupID = tgroup.TransactionGroupID;
                tgroup.Transactions.Add(transaction);



                var transaction2 = new Transaction();
                transaction2.TransactionID = Guid.NewGuid();
                transaction2.TransactionGroupID = transactionGroupId;
                transaction2.TransactionDate = DateTime.Now;

                transaction2.ParentCommodityID = transaction.ParentCommodityID;
                transaction2.CommodityID = c.CommodityID;
                transaction2.HubOwnerID = user.DefaultHubObj.HubOwnerID;

                transaction2.LedgerID = Cats.Models.Ledger.Constants.GOODS_UNDER_CARE;
                if (receive.ResponsibleDonorID != null)
                    transaction2.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.DONOR, receive.ResponsibleDonorID.Value);

                //Decide from where the -ve side of the transaction comes from
                //it is either from the allocated stock
                // or it is from goods under care.

                // this means that this receipt is done without having gone through the gift certificate process.

                if (receiveModels.CommoditySourceID == CommoditySource.Constants.DONATION || receiveModels.CommoditySourceID == CommoditySource.Constants.LOCALPURCHASE)
                {
                    transaction2.LedgerID = Cats.Models.Ledger.Constants.GOODS_UNDER_CARE;
                    transaction2.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.DONOR, receive.ResponsibleDonorID.Value);
                }
                else if (receiveModels.CommoditySourceID == CommoditySource.Constants.REPAYMENT)
                {
                    transaction2.LedgerID = Cats.Models.Ledger.Constants.GOODS_RECIEVABLE;
                    transaction2.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, receiveModels.SourceHubID.Value);
                }
                else
                {
                    transaction2.LedgerID = Cats.Models.Ledger.Constants.LIABILITIES;
                    transaction2.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, receiveModels.SourceHubID.Value);
                }

                transaction2.DonorID = donorId;

                transaction2.ShippingInstructionID = _shippingInstructionService.GetSINumberIdWithCreate(receiveModels.SINumber).ShippingInstructionID;
                transaction2.ProjectCodeID = _projectCodeService.GetProjectCodeIdWIthCreate(receiveModels.ProjectNumber).ProjectCodeID;
                transaction2.HubID = user.DefaultHubObj.HubID;
                transaction2.UnitID = c.UnitID;
                // this is the credit part, so make it Negative
                if (c.ReceivedQuantityInMT != null) transaction2.QuantityInMT = -c.ReceivedQuantityInMT.Value;
                if (c.ReceivedQuantityInUnit != null) transaction2.QuantityInUnit = -c.ReceivedQuantityInUnit.Value;
                if (c.CommodityGradeID != null) transaction2.CommodityGradeID = c.CommodityGradeID.Value;

                transaction2.ProgramID = receiveModels.ProgramID;
                transaction2.StoreID = receiveModels.StoreID;
                transaction2.Stack = receiveModels.StackNumber;
                transaction2.TransactionGroupID = tgroup.TransactionGroupID;
                tgroup.Transactions.Add(transaction2);
                #endregion

                #region plan side of the transaction
                //transaction for statistics
                transaction = new Transaction();
                transaction.TransactionID = Guid.NewGuid();
                transaction.TransactionGroupID = transactionGroupId;
                transaction.TransactionDate = DateTime.Now;
                transaction.ParentCommodityID = _unitOfWork.CommodityRepository.FindById(c.CommodityID).ParentID ?? c.CommodityID;
                transaction.CommodityID = c.CommodityID;
                transaction.DonorID = donorId;
                transaction.LedgerID = Cats.Models.Ledger.Constants.STATISTICS_FREE_STOCK;
                transaction.HubOwnerID = user.DefaultHubObj.HubOwnerID;

                transaction.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, receive.HubID);
                transaction.ShippingInstructionID = _shippingInstructionService.GetSINumberIdWithCreate(receiveModels.SINumber).ShippingInstructionID;

                transaction.ProjectCodeID = _projectCodeService.GetProjectCodeIdWIthCreate(receiveModels.ProjectNumber).ProjectCodeID;
                transaction.HubID = user.DefaultHubObj.HubID;
                transaction.UnitID = c.UnitID;
                if (c.ReceivedQuantityInMT != null) transaction.QuantityInMT = c.ReceivedQuantityInMT.Value;
                if (c.ReceivedQuantityInUnit != null) transaction.QuantityInUnit = c.ReceivedQuantityInUnit.Value;
                if (c.CommodityGradeID != null) transaction.CommodityGradeID = c.CommodityGradeID.Value;

                transaction.ProgramID = receiveModels.ProgramID;
                transaction.StoreID = receiveModels.StoreID;
                transaction.Stack = receiveModels.StackNumber;
                transaction.TransactionGroupID = tgroup.TransactionGroupID;
                tgroup.Transactions.Add(transaction);


                // transaction for Receivable 
                transaction2 = new Transaction();
                transaction2.TransactionID = Guid.NewGuid();
                transaction2.TransactionGroupID = transactionGroupId;
                transaction2.TransactionDate = DateTime.Now;
                //TAKEs the PARENT FROM THE FIRST TRANSACTION
                transaction2.ParentCommodityID = transaction.ParentCommodityID;
                transaction2.CommodityID = c.CommodityID;
                transaction2.DonorID = donorId;
                transaction2.HubOwnerID = user.DefaultHubObj.HubOwnerID;

                transaction2.LedgerID = Cats.Models.Ledger.Constants.GOODS_RECIEVABLE;
                if (receive.ResponsibleDonorID != null)
                    transaction2.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.DONOR, receive.ResponsibleDonorID.Value);

                //Decide from where the -ve side of the transaction comes from
                //it is either from the allocated stock
                // or it is from goods under care.

                // this means that this receipt is done without having gone through the gift certificate process.

                #region "commented out"
                if (receiveModels.CommoditySourceID == CommoditySource.Constants.DONATION || receiveModels.CommoditySourceID == CommoditySource.Constants.LOCALPURCHASE)
                {
                    transaction2.LedgerID = Cats.Models.Ledger.Constants.GOODS_UNDER_CARE;
                    transaction2.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.DONOR, receive.ResponsibleDonorID.Value);
                }
                else if (receiveModels.CommoditySourceID == CommoditySource.Constants.REPAYMENT)
                {
                    transaction2.LedgerID = Cats.Models.Ledger.Constants.GOODS_RECIEVABLE;
                    transaction2.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, receiveModels.SourceHubID.Value);
                }
                else
                {
                    transaction2.LedgerID = Cats.Models.Ledger.Constants.LIABILITIES;
                    transaction2.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, receiveModels.SourceHubID.Value);
                }
                #endregion



                transaction2.ShippingInstructionID = _shippingInstructionService.GetSINumberIdWithCreate(receiveModels.SINumber).ShippingInstructionID;
                transaction2.ProjectCodeID = _projectCodeService.GetProjectCodeIdWIthCreate(receiveModels.ProjectNumber).ProjectCodeID;
                transaction2.HubID = user.DefaultHubObj.HubID;
                transaction2.UnitID = c.UnitID;
                // this is the credit part, so make it Negative
                if (c.ReceivedQuantityInMT != null) transaction2.QuantityInMT = -c.ReceivedQuantityInMT.Value;
                if (c.ReceivedQuantityInUnit != null) transaction2.QuantityInUnit = -c.ReceivedQuantityInUnit.Value;
                if (c.CommodityGradeID != null) transaction2.CommodityGradeID = c.CommodityGradeID.Value;

                transaction2.ProgramID = receiveModels.ProgramID;
                transaction2.StoreID = receiveModels.StoreID;
                transaction2.Stack = receiveModels.StackNumber;
                transaction2.TransactionGroupID = tgroup.TransactionGroupID;
                // hack to get past same key object in context error
                //repository.Transaction = new TransactionRepository();
                tgroup.Transactions.Add(transaction2);

                #endregion
            }


            try
            {
                _unitOfWork.ReceiveRepository.Add(receive);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception exp)
            {
                //TODO: Save the exception somewhere
                throw new Exception("The Receipt Transaction Cannot be saved. <br />Detail Message :" + exp.StackTrace);

            }

        }

        public bool ReceiptTransaction(ReceiveNewViewModel viewModel)
        {
            //Todo: Construct Receive from the viewModel .... refactor 

            #region BindReceiveFromViewModel

            var receive = new Receive
            {
                
                ReceiveID = Guid.NewGuid(),

                GRN = viewModel.Grn,
                CommodityTypeID = viewModel.CommodityTypeId,

                SourceDonorID = viewModel.SourceDonorId,
                ResponsibleDonorID = viewModel.ResponsibleDonorId,

                TransporterID = viewModel.TransporterId > 0 ? viewModel.TransporterId : 1,
                PlateNo_Prime = viewModel.PlateNoPrime,
                PlateNo_Trailer = viewModel.PlateNoTrailer,
                DriverName = viewModel.DriverName,
                WeightBridgeTicketNumber = viewModel.WeightBridgeTicketNumber,
                WeightBeforeUnloading = viewModel.WeightBeforeUnloading,
                WeightAfterUnloading = viewModel.WeightAfterUnloading,

                VesselName = viewModel.VesselName,
                PortName = viewModel.PortName,

                ReceiptDate = viewModel.ReceiptDate,
                CreatedDate = DateTime.Now,
                WayBillNo = viewModel.WayBillNo,
                CommoditySourceID = viewModel.CommoditySourceTypeId,
                ReceivedByStoreMan = viewModel.ReceivedByStoreMan,

                PurchaseOrder = viewModel.PurchaseOrder,
                SupplierName = viewModel.SupplierName,

                Remark = viewModel.Remark,

                ReceiptAllocationID = viewModel.ReceiptAllocationId,
                HubID = viewModel.CurrentHub,
                UserProfileID =  viewModel.UserProfileId

            };

            #endregion

            //Todo: Construct ReceiveDetail from the viewModel Transaction 

            var transactionGroup = new TransactionGroup { TransactionGroupID = Guid.NewGuid() };

            #region transaction for receiveDetail

            //foreach (var receiveDetailNewViewModel in viewModel.ReceiveDetailNewViewModels)
            //{
            //    ReceiveSingleTransaction(viewModel, receiveDetailNewViewModel, receive, transactionGroup);
            //}

            //Tem implantation for one Receive 

            //check for non food 

            #region

            if (viewModel.CommodityTypeId == 2)
            {
                viewModel.ReceiveDetailNewViewModel.ReceivedQuantityInMt = 0;
                viewModel.ReceiveDetailNewViewModel.SentQuantityInMt = 0;
            }

            #endregion

            //Construct receive detail from viewModel 

            #region

            var receiveDetail = new ReceiveDetail
            {
                ReceiveDetailID = Guid.NewGuid(), //Todo: if there is existing id dont give new one  

                CommodityID = viewModel.ReceiveDetailNewViewModel.CommodityId,
                Description = viewModel.ReceiveDetailNewViewModel.Description,
                SentQuantityInMT = viewModel.ReceiveDetailNewViewModel.SentQuantityInMt,
                SentQuantityInUnit = viewModel.ReceiveDetailNewViewModel.SentQuantityInUnit,
                UnitID = viewModel.ReceiveDetailNewViewModel.UnitId,
                ReceiveID = receive.ReceiveID,
                TransactionGroupID = transactionGroup.TransactionGroupID,
                TransactionGroup = transactionGroup
            };


            #endregion

            //add to receive 

            receive.ReceiveDetails.Add(receiveDetail);

            var parentCommodityId =
                _unitOfWork.CommodityRepository.FindById(viewModel.ReceiveDetailNewViewModel.CommodityId).ParentID ??
                viewModel.ReceiveDetailNewViewModel.CommodityId;

            //physical stock movement 

            #region

            //transaction for goods on hand 

            #region On Positive Side

            var transactionOne = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                TransactionGroupID = transactionGroup.TransactionGroupID,
                TransactionDate = DateTime.Now,
                ParentCommodityID =
                    _unitOfWork.CommodityRepository.FindById(viewModel.ReceiveDetailNewViewModel.CommodityId).ParentID ??
                    viewModel.ReceiveDetailNewViewModel.CommodityId,
                CommodityID = parentCommodityId,
                LedgerID = Ledger.Constants.GOODS_ON_HAND,
                //HubOwnerID = 
                DonorID = receive.SourceDonorID,
                AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, receive.HubID),
                ShippingInstructionID =
                    _shippingInstructionService.GetSINumberIdWithCreate(viewModel.SiNumber).ShippingInstructionID,
                ProjectCodeID = _projectCodeService.GetProjectCodeIdWIthCreate(viewModel.ProjectCode).ProjectCodeID,
                HubID = viewModel.CurrentHub,
                UnitID = viewModel.ReceiveDetailNewViewModel.UnitId,
                QuantityInMT = viewModel.ReceiveDetailNewViewModel.ReceivedQuantityInMt,
                QuantityInUnit = viewModel.ReceiveDetailNewViewModel.ReceivedQuantityInUnit,

                //CommodityGradeID = 
                ProgramID = viewModel.ProgramId,
                StoreID = viewModel.StoreId,
                Stack = viewModel.StackNumber,
            };
            transactionGroup.Transactions.Add(transactionOne);

            #endregion

            // transaction for goods under care, receivable, liabilities 

            #region Negative Side

            var transactionTwo = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                TransactionGroupID = transactionGroup.TransactionGroupID,
                TransactionDate = DateTime.Now,
                ParentCommodityID =
                    _unitOfWork.CommodityRepository.FindById(viewModel.ReceiveDetailNewViewModel.CommodityId).ParentID ??
                    viewModel.ReceiveDetailNewViewModel.CommodityId,
                CommodityID = parentCommodityId,
                LedgerID = Ledger.Constants.GOODS_UNDER_CARE,

                //HubOwnerID = 
                DonorID = receive.SourceDonorID, //

                ShippingInstructionID =
                    _shippingInstructionService.GetSINumberIdWithCreate(viewModel.SiNumber).ShippingInstructionID,
                ProjectCodeID = _projectCodeService.GetProjectCodeIdWIthCreate(viewModel.ProjectCode).ProjectCodeID,
                HubID = viewModel.CurrentHub,
                UnitID = viewModel.ReceiveDetailNewViewModel.UnitId,
                QuantityInMT = -viewModel.ReceiveDetailNewViewModel.ReceivedQuantityInMt,
                QuantityInUnit = -viewModel.ReceiveDetailNewViewModel.ReceivedQuantityInUnit,

                //CommodityGradeID = 
                ProgramID = viewModel.ProgramId,
                StoreID = viewModel.StoreId,
                Stack = viewModel.StackNumber,
            };

            switch (viewModel.CommoditySourceTypeId)
            {
                case CommoditySource.Constants.LOCALPURCHASE:
                case CommoditySource.Constants.DONATION:
                    transactionTwo.LedgerID = Ledger.Constants.GOODS_UNDER_CARE;
                    transactionTwo.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.DONOR,
                        receive.ResponsibleDonorID.GetValueOrDefault(0));
                    break;
                case CommoditySource.Constants.REPAYMENT:
                    transactionTwo.LedgerID = Ledger.Constants.GOODS_RECIEVABLE;
                    transactionTwo.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB,
                        viewModel.SourceHubId.GetValueOrDefault(0));
                    break;
                default:
                    transactionTwo.LedgerID = Ledger.Constants.LIABILITIES;
                    transactionTwo.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB,
                        viewModel.SourceHubId.GetValueOrDefault(0));
                    break;
            }

            transactionGroup.Transactions.Add(transactionTwo);

            #endregion

            #endregion

            // plan side

            #region

            #region Positive Side

            //statstics free

            var transactionThree = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                TransactionGroupID = transactionGroup.TransactionGroupID,
                TransactionDate = DateTime.Now,
                ParentCommodityID =
                    _unitOfWork.CommodityRepository.FindById(viewModel.ReceiveDetailNewViewModel.CommodityId).ParentID ??
                    viewModel.ReceiveDetailNewViewModel.CommodityId,
                CommodityID = parentCommodityId,
                LedgerID = Ledger.Constants.STATISTICS_FREE_STOCK,
                //HubOwnerID = 
                DonorID = receive.SourceDonorID,
                AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, receive.HubID),
                ShippingInstructionID =
                    _shippingInstructionService.GetSINumberIdWithCreate(viewModel.SiNumber).ShippingInstructionID,
                ProjectCodeID = _projectCodeService.GetProjectCodeIdWIthCreate(viewModel.ProjectCode).ProjectCodeID,
                HubID = viewModel.CurrentHub,
                UnitID = viewModel.ReceiveDetailNewViewModel.UnitId,
                QuantityInMT = viewModel.ReceiveDetailNewViewModel.ReceivedQuantityInMt,
                QuantityInUnit = viewModel.ReceiveDetailNewViewModel.ReceivedQuantityInUnit,

                //CommodityGradeID = 
                ProgramID = viewModel.ProgramId,
                StoreID = viewModel.StoreId,
                Stack = viewModel.StackNumber,
            };

            transactionGroup.Transactions.Add(transactionThree);

            #endregion

            #region Negative Side

            var transactionFour = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                TransactionGroupID = transactionGroup.TransactionGroupID,
                TransactionDate = DateTime.Now,
                ParentCommodityID =
                    _unitOfWork.CommodityRepository.FindById(viewModel.ReceiveDetailNewViewModel.CommodityId).ParentID ??
                    viewModel.ReceiveDetailNewViewModel.CommodityId,
                CommodityID = parentCommodityId,
                //HubOwnerID = 
                DonorID = receive.SourceDonorID,
                ShippingInstructionID =
                    _shippingInstructionService.GetSINumberIdWithCreate(viewModel.SiNumber).ShippingInstructionID,
                ProjectCodeID = _projectCodeService.GetProjectCodeIdWIthCreate(viewModel.ProjectCode).ProjectCodeID,
                HubID = viewModel.CurrentHub,
                UnitID = viewModel.ReceiveDetailNewViewModel.UnitId,
                QuantityInMT = -viewModel.ReceiveDetailNewViewModel.ReceivedQuantityInMt,
                QuantityInUnit = -viewModel.ReceiveDetailNewViewModel.ReceivedQuantityInUnit,

                //CommodityGradeID = 
                ProgramID = viewModel.ProgramId,
                StoreID = viewModel.StoreId,
                Stack = viewModel.StackNumber,
            };

            if (transactionFour.CommoditySourceID == CommoditySource.Constants.DONATION ||
                viewModel.CommoditySourceTypeId == CommoditySource.Constants.LOCALPURCHASE)
            {
                transactionFour.LedgerID = Ledger.Constants.GOODS_UNDER_CARE;
                transactionFour.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.DONOR,
                    receive.ResponsibleDonorID.GetValueOrDefault(0));
            }
            else if (transactionFour.CommoditySourceID == CommoditySource.Constants.REPAYMENT)
            {
                transactionFour.LedgerID = Ledger.Constants.GOODS_RECIEVABLE;
                transactionFour.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB,
                    viewModel.SourceHubId.GetValueOrDefault(0));
            }
            else
            {
                transactionFour.LedgerID = Ledger.Constants.LIABILITIES;
                transactionFour.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB,
                    viewModel.SourceHubId.GetValueOrDefault(0));
            }

            transactionGroup.Transactions.Add(transactionFour);

            #endregion

            #endregion

            #endregion

            //Todo: Save Receive 

            try
            {
                _unitOfWork.ReceiveRepository.Add(receive);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Gets the transportation reports.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        public List<TransporationReport> GetTransportationReports(OperationMode mode, DateTime? fromDate, DateTime? toDate)
        {
            int ledgerId = (mode == OperationMode.Dispatch) ? Cats.Models.Ledger.Constants.GOODS_IN_TRANSIT : Cats.Models.Ledger.Constants.GOODS_ON_HAND;
            var list = _unitOfWork.TransactionRepository.Get(item =>
                        (item.LedgerID == ledgerId && (item.QuantityInMT > 0 || item.QuantityInUnit > 0))
                              &&
                              (item.TransactionGroup.DispatchDetails.Any() || item.TransactionGroup.ReceiveDetails.Any())
                              &&
                              (!item.TransactionGroup.InternalMovements.Any() || !item.TransactionGroup.Adjustments.Any())
                       );

            if (fromDate.HasValue)
            {
                list = list.Where(p => p.TransactionDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                list = list.Where(p => p.TransactionDate <= toDate.Value);
            }

            return (from t in list
                    group t by new { t.Commodity, t.Program } into tgroup
                    select new TransporationReport()
                    {
                        Commodity = tgroup.Key.Commodity.Name,
                        Program = tgroup.Key.Program.Name,
                        NoOfTrucks = tgroup.Count(),
                        QuantityInMT = tgroup.Sum(p => p.QuantityInMT),
                        QuantityInUnit = tgroup.Sum(p => p.QuantityInUnit)
                    }).ToList();

        }

        /// <summary>
        /// Gets the grouped transportation reports.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        public List<GroupedTransportation> GetGroupedTransportationReports(OperationMode mode, DateTime? fromDate, DateTime? toDate)
        {
            var list = (from tr in GetTransportationReports(mode, fromDate, toDate)
                        group tr by tr.Program into trg
                        select new GroupedTransportation()
                        {
                            Program = trg.Key,
                            Transportations = trg.ToList()
                        });
            return list.ToList(); ;
        }


        /// <summary>
        /// Gets the available allocations.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        public List<ReceiptAllocation> GetAvailableAllocations(int hubId)
        {
            var avaliableRAll =
                _unitOfWork.ReceiptAllocationRepository.FindBy(
                    t => t.QuantityInMT >= (_unitOfWork.TransactionRepository.FindBy(v =>
                                                                                      v.ShippingInstruction.Value ==
                                                                                      t.SINumber
                                                                                      && v.HubID == hubId
                                                                                      &&
                                                                                      v.LedgerID ==
                                                                                      Cats.Models.Ledger.Constants.
                                                                                          GOODS_ON_HAND_UNCOMMITED
                                                                                      && v.QuantityInMT > 0).Select(
                                                                                          v => v.QuantityInMT).Sum()));
            //var avaliableRAll = (from rAll in db.ReceiptAllocations
            //                     where rAll.QuantityInMT >= (from v in db.Transactions
            //                                                 where v.ShippingInstruction.Value == rAll.SINumber
            //                                                       && v.HubID == hubId
            //                                                       && v.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED
            //                                                       && v.QuantityInMT > 0
            //                                                 select v.QuantityInMT).Sum()
            //                     select rAll);

            return avaliableRAll.ToList();
        }


        /// <summary>
        /// Saves the dispatch transaction.
        /// </summary>
        /// <param name="dispatchModel">The dispatch model.</param>
        /// <param name="user">The user.</param>
        public bool SaveDispatchTransaction(DispatchModel dispatchModel, UserProfile user) //used to return void
        {
            Dispatch dispatch = dispatchModel.GenerateDipatch(user);
            dispatch.DispatchID = Guid.NewGuid();
            dispatch.HubID = user.DefaultHub.Value;
            dispatch.UserProfileID = user.UserProfileID;
            dispatch.DispatchAllocationID = dispatchModel.DispatchAllocationID;
            dispatch.OtherDispatchAllocationID = dispatchModel.OtherDispatchAllocationID;
            CommodityType commType = _unitOfWork.CommodityTypeRepository.FindById(dispatchModel.CommodityTypeID);

            foreach (DispatchDetailModel detail in dispatchModel.DispatchDetails)
            {

                if (commType.CommodityTypeID == 2)//if it's a non food
                {
                    detail.DispatchedQuantityMT = 0;
                    detail.RequestedQuantityMT = 0;
                }

                TransactionGroup group = new TransactionGroup();
                group.TransactionGroupID = Guid.NewGuid();
                if (dispatchModel.Type == 1)
                {
                    Transaction transaction = GetGoodsOnHandFDPTransaction(dispatchModel, dispatch, detail);
                    group.Transactions.Add(transaction);

                    Transaction transaction2 = GetGoodsInTransitFDPTransaction(dispatchModel, dispatch, detail);
                    group.Transactions.Add(transaction2);

                    Transaction transaction3 = GetStatisticsFDPTransaction(dispatchModel, dispatch, detail);
                    group.Transactions.Add(transaction3);

                    Transaction transaction4 = GetCommitedToFDPTransaction(dispatchModel, dispatch, detail);
                    group.Transactions.Add(transaction4);
                }
                else
                {
                    Transaction transaction = GetGoodsOnHandHUBTransaction(dispatchModel, dispatch, detail);
                    group.Transactions.Add(transaction);

                    Transaction transaction2 = GetGoodInTransitHUBTransaction(dispatchModel, dispatch, detail);
                    group.Transactions.Add(transaction2);

                    Transaction transaction3 = GetStatisticsHUBTransaction(dispatchModel, dispatch, detail);
                    group.Transactions.Add(transaction3);

                    Transaction transaction4 = GetCommittedToFDPHUBTransaction(dispatchModel, dispatch, detail);
                    group.Transactions.Add(transaction4);
                }

                DispatchDetail dispatchDetail = GenerateDispatchDetail(detail);
                dispatchDetail.DispatchDetailID = Guid.NewGuid();
                dispatchDetail.TransactionGroup = group;


                dispatch.DispatchDetails.Add(dispatchDetail);

            }
            // Try to save this transaction
            //    db.Database.Connection.Open();
            //  DbTransaction dbTransaction = db.Database.Connection.BeginTransaction();
            try
            {
                _unitOfWork.DispatchRepository.Add(dispatch);
                _unitOfWork.Save();
                return true;
                //repository.Dispatch.Add(dispatch);
                //dbTransaction.Commit();
            }
            catch (Exception exp)
            {
                // dbTransaction.Rollback();
                //TODO: Save the detail of this exception somewhere
                throw new Exception("The Dispatch Transaction Cannot be saved. <br />Detail Message :" + exp.Message);
            }

            if (dispatch.Type == 1)
            {
                string sms = dispatch.GetSMSText();
                //SMS.SendSMS(dispatch.FDPID.Value, sms);
            }
            return false;
        }

        public void SaveDispatchTransaction(DispatchViewModel dispatchViewModel)
        {

            var dispatch = new Dispatch
            {
                BidNumber = dispatchViewModel.BidNumber,
                CreatedDate = dispatchViewModel.CreatedDate,
                DispatchAllocationID = dispatchViewModel.DispatchAllocationID,
                DispatchDate = dispatchViewModel.DispatchDate,
                DispatchID = Guid.NewGuid(),
                DispatchedByStoreMan = dispatchViewModel.DispatchedByStoreMan,
                DriverName = dispatchViewModel.DriverName,
                FDPID = dispatchViewModel.FDPID,
                GIN = dispatchViewModel.GIN,
                HubID = dispatchViewModel.HubID,
                PeriodMonth = dispatchViewModel.Month,
                PeriodYear = dispatchViewModel.Year,
                PlateNo_Prime = dispatchViewModel.PlateNo_Prime,
                PlateNo_Trailer = dispatchViewModel.PlateNo_Trailer,
                Remark = dispatchViewModel.Remark,
                RequisitionNo = dispatchViewModel.RequisitionNo,
                Round = dispatchViewModel.Round,
                TransporterID = dispatchViewModel.TransporterID,
                UserProfileID = dispatchViewModel.UserProfileID,
                WeighBridgeTicketNumber = dispatchViewModel.WeighBridgeTicketNumber
            };

            //dispatch.Type = dispatchViewModel.Type;

            var group = new TransactionGroup();
            group.TransactionGroupID = Guid.NewGuid();

            var dispatchDetail = new DispatchDetail
            {
                DispatchID = dispatch.DispatchID,
                CommodityID = dispatchViewModel.CommodityID,
                Description = dispatchViewModel.Commodity,
                DispatchDetailID = Guid.NewGuid(),
                RequestedQuantityInMT = dispatchViewModel.Quantity,
                RequestedQunatityInUnit = dispatchViewModel.QuantityInUnit,
                QuantityPerUnit = dispatchViewModel.QuantityPerUnit,
                UnitID = dispatchViewModel.UnitID,
                TransactionGroupID = @group.TransactionGroupID
            };


            // Physical movement of stock
            var transactionInTransit = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.FDP, dispatchViewModel.FDPID),
                ProgramID = dispatchViewModel.ProgramID,
                ParentCommodityID = dispatchViewModel.CommodityID,
                CommodityID = dispatchViewModel.CommodityID,
                FDPID = dispatchViewModel.FDPID,
                HubID = dispatchViewModel.HubID,
                HubOwnerID = _unitOfWork.HubRepository.FindById(dispatchViewModel.HubID).HubOwnerID,
                LedgerID = Models.Ledger.Constants.GOODS_IN_TRANSIT,
                QuantityInMT = +dispatchViewModel.Quantity,
                QuantityInUnit = +dispatchViewModel.QuantityInUnit,
                ShippingInstructionID = dispatchViewModel.ShippingInstructionID,
                ProjectCodeID = dispatchViewModel.ProjectCodeID,
                Round = dispatchViewModel.Round,
                PlanId = dispatchViewModel.PlanId,
                TransactionDate = DateTime.Now,
                UnitID = dispatchViewModel.UnitID,
                TransactionGroupID = @group.TransactionGroupID
            };
            //transaction2.Stack = dispatchModel.StackNumber;
            //transaction2.StoreID = dispatchModel.StoreID;
            //group.Transactions.Add(transaction2);



            var transactionGoh = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.FDP, dispatchViewModel.FDPID),
                ProgramID = dispatchViewModel.ProgramID,
                ParentCommodityID = dispatchViewModel.CommodityID,
                CommodityID = dispatchViewModel.CommodityID,
                FDPID = dispatchViewModel.FDPID,
                HubID = dispatchViewModel.HubID,
                HubOwnerID = _unitOfWork.HubRepository.FindById(dispatchViewModel.HubID).HubOwnerID,
                LedgerID = Models.Ledger.Constants.GOODS_ON_HAND,
                QuantityInMT = -dispatchViewModel.Quantity,
                QuantityInUnit = -dispatchViewModel.QuantityInUnit,
                ShippingInstructionID = dispatchViewModel.ShippingInstructionID,
                ProjectCodeID = dispatchViewModel.ProjectCodeID,
                Round = dispatchViewModel.Round,
                PlanId = dispatchViewModel.PlanId,
                TransactionDate = DateTime.Now,
                UnitID = dispatchViewModel.UnitID,
                TransactionGroupID = @group.TransactionGroupID
            };
            //transaction.Stack = dispatch.StackNumber;
            //transaction.StoreID = dispatch.StoreID;


            // plan side of the transaction (Red Border)

            var transactionComitedToFdp = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.FDP, dispatchViewModel.FDPID),
                ProgramID = dispatchViewModel.ProgramID,
                ParentCommodityID = dispatchViewModel.CommodityID,
                CommodityID = dispatchViewModel.CommodityID,
                FDPID = dispatchViewModel.FDPID,
                HubID = dispatchViewModel.HubID,
                HubOwnerID = _unitOfWork.HubRepository.FindById(dispatchViewModel.HubID).HubOwnerID,
                LedgerID = Models.Ledger.Constants.COMMITED_TO_FDP,
                QuantityInMT = +dispatchViewModel.Quantity,
                QuantityInUnit = +dispatchViewModel.QuantityInUnit,
                ShippingInstructionID = dispatchViewModel.ShippingInstructionID,
                ProjectCodeID = dispatchViewModel.ProjectCodeID,
                Round = dispatchViewModel.Round,
                PlanId = dispatchViewModel.PlanId,
                TransactionDate = DateTime.Now,
                UnitID = dispatchViewModel.UnitID,
                TransactionGroupID = @group.TransactionGroupID
            };
            //transaction2.Stack = dispatchModel.StackNumber;
            //transaction2.StoreID = dispatchModel.StoreID;
            //group.Transactions.Add(transaction2);





            var transactionInTansitFreeStock = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.FDP, dispatchViewModel.FDPID),
                ProgramID = dispatchViewModel.ProgramID,
                ParentCommodityID = dispatchViewModel.CommodityID,
                CommodityID = dispatchViewModel.CommodityID,
                FDPID = dispatchViewModel.FDPID,
                HubID = dispatchViewModel.HubID,
                HubOwnerID = _unitOfWork.HubRepository.FindById(dispatchViewModel.HubID).HubOwnerID,
                LedgerID = Cats.Models.Ledger.Constants.STATISTICS_FREE_STOCK,
                QuantityInMT = -dispatchViewModel.Quantity,
                QuantityInUnit = -dispatchViewModel.QuantityInUnit,
                ShippingInstructionID = dispatchViewModel.ShippingInstructionID,
                ProjectCodeID = dispatchViewModel.ProjectCodeID,
                Round = dispatchViewModel.Round,
                PlanId = dispatchViewModel.PlanId,
                TransactionDate = DateTime.Now,
                UnitID = dispatchViewModel.UnitID,
                TransactionGroupID = @group.TransactionGroupID
            };
            //transaction.Stack = dispatch.StackNumber;
            //transaction.StoreID = dispatch.StoreID;
            dispatch.DispatchDetails.Add(dispatchDetail);

            try
            {
                _unitOfWork.TransactionGroupRepository.Add(group);
                _unitOfWork.TransactionRepository.Add(transactionInTransit);
                _unitOfWork.TransactionRepository.Add(transactionGoh);
                _unitOfWork.TransactionRepository.Add(transactionInTansitFreeStock);
                _unitOfWork.TransactionRepository.Add(transactionComitedToFdp);
                _unitOfWork.DispatchRepository.Add(dispatch);
                _unitOfWork.Save();

            }

            catch (Exception exp)
            {
                // dbTransaction.Rollback();
                //TODO: Save the detail of this exception somewhere
                throw new Exception("The Dispatch Transaction Cannot be saved. <br />Detail Message :" + exp.Message);
            }

        }

        #region dispatch transaction helpers
        //TODO: this section has to be cleaned
        /// <summary>
        /// Gets the positive FDP transaction.
        /// </summary>
        /// <param name="dispatchModel">The dispatch model.</param>
        /// <param name="dispatch">The dispatch.</param>
        /// <param name="detail">The detail.</param>
        /// <returns></returns>
        private Transaction GetGoodsInTransitFDPTransaction(DispatchModel dispatchModel, Dispatch dispatch, DispatchDetailModel detail)
        {
            Transaction transaction2 = new Transaction();
            transaction2.TransactionID = Guid.NewGuid();
            transaction2.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.FDP, dispatchModel.FDPID.Value);
            transaction2.ProgramID = dispatchModel.ProgramID;
            transaction2.ParentCommodityID = detail.CommodityID;
            transaction2.CommodityID = detail.CommodityID;
            transaction2.HubID = dispatch.HubID;
            transaction2.HubOwnerID = _unitOfWork.HubRepository.FindById(dispatch.HubID).HubOwnerID;
            transaction2.LedgerID = Cats.Models.Ledger.Constants.GOODS_IN_TRANSIT;
            transaction2.QuantityInMT = +detail.DispatchedQuantityMT.Value;
            transaction2.QuantityInUnit = +detail.DispatchedQuantity.Value;
            transaction2.ShippingInstructionID = _shippingInstructionService.GetShipingInstructionId(dispatchModel.SINumber);
            transaction2.ProjectCodeID = _projectCodeService.GetProjectCodeId(dispatchModel.ProjectNumber);
            transaction2.Stack = dispatchModel.StackNumber;
            transaction2.StoreID = dispatchModel.StoreID;
            transaction2.TransactionDate = DateTime.Now;
            transaction2.UnitID = detail.Unit;
            return transaction2;
        }

        /// <summary>
        /// Gets the negative FDP transaction.
        /// </summary>
        /// <param name="dispatchModel">The dispatch model.</param>
        /// <param name="dispatch">The dispatch.</param>
        /// <param name="detail">The detail.</param>
        /// <returns></returns>
        private Transaction GetGoodsOnHandFDPTransaction(DispatchModel dispatchModel, Dispatch dispatch, DispatchDetailModel detail)
        {
            Transaction transaction = new Transaction();
            transaction.TransactionID = Guid.NewGuid();
            transaction.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.FDP, dispatch.FDPID.Value);
            transaction.ProgramID = dispatchModel.ProgramID;
            transaction.ParentCommodityID = detail.CommodityID;
            transaction.CommodityID = detail.CommodityID;
            transaction.HubID = dispatch.HubID;
            transaction.HubOwnerID = _unitOfWork.HubRepository.FindById(dispatch.HubID).HubOwnerID;
            transaction.LedgerID = Cats.Models.Ledger.Constants.GOODS_ON_HAND; //previously GOODS_ON_HAND_UNCOMMITED
            transaction.QuantityInMT = -detail.DispatchedQuantityMT.Value;
            transaction.QuantityInUnit = -detail.DispatchedQuantity.Value;
            transaction.ShippingInstructionID = _shippingInstructionService.GetShipingInstructionId(dispatchModel.SINumber);
            transaction.ProjectCodeID = _projectCodeService.GetProjectCodeId(dispatchModel.ProjectNumber);
            transaction.Stack = dispatchModel.StackNumber;
            transaction.StoreID = dispatchModel.StoreID;
            transaction.TransactionDate = DateTime.Now;
            transaction.UnitID = detail.Unit;
            return transaction;
        }

        private Transaction GetStatisticsFDPTransaction(DispatchModel dispatchModel, Dispatch dispatch, DispatchDetailModel detail)
        {
            Transaction transaction = new Transaction();
            transaction.TransactionID = Guid.NewGuid();
            transaction.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.FDP, dispatch.FDPID.Value);
            transaction.ProgramID = dispatchModel.ProgramID;
            transaction.ParentCommodityID = detail.CommodityID;
            transaction.CommodityID = detail.CommodityID;
            transaction.HubID = dispatch.HubID;
            transaction.HubOwnerID = _unitOfWork.HubRepository.FindById(dispatch.HubID).HubOwnerID;
            transaction.LedgerID = Cats.Models.Ledger.Constants.STATISTICS_FREE_STOCK;
            transaction.QuantityInMT = -detail.DispatchedQuantityMT.Value;
            transaction.QuantityInUnit = -detail.DispatchedQuantity.Value;
            transaction.ShippingInstructionID = _shippingInstructionService.GetShipingInstructionId(dispatchModel.SINumber);
            transaction.ProjectCodeID = _projectCodeService.GetProjectCodeId(dispatchModel.ProjectNumber);
            transaction.Stack = dispatchModel.StackNumber;
            transaction.StoreID = dispatchModel.StoreID;
            transaction.TransactionDate = DateTime.Now;
            transaction.UnitID = detail.Unit;
            return transaction;
        }

        private Transaction GetCommitedToFDPTransaction(DispatchModel dispatchModel, Dispatch dispatch, DispatchDetailModel detail)
        {
            Transaction transaction = new Transaction();
            transaction.TransactionID = Guid.NewGuid();
            transaction.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.FDP, dispatch.FDPID.Value);
            transaction.ProgramID = dispatchModel.ProgramID;
            transaction.ParentCommodityID = detail.CommodityID;
            transaction.CommodityID = detail.CommodityID;
            transaction.HubID = dispatch.HubID;
            transaction.HubOwnerID = _unitOfWork.HubRepository.FindById(dispatch.HubID).HubOwnerID;
            transaction.LedgerID = Cats.Models.Ledger.Constants.COMMITED_TO_FDP;
            transaction.QuantityInMT = +detail.DispatchedQuantityMT.Value;
            transaction.QuantityInUnit = +detail.DispatchedQuantity.Value;
            transaction.ShippingInstructionID = _shippingInstructionService.GetShipingInstructionId(dispatchModel.SINumber);
            transaction.ProjectCodeID = _projectCodeService.GetProjectCodeId(dispatchModel.ProjectNumber);
            transaction.Stack = dispatchModel.StackNumber;
            transaction.StoreID = dispatchModel.StoreID;
            transaction.TransactionDate = DateTime.Now;
            transaction.UnitID = detail.Unit;
            return transaction;
        }

        /// <summary>
        /// Gets the positive HUB transaction.
        /// </summary>
        /// <param name="dispatchModel">The dispatch model.</param>
        /// <param name="dispatch">The dispatch.</param>
        /// <param name="detail">The detail.</param>
        /// <returns></returns>
        private Transaction GetGoodInTransitHUBTransaction(DispatchModel dispatchModel, Dispatch dispatch, DispatchDetailModel detail)
        {
            Transaction transaction2 = new Transaction();
            transaction2.TransactionID = Guid.NewGuid();
            transaction2.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, dispatchModel.ToHubID.Value);
            transaction2.ProgramID = dispatchModel.ProgramID;
            transaction2.ParentCommodityID = detail.CommodityID;
            transaction2.CommodityID = detail.CommodityID;
            transaction2.HubID = dispatch.HubID;
            transaction2.HubOwnerID = _unitOfWork.HubRepository.FindById(dispatch.HubID).HubOwnerID;
            transaction2.LedgerID = Cats.Models.Ledger.Constants.GOODS_IN_TRANSIT;
            transaction2.QuantityInMT = +detail.DispatchedQuantityMT.Value;
            transaction2.QuantityInUnit = +detail.DispatchedQuantity.Value;
            transaction2.ShippingInstructionID = _shippingInstructionService.GetShipingInstructionId(dispatchModel.SINumber);

            transaction2.ProjectCodeID = _projectCodeService.GetProjectCodeId(dispatchModel.ProjectNumber);
            transaction2.Stack = dispatchModel.StackNumber;
            transaction2.StoreID = dispatchModel.StoreID;
            transaction2.TransactionDate = DateTime.Now;
            transaction2.UnitID = detail.Unit;
            return transaction2;
        }

        private Transaction GetGoodsOnHandHUBTransaction(DispatchModel dispatchModel, Dispatch dispatch, DispatchDetailModel detail)
        {
            Transaction transaction2 = new Transaction();
            transaction2.TransactionID = Guid.NewGuid();
            transaction2.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, dispatchModel.ToHubID.Value);
            transaction2.ProgramID = dispatchModel.ProgramID;
            transaction2.ParentCommodityID = detail.CommodityID;
            transaction2.CommodityID = detail.CommodityID;
            transaction2.HubID = dispatch.HubID;
            transaction2.HubOwnerID = _unitOfWork.HubRepository.FindById(dispatch.HubID).HubOwnerID;
            transaction2.LedgerID = Cats.Models.Ledger.Constants.GOODS_ON_HAND; //Previously GOODS_ON_HAND_UNCOMMITED
            transaction2.QuantityInMT = -detail.DispatchedQuantityMT.Value;
            transaction2.QuantityInUnit = -detail.DispatchedQuantity.Value;
            transaction2.ShippingInstructionID = _shippingInstructionService.GetShipingInstructionId(dispatchModel.SINumber);

            transaction2.ProjectCodeID = _projectCodeService.GetProjectCodeId(dispatchModel.ProjectNumber);
            transaction2.Stack = dispatchModel.StackNumber;
            transaction2.StoreID = dispatchModel.StoreID;
            transaction2.TransactionDate = DateTime.Now;
            transaction2.UnitID = detail.Unit;
            return transaction2;
        }

        /// <summary>
        /// Gets the negative HUB Transaction.
        /// </summary>
        /// <param name="dispatchModel">The dispatch model.</param>
        /// <param name="dispatch">The dispatch.</param>
        /// <param name="detail">The detail.</param>
        /// <returns></returns>
        private Transaction GetStatisticsHUBTransaction(DispatchModel dispatchModel, Dispatch dispatch, DispatchDetailModel detail)
        {
            Transaction transaction = new Transaction();
            transaction.TransactionID = Guid.NewGuid();
            transaction.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, dispatch.HubID);
            transaction.ProgramID = dispatchModel.ProgramID;
            transaction.ParentCommodityID = detail.CommodityID;
            transaction.CommodityID = detail.CommodityID;
            transaction.HubID = dispatch.HubID;
            transaction.HubOwnerID = _unitOfWork.HubRepository.FindById(dispatch.HubID).HubOwnerID;
            transaction.LedgerID = Cats.Models.Ledger.Constants.STATISTICS_FREE_STOCK;
            transaction.QuantityInMT = -detail.DispatchedQuantityMT.Value;
            transaction.QuantityInUnit = -detail.DispatchedQuantity.Value;
            transaction.ShippingInstructionID = _shippingInstructionService.GetShipingInstructionId(dispatchModel.SINumber);
            //transaction.ProjectCodeID = _projectCodeService.GetProjectCodeId(dispatchModel.ProjectNumber);
            transaction.Stack = dispatchModel.StackNumber;
            transaction.StoreID = dispatchModel.StoreID;
            transaction.TransactionDate = DateTime.Now;
            transaction.UnitID = detail.Unit;
            return transaction;
        }

        private Transaction GetCommittedToFDPHUBTransaction(DispatchModel dispatchModel, Dispatch dispatch, DispatchDetailModel detail)
        {
            Transaction transaction = new Transaction();
            transaction.TransactionID = Guid.NewGuid();
            transaction.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, dispatch.HubID);
            transaction.ProgramID = dispatchModel.ProgramID;
            transaction.ParentCommodityID = detail.CommodityID;
            transaction.CommodityID = detail.CommodityID;
            transaction.HubID = dispatch.HubID;
            transaction.HubOwnerID = _unitOfWork.HubRepository.FindById(dispatch.HubID).HubOwnerID;
            transaction.LedgerID = Cats.Models.Ledger.Constants.STATISTICS_FREE_STOCK;
            transaction.QuantityInMT = -detail.DispatchedQuantityMT.Value;
            transaction.QuantityInUnit = -detail.DispatchedQuantity.Value;
            transaction.ShippingInstructionID = _shippingInstructionService.GetShipingInstructionId(dispatchModel.SINumber);
            //transaction.ProjectCodeID = _projectCodeService.GetProjectCodeId(dispatchModel.ProjectNumber);
            transaction.Stack = dispatchModel.StackNumber;
            transaction.StoreID = dispatchModel.StoreID;
            transaction.TransactionDate = DateTime.Now;
            transaction.UnitID = detail.Unit;
            return transaction;
        }

        /// <summary>
        /// Generates the dispatch detail.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        private DispatchDetail GenerateDispatchDetail(DispatchDetailModel c)
        {
            if (c != null)
            {
                DispatchDetail dispatchDetail = new DispatchDetail()
                {
                    CommodityID = c.CommodityID,
                    Description = c.Description,
                    // DispatchDetailID = c.Id,
                    RequestedQuantityInMT = c.RequestedQuantityMT.Value,
                    //DispatchedQuantityInMT = c.DispatchedQuantityMT,
                    //DispatchedQuantityInUnit = c.DispatchedQuantity,
                    RequestedQunatityInUnit = c.RequestedQuantity.Value,
                    UnitID = c.Unit
                };
                if (c.Id.HasValue)
                {
                    dispatchDetail.DispatchDetailID = c.Id.Value;
                }

                return dispatchDetail;
            }
            else
            {
                return null;
            }
        }

        #endregion


        /// <summary>
        /// Finds the by transaction group ID.
        /// </summary>
        /// <param name="partition">The partition.</param>
        /// <param name="transactionGroupID">The transaction group ID.</param>
        /// <returns></returns>
        public Transaction FindByTransactionGroupID(Guid transactionGroupID)
        {
            return _unitOfWork.TransactionRepository.Get(tr => tr.TransactionGroupID == transactionGroupID).FirstOrDefault();
        }



        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="user"></param>
        /// <exception cref="System.Exception"></exception>
        public void SaveInternalMovementTrasnsaction(InternalMovementViewModel viewModel, UserProfile user)
        {
            InternalMovement internalMovement = new InternalMovement();
            TransactionGroup transactionGroup = new TransactionGroup();
            Transaction transactionFromStore = new Transaction();
            var transactionGroupId = Guid.NewGuid();



            Commodity commodity = _unitOfWork.CommodityRepository.FindById(viewModel.CommodityId);

            transactionFromStore.TransactionID = Guid.NewGuid();
            transactionFromStore.TransactionGroupID = transactionGroupId;
            transactionFromStore.LedgerID = 2;
            transactionFromStore.HubOwnerID = user.DefaultHubObj.HubOwner.HubOwnerID;
            //trasaction.AccountID
            transactionFromStore.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, user.DefaultHub.Value); // 
            transactionFromStore.HubID = user.DefaultHub.Value;
            transactionFromStore.StoreID = viewModel.FromStoreId;  //
            transactionFromStore.Stack = viewModel.FromStackId; //
            transactionFromStore.ProjectCodeID = viewModel.ProjectCodeId;
            transactionFromStore.ShippingInstructionID = viewModel.ShippingInstructionId;
            transactionFromStore.ProgramID = viewModel.ProgramId;
            transactionFromStore.ParentCommodityID = (commodity.ParentID == null)
                                                       ? commodity.CommodityID
                                                       : commodity.ParentID.Value;
            transactionFromStore.CommodityID = viewModel.CommodityId;
            transactionFromStore.CommodityGradeID = null; // How did I get this value ? 
            transactionFromStore.QuantityInMT = 0 - viewModel.QuantityInMt;
            transactionFromStore.QuantityInUnit = 0 - viewModel.QuantityInUnit;
            transactionFromStore.UnitID = viewModel.UnitId;
            transactionFromStore.TransactionDate = DateTime.Now;



            Transaction transactionToStore = new Transaction();

            transactionToStore.TransactionID = Guid.NewGuid();
            transactionToStore.TransactionGroupID = transactionGroupId;
            transactionToStore.LedgerID = 2;
            transactionToStore.HubOwnerID = user.DefaultHubObj.HubOwner.HubOwnerID;
            //transactionToStore.AccountID
            transactionToStore.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, user.DefaultHub.Value); // 
            transactionToStore.HubID = user.DefaultHub.Value;
            transactionToStore.StoreID = viewModel.ToStoreId;  //
            transactionToStore.Stack = viewModel.ToStackId; //
            transactionToStore.ProjectCodeID = viewModel.ProjectCodeId;
            transactionToStore.ShippingInstructionID = viewModel.ShippingInstructionId;
            transactionToStore.ProgramID = viewModel.ProgramId;

            transactionToStore.ParentCommodityID = (commodity.ParentID == null)
                                                       ? commodity.CommodityID
                                                       : commodity.ParentID.Value;
            transactionToStore.CommodityID = viewModel.CommodityId;
            transactionToStore.CommodityGradeID = null; // How did I get this value ? 
            transactionToStore.QuantityInMT = viewModel.QuantityInMt;
            transactionToStore.QuantityInUnit = viewModel.QuantityInUnit;
            transactionToStore.UnitID = viewModel.UnitId;
            transactionToStore.TransactionDate = DateTime.Now;

            transactionGroup.TransactionGroupID = transactionGroupId;
            transactionGroup.Transactions.Add(transactionFromStore);
            transactionGroup.Transactions.Add(transactionToStore);
            transactionGroup.PartitionId = 0;

            internalMovement.InternalMovementID = Guid.NewGuid();
            internalMovement.PartitionId = 0;
            internalMovement.TransactionGroupID = transactionGroupId;
            internalMovement.TransactionGroup = transactionGroup;
            internalMovement.TransferDate = viewModel.SelectedDate;
            internalMovement.DReason = viewModel.ReasonId;
            internalMovement.Notes = viewModel.Note;
            internalMovement.ApprovedBy = viewModel.ApprovedBy;
            internalMovement.ReferenceNumber = viewModel.ReferenceNumber;
            internalMovement.HubID = user.DefaultHub.Value;



            // Try to save this transaction

            try
            {
                _unitOfWork.InternalMovementRepository.Add(internalMovement);
                _unitOfWork.Save();
            }
            catch (Exception exp)
            {
                //dbTransaction.Rollback();
                //TODO: Save the detail of this exception somewhere
                throw new Exception("The Internal Movement Transaction Cannot be saved. <br />Detail Message :" + exp.Message);
            }

        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="user"></param>
        /// <exception cref="System.Exception"></exception>
        public void SaveLossTrasnsaction(LossesAndAdjustmentsViewModel viewModel, UserProfile user)
        {
            Commodity commodity = _unitOfWork.CommodityRepository.FindById(viewModel.CommodityId);

            int transactionsign = 1;
            if (viewModel.ReasonId == Cats.Models.Adjustment.Constants.LOST_AND_FOUND) transactionsign = -1;


            Adjustment lossAndAdjustment = new Adjustment();
            TransactionGroup transactionGroup = new TransactionGroup();
            Transaction transactionOne = new Transaction();

            var transactionGroupId = Guid.NewGuid();

            transactionOne.TransactionID = Guid.NewGuid();
            transactionOne.TransactionGroupID = transactionGroupId;
            transactionOne.LedgerID = Cats.Models.Ledger.Constants.GOODS_ON_HAND;
            transactionOne.HubOwnerID = user.DefaultHubObj.HubOwner.HubOwnerID;
            transactionOne.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, user.DefaultHub.Value); // 
            transactionOne.HubID = user.DefaultHub.Value;
            transactionOne.StoreID = viewModel.StoreId;  //
            transactionOne.ProjectCodeID = viewModel.ProjectCodeId;
            transactionOne.ShippingInstructionID = viewModel.ShippingInstructionId;

            transactionOne.ParentCommodityID = (commodity.ParentID == null)
                                                       ? commodity.CommodityID
                                                       : commodity.ParentID.Value;
            transactionOne.CommodityID = viewModel.CommodityId;
            transactionOne.ProgramID = viewModel.ProgramId;
            transactionOne.CommodityGradeID = null; // How did I get this value ? 
            transactionOne.QuantityInMT = -1*transactionsign*viewModel.QuantityInMt;
            transactionOne.QuantityInUnit = -1*transactionsign*viewModel.QuantityInUint;
            transactionOne.UnitID = viewModel.UnitId;
            transactionOne.TransactionDate = DateTime.Now;



            Transaction transactionTwo = new Transaction();

            transactionTwo.TransactionID = Guid.NewGuid();
            transactionTwo.TransactionGroupID = transactionGroupId;
            transactionTwo.LedgerID = Cats.Models.Ledger.Constants.LOSS_IN_TRANSIT;
            transactionTwo.HubOwnerID = user.DefaultHubObj.HubOwnerID;
            transactionTwo.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, user.DefaultHub.Value); // 
            transactionTwo.HubID = user.DefaultHub.Value;
            transactionTwo.StoreID = viewModel.StoreId;  //
            transactionTwo.ProjectCodeID = viewModel.ProjectCodeId;
            transactionTwo.ShippingInstructionID = viewModel.ShippingInstructionId;
            transactionTwo.ParentCommodityID = (commodity.ParentID == null)
                                                       ? commodity.CommodityID
                                                       : commodity.ParentID.Value;
            transactionTwo.CommodityID = viewModel.CommodityId;
            transactionTwo.ProgramID = viewModel.ProgramId;
            transactionTwo.CommodityGradeID = null; // How did I get this value ? 
            transactionTwo.QuantityInMT = transactionsign*viewModel.QuantityInMt;
            transactionTwo.QuantityInUnit = transactionsign*viewModel.QuantityInUint;
            transactionTwo.UnitID = viewModel.UnitId;
            transactionTwo.TransactionDate = DateTime.Now;

            transactionGroup.TransactionGroupID = transactionGroupId;
            transactionGroup.Transactions.Add(transactionOne);
            transactionGroup.Transactions.Add(transactionTwo);


            lossAndAdjustment.PartitionId = 0;
            lossAndAdjustment.AdjustmentID = Guid.NewGuid();
            lossAndAdjustment.TransactionGroupID = transactionGroupId;
            lossAndAdjustment.TransactionGroup = transactionGroup;
            lossAndAdjustment.HubID = user.DefaultHub.Value;
            lossAndAdjustment.AdjustmentReasonID = viewModel.ReasonId;
            lossAndAdjustment.AdjustmentDirection = "L";
            lossAndAdjustment.AdjustmentDate = viewModel.SelectedDate;
            lossAndAdjustment.ApprovedBy = viewModel.ApprovedBy;
            lossAndAdjustment.Remarks = viewModel.Description;
            lossAndAdjustment.UserProfileID = user.UserProfileID;
            lossAndAdjustment.ReferenceNumber = viewModel.MemoNumber;
            lossAndAdjustment.StoreManName = viewModel.StoreMan;



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

        private Transaction createLossAdjustmentTransaction(LossesAndAdjustmentsViewModel viewModel, UserProfile user)
        {
            Transaction lossAdjtransaction=new Transaction();
            Commodity commodity = _unitOfWork.CommodityRepository.FindById(viewModel.CommodityId);
            lossAdjtransaction.TransactionID = Guid.NewGuid();
            lossAdjtransaction.HubOwnerID = user.DefaultHubObj.HubOwner.HubOwnerID;
            lossAdjtransaction.AccountID = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, user.DefaultHub.Value); // 
            lossAdjtransaction.HubID = user.DefaultHub.Value;
            lossAdjtransaction.StoreID = viewModel.StoreId;  //
            lossAdjtransaction.ProjectCodeID = viewModel.ProjectCodeId;
            lossAdjtransaction.ShippingInstructionID = viewModel.ShippingInstructionId;
            lossAdjtransaction.ParentCommodityID = (commodity.ParentID == null)
                                                       ? commodity.CommodityID
                                                       : commodity.ParentID.Value;
            lossAdjtransaction.CommodityID = viewModel.CommodityId;
            lossAdjtransaction.ProgramID = viewModel.ProgramId;
            lossAdjtransaction.CommodityGradeID = null; // How did I get this value ? 
            lossAdjtransaction.UnitID = viewModel.UnitId;
            lossAdjtransaction.TransactionDate = DateTime.Now;
            return lossAdjtransaction;
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="user"></param>
        
        public void SaveAdjustmentTrasnsaction(LossesAndAdjustmentsViewModel viewModel, UserProfile user)
        {

            
            Adjustment lossAndAdjustment = new Adjustment();
            TransactionGroup transactionGroup = new TransactionGroup();
            /*
            1	Data Entry Error
            2	Loss (Theft)
            3	Loss (Natural Cause)
            4	Damage
            5  	Data Entry Error
            6	Lost and Found
            
            */
            int transactionSign = -1;
            
            
            if(viewModel.ReasonId==Cats.Models.Adjustment.Constants.DATA_ENTRY_ERROR_MINUS) transactionSign = 1;
               
            var transactionGroupId = Guid.NewGuid();

            //goods in transit
            Transaction transactionOne = createLossAdjustmentTransaction(viewModel, user);

            transactionOne.TransactionGroupID = transactionGroupId;
            transactionOne.LedgerID = Cats.Models.Ledger.Constants.GOODS_IN_TRANSIT;
            transactionOne.QuantityInMT = transactionSign * viewModel.QuantityInMt;
            transactionOne.QuantityInUnit = transactionSign * viewModel.QuantityInUint;
            
            Transaction transactionTwo = createLossAdjustmentTransaction(viewModel, user);

            //goods on hand
            transactionTwo.TransactionGroupID = transactionGroupId;
            transactionTwo.LedgerID = Cats.Models.Ledger.Constants.GOODS_ON_HAND;
            transactionTwo.QuantityInMT = -1 * transactionSign * viewModel.QuantityInMt;
            transactionTwo.QuantityInUnit = -1 * transactionSign * viewModel.QuantityInUint;

            transactionGroup.Transactions.Add(transactionOne);
            transactionGroup.Transactions.Add(transactionTwo);

            
                
                //commited to fdp
                Transaction transactionThree = createLossAdjustmentTransaction(viewModel, user);
                transactionThree.TransactionGroupID = transactionGroupId;
                transactionThree.LedgerID = Cats.Models.Ledger.Constants.COMMITED_TO_FDP;
                transactionThree.QuantityInMT = transactionSign*viewModel.QuantityInMt;
                transactionThree.QuantityInUnit = transactionSign*viewModel.QuantityInUint;

                //statistic free stock
                Transaction transactionFour = createLossAdjustmentTransaction(viewModel, user);
                transactionFour.TransactionGroupID = transactionGroupId;
                transactionFour.LedgerID = Cats.Models.Ledger.Constants.STATISTICS_FREE_STOCK;
                transactionFour.QuantityInMT = -1*transactionSign*viewModel.QuantityInMt;
                transactionFour.QuantityInUnit = -1*transactionSign*viewModel.QuantityInUint;

                transactionGroup.Transactions.Add(transactionThree);
                transactionGroup.Transactions.Add(transactionFour);

            
            
            transactionGroup.TransactionGroupID = transactionGroupId;
                
            lossAndAdjustment.TransactionGroupID = transactionGroupId;
            lossAndAdjustment.AdjustmentID = Guid.NewGuid();
            lossAndAdjustment.PartitionId = 0;
            lossAndAdjustment.TransactionGroup = transactionGroup;
            lossAndAdjustment.HubID = user.DefaultHub.Value;
            lossAndAdjustment.AdjustmentReasonID = viewModel.ReasonId;
            lossAndAdjustment.AdjustmentDirection = "A";
            lossAndAdjustment.AdjustmentDate = viewModel.SelectedDate;
            lossAndAdjustment.ApprovedBy = viewModel.ApprovedBy;
            lossAndAdjustment.Remarks = viewModel.Description;
            lossAndAdjustment.UserProfileID = user.UserProfileID;
            lossAndAdjustment.ReferenceNumber = viewModel.MemoNumber;
            lossAndAdjustment.StoreManName = viewModel.StoreMan;

            // Try to save this transaction
            try
            {
                _unitOfWork.AdjustmentRepository.Add(lossAndAdjustment);
                _unitOfWork.Save();
            }
            catch (Exception exp)
            {
                //dbTransaction.Rollback();
                //TODO: Save the detail of this exception somewhere
                throw new Exception("The Loss / Adjustment Transaction Cannot be saved. <br />Detail Message :" + exp.Message);
            }
        }

        /// <summary>
        /// Saves the loss adjustment transaction.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="user">The user.</param>
        public void SaveLossAdjustmentTransaction(LossesAndAdjustmentsViewModel viewModel, UserProfile user)
        {
            if (viewModel.ReasonId==Cats.Models.Adjustment.Constants.DATA_ENTRY_ERROR_MINUS||viewModel.ReasonId==Cats.Models.Adjustment.Constants.DATA_ENTRY_ERROR_PLUS)
            {
                SaveAdjustmentTrasnsaction(viewModel, user);
            }
            else
            {
                SaveLossTrasnsaction(viewModel, user);

            }
        }


        /// <summary>
        /// Gets the total received from receipt allocation.
        /// </summary>
        /// <param name="siNumber">The si number.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        public decimal GetTotalReceivedFromReceiptAllocation(string siNumber, int commodityId, int hubId)
        {
            return GetTotalReceivedFromReceiptAllocation(
                _shippingInstructionService.GetShipingInstructionId(siNumber), commodityId, hubId);
        }


        /// <summary>
        /// Gets the commodity balance for hub.
        /// </summary>
        /// <param name="HubId">The hub id.</param>
        /// <param name="parentCommodityId">The parent commodity id.</param>
        /// <param name="si">The si.</param>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public decimal GetCommodityBalanceForHub(int HubId, int parentCommodityId, int si, int project)
        {
            var balance = _unitOfWork.TransactionRepository.Get(v =>
                                                                v.HubID == HubId &&
                                                                v.ParentCommodityID == parentCommodityId &&
                                                                v.ShippingInstructionID == si &&
                                                                v.ProjectCodeID == project &&
                                                                v.LedgerID == Cats.Models.Ledger.Constants.GOODS_ON_HAND_UNCOMMITED)
                .Select(v => v.QuantityInMT).ToList();
            if (balance.Any())
            {
                return balance.Sum();
            }
            return 0;
        }


        /// <summary>
        /// Saves the starting balance transaction.
        /// </summary>
        /// <param name="startingBalance">The starting balance.</param>
        /// <param name="user">The user.</param>
        /// <exception cref="System.Exception"></exception>
        public void SaveStartingBalanceTransaction(StartingBalanceViewModel startingBalance, UserProfile user)
        {
            int repositoryAccountGetAccountIDWithCreateNegative = _accountService.GetAccountIdWithCreate(Account.Constants.DONOR, startingBalance.DonorID); ;

            int repositoryProjectCodeGetProjectCodeIdWIthCreateProjectCodeID = _projectCodeService.GetProjectCodeIdWIthCreate(startingBalance.ProjectNumber).ProjectCodeID; ;
            int repositoryShippingInstructionGetSINumberIdWithCreateShippingInstructionID = _shippingInstructionService.GetSINumberIdWithCreate(startingBalance.SINumber).ShippingInstructionID; ;
            int repositoryAccountGetAccountIDWithCreatePosetive = _accountService.GetAccountIdWithCreate(Account.Constants.HUB, user.DefaultHub.Value); ;

            TransactionGroup transactionGroup = new TransactionGroup();

            Transaction transactionOne = new Transaction();

            var transactionGroupId = Guid.NewGuid();

            transactionOne.TransactionID = Guid.NewGuid();
            transactionOne.TransactionGroupID = transactionGroupId;
            transactionOne.PartitionId = 0;
            transactionOne.LedgerID = Cats.Models.Ledger.Constants.GOODS_UNDER_CARE;
            transactionOne.HubOwnerID = user.DefaultHubObj.HubOwner.HubOwnerID;
            transactionOne.AccountID = repositoryAccountGetAccountIDWithCreateNegative;
            transactionOne.HubID = user.DefaultHub.Value;
            transactionOne.StoreID = startingBalance.StoreID;
            transactionOne.Stack = startingBalance.StackNumber;
            transactionOne.ProjectCodeID = repositoryProjectCodeGetProjectCodeIdWIthCreateProjectCodeID;
            transactionOne.ShippingInstructionID = repositoryShippingInstructionGetSINumberIdWithCreateShippingInstructionID;
            transactionOne.ProgramID = startingBalance.ProgramID;
            var comm = _unitOfWork.CommodityRepository.FindById(startingBalance.CommodityID);
            transactionOne.ParentCommodityID = (comm.ParentID != null)
                                                       ? comm.ParentID.Value
                                                       : comm.CommodityID;
            transactionOne.CommodityID = startingBalance.CommodityID;
            transactionOne.CommodityGradeID = null;
            transactionOne.QuantityInMT = 0 - startingBalance.QuantityInMT;
            transactionOne.QuantityInUnit = 0 - startingBalance.QuantityInUnit;
            transactionOne.UnitID = startingBalance.UnitID;
            transactionOne.TransactionDate = DateTime.Now;

            Transaction transactionTwo = new Transaction();

            transactionTwo.TransactionID = Guid.NewGuid();
            transactionTwo.TransactionGroupID = transactionGroupId;
            transactionTwo.PartitionId = 0;
            transactionTwo.LedgerID = Cats.Models.Ledger.Constants.GOODS_ON_HAND;
            transactionTwo.HubOwnerID = user.DefaultHubObj.HubOwnerID;
            transactionTwo.AccountID = repositoryAccountGetAccountIDWithCreatePosetive;
            transactionTwo.HubID = user.DefaultHub.Value;
            transactionTwo.StoreID = startingBalance.StoreID;
            transactionTwo.Stack = startingBalance.StackNumber;
            transactionTwo.ProjectCodeID = repositoryProjectCodeGetProjectCodeIdWIthCreateProjectCodeID;
            transactionTwo.ShippingInstructionID = repositoryShippingInstructionGetSINumberIdWithCreateShippingInstructionID;
            transactionTwo.ProgramID = startingBalance.ProgramID;
            transactionTwo.ParentCommodityID = (comm.ParentID != null)
                                                       ? comm.ParentID.Value
                                                       : comm.CommodityID;
            transactionTwo.CommodityID = startingBalance.CommodityID;
            transactionTwo.CommodityGradeID = null; // How did I get this value ? 
            transactionTwo.QuantityInMT = startingBalance.QuantityInMT;
            transactionTwo.QuantityInUnit = startingBalance.QuantityInUnit;
            transactionTwo.UnitID = startingBalance.UnitID;
            transactionTwo.TransactionDate = DateTime.Now;

            transactionGroup.PartitionId = 0;

            try
            {
                transactionGroup.TransactionGroupID = transactionGroupId;
                transactionGroup.Transactions.Add(transactionOne);
                transactionGroup.Transactions.Add(transactionTwo);
                _unitOfWork.TransactionGroupRepository.Add(transactionGroup);
                _unitOfWork.Save();

            }
            catch (Exception exp)
            {

                //TODO: Save the detail of this exception somewhere
                throw new Exception("The Starting Balance Transaction Cannot be saved. <br />Detail Message :" + exp.Message);
            }
        }


        /// <summary>
        /// Gets the list of starting balances.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <returns></returns>
        public List<StartingBalanceViewModelDto> GetListOfStartingBalances(int hubID)
        {


            return (from t in _unitOfWork.TransactionRepository.Get(t => t.Account != null, null, "ProjectCode,Program,Commodity,Account,TransactionGroup.ReceiveDetails,TransactionGroup.DispatchDetails,TransactionGroup.InternalMovements,TransactionGroup.Adjustments")
                    where
                    !t.TransactionGroup.ReceiveDetails.Any()
                    &&
                    !t.TransactionGroup.DispatchDetails.Any()
                    &&
                    !t.TransactionGroup.InternalMovements.Any()
                    &&
                    !t.TransactionGroup.Adjustments.Any()
                    &&
                    t.HubID == hubID

                    join d in _unitOfWork.DonorRepository.Get() on t.Account.EntityID equals d.DonorID
                    where t.Account.EntityType == "Donor"
                    select new StartingBalanceViewModelDto()
                    {
                        CommodityName = t.Commodity.Name,
                        SINumber = t.ShippingInstruction.Value,
                        ProgramName = t.Program.Name,
                        ProjectCode = t.ProjectCode.Value,
                        QuantityInMT = Math.Abs(t.QuantityInMT),
                        QuantityInUnit = Math.Abs(t.QuantityInUnit),
                        StackNumber = (t.Stack.HasValue) ? t.Stack.Value : 0,
                        StoreName = t.Store.Name,
                        UnitName = t.Unit.Name,
                        DonorName = d.Name,
                    }).ToList();
        }


        /// <summary>
        /// Gets the offloading report.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="dispatchesViewModel">The dispatches view model.</param>
        /// <returns></returns>
        public List<OffloadingReport> GetOffloadingReport(int hubID, DispatchesViewModel dispatchesViewModel)
        {
            DateTime sTime = DateTime.Now;
            DateTime eTime = DateTime.Now;

            if (!dispatchesViewModel.PeriodId.HasValue || dispatchesViewModel.PeriodId == 6)
            {
                //filter it to only the current week
                //sTime = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                eTime = sTime.AddDays(7).Date;
            }
            else
            {
                //start end date filters
                if (dispatchesViewModel.PeriodId == 8)
                {
                    sTime = dispatchesViewModel.StartDate ?? sTime;
                    eTime = dispatchesViewModel.EndDate ?? eTime;
                }
                //allocation round
                else if (dispatchesViewModel.PeriodId == 9)
                {
                }
                //allocation year + month
                else if (dispatchesViewModel.PeriodId == 9)
                {
                }
            }

            string StartTime = sTime.ToShortDateString();
            string EndTime = eTime.ToShortDateString();
            // string HUbName = repository.Hub.FindById(hubID).HubNameWithOwner;
            var dbGetOffloadingReport = _unitOfWork.ReportRepository.RPT_Offloading(hubID, sTime, eTime).ToList();

            if (dispatchesViewModel.ProgramId.HasValue && dispatchesViewModel.ProgramId != 0)
            {
                dbGetOffloadingReport = dbGetOffloadingReport.Where(p => p.ProgramID == dispatchesViewModel.ProgramId).ToList();
            }
            if (dispatchesViewModel.AreaId.HasValue && dispatchesViewModel.AreaId != 0)
            {
                dbGetOffloadingReport = dbGetOffloadingReport.Where(p => p.RegionID == dispatchesViewModel.AreaId).ToList();
            }
            if (dispatchesViewModel.bidRefId != null)
            {
                dbGetOffloadingReport = dbGetOffloadingReport.Where(p => p.BidRefNo == dispatchesViewModel.bidRefId).ToList();
            }


            return (from t in dbGetOffloadingReport
                    group t by new { t.BidRefNo, t.ProgramName, t.Round, t.PeriodMonth, t.PeriodYear, t.RegionName }
                        into b
                        select new OffloadingReport()
                        {
                            ContractNumber = b.Key.BidRefNo,
                            EndDate = EndTime,
                            StartDate = StartTime,
                            Month = Convert.ToString(b.Key.PeriodMonth),
                            Round = Convert.ToString(b.Key.Round),
                            Year = b.Key.PeriodYear,//??0, modified Banty 23_5_13
                            Region = b.Key.RegionName,
                            Program = b.Key.ProgramName,
                            OffloadingDetails = b.Select(t1 => new OffloadingDetail()
                            {
                                RequisitionNumber = t1.RequisitionNo,
                                Product = t1.CommodityName,
                                Zone = t1.ZoneName,
                                Woreda = t1.WoredaName,
                                Destination = t1.FDPName,
                                Allocation = t1.AllocatedInMT ?? 0,
                                Dispatched = t1.DispatchedQuantity ?? 0,
                                Remaining = t1.RemainingAmount ?? 0,
                                Transporter = t1.TransaporterName,
                                Donor = t1.DonorName
                            }).ToList()

                        }).ToList();
        }


        /// <summary>
        /// Gets the receive report.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="receiptsViewModel">The receipts view model.</param>
        /// <returns></returns>
        public List<ReceiveReport> GetReceiveReport(int hubID, ReceiptsViewModel receiptsViewModel)
        {
            DateTime sTime = DateTime.Now;
            DateTime eTime = DateTime.Now;

            if (!receiptsViewModel.PeriodId.HasValue)
            {
                sTime = new DateTime(1979, 01, 01, 00, 00, 00, 000);
            }
            else
            {
                //start end date filters
                if (receiptsViewModel.PeriodId == 8)
                {
                    sTime = receiptsViewModel.StartDate ?? sTime;
                    eTime = receiptsViewModel.EndDate ?? eTime;
                }
                //allocation round
                else if (receiptsViewModel.PeriodId == 6)
                {
                    //filter it to only the current week
                    // sTime = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    eTime = sTime.AddDays(7).Date;
                }
                //allocation year + month
                else if (receiptsViewModel.PeriodId == 9)
                {
                }
            }

            string StartTime = sTime.ToShortDateString();
            string EndTime = eTime.ToShortDateString();
            // string HUbName = repository.Hub.FindById(hubID).HubNameWithOwner;
            var dbGetReceiptReport = _unitOfWork.ReportRepository.RPT_ReceiptReport(hubID, sTime, eTime).ToList();

            if (receiptsViewModel.ProgramId.HasValue && receiptsViewModel.ProgramId != 0)
            {
                dbGetReceiptReport = dbGetReceiptReport.Where(p => p.ProgramID == receiptsViewModel.ProgramId).ToList();
            }
            if (receiptsViewModel.CommodityTypeId.HasValue && receiptsViewModel.CommodityTypeId != 0)
            {
                dbGetReceiptReport = dbGetReceiptReport.Where(p => p.CommodityTypeID == receiptsViewModel.CommodityTypeId).ToList();
            }

            return (from t in dbGetReceiptReport
                    group t by new { t.BudgetYear }
                        into b
                        select new ReceiveReport()
                        {
                            BudgetYear = b.Key.BudgetYear.Value,
                            rows = b.Select(t1 => new ReceiveRow()
                            {
                                Product = t1.CommodityName,
                                Program = t1.ProgramName,
                                Quantity = t1.BalanceInMt.Value,
                                Quarter = t1.Quarter.Value
                                /*MeasurementUnit = t1.BalanceInUnit.Value*/

                            }).ToList()

                        }).ToList();
        }


        /// <summary>
        /// Gets the distribution report.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="distributionViewModel">The distribution view model.</param>
        /// <returns></returns>
        public List<DistributionRows> GetDistributionReport(int hubID, DistributionViewModel distributionViewModel)
        {

            var dbDistributionReport = _unitOfWork.ReportRepository.RPT_Distribution(hubID).ToList();

            if (distributionViewModel.PeriodId.HasValue && distributionViewModel.PeriodId != 0)
            {
                dbDistributionReport = dbDistributionReport.Where(p => p.Quarter == distributionViewModel.PeriodId.Value).ToList();
            }
            if (distributionViewModel.ProgramId.HasValue && distributionViewModel.ProgramId != 0)
            {
                dbDistributionReport = dbDistributionReport.Where(p => p.ProgramID == distributionViewModel.ProgramId.Value).ToList();
            }
            if (distributionViewModel.AreaId.HasValue && distributionViewModel.AreaId != 0)
            {
                dbDistributionReport = dbDistributionReport.Where(p => p.RegionID == distributionViewModel.AreaId.Value).ToList();
            }

            return (from t in dbDistributionReport
                    //  group t by new { t.BudgetYear }
                    //      into b
                    select new DistributionRows()
                    {
                        BudgetYear = t.PeriodYear,
                        Program = t.ProgramName,
                        DistributedAmount = t.DispatchedQuantity.Value,
                        Month = Convert.ToString(t.PeriodMonth),
                        Quarter = t.Quarter.Value,
                        Region = t.RegionName
                    }).ToList();
        }

        public IEnumerable<Transaction> getTransactionsAsof(DateTime date)
        {
            return _unitOfWork.TransactionRepository.FindBy(d => d.TransactionDate <= date);
        }

        public bool DeleteById(System.Guid id)
        {
            var original = FindById(id);
            if (original == null) return false;
            _unitOfWork.TransactionRepository.Delete(original);
            _unitOfWork.Save();
            return true;
        }

        public Transaction FindById(System.Guid id)
        {
            return _unitOfWork.TransactionRepository.Get(t => t.TransactionID == id).FirstOrDefault();
        }

        public IEnumerable<Transaction> Get(System.Linq.Expressions.Expression<Func<Transaction, bool>> filter = null, Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.TransactionRepository.Get(filter, orderBy, includeProperties);
        }
    }
}
