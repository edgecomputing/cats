using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Areas.Settings.Models.ViewModels;
using Cats.Areas.Settings.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Settings.Controllers
{
     [Authorize]
    public class CommoditySourceController : Controller
    {
        private readonly ICommoditySourceService _commoditySourceService;

        public CommoditySourceController(ICommoditySourceService commoditySourceService)
        {
            _commoditySourceService = commoditySourceService;
        }
        //
        // GET: /CommoditySource/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CommoditySource_Read([DataSourceRequest]DataSourceRequest request)
        {
            var commoditySources = _commoditySourceService.GetAllCommoditySource();
            var commoditysourcesViewModel =
                CommoditySourceViewModelBinder.BindListCommoditySourceViewModel(commoditySources);
            return Json(commoditysourcesViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CommoditySource_Create([DataSourceRequest] DataSourceRequest request, CommoditySourceViewModel commoditySourceViewModel)
        {
            if (commoditySourceViewModel != null && ModelState.IsValid)
            {
                var commoditySource = CommoditySourceViewModelBinder.BindCommoditySource(commoditySourceViewModel);
                _commoditySourceService.AddCommoditySource(commoditySource);
            }
            return Json(new[] { commoditySourceViewModel }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CommoditySource_Update([DataSourceRequest] DataSourceRequest request, CommoditySourceViewModel commoditySourceViewModel)
        {
            if (commoditySourceViewModel != null && ModelState.IsValid)
            {
                var target = _commoditySourceService.FindById(commoditySourceViewModel.CommoditySourceID);
                var commoditySource = CommoditySourceViewModelBinder.BindCommoditySource(commoditySourceViewModel, target);
                _commoditySourceService.EditCommoditySource(commoditySource);
            }

            return Json(new[] { commoditySourceViewModel }.ToDataSourceResult(request, ModelState));
        }


        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult CommoditySource_Destroy([DataSourceRequest] DataSourceRequest request,
        //                                          CommoditySourceViewModel commoditySourceViewModel)
        //{
        //    if (commoditySourceViewModel != null)
        //    {
        //        _commoditySourceService.DeleteById(commoditySourceViewModel.CommoditySourceID);
        //    }

        //    return Json(ModelState.ToDataSourceResult());
        //}


        public ActionResult CommoditySource_Destroy(int id)
        {
            var hub = _commoditySourceService.FindById(id);
            try
            {
                _commoditySourceService.DeleteCommoditySource(hub);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Errors", @"Unable to delete CommoditySource");
            }
            return RedirectToAction("Index");
        }

  





    }
}
