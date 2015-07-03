

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Data.Hub.UnitWork;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;


namespace Cats.Services.Hub
{

    public class AdjustmentService : IAdjustmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionService _transactionService;

        public AdjustmentService(IUnitOfWork unitOfWork,ITransactionService transactionService)
        {
            this._unitOfWork = unitOfWork;
            this._transactionService = transactionService;
        }
        #region Default Service Implementation
        public bool AddAdjustment(Adjustment adjustment)
        {
            _unitOfWork.AdjustmentRepository.Add(adjustment);
            _unitOfWork.Save();
            return true;

        }
        public bool EditAdjustment(Adjustment adjustment)
        {
            _unitOfWork.AdjustmentRepository.Edit(adjustment);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteAdjustment(Adjustment adjustment)
        {
            if (adjustment == null) return false;
            _unitOfWork.AdjustmentRepository.Delete(adjustment);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.AdjustmentRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.AdjustmentRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Adjustment> GetAllAdjustment()
        {
            return _unitOfWork.AdjustmentRepository.GetAll();
        }
        public Adjustment FindById(int id)
        {
            return _unitOfWork.AdjustmentRepository.FindById(id);
        }
        public List<Adjustment> FindBy(Expression<Func<Adjustment, bool>> predicate)
        {
            return _unitOfWork.AdjustmentRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }


        public List<LossAndAdjustmentLogViewModel> GetAllLossAndAdjustmentLog(int hubId)
        {
            List<LossAndAdjustmentLogViewModel> lossAndAdjustmentsViewModel = new List<LossAndAdjustmentLogViewModel>();

            var lossAndAdjustments = (from c in _unitOfWork.AdjustmentRepository.GetAll()
                                      where c.HubID == hubId
                                      select c);

            foreach (var lossAndAdjustment in lossAndAdjustments)
            {
                lossAndAdjustmentsViewModel.AddRange(from transaction in lossAndAdjustment.TransactionGroup.Transactions
                                                     where transaction.TransactionGroupID == lossAndAdjustment.TransactionGroupID
                                                     where transaction.QuantityInMT >= 0
                                                     select new LossAndAdjustmentLogViewModel
                                                     {
                                                         TransactionId = transaction.TransactionID,
                                                         Type = lossAndAdjustment.AdjustmentDirection,
                                                         CommodityName = transaction.CommodityID.HasValue ? _unitOfWork.CommodityRepository.FindById(transaction.CommodityID.Value).Name:string.Empty,
                                                         //ProjectCodeName = transaction.ProjectCode.Value,
                                                         MemoNumber = lossAndAdjustment.ReferenceNumber,
                                                         ShippingInstructionName = transaction.ShippingInstruction.Value,
                                                         //Store = string.Format("{0} - {1} ", transaction.Store.Name, transaction.Store.StoreManName),
                                                         //StoreMan = lossAndAdjustment.StoreManName,
                                                         Reason = lossAndAdjustment.AdjustmentReason.Name,
                                                         Description = lossAndAdjustment.Remarks,
                                                         Unit = transaction.Unit.Name,
                                                         QuantityInMt = transaction.QuantityInMT,
                                                         QuantityInUnit = transaction.QuantityInUnit,
                                                         ApprovedBy = lossAndAdjustment.ApprovedBy,
                                                         Date = lossAndAdjustment.AdjustmentDate
                                                     });
            }

            return lossAndAdjustmentsViewModel;
        }

        public void AddNewLossAndAdjustment(LossesAndAdjustmentsViewModel viewModel, UserProfile user)
        {
            _transactionService.SaveLossAdjustmentTransaction(viewModel, user);
        }

    }
}


