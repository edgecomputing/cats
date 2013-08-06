using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LanguageHelpers.Localization.Models;

namespace LanguageHelpers.Localization.Services
{
    public interface ILanguageService
    {
        bool AddLanguage(Language language);
        bool DeleteLanguage(Language language);
        bool DeleteById(int id);
        bool EditLanguage(Language language);
        Language FindById(int id);
        List<Language> GetAllLanguage();
        List<Language> FindBy(Expression<Func<Language, bool>> predicate);

        IEnumerable<Language> Get(
             Expression<Func<Language, bool>> filter = null,
             Func<IQueryable<Language>, IOrderedQueryable<Language>> orderBy = null,
             string includeProperties = "");

        bool Save();
    }
}
