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
    public class CommodityTypeController : Controller
    {
        private readonly ICommodityTypeService _commodityTypeService;
        //
        // GET: /CommodityType/

        public CommodityTypeController(ICommodityTypeService commodityTypeService, 
            IUserProfileService userProfileService)
            
        {
            _commodityTypeService = commodityTypeService;
        }
        
        public ViewResult Index()
        {
            return View("Index", _commodityTypeService.GetAllCommodityType().ToList());
        }

        public IEnumerable<CommodityTypeViewModel> toViewModelCollection(List<CommodityType> ermodel)
        {
            return (from item in ermodel
                    select new CommodityTypeViewModel
                        {
                            CommodityTypeId=item.CommodityTypeID,
                            Name=item.Name
                        }
                    );
        }
      /*  public Cats.Areas.Settings.Models.HubOwnerViewModel toViewModel(Cats.Models.Hubs.HubOwner item)
        {
            return new HubOwnerViewModel
            {
                Name = item.Name,
                HubOwnerID = item.HubOwnerID,
                LongName = item.LongName
            };
        }*/
        public ActionResult ReadAllJson([DataSourceRequest] DataSourceRequest request)
        {

            var list = toViewModelCollection(_commodityTypeService.GetAllCommodityType());
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateJson([DataSourceRequest] DataSourceRequest request, CommodityTypeViewModel viewModel)
        {
            if (viewModel != null && ModelState.IsValid)
            {
                var original = _commodityTypeService.FindById(viewModel.CommodityTypeId);
                original.Name = viewModel.Name;

                _commodityTypeService.EditCommodityType(original);
            }
            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult CreateJson([DataSourceRequest] DataSourceRequest request, CommodityTypeViewModel viewModel)
        {
            if (viewModel != null && ModelState.IsValid)
            {
                CommodityType original = new CommodityType
                {
                    Name = viewModel.Name,
                    
                };
                _commodityTypeService.AddCommodityType(original);
            }
            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult DeleteJson([DataSourceRequest] DataSourceRequest request, CommodityTypeViewModel viewModel)
        {
            var original = _commodityTypeService.FindById(viewModel.CommodityTypeId);
            _commodityTypeService.DeleteCommodityType(original);

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }



        public ActionResult Update()
        {
            return PartialView("Update",_commodityTypeService.GetAllCommodityType().ToList());
        }
        //
        // GET: /CommodityType/Details/5

        public ViewResult Details(int id)
        {
            var commoditytype =_commodityTypeService.FindById(id);
            return View(commoditytype);
        }

        //
        // GET: /CommodityType/Create

        public ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /CommodityType/Create

        [HttpPost]
        public ActionResult Create(CommodityType commoditytype)
        {
            if (ModelState.IsValid)
            {
                _commodityTypeService.AddCommodityType(commoditytype);
                return Json(new { success = true });  
            }

            return PartialView(commoditytype);
        }
        
        //
        // GET: /CommodityType/Edit/5

        public ActionResult Edit(int id)
        {
            var commoditytype = _commodityTypeService.FindById(id);
            return PartialView(commoditytype);
        }

        //
        // POST: /CommodityType/Edit/5

        [HttpPost]
        public ActionResult Edit(CommodityType commoditytype)
        {

            if (ModelState.IsValid)
            {
                _commodityTypeService.EditCommodityType(commoditytype);
                return Json(new { success = true });
            }
           // ViewBag.CommodityTypeID = new SelectList(db.Warehouses, "CommodityTypeID", "Name", store.WarehouseID);
            return PartialView(commoditytype);
        }

        //
        // GET: /CommodityType/Delete/5

        public ActionResult Delete(int id)
        {
            
            CommodityType commoditytype = _commodityTypeService.FindById(id);
            return View(commoditytype);
        }

        //
        // POST: /CommodityType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Commodity commodity = CommodityRepo.FindById(id);
            var delCommodityType = _commodityTypeService.FindById(id);
            if (delCommodityType != null &&
                (delCommodityType.Commodities.Count == 0))
            {

                _commodityTypeService.DeleteById(id);
                return RedirectToAction("Index");
            }

            ViewBag.ERROR_MSG = "This Commodity Type is referenced, so it can't be deleted";
            ViewBag.ERROR = true;
            return Delete(id);
        }

        [HttpPost]
        public ActionResult _GetCommodityTypes()
        {
            var result = _commodityTypeService.GetAllCommodityType();
            return new JsonResult
                {
                    Data = new SelectList(result.ToList(), "CommodityTypeID", "Name")
                };
        }
    }
}