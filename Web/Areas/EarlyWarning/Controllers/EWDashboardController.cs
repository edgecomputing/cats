using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class EWDashboardController : Controller
    {
        private readonly IHRDService _hrdService;
        private readonly IHRDDetailService _hrdDetailService;
        private readonly IRationDetailService _rationDetailService;
        private readonly IRegionalRequestService _regionalRequestService;
        public EWDashboardController(IHRDService hrdService, IHRDDetailService hrdDetailService,
                              IRationDetailService rationDetailService,IRegionalRequestService regionalRequestService)
        {
            _hrdService = hrdService;
            _hrdDetailService = hrdDetailService;
            _rationDetailService = rationDetailService;
            _regionalRequestService = regionalRequestService;

        }

        public JsonResult GetRation()
        {
            var currentHrd = _hrdService.FindBy(m => m.Status == 3).FirstOrDefault();

            var rationDetail = _rationDetailService.FindBy(m => m.RationID == currentHrd.RationID);
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

            var currentHrd = _hrdService.FindBy(m => m.Status == 3).FirstOrDefault();
            var requests = _regionalRequestService.FindBy(m => m.PlanID == currentHrd.PlanID);
            var requestDetail = GetRecentRegionalRequests(requests);

            return Json(requestDetail, JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<RegionalRequestViewModel> GetRecentRegionalRequests(IEnumerable<RegionalRequest> regionalRequests)
        {
            return (from regionalRequest in regionalRequests
                   // from requestDetail in regionalRequest.RegionalRequestDetails
                    select new RegionalRequestViewModel()
                        {
                            Region = regionalRequest.AdminUnit.Name,
                            Round = regionalRequest.Round,
                            MonthName = RequestHelper.MonthName(regionalRequest.Month),
                            StatusID = regionalRequest.Status

                        });
        }
        //public JsonResult GetRequestedInfo(int planID)
        //{
        //    var request = _regionalRequestService.FindBy(m => m.PlanID == planID);
        //    var requestDetail = GetRquestDetailViewModel(request);
        //    return Json(requestDetail, JsonRequestBehavior.AllowGet);
        //}
        //private IEnumerable<RegionalRequestInfoViewModel>  GetRquestDetailViewModel(IEnumerable<RegionalRequest> regionalRequests)
        //{
        //    var requestCount = (from regionalRequest in regionalRequests
        //                        group regionalRequest by regionalRequest.RegionID
        //                        into regionalGroup
        //                        select new
        //                            {
        //                                Region = regionalGroup.Key,
        //                                totalRequest = regionalGroup.Count()
        //                            });
          


        //}
    }
}
