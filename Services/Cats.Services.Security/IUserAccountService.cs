using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models.Security;
using Cats.Models.Security.ViewModels;

namespace Cats.Services.Security
{
    public interface IUserAccountService
    {
        // CRUD Operations
        bool Add(UserProfile entity, Dictionary<string, List<string>> roles);
        bool Add(UserProfile entity, string store, string application);
        bool Delete(UserProfile user);
        bool DeleteById(int id);
        bool Save(UserProfile user);
        UserProfile FindById(int id);
        List<UserProfile> GetAll();
        List<UserInfo> GetUsers();
        List<UserProfile> FindBy(Expression<Func<UserProfile, bool>> predicate);

        List<UserProfile> GetUserPreferences();

        // User Account Business Logic
        bool Authenticate(UserInfo userInfo);
        bool Authenticate(UserProfile userInfo);
        bool Authenticate(string userName, string password);
        bool ChangePassword(string userName, string password);
        bool ChangePassword(UserProfile userInfo, string password);
        bool ChangePassword(int userId, string password);
        string ResetPassword(UserInfo userInfo);
        string ResetPassword(string userName);
        bool DisableAccount(string userName);

        // Utility methods
        string HashPassword(string password);
        UserProfile GetUserDetail(int userId);
        UserProfile GetUserDetail(string userName);
        UserInfo GetUserInfo(string userName);
        UserInfo GetUserInfo(int userId);
        List<Role> GetUserPermissions(string UserName, string store, string application);

        string[] GetRoles(string application);
        List<Application> GetApplications(string store);
        List<Role> GetRolesList(string application);
        void EditUserRole(string owner, string userName, Dictionary<string, List<Role>> applications);
        List<Application> GetUserPermissions(string UserName);
    }
}
