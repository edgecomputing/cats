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
            return _unitOfWork.NeedAssessmentDetailRepository.FindBy(d => d.NeedAssessmentHeader.NeedAssessment.NeedAApproved == false);
        }
       public List<NeedAssessmentDetail> GetApproved()
        {
            return _unitOfWork.NeedAssessmentDetailRepository.FindBy(d => d.NeedAssessmentHeader.NeedAssessment.NeedAApproved == true);
        }

       public int GetNeedAssessmentBeneficiaryNo(int year , int season, int woredaId)
       {
           var beneficiaryNo = _unitOfWork.NeedAssessmentDetailRepository.FindBy(w => w.NeedAssessmentHeader.NeedAssessment.NeedADate.Value.Year == year && w.NeedAssessmentHeader.NeedAssessment.Season == season && w.Woreda == woredaId &&  w.NeedAssessmentHeader.NeedAssessment.NeedAApproved == true).SingleOrDefault();

           if (beneficiaryNo != null)
           {
               var totalBeneficiaties = (int)(beneficiaryNo.PSNPFromWoredasMale + beneficiaryNo.PSNPFromWoredasFemale + beneficiaryNo.NonPSNPFromWoredasMale + beneficiaryNo.NonPSNPFromWoredasFemale);
               return totalBeneficiaties;
           }
           return 0;
       }
       
       public int GetNeedAssessmentMonthsGetNeedAssessmentMonths(int year, int season, int woredaId)
       {
           var months = _unitOfWork.NeedAssessmentDetailRepository.FindBy(w => w.NeedAssessmentHeader.NeedAssessment.NeedADate.Value.Year == year && w.NeedAssessmentHeader.NeedAssessment.Season == season && w.Woreda == woredaId && w.NeedAssessmentHeader.NeedAssessment.NeedAApproved == true).SingleOrDefault();

           if (months != null)
           {

               if (months.NonPSNPFromWoredasDOA != null)
               {
                   var totalMonths = (int)(months.NonPSNPFromWoredasDOA);
                   return totalMonths;
               }
               else if (months.PSNPFromWoredasDOA != null)
               {
                   return (int)(months.PSNPFromWoredasDOA);
               }
               else return 0;
               
           }
           return 0;
       }
       public int GetNeedAssessmentBeneficiaryNoFromPlan(int planID, int woredaID)
       {
           var beneficiaryNo = _unitOfWork.NeedAssessmentDetailRepository.FindBy(m => m.NeedAssessmentHeader.NeedAssessment.PlanID == planID && m.Woreda == woredaID).FirstOrDefault();
           if (beneficiaryNo != null)
           {
               //var totalBeneficiaties = (int)(beneficiaryNo.PSNPFromWoredasMale + beneficiaryNo.PSNPFromWoredasFemale + beneficiaryNo.NonPSNPFromWoredasMale + beneficiaryNo.NonPSNPFromWoredasFemale);
               var totalBeneficiaties = (int)(beneficiaryNo.ProjectedMale + beneficiaryNo.ProjectedFemale + beneficiaryNo.NonPSNP);
               return totalBeneficiaties;
           }
           return 0;
       }
       public int GetNeedAssessmentMonthsFromPlan(int planID, int woredaID)
       {
           var months = _unitOfWork.NeedAssessmentDetailRepository.FindBy(w => w.NeedAssessmentHeader.NeedAssessment.PlanID == planID && w.Woreda == woredaID).SingleOrDefault();

           if (months != null)
           {

               if (months.NonPSNPFromWoredasDOA != null)
               {
                   var totalMonths = (int)(months.NonPSNPFromWoredasDOA);
                   return totalMonths;
               }
               else if (months.PSNPFromWoredasDOA != null)
               {
                   return (int)(months.PSNPFromWoredasDOA);
               }
               else return 0;

           }
           return 0;
       }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


