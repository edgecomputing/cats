
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
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


    }
}


