using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.Logistics
{

    public class ReceiptPlanDetailService : IReceiptPlanDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ReceiptPlanDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddReceiptPlanDetail(ReceiptPlanDetail receiptPlanDetail)
        {
            _unitOfWork.ReceiptPlanDetailRepository.Add(receiptPlanDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditReceiptPlanDetail(ReceiptPlanDetail receiptPlanDetail)
        {
            _unitOfWork.ReceiptPlanDetailRepository.Edit(receiptPlanDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteReceiptPlanDetail(ReceiptPlanDetail receiptPlanDetail)
        {
            if (receiptPlanDetail == null) return false;
            _unitOfWork.ReceiptPlanDetailRepository.Delete(receiptPlanDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ReceiptPlanDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ReceiptPlanDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<ReceiptPlanDetail> GetAllReceiptPlanDetail()
        {
            return _unitOfWork.ReceiptPlanDetailRepository.GetAll();
        }
        public ReceiptPlanDetail FindById(int id)
        {
            return _unitOfWork.ReceiptPlanDetailRepository.FindById(id);
        }
        public List<ReceiptPlanDetail> FindBy(Expression<Func<ReceiptPlanDetail, bool>> predicate)
        {
            return _unitOfWork.ReceiptPlanDetailRepository.FindBy(predicate);
        }
        #endregion

        public List<ReceiptPlanDetail> GetNewReceiptPlanDetail()
        {
            var hubs = _unitOfWork.HubRepository.Get(h => h.HubOwnerID == 1);
            List<ReceiptPlanDetail> _receiptPlanDetails = new List<ReceiptPlanDetail>();
            foreach (var hub in hubs)
            {
                var receiptDetailPlan = new ReceiptPlanDetail();
                receiptDetailPlan.HubId = hub.HubID;
                receiptDetailPlan.Hub = hub;
                receiptDetailPlan.Allocated = 0;
                receiptDetailPlan.Received = 0;
                receiptDetailPlan.Balance = 0;
                _receiptPlanDetails.Add(receiptDetailPlan);

            }
            return _receiptPlanDetails;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}

 
      
