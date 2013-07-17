using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface ITransactionService
    {
        bool Addtransaction(Transaction transaction);
        bool EditTransaction(Transaction transaction);
        bool DeleteTransaction(Transaction transaction);
        bool DeleteById(int id);
        List<Transaction> GetAllTransaction();
        Transaction FindById(int id);
        List<Transaction> FindBy(Expression<Func<Transaction, bool>> predicate);

        IEnumerable<Transaction> Get(
            Expression<Func<Transaction, bool>> filter = null,
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderBy = null,
            string includeProperties = "");

        List<ProjectCode> getAllProjectByHubCommodity(int hubId, int commodityId);
        List<ShippingInstruction> getAllSIByHubCommodity(int hubId, int commodityId);
        List<ReceiptAllocation> getSIBalance(int hubId, int commodityId);
        List<ReceiptAllocation> getProjectBalance(int hubId, int commodityId);
    }
}
