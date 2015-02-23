using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Areas.Logistics.Models;
using Cats.Helpers;
using Cats.Models;

namespace Cats.ViewModelBinder
{
    public class ReceiptPlanViewModelBinder
    {
        public static IEnumerable<DonationHeaderViewModel> GetReceiptHeaderPlanViewModel(List<DonationPlanHeader> donationPlanHeaders)
        {
            return donationPlanHeaders.Select(plan => new DonationHeaderViewModel()
                                                          {
                                                              DonationHeaderPlanID = plan.DonationHeaderPlanID,
                                                              SINumber = plan.ShippingInstruction.Value,
                                                              ProgramName = plan.Program.Name,
                                                              ProgramID = plan.ProgramID,
                                                              DonorName = plan.Donor.Name,
                                                              DonorID = plan.DonorID,
                                                              DateOfAllocation = plan.AllocationDate.Value.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference()),
                                                              CommodityName = plan.Commodity.Name,
                                                              CommodityID = plan.CommodityID,
                                                              IsCommited = plan.IsCommited,
                                                             // Vessel = plan.Vessel,
                                                              StrETA = plan.ETA.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference()),
                                                             // ReferenceNo = plan.ReferenceNo,
                                                              //strEnteredBy  = plan.UserProfile.FirstName + " " + plan.UserProfile.LastName
                                                          });
        }

        //public static IEnumerable<DonationViewModel.DonationDetail> GetDonationDetailViewModel(List<Cats.Models.DonationPlanDetail> donationPlanDetails )
        //{
        //    return donationPlanDetails.Select(planDetail => new DonationViewModel.DonationDetail()
        //                                                       {
        //                                                           HubID = planDetail.HubID,
        //                                                           Hub = planDetail.Hub.Name,
        //                                                           AllocatedAmount = planDetail.AllocatedAmount,
        //                                                           ReceivedAmount = planDetail.ReceivedAmount,
        //                                                           Balance = planDetail.Balance
        //                                                       });
            
        //}
    }
}