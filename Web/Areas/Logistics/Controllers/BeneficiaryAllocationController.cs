using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Infrastructure;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Logistics.Controllers
{
    public class BeneficiaryAllocationController : Controller
    {
        private readonly IBeneficiaryAllocationService _beneficiaryAllocationService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IReliefRequisitionService _reliefRequisitionService;

        public BeneficiaryAllocationController(IBeneficiaryAllocationService beneficiaryAllocationService,
            IAdminUnitService adminUnitService, IReliefRequisitionService reliefRequisitionService)
        {
            this._beneficiaryAllocationService = beneficiaryAllocationService;
            this._reliefRequisitionService = reliefRequisitionService;
            this._adminUnitService = adminUnitService;
        }
        //
        // GET: /Logistics/BeneficiaryAllocation/
        public ActionResult Index()
        {
            ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
            ViewBag.RequisitionID = new SelectList(_reliefRequisitionService.Get(t => t.Status == 4).ToList(), "RequisitionID", "RequisitionNo");
            return View();
        }
      
        public JsonResult BeneficiaryAllocations_Read([DataSourceRequest]DataSourceRequest request)

        {
            IQueryable<BeneficiaryAllocation> beneficiaryAllocation = _beneficiaryAllocationService.GetBenficiaryAllocation().AsQueryable();
            DataSourceResult result = beneficiaryAllocation.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(int RegionID, int FilterZone, int RequisitionID=0)
        {
            ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name", RegionID);
           // ViewBag.ZoneID = new SelectList(_adminUnitService.GetZones(2), "AdminUnitID", "Name", ZoneID);
            ViewBag.RequisitionID = new SelectList(_reliefRequisitionService.Get(t => t.Status == 4).ToList(), "RequisitionID", "RequisitionNo", RequisitionID);
            var beneficiaryAllocation =
                _beneficiaryAllocationService.GetBenficiaryAllocation(
                    t => t.RegionID == RegionID && t.ZoneID == FilterZone && t.RequisitionID == RequisitionID);
            return View(beneficiaryAllocation.ToList());
        }

        public JsonResult ZonesList(int id)
        {
            var zones = _adminUnitService.GetZones(id);

            return Json(new SelectList(zones.ToArray(), "AdminUnitID", "Name"), JsonRequestBehavior.AllowGet);
        }
        public FileResult Print()
        {
            var reportPath = Server.MapPath("~/Report/EarlyWarning/RRDDetail.rdlc");
            var reportData = _beneficiaryAllocationService.GetBenficiaryAllocation().ToList();
            var dataSourceName = "RRDDetail";
            var result = ReportHelper.PrintReport(reportPath, reportData, dataSourceName);

            return File(result.RenderBytes, result.MimeType);
        }
    
    }
}
