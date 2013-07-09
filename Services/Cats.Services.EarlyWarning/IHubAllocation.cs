
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IHubAllocationService
    {

        bool AddHubAllocation(HubAllocation hubAllocation);
        bool DeleteHubAllocation(HubAllocation hubAllocation);
        bool DeleteById(int id);
        bool EditHubAllocation(HubAllocation hubAllocation);
        HubAllocation FindById(int id);
        List<HubAllocation> GetAllHubAllocation();
        List<HubAllocation> FindBy(Expression<Func<HubAllocation, bool>> predicate);

        bool UpdateRequisitionStatus(string requisitionNo);
    }
}


      
