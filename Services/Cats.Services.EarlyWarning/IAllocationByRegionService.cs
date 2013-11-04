using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IAllocationByRegionService
    {
        AllocationByRegion  FindById(int id);
        List<AllocationByRegion> GetAllAllocations();
        List<AllocationByRegion> FindBy(Expression<Func<AllocationByRegion, bool>> predicate);
    }
}
