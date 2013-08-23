
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;
namespace Cats.Services.EarlyWarning
{
    public interface IBusinessProcessStateService
    {
         bool Add(BusinessProcessState item);
         bool Update(BusinessProcessState item);
         bool Delete(BusinessProcessState item);
         bool DeleteById(int id);
         BusinessProcessState FindById(int id);
         List<BusinessProcessState> GetAll();
         List<BusinessProcessState> FindBy(Expression<Func<BusinessProcessState, bool>> predicate);
    }
}