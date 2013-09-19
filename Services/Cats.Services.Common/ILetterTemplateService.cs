using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Common
{
    public interface ILetterTemplateService
    {

        bool AddLetterTemplate(LetterTemplate letterTemplate);
        bool DeleteLetterTemplate(LetterTemplate letterTemplate);
        bool DeleteById(int id);
        bool EditLetterTemplate(LetterTemplate letterTemplate);
        LetterTemplate FindById(int id);
        List<LetterTemplate> GetAllLetterTemplate();
        List<LetterTemplate> FindBy(Expression<Func<LetterTemplate, bool>> predicate);
        List<LetterTemplateViewModel> GetAllLetterTemplates();

    }
}


