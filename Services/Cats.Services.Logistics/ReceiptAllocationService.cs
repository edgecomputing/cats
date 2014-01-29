using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.EarlyWarning;

namespace Cats.Services.Logistics
{
    public class ReceiptAllocationService:IReceiptAllocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EarlyWarning.IShippingInstructionService _ShippingInstructionService;

        public ReceiptAllocationService(IUnitOfWork unitOfWork, IShippingInstructionService shippingInstructionService)
        {
            this._unitOfWork = unitOfWork;
            _ShippingInstructionService = shippingInstructionService;
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

        
        public Transaction GetUncommitedAllocationTransaction(int CommodityID, int ShipingInstructionID, int HubID)
        {
            return (_unitOfWork.TransactionRepository.FindBy(
                    t => t.CommodityID == CommodityID
                    && t.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED
                    && t.ShippingInstructionID == ShipingInstructionID
                    && t.HubID == HubID).FirstOrDefault());

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


        public List<ReceiptAllocation> GetUnclosedAllocationsDetached(int hubId, int commoditySoureType, bool? closedToo, string weightMeasurmentCode, int? CommodityType)
        {
            var getDetachecedList = new List<ReceiptAllocation>();

           
            var receiptAll = _unitOfWork.ReceiptAllocationRepository.Get().ToList();
            var unclosed = (from rAll in receiptAll
                            where hubId == rAll.HubID && rAll.CommoditySourceID == commoditySoureType
                            select rAll).ToList();

            if (closedToo == null || closedToo == false)
            {
                unclosed = unclosed.Where(p => p.IsClosed == false).ToList();
            }
            else
            {
                unclosed = unclosed.Where(p => p.IsClosed == true).ToList();
            }


            unclosed = CommodityType.HasValue ? unclosed.Where(p => p.Commodity.CommodityTypeID == CommodityType.Value).ToList() : unclosed.Where(p => p.Commodity.CommodityTypeID == 1).ToList();

            foreach (ReceiptAllocation receiptAllocation in unclosed)
            {

                
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
                getDetachecedList.Add(receiptAllocation);
            }

            return getDetachecedList.ToList();

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

        private decimal GetReceivedAlreadyInUnit(ReceiptAllocation receiptAllocation)
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
    }
}
