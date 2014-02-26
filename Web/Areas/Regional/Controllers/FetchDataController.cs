using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}