using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Logistics;
using Cats.Services.Hub;
using Cats.Services.Hub.Interfaces;
using Cats.Data.UnitWork;
using Cats.Services.Common;
using Cats.Services.Security;
using Cats.Helpers;
using Cats.Models;
using Cats.Areas.Logistics.Models;
using Cats.Models.Hubs;

namespace Cats.Areas.Logistics.Controllers
{
    public class LogisticsStockStatusController : Controller
    {
        private readonly Cats.Services.Hub.ITransactionService _transcationService;
        //private readonly ITransactionGroupService _transactionGroupService;
        private readonly IStockStatusService _stockStatusService;
        private readonly IHubService _hubService;
        
        private IUnitOfWork _unitOfWork;
        private IUserDashboardPreferenceService _userDashboardPreferenceService;
        private IDashboardWidgetService _dashboardWidgetService;
        private IUserAccountService _userService;

        public LogisticsStockStatusController
        (
            IUnitOfWork unitOfWork, 
            IUserDashboardPreferenceService userDashboardPreferenceService,
            IDashboardWidgetService dashboardWidgetservice,
            IUserAccountService userService,
            IHubService hubService,

            //ITransactionService transactionService
            //ITransactionGroupService transactionGroupService,
            IStockStatusService stockStatusService
        )
        {
            _unitOfWork = unitOfWork;
            _userDashboardPreferenceService = userDashboardPreferenceService;
            dashboardWidgetservice = dashboardWidgetservice;
            _userService = userService;
            _hubService = hubService;
        
            //_transcationService = transactionService;
            //_transactionGroupService = transactionGroupService;
            _stockStatusService = stockStatusService;
        }
               
        // GET:/Logistics/StockStatus/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AngularGrid() {
            return View();
        }

        public ActionResult angu() {
            return View();
        }
        public ActionResult Status()
        {
            return View();
        }

        public ActionResult nghigh() {
            return View();
        }

        public JsonResult Result() {
            //var x = 1;
            //if(true){
            //    x = 2;     
            //}

            var re = new FreeStockSummaryModel()
            {
                freeStock = 50,
                physicalStock = 50
            };
            
            return Json(re,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHubs()
        {
            var hubs = _stockStatusService.GetHubs();
            return Json(hubs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPrograms() {
            var programs = _stockStatusService.GetPrograms();
            return Json(programs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStockStatusN() {
            var status = _stockStatusService.GetFreeStockStatusD(1, 1, DateTime.Now);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStockStatusD(int hub, int program, DateTime date ) {
            var st = _stockStatusService.GetFreeStockStatusD(hub, program, date);
            return Json(st, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStockStatus(int hub, int program, string date)
        {
            var st = _stockStatusService.GetFreeStockStatus(hub, program, date);
            return Json(st, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStockStatusSummaryN()
        {
            var st = _stockStatusService.GetStockSummaryD(1, DateTime.Now);
            return Json(st, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStockStatusSummaryP(int program, DateTime date) {
            var st = _stockStatusService.GetStockSummaryD(program, date);
            return Json(st, JsonRequestBehavior.AllowGet);
        }
    }
}