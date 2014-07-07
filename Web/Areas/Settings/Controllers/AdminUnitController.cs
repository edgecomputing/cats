using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Administration;
using Cats.Areas.Settings.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using  Cats.Areas.Settings.ViewModelBinder;

namespace Cats.Areas.Settings.Controllers
{
    [Authorize]
    public class AdminUnitController : Controller
    {
        private readonly IAdminUnitService _adminUnitService;
        // private readonly IFDPService _fdpService;

        public AdminUnitController(IAdminUnitService adminUnitService)
        {
            _adminUnitService = adminUnitService;
        }
        
        //
        // GET: /FDP/

        public ActionResult Index()
        {
            ViewBag.RegionCollection = _adminUnitService.GetRegions();
            //ViewData["Zones"] = _adminUnitService.GetZones();
            //ViewData["Regions"] = _adminUnitService.GetAllRegions();
            return View();
        }

        public JsonResult AdminUnit_Read([DataSourceRequest]DataSourceRequest request, int parentAdminUnitID)
        {
            List<AdminUnit> admins = _adminUnitService.FindBy(t => t != null && (t.ParentID == parentAdminUnitID));
            var adminUnitViewModel = AdminUnitViewModelBinder.BindListAdminUnitViewModel(admins);
            return Json(adminUnitViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GISMapping()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AdminUnit_Create([DataSourceRequest] DataSourceRequest request, AdminUnitViewModel adminUnitViewModel, int? paramParentID, int? paramAdminUnitTypeID)
        {
            if (adminUnitViewModel != null && ModelState.IsValid)
            {
                try
                {
                    if (paramParentID.HasValue)
                    {
                        adminUnitViewModel.ParentID = (int)paramParentID;
                    }
                    if (paramAdminUnitTypeID.HasValue)
                    {
                        adminUnitViewModel.AdminUnitTypeID = (int)paramAdminUnitTypeID;
                    }
                    var adminUnit = AdminUnitViewModelBinder.BindAdminUnit(adminUnitViewModel);
                    _adminUnitService.AddAdminUnit(adminUnit);
                    ModelState.AddModelError("Success", "Success: Admin Unit Registered.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Errors", "Error: FDP not registered. All fields need to be filled.");
                }
            }
            return Json(new[] { adminUnitViewModel }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AdminUnit_Update([DataSourceRequest] DataSourceRequest request, AdminUnitViewModel adminUnitViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var adminUnit = AdminUnitViewModelBinder.BindAdminUnit(adminUnitViewModel);
                    _adminUnitService.EditAdminUnit(adminUnit);
                   // ModelState.AddModelError("Success", "Success: Updated.");
                    return Json(new[] { adminUnitViewModel }.ToDataSourceResult(request, ModelState));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Errors", "Error: not registered. All fields need to be filled.");
                }
            }
            return Json(new[] { adminUnitViewModel }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult AdminUnitWoreda_Destroy(int id)
        {
            var hub = _adminUnitService.FindById(id);
            try
            {
                _adminUnitService.DeleteById(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Errors", "Unable to delete Admin Unit");
            }
            return RedirectToAction("Index");
        }

        public ActionResult AdminUnitZone_Destroy(int id)
        {
            var hub = _adminUnitService.FindById(id);
            try
            {
                _adminUnitService.DeleteById(id);
                return RedirectToAction( "ManageZones" );
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Errors", "Unable to delete Admin Unit");
            }
            return RedirectToAction("ManageZones");
        }

        public ActionResult ManageZones()
        {
            ViewBag.RegionCollection = _adminUnitService.GetAllRegions();
            return View();
        }

        public ActionResult AdminUnit_Destroy(AdminUnitViewModel adminUnitViewModel)
        {
            var au = _adminUnitService.FindById(adminUnitViewModel.AdminUnitID);
            try
            {
                _adminUnitService.DeleteAdminUnit(au);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Errors", "Unable to delete FDP");
            }
            return Json(ModelState.ToDataSourceResult());
        }
    }
}
