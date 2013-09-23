using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Models.ViewModels;
using Cats.Web.Adminstration.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Web.Adminstration.Controllers
{
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


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CommoditySource_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  CommoditySourceViewModel commoditySourceViewModel)
        {
            if (commoditySourceViewModel != null)
            {
                _commoditySourceService.DeleteById(commoditySourceViewModel.CommoditySourceID);
            }

            return Json(ModelState.ToDataSourceResult());
        }
        
  


  





    }
}
