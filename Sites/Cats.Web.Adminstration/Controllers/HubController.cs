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
     [Authorize]
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
            var hub = new Hub();
            if (hubViewModel != null && ModelState.IsValid)
            {
                hub = HubViewModelBinder.BindHub(hubViewModel);
                _hubService.AddHub(hub);
            }
            return Json(new[] { hub }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Hub_Update([DataSourceRequest] DataSourceRequest request, Hub hub)
        {
            if (hub != null && ModelState.IsValid)
            {
                var target = _hubService.FindById(hub.HubID);
                _hubService.EditHub(target);
            }

            return Json(new[] { hub }.ToDataSourceResult(request, ModelState));
        }

        
        public ActionResult Hub_Destroy(int id)
        {
            var hub = _hubService.FindById(id);
            try
            {
                _hubService.DeleteHub(hub);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Errors", "Unable to delete Hub");
            }
            return RedirectToAction("Index");
        }
        
    }
}
