using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models.Security;

namespace Cats.Services.Security
{
    public interface IUserAccountService
    {
        // CRUD Operations
        bool Add(UserAccount user);
        bool Delete(UserAccount user);
        bool DeleteById(int id);
        bool Save(UserAccount user);
        UserAccount FindById(int id);
        List<UserAccount> GetAll();
        List<UserAccount> FindBy(Expression<Func<UserAccount, bool>> predicate);

        // User Account Business Logic
        bool Authenticate(UserInfo userInfo);
        bool Authenticate(UserAccount userInfo);
        bool Authenticate(string userName, string password);
        bool ChangePassword(string userName, string password);
        bool ChangePassword(UserAccount userInfo, string password);
        bool ChangePassword(int userId, string password);
        string ResetPassword(UserInfo userInfo);
        string ResetPassword(string userName);
        bool DisableAccount(string userName);

        // Utility methods
        string HashPassword(string password);
        UserAccount GetUserDetail(int userId);
        UserAccount GetUserDetail(string userName);
        UserInfo GetUserInfo(string userName);
        string[] GetUserPermissions(int userId, string store, string application);

    }
}
