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
using Cats.Services.Administration;
using Cats.Services.PSNP;
using Cats.Services.Security;
using Cats.Services.Transaction;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Services.EarlyWarning;
using Cats.Services.Common;
using Cats.Infrastructure;
using log4net;
using IAdminUnitService = Cats.Services.EarlyWarning.IAdminUnitService;


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
        private readonly IPlanService _planService;
        private readonly IUserAccountService _userAccountService;
        private readonly Cats.Services.Transaction.ITransactionService _transactionService;
        private readonly IUserProfileService _userProfileService;

        public RegionalPSNPPlanController(IRegionalPSNPPlanService regionalPSNPPlanServiceParam
                                          , IRationService rationServiceParam
                                          , IAdminUnitService adminUnitServiceParam
                                          , IBusinessProcessService BusinessProcessServiceParam
                                          , IBusinessProcessStateService BusinessProcessStateServiceParam
                                          , IApplicationSettingService ApplicationSettingParam
                                          , ILog log
                                          , IPlanService planService
                                          ,IUserAccountService userAccountService, ITransactionService transactionService, IUserProfileService userProfileService)
        {
            this._regionalPSNPPlanService = regionalPSNPPlanServiceParam;
            this._rationService = rationServiceParam;
            this._adminUnitService = adminUnitServiceParam;
            this._BusinessProcessService = BusinessProcessServiceParam;
            this._BusinessProcessStateService = BusinessProcessStateServiceParam;
            this._ApplicationSettingService = ApplicationSettingParam;
            this._log = log;
            this._planService = planService;
            this._userAccountService = userAccountService;
            _transactionService = transactionService;
            this._userProfileService = userProfileService;
        }

        public IEnumerable<RegionalPSNPPlanViewModel> toViewModel(IEnumerable<Cats.Models.RegionalPSNPPlan> list)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            try
            {
                return (from plan in list
                        select new RegionalPSNPPlanViewModel
                        {
                            RegionalPSNPPlanID = plan.RegionalPSNPPlanID,
                            Duration = plan.Duration,
                            PlanName = plan.Plan.PlanName, 
                            Year = plan.Year,
                           // RegionName = plan.Region.Name,
                            RationName = plan.Ration.RefrenceNumber,
                            From = plan.Plan.StartDate.ToCTSPreferedDateFormat(datePref),
                            To = plan.Plan.EndDate.ToCTSPreferedDateFormat(datePref),
                            StatusName = plan.AttachedBusinessProcess.CurrentState.BaseStateTemplate.Name,
                            UserId =(int) plan.User

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
            var psnpPlans = _planService.FindBy(p => p.ProgramID == 2);
            ViewBag.PlanId = new SelectList(_planService.FindBy(p => p.ProgramID == 2), "PlanID", "PlanName");
        }
        //
        // GET: /PSNP/RegionalPSNPPlan/

        public ActionResult Index()
        {
            
            IEnumerable<RegionalPSNPPlan> list = (IEnumerable<Cats.Models.RegionalPSNPPlan>)_regionalPSNPPlanService.GetAllRegionalPSNPPlan();

            return View(list);

        }
        public ActionResult GetListAjax([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Cats.Models.RegionalPSNPPlan> list = (IEnumerable<Cats.Models.RegionalPSNPPlan>)_regionalPSNPPlanService.GetAllRegionalPSNPPlan().OrderByDescending(m=>m.RegionalPSNPPlanID);
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

            if (item.StatusID == (int) Cats.Models.Constant.PSNPWorkFlow.Completed)
                PostPSNP(item);
            return RedirectToAction("Index");
        }


        public void PostPSNP(RegionalPSNPPlan plan)
        {
            try
            {
                var ration = _rationService.FindBy(r => r.RationID == plan.RationID).FirstOrDefault();
                _transactionService.PostPSNPPlan(plan, ration);
            }
            catch (Exception ex)
            {
                
                _log.Error("Error in posting psnp:",new Exception(ex.Message));
            }
           
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
            var planName = regionalpsnpplan.Plan.PlanName;
            var startDate = regionalpsnpplan.Plan.StartDate;
            var firstDayOfTheMonth = startDate.AddDays(1 - startDate.Day);
            var endDate = firstDayOfTheMonth.AddMonths(regionalpsnpplan.Duration).AddDays(-1);
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
             if (ModelState.IsValid)
                {
                    
                        _regionalPSNPPlanService.AddPsnpPlan(planName, firstDayOfTheMonth, endDate);
                        var plan = _planService.Get(m => m.PlanName == planName,null,"Program").FirstOrDefault();
                         regionalpsnpplan.Plan = plan;

                         //check if this psnp plan exitsts for this year and Plan
                         var exists = plan != null && _regionalPSNPPlanService.DoesPsnpPlanExistForThisRegion(plan.PlanID, regionalpsnpplan.Year);

                         if (!exists)
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

                         var psnpPlan=  _regionalPSNPPlanService.CreatePsnpPlan(regionalpsnpplan.Year,regionalpsnpplan.Duration,regionalpsnpplan.RationID,regionalpsnpplan.StatusID,plan.PlanID,user.UserProfileID);
                        //_planService.ChangePlanStatus(regionalpsnpplan.PlanId);
                        BusinessProcess bp = _BusinessProcessService.CreateBusinessProcess(BP_PSNP,
                                                                                           regionalpsnpplan.
                                                                                               RegionalPSNPPlanID,
                                                                                           "PSNP", createdstate);
                        psnpPlan.StatusID = bp.BusinessProcessID;
                        _regionalPSNPPlanService.UpdateRegionalPSNPPlan(psnpPlan);
                        return RedirectToAction("Index");

                    }
                    ViewBag.ErrorMessage1 = "The workflow assosiated with PSNP planning doesnot exist.";
                    ViewBag.ErrorMessage2 = "Please make sure the workflow is created and configured.";
                }
                LoadLookups();
                ModelState.AddModelError("Errors", @"PSNP plan already made for this period and plan Name.");
                return View(regionalpsnpplan);
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