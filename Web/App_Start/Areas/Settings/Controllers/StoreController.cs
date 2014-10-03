using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Areas.Settings.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;


namespace Cats.Areas.Settings.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IHubService _hubService;

        public StoreController(IStoreService storeService, IHubService hubService)
        {
            _storeService = storeService;
            _hubService = hubService;
        }
        // GET: /Store/

        public ActionResult Index()
        {
            var store = _storeService.GetAllStore();
            ViewBag.HubID = _hubService.GetAllHub();
            return View(store);
        }
        public ActionResult Store_Read([DataSourceRequest] DataSourceRequest request)
        {
            var store = _storeService.GetAllStore().OrderByDescending(m => m.StoreID);
            var storeToDisplay = GetStores(store).ToList();
            return Json(storeToDisplay.ToDataSourceResult(request));
        }
        private IEnumerable<StoreViewModel> GetStores(IEnumerable<Store> store)
        {
            return from stores in store
                   select new StoreViewModel()
                   {
                       StoreID = stores.StoreID,
                       Number = stores.Number,
                       Name = stores.Name,
                       HubID = stores.HubID,
                       HubName = stores.Hub.HubNameWithOwner,
                       IsActive = stores.IsActive,
                       IsTemporary = stores.IsTemporary,
                       StackCount = stores.StackCount,
                       StoreManName = stores.StoreManName
                   };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Store_Create([DataSourceRequest] DataSourceRequest request, StoreViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _storeService.AddStore(BindStore(model));
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        private Store BindStore(StoreViewModel model)
        {
            if (model == null) return null;
            var store = new Store()
            {
                StoreID = model.StoreID,
                Number = model.Number,
                Name = model.Name,
                HubID = model.HubID,
                IsActive = model.IsActive,
                IsTemporary = model.IsTemporary,
                StackCount = model.StackCount,
                StoreManName = model.StoreManName

            };
            return store;
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Store_Update([DataSourceRequest] DataSourceRequest request, StoreViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var origin = _storeService.FindById(model.StoreID);
                origin.Number = model.Number;
                origin.Name = model.Name;
                origin.HubID = model.HubID;
                origin.IsActive = model.IsActive;
                origin.IsTemporary = model.IsTemporary;
                origin.StackCount = model.StackCount;
                origin.StoreManName = model.StoreManName;
                _storeService.EditStore(origin);
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy_Store([DataSourceRequest] DataSourceRequest request, StoreViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var store = _storeService.FindById(model.StoreID);
                _storeService.DeleteStore(store);
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Delete(int id)
        {
            var store = _storeService.FindById(id);
            if (store != null)
            {
                _storeService.DeleteStore(store);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
