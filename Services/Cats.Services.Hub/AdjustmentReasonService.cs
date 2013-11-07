

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class AdjustmentReasonService : IAdjustmentReasonService
    {
        private readonly IUnitOfWork _unitOfWork;


        public AdjustmentReasonService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddAdjustmentReason(AdjustmentReason adjustmentReason)
        {
            _unitOfWork.AdjustmentReasonRepository.Add(adjustmentReason);
            _unitOfWork.Save();
            return true;

        }
        public bool EditAdjustmentReason(AdjustmentReason adjustmentReason)
        {
            _unitOfWork.AdjustmentReasonRepository.Edit(adjustmentReason);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteAdjustmentReason(AdjustmentReason adjustmentReason)
        {
            if (adjustmentReason == null) return false;
            _unitOfWork.AdjustmentReasonRepository.Delete(adjustmentReason);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.AdjustmentReasonRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.AdjustmentReasonRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<AdjustmentReason> GetAllAdjustmentReason()
        {
            return _unitOfWork.AdjustmentReasonRepository.GetAll();
        }
        public AdjustmentReason FindById(int id)
        {
            return _unitOfWork.AdjustmentReasonRepository.FindById(id);
        }
        public List<AdjustmentReason> FindBy(Expression<Func<AdjustmentReason, bool>> predicate)
        {
            return _unitOfWork.AdjustmentReasonRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


