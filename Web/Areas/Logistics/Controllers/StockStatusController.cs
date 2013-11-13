using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Logistics;
using Cats.Services.Hub;
using Cats.Data.UnitWork;
using Cats.Services.Common;
using Cats.Services.Security;
using Cats.Helpers;
using Cats.Models;
using Cats.Areas.Logistics.Models;
//using Cats.Models.Hub;

namespace Cats.Areas.Logistics.Controllers
{
    public class StockStatusController : Controller
    {
        private readonly Cats.Services.Hub.ITransactionService _transcationService;
        //private readonly ITransactionGroupService _transactionGroupService;
        //private readonly IStockStatusService _stockStatusService;
        private readonly IHubService _hubService;
        
        private IUnitOfWork _unitOfWork;
        private IUserDashboardPreferenceService _userDashboardPreferenceService;
        private IDashboardWidgetService _dashboardWidgetService;
        private IUserAccountService _userService;

        public StockStatusController
        (
            IUnitOfWork unitOfWork, 
            IUserDashboardPreferenceService userDashboardPreferenceService,
            IDashboardWidgetService dashboardWidgetservice,
            IUserAccountService userService,
            IHubService hubService

            //ITransactionService transactionService
            //ITransactionGroupService transactionGroupService,
            //IStockStatusService stockStatusService
        )
        {
            _unitOfWork = unitOfWork;
            _userDashboardPreferenceService = userDashboardPreferenceService;
            dashboardWidgetservice = dashboardWidgetservice;
            _userService = userService;
            _hubService = hubService;
        
            //_transcationService = transactionService;
            //_transactionGroupService = transactionGroupService;
            //_stockStatusService = stockStatusService;
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

        //public ActionResult GetHubs()
        //{
        //    //var hubs = _hubService.GetHubs();
        //    return Json(hubs, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult FreeStock()
        //{
        //    //var x = (from h in hello select new { h.LedgerID, h.Month });
        //    return Json(_stockStatusService.FreeStockByHub(1), JsonRequestBehavior.AllowGet);
        //}
    }
}