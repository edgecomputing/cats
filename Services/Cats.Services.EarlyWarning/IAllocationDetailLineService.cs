
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Models;

namespace DRMFSS.BLL.Services
{
    public interface IAllocationDetailLineService
    {

        bool AddAllocationDetailLine(AllocationDetailLine Entity);
        bool DeleteAllocationDetailLine(AllocationDetailLine Entity);
        bool DeleteById(int id);
        bool EditAllocationDetailLine(AllocationDetailLine Entity);
        AllocationDetailLine FindById(int id);
        List<AllocationDetailLine> GetAllAllocationDetailLine();
        List<AllocationDetailLine> FindBy(Expression<Func<AllocationDetailLine, bool>> predicate);


    }
}


