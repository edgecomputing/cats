using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Helpers.Localization.Models;
using Helpers.Localization.Data.UnitWork;


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