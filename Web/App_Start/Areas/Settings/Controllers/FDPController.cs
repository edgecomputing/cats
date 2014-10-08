using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Administration;
using Cats.Areas.Settings.Models.ViewModels;
using Cats.Areas.Settings.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using FDP = Cats.Models.FDP;

namespace Cats.Areas.Settings.Controllers
{
    [Authorize]
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

        public ActionResult map()
        {
            return View();
        }

        public JsonResult FDP_Read([DataSourceRequest]DataSourceRequest request, int? adminUnitID)
        {
            var fdps = _fdpService.Get(t => t != null && (t.AdminUnitID == adminUnitID));
            var fdpsViewModel = FDPViewModelBinder.BindListFDPViewModel((List<FDP>)fdps);
            return Json(fdpsViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FDP_Create([DataSourceRequest] DataSourceRequest request, FDPViewModel fdpViewModel , int? adminUnitID)
        {

            var result = new List<FDPViewModel>();

            
            if (fdpViewModel != null && ModelState.IsValid && adminUnitID.HasValue)
            {
                try
                {
                    //foreach (var viewModel in fdpViewModel)
                    //{

                    if (CheckIfDFPExists((int)adminUnitID, fdpViewModel.Name))
                        {
                            fdpViewModel.AdminUnitID = adminUnitID.Value;
                            var fdp = FDPViewModelBinder.BindFDP(fdpViewModel);
                            _fdpService.AddFDP(fdp);
                            //result.Add(fdpViewModel);
                        }
                   // }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Errors", @"Error: FDP not registered. All fields need to be filled.");
                }
            }
            return Json(result.ToDataSourceResult(request, ModelState));
        }

        private bool CheckIfDFPExists(int woredaId , string name)
        {
            try
            {
                if (_fdpService.FindBy(f => f.Name == name && f.AdminUnitID == woredaId).Any())
                {
                    return false;
                }
                else return true;
            }
            catch (Exception)
            {

                return true;
            }
        }
        public ActionResult FDP_Update(int fdpId)
        {
            var fdp = _fdpService.FindById(fdpId);
            var fvm = FDPViewModelBinder.BindFDPViewModel(fdp);
            return View(fvm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FDP_Update([DataSourceRequest] DataSourceRequest request, FDPViewModel fdpViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fdp = FDPViewModelBinder.BindFDP(fdpViewModel);
                    _fdpService.EditFDP(fdp);
                    ModelState.AddModelError("Success", @"Success: FDP Updated.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Errors", @"Error: FDP not registered. All fields need to be filled.");
                }
            }
            return Json(new[] { fdpViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FDP_Save([DataSourceRequest] DataSourceRequest request, FDPViewModel fdpViewModel)
        {
            if (fdpViewModel != null && ModelState.IsValid)
            {
                //var target = _fdpService.FindById(fdpViewModel.FDPID);
                var fdp = FDPViewModelBinder.BindFDP(fdpViewModel);
                _fdpService.EditFDP(fdp);
            }

            return Json(new[] { fdpViewModel }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult GetGeography()
        {
            var Fdps = _fdpService.FindBy(g => g.Latitude != null && g.Longitude != null);

            var geography = (from fdp in Fdps
                             select new
                             {
                                 fdp.Name,
                                 fdp.Latitude,
                                 fdp.Longitude
                             }
            );

            return Json(geography, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFdpGeolocation(int fdpId)
        {
            var fdp = _fdpService.FindBy(f => f.FDPID == fdpId);
            var location = (from fdp1 in fdp
                            select new
                                {
                                    fdp1.Name,
                                    fdp1.Latitude,
                                    fdp1.Longitude
                                }
                                );
            return Json(location, JsonRequestBehavior.AllowGet);
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult FDP_Destroy([DataSourceRequest] DataSourceRequest request,
        //                                          FDPViewModel fdpViewModel)
        //{
        //    if (fdpViewModel != null)
        //    {
        //        try
        //        {
        //            _fdpService.DeleteById(fdpViewModel.FDPID);
        //            ModelState.AddModelError("Success", "Success: FDP Deleted.");
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("Errors", "Error: FDP not deleted. Foreign ke.");
        //        }

        //    }
        //    RedirectToAction("Index", "FDP");
        //    return Json(ModelState.ToDataSourceResult());
        //}


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

        public ActionResult FDP_Destroy(int id)
        {
            var hub = _fdpService.FindById(id);
            try
            {
                _fdpService.DeleteFDP(hub);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Errors", @"Unable to delete FDP");
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetFdps(int woredaId)
        {
            var fdps = from p in _fdpService.FindBy(x => x.AdminUnitID == woredaId)
                       select new AdminUnitItem { Id = p.FDPID, Name = p.Name };

            return Json(new SelectList(fdps.OrderBy(o => o.Name), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
    }
}
