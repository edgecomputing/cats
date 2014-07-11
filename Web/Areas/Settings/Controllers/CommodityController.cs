//using Cats.Web.Adminstration.ViewModelBinder;
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
            if (TempData["error"] != null)
            {
                ViewData["error"] = TempData["error"].ToString();
            }

            ViewData["Commodities"] = _commodityService.GetAllCommodity().Where(c=>c.ParentID == null);
            ViewData["CommodityTypes"] = _commodityTypeService.GetAllCommodityType();
            return View();
        }

        public ActionResult Commodity_Read([DataSourceRequest]DataSourceRequest request)
        {

            var commodities = _commodityService.GetAllCommodity().Where(c=>c.ParentID != null).ToList();
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
                target.ParentID = commodityViewModel.ParentID;
                target.Name = commodityViewModel.Name;
                target.NameAM = commodityViewModel.NameAM;
                target.LongName = commodityViewModel.LongName;
              //  var commodity = CommodityViewModelBinder.BindCommodity(commodityViewModel, target);
                
                _commodityService.EditCommodity(target);
            }

            return Json(new[] { commodityViewModel }.ToDataSourceResult(request, ModelState));
        }


        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Commodity_Destroy([DataSourceRequest] DataSourceRequest request,
        //                                          CommodityViewModel commodityViewModel)
        //{
        //    if (commodityViewModel != null)
        //    {
        //        _commodityService.DeleteById(commodityViewModel.CommodityID);
        //    }

        //    return Json(ModelState.ToDataSourceResult());
        //}

         
        public ActionResult Commodity_Destroy(int id)
        {
            var commodity = _commodityService.FindById(id);
            try
            {
                _commodityService.DeleteCommodity(commodity);
                TempData["error"] = "Commodity is Deleted!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Unable to delete commodity. This commodity is being used by the system.";
            }
            return RedirectToAction("Index");
        }
    }
}
