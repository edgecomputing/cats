using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.PSNP;
using Cats.Data;
using Cats.Services.PSNP;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Services.EarlyWarning;
using Cats.Services.Common;
using Cats.Infrastructure;
using log4net;


namespace Cats.Areas.PSNP
{
    public class RegionalPSNPPlanController : Controller
    {
        private readonly IRegionalPSNPPlanService _regionalPSNPPlanService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IRationService _rationService;
        private readonly IBusinessProcessService _BusinessProcessService;
        private readonly IBusinessProcessStateService _BusinessProcessStateService;
        private readonly IApplicationSettingService _ApplicationSettingService;
        private readonly ILog _log;

        public RegionalPSNPPlanController(IRegionalPSNPPlanService regionalPSNPPlanServiceParam
                                          , IRationService rationServiceParam
                                          , IAdminUnitService adminUnitServiceParam
                                          , IBusinessProcessService BusinessProcessServiceParam
                                          , IBusinessProcessStateService BusinessProcessStateServiceParam
                                          , IApplicationSettingService ApplicationSettingParam
                                          , ILog log

                                         )
        {
            this._regionalPSNPPlanService = regionalPSNPPlanServiceParam;
            this._rationService = rationServiceParam;
            this._adminUnitService = adminUnitServiceParam;
            this._BusinessProcessService = BusinessProcessServiceParam;
            this._BusinessProcessStateService = BusinessProcessStateServiceParam;
            this._ApplicationSettingService = ApplicationSettingParam;
            this._log = log;
        }

        public IEnumerable<RegionalPSNPPlanViewModel> toViewModel(IEnumerable<Cats.Models.RegionalPSNPPlan> list)
        {
            try
            {
                return (from plan in list
                        select new RegionalPSNPPlanViewModel
                        {
                            RegionalPSNPPlanID = plan.RegionalPSNPPlanID,
                            Duration = plan.Duration,
                            RegionID = plan.RegionID,
                            Year = plan.Year,
                            RegionName = plan.Region.Name,
                            RationName = plan.Ration.RefrenceNumber


                        });
            }
            catch (Exception e)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(e,_log);

            }
            return new List<RegionalPSNPPlanViewModel>();
        }
        public void LoadLookups()
        {

            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.RationID = new SelectList(_rationService.GetAllRation(), "RationID", "RefrenceNumber");
        }
        //
        // GET: /PSNP/RegionalPSNPPlan/

        public ActionResult Index()
        {
            IEnumerable<Cats.Models.RegionalPSNPPlan> list = (IEnumerable<Cats.Models.RegionalPSNPPlan>)_regionalPSNPPlanService.GetAllRegionalPSNPPlan();

            return View(list);

        }
        public ActionResult GetListAjax([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Cats.Models.RegionalPSNPPlan> list = (IEnumerable<Cats.Models.RegionalPSNPPlan>)_regionalPSNPPlanService.GetAllRegionalPSNPPlan();
            return Json(toViewModel(list).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Print(int id = 0)
        {
            if (id == 0)
            {
                RedirectToAction("Index");
            }
            var reportPath = Server.MapPath("~/Report/PSNP/AnnualPlan.rdlc");
            var reportData = _regionalPSNPPlanService.GetAnnualPlanRpt(id);
            var dataSourceName = "annualplan";
            var result = ReportHelper.PrintReport(reportPath, reportData, dataSourceName);

            return File(result.RenderBytes, result.MimeType);
        }
        public ActionResult Details(int id = 0)
        {
            RegionalPSNPPlan regionalpsnpplan = _regionalPSNPPlanService.FindById(id);
            if (regionalpsnpplan == null)
            {
                return HttpNotFound();
            }
            return View(regionalpsnpplan);
        }
        public ActionResult Promote(BusinessProcessState st)
        {
            _BusinessProcessService.PromotWorkflow(st);
            return RedirectToAction("Index");

        }
        public ActionResult promotWorkflow(int id, int nextState)
        {

            RegionalPSNPPlan item = _regionalPSNPPlanService.FindById(id);
            item.StatusID = nextState;
            _regionalPSNPPlanService.UpdateRegionalPSNPPlan(item);
            return RedirectToAction("Index");
        }
        //
        // GET: /PSNP/RegionalPSNPPlan/Create

        public ActionResult Create()
        {
            LoadLookups();
            return View();
        }


        //
        // POST: /PSNP/RegionalPSNPPlan/Create

        [HttpPost]
        public ActionResult Create(RegionalPSNPPlan regionalpsnpplan)
        {
            //regionalpsnpplan.StatusID = 1;
            if (ModelState.IsValid)
            {

                int BP_PSNP = _ApplicationSettingService.getPSNPWorkflow();
                if (BP_PSNP != 0)
                {
                    BusinessProcessState createdstate = new BusinessProcessState
                    {
                        DatePerformed = DateTime.Now,
                        PerformedBy = "System",
                        Comment = "Created workflow for PSNP Plan"

                    };
                    _regionalPSNPPlanService.AddRegionalPSNPPlan(regionalpsnpplan);
                    BusinessProcess bp = _BusinessProcessService.CreateBusinessProcess(BP_PSNP, regionalpsnpplan.RegionalPSNPPlanID, "PSNP", createdstate);
                    regionalpsnpplan.StatusID = bp.BusinessProcessID;
                    _regionalPSNPPlanService.UpdateRegionalPSNPPlan(regionalpsnpplan);
                    return RedirectToAction("Index");

                }
                ViewBag.ErrorMessage1 = "The workflow assosiated with PSNP planning doesnot exist.";
                ViewBag.ErrorMessage2 = "Please make sure the workflow is created and configured.";
            }

            LoadLookups();
            return View(regionalpsnpplan);
        }

        //
        // GET: /PSNP/RegionalPSNPPlan/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LoadLookups();
            RegionalPSNPPlan regionalpsnpplan = _regionalPSNPPlanService.FindById(id);
            if (regionalpsnpplan == null)
            {
                return HttpNotFound();
            }
            return View(regionalpsnpplan);
        }


        [HttpPost]
        public ActionResult Edit(RegionalPSNPPlan regionalpsnpplan)
        {
            if (ModelState.IsValid)
            {
                _regionalPSNPPlanService.UpdateRegionalPSNPPlan(regionalpsnpplan);
                return RedirectToAction("Index");
            }
            LoadLookups();
            return View(regionalpsnpplan);
        }

        //
        // GET: /PSNP/RegionalPSNPPlan/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RegionalPSNPPlan regionalpsnpplan = _regionalPSNPPlanService.FindById(id);
            if (regionalpsnpplan == null)
            {
                return HttpNotFound();
            }
            return View(regionalpsnpplan);
        }

        //
        // POST: /PSNP/RegionalPSNPPlan/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _regionalPSNPPlanService.DeleteById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}