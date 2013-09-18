
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;
using Cats.Models.Hub.ViewModels;
using Cats.Models.Hub.ViewModels.Dispatch;


namespace Cats.Services.Hub
{
    public interface IOtherDispatchAllocationService:IDisposable
    {

        bool AddOtherDispatchAllocation(OtherDispatchAllocation otherDispatchAllocation);
        bool DeleteOtherDispatchAllocation(OtherDispatchAllocation otherDispatchAllocation);
        bool DeleteById(int id);
        bool EditOtherDispatchAllocation(OtherDispatchAllocation otherDispatchAllocation);
        OtherDispatchAllocation FindById(int id);
        OtherDispatchAllocation FindById(Guid id);
        List<OtherDispatchAllocation> GetAllOtherDispatchAllocation();
        List<OtherDispatchAllocation> FindBy(Expression<Func<OtherDispatchAllocation, bool>> predicate);

        void Save(OtherDispatchAllocationViewModel model);


        OtherDispatchAllocationViewModel GetViewModelByID(Guid otherDispatchAllocationId);

        List<OtherDispatchAllocation> GetAllToCurrentOwnerHubs(UserProfile user);

        List<OtherDispatchAllocation> GetAllToOtherOwnerHubs(UserProfile user);

        List<OtherDispatchAllocationDto> GetCommitedLoanAllocationsDetached(UserProfile user, bool? closedToo, int? CommodityType);

        List<OtherDispatchAllocationDto> GetCommitedTransferAllocationsDetached(UserProfile user, bool? closedToo, int? CommodityType);

        void CloseById(Guid otherDispatchAllocationId);
    
    }
}


