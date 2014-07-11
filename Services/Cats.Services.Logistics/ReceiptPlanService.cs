using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.Logistics
{

    public class ReceiptPlanService : IReceiptPlanService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ReceiptPlanService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation

        public bool AddReceiptPlan(ReceiptPlan receiptPlan)
        {
            _unitOfWork.ReceiptPlanRepository.Add(receiptPlan);
            _unitOfWork.Save();
            return true;

        }
        public bool EditReceiptPlan(ReceiptPlan receiptPlan)
        {
            _unitOfWork.ReceiptPlanRepository.Edit(receiptPlan);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteReceiptPlan(ReceiptPlan receiptPlan)
        {
            if (receiptPlan == null) return false;
            _unitOfWork.ReceiptPlanRepository.Delete(receiptPlan);
            _unitOfWork.Save();
            return true;
        }

       

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ReceiptPlanRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ReceiptPlanRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<ReceiptPlan> GetAllReceiptPlan()
        {
            return _unitOfWork.ReceiptPlanRepository.GetAll();
        }
        public ReceiptPlan FindById(int id)
        {
            return _unitOfWork.ReceiptPlanRepository.FindById(id);
        }
        public List<ReceiptPlan> FindBy(Expression<Func<ReceiptPlan, bool>> predicate)
        {
            return _unitOfWork.ReceiptPlanRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}

 
      
