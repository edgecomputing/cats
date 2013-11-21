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
    public interface ISIPCAllocationService : IDisposable
    {
        bool Create(SIPCAllocation allocation);

        SIPCAllocation FindById(int id);
        List<SIPCAllocation> GetAll();
        List<SIPCAllocation> FindBy(Expression<Func<SIPCAllocation, bool>> predicate);

        bool Update(SIPCAllocation allocation);

        bool Delete(SIPCAllocation allocation);
        bool DeleteById(int id);
    }
}
