using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Areas.Logistics.Models;
using Cats.Models;

namespace Cats.ViewModelBinder
{
    public class ReceiptPlaniewModelBinder
    {
        public static IEnumerable<DonationHeaderViewModel> GetReceiptHeaderPlanViewModel(List<ReceiptPlan> receiptPlans)
        {
            return receiptPlans.Select(plan => new DonationHeaderViewModel()
                                                   {
                                                       SINumber = plan.GiftCertificateDetail.GiftCertificate.ShippingInstruction.Value,
                                                       Program = plan.GiftCertificateDetail.GiftCertificate.Program.Name,
                                                       ProgramId = plan.GiftCertificateDetail.GiftCertificate.ProgramID,
                                                       Donor = plan.GiftCertificateDetail.GiftCertificate.Donor.Name,
                                                       DonorId = plan.GiftCertificateDetail.GiftCertificate.DonorID,
                                                       ExpiryDate = (DateTime) plan.GiftCertificateDetail.ExpiryDate,
                                                       Commodity = plan.GiftCertificateDetail.Commodity.Name,
                                                       ComoodityId = plan.GiftCertificateDetail.CommodityID
                                                   });
        }

        public static IEnumerable<DonationDetailViewModel> GetDonationDetailViewModel(List<ReceiptPlanDetail> receiptPlanDetails )
        {
            return receiptPlanDetails.Select(planDetail => new DonationDetailViewModel()
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