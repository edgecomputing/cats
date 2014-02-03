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
        public static IEnumerable<DonationHeaderViewModel> GetReceiptHeaderPlanViewModel(List<ReceiptPlan> receiptPlans)
        {
            return receiptPlans.Select(plan => plan.ReceiptDate != null ? new DonationHeaderViewModel()
                                                                              {
                                                                                  ReceiptHeaderId = plan.ReceiptHeaderId,
                                                                                  SINumber = plan.GiftCertificateDetail.GiftCertificate.ShippingInstruction.Value,
                                                                                  Program = plan.GiftCertificateDetail.GiftCertificate.Program.Name,
                                                                                  ProgramId = plan.GiftCertificateDetail.GiftCertificate.ProgramID,
                                                                                  Donor = plan.GiftCertificateDetail.GiftCertificate.Donor.Name,
                                                                                  DonorId = plan.GiftCertificateDetail.GiftCertificate.DonorID,
                                                                                  Date = plan.ReceiptDate.Value.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference()),
                                                                                  Commodity = plan.GiftCertificateDetail.Commodity.Name,
                                                                                  CommodityId = plan.GiftCertificateDetail.CommodityID,
                                                       
                                                                              } : null);
        }

        public static IEnumerable<DonationDetailViewModel.DonationDetail> GetDonationDetailViewModel(List<ReceiptPlanDetail> receiptPlanDetails )
        {
            return receiptPlanDetails.Select(planDetail => new DonationDetailViewModel.DonationDetail()
                                                               {
                                                                   HubId = planDetail.HubId,
                                                                   Hub = planDetail.Hub.Name,
                                                                   Allocated = planDetail.Allocated,
                                                                   Received = planDetail.Received,
                                                                   Balance = planDetail.Balance
                                                               });
        }
    }
}