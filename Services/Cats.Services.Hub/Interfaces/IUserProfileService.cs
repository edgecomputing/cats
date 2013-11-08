using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{
   public  interface IUserProfileService:IDisposable
    {

       bool AddUserProfile(UserProfile entity);
       bool DeleteUserProfile(UserProfile entity);
       bool DeleteById(int id);
       bool EditUserProfile(UserProfile entity);
       UserProfile FindById(int id);
       List<UserProfile> GetAllUserProfile();
       List<UserProfile> Get(Expression<Func<UserProfile, bool>> filter = null, Func<IQueryable<UserProfile>, IOrderedQueryable<UserProfile>> orderBy = null, string includeProperties = "");
       List<UserProfile> FindBy(Expression<Func<UserProfile, bool>> predicate);

       bool ChangePassword(int profileId, string password);
       UserProfile GetUser(string userName);
       bool EditInfo(UserProfile profile);

    }
}


     


      
    