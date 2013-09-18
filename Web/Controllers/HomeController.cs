using System.Globalization;
using Cats.Models;
using Cats.Services.EarlyWarning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Data.UnitWork;
using Cats.Services.Common;

namespace Cats.Controllers
{
    
    public class HomeController : Controller
    {
        private IRegionalRequestService _regionalRequestService;
        private IReliefRequisitionService _reliefRequistionService; 
        private IUnitOfWork _unitOfWork = new UnitOfWork();
        private IUserDashboardPreferenceService _userDashboardPreferenceService;
             

        public HomeController(IUserDashboardPreferenceService userDashboardPreferenceService) {
            _regionalRequestService = new RegionalRequestService(_unitOfWork);
            _reliefRequistionService = new ReliefRequisitionService(_unitOfWork);
            _userDashboardPreferenceService = userDashboardPreferenceService;
        }

        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index(int regionId=4)
        {

            //var req = _reliefRequistionService.FindBy(t => t.RegionID == regionId);
            var req = _regionalRequestService.FindBy(t => t.RegionID == regionId);
            ////ViewBag.Requests = req;
            var userID = System.Web.HttpContext.Current.User.Identity.Name;

            var userDashboardPreferences = _userDashboardPreferenceService.Get(t => t.UserID.ToString(CultureInfo.InvariantCulture) == userID);
            List<DashboardWidget> dashboardWidgets = new List<DashboardWidget>();
            foreach(var userDashboardPreference in userDashboardPreferences)
            {
                dashboardWidgets.Add(new DashboardWidget(userDashboardPreference.DashboardWidgetID));
            }
            return View();
            //return Json(req, JsonRequestBehavior.AllowGet);
           // return Json(req, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegionalMonthlyRequests()
        {
            var request = _unitOfWork.RegionalRequestRepository.FindById(2);
            return Json(request, JsonRequestBehavior.AllowGet);
        }
    }
}
