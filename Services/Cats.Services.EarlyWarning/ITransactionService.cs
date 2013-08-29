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
        bool Addtransaction(Cats.Models.Transaction transaction);
        bool EditTransaction(Cats.Models.Transaction transaction);
        bool DeleteTransaction(Cats.Models.Transaction transaction);
        bool DeleteById(int id);
        List<Cats.Models.Transaction> GetAllTransaction();
        Cats.Models.Transaction FindById(int id);
        List<Cats.Models.Transaction> FindBy(Expression<Func<Cats.Models.Transaction, bool>> predicate);

        IEnumerable<Cats.Models.Transaction> Get(
            Expression<Func<Cats.Models.Transaction, bool>> filter = null,
            Func<IQueryable<Cats.Models.Transaction>, IOrderedQueryable<Cats.Models.Transaction>> orderBy = null,
            string includeProperties = "");

        List<ProjectCode> getAllProjectByHubCommodity(int hubId, int commodityId);
        List<ShippingInstruction> getAllSIByHubCommodity(int hubId, int commodityId);
        List<ReceiptAllocation> getSIBalance(int hubId, int commodityId);
        List<ReceiptAllocation> getProjectBalance(int hubId, int commodityId);
    }
}
