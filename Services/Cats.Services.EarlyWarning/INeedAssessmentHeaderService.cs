using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface INeedAssessmentHeaderService
    {

        bool AddNeedAssessmentHeader(NeedAssessmentHeader needAssessmentHeader);
        bool DeleteNeedAssessmentHeader(NeedAssessmentHeader needAssessmentHeader);
        bool DeleteById(int id);
        bool EditNeedAssessmentHeader(NeedAssessmentHeader needAssessmentHeader);
        NeedAssessmentHeader FindById(int id);
        List<NeedAssessmentHeader> GetAllNeedAssessmentHeader();
        List<NeedAssessmentHeader> FindBy(Expression<Func<NeedAssessmentHeader, bool>> predicate);

        IEnumerable<NeedAssessmentHeader> Get(
            Expression<Func<NeedAssessmentHeader, bool>> filter = null,
            Func<IQueryable<NeedAssessmentHeader>, IOrderedQueryable<NeedAssessmentHeader>> orderBy = null,
            string includeProperties = "");

    }
}


