using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{
    public interface IUserRoleService:IDisposable
    {
       bool AddUserRole(UserRole entity);
       bool DeleteUserRole(UserRole entity);
       bool DeleteById(int id);
       bool EditUserRole(UserRole entity);
       UserRole FindById(int id);
       List<UserRole> GetAllUserRole();
       List<UserRole> Get(Expression<Func<UserRole, bool>> filter = null, Func<IQueryable<UserRole>, IOrderedQueryable<UserRole>> orderBy = null, string includeProperties = "");
       List<UserRole> FindBy(Expression<Func<UserRole, bool>> predicate);

       
        

    }
}


     


      
