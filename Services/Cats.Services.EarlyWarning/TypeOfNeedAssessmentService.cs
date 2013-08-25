using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{

    public class TypeOfNeedAssessmentService : ITypeOfNeedAssessmentService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TypeOfNeedAssessmentService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddTypeOfNeedAssessment(TypeOfNeedAssessment typeOfNeedAssessment)
        {
            _unitOfWork.TypeOfNeedAssessmentRepository.Add(typeOfNeedAssessment);
            _unitOfWork.Save();
            return true;

        }
        public bool EditTypeOfNeedAssessment(TypeOfNeedAssessment typeOfNeedAssessment)
        {
            _unitOfWork.TypeOfNeedAssessmentRepository.Edit(typeOfNeedAssessment);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteTypeOfNeedAssessment(TypeOfNeedAssessment typeOfNeedAssessment)
        {
            if (typeOfNeedAssessment == null) return false;
            _unitOfWork.TypeOfNeedAssessmentRepository.Delete(typeOfNeedAssessment);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TypeOfNeedAssessmentRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TypeOfNeedAssessmentRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<TypeOfNeedAssessment> GetAllTypeOfNeedAssessment()
        {
            return _unitOfWork.TypeOfNeedAssessmentRepository.GetAll();
        }
        public TypeOfNeedAssessment FindById(int id)
        {
            return _unitOfWork.TypeOfNeedAssessmentRepository.FindById(id);
        }
        public List<TypeOfNeedAssessment> FindBy(Expression<Func<TypeOfNeedAssessment, bool>> predicate)
        {
            return _unitOfWork.TypeOfNeedAssessmentRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}

 
      
