

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Text;

using System.Data.Objects.DataClasses;
using Cats.Data.Hub;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{

    public class ReceiptAllocationService : IReceiptAllocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShippingInstructionService _ShippingInstructionService;
        private readonly IDispatchAllocationService _DispatchAllocationService;

        public ReceiptAllocationService(IUnitOfWork unitOfWork, IShippingInstructionService ShippingInstructionService, IDispatchAllocationService DispatchAllocationService)
        {
            this._unitOfWork = unitOfWork;
            this._ShippingInstructionService = ShippingInstructionService;
            this._DispatchAllocationService = DispatchAllocationService;
        }
        #region Default Service Implementation
        public bool AddReceiptAllocation(ReceiptAllocation receiptAllocation)
        {

            _unitOfWork.ReceiptAllocationRepository.Add(receiptAllocation);
            _unitOfWork.Save();
            return true;

        }
        public bool EditReceiptAllocation(ReceiptAllocation receiptAllocation)
        {
            _unitOfWork.ReceiptAllocationRepository.Edit(receiptAllocation);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteReceiptAllocation(ReceiptAllocation receiptAllocation)
        {
            if (receiptAllocation == null) return false;
            _unitOfWork.ReceiptAllocationRepository.Delete(receiptAllocation);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ReceiptAllocationRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ReceiptAllocationRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<ReceiptAllocation> GetAllReceiptAllocation()
        {
            return _unitOfWork.ReceiptAllocationRepository.GetAll();
        }
        public ReceiptAllocation FindById(int id)
        {
            return _unitOfWork.ReceiptAllocationRepository.FindById(id);
        }
        public List<ReceiptAllocation> FindBy(Expression<Func<ReceiptAllocation, bool>> predicate)
        {
            return _unitOfWork.ReceiptAllocationRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

        public ReceiptAllocation FindByID(Guid id)
        {
            return _unitOfWork.ReceiptAllocationRepository.FindBy(t => t.ReceiptAllocationID == id).FirstOrDefault();
        }

        public List<ReceiptAllocation> FindBySINumber(string SINumber)
        {
            return _unitOfWork.ReceiptAllocationRepository.FindBy(r => r.SINumber == SINumber).ToList();
            
        }

        /// <summary>
        /// Gets the uncommited allocation transaction.
        /// </summary>
        /// <param name="CommodityID">The commodity ID.</param>
        /// <param name="ShipingInstructionID">The shiping instruction ID.</param>
        /// <param name="HubID">The hub ID.</param>
        /// <returns></returns>
        public Transaction GetUncommitedAllocationTransaction(int CommodityID, int ShipingInstructionID, int HubID)
        {
            return (_unitOfWork.TransactionRepository.FindBy(
                    t=>t.CommodityID == CommodityID 
                    && t.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED 
                    && t.ShippingInstructionID == ShipingInstructionID 
                    && t.HubID ==HubID).FirstOrDefault());
           
        }

        /// <summary>
        /// Gets the balance.
        /// </summary>
        /// <param name="siNumber">The si number.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <returns></returns>
        public decimal GetBalance(string siNumber, int commodityId)
        {

            decimal total = (_unitOfWork.ReceiptAllocationRepository.FindBy(r=>r.SINumber == siNumber
                                && r.CommodityID == commodityId).Select(t=>t.QuantityInMT).Sum());
           
            return total;
        }

        /// <summary>
        /// Gets the balance for SI.
        /// </summary>
        /// <param name="SInumber">The S inumber.</param>
        /// <param name="hubID"> </param>
        /// <returns></returns>
        public decimal GetBalanceForSI(string SInumber)
        {
            decimal total = 0;

            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get();
            var data = (from ra in receiptAll
                        where ra.SINumber == SInumber && ra.QuantityInMT != null
                        select ra.QuantityInMT);

            if (data != null && data.Count() > 0)
            {
                total = data.Sum();
            }

            return total;
        }

        public List<Commodity> GetAvailableCommodities(string SINumber, int hubId)
        {
            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get();
            var query = (from q in receiptAll
                         where q.SINumber == SINumber && q.HubID == hubId
                         select q.Commodity).Distinct();//.OrderBy(p=>p.ParentID).GroupBy(p=>p.ParentID);

            List<Commodity> optGroupList = new List<Commodity>();

            foreach (var commodity in query)
            {
                if (commodity.ParentID == null)//parent
                {
                    if (!(optGroupList.Exists(p => p.CommodityID == commodity.CommodityID)))
                    {
                        optGroupList.Add(commodity);
                        foreach (var childComm in commodity.Commodity1)
                        {
                            if (!query.Any(p => p.CommodityID == childComm.CommodityID))
                                optGroupList.Add(childComm);
                        }
                    }
                }
                else //child
                {
                    if (!(optGroupList.Exists(p => p.CommodityID == commodity.CommodityID)))
                    {
                        if (!(optGroupList.Exists(p => p.CommodityID == commodity.Commodity2.CommodityID)))
                        {
                            optGroupList.Add(commodity.Commodity2);
                        }
                        optGroupList.Insert(optGroupList.IndexOf(commodity.Commodity2) + 1, commodity);
                    }
                }
            }

           
            return optGroupList;
        }

        public List<string> GetSIsWithOutGiftCertificate()
        {
            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get();
            var query = (from q in receiptAll
                         where q.GiftCertificateDetail == null
                         select q.SINumber).Distinct();
            return query.ToList();
        }

        public decimal GetTotalAllocation(string siNumber, int commodityId, int hubId, int? commoditySourceId)
        {
            decimal totalAllocation = 0;
            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get();
            var allocationSum = (from v in receiptAll
                                 where v.SINumber == siNumber
                                       && v.HubID == hubId
                                       && v.IsClosed == false
                                       && v.CommodityID == commodityId
                                       && v.CommoditySourceID == commoditySourceId
                                       && v.QuantityInMT > 0
                                 select v.QuantityInMT);

            if (allocationSum.Count() > 0)
            {
                totalAllocation = allocationSum.Sum();
            }
            return totalAllocation;
        }

        /// <summary>
        /// Commits the receive allocation.
        /// </summary>
        /// <param name="checkedRecords">The checked records.</param>
        /// <param name="user"></param>
        public void CommitReceiveAllocation(string[] checkedRecords, UserProfile user)
        {

            var db1 = ((IObjectContextAdapter)_unitOfWork).ObjectContext;
            var st = db1.CreateObjectSet<ReceiptAllocation>();
            foreach (string id in checkedRecords)
            {
                //Modified Banty:24/5/2013 to support dbcontext

                //db.ReceiptAllocations.MergeOption  = MergeOption.PreserveChanges;
                st.MergeOption = MergeOption.PreserveChanges;


                //ReceiptAllocation original = db.ReceiptAllocations.FirstOrDefault(p => p.ReceiptAllocationID == Guid.Parse(id));
                var original = st.FirstOrDefault(t => t.ReceiptAllocationID == Guid.Parse(id));

                if (original != null)
                {
                    original.IsCommited = true;
                    db1.SaveChanges();
                    st.MergeOption = MergeOption.NoTracking;
                }
                st.MergeOption = MergeOption.NoTracking;
            }

        }

        public int[] GetListOfSource(int commoditySoureType)
        {
            var x = new int[] { };

            if (commoditySoureType == CommoditySource.Constants.DONATION)
            //TODO remove this Condition line later
            {
                x = new int[] { CommoditySource.Constants.DONATION };
            }
            else if (commoditySoureType == CommoditySource.Constants.LOCALPURCHASE)
            {
                x = new int[] {CommoditySource.Constants.LOCALPURCHASE };
            }
            //else if (commoditySoureType == CommoditySource.Constants.TRANSFER)
            //{
            //    x = new int[] { CommoditySource.Constants.TRANSFER };
            //}
            else if (CommoditySource.Constants.TRANSFER == commoditySoureType ||
                     CommoditySource.Constants.REPAYMENT == commoditySoureType ||
                     CommoditySource.Constants.LOAN == commoditySoureType ||
                     CommoditySource.Constants.SWAP == commoditySoureType)
            {
                x = new int[]
                            {
                                CommoditySource.Constants.REPAYMENT,
                                CommoditySource.Constants.LOAN,
                                CommoditySource.Constants.SWAP,
                                CommoditySource.Constants.TRANSFER
                            };
            }
            return x;
        }

        public List<ReceiptAllocation> GetUnclosedAllocationsDetached(int hubId, int commoditySoureType, bool? closedToo, string weightMeasurmentCode, int? CommodityType)
        {
            List<ReceiptAllocation> GetDetachecedList = new List<ReceiptAllocation>();

            var x = GetListOfSource(commoditySoureType);
            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get().ToList();
            var unclosed = (from rAll in receiptAll
                            where hubId == rAll.HubID
                                 select rAll).ToList();

            if (closedToo == null || closedToo == false)
            {
                unclosed = unclosed.Where(p => p.IsClosed == false).ToList();
            }
            else
            {
                unclosed = unclosed.Where(p => p.IsClosed == true).ToList();
            }


            if (CommodityType.HasValue)
            {
                unclosed = unclosed.Where(p => p.Commodity.CommodityTypeID == CommodityType.Value).ToList();
            }
            else
            {
                unclosed = unclosed.Where(p => p.Commodity.CommodityTypeID == 1).ToList();//by default
            }

            foreach (ReceiptAllocation receiptAllocation in unclosed)
            {
                
               // int si = _unitOfWork.ShippingInstructionRepository.Get(s=>s.ShippingInstructionID == receiptAllocation.si
                int si = _ShippingInstructionService.GetShipingInstructionId(receiptAllocation.SINumber);
                receiptAllocation.RemainingBalanceInMt = receiptAllocation.QuantityInMT -
                                                          GetReceivedAlready(
                                                          receiptAllocation);
                receiptAllocation.CommodityName = receiptAllocation.Commodity.Name;


                if (receiptAllocation.QuantityInUnit != null)
                    receiptAllocation.RemainingBalanceInUnit = receiptAllocation.QuantityInUnit.Value -
                                                               GetReceivedAlreadyInUnit(receiptAllocation);
                else
                    receiptAllocation.RemainingBalanceInUnit = 0 -
                                           GetReceivedAlreadyInUnit(receiptAllocation);


                if (weightMeasurmentCode == "qn")
                {
                    receiptAllocation.QuantityInMT *= 10;
                    receiptAllocation.RemainingBalanceInMt *= 10;
                    receiptAllocation.ReceivedQuantityInMT *= 10;
                }
                //modified Banty:24_5_2013 from db.Detach to ((IObjectContextAdapter)db).ObjectContext.Detach
             //   ((IObjectContextAdapter)db).ObjectContext.Detach(receiptAllocation);
                //Commented out by banty 16/8/2013
                GetDetachecedList.Add(receiptAllocation);
            }

            return GetDetachecedList.ToList();

        }

        public decimal GetReceivedAlready(ReceiptAllocation receiptAllocation)
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

        public decimal GetReceivedAlreadyInUnit(ReceiptAllocation receiptAllocation)
        {
            decimal sum = 0;
            if (receiptAllocation.Receives != null)
                foreach (Receive r in receiptAllocation.Receives)
                {
                    foreach (ReceiveDetail rd in r.ReceiveDetails)
                    {
                        sum = sum + Math.Abs(rd.QuantityInUnit);
                    }
                }
            return sum;
        }
       

        public void CloseById(Guid id)
        {
            var delAllocation = _unitOfWork.ReceiptAllocationRepository.Get().FirstOrDefault(allocation => allocation.ReceiptAllocationID == id);
            if (delAllocation != null) delAllocation.IsClosed = true;
            _unitOfWork.ReceiptAllocationRepository.Add(delAllocation);
            _unitOfWork.Save();
        }


        //public List<ReceiptAllocation> GetAllByType(int commoditySourceType)
        //{
        //    return db.ReceiptAllocations.Where(p => p.CommoditySourceID == commoditySourceType).ToList();
        //}


        public List<string> GetSIsWithOutGiftCertificate(int commoditySoureType)
        {
            var x = GetListOfSource(commoditySoureType);
            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get();
            var query = (from q in receiptAll
                         where q.GiftCertificateDetail == null && x.Any(p1 => p1 == q.CommoditySourceID)
                         select q.SINumber).Distinct();

            return query.ToList();
        }


        public List<ReceiptAllocation> GetAllByTypeMerged(int commoditySoureType)
        {
            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get();
            var x = new int[] { };

            if (commoditySoureType == CommoditySource.Constants.DONATION)//TODO remove this Condition line later
            {
                x = new int[] { CommoditySource.Constants.DONATION };
            }
            else if (commoditySoureType == CommoditySource.Constants.LOCALPURCHASE)
            {
                x = new int[] { CommoditySource.Constants.LOCALPURCHASE };
            }
            //else if (commoditySoureType == CommoditySource.Constants.TRANSFER)
            //{
            //    x = new int[] { CommoditySource.Constants.TRANSFER };
            //}
            else if (CommoditySource.Constants.REPAYMENT == commoditySoureType ||
                commoditySoureType == CommoditySource.Constants.TRANSFER ||
                CommoditySource.Constants.LOAN == commoditySoureType ||
                CommoditySource.Constants.SWAP == commoditySoureType)
            {
                x = new int[]{  CommoditySource.Constants.REPAYMENT, 
                                CommoditySource.Constants.LOAN, 
                                CommoditySource.Constants.SWAP,
                                CommoditySource.Constants.TRANSFER};
            }

           // var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get();
            var query = (from q in receiptAll
                         where x.Any(p1 => p1 == q.CommoditySourceID)
                         select q);

            return query.ToList();
        }

        public bool IsSINSource(int source, string siNumber)
        {
            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get();
            var query = (from q in receiptAll
                         where q.SINumber == siNumber && q.CommoditySourceID == source
                         select q);

            return query.Any();
        }


        public List<Commodity> GetAvailableCommoditiesFromUnclosed(string SINumber, int hubId, int? commoditySourceId)
        {
            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get();
            var query = (from q in receiptAll
                         where q.SINumber == SINumber && q.IsClosed == false && q.HubID == hubId && q.CommoditySourceID == commoditySourceId
                         select q.Commodity).Distinct();

            return query.ToList();
        }

        /// <summary>
        /// Gets the SI balances.
        /// </summary>
        /// <returns></returns>
        public List<ReceiptAllocation> GetSIBalances(string SINumber)
        {
            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get();
            var list = (from RA in receiptAll
                        where RA.IsClosed == false && RA.SINumber == SINumber
                        select RA).ToList();

            return list;
        }

        public List<SIBalance> GetSIBalanceForCommodity(int hubId, int CommodityId)
        {
            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get();
            var list = (from RA in receiptAll
                        where
                            (RA.IsClosed == false && RA.HubID == hubId) &&
                            (RA.Commodity.ParentID == CommodityId || RA.CommodityID == CommodityId)
                        group RA by new { RA.SINumber, RA.ProjectNumber, RA.Commodity, RA.Program } into si
                        select new SIBalance
                        {
                            AvailableBalance = 0,
                            CommitedToFDP = 0,
                            CommitedToOthers = 0,
                            Commodity = si.Key.Commodity.Name,
                            Dispatchable = si.Sum(p => p.QuantityInMT),
                            SINumber = si.Key.SINumber,
                            Program = si.Key.Program.Name,
                            Project = si.Key.ProjectNumber,
                            ProjectCodeID = 0,
                            ReaminingExpectedReceipts = si.Sum(p => p.QuantityInMT),//RA.QuantityInMT,
                            TotalDispatchable = 0,//si.Sum(p => p.QuantityInMT),

                        }).ToList();


            foreach (var siBalance in list)
            {
                var sis = _ShippingInstructionService.GetShipingInstructionId(siBalance.SINumber);
                //siBalance.ReaminingExpectedReceipts     = totalSumForComm; 

                //decimal totalSumForComm = (from rAll in db.ReceiptAllocations
                //                           where rAll.IsClosed == false && rAll.SINumber == siBalance.SINumber
                //                                 && rAll.CommodityID == CommodityId ||
                //                                 rAll.Commodity.ParentID == CommodityId
                //                           select rAll.QuantityInMT).Sum();
                //siBalance.ReaminingExpectedReceipts = totalSumForComm;


                if (sis != 0)
                {
                    //siBalance.SINumberID = sis;
                    //var correctedBalance = repository.ShippingInstruction.GetBalance(hubId, CommodityId, sis);

                    //if()
                    //    repository.ReceiptAllocation.GetReceivedAlready()
                    //list.Sum(p => p.ReaminingExpectedReceipts);


                    //siBalance.SINumberID);
                    //siBalance.Dispatchable = correctedBalance.Dispatchable;
                    //siBalance.TotalDispatchable = correctedBalance.TotalDispatchable;
                    //siBalance.Dispatchable = correctedBalance.Dispatchable;
                    //siBalance.ReaminingExpectedReceipts = correctedBalance.ReaminingExpectedReceipts;
                    var commodityId = _unitOfWork.CommodityRepository.FindById(CommodityId).ParentID ?? CommodityId;

                    ShippingInstruction sIns = _unitOfWork.ShippingInstructionRepository.FindById(sis);
                    
                    // convert the amount which is in Quintals to ... MT
                    siBalance.CommitedToFDP = (from v in sIns.DispatchAllocations
                                               where v.IsClosed == false && v.CommodityID == commodityId
                                               select v.Amount / 10).DefaultIfEmpty().Sum();
                    var utilGetDispatchedAllocationFromSiResult = _unitOfWork.ReportRepository.util_GetDispatchedAllocationFromSI(hubId, sis).FirstOrDefault();
                    if (utilGetDispatchedAllocationFromSiResult != null)
                    {
                        if (utilGetDispatchedAllocationFromSiResult.QuantityInUnit != null)
                            siBalance.CommitedToFDP -= utilGetDispatchedAllocationFromSiResult.QuantityInUnit.Value;
                    }


                    siBalance.CommitedToOthers = (from v in sIns.OtherDispatchAllocations
                                                  where v.IsClosed == false && v.CommodityID == commodityId
                                                  select v.QuantityInMT).DefaultIfEmpty().Sum();
                    //sum up all the Expected reamining quantities
                    //siBalance.ReaminingExpectedReceipts = siBalance.ReaminingExpectedReceipts;
                    siBalance.Dispatchable = siBalance.AvailableBalance - (siBalance.CommitedToFDP + siBalance.CommitedToOthers) + siBalance.ReaminingExpectedReceipts;

                    //TODO if(siBalance.CommitedToFDP + siBalance.CommitedToOthers == 0 )//set total to 0
                    if ((siBalance.CommitedToFDP + siBalance.CommitedToOthers) == 0)
                        siBalance.TotalDispatchable = 0;
                    else
                        siBalance.TotalDispatchable = siBalance.AvailableBalance -
                                                  (siBalance.CommitedToFDP + siBalance.CommitedToOthers);

                }
            }

            return list;
            //return null;
        }
        public bool DeleteByID(Guid id)
        {
            var origin = FindById(id);
            if (origin == null) return false;
            _unitOfWork.ReceiptAllocationRepository.Delete(origin);
            _unitOfWork.Save();
            return true;
        }

       

        public ReceiptAllocation FindById(Guid id)
        {
            return _unitOfWork.ReceiptAllocationRepository.FindBy(t => t.ReceiptAllocationID == id).FirstOrDefault();
        }

        public List<SIBalance> GetSIBalanceForCommodityInUnit(int hubId, int CommodityId)
        {
            var list = (from RA in _unitOfWork.ReceiptAllocationRepository.Get()
                        where
                            (RA.IsClosed == false && RA.HubID == hubId) &&
                            (RA.Commodity.ParentID == CommodityId || RA.CommodityID == CommodityId)
                        group RA by new { RA.SINumber, RA.ProjectNumber, RA.Commodity, RA.Program } into si
                        select new SIBalance
                        {
                            AvailableBalance = 0,
                            CommitedToFDP = 0,
                            CommitedToOthers = 0,
                            Commodity = si.Key.Commodity.Name,
                            Dispatchable = si.Sum(p => p.QuantityInUnit ?? 0),
                            //                  {
                            //                      if (p.QuantityInUnit == null)
                            //return 0;
                            //                      return p.QuantityInUnit.Value;
                            //                  }),
                            SINumber = si.Key.SINumber,
                            Program = si.Key.Program.Name,
                            Project = si.Key.ProjectNumber,
                            ProjectCodeID = 0,
                            ReaminingExpectedReceipts = si.Sum(p => p.QuantityInUnit ?? 0),
                            //                               {
                            //                                   if (p.QuantityInUnit == null)
                            //return 0;
                            //                                   return p.QuantityInUnit.Value;
                            //                               }),
                            TotalDispatchable = 0,//si.Sum(p => p.QuantityInMT),

                        }).ToList();


            foreach (var siBalance in list)
            {
                var sis = _ShippingInstructionService.GetShipingInstructionId(siBalance.SINumber);
                //siBalance.ReaminingExpectedReceipts     = totalSumForComm; 

                //decimal totalSumForComm = (from rAll in db.ReceiptAllocations
                //                           where rAll.IsClosed == false && rAll.SINumber == siBalance.SINumber
                //                                 && rAll.CommodityID == CommodityId ||
                //                                 rAll.Commodity.ParentID == CommodityId
                //                           select rAll.QuantityInMT).Sum();
                //siBalance.ReaminingExpectedReceipts = totalSumForComm;


                if (sis != 0)
                {
              
                   
                    int commodityId = _unitOfWork.CommodityRepository.FindById(CommodityId).ParentID ?? CommodityId;
                    ShippingInstruction si = _unitOfWork.ShippingInstructionRepository.FindById(sis);
                    // convert the amount which is in Quintals to ... MT
                    siBalance.CommitedToFDP = (from v in si.DispatchAllocations
                                               where v.IsClosed == false && v.CommodityID == commodityId
                                               select v.AmountInUnit).DefaultIfEmpty().Sum();
                    var utilGetDispatchedAllocationFromSiResult = _unitOfWork.ReportRepository.util_GetDispatchedAllocationFromSI(hubId, sis).FirstOrDefault();
                    if (utilGetDispatchedAllocationFromSiResult != null)
                        if (utilGetDispatchedAllocationFromSiResult.QuantityInUnit != null)
                            siBalance.CommitedToFDP -= utilGetDispatchedAllocationFromSiResult.QuantityInUnit.Value;

                    siBalance.CommitedToOthers = (from v in si.OtherDispatchAllocations
                                                  where v.IsClosed == false && v.CommodityID == commodityId
                                                  select v.QuantityInUnit).DefaultIfEmpty().Sum();
                    //sum up all the Expected reamining quantities
                    //siBalance.ReaminingExpectedReceipts = siBalance.ReaminingExpectedReceipts;
                    siBalance.Dispatchable = siBalance.AvailableBalance - (siBalance.CommitedToFDP + siBalance.CommitedToOthers) + siBalance.ReaminingExpectedReceipts;

                    //TODO if(siBalance.CommitedToFDP + siBalance.CommitedToOthers == 0 )//set total to 0
                    if ((siBalance.CommitedToFDP + siBalance.CommitedToOthers) == 0)
                        siBalance.TotalDispatchable = 0;
                    else
                        siBalance.TotalDispatchable = siBalance.AvailableBalance -
                                                  (siBalance.CommitedToFDP + siBalance.CommitedToOthers);

                }
            }

            return list;
            //return null;
        }

    }
}


