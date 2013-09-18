using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models.Security;

namespace Cats.Services.Security
{
   public interface ISettingService:IDisposable
    {
        bool AddSetting(Setting setting);
        bool DeleteSetting(Setting setting);
        bool DeleteById(int id);
        bool EditSetting(Setting setting);
        Setting FindById(int id);
        List<Setting> GetAllSetting();
        List<Setting> FindBy(Expression<Func<Setting, bool>> predicate);

        string GetSettingValue(string Key);
    }
}
