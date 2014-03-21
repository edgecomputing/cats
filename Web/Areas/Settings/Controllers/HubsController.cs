using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Settings.Controllers
{
    public class HubsController : Controller
    {
        private readonly IHubService _hubService;
        private readonly IHubOwnerService _hubOwnerService;
        private bool _disposed = false;

        public HubsController(IHubService hubService, IHubOwnerService hubOwnerService)
        {
            _hubService = hubService;
            _hubOwnerService = hubOwnerService;
        }
        public IEnumerable<Cats.Models.Hubs.HubView> toViewModel(List<Cats.Models.Hubs.Hub> ermodel)
        {
            return (from item in ermodel
                    select new HubView()
                    {

                        Name = item.Name,
                        HubId = item.HubID,
                        HubOwnerID = item.HubOwnerID
                    });
        }
        
        
        //
        // GET: /Settings/Hubs/

        public ActionResult Index()
        {
            ViewBag.HubOwnersList = _hubOwnerService.GetAllHubOwner().OrderBy(o => o.Name);
            return View();
        }
        public ActionResult ReadAllJson([DataSourceRequest] DataSourceRequest request)
        {

            var list = toViewModel(_hubService.GetAllHub());
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateJson([DataSourceRequest] DataSourceRequest request, Cats.Models.Hubs.HubView viewModel)
        {
             if (viewModel != null && ModelState.IsValid)
            {
                var original = _hubService.FindById(viewModel.HubId);
                original.Name = viewModel.Name;
                original.HubOwnerID = viewModel.HubOwnerID;
                _hubService.EditHub(original);
            }
             return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult CreateJson([DataSourceRequest] DataSourceRequest request, Cats.Models.Hubs.HubView viewModel)
        {
            if (viewModel != null && ModelState.IsValid)
            {
                Cats.Models.Hubs.Hub original = new Cats.Models.Hubs.Hub{
                    Name = viewModel.Name,
                    HubOwnerID = viewModel.HubOwnerID
                    };
                _hubService.AddHub(original);
            }
            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }
       public ActionResult DeleteJson([DataSourceRequest] DataSourceRequest request, Cats.Models.Hubs.HubView viewModel)
        {
            var original = _hubService.FindById(viewModel.HubId);
            _hubService.DeleteHub(original);

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }
        #region 
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _hubService.Dispose();
                   // _commodityTypeService.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
#endregion
        
    }
}
