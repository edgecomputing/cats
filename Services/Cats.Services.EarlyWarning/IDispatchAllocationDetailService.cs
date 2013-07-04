using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Data;

namespace Cats.Services.EarlyWarning
{
    public interface IDispatchAllocationDetailService
    {
        bool AddDispatchDetail(DispatchAllocation _dispatchAllocationDetail);

        bool EditDispatchDetail(DispatchAllocation _dispatchAllocationDetail);

        bool DeleteDispatchDetail(DispatchAllocation _dispatchAllocationDetail);
        
        bool DeleteById(int id);
        
        bool Save();

        List<DispatchAllocation> GetAllDispatchAllocationDetail();

        DispatchAllocation FindById(Guid id);

        IEnumerable<DispatchAllocation> FindBy(Expression<Func<DispatchAllocation, bool>> predicate);
        
    }
}
