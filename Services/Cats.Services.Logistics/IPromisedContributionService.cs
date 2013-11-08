using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Models.ViewModels;

namespace Cats.Services.Logistics
{
    public interface IPromisedContributionService : IDisposable
    {
            bool Create(PromisedContribution promisedContribution);

            PromisedContribution FindById(int id);
            List<PromisedContribution> GetAll();
            List<PromisedContribution> FindBy(Expression<Func<PromisedContribution, bool>> predicate);

            bool Update(PromisedContribution promisedContribution);

            bool Delete(PromisedContribution promisedContribution);
            bool DeleteById(int id);
    }
}


        