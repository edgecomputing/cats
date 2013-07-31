using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{

    public class NeedAssessmentService : INeedAssessmentService
    {
        private readonly IUnitOfWork _unitOfWork;


        public NeedAssessmentService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddNeedAssessment(NeedAssessment needAssessment)
        {
            _unitOfWork.NeedAssessmentRepository.Add(needAssessment);
            _unitOfWork.Save();
            return true;

        }
        public bool EditNeedAssessment(NeedAssessment needAssessment)
        {
            _unitOfWork.NeedAssessmentRepository.Edit(needAssessment);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteNeedAssessment(NeedAssessment needAssessment)
        {
            if (needAssessment == null) return false;
            _unitOfWork.NeedAssessmentRepository.Delete(needAssessment);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.NeedAssessmentRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.NeedAssessmentRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<NeedAssessment> GetAllNeedAssessment()
        {
            return _unitOfWork.NeedAssessmentRepository.GetAll();
        }
        public NeedAssessment FindById(int id)
        {
            return _unitOfWork.NeedAssessmentRepository.FindById(id);
        }
        public List<NeedAssessment> FindBy(Expression<Func<NeedAssessment, bool>> predicate)
        {
            return _unitOfWork.NeedAssessmentRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}

 
      
