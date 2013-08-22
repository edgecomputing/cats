using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public partial interface INeedAssessmentService
    {

        bool AddNeedAssessment(NeedAssessment needAssessment);
        bool DeleteNeedAssessment(NeedAssessment needAssessment);
        bool DeleteById(int id);
        bool EditNeedAssessment(NeedAssessment needAssessment);
        NeedAssessment FindById(int id);
        List<NeedAssessment> GetAllNeedAssessment();
        List<NeedAssessment> FindBy(Expression<Func<NeedAssessment, bool>> predicate);
        IEnumerable<NeedAssessmentHeaderViewModel>  ReturnViewModel();

        IEnumerable<NeedAssessmentViewModel> ReturnNeedAssessmentHeaderViewModel(int region);
        IEnumerable<NeedAssessmentViewModel> ReturnNeedAssessmentDetailViewModel(int zone);
    }
}


