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
using ICommodityTypeService = Cats.Services.Administration.ICommodityTypeService;

namespace Cats.Web.Adminstration.Controllers
{
     [Authorize]
    public class CommodityController : Controller
    {
        private readonly ICommodityService _commodityService;
        private readonly ICommodityTypeService _commodityTypeService;

        public CommodityController(ICommodityTypeService commodityTypeService,ICommodityService commodityService)
        {
            _commodityService = commodityService;
            _commodityTypeService = commodityTypeService;
        }
        //
        // GET: /Commodity/

        public ActionResult Index()
        {
            ViewData["Commodities"] = _commodityService.GetAllCommodity();
            ViewData["CommodityTypes"] = _commodityTypeService.GetAllCommodityType();
            return View();
        }

        public ActionResult Commodity_Read([DataSourceRequest]DataSourceRequest request)
        {

            var commodities = _commodityService.GetAllCommodity();
            var commoditiesViewModel = CommodityViewModelBinder.BindListCommodityViewModel(commodities);
            return Json(commoditiesViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        //
        // Post: /CommodityType/Commodity_Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Commodity_Create([DataSourceRequest] DataSourceRequest request, CommodityViewModel commodityViewModel)
        {
            if (commodityViewModel != null && ModelState.IsValid)
            {
                var commodity = CommodityViewModelBinder.BindCommodity(commodityViewModel);
                _commodityService.AddCommodity(commodity);

            }
            return Json(new[] { commodityViewModel }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Commodity_Update([DataSourceRequest] DataSourceRequest request, CommodityViewModel commodityViewModel)
        {
            if (commodityViewModel != null && ModelState.IsValid)
            {
                var target = _commodityService.FindById(commodityViewModel.CommodityID);
                var commodity = CommodityViewModelBinder.BindCommodity(commodityViewModel, target);
                _commodityService.EditCommodity(commodity);
            }

            return Json(new[] { commodityViewModel }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Commodity_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  CommodityViewModel commodityViewModel)
        {
            if (commodityViewModel != null)
            {
                _commodityService.DeleteById(commodityViewModel.CommodityID);
            }

            return Json(ModelState.ToDataSourceResult());
        }


    }
}
