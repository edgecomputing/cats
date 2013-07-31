using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class RationController : Controller
    {
        private readonly IRationService _rationService;
        private readonly ICommodityService _commodityService;

        public RationController(IRationService rationService, ICommodityService commodityService)
        {
            this._rationService = rationService;
            this._commodityService = commodityService;
        }
        //
        // GET: /EarlyWarning/Ration/

        public ActionResult Index()
        {

            return View();
        }

        private RationViewModel BindRationViewModel(Ration ration)
        {
            RationViewModel rationViewModel = null;
            if (ration != null)
            {
                rationViewModel = new RationViewModel();
                rationViewModel.Amount = ration.Amount;
                rationViewModel.Commodity = _commodityService.FindById(ration.CommodityID).Name;
                rationViewModel.CommodityID = ration.CommodityID;
                rationViewModel.RationID = ration.RationID;
            }
            return rationViewModel;
        }
        public ActionResult Edit(int id)
        {
            var obj = _rationService.FindById(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(RationViewModel rationViewModel)
        {
            if (rationViewModel != null && ModelState.IsValid)
            {
                var obj = _rationService.FindById(rationViewModel.RationID);
                obj.Amount = rationViewModel.Amount;
                _rationService.EditRation(obj);
                return View("Index");
            }
            return View(rationViewModel);
        }

        public ActionResult Ration_Read([DataSourceRequest] DataSourceRequest request)
        {
            var rations = _rationService.GetAllRation();
            var rationViewModels = (from item in rations select BindRationViewModel(item));
            return Json(rationViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ration_Create([DataSourceRequest] DataSourceRequest request, RationViewModel rationViewModel)
        {
            if (rationViewModel != null && ModelState.IsValid)
            {
                _rationService.AddRation(BindRation(rationViewModel));
            }

            return Json(new[] { rationViewModel }.ToDataSourceResult(request, ModelState));
        }

        private Ration BindRation(RationViewModel rationViewModel)
        {
            if (rationViewModel == null) return null;
            var ration = new Ration()
                             {
                                 RationID = rationViewModel.RationID,
                                 CommodityID = rationViewModel.CommodityID,
                                 Amount = rationViewModel.CommodityID
                             };
            return ration;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ration_Update([DataSourceRequest] DataSourceRequest request, RationViewModel rationViewModel)
        {
            if(rationViewModel!=null && ModelState.IsValid)
            {
                var origin = _rationService.FindById(rationViewModel.RationID);
                origin.Amount = rationViewModel.Amount;
                _rationService.EditRation(origin);
            }
            return Json(new[] {rationViewModel}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ration_Destroy([DataSourceRequest] DataSourceRequest request,RationViewModel rationViewModel)
        {
            if(rationViewModel!=null&& ModelState.IsValid)
            {
                _rationService.DeleteById(rationViewModel.RationID);
            }
            return Json(ModelState.ToDataSourceResult());
        }
        private bool _disposed=false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _commodityService.Dispose();
                    _rationService.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
