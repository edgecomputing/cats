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
            var requests = _eWDashboardService.FindByRequest(m => m.PlanID == currentHrd.PlanID && m.ProgramId==1);
            var requestDetail = GetRecentRegionalRequests(requests);

            return Json(requestDetail, JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<RegionalRequestViewModel> GetRecentRegionalRequests(IEnumerable<RegionalRequest> regionalRequests)
        {
            return (from regionalRequest in regionalRequests
                    where regionalRequest.Status==(int)RegionalRequestStatus.Draft && regionalRequest.ProgramId==1// only for relief program
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

                        });
        }
        public JsonResult GetRequisition()
        {
            var currentHrd = _eWDashboardService.FindByHrd(m => m.Status == 3).FirstOrDefault();
            var requests = _eWDashboardService.FindByRequest(m => m.PlanID == currentHrd.PlanID);
            var requisitions = GetRequisisition(requests);
            return Json(requisitions, JsonRequestBehavior.AllowGet);

        }
        private IEnumerable<ReliefRequisitionInfoViewModel> GetRequisisition(IEnumerable<RegionalRequest> requests)
        {
            var reliefRequisitions = _eWDashboardService.GetAllReliefRequisition();
            return (from reliefRequisition in reliefRequisitions
                    from request in requests
                    where reliefRequisition.RegionalRequestID == request.RegionalRequestID
                    select new ReliefRequisitionInfoViewModel()
                        {
                            RequisitonNumber = reliefRequisition.RequisitionNo,
                            Commodity = reliefRequisition.Commodity.Name,
                            Zone = reliefRequisition.AdminUnit.AdminUnit2.Name,
                            Beneficiary = reliefRequisition.ReliefRequisitionDetails.Sum(m=>m.BenficiaryNo),
                            Amount = reliefRequisition.ReliefRequisitionDetails.Sum(m=>m.Amount),
                            Status =_eWDashboardService.GetStatusName(WORKFLOW.RELIEF_REQUISITION,
                                                                  reliefRequisition.Status.Value)


                        });
        }
        //public JsonResult GetRequestedInfo(int planID)
        //{
        //    var request = _regionalRequestService.FindBy(m => m.PlanID == planID);
        //    var requestDetail = GetRquestDetailViewModel(request);
        //    return Json(requestDetail, JsonRequestBehavior.AllowGet);
        //}
        //private IEnumerable<RegionalRequestInfoViewModel> GetRquestDetailViewModel(IEnumerable<RegionalRequest> regionalRequests)
        //{
        //    var requestCount = (from regionalRequest in regionalRequests
        //                        group regionalRequest by regionalRequest.RegionID
        //                            into regionalGroup
        //                            select new
        //                                {
        //                                    Region = regionalGroup.Key,
        //                                    totalRequest = regionalGroup.Count()
        //                                });



        //}
    }
}
