using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Infrastructure;
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
        private IAdminUnitService _adminUnitService;
        
        public LogisticsStockStatusController
        (
            IUnitOfWork unitOfWork,
            IUserDashboardPreferenceService userDashboardPreferenceService,
            IDashboardWidgetService dashboardWidgetservice,
            IUserAccountService userService,
            IHubService hubService,
            IStockStatusService stockStatusService, IAdminUnitService adminUnitService)
        {
            _unitOfWork = unitOfWork;
            _userDashboardPreferenceService = userDashboardPreferenceService;
            _dashboardWidgetService = dashboardWidgetservice;
            _userService = userService;
            _hubService = hubService;
            _stockStatusService = stockStatusService;
            _adminUnitService = adminUnitService;
        }

        // GET:/Logistics/StockStatus/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AngularGrid()
        {
            return View();
        }

        public ActionResult angu()
        {
            return View();
        }
        public ActionResult Status()
        {
            return View();
        }

        public ActionResult nghigh()
        {
            return View();
        }

        public JsonResult Result()
        {
            var re = new FreeStockSummaryModel()
            {
                freeStock = 50,
                physicalStock = 50
            };

            return Json(re, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHubs()
        {
            var hubs = _stockStatusService.GetHubs();
            return Json(hubs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPrograms()
        {
            var programs = _stockStatusService.GetPrograms();
            return Json(programs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStockStatusN()
        {
            var status = _stockStatusService.GetFreeStockStatusD(1, 1, DateTime.Now);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStockStatusD(int hub, int program, DateTime date)
        {
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

        public JsonResult GetFreeAndphysicalStockStatus()
        {
            var st = _stockStatusService.GetFreeAndPhysicalStockSummary();
            var q = (from s in st
                     select new SummaryFreeAndPhysicalStockModel
                     {
                         CommodityName = s.CommodityName,
                         HubName = s.HubName,
                         Program = s.Program,
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

        #region ReceivedCommodityStockStatus
        public ActionResult ReceivedCommodity()
        {
            ViewBag.SelectHubID = new SelectList(_stockStatusService.GetHubs(), "HubID", "Name");
            ViewBag.SelectProgramID = new SelectList(_stockStatusService.GetPrograms(), "ProgramID", "Name");
            return View();
        }
        public JsonResult CommodityReceived_read([DataSourceRequest]DataSourceRequest request, int hubId = -1, int programId = -1)
        {
            var data = (hubId == -1 || programId == -1)
                           ? new List<VWCommodityReceived>()
                           : _stockStatusService.GetReceivedCommodity(t => t.HubID == hubId && t.ProgramID == programId);
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

       public ActionResult PrintSummaryFreePhysicalStock()
       {
           return GetSummaryFreePhysicalStock(true, false);
       }

       public ActionResult ExportSummaryFreePhysicalStock()
       {
           return GetSummaryFreePhysicalStock(false, false);
       }

       public ActionResult PrintReceivedCommodity(int hubId = -1,int programId=-1)
       {
           return GetReceivedCommodity(hubId, programId, true, false);
        }
       
        public ActionResult ExportReceivedCommodity(int hubId = -1, int programId = -1)
        {
            return GetReceivedCommodity(hubId, programId, false, false);
        }
        private ActionResult GetReceivedCommodity(int hubId, int programId, bool isPdf = true, bool isPortrait = true)
        {
            var data = (hubId == -1)
                           ? null
                           : _stockStatusService.GetReceivedCommodity(t => t.HubID == hubId);
            var reportPath = Server.MapPath("~/Report/Logisitcs/ReceivedCommodityStatus.rdlc");
            var dataSources = "dataset";

            var result = ReportHelper.PrintReport(reportPath, data, dataSources, isPdf, isPortrait);
            return File(result.RenderBytes, result.MimeType);
        }
        #endregion

        #region Carry Over Stock
        public ActionResult CarryOverStock()
        {
            ViewBag.SelectHubID = new SelectList(_stockStatusService.GetHubs(), "HubID", "Name");
            ViewBag.SelectProgramID = new SelectList(_stockStatusService.GetPrograms(), "ProgramID", "Name");
            return View();
        }
        public JsonResult CarryOverStock_read([DataSourceRequest]DataSourceRequest request, int hubId = -1, int programId = -1)
        {
            var data = (hubId == -1 || programId == -1)
                           ? new List<VWCarryOver>()
                           : _stockStatusService.GetCarryOverStock(t => t.HubID == hubId && t.ProgramID == programId);
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

       }
       private ActionResult GetSummaryFreePhysicalStock(bool isPdf = true, bool isPortrait = true)
       {
           var data = _stockStatusService.GetSummaryFreePhysicalStock();
           var reportPath = Server.MapPath("~/Report/Logisitcs/SummaryFreePhysicalStock.rdlc");
           var dataSources = "dataset";

           var result = ReportHelper.PrintReport(reportPath, data, dataSources, isPdf, isPortrait);
           return File(result.RenderBytes, result.MimeType);
       }
       private ActionResult GetCarryOverStock(int hubId, int programId, bool isPdf = true, bool isPortrait = true)
       {
           var data = (hubId == -1)
                          ? null
                          : _stockStatusService.GetCarryOverStock(t => t.HubID == hubId);
           var reportPath = Server.MapPath("~/Report/Logisitcs/CarryOverStockStatus.rdlc");
           var dataSources = "dataset";

           var result = ReportHelper.PrintReport(reportPath, data, dataSources, isPdf, isPortrait);
           return File(result.RenderBytes, result.MimeType);
       }
        public ActionResult PrintCarryOverStock(int hubId = -1, int programId = -1)
        {
            return GetCarryOverStock(hubId, programId, true, false);
        }

        public ActionResult ExportCarryOverStock(int hubId = -1, int programId = -1)
        {
            return GetCarryOverStock(hubId, programId, false, false);
        }

        #endregion

        #region Transferred Stock Status
        public ActionResult TransferredStock()
        {
            ViewBag.SelectHubID = new SelectList(_stockStatusService.GetHubs(), "HubID", "Name");
            ViewBag.SelectProgramID = new SelectList(_stockStatusService.GetPrograms(), "ProgramID", "Name");
            return View();
        }
        public JsonResult TransferredStock_read([DataSourceRequest]DataSourceRequest request, int hubId = -1, int programId = -1)
        {
            var data = (hubId == -1 || programId == -1)
                           ? new List<VWTransferredStock>()
                           : _stockStatusService.GetTransferredStock(t => t.HubID == hubId && t.ProgramID == programId);
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        public ActionResult PrintTransferredStock(int hubId = -1, int programId = -1)
        {
            return GetTransferredStock(hubId, programId, true, false);
        }

        public ActionResult ExportTransferredStock(int hubId = -1, int programId = -1)
        {
            return GetTransferredStock(hubId, programId, false, false);
        }


        private ActionResult GetTransferredStock(int hubId, int programId, bool isPdf = true, bool isPortrait = true)
        {
            var data = (hubId == -1)
                           ? null
                           : _stockStatusService.GetTransferredStock(t => t.HubID == hubId);
            var reportPath = Server.MapPath("~/Report/Logisitcs/TransferredStockStatus.rdlc");
            var dataSources = "dataset";

            var result = ReportHelper.PrintReport(reportPath, data, dataSources, isPdf, isPortrait);
            return File(result.RenderBytes, result.MimeType);
        }
        #endregion

        public JsonResult GetStockStatusSummaryP(int program, DateTime date)
        {
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

        public ActionResult SummaryForFreeAndPhysicalStock()
        {
            return View();
        }

        public ActionResult DispatchCommodity()
        {
            try
            {
                ViewBag.SelectHubID = new SelectList(_stockStatusService.GetHubs(), "HubID", "Name");
                ViewBag.SelectProgramID = new SelectList(_stockStatusService.GetPrograms(), "ProgramID", "Name");
                ViewBag.SelectRegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
                ViewBag.SelectZoneID = new SelectList(_adminUnitService.GetAllAdminUnit().Where(a => a.ParentID == 2), "AdminUnitID", "Name");
                ViewBag.SelectWoredaID = new SelectList(_adminUnitService.GetAllAdminUnit().Where(a => a.ParentID == 3), "AdminUnitID", "Name");
                ViewBag.SelectFDPID = new SelectList(_adminUnitService.GetAllAdminUnit().Where(a => a.ParentID == 4), "AdminUnitID", "Name");
                return View();
            }
            catch (Exception)
            {
               
                
                throw;
            }
           
        }

        public JsonResult DispatchedReceived_read([DataSourceRequest]DataSourceRequest request, 
                                                                                                                                    int hubId = -1, 
                                                                                                                                    int programId = -1,
                                                                                                                                    int regionId = -1,
                                                                                                                                    int zoneId=-1
                                                                                                                                    )
        {
            List<VWDispatchCommodity> data;
            if (regionId!=-1 && zoneId == -1)
            {
                data = (hubId == -1 || programId == -1 || regionId == -1)
                           ? new List<VWDispatchCommodity>()
                           : _stockStatusService.GetDispatchedCommodity(t => t.HubId == hubId && t.ProgramID == programId && t.RegionId == regionId);
            }
            else if (regionId!=-1 && zoneId!=-1)
            {
                data = (hubId == -1 || programId == -1 || regionId == -1)
                           ? new List<VWDispatchCommodity>()
                           : _stockStatusService.GetDispatchedCommodity(t => t.HubId == hubId && t.ProgramID == programId && t.RegionId == regionId && t.ZoneId == zoneId);
            }
            else
            {
                data = (hubId == -1 || programId == -1)
                            ? new List<VWDispatchCommodity>()
                            : _stockStatusService.GetDispatchedCommodity(t => t.HubId == hubId && t.ProgramID == programId);
               
            }


            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetZonesByRegion(int regionId = -1)
        {
            return Json(new SelectList(_adminUnitService.GetAllAdminUnit().Where(p => p.ParentID == regionId).ToArray(), "AdminUnitID", "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWoredsByZone(int zoneId = -1)
        {
            return Json(new SelectList(_adminUnitService.GetAllAdminUnit().Where(p => p.ParentID == zoneId).ToArray(), "AdminUnitID", "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFDPsByWoreda(int woredaId = -1)
        {
            return Json(new SelectList(_adminUnitService.GetAllAdminUnit().Where(p => p.ParentID == woredaId).ToArray(), "AdminUnitID", "Name"), JsonRequestBehavior.AllowGet);
        }

    }
}