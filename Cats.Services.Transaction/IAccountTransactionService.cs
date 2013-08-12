using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Transaction
{
    public interface IAccountTransactionService
    {
        bool AddAccountTransaction(AccountTransaction item);
        bool UpdateAccountTransaction(AccountTransaction item);

        bool DeleteAccountTransaction(AccountTransaction item);
        bool DeleteById(Guid id);

        AccountTransaction FindById(Guid id);
        List<AccountTransaction> GetAllAccountTransaction();
        List<AccountTransaction> FindBy(Expression<Func<AccountTransaction, bool>> predicate);
        List<AccountTransaction> PostPSNPPlan(RegionalPSNPPlan plan, Ration ration);
    }
}