using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Models.ViewModels;
using Cats.Web.Adminstration.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Web.Adminstration.Controllers
{
    public class FDPController : Controller
    {
        private readonly IAdminUnitService _adminUnitService;
        private readonly IFDPService _fdpService;

        public FDPController(IAdminUnitService adminUnitService, IFDPService fdpService)
        {
            _adminUnitService = adminUnitService;
            _fdpService = fdpService;
        }
        //
        // GET: /FDP/

        public ActionResult Index()
        {
            ViewBag.RegionCollection = _adminUnitService.GetAllRegions();
            //ViewData["Zones"] = _adminUnitService.GetZones();
            //ViewData["Regions"] = _adminUnitService.GetAllRegions();
            return View();
        }

        public JsonResult FDP_Read([DataSourceRequest]DataSourceRequest request, int? adminUnitID)
        {
            var fdps = _fdpService.Get(t => t != null && (t.AdminUnitID == adminUnitID));
            var fdpsViewModel = FDPViewModelBinder.BindListFDPViewModel((List<FDP>)fdps);
            return Json(fdpsViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FDP_Create([DataSourceRequest] DataSourceRequest request, FDPViewModel fdpViewModel,int? adminUnitID)
        {
            if (fdpViewModel != null && ModelState.IsValid && adminUnitID.HasValue)
            {
                try
                {
                    fdpViewModel.AdminUnitID = adminUnitID.Value;
                    var fdp = FDPViewModelBinder.BindFDP(fdpViewModel);
                    _fdpService.AddFDP(fdp);
                    ModelState.AddModelError("Success", "Success: FDP Registered.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Errors", "Error: FDP not registered. All fields need to be filled.");
                }
            }
            return Json(new[] { fdpViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FDP_Update([DataSourceRequest] DataSourceRequest request, FDPViewModel fdpViewModel)
        {
            if (fdpViewModel != null && ModelState.IsValid )
            {
                
                //var target = _fdpService.FindById(fdpViewModel.FDPID);
                var fdp = FDPViewModelBinder.BindFDP(fdpViewModel);
                _fdpService.EditFDP(fdp);
            }

            return Json(new[] { fdpViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FDP_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  FDPViewModel fdpViewModel)
        {
            if (fdpViewModel != null)
            {
                try
                {
                    _fdpService.DeleteById(fdpViewModel.FDPID);
                    ModelState.AddModelError("Success", "Success: FDP Deleted.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Errors", "Error: FDP not deleted. Foreign ke.");
                }

            }
            RedirectToAction("Index", "FDP");
            return Json(ModelState.ToDataSourceResult());
        }


        public JsonResult GetCascadeRegions([DataSourceRequest] DataSourceRequest request)
        {
            var regions = _adminUnitService.GetAllRegions();
            var regionsViewModel = AdminUnitViewModelBinder.BindListAdminUnitViewModel(regions).ToList();

            return Json(regionsViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeZones([DataSourceRequest] DataSourceRequest request, int AdminUnitID)
        {
            var zonesByRegion = _adminUnitService.GetZones(AdminUnitID);
            var zonesViewModel = AdminUnitViewModelBinder.BindListAdminUnitViewModel(zonesByRegion).ToList();

            return Json(zonesViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeWoredas([DataSourceRequest] DataSourceRequest request, int zoneID)
        {
            var woredasByRegion = _adminUnitService.GetWoredasByZone(zoneID);
            var woredasViewModel = AdminUnitViewModelBinder.BindListAdminUnitViewModel(woredasByRegion).ToList();

            return Json(woredasViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

    }
}
