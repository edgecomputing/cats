using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class RationController : Controller
    {
        private readonly IRationService _rationService;
        private readonly IRationDetailService _rationDetailService;
        private readonly ICommodityService _commodityService;
        

        public RationController(IRationService rationService, ICommodityService commodityService, IRationDetailService rationDetailService)
        {
            this._rationService = rationService;
            this._rationDetailService = rationDetailService;
            this._commodityService = commodityService;
        }
        //
        // GET: /EarlyWarning/Ration/

        public ActionResult Index()
        {
            var rations = _rationService.GetAllRation();
            var rationViewModels = (from item in rations select BindRationViewModel(item));
            return View(rationViewModels);
        }

        public ActionResult Details(int id)
        {
            ViewBag.RationID = id;
            var ration = _rationService.FindById(id);
            var rationViewModel = BindRationViewModel(ration);
            var commdities = (from item in _commodityService.GetAllCommodity()
                              select new
                                         {
                                             item.CommodityID,
                                             item.Name
                                         });
            ViewData["commodities"] = commdities;
            ViewData["UnitMeasures"] = _commodityService.GetAllUnit();
            return View(rationViewModel);
        }
        private RationDetailViewModel BindRationDetailViewModel(RationDetail rationDetail)
        {
            RationDetailViewModel rationViewModel = null;
            if (rationDetail != null)
            {
                rationViewModel = new RationDetailViewModel();
                rationViewModel.Amount = rationDetail.Amount;
                rationViewModel.Commodity = _commodityService.FindById(rationDetail.CommodityID).Name;
                rationViewModel.CommodityID = rationDetail.CommodityID;
                rationViewModel.RationID = rationDetail.RationID;
                rationViewModel.RationDetailID = rationDetail.RationDetailID;
                rationViewModel.UnitID = rationDetail.UnitID.HasValue?rationDetail.UnitID.Value:-1;
                // rationViewModel.UnitID = rationDetail.UnitID;
            }
            return rationViewModel;
        }
        public ActionResult Edit(int id)
        {
            var obj = _rationService.FindById(id);
            var rationViewModel = BindRationViewModel(obj);
            return PartialView("_Edit", rationViewModel);
        }
        private RationViewModel BindRationViewModel(Ration ration)
        {
            if (ration == null) return null;
            var rationViewModel = new RationViewModel();
            rationViewModel.RationID = ration.RationID;
            rationViewModel.IsDefaultRation = ration.IsDefaultRation;
            rationViewModel.CreatedBy = ration.CreatedBy;
            

            rationViewModel.CreatedDate = ration.CreatedDate;
            rationViewModel.UpdatedBy = ration.UpdatedBy;
            rationViewModel.UpdatedDate = ration.UpdatedDate;
            rationViewModel.ReferenceNumber = ration.RefrenceNumber;
            rationViewModel.CreatedDateEC = ration.CreatedDate.HasValue
                                                ? EthiopianDate.GregorianToEthiopian(ration.CreatedDate.Value)
                                                : "";
            rationViewModel.UpdatedDateEC = ration.UpdatedDate.HasValue
                                     ? EthiopianDate.GregorianToEthiopian(ration.UpdatedDate.Value)
                                     : "";
            return rationViewModel;
        }
        [HttpPost]
        public ActionResult Edit(RationViewModel rationViewModel)
        {
            if (rationViewModel != null && ModelState.IsValid)
            {
                try
                {
                    var orign = _rationService.FindById(rationViewModel.RationID);
                    orign.IsDefaultRation = rationViewModel.IsDefaultRation;

                    orign.UpdatedBy = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserAccountId;
                    orign.UpdatedDate = DateTime.Today;
                    _rationService.EditRation(orign);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return PartialView("_Edit", rationViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(Ration ration)
        {
            if (ration != null && ModelState.IsValid)
            {
                try
                {
                    ration.CreatedBy = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserAccountId;
                    ration.CreatedDate = DateTime.Today;
                    _rationService.AddRation(ration);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return PartialView("_Create", ration);
        }
        public ActionResult Ration_Read([DataSourceRequest] DataSourceRequest request)
        {
         
            var rations = _rationService.GetAllRation();
            var rationViewModels = (from item in rations select BindRationViewModel(item));
            return Json(rationViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult RationDetail_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            var rationDetails = _rationDetailService.Get(t => t.RationID == id);
            var rationViewModels = (from item in rationDetails select BindRationDetailViewModel(item));
            return Json(rationViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RationDetail_Create([DataSourceRequest] DataSourceRequest request, RationDetailViewModel rationDetailViewModel,int id)
        {
            if (rationDetailViewModel != null && ModelState.IsValid)
            {
                rationDetailViewModel.RationID = id;
               
                _rationDetailService.AddRationDetail(BindRationDetail(rationDetailViewModel));
            }

            return Json(new[] { rationDetailViewModel }.ToDataSourceResult(request, ModelState));
        }

        private RationDetail BindRationDetail(RationDetailViewModel rationDetailViewModel)
        {
            if (rationDetailViewModel == null) return null;
            var ration = new RationDetail()
                             {
                                 RationDetailID = rationDetailViewModel.RationDetailID,
                                 RationID = rationDetailViewModel.RationID,
                                 CommodityID = rationDetailViewModel.CommodityID,
                                 Amount = rationDetailViewModel.Amount,
                                 UnitID=rationDetailViewModel.UnitID
                                 
                             };
            return ration;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RationDetail_Update([DataSourceRequest] DataSourceRequest request, RationDetailViewModel rationDetailViewModel)
        {
            if (rationDetailViewModel != null && ModelState.IsValid)
            {
                var origin = _rationDetailService.FindById(rationDetailViewModel.RationID);
                origin.Amount = rationDetailViewModel.Amount;
                _rationDetailService.EditRationDetail(origin);
            }
            return Json(new[] { rationDetailViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RationDetail_Destroy([DataSourceRequest] DataSourceRequest request, RationDetailViewModel rationDetailViewModel)
        {
            if (rationDetailViewModel != null && ModelState.IsValid)
            {
                _rationDetailService.DeleteById(rationDetailViewModel.RationDetailID);
            }
            return Json(ModelState.ToDataSourceResult());
        }
        private bool _disposed = false;
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
