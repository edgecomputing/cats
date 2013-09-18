using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;

namespace Cats.Services.Hub
{
    public interface ITransactionGroupService
    {

        bool AddTransactionGroup(TransactionGroup entity);
        bool DeleteTransactionGroup(TransactionGroup entity);
        bool DeleteById(int id);
        bool EditTransactionGroup(TransactionGroup entity);
        TransactionGroup FindById(int id);
        List<TransactionGroup> GetAllTransactionGroup();
        List<TransactionGroup> FindBy(Expression<Func<TransactionGroup, bool>> predicate);

        Guid GetLastTrasactionGroupId();
       

    }
}


      


      