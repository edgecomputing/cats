
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;

namespace Cats.Services.Hub
{
    public interface IHubSettingValueService
    {

        bool AddHubSettingValue(HubSettingValue hubSettingValue);
        bool DeleteHubSettingValue(HubSettingValue hubSettingValue);
        bool DeleteById(int id);
        bool EditHubSettingValue(HubSettingValue hubSettingValue);
        HubSettingValue FindById(int id);
        List<HubSettingValue> GetAllHubSettingValue();
        List<HubSettingValue> FindBy(Expression<Func<HubSettingValue, bool>> predicate);


    }
}


