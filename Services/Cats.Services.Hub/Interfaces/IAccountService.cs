using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{
   public interface  IAccountService:IDisposable
   {

       bool AddAccount(Account entity);
       bool EditAccount(Account entity);
       bool DeleteAccount(Account account);

       bool DeleteById(int id);

       List<Account> GetAllAccount();

        Account FindById(int id);

       List<Account> FindBy(Expression<Func<Account, bool>> predicate);




       int GetAccountId(string entityType, int entityId);

       int GetAccountIdWithCreate(string entityType, int entityId);
  
   }
}
