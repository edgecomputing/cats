using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class PlanController : Controller
    {
        //
        // GET: /EarlyWarning/Plan/
        private IPlanService _hrdPlanService;
        private IUserAccountService _userAccountService;
        private ICommonService _commonService;
        private INeedAssessmentService _needAssessmentService;
        private IHRDService _hrdService;
        public PlanController(IPlanService hrdPlanService,IUserAccountService userAccountService,
                              ICommonService commonService,INeedAssessmentService needAssessmentService,IHRDService hrdService)
        {
            _hrdPlanService = hrdPlanService;
            _userAccountService = userAccountService;
            _commonService = commonService;
            _needAssessmentService = needAssessmentService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan_Read([DataSourceRequest] DataSourceRequest request)
        {

            var plans = _hrdPlanService.FindBy(m=>m.Program.Name=="Relief").OrderByDescending(m=>m.PlanID);
            var plansToDisplay = GetPlan(plans).ToList();
            return Json(plansToDisplay.ToDataSourceResult(request));
        }

        private IEnumerable<PlanViewModel> GetPlan(IEnumerable<Plan>  plans)
        {
             var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from plan in plans
                    select new PlanViewModel
                        {
                            planID = plan.PlanID,
                            planName = plan.PlanName,
                            StartDate = plan.StartDate.ToCTSPreferedDateFormat(datePref),
                            EndDate = plan.EndDate.ToCTSPreferedDateFormat(datePref),
                            ProgramID = plan.ProgramID,
                            Program = plan.Program.Name,
                            StatusID = plan.Status,
                            Status = _commonService.GetStatusName(WORKFLOW.Plan, plan.Status) 
                        });
        }
        public ActionResult Create()
        {
            var plan = new Plan();
            ViewBag.ProgramID = new SelectList(_hrdPlanService.GetPrograms(),"ProgramID", "Name");
            plan.StartDate = DateTime.Now;
            plan.EndDate = DateTime.Now;
            return View(plan);
        }

        [HttpPost]
        public ActionResult Create(Plan plan)
        {
            if (ModelState.IsValid)
            {
                plan.Status = (int)PlanStatus.Draft;
                _hrdPlanService.AddPlan(plan);
                return RedirectToAction("Index");

            }
            ViewBag.ProgramID = new SelectList(_hrdPlanService.GetPrograms(), "ProgramID", "Name");
            return View(plan);
        }
        public ActionResult Edit(int id)
        {
            var plan = _hrdPlanService.FindById(id);
            ViewBag.ProgramID = new SelectList(_hrdPlanService.GetPrograms(), "ProgramID", "Name",plan.ProgramID);
            if (plan==null)
            {
              
            }
            return View (plan);
        }
        [HttpPost]
        public ActionResult Edit(Plan plan)
        {
            if(ModelState.IsValid)
            {
                _hrdPlanService.EditPlan(plan);
                return RedirectToAction("Index");
            }
            return View(plan);
        }
        public ActionResult Detail(int id)
        {
            var plan = _hrdPlanService.FindById(id);

            var needAssessment = _needAssessmentService.FindBy(m => m.PlanID == plan.PlanID).ToList();
            var hrd = _hrdService.FindBy(m => m.PlanID == plan.PlanID).ToList();
            var planWithHrdViewModel = new PlanWithHRDViewModel()
                {
                    Plan = plan,
                    HRDs = hrd,
                    NeedAssessments = needAssessment
                };

            return View(plan);
        }
    }
}
