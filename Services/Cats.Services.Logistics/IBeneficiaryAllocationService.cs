using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models.ViewModels;

namespace Cats.Services.Logistics
{
   public interface IBeneficiaryAllocationService
   {

       IEnumerable<BeneficiaryAllocation> GetBenficiaryAllocation(Expression<Func<BeneficiaryAllocation, bool>> predicate = null);

   }
}
