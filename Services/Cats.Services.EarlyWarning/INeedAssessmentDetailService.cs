using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface INeedAssessmentDetailService
    {

        bool AddNeedAssessmentDetail(NeedAssessmentDetail needAssessmentDetail);
        bool DeleteNeedAssessmentDetail(NeedAssessmentDetail needAssessmentDetail);
        bool DeleteById(int id);
        bool EditNeedAssessmentDetail(NeedAssessmentDetail needAssessmentDetail);
        NeedAssessmentDetail FindById(int id);
        List<NeedAssessmentDetail> GetAllNeedAssessmentDetail();
        List<NeedAssessmentDetail> FindBy(Expression<Func<NeedAssessmentDetail, bool>> predicate);
        List<NeedAssessmentDetail> GetDraft();
        List<NeedAssessmentDetail> GetApproved();
        //int GetNeedAssessmentMonths(int year, int season, int woredaId);
        int GetNeedAssessmentBeneficiaryNo(int year, int season, int woredaID);
        int GetNeedAssessmentMonthsFromPlan(int planID,int woredaID);
        int GetNeedAssessmentBeneficiaryNoFromPlan(int planID, int woredaID);

    }
}

