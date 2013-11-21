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
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Logistics.Controllers
{
    public class LogisticsStockStatusController : Controller
    {
        private readonly Cats.Services.Hub.ITransactionService _transcationService;
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
            IStockStatusService stockStatusService
        )
        {
            _unitOfWork = unitOfWork;
            _userDashboardPreferenceService = userDashboardPreferenceService;
            _dashboardWidgetService = dashboardWidgetservice;
            _userService = userService;
            _hubService = hubService;
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
            var q = (from s in st
                     select new HubFreeStockView
                     {
                         CommodityName = s.CommodityName,
                         FreeStock = s.FreeStock.ToPreferedWeightUnit(),
                         PhysicalStock = s.PhysicalStock.ToPreferedWeightUnit()
                     });
            return Json(q, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStockStatus(int hub, int program, string date)
        {
            var st = _stockStatusService.GetFreeStockStatus(hub, program, date);
            var q = (from s in st
                     select new HubFreeStockView
                     {
                         CommodityName = s.CommodityName,
                         FreeStock = s.FreeStock.ToPreferedWeightUnit(),
                         PhysicalStock = s.PhysicalStock.ToPreferedWeightUnit()
                     });
            return Json(q, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStockStatusSummaryN()
        {
            var st = _stockStatusService.GetStockSummaryD(1, DateTime.Now);
            
            var q = (from s in st
                     select new HubFreeStockSummaryView
                     {
                         HubName = s.HubName,
                         TotalFreestock = s.TotalFreestock.ToPreferedWeightUnit(),
                         TotalPhysicalStock = s.TotalPhysicalStock.ToPreferedWeightUnit()
                     });

            return Json(q, JsonRequestBehavior.AllowGet);
        }

       public ActionResult ReceivedCommodity()
       {
          ViewBag.SelectHubID=new SelectList(_stockStatusService.GetHubs(),"HubID","Name");
          ViewBag.SelectProgramID = new SelectList(_stockStatusService.GetPrograms(), "ProgramID", "Name");
           return View();
       }
        public JsonResult CommodityReceived_read([DataSourceRequest]DataSourceRequest request,int hubId=-1,int programId=-1)
        {
            var data = (hubId==-1|| programId==-1) ? null:_stockStatusService.GetReceivedCommodity(t=>t.HubID==hubId && t.ProgramID==programId);
          return  Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
     
            
        }

        public JsonResult GetStockStatusSummaryP(int program, DateTime date) {
            var st = _stockStatusService.GetStockSummaryD(program, date);

            var q = (from s in st
                     select new HubFreeStockSummaryView
                     {
                         HubName = s.HubName,
                         TotalFreestock = s.TotalFreestock.ToPreferedWeightUnit(),
                         TotalPhysicalStock = s.TotalPhysicalStock.ToPreferedWeightUnit()
                     });
            return Json(q, JsonRequestBehavior.AllowGet);
        }

    }
}