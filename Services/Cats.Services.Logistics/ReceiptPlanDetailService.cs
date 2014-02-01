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

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}

 
      
