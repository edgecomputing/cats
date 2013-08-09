using System;
using System.Linq;
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

       public int GetNeedAssessmentBeneficiaryNo(int id,int weredaId)
       {
           var beneficiaryNo = _unitOfWork.NeedAssessmentDetailRepository.FindBy(w => w.District == weredaId && w.NAId == id && w.NeedAssessmentHeader.NeedAApproved == true).SingleOrDefault();

           if (beneficiaryNo != null)
           {
               var totalBeneficiaties = (int)(beneficiaryNo.VPoorNoOfB + beneficiaryNo.PoorNoOfB + beneficiaryNo.MiddleNoOfB + beneficiaryNo.BOffNoOfB);
               return totalBeneficiaties;
           }
           return 0;
       }
       public int GetNeedAssessmentMonths(int id,int weredaId)
       {
           var months = _unitOfWork.NeedAssessmentDetailRepository.FindBy(w => w.District == weredaId && w.NAId == id && w.NeedAssessmentHeader.NeedAApproved == true).SingleOrDefault();

           if (months != null)
           {
               var totalMonths = (int)(months.VPoorNoOfM + months.PoorNoOfM + months.MiddleNoOfM + months.BOffNoOfM);
               return totalMonths;
           }
           return 0;
       }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


