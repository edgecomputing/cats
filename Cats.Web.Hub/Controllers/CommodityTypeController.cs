using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Models.Hub.Repository;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers
{
    [Authorize]
    public class CommodityTypeController : BaseController
    {
        private readonly ICommodityTypeService _commodityTypeService;
        //
        // GET: /CommodityType/

        public CommodityTypeController(ICommodityTypeService commodityTypeService)
        {
            _commodityTypeService = commodityTypeService;
        }
        
        public ViewResult Index()
        {
            return View("Index", _commodityTypeService.GetAllCommodityType().ToList());
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