using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class RationController : Controller
    {
        private readonly IRationService _rationService;
        private readonly ICommodityService _commodityService;

        public RationController(IRationService rationService,ICommodityService commodityService)
        {
            this._rationService = rationService;
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

        private RationViewModel BindRationViewModel(Ration ration)
        {
            RationViewModel rationViewModel = null;
            if(ration !=null)
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
    }
}
