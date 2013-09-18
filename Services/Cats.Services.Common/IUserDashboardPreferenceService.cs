using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Common
{
    public interface IUserDashboardPreferenceService : IDisposable
    {
        bool AddUserDashboardPreference(UserDashboardPreference userDashboardPreference);
        bool DeleteUserDashboardPreference(UserDashboardPreference userDashboardPreference);
        bool DeleteById(int id);
        bool EditUserDashboardPreference(UserDashboardPreference userDashboardPreference);
        UserDashboardPreference FindById(int id);
        List<UserDashboardPreference> GetAllUserDashboardPreference();
        List<UserDashboardPreference> FindBy(Expression<Func<UserDashboardPreference, bool>> predicate);
        IEnumerable<UserDashboardPreference> Get(
                   Expression<Func<UserDashboardPreference, bool>> filter = null,
                   Func<IQueryable<UserDashboardPreference>, IOrderedQueryable<UserDashboardPreference>> orderBy = null,
                   string includeProperties = "");
    }
}
