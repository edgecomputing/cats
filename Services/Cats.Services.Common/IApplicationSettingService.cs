using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Common
{
    public interface IApplicationSettingService
    {
        bool AddApplicationSetting(ApplicationSetting item);
        bool UpdateApplicationSetting(ApplicationSetting item);

        bool DeleteApplicationSetting(ApplicationSetting item);
        bool DeleteById(int id);
        int getPSNPWorkflow();
        int getDefaultRation();
        ApplicationSetting FindById(int id);
        List<ApplicationSetting> GetAllApplicationSetting();
        List<ApplicationSetting> FindBy(Expression<Func<ApplicationSetting, bool>> predicate);
        void SetValue(string name, string value);
        string FindValue(string name);
        
    }
}