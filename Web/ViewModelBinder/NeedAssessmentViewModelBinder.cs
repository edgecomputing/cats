using System;
using System.Collections.Generic;
using System.Linq;
using Cats.Models;
using log4net;
using Cats.Data.UnitWork;
using Cats.Helpers;

namespace Cats.ViewModelBinder
{
    public  class NeedAssessmentViewModelBinder
    {
       

       


        public static IEnumerable<NeedAssessmentHeaderViewModel> ReturnViewModel()
        {
              IUnitOfWork _unitOfWork= new UnitOfWork();
            var needAssessment = _unitOfWork.NeedAssessmentRepository.Get(g => g.NeedAApproved == false); //featch unapproved need assessments
            return needAssessment.Select(need => new NeedAssessmentHeaderViewModel()
            {
                NeedAID = need.NeedAID,
                Region = need.Region,
                RegionName = need.AdminUnit.Name,
                Season = need.Season1.SeasonID,
                SeasonName = need.Season1.Name,
                NeedADate = (DateTime)need.NeedADate,
                NeedAApproved = need.NeedAApproved,
                NeedACreaterName = need.UserProfile1.UserName,
                NeedACreatedBy = need.NeddACreatedBy,
                TypeOfNeedAssessment = need.TypeOfNeedAssessment1.TypeOfNeedAssessmentID,
                Date = need.NeedADate.Value.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference())
            });
        }
        public static IEnumerable<NeedAssessmentHeaderViewModel> ReturnViewModelApproved()
        {
            IUnitOfWork _unitOfWork = new UnitOfWork();
            var needAssessment = _unitOfWork.NeedAssessmentRepository.Get(g => g.NeedAApproved == true); //featch unapproved need assessments
            return needAssessment.Select(need => new NeedAssessmentHeaderViewModel()
            {
                NeedAID = need.NeedAID,
                Region = need.Region,
                RegionName = need.AdminUnit.Name,
                Season = need.Season1.SeasonID,
                SeasonName = need.Season1.Name,
                NeedADate = (DateTime)need.NeedADate,
                NeedAApproved = need.NeedAApproved,
                NeedACreaterName = need.UserProfile1.UserName,
                NeedACreatedBy = need.NeddACreatedBy,
                TypeOfNeedAssessment = need.TypeOfNeedAssessment1.TypeOfNeedAssessmentID,
                Date = need.NeedADate.Value.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference())
            });
        }

        public static  IEnumerable<NeedAssessmentDao> ReturnNeedAssessmentHeaderViewModel(int regionId)
        {
            IUnitOfWork _unitOfWork = new UnitOfWork();
            List<NeedAssessmentHeader> needAssessment = _unitOfWork.NeedAssessmentHeaderRepository.Get(r => r.NeedAssessment.Region == regionId).ToList();

            return needAssessment.Select(need => need.NeedAssessment.NeedADate != null ? new NeedAssessmentDao()
            {
                NeedAID = (int)need.NeedAID,
                NAHeaderId = need.NAHeaderId,
                RegionId = need.NeedAssessment.Region,
                Region = need.NeedAssessment.AdminUnit.Name,
                Zone = need.AdminUnit.Name,

            } : null);
        }

        public static IEnumerable<NeedAssessmentWoredaDao> ReturnNeedAssessmentDetailViewModel(List<NeedAssessmentDetail> woredas)//,string season)
        {
           // IUnitOfWork _unitOfWork = new UnitOfWork();
           // var woredas = _unitOfWork.NeedAssessmentDetailRepository.FindBy(z => z.NeedAssessmentHeader.NeedAssessment.NeedAID == region);// .NeedAssessmentHeader.AdminUnit.ParentID == region);
            return woredas.Select(adminUnit => new NeedAssessmentWoredaDao
            {
                NAId = adminUnit.NAId,
                NeedAID = (int)adminUnit.NeedAId,
                WoredaName = adminUnit.AdminUnit.Name,
                Woreda = adminUnit.AdminUnit.AdminUnitID,
                Zone = (int)adminUnit.AdminUnit.ParentID,
                ZoneName = adminUnit.NeedAssessmentHeader.AdminUnit.Name,
                ProjectedMale = adminUnit.ProjectedMale,
                ProjectedFemale = adminUnit.ProjectedFemale,
                RegularPSNP = adminUnit.RegularPSNP,
                PSNP = adminUnit.PSNP,
                NonPSNP = adminUnit.NonPSNP,
                Contingencybudget = adminUnit.Contingencybudget,
                TotalBeneficiaries = adminUnit.TotalBeneficiaries,
                PSNPFromWoredasMale = adminUnit.PSNPFromWoredasMale,
                PSNPFromWoredasFemale = adminUnit.PSNPFromWoredasFemale,
                PSNPFromWoredasDOA = adminUnit.PSNPFromWoredasDOA,
                NonPSNPFromWoredasMale = adminUnit.NonPSNPFromWoredasMale,
                NonPSNPFromWoredasFemale = adminUnit.NonPSNPFromWoredasFemale,
                NonPSNPFromWoredasDOA = adminUnit.NonPSNPFromWoredasDOA
            });
        }

        public static IEnumerable<NeedAssessmentDetail> GetDetail(IEnumerable<NeedAssessmentViewModel> detailViewModel)
        {
            return detailViewModel.Select(viewModel => new NeedAssessmentDetail
            {
                NeedAId = viewModel.NeedAID,
                NAId = viewModel.NAId,
                Woreda = viewModel.Woreda,
                NeedAssessmentHeader = { Zone = viewModel.Zone },
                ProjectedMale = viewModel.ProjectedMale,
                ProjectedFemale = viewModel.ProjectedFemale,
                RegularPSNP = viewModel.RegularPSNP,
                PSNP = viewModel.PSNP,
                NonPSNP = viewModel.NonPSNP,
                Contingencybudget = viewModel.Contingencybudget,
                TotalBeneficiaries = viewModel.TotalBeneficiaries,
                PSNPFromWoredasMale = viewModel.PSNPFromWoredasMale,
                PSNPFromWoredasFemale = viewModel.PSNPFromWoredasFemale,
                PSNPFromWoredasDOA = viewModel.PSNPFromWoredasDOA,
                NonPSNPFromWoredasMale = viewModel.NonPSNPFromWoredasMale,
                NonPSNPFromWoredasFemale = viewModel.NonPSNPFromWoredasFemale,
                NonPSNPFromWoredasDOA = viewModel.NonPSNPFromWoredasDOA
            }).ToList();
        }
    }
}