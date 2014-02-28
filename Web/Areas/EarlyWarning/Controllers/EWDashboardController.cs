using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Dashboard;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class EWDashboardController : Controller
    {

        private readonly IEWDashboardService _eWDashboardService;
        public EWDashboardController(IEWDashboardService ewDashboardService)
        {
            _eWDashboardService = ewDashboardService;

        }

        public JsonResult GetRation()
        {
            var currentHrd = _eWDashboardService.FindByHrd(m => m.Status == 3).FirstOrDefault();

            var rationDetail = _eWDashboardService.FindByRationDetail(m => m.RationID == currentHrd.RationID);
            var rationDetailInfo = GetRationDetailInfo(rationDetail);

            return Json(rationDetailInfo, JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<RationDetailViewModel>  GetRationDetailInfo(IEnumerable<RationDetail> rationDetails)
        {
            return (from rationDetail in rationDetails
                    select new RationDetailViewModel()
                        {
                            Commodity = rationDetail.Commodity.Name,
                            Amount = rationDetail.Amount
                        });
        }

        public JsonResult GetRegionalRequests()
        {

            var currentHrd = _eWDashboardService.FindByHrd(m => m.Status == 3).FirstOrDefault();
            var requests = _eWDashboardService.FindByRequest(m => m.PlanID == currentHrd.PlanID && m.ProgramId==1).OrderByDescending(m=>m.RegionalRequestID);
            var requestDetail = GetRecentRegionalRequests(requests);

            return Json(requestDetail, JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<RegionalRequestViewModel> GetRecentRegionalRequests(IEnumerable<RegionalRequest> regionalRequests)
        {
            return (from regionalRequest in regionalRequests
                    where  regionalRequest.ProgramId==1// only for relief program
                   // from requestDetail in regionalRequest.RegionalRequestDetails
                    select new RegionalRequestViewModel()
                        {
                            Region = regionalRequest.AdminUnit.Name,
                            Round = regionalRequest.Round,
                            MonthName = RequestHelper.MonthName(regionalRequest.Month),
                            StatusID = regionalRequest.Status,
                            Beneficiary = regionalRequest.RegionalRequestDetails.Sum(m=>m.Beneficiaries),
                            NumberOfFDPS = regionalRequest.RegionalRequestDetails.Count(),
                            Status = _eWDashboardService.GetStatusName(WORKFLOW.REGIONAL_REQUEST, regionalRequest.Status)

                        }).Take(5);
        }
        public JsonResult GetRequisition()
        {
            var currentHrd = _eWDashboardService.FindByHrd(m => m.Status == 3).FirstOrDefault();
            var requests = _eWDashboardService.FindByRequest(m => m.PlanID == currentHrd.PlanID).OrderByDescending(m=>m.RegionalRequestID);
            var requisitions = GetRequisisition(requests);
            return Json(requisitions, JsonRequestBehavior.AllowGet);

        }
        private IEnumerable<ReliefRequisitionInfoViewModel> GetRequisisition(IEnumerable<RegionalRequest> requests)
        {
            var reliefRequisitions = _eWDashboardService.GetAllReliefRequisition();
            return (from reliefRequisition in reliefRequisitions
                    from request in requests
                    where reliefRequisition.RegionalRequestID == request.RegionalRequestID && reliefRequisition.Status==(int)ReliefRequisitionStatus.Draft 
                    select new ReliefRequisitionInfoViewModel()
                        {
                            RequisitonNumber = reliefRequisition.RequisitionNo,
                            Region = reliefRequisition.AdminUnit.Name,
                            Zone = reliefRequisition.AdminUnit1.Name,
                            Commodity = reliefRequisition.Commodity.Name,
                            Beneficiary = reliefRequisition.ReliefRequisitionDetails.Sum(m=>m.BenficiaryNo),
                            Amount = reliefRequisition.ReliefRequisitionDetails.Sum(m=>m.Amount),
                            Status =_eWDashboardService.GetStatusName(WORKFLOW.RELIEF_REQUISITION,
                                                                  reliefRequisition.Status.Value)


                        }).Take(5);
        }
        public JsonResult GetRequestedInfo()
        {
            var currentHrd = _eWDashboardService.FindByHrd(m => m.Status == 3).FirstOrDefault();
            var request = _eWDashboardService.FindByRequest(m => m.PlanID == currentHrd.PlanID).OrderByDescending(m=>m.RegionalRequestID);
            var requestDetail = GetRquestDetailViewModel(request);
            return Json(requestDetail, JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<RegionalRequestInfoViewModel> GetRquestDetailViewModel(IEnumerable<RegionalRequest> regionalRequests)
        {
            var request = (from regionalRequest in regionalRequests
                           select new RegionalRequestInfoViewModel
                               {
                                   RegionID = regionalRequest.RegionID,
                                   RegionName = regionalRequest.AdminUnit.Name,
                                   NoOfRequests =
                                       _eWDashboardService.FindByRequest(m => m.RegionID == regionalRequest.RegionID
                                                                              && m.PlanID == regionalRequest.PlanID).
                               Count,
                                   // Remaining = _eWDashboardService.GetRemainingRequest(regionalRequest.RegionID,regionalRequest.PlanID)


                               });
            var distinictRequest = request.GroupBy(m=>m.RegionID,(key, group) => group.First()).ToList();
            return distinictRequest;

        }
        public JsonResult GetStatusInPercentage()
        {
            var hrd = GetCurrentHrd();
            var request = _eWDashboardService.FindByRequest(m => m.PlanID == hrd.PlanID);
            decimal draft = request.Count(m => m.Status == (int) RegionalRequestStatus.Draft);
            decimal approved = request.Count(m => m.Status == (int) RegionalRequestStatus.Approved);
            decimal closed = request.Count(m => m.Status == (int) RegionalRequestStatus.Closed);
            var percentage = new RequestPercentageViewModel
                {
                    Pending = ((draft)/(request.Count))*100,
                    Approved = (approved/request.Count)*100,
                    RequisitionCreated = (closed/request.Count)*100
                };
            return Json(percentage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRequisitionStatusPercentage()
        {
            var currentHrd = _eWDashboardService.FindByHrd(m => m.Status == 3).FirstOrDefault();
            var requests = _eWDashboardService.FindByRequest(m => m.PlanID == currentHrd.PlanID).OrderByDescending(m => m.RegionalRequestID);
            var allRequisitions = _eWDashboardService.GetAllReliefRequisition();

            var requisitons = (from requisiton in allRequisitions
                               from request in requests
                               where requisiton.RegionalRequestID == request.RegionalRequestID
                               select new
                                   {
                                       requisiton.Status
                                   }).ToList();

            decimal draft = requisitons.Count(m => m.Status == (int) ReliefRequisitionStatus.Draft);
            decimal approved = requisitons.Count(m => m.Status == (int)ReliefRequisitionStatus.Approved);
            decimal hubAssigned = requisitons.Count(m => m.Status == (int)ReliefRequisitionStatus.HubAssigned);
            decimal pcAssigned = requisitons.Count(m => m.Status == (int)ReliefRequisitionStatus.ProjectCodeAssigned);
            decimal transportRequisitionCreated = requisitons.Count(m => m.Status == (int)ReliefRequisitionStatus.TransportRequisitionCreated);
            decimal transportOrderCreated=requisitons.Count(m => m.Status == (int) ReliefRequisitionStatus.TransportOrderCreated);

            var requisitionStatusPercentage = new RequisitionStatusPercentage()
                {
                    Pending = (draft/requisitons.Count)*100,
                    Approved = (approved/requisitons.Count)*100,
                    HubAssigned = (hubAssigned/requisitons.Count)*100,
                    ProjectCodeAssigned = (pcAssigned/requisitons.Count)*100,
                    TransportRequistionCreated = (transportRequisitionCreated/requisitons.Count)*100,
                    TransportOrderCreated = (transportOrderCreated/requisitons.Count)*100,
                    NoOfDraft = (int)draft,
                    NoOfApproved = (int)approved,
                    NoHubAssigned = (int)hubAssigned,
                    NoOfPcAssigned = (int)pcAssigned,
                    NoOfTransportReqCreated = (int)transportRequisitionCreated,
                    NoOfTransportOrderCreated = (int)transportOrderCreated

                };
            return Json(requisitionStatusPercentage, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetHrdRegionPercentage()
        {
            var currentHrd = _eWDashboardService.FindByHrd(m => m.Status == 3).FirstOrDefault();
            IEnumerable<RegionalTotalViewModel> regionalSummery = new List<RegionalTotalViewModel>(); 
            if (currentHrd != null)
            {

                var regionGroup = from detail in currentHrd.HRDDetails
                                     group detail by detail.AdminUnit.AdminUnit2.AdminUnit2
                                     into regionalDetail
                                     select new
                                         {
                                             Region = regionalDetail.Key,
                                             NumberOfBeneficiaries = regionalDetail.Sum(m => m.NumberOfBeneficiaries)     
                                         };
                regionalSummery= (from total in regionGroup
                        select new RegionalTotalViewModel
                        {
                            RegionName = total.Region.Name,
                            TotalBeneficary = total.NumberOfBeneficiaries,
                         
                        });
                decimal totalNationalBeneficiary = regionalSummery.Sum(m => m.TotalBeneficary);
                regionalSummery = (from regionalTotalViewModel in regionalSummery
                                   where regionalTotalViewModel.TotalBeneficary>0
                                   select new RegionalTotalViewModel()
                                       {
                                           RegionName = regionalTotalViewModel.RegionName,
                                           TotalBeneficary = regionalTotalViewModel.TotalBeneficary,
                                           BeneficiaryPercentage =(regionalTotalViewModel.TotalBeneficary/totalNationalBeneficiary)*100

                                       }).OrderByDescending(m=>m.TotalBeneficary);


            }
            return Json(regionalSummery, JsonRequestBehavior.AllowGet);
        }
        private HRD GetCurrentHrd()
        {
            return _eWDashboardService.FindByHrd(m => m.Status == 3).FirstOrDefault();
        }
        public JsonResult GetRecentGiftCertificates()
        {
            var draftGiftCertificate = _eWDashboardService.GetAllGiftCertificate().Where(m => m.StatusID == 1);

            var giftCertificate = GetGiftCertificate(draftGiftCertificate);

            return Json(giftCertificate, JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<GiftCertificateViewModel> GetGiftCertificate(IEnumerable<Cats.Models.GiftCertificate> giftCertificates)
        {
            return (from giftCertificate in giftCertificates
                    select new GiftCertificateViewModel()
                        {
                            DonorName = giftCertificate.Donor.Name,
                            SINumber = giftCertificate.ShippingInstruction.Value,
                            DclarationNumber = giftCertificate.DeclarationNumber,
                            Status = "Draft"
                            // Commodity = giftCertificate.GiftCertificateDetails.FirstOrDefault().Commodity.Name
                        }).Take(5);


        }
    }
}
