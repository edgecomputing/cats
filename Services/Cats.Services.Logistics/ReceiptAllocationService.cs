using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;
namespace Cats.Services.Logistics
{
    public class ReceiptAllocationService:IReceiptAllocationService
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public ReceiptAllocationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
          
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
    }
}
