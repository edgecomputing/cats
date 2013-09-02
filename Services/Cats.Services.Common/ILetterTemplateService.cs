using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Common
{
    public interface ILetterTemplateService
    {

        bool AddLetterTemplate(LetterTemplate_ letterTemplate);
        bool DeleteLetterTemplate(LetterTemplate_ letterTemplate);
        bool DeleteById(int id);
        bool EditLetterTemplate(LetterTemplate_ letterTemplate);
        LetterTemplate_ FindById(int id);
        List<LetterTemplate_> GetAllLetterTemplate();
        List<LetterTemplate_> FindBy(Expression<Func<LetterTemplate_, bool>> predicate);
        List<LetterTemplateViewModel> GetAllLetterTemplates();

    }
}


