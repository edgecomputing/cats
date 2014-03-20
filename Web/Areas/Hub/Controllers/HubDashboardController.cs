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

        public JsonResult StockStatus(int hub, int program)
        {

            var st = _stockStatusService.GetStockSummaryD(1, DateTime.Now);

            //st.Take()
            var value = st.Find(t => t.HubID == hub);

            var free = (value.TotalFreestock / value.TotalPhysicalStock) * 100;
            var commited = ((value.TotalPhysicalStock - value.TotalFreestock) / value.TotalPhysicalStock) * 100;

            var q = (from s in st
                     where s.HubID == hub
                     select s);

            //var free = q.First;
            // return Json(q, JsonRequestBehavior.AllowGet);

            var j = new StockStatusViewModel()
            {
                freeStockAmount = value.TotalFreestock,
                freestockPercent = free,
                physicalStockAmount = (value.TotalPhysicalStock - value.TotalFreestock),
                physicalStockPercent = commited,
                totalStock = value.TotalPhysicalStock
            };

            return Json(j, JsonRequestBehavior.AllowGet);
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