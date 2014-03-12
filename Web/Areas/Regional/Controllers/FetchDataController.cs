using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Regional.Models;
using Cats.Services.Dashboard;

namespace Cats.Areas.Regional.Controllers
{
    public class FetchDataController : Controller
    {
        //
        // GET: /Regional/FetchData/
        private readonly IRegionalDashboard _regionalDashboard;

        public FetchDataController(IRegionalDashboard regionalDashboard)
        {
            _regionalDashboard = regionalDashboard;
        }

        public JsonResult Requests(int regionID)
        {
            var requests = _regionalDashboard.GetRecentRequests(regionID);
            return Json(requests, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Requisitions(int regionID)
        {
            var result = _regionalDashboard.GetRecentRequisitions(regionID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RequisitionPercentage(int regionID)
        {
            var result = _regionalDashboard.RequisitionsPercentage(regionID);
            //int sum = 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Dispatches(int regionID)
        {
            var result = _regionalDashboard.GetRecentDispatches(regionID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegionalData(int regionID)
        {
            var d = new DashboardData()
                {
                    ApprovedRequests = 33,
                    PendingRequests = 33,
                    HubAssignedRequests = 34,
                    ApprovedRequisitions = 52,
                    HubAssignedRequisitions = 12,
                    PendingRequisitions = 36,
                    Above18 = 45,
                    Bet5And8 = 26,
                    Below5 = 29,
                    Female = 24152,
                    Male = 21451,
                    IncomingCommodity = 25131,
                    IncomingDispatches = 2142
                };

            return Json(d, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ImportantNumbers(int regionID)
        {
            var d = new ImportantNumbers()
            {
                TotalCommodity = 4502,
                TotalFDPS = 1425,
                TotalPeople = 54050,
                TotalRequests = 51470,
                TotalRequistions = 12451
                
            };

            return Json(d, JsonRequestBehavior.AllowGet);
        }
    }
}