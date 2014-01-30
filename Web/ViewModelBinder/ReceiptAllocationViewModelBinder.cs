using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Areas.Logistics.Models;

namespace Cats.ViewModelBinder
{
    public class ReceiptAllocationViewModelBinder
    {
        public static IEnumerable<ReceiptAllocationViewModel> BindReceiptAllocationViewModel(List<Cats.Models.ReceiptAllocation> receiptAllocations )
        {
            return receiptAllocations.Select(s => new ReceiptAllocationViewModel()
                                                      {
                                                          QuantityInMT = s.QuantityInMT,
                                                          QuantityInUnit = (decimal) s.QuantityInUnit,
                                                          ReceiptAllocationID = s.ReceiptAllocationID,
                                                          ProjectNumber = s.ProjectNumber
                                                          
                                                      });
        }
    }
}