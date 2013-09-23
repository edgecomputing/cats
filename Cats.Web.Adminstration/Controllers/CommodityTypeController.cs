using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Data.UnitWork;
using Cats.Services.Administration;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Web.Adminstration.ViewModelBinder;
using Cats.Web.Adminstration.Models.ViewModels;
namespace Cats.Web.Adminstration.Controllers
{
    public class CommodityTypeController : Controller
    {
        private readonly ICommodityTypeService _commodityTypeService;
        //
        // GET: /CommodityType/
        public CommodityTypeController(ICommodityTypeService commodityTypeService)
        {
            _commodityTypeService = commodityTypeService;
        }
        
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CommodityType_Read([DataSourceRequest]DataSourceRequest request)
        {
            var commodityTypes = _commodityTypeService.GetAllCommodityType();
            var commodityTypesViewModel = CommodityTypeViewModelBinder.BindListCommodityTypeViewModel(commodityTypes);
            return Json(commodityTypesViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        //
        // Post: /CommodityType/CommodityType_Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CommodityType_Create([DataSourceRequest] DataSourceRequest request, CommodityTypeViewModel commodityTypeViewModel)
        {
            if (commodityTypeViewModel != null && ModelState.IsValid)
            {
                var commodityType = CommodityTypeViewModelBinder.BindCommodityType(commodityTypeViewModel);
                _commodityTypeService.AddCommodityType(commodityType);
            }
            return Json(new[] {commodityTypeViewModel}.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CommodityType_Update([DataSourceRequest] DataSourceRequest request, CommodityTypeViewModel commodityTypeViewModel)
        {
            if (commodityTypeViewModel != null && ModelState.IsValid)
            {
                var target = _commodityTypeService.FindById(commodityTypeViewModel.CommodityTypeId);
                var commodityType = CommodityTypeViewModelBinder.BindCommodityType(commodityTypeViewModel,target);
                
                _commodityTypeService.EditCommodityType(commodityType);
            }

            return Json(new[] { commodityTypeViewModel }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CommodityType_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  CommodityTypeViewModel commodityTypeViewModel)
        {
            if (commodityTypeViewModel != null)
            {
                _commodityTypeService.DeleteById(commodityTypeViewModel.CommodityTypeId);
            }

            return Json(ModelState.ToDataSourceResult());
        }
    }
}
