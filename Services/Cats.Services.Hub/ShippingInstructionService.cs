

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Common;


namespace Cats.Services.Hub
{

    public class ShippingInstructionService : IShippingInstructionService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ShippingInstructionService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddShippingInstruction(ShippingInstruction shippingInstruction)
        {
            _unitOfWork.ShippingInstructionRepository.Add(shippingInstruction);
            _unitOfWork.Save();
            return true;

        }
        public bool EditShippingInstruction(ShippingInstruction shippingInstruction)
        {
            _unitOfWork.ShippingInstructionRepository.Edit(shippingInstruction);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteShippingInstruction(ShippingInstruction shippingInstruction)
        {
            if (shippingInstruction == null) return false;
            _unitOfWork.ShippingInstructionRepository.Delete(shippingInstruction);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ShippingInstructionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ShippingInstructionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<ShippingInstruction> GetAllShippingInstruction()
        {
            return _unitOfWork.ShippingInstructionRepository.GetAll();
        }
        public ShippingInstruction FindById(int id)
        {
            return _unitOfWork.ShippingInstructionRepository.FindById(id);
        }
        public List<ShippingInstruction> FindBy(Expression<Func<ShippingInstruction, bool>> predicate)
        {
            return _unitOfWork.ShippingInstructionRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
        /// <summary>
        /// Gets the shiping instruction id.
        /// </summary>
        /// <param name="si">The si.</param>
        /// <returns></returns>
        public int GetShipingInstructionId(string si)
        {
            var instruction =
                _unitOfWork.ShippingInstructionRepository.FindBy(t => t.Value.ToUpper() == si.ToUpper()).SingleOrDefault
                    ();

            if (instruction != null)
            {
                return instruction.ShippingInstructionID;
            }
            return 0;
        }

        /// <summary>
        /// Determines whether the specified si number has balance.
        /// </summary>
        /// <param name="SiNumber">The si number.</param>
        /// <param name="FDPID">The FDPID.</param>
        /// <returns>
        ///   <c>true</c> if the specified si number has balance; otherwise, <c>false</c>.
        /// </returns>
        public bool HasBalance(string SiNumber, int FDPID)
        {
            var balance = _unitOfWork.TransactionRepository.FindBy(t =>
                                                                   t.ShippingInstruction.Value == SiNumber &&
                                                                   t.Account.EntityType == Account.Constants.FDP &&
                                                                   t.Account.EntityID == FDPID
                ).Select(t => t.QuantityInMT).ToList().Sum();

            return (balance > 0);
        }


        /// <summary>
        /// Gets the SI number id With create.
        /// </summary>
        /// <param name="SiNumber">The si number.</param>
        /// <returns></returns>
        public ShippingInstruction GetSINumberIdWithCreate(string SiNumber)
        {
            var instruction =
                _unitOfWork.ShippingInstructionRepository.FindBy(t => t.Value.ToUpper() == SiNumber.ToUpper()).
                    SingleOrDefault();

            if (instruction != null)
            {
                return instruction;
            }
            else
            {
                ShippingInstruction newInstruction = new ShippingInstruction()
                {
                    Value = SiNumber.ToUpperInvariant()
                };
                _unitOfWork.ShippingInstructionRepository.Add(newInstruction);
                _unitOfWork.Save();

                return newInstruction;
            }
        }


        public List<ShippingInstructionViewModel> GetAllShippingInstructionForReport()
        {
            var tempShippingInstructions = _unitOfWork.ShippingInstructionRepository.GetAll();
            var shippingInstructions = (from c in tempShippingInstructions
                                        select
                                            new ShippingInstructionViewModel()
                                            {
                                                ShippingInstructionId = c.ShippingInstructionID,
                                                ShippingInstructionName = c.Value
                                            }).ToList();
            return shippingInstructions;
        }

        public List<ShippingInstructionViewModel> GetShippingInstructionsForProjectCode(int hubId, int projectCodeId)
        {
            var tempShippingInstructions =
                _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.ProjectCodeID == projectCodeId);
            var shippingInstructions = (from v in tempShippingInstructions
                                        select
                                            new ShippingInstructionViewModel
                                            {
                                                ShippingInstructionId = v.ShippingInstructionID.Value,
                                                ShippingInstructionName = v.ShippingInstruction.Value
                                            }).Distinct().
                ToList();
            return shippingInstructions;
        }


        public List<ShippingInstruction> GetShippingInstructionsWithBalance(int hubID, int commodityId)
        {
            var com = _unitOfWork.CommodityRepository.FindById(commodityId);
            var tempSis =
                    _unitOfWork.TransactionRepository.FindBy(
                        t => t.HubID == hubID && t.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED &&
                             t.ParentCommodityID == commodityId);
            if (com.CommodityTypeID == 1)
            {

                var sis = from v in tempSis
                          group v by v.ShippingInstruction
                              into g
                              select new { g.Key, SUM = g.Sum(b => b.QuantityInMT) };

                return (from v in sis
                        //where v.SUM > 0
                        select v.Key).ToList();
            }
            else
            {
                var sis = from v in tempSis//non-food should be 
                          group v by v.ShippingInstruction
                              into g
                              select new { g.Key, SUM = g.Sum(b => b.QuantityInUnit) };

                return (from v in sis
                        //where v.SUM > 0
                        select v.Key).ToList();
            }
        }

        public ProjectCode GetProjectCodeForSI(int hubId, int commodityId, int shippingInstructionID)
        {

            var projectCode = (from v in _unitOfWork.TransactionRepository.GetAll()
                               where
                                   v.HubID == hubId && v.ParentCommodityID == commodityId &&
                                   v.ShippingInstructionID == shippingInstructionID
                               select v.ProjectCode).FirstOrDefault();
            return projectCode;

        }

        /// <summary>
        /// Gets the balance.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <param name="shippingInstructionID">The shipping instruction ID.</param>
        /// <returns></returns>
        public SIBalance GetBalance(int hubID, int commodityId, int shippingInstructionID)
        {
            SIBalance siBalance = new SIBalance();

            siBalance.Commodity = _unitOfWork.CommodityRepository.FindById(commodityId).Name;
            siBalance.SINumberID = shippingInstructionID;

            ProjectCode projectCode = GetProjectCodeForSI(hubID, commodityId, shippingInstructionID);
            siBalance.ProjectCodeID = projectCode.ProjectCodeID;
            siBalance.Project = projectCode.Value;

            ShippingInstruction si = _unitOfWork.ShippingInstructionRepository.FindById(shippingInstructionID);
            var availableBalance = (from v in si.Transactions
                                    where v.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED && commodityId == v.ParentCommodityID
                                    select v.QuantityInMT).DefaultIfEmpty().Sum();

            var firstOrDefaultans = si.Transactions.FirstOrDefault();
            if (firstOrDefaultans != null)
                siBalance.Program = firstOrDefaultans.Program.Name;

            siBalance.AvailableBalance = availableBalance;
            siBalance.SINumber = si.Value;

            // convert the amount which is in Quintals to ... MT
            siBalance.CommitedToFDP = (from v in si.DispatchAllocations
                                       where v.IsClosed == false && v.CommodityID == commodityId
                                       select v.Amount / 10).DefaultIfEmpty().Sum();


            var utilGetDispatchedAllocationFromSiResult = _unitOfWork.ReportRepository.util_GetDispatchedAllocationFromSI(hubID, shippingInstructionID).FirstOrDefault();
            if (utilGetDispatchedAllocationFromSiResult != null)
                if (utilGetDispatchedAllocationFromSiResult.Quantity != null)
                    siBalance.CommitedToFDP -= utilGetDispatchedAllocationFromSiResult.Quantity.Value;

            siBalance.CommitedToOthers = (from v in si.OtherDispatchAllocations
                                          where v.IsClosed == false && v.CommodityID == commodityId
                                          select v.QuantityInMT).DefaultIfEmpty().Sum();


            
            decimal ReaminingExpectedReceipts = 0;
            var allocation = _unitOfWork.ReceiptAllocationRepository.FindBy(r => r.SINumber == siBalance.SINumber).ToList();
            var rAll = allocation
                .Where(
                p =>
                {
                    if (p.Commodity.ParentID == null)
                        return p.CommodityID == commodityId;
                    else
                        return p.Commodity.ParentID == commodityId;
                }
                )
                .Where(q => q.IsClosed == false);

            foreach (var receiptAllocation in rAll)
            {
                ReaminingExpectedReceipts = ReaminingExpectedReceipts +
                                   (receiptAllocation.QuantityInMT
                                    -
                                    GetReceivedAlready(receiptAllocation));
            }
            siBalance.ReaminingExpectedReceipts = ReaminingExpectedReceipts;
            decimal newVariable = (siBalance.CommitedToFDP + siBalance.CommitedToOthers);

            siBalance.Dispatchable = siBalance.AvailableBalance - newVariable + ReaminingExpectedReceipts;

            if (newVariable > 0)
                siBalance.TotalDispatchable = siBalance.AvailableBalance -
                                          (siBalance.CommitedToFDP + siBalance.CommitedToOthers);
            else
                siBalance.TotalDispatchable = siBalance.AvailableBalance;
            return siBalance;

        }
        private decimal GetReceivedAlready(ReceiptAllocation receiptAllocation)
        {
            decimal sum = 0;
            if (receiptAllocation.Receives != null)
                foreach (Receive r in receiptAllocation.Receives)
                {
                    foreach (ReceiveDetail rd in r.ReceiveDetails)
                    {
                        sum = sum + Math.Abs(rd.QuantityInMT);
                    }
                }
            return sum;
        }

        /// <summary>
        /// Gets the balance In Units for non-food items.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <param name="shippingInstructionID">The shipping instruction ID.</param>
        /// <returns></returns>
        public SIBalance GetBalanceInUnit(int hubID, int commodityId, int shippingInstructionID)
        {
            SIBalance siBalance = new SIBalance();

            siBalance.Commodity = _unitOfWork.CommodityRepository.FindById(commodityId).Name;
            siBalance.SINumberID = shippingInstructionID;

            ProjectCode projectCode = GetProjectCodeForSI(hubID, commodityId, shippingInstructionID);
            siBalance.ProjectCodeID = projectCode.ProjectCodeID;
            siBalance.Project = projectCode.Value;

            ShippingInstruction si = _unitOfWork.ShippingInstructionRepository.FindById(shippingInstructionID);
            var availableBalance = (from v in si.Transactions
                                    where v.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED && commodityId == v.ParentCommodityID
                                    select v.QuantityInUnit).DefaultIfEmpty().Sum();

            var firstOrDefaultans = si.Transactions.FirstOrDefault();
            if (firstOrDefaultans != null)
                siBalance.Program = firstOrDefaultans.Program.Name;

            siBalance.AvailableBalance = availableBalance;
            siBalance.SINumber = si.Value;

            // convert the amount which is in Quintals to ... MT
            siBalance.CommitedToFDP = (from v in si.DispatchAllocations
                                       where v.IsClosed == false && v.CommodityID == commodityId
                                       select v.AmountInUnit).DefaultIfEmpty().Sum();
            //select v.Amount / 10).DefaultIfEmpty().Sum();

            var utilGetDispatchedAllocationFromSiResult = _unitOfWork.ReportRepository.util_GetDispatchedAllocationFromSI(hubID, shippingInstructionID).FirstOrDefault();
            if (utilGetDispatchedAllocationFromSiResult != null)
                if (utilGetDispatchedAllocationFromSiResult.QuantityInUnit != null)
                    siBalance.CommitedToFDP -= utilGetDispatchedAllocationFromSiResult.QuantityInUnit.Value;

            siBalance.CommitedToOthers = (from v in si.OtherDispatchAllocations
                                          where v.IsClosed == false && v.CommodityID == commodityId
                                          select v.QuantityInUnit).DefaultIfEmpty().Sum();



            decimal ReaminingExpectedReceipts = 0;
            //TODO:After Implementing ReceiptAllocationService please return here
            //var rAll = repository.ReceiptAllocation.FindBySINumber(siBalance.SINumber)
            //    .Where(
            //    p =>
            //    {
            //        if (p.Commodity.ParentID == null)
            //            return p.CommodityID == commodityId;
            //        else
            //            return p.Commodity.ParentID == commodityId;
            //    }
            //    )
            //    .Where(q => q.IsClosed == false);

            //foreach (var receiptAllocation in rAll)
            //{
            //    decimal Qunt = 0;
            //    if (receiptAllocation.QuantityInUnit != null)
            //        Qunt = receiptAllocation.QuantityInUnit.Value;
            //    ReaminingExpectedReceipts = ReaminingExpectedReceipts +
            //                                    (Qunt
            //                                     -
            //                                     repository.ReceiptAllocation.GetReceivedAlreadyInUnit(receiptAllocation));
            //}
            siBalance.ReaminingExpectedReceipts = ReaminingExpectedReceipts;
            siBalance.Dispatchable = siBalance.AvailableBalance - (siBalance.CommitedToFDP + siBalance.CommitedToOthers) + ReaminingExpectedReceipts;
            siBalance.TotalDispatchable = siBalance.AvailableBalance -
                                          (siBalance.CommitedToFDP + siBalance.CommitedToOthers);
            return siBalance;

        }




    }
}


