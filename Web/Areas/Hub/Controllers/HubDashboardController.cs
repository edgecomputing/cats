using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Hub.Models;
using Cats.Services.Hub.Interfaces;
using Cats.Models.Hubs;
using Cats.Helpers;

namespace Cats.Areas.Hub.Controllers
{
    public class HubDashboardController : Controller
    {
        //
        // GET: /Hub/HubDashboard/
        private readonly IStockStatusService _stockStatusService;

        public HubDashboardController(IStockStatusService stockStatusService)
        {
            _stockStatusService = stockStatusService;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult StockStatus (int hub, int program) {
        
            var s = new StockStatusViewModel() { 
               freeStockAmount = 45000,
               freestockPercent = 10,
               physicalStockAmount = 55000,
               physicalStockPercent = 90,
               totalStock = 100000
            };

            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CommodityStockStatus(int hub, int program)
        {
            var st = _stockStatusService.GetFreeStockStatusD(hub, program, DateTime.Now);
            var q = (from s in st
                     select new HubFreeStockView
                     {
                         CommodityName = s.CommodityName,
                         FreeStock = s.FreeStock.ToPreferedWeightUnit(),
                         PhysicalStock = s.PhysicalStock.ToPreferedWeightUnit()
                     });

            return Json(q, JsonRequestBehavior.AllowGet);
        }
    }
}