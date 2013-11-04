using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Helpers;

namespace Cats.ViewModelBinder
{
    public class HubAllocationViewModelBinder
    {
         public static List<RequisitionViewModel> ReturnRequisitionGroupByReuisitionNo(List<ReliefRequisition> requisition )
        {
            
            var result = (from req in requisition
                         select new RequisitionViewModel()
                                     {
                                                        RequisitionNo =req.RequisitionNo,
                                                        RequisitionId = req.RequisitionID,
                                                        RequisitionDate = DateTime.Parse(req.RequestedDate.ToString()),
                                                        Commodity = req.Commodity.Name,
                                                        BenficiaryNo = req.ReliefRequisitionDetails.Sum(a=>a.BenficiaryNo),
                                                        Amount = req.ReliefRequisitionDetails.Sum(a => a.Amount),
                                                        Status = int.Parse( req.Status.ToString()),
                                                        Region = req.AdminUnit.Name,
                                                        RegionId = (int) req.RegionID,
                                                        Zone = req.AdminUnit1.Name,
                                                       
                                                       
                                                        AmountAllocated = req.ReliefRequisitionDetails.Sum(a=>a.Amount),
                                                        StrRequisitionDate = req.RequestedDate.Value.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference())
                                     });
                                                   


            return Enumerable.Cast<RequisitionViewModel>(result).ToList();
          


        }
    }
}