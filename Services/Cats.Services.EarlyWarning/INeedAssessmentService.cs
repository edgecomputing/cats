using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface INeedAssessmentService
    {

        bool AddNeedAssessment(NeedAssement needAssessment);
        bool DeleteNeedAssessment(NeedAssement needAssessment);
        bool DeleteById(int id);
        bool EditNeedAssessment(NeedAssement needAssessment);
        NeedAssement FindById(int id);
        List<NeedAssement> GetAllNeedAssessment();
        List<NeedAssement> FindBy(Expression<Func<NeedAssement, bool>> predicate);


    }
}


