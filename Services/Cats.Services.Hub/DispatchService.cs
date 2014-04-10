

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Data.Hub.UnitWork;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class DispatchService : IDispatchService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DispatchService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddDispatch(Dispatch dispatch)
        {
            _unitOfWork.DispatchRepository.Add(dispatch);

            _unitOfWork.Save();
            return true;

        }
        public bool EditDispatch(Dispatch dispatch)
        {
            _unitOfWork.DispatchRepository.Edit(dispatch);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDispatch(Dispatch dispatch)
        {
            if (dispatch == null) return false;
            _unitOfWork.DispatchRepository.Delete(dispatch);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DispatchRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DispatchRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Dispatch> GetAllDispatch()
        {
            return _unitOfWork.DispatchRepository.GetAll();
        }
        public Dispatch FindById(int id)
        {
            return _unitOfWork.DispatchRepository.FindById(id);
        }
        public Dispatch FindById(System.Guid id)
        {
            return _unitOfWork.DispatchRepository.GetAll().FirstOrDefault(t => t.DispatchID == id);

        }
        public List<Dispatch> FindBy(Expression<Func<Dispatch, bool>> predicate)
        {
            return _unitOfWork.DispatchRepository.FindBy(predicate);
        }
        #endregion

        public Dispatch GetDispatchByGIN(string ginNo)
        {
            return _unitOfWork.DispatchRepository.Get(t => t.GIN == ginNo).FirstOrDefault();
        }

        /// <summary>
        /// Gets the dispatch transaction.
        /// </summary>
        /// <param name="dispatchId">The dispatch id.</param>
        /// <returns></returns>
        public Transaction GetDispatchTransaction(Guid dispatchId)
        {
            var transactionGroup =
                _unitOfWork.DispatchDetailRepository.Get(t => t.DispatchID == dispatchId).Select(t => t.TransactionGroup)
                    .FirstOrDefault();
            if (transactionGroup != null && transactionGroup.Transactions.Count > 0)
            {
                return transactionGroup.Transactions.First();
            }
            return null;
        }

        /// <summary>
        /// Gets the FDP balance.
        /// </summary>
        /// <param name="FDPID">The FDPID.</param>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        public FDPBalance GetFDPBalance(int FDPID, string RequisitionNo)
        {

            var query =
                _unitOfWork.DispatchAllocationRepository.Get(t => t.FDPID == FDPID && t.RequisitionNo == RequisitionNo);
                        

            FDPBalance balance = new FDPBalance();
            if (query.Count() > 0)
            {
                var total = query.Select(t => t.Amount);
                if (total.Count() > 0)
                {
                    balance.TotalAllocation = total.Sum();
                }
                var commited = query.Where(p => p.ShippingInstructionID.HasValue && p.ProjectCodeID.HasValue).Select(t => t.Amount);
                if (commited.Count() > 0)
                {
                    balance.CommitedAllocation = Math.Abs(commited.Sum());
                }
                var transaction = query.FirstOrDefault();
                balance.CommodityTypeID = transaction.Commodity.CommodityTypeID;
                balance.ProgramID = (transaction.ProgramID.HasValue) ? transaction.ProgramID.Value : -1;
                balance.Commodity = transaction.Commodity.Name;
                balance.ProjectCode = (transaction.ProjectCodeID.HasValue) ? transaction.ProjectCode.Value : "Not Applicable";
                // find more details from the dispatch allocation table.
                // TOCHECK: check if this is woring correctly
                //if(transaction.TransactionGroup.DispatchAllocations.Any())
                //{
                //    DispatchAllocation dispatchAllocation =
                //        transaction.TransactionGroup.DispatchAllocations.FirstOrDefault();
                //    balance.BidNumber = dispatchAllocation.BidRefNo;
                //    balance.TransporterID = dispatchAllocation.TransporterID.Value;
                //}
                var tempDispatches =
                    _unitOfWork.DispatchRepository.Get(t => t.RequisitionNo == RequisitionNo && t.FDPID == FDPID);
                var transactions = (from v in tempDispatches
                                    select v.DispatchDetails.FirstOrDefault().TransactionGroup.Transactions);
                List<Transaction> trans = new List<Transaction>();
                foreach (var tran in transactions)
                {
                    if (tran != null)
                    {
                        foreach (var t in tran)
                        {
                            trans.Add(t);
                        }
                    }

                }


                if (balance.CommodityTypeID == 1)
                {
                    balance.TotalDispatchedMT = (from v in trans
                                                 where v.LedgerID == Ledger.Constants.GOODS_IN_TRANSIT
                                                 select v.QuantityInMT).DefaultIfEmpty().Sum();

                }
                else
                {
                    balance.TotalDispatchedMT = (from v in trans
                                                 where v.LedgerID == Ledger.Constants.GOODS_IN_TRANSIT
                                                 select v.QuantityInUnit).DefaultIfEmpty().Sum();

                }

                // Convert the MT to Quintal,
                balance.CurrentBalance = balance.TotalAllocation - (balance.TotalDispatchedMT * 10);
            }

            return balance;
        }

        /// <summary>
        /// Gets the available commodities.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        public List<Commodity> GetAvailableCommodities(string SINumber, int hubID)
        {
            var query =
                _unitOfWork.TransactionRepository.Get(
                    t =>
                    t.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED && t.ShippingInstruction.Value == SINumber &&
                    t.HubID == hubID).Select(t => t.Commodity).Distinct();

            return query.ToList();
        }



        public List<DispatchModelModelDto> ByHubIdAndAllocationIDetached(int hubId, Guid dispatchAllocationId)
        {
            List<DispatchModelModelDto> dispatchs = new List<DispatchModelModelDto>();
            var tempDispatches =
                _unitOfWork.DispatchRepository.Get(
                    t => t.HubID == hubId && t.DispatchAllocationID == dispatchAllocationId);
            var query = (from r in tempDispatches
                         select new DispatchModelModelDto()
                         {
                             DispatchDate = r.DispatchDate,
                             GIN = r.GIN,
                             DispatchedByStoreMan = r.DispatchedByStoreMan,
                             DispatchID = r.DispatchID
                         });

            return query.ToList();
        }

        public List<DispatchModelModelDto> ByHubIdAndOtherAllocationIDetached(int hubId, Guid OtherDispatchAllocationId)
        {
            List<DispatchModelModelDto> dispatchs = new List<DispatchModelModelDto>();
            var tempDispatches =
              _unitOfWork.DispatchRepository.Get(
                  t => t.HubID == hubId && t.OtherDispatchAllocationID == OtherDispatchAllocationId);
            var query = (from r in tempDispatches
                         select new DispatchModelModelDto()
                         {
                             DispatchDate = r.DispatchDate,
                             GIN = r.GIN,
                             DispatchedByStoreMan = r.DispatchedByStoreMan,
                             DispatchID = r.DispatchID
                         });

            return query.ToList();
        }

        #region Added by banty
        public DispatchViewModel CreateDispatchFromDispatchAllocation(Guid dispatchAllocationId,decimal quantityInUnit)
        {
           
            var dispatchAllocation = _unitOfWork.DispatchAllocationRepository.FindById(dispatchAllocationId);
            
            var dispatch = new DispatchViewModel();
            dispatch.BidNumber = dispatchAllocation.BidRefNo;
            dispatch.CreatedDate = DateTime.Today;
            dispatch.DispatchAllocationID = dispatchAllocation.DispatchAllocationID;
            dispatch.DispatchDate = DateTime.Today;
            dispatch.DispatchID = Guid.NewGuid();
            dispatch.DispatchedByStoreMan = string.Empty;
            dispatch.DriverName = string.Empty;
            dispatch.FDPID = dispatchAllocation.FDPID;
            dispatch.GIN = string.Empty;
            dispatch.HubID = dispatchAllocation.HubID;
           // dispatch.ProgramID = dispatchAllocation.ProgramID;
            if (dispatchAllocation.Month.HasValue)
                dispatch.Month = dispatchAllocation.Month.Value;
            if (dispatchAllocation.Year.HasValue)
                dispatch.Year = dispatchAllocation.Year.Value;
            dispatch.PlateNo_Prime = string.Empty;
            dispatch.PlateNo_Trailer = string.Empty;
            dispatch.Remark = string.Empty;
            dispatch.RequisitionNo = dispatchAllocation.RequisitionNo;
            dispatch.RequisitionId = dispatchAllocation.RequisitionId;
            dispatch.ProgramID = dispatchAllocation.ProgramID.HasValue?dispatchAllocation.ProgramID.Value:0;
            if (dispatchAllocation.Round.HasValue)
                dispatch.Round = dispatchAllocation.Round.Value;
            if (dispatchAllocation.TransporterID.HasValue)
                dispatch.TransporterID = dispatchAllocation.TransporterID.Value;
          
            dispatch.WeighBridgeTicketNumber = string.Empty;

          //  Dispatch dispatchDetail = new DispatchDetail();
            dispatch.CommodityID = dispatchAllocation.CommodityID;
            dispatch.Commodity = dispatchAllocation.Commodity.Name;
            //dispatch.DispatchDetailID = Guid.NewGuid();
            dispatch.DispatchID = dispatch.DispatchID;
            dispatch.Quantity = 0;
            dispatch.QuantityInUnit = 0;
            dispatch.UnitID = dispatchAllocation.Unit;
            dispatch.ShippingInstructionID = dispatchAllocation.ShippingInstructionID;
            dispatch.ProjectCodeID = dispatchAllocation.ProjectCodeID;

           // dispatch.PartitionID = 0;

            //dispatch.DispatchDetails.Add(dispatchDetail);

            return dispatch;
        }




        public IEnumerable<Dispatch> Get(Expression<Func<Dispatch, bool>> filter = null, Func<IQueryable<Dispatch>, IOrderedQueryable<Dispatch>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.DispatchRepository.Get(filter, null, includeProperties);
        }

        #endregion
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


