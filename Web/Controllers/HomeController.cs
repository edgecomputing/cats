using System.Globalization;
using Cats.Helpers;
using Cats.Models;
using Cats.Services.EarlyWarning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Data.UnitWork;
using Cats.Services.Common;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Models.ViewModels;
using Cats.Services.Security;
using UserProfile = Cats.Models.Security.UserProfile;

namespace Cats.Controllers
{
    public class HomeController : Controller
    {
        private IRegionalRequestService _regionalRequestService;
        private IReliefRequisitionService _reliefRequistionService;
        private IUnitOfWork _unitOfWork;
        private IUserDashboardPreferenceService _userDashboardPreferenceService;
        private IDashboardWidgetService _dashboardWidgetService;
        private IUserAccountService userService;
        private readonly IDashboardService _IDashboardService;
        private readonly IUserAccountService _userAccountService;
        private readonly INotificationService _notificationService;
        public HomeController(IUserDashboardPreferenceService userDashboardPreferenceService,
            IDashboardWidgetService dashboardWidgetService,
            IUserAccountService _userService,
            IUnitOfWork unitOfWork,
            IRegionalRequestService regionalRequestService,
            IReliefRequisitionService reliefRequisitionService, IDashboardService iDashboardService, IUserAccountService userAccountService, INotificationService notificationService)
        {
            _regionalRequestService = regionalRequestService;
            _reliefRequistionService = reliefRequisitionService;
            _IDashboardService = iDashboardService;
            this._userAccountService = userAccountService;
            _notificationService = notificationService;
            _userDashboardPreferenceService = userDashboardPreferenceService;
            _dashboardWidgetService = dashboardWidgetService;
            this.userService = _userService;
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Home/
        //     [Authorize]
        public ActionResult Index()
        {
            //var req = _reliefRequistionService.FindBy(t => t.RegionID == regionId);
            //var req = _regionalRequestService.FindBy(t => t.RegionID == regionId);
            //ViewBag.Requests = req;

            var currentUser = UserAccountHelper.GetUser(HttpContext.User.Identity.Name);

            var userID = currentUser.UserProfileID;
            if (currentUser.IsAdmin) {
                return RedirectToAction("Index", "AdminDashboard", new { Area = "Settings" });
            }
			if (currentUser.DefaultHub!= null) {
                return RedirectToAction("Index", "Home", new { Area = "Hub" });
            }
            if (currentUser.RegionalUser)
            {
                ViewBag.RegionID = currentUser.RegionID;
                return RedirectToAction("Index", "Home", new { Area = "Regional" });
            }
                switch (currentUser.CaseTeam)
                {
                    case 1:
                        return RedirectToAction("Index", "Home", new { Area = "EarlyWarning" });
                        break;
                    case 2:
                        return RedirectToAction("Index", "Home", new { Area = "PSNP" });
                        break;
                    case 3:
                        return RedirectToAction("Index", "Home", new { Area = "Logistics" });
                        break;
                    case 4:
                        return RedirectToAction("Index", "Home", new { Area = "Procurement" });
                        break;
                    case 5:
                        return RedirectToAction("Index", "Home", new { Area = "Finance" });
                        break;

                }

            
            // If the user is not niether regional nor caseteam user return this default page
            var userDashboardPreferences = _userDashboardPreferenceService.Get(t => t.UserID == userID).OrderBy(m => m.OrderNo);
            var dashboardWidgets = userDashboardPreferences.Select(userDashboardPreference =>
                                    _dashboardWidgetService.FindById(userDashboardPreference.DashboardWidgetID)).ToList();
            return View(dashboardWidgets);

            //return Json(req, JsonRequestBehavior.AllowGet);
            //return Json(req, JsonRequestBehavior.AllowGet);
            //var widgets = new List<DashboardWidget>();
            //return View(widgets);
        }

        public ActionResult Preference()
        {
            if (TempData["PreferenceUpdateSuccessMsg"] != null)
                ModelState.AddModelError("Success", TempData["PreferenceUpdateSuccessMsg"].ToString());
            if (TempData["PreferenceUpdateErrorMsg"] != null)
                ModelState.AddModelError("Errors", TempData["PreferenceUpdateErrorMsg"].ToString());

            var userID = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserProfileID;
            var userDashboardPreferences = _userDashboardPreferenceService.Get(t => t.UserID == userID).OrderBy(m => m.OrderNo);
            var selectedDashboardWidgets = userDashboardPreferences.Select(userDashboardPreference =>
                                    _dashboardWidgetService.FindById(userDashboardPreference.DashboardWidgetID)).ToList();
            ViewBag.SelectedDashboards = selectedDashboardWidgets;
            var allDashboardWidgets = _dashboardWidgetService.GetAllDashboardWidget();
            var unselectedDashbaords = allDashboardWidgets.Where(dashboardWidget => !selectedDashboardWidgets.Contains(dashboardWidget)).ToList();
            ViewBag.UnselectedDashbaords = unselectedDashbaords;

            var user = userService.GetUserDetail(HttpContext.User.Identity.Name);
            //var userPreference = new UserPreferenceEditModel
            //{
            //    Language = user.LanguageCode,
            //    KeyboardLanguage = user.Keyboard,
            //    PreferedWeightMeasurement = user.PreferedWeightMeasurment,
            //    DatePreference = user.DatePreference,
            //    DefaultTheme = user.DefaultTheme
            //};
            var userPreferenceViewModel = new UserPreferenceViewModel(user);
            ViewBag.Languages = new SelectList(userPreferenceViewModel.Languages, "StringID", "Name", userPreferenceViewModel.Language);
            ViewBag.DateFormatPreference = new SelectList(userPreferenceViewModel.DateFormatPreferences, "StringID", "Name", userPreferenceViewModel.DateFormatPreference);
            ViewBag.WeightPrefernce = new SelectList(userPreferenceViewModel.WeightPerferences, "StringID", "Name", userPreferenceViewModel.WeightPrefernce);
            ViewBag.KeyboardLanguage = new SelectList(userPreferenceViewModel.KeyboardLanguages, "StringID", "Name", userPreferenceViewModel.KeyboardLanguage);
            ViewBag.ThemePreference = new SelectList(userPreferenceViewModel.ThemePreferences, "StringID", "Name", userPreferenceViewModel.ThemePreference);

            return View(userPreferenceViewModel);
        }

        public JsonResult SavePreference([DataSourceRequest] DataSourceRequest request, List<int> selectedDashboardIDs)
        {
            var userID = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserProfileID;
            var userDashboardPreferences = _userDashboardPreferenceService.Get(t => t.UserID == userID).OrderBy(m => m.OrderNo);
            var selectedDashboardWidgets = userDashboardPreferences.Select(userDashboardPreference =>
                                    _dashboardWidgetService.FindById(userDashboardPreference.DashboardWidgetID)).ToList();
            var selectedDashboardWidgetIDs = selectedDashboardWidgets.Select(selectedDashboardWidget => selectedDashboardWidget.DashboardWidgetID).ToList();

            //Create the newly selected dashboards in user preference
            var order = 1;
            foreach (var selectedDashboardID in selectedDashboardIDs)
            {
                if (!selectedDashboardWidgetIDs.Contains(selectedDashboardID))
                {
                    var userDashboardPreference = new UserDashboardPreference
                        {
                            DashboardWidgetID = selectedDashboardID,
                            UserID = userID,
                            OrderNo = order
                        };
                    _userDashboardPreferenceService.AddUserDashboardPreference(userDashboardPreference);
                }
                order++;
            }

            //Delete the removed dashboards from the user preference
            foreach (var userDashboardPreferenceObj in selectedDashboardWidgetIDs.Where(selectedDashboardWidgetID => !selectedDashboardIDs.Contains(selectedDashboardWidgetID)).Select(id => _userDashboardPreferenceService.Get(
                t => t.DashboardWidgetID == id && t.UserID == userID).FirstOrDefault()))
            {
                _userDashboardPreferenceService.DeleteUserDashboardPreference(userDashboardPreferenceObj);
            }

            //Edit the selected dashboards order
            var orderNo = 1;
            foreach (var userDashboardPreferenceObj in selectedDashboardIDs.Select(id => _userDashboardPreferenceService.Get(
                t => t.DashboardWidgetID == id && t.UserID == userID).FirstOrDefault()))
            {
                userDashboardPreferenceObj.OrderNo = orderNo;
                _userDashboardPreferenceService.EditUserDashboardPreference(userDashboardPreferenceObj);
                orderNo++;
            }

            ModelState.AddModelError("Success", "Preference Saved.");
            return Json(true, JsonRequestBehavior.AllowGet);
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


        public ActionResult Redirect2Hub()
        {
            return Redirect("/hub");
        }


        public ActionResult GetUnreadNotification([DataSourceRequest] DataSourceRequest request)
        {

            var user = System.Web.HttpContext.Current.User.Identity.Name;
            var roles = _userAccountService.GetUserPermissions(user).Select(a => a.Roles).ToList();
            var allUserRollsInAllApplications = new List<string>();

            foreach (var app in roles)
            {
                allUserRollsInAllApplications.AddRange(app.Select(role => role.RoleName));
            }

            var totalUnread = _notificationService.GetAllNotification().Where(n => n.IsRead == false && allUserRollsInAllApplications.Contains(n.RoleName)).ToList();

            var notificationViewModel = Cats.ViewModelBinder.NotificationViewModelBinder.ReturnNotificationViewModel(totalUnread.ToList());
            return Json(notificationViewModel.ToDataSourceResult(request));
        }
        public ActionResult GetUnreadNotificationDetail([DataSourceRequest] DataSourceRequest request)
        {
            return View();
        }

        public ActionResult ReportListing()
        {
            return View();
        }
    }
}
