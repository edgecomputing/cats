using System.Collections.Generic;
using System.Linq;
using Cats.Services.EarlyWarning;
using System.Web.Mvc;
using Cats.Services.Common;
using Cats.Services.Security;


namespace Cats.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly INeedAssessmentSummaryService _needAssessmentSummaryService;
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly IUserAccountService _userAccountService;
        private readonly INotificationService _notificationService;
        public DashboardController(INotificationService notificationService, IUserAccountService userAccountService)
        {
            _notificationService = notificationService;
            _userAccountService = userAccountService;
            this._dashboardService = new Cats.Services.EarlyWarning.DashboardService();
            this._needAssessmentSummaryService = new Cats.Services.Common.NeedAssessmentSummaryService();
        }

        public ActionResult RequestsById(int RegionId = 10)
        {
            var model = _dashboardService.RegionalRequests(RegionId);
            return PartialView("_Requests", model);
        }

        public JsonResult dRegionalMonthlyRequests()
        {
            return Json(_dashboardService.Requests(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Requests()
        {
            var model = _dashboardService.Requests();
            return PartialView("_Requests", model);
        }

        public JsonResult RegionalMonthlyRequests()
        {
            return Json(_dashboardService.RMRequests(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegionalRequestsById(int RegionId)
        {
            return Json(_dashboardService.RegionalRequestsByRegionID(RegionId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PieRequests()
        {
            return Json(_dashboardService.PieRegionalRequests(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReliefRequisitionBasedOnStatus()
        {
            return Json(_dashboardService.RequisitionBasedOnStatus(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BarBeneficiaries()
        {
            return Json(_dashboardService.BarNoOfBeneficiaries(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BarNeedAssessment(string regionName)
        {
            return Json(_needAssessmentSummaryService.NeedAssessmentByRegion(regionName), JsonRequestBehavior.AllowGet);
        }

        // int y = DateTime.Now.Year;

        public JsonResult BarNeedAssessmentbY(int year = 2013)
        {
            return Json(_needAssessmentSummaryService.NeedAssessmentByYear(year), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getYears()
        {
            return Json(_needAssessmentSummaryService.GetYears(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult BarRegionalReqDetailCommodity()
        {
            return Json(_dashboardService.RegionalRequestsBeneficiary(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ZonalBeneficiaries(string RegionName)
        {
            return Json(_dashboardService.ZonalBeneficiaries(_dashboardService.getRegionId(RegionName)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ZonalMonthlyBeneficiaries(string RegionName, string ZoneName)
        {
            return Json(_dashboardService.ZonalMonthlyBeneficiaries(RegionName, ZoneName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnreadNotifications()
        {
            var user = System.Web.HttpContext.Current.User.Identity.Name;
            var notifications = _dashboardService.GetUnreadNotifications();
            var roles = _userAccountService.GetUserPermissions(user).Select(a => a.Roles).ToList();
            var allUserRollsInAllApplications = new List<string>();

            foreach (var app in roles)
            {
                allUserRollsInAllApplications.AddRange(app.Select(role => role.RoleName));
            }
            var totalUnread = _notificationService.GetAllNotification().Where(n => n.IsRead == false && allUserRollsInAllApplications.Contains(n.RoleName)).ToList();
            return Json(totalUnread, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegionalRequestsPieChart()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            //_dashboardService.Dispose();
        //_needAssessmentSummaryService;
        //_reliefRequisitionService;
        //_userAccountService;
        //_notificationService;
            base.Dispose(disposing);
        }

    }
}