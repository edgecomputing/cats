using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{

    public class NeedAssessmentDetailService : INeedAssessmentDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public NeedAssessmentDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddNeedAssessmentDetail(NeedAssessmentDetail needAssessmentDetail)
        {
            _unitOfWork.NeedAssessmentDetailRepository.Add(needAssessmentDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditNeedAssessmentDetail(NeedAssessmentDetail needAssessmentDetail)
        {
            _unitOfWork.NeedAssessmentDetailRepository.Edit(needAssessmentDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteNeedAssessmentDetail(NeedAssessmentDetail needAssessmentDetail)
        {
            if (needAssessmentDetail == null) return false;
            _unitOfWork.NeedAssessmentDetailRepository.Delete(needAssessmentDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.NeedAssessmentDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.NeedAssessmentDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<NeedAssessmentDetail> GetAllNeedAssessmentDetail()
        {
            return _unitOfWork.NeedAssessmentDetailRepository.GetAll();
        }
        public NeedAssessmentDetail FindById(int id)
        {
            return _unitOfWork.NeedAssessmentDetailRepository.FindById(id);
        }
        public List<NeedAssessmentDetail> FindBy(Expression<Func<NeedAssessmentDetail, bool>> predicate)
        {
            return _unitOfWork.NeedAssessmentDetailRepository.FindBy(predicate);
        }
        #endregion


        public List<NeedAssessmentDetail> GetDraft()
        {
            return _unitOfWork.NeedAssessmentDetailRepository.FindBy(d => d.NeedAssessmentHeader.NeedAApproved == false);
        }
       public List<NeedAssessmentDetail> GetApproved()
        {
            return _unitOfWork.NeedAssessmentDetailRepository.FindBy(d => d.NeedAssessmentHeader.NeedAApproved == true);
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


