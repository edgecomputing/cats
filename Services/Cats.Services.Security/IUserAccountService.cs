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
        bool Add(User user);
        bool Delete(User transporter);
        bool DeleteById(int id);
        bool Save(User transporter);
        User FindById(int id);
        List<User> GetAll();
        List<User> FindBy(Expression<Func<User, bool>> predicate);
    }
}
