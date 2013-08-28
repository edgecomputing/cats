using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface ITypeOfNeedAssessmentService
    {

        bool AddTypeOfNeedAssessment(TypeOfNeedAssessment typeOfNeedAssessment);
        bool DeleteTypeOfNeedAssessment(TypeOfNeedAssessment typeOfNeedAssessment);
        bool DeleteById(int id);
        bool EditTypeOfNeedAssessment(TypeOfNeedAssessment typeOfNeedAssessment);
        TypeOfNeedAssessment FindById(int id);
        List<TypeOfNeedAssessment> GetAllTypeOfNeedAssessment();
        List<TypeOfNeedAssessment> FindBy(Expression<Func<TypeOfNeedAssessment, bool>> predicate);


    }
}


