using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool Addtransaction(Transaction transaction)
        {
            _unitOfWork.TransactionRepository.Add(transaction);
            _unitOfWork.Save();
            return true;

        }
        public bool EditTransaction(Transaction transaction)
        {
            _unitOfWork.TransactionRepository.Edit(transaction);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteTransaction(Transaction transaction)
        {
            if (transaction == null) return false;
            _unitOfWork.TransactionRepository.Delete(transaction);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TransactionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TransactionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Transaction> GetAllTransaction()
        {
            return _unitOfWork.TransactionRepository.GetAll();
        }
        public Transaction FindById(int id)
        {
            return _unitOfWork.TransactionRepository.FindById(id);
        }
        public List<Transaction> FindBy(Expression<Func<Transaction, bool>> predicate)
        {
            return _unitOfWork.TransactionRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

        public IEnumerable<Transaction> Get(
         Expression<Func<Transaction, bool>> filter = null,
         Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderBy = null,
         string includeProperties = "")
        {
            return _unitOfWork.TransactionRepository.Get(filter, orderBy, includeProperties);
        }

        public List<ProjectCode> getAllProjectByHubCommodity(int hubId, int commodityId)
        {
            var receiptAllocation = ReceiptAllocationFindBy(t => t.HubID == hubId && t.CommodityID == commodityId);
            var transaction = FindBy(t => t.CommodityID == commodityId && t.HubID == hubId);
            //var projectCode=(from receipt in receiptAllocation
            //                 where receipt.QuantityInMT >= (from t in transaction
            //                                                where receipt.ProjectNumber == _unitOfWork.ProjectCodeRepository.FindById(t.ProjectCodeID).Value
            //                                                        && t.LedgerID == 2
            //                                                        && t.QuantityInMT > 0
            //                                                select t.QuantityInMT).Sum()
            //                 select receipt);
            var projectCodeList=new List<ProjectCode>();
            foreach (var receiv in receiptAllocation)
            {
                int projectCodeID=0;
                try
                {
                     projectCodeID = _unitOfWork.ProjectCodeRepository.FindBy(t => t.Value == receiv.ProjectNumber).FirstOrDefault().ProjectCodeID;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                var transactionQuantity = transaction.FindAll(t => t.ProjectCodeID == projectCodeID && t.LedgerID == 2 && t.QuantityInMT >0).Sum(t=>t.QuantityInMT);
                if(receiv.QuantityInMT > transactionQuantity )
                    projectCodeList.Add(new ProjectCode { ProjectCodeID = projectCodeID, Value = receiv.ProjectNumber});
            }
            return projectCodeList;
        }
        public List<ShippingInstruction> getAllSIByHubCommodity(int hubId, int commodityId)
        {
            var transaction = FindBy(t => t.CommodityID == commodityId && t.HubID == hubId);
            var receiptAllocation = ReceiptAllocationFindBy(t => t.HubID == hubId && t.CommodityID == commodityId);

            var ShippingInstructionList = new List<ShippingInstruction>();
            foreach (var receiv in receiptAllocation)
            {
                int SiId = 0;
                try
                {
                    SiId = _unitOfWork.ShippingInstructionRepository.FindBy(t => t.Value == receiv.SINumber).FirstOrDefault().ShippingInstructionID;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                var transactionQuantity = transaction.FindAll(t => t.ProjectCodeID == SiId && t.LedgerID == 2 && t.QuantityInMT > 0).Sum(t => t.QuantityInMT);
                if (receiv.QuantityInMT > transactionQuantity)
                    ShippingInstructionList.Add(new ShippingInstruction { ShippingInstructionID = SiId, Value = receiv.SINumber });
            }
            return ShippingInstructionList;
        }

        public List<ReceiptAllocation> getSIBalance(int hubId, int commodityId)
        {
            var transaction = FindBy(t => t.CommodityID == commodityId && t.HubID == hubId);
            var receiptAllocation = ReceiptAllocationFindBy(t => t.HubID == hubId && t.CommodityID == commodityId);

            var ShippingInstructionList = new List<ReceiptAllocation>();
            decimal balance = 0;
            foreach (var receiv in receiptAllocation)
            {
                int SiId = 0;
                try
                {
                    SiId = _unitOfWork.ShippingInstructionRepository.FindBy(t => t.Value == receiv.SINumber).FirstOrDefault().ShippingInstructionID;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                var transactionQuantity = transaction.FindAll(t => t.ProjectCodeID == SiId && t.LedgerID == 2 && t.QuantityInMT > 0).Sum(t => t.QuantityInMT);
                if (receiv.QuantityInMT > transactionQuantity)
                {
                    balance = receiv.QuantityInMT - transactionQuantity;
                    ShippingInstructionList.Add(new ReceiptAllocation
                                                    {SINumber = receiv.SINumber, QuantityInMT  = balance});
                }
                //else
                //{
                //    ShippingInstructionList.Add(new ReceiptAllocation { SINumber = receiv.SINumber, QuantityInMT = 0 });
                //}
            }
            return ShippingInstructionList;
        }
        public List<ReceiptAllocation> getProjectBalance(int hubId, int commodityId)
        {
            var receiptAllocation = ReceiptAllocationFindBy(t => t.HubID == hubId && t.CommodityID == commodityId);
            var transaction = FindBy(t => t.CommodityID == commodityId && t.HubID == hubId);
            var projectCodeList = new List<ReceiptAllocation>();
            decimal balance = 0;
            foreach (var receiv in receiptAllocation)
            {

                int projectCodeID = 0;
                try
                {
                    projectCodeID = _unitOfWork.ProjectCodeRepository.FindBy(t => t.Value == receiv.ProjectNumber).FirstOrDefault().ProjectCodeID;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                var transactionQuantity = transaction.FindAll(t => t.ProjectCodeID == projectCodeID && t.LedgerID == 2 && t.QuantityInMT > 0).Sum(t => t.QuantityInMT);
                if (receiv.QuantityInMT > transactionQuantity)
                {
                    balance = receiv.QuantityInMT - transactionQuantity;
                    projectCodeList.Add(new ReceiptAllocation { ProjectNumber = receiv.ProjectNumber, QuantityInMT = balance });
                }
                //else
                //{
                //    projectCodeList.Add(new ReceiptAllocation { ProjectNumber = receiv.ProjectNumber, QuantityInMT = 0 });
                //}
            }

            return projectCodeList;
        }

        public List<ReceiptAllocation> ReceiptAllocationFindBy(Expression<Func<ReceiptAllocation, bool>> predicate)
        {
            return _unitOfWork.ReceiptAllocationReository.FindBy(predicate);
        }

        

    }
    
}
