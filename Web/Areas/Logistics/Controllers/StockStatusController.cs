using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Logistics;
using Cats.Services.Hub;
//using Cats.Models.Hub;

namespace Cats.Areas.Logistics.Controllers
{
    public class StockStatusController : Controller
    {
        private readonly ITransactionService _transcationService;
        private readonly ITransactionGroupService _tansactionGroupService;

        public StockStatusController
            (
              ITransactionService transactionService,
              ITransactionGroupService transactionGroupService
            )
        {
            _transcationService = transactionService;
            _tansactionGroupService = transactionGroupService;
        }
               
        //
        // GET: /Logistics/StockStatus/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult FreeStock() {
            var hello = _transcationService.FreeStockStatus();
            //var x = (from h in hello select new { h.LedgerID, h.Month });
            return Json(hello, JsonRequestBehavior.AllowGet);
        }

    }
}
