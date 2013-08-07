using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LanguageHelpers.Localization.Models;


namespace Helpers.Localization.Services
{
   public interface ILocalizedTextService
    {
      bool AddLocalizedText(LocalizedText item);
      bool UpdateLocalizedText(LocalizedText item);
      
      bool DeleteLocalizedText(LocalizedText item);
      bool DeleteById(int id);
      
      LocalizedText FindById(int id);
      List<LocalizedText> GetAllLocalizedText();
      List<LocalizedText> FindBy(Expression<Func<LocalizedText, bool>> predicate);
    }
}