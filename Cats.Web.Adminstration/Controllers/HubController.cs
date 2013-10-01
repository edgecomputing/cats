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
    public class HubController : Controller
    {
        private readonly IHubService _hubService;
        private readonly IHubOwnerService _hubOwnerService;

        public HubController(IHubService hubService, IHubOwnerService hubOwnerService)
        {
            _hubService = hubService;
            _hubOwnerService = hubOwnerService;
        }
        //
        // GET: /Hub/

        public ActionResult Index()
        {
            ViewData["HubOwners"] = _hubOwnerService.GetAllHubOwner();
            return View();
        }

        public JsonResult Hub_Read([DataSourceRequest]DataSourceRequest request)
        {
            var hubs = _hubService.GetAllHub();
            var hubsViewModel = HubViewModelBinder.BindListHubViewModel(hubs);
            return Json(hubsViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Hub_Create([DataSourceRequest] DataSourceRequest request, HubViewModel hubViewModel)
        {
            if (hubViewModel != null && ModelState.IsValid)
            {
                try
                {
                    var hub = HubViewModelBinder.BindHub(hubViewModel);
                    _hubService.AddHub(hub);
                    ModelState.AddModelError("Success", "Success: Hub Registered.");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("Errors", "Error: Hub not registered. All fields need to be filled.");
                }
            }
            return Json(new[] { hubViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Hub_Update([DataSourceRequest] DataSourceRequest request, HubViewModel hubViewModel)
        {
            if (hubViewModel != null && ModelState.IsValid)
            {
                var target = _hubService.FindById(hubViewModel.HubID);
                var hub = HubViewModelBinder.BindHub(hubViewModel, target);
                _hubService.EditHub(hub);
            }

            return Json(new[] { hubViewModel }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Hub_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  HubViewModel hubViewModel)
        {
            if (hubViewModel != null)
            {
                try
                {
                    _hubService.DeleteById(hubViewModel.HubID);
                    ModelState.AddModelError("Success", "Success: Hub Deleted.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Errors", "Error: Hub not deleted. Foreign ke.");
                }
                
            }

            return Json(ModelState.ToDataSourceResult());
        }
        
    }
}
