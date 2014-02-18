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
    }
}