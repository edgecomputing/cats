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
        bool Add(User user);
        bool Delete(User transporter);
        bool DeleteById(int id);
        bool Save(User transporter);
        User FindById(int id);
        List<User> GetAll();
        List<User> FindBy(Expression<Func<User, bool>> predicate);

        // User Account Business Logic
        bool Authenticate(User userInfo);
        bool Authenticate(string userName, string password);
        bool ChangePassword(string userName, string password);
        bool ChangePassword(User userInfo, string password);
        bool ChangePassword(int userId, string password);
        string ResetPassword(User userInfo);
        string ResetPassword(string userName);
        bool DisableAccount(string userName);

        // Utility methods
        string HashPassword(string password);
        User GetUserDetail(int userId);
        User GetUserDetail(string userName);
        UserInfo GetUserInfoDetail(string userName);
        string[] GetUserPermissions(int userId, string store, string application);

    }
}
