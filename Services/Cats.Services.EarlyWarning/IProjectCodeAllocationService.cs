using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Data; 

namespace Cats.Services.EarlyWarning
{
    public interface IProjectCodeAllocationService
    {
        bool AddProjectCodeAllocationDetail(ProjectCodeAllocation _ProjectCodeAllocationDetail);

        bool EditProjectCodeAllocationDetail(ProjectCodeAllocation _ProjectCodeAllocationDetail);

        bool DeleteProjectCodeAllocationDetail(ProjectCodeAllocation _ProjectCodeAllocationDetail);
        
        bool DeleteById(int id);
        
        bool Save();

        bool Save(ProjectCodeAllocation _ProjectAllocation);

        IEnumerable<ProjectCodeAllocation> GetAllProjectCodeAllocationDetail();

        ProjectCodeAllocation FindById(int id);

        IEnumerable<ProjectCodeAllocation> FindBy(Expression<Func<ProjectCodeAllocation, bool>> predicate);

        bool SaveProjectCodeAllocation(IEnumerable<ProjectCodeAllocation> projectAllocations);
        List<HubAllocation> GetHubAllocation(Expression<Func<HubAllocation, bool>> predicate);
        List<HubAllocation> GetAllRequisitionsInHubAllocation();
    }
}
