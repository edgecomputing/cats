using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Areas.Logistics.Models;

namespace Cats.ViewModelBinder
{
    public class ReceiptAllocationViewModelBinder
    {
        public static IEnumerable<ReceiptAllocationViewModel> BindReceiptAllocationViewModel(List<Cats.Models.Hubs.ReceiptAllocation> receiptAllocations )
        {
            return receiptAllocations.Select(s => new ReceiptAllocationViewModel()
                                                      {
                                                          QuantityInMT = s.QuantityInMT,
                                                          ReceiptAllocationID = s.ReceiptAllocationID,
                                                          ProjectNumber = s.ProjectNumber,
                                                          ReceivedQuantity = s.ReceivedQuantityInMT,
                                                          AllocatedQuantity = s.QuantityInMT,
                                                          Balance = s.RemainingBalanceInMT,
                                                          CommodityName = s.Commodity.Name,
                                                          SINumber = s.SINumber,
                                                          Hub = s.Hub.Name
                                                      });
        }
    }
}