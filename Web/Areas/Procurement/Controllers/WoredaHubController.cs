using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.PSNP;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Procurement.Controllers
{
    public class WoredaHubController : Controller
    {
        private IWoredaHubService _woredaHubService;
        private IWoredaHubLinkService _woredaHubLinkService;
        private static IHRDService _hrdService;
        private IRegionalPSNPPlanService _regionalPSNPPlanService;
        private IAdminUnitService _adminUnitService;
        private IHubService _hubService;
        private IHRDDetailService _hrdDetailService;
        private IRegionalPSNPPlanDetailService _regionalPSNPPlanDetailService;
        private IApplicationSettingService _applicationSettingService;
        private static IPlanService _planService;

        public WoredaHubController (IWoredaHubService woredaHubService, IWoredaHubLinkService woredaHubLinkService, 
            IHRDService hrdService, IRegionalPSNPPlanService regionalPSNPPlanService, IAdminUnitService adminUnitService,
            IHubService hubService, IHRDDetailService hrdDetailService, IRegionalPSNPPlanDetailService regionalPSNPPlanDetailService,
            IApplicationSettingService applicationSettingService, IUserAccountService userAccountService, IPlanService planService)
        {
            _woredaHubService = woredaHubService;
            _woredaHubLinkService = woredaHubLinkService;
            _hrdService = hrdService;
            _regionalPSNPPlanService = regionalPSNPPlanService;
            _adminUnitService = adminUnitService;
            _hubService = hubService;
            _hrdDetailService = hrdDetailService;
            _regionalPSNPPlanDetailService = regionalPSNPPlanDetailService;
            _applicationSettingService = applicationSettingService;
            _planService = planService;
        }
        //
        // GET: /Procurement/WoredaHubLink/

        public ActionResult Index()
        {
            var woredaHubs = _woredaHubService.GetAllWoredaHub();
            return View(woredaHubs);
        }

        //public ActionResult CreateWoredaHub()
        //{
        //    var woredas = new List<AdminUnit>();
        //    var woredasFromHRD = new List<AdminUnit>();
        //    var woredasFromPSNP = new List<AdminUnit>();
        //    var applicationSetting = _applicationSettingService.FindBy(m => m.SettingName == "CurrentHRD").FirstOrDefault();
        //    if (applicationSetting != null)
        //    {
        //        var currentHRD = int.Parse(applicationSetting.SettingValue);
        //        var hrdDetails = _hrdDetailService.Get(t => t.HRD.HRDID == currentHRD, null, "AdminUnit").ToList();
        //        woredasFromHRD = (from item in hrdDetails select item.AdminUnit).Distinct().ToList();
        //    }
        //    var firstOrDefault = _applicationSettingService.FindBy(m => m.SettingName == "CurentPSNPPlan").FirstOrDefault();
        //    if (firstOrDefault != null)
        //    {
        //        var currentPSNPPlan = int.Parse(firstOrDefault.SettingValue);
        //        var regionalPSNPPlanDetails = _regionalPSNPPlanDetailService.Get(t => t.RegionalPSNPPlan.Plan.PlanID == currentPSNPPlan).ToList();
        //        woredasFromPSNP = (from item in regionalPSNPPlanDetails select item.PlanedFDP.AdminUnit).Distinct().ToList();
        //    }
        //    woredas = woredasFromHRD.Union(woredasFromPSNP).Distinct().ToList();
        //    ViewData["Woredas"] = new SelectList(woredas, "AdminUnitID", "Name");
        //    ViewData["Hubs"] = new SelectList(_hubService.GetAllHub().ToList(), "HubID", "Name");

        //    foreach (var woreda in woredas)
        //    {
                
        //    }

        //    var woredaHub = _woredaHubService.Get(t=>t.Status.ToString() == WoredaHubLinkVersionStatus.Approved.ToString()).FirstOrDefault();
        //    var woredaHubViewModel = WoredaHubViewModelBinder(woredaHub);
            
        //    if (woredaHub != null) return View("Details", woredaHubViewModel);
        //}

        public ActionResult WoredaHub_Read([DataSourceRequest] DataSourceRequest request)
        {
            
            var woredaHubs = _woredaHubService.GetAllWoredaHub();
            var woredaHubsToDisplay = WoredaHubListViewModelBinder(woredaHubs).ToList().OrderByDescending(t=>t.StartDate);
            return Json(woredaHubsToDisplay.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WoredaHub_Create([DataSourceRequest] DataSourceRequest request, WoredaHubViewModel woredaHubViewModel)
        {
            if (woredaHubViewModel != null && ModelState.IsValid)
            {
                _woredaHubService.AddWoredaHub(WoredaHubListBinder(woredaHubViewModel));
            }

            return Json(new[] { woredaHubViewModel }.ToDataSourceResult(request, ModelState));
        }

        public static WoredaHubViewModel WoredaHubViewModelBinder(WoredaHub woredaHub)
        {
            return new WoredaHubViewModel
            {
                WoredaHubID = woredaHub.WoredaHubID,
                WoredaHubName = woredaHub.StartDate + " - " + woredaHub.Status,
                HRDID = woredaHub.HRDID,
                HRD = _hrdService.FindById(woredaHub.HRDID).Season.Name + " - " + _hrdService.FindById(woredaHub.HRDID).Year,
                PlanID = woredaHub.PlanID,
                Plan = _planService.FindById(woredaHub.PlanID).PlanName,
                StartDate = woredaHub.StartDate,
                EndDate = woredaHub.EndDate,
                Status = woredaHub.Status
            };
        }

        public static List<WoredaHubViewModel> WoredaHubListViewModelBinder(List<WoredaHub> woredaHubs)
        {
            return woredaHubs.Select(woredaHub => new WoredaHubViewModel
            {
                WoredaHubID = woredaHub.WoredaHubID,
                WoredaHubName = woredaHub.StartDate + " - " + woredaHub.Status,
                HRDID = woredaHub.HRDID,
                HRD = _hrdService.FindById(woredaHub.HRDID).Season.Name + " - " + _hrdService.FindById(woredaHub.HRDID).Year,
                PlanID = woredaHub.PlanID,
                Plan = _planService.FindById(woredaHub.PlanID).PlanName,
                StartDate = woredaHub.StartDate,
                EndDate = woredaHub.EndDate,
                Status = woredaHub.Status
            }).ToList();
        }

        public static WoredaHub WoredaHubListBinder(WoredaHubViewModel model)
        {
            if (model == null) return null;
            var woredaHub = new WoredaHub()
            {
                WoredaHubID = model.WoredaHubID,
                HRDID = model.HRDID,
                PlanID = model.PlanID,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = model.Status
            };
            return woredaHub;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WoredaHub_Update([DataSourceRequest] DataSourceRequest request, WoredaHubViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var origin = _woredaHubService.FindById(model.WoredaHubID);
                origin.StartDate = model.StartDate;
                origin.EndDate = model.EndDate;
                origin.Status = model.Status;

                _woredaHubService.EditWoredaHub(origin);
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WoredaHub_Destroy([DataSourceRequest] DataSourceRequest request, WoredaHubViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var woredahub = _woredaHubService.FindById(model.WoredaHubID);
                _woredaHubService.DeleteWoredaHub(woredahub);
            }
            return Json(ModelState.ToDataSourceResult());
        }

        //
        // GET: /Procurement/WoredaHubLink/Details/5

        public ActionResult Details(int id)
        {
            
            
            var woredaHub = _woredaHubService.FindById(id);
            var woredaHubViewModel = WoredaHubViewModelBinder(woredaHub);
            return View(woredaHubViewModel);
        }

        //
        // POST: /Procurement/WoredaHubLink/Delete/5
        public ActionResult Delete(int id)
        {
            var woredaHubVersion = _woredaHubService.FindById(id);
            if (woredaHubVersion != null)
            {
                _woredaHubService.DeleteWoredaHub(woredaHubVersion);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult DeleteLink(int id)
        {
            var woredaHubLink = _woredaHubLinkService.FindById(id);
            if (woredaHubLink != null)
            {
                _woredaHubLinkService.DeleteWoredaHubLink(woredaHubLink);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult WoredaHubLink_Read([DataSourceRequest] DataSourceRequest request, int woredaHubID)
        {

            var woredaHubLinks = _woredaHubLinkService.Get(t => t.WoredaHubID == woredaHubID).ToList();
            var woredaHubLinksToDisplay = WoredaHubLinkBinding.WoredaHubLinkListViewModelBinder(woredaHubLinks).ToList().OrderByDescending(t => t.Woreda);
            return Json(woredaHubLinksToDisplay.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WoredaHubLink_Create([DataSourceRequest] DataSourceRequest request, WoredaHubLinkViewModel woredaHubLinkViewModel, int woredaHubID)
        {
            if (woredaHubLinkViewModel != null && ModelState.IsValid)
            {
                woredaHubLinkViewModel.WoredaHubID = woredaHubID;
                _woredaHubLinkService.AddWoredaHubLink(WoredaHubLinkBinding.WoredaHubLinkListBinder(woredaHubLinkViewModel));
            }

            return Json(new[] { woredaHubLinkViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WoredaHubLink_Update([DataSourceRequest] DataSourceRequest request, WoredaHubLinkViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var origin = _woredaHubLinkService.FindById(model.WoredaHubLinkID);
                origin.WoredaID = model.WoredaID;
                origin.HubID = model.HubID;

                _woredaHubLinkService.EditWoredaHubLink(origin);
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}
