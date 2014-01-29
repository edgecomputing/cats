using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface IReceiptAllocationService
    {
        bool AddReceiptAllocation(ReceiptAllocation receiptAllocation);
        bool DeleteReceiptAllocation(ReceiptAllocation receiptAllocation);
        bool DeleteById(int id);
        bool EditReceiptAllocation(ReceiptAllocation receiptAllocation);
        ReceiptAllocation FindById(int id);
        List<ReceiptAllocation> GetAllReceiptAllocation();
        List<ReceiptAllocation> FindBy(Expression<Func<ReceiptAllocation, bool>> predicate);
        ReceiptAllocation FindById(Guid id);

       List<ReceiptAllocation> FindBySINumber(string SINumber);    
       Transaction GetUncommitedAllocationTransaction(int CommodityID, int ShipingInstructionID, int HubID);
       bool DeleteByID(Guid id);
       ReceiptAllocation FindByID(Guid id);

        List<ReceiptAllocation> GetUnclosedAllocationsDetached(int hubId, int commoditySoureType, bool? closedToo,
                                                               string weightMeasurmentCode, int? CommodityType);
    }
}
