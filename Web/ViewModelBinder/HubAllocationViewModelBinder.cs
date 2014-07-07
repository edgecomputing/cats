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
            if (requisition==null)
                return  new List<RequisitionViewModel>();
            

             var result = (from req in requisition
                         select new RequisitionViewModel()
                                     {
                                        RequisitionNo =req.RequisitionNo,
                                        RequisitionId = req.RequisitionID,
                                        RequisitionDate = DateTime.Parse(req.RequestedDate.ToString()),
                                        Commodity = req.Commodity.Name,
                                        BenficiaryNo = req.ReliefRequisitionDetails.Sum(a=>a.BenficiaryNo),
                                        Amount = req.ReliefRequisitionDetails.Sum(a => a.Amount).ToPreferedWeightUnit(),
                                        Status = int.Parse( req.Status.ToString()),
                                        Region = req.AdminUnit.Name,
                                        RegionId = (int) req.RegionID,
                                        Zone = req.AdminUnit1.Name,
                                        ProgramID = req.ProgramID,
                                        Program = req.Program.Name,
                                        Round = req.Round.ToString(),
                                        AmountAllocated = req.ReliefRequisitionDetails.Sum(a=>a.Amount),
                                        StrRequisitionDate = req.RequestedDate.Value.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference())
                                      });

             var r = new List<RequisitionViewModel>();
             
             foreach (var req in requisition)
             {
                 var n = new RequisitionViewModel();
                 n.RequisitionNo = req.RequisitionNo;
                 n.RequisitionId = req.RequisitionID;
                 n.RequisitionDate = DateTime.Parse(req.RequestedDate.ToString());
                 n.Commodity = req.Commodity.Name;
                 n.BenficiaryNo = req.ReliefRequisitionDetails.Sum(a => a.BenficiaryNo);
                 var m = req.ReliefRequisitionDetails.Sum(a => a.Amount);
                 n.Amount = m.ToPreferedWeightUnit();
                 n.Status = int.Parse(req.Status.ToString());
                 n.Region = req.AdminUnit.Name;
                 n.RegionId = (int)req.RegionID;
                 n.Zone = req.AdminUnit1.Name;

                 n.AmountAllocated = req.ReliefRequisitionDetails.Sum(a => a.Amount);
                 n.StrRequisitionDate = req.RequestedDate.Value.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference());

                 r.Add(n);
             }

            return r.ToList();
          


        }
    }
}