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
        private IPlanService _planService;
        private IUserAccountService _userAccountService;
        private ICommonService _commonService;
        private INeedAssessmentService _needAssessmentService;
        private IHRDService _hrdService;
        public PlanController(IPlanService hrdPlanService,IUserAccountService userAccountService,
                              ICommonService commonService,INeedAssessmentService needAssessmentService,IHRDService hrdService)
        {
            _planService = hrdPlanService;
            _userAccountService = userAccountService;
            _commonService = commonService;
            _needAssessmentService = needAssessmentService;
        }
        public ActionResult Index(int id=0)
        {
            ViewBag.Status = id;
            return View();
        }

        public ActionResult Plan_Read([DataSourceRequest] DataSourceRequest request,int id=0)
        {

            var plans =id == 0 ? _planService.Get(m=>m.Status==(int)PlanStatus.Draft).OrderByDescending(m=>m.PlanID).ToList():_planService.Get(m=>m.Status==id).ToList();
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
            ViewBag.ProgramID = new SelectList(_planService.GetPrograms(),"ProgramID", "Name");
            plan.StartDate = DateTime.Now;
            plan.EndDate = DateTime.Now;
            return View(plan);
        }

        [HttpPost]
        public ActionResult Create(Plan plan)
        {
            var startDate = plan.StartDate;
            var endDate = plan.StartDate.AddMonths(plan.Duration);
            if (ModelState.IsValid)
            {
                var existingPlan =
                    _planService.FindBy(m => m.PlanName == plan.PlanName && m.ProgramID == plan.ProgramID)
                                .FirstOrDefault();
                if (existingPlan!=null)
                {
                    ModelState.AddModelError("Errors", @"Plan with this Name and Program already Exists please change Plan Name");
                }
                else
                {
                    try
                    {
                        plan.EndDate = endDate;
                        plan.Status = (int)PlanStatus.Draft;
                        _planService.AddPlan(plan);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError("Errors", @"Plan with this name already Existed");
                        ViewBag.ProgramID = new SelectList(_planService.GetPrograms(), "ProgramID", "Name");
                        return View(plan);
                    } 
                }
                    
                

            }
            ViewBag.ProgramID = new SelectList(_planService.GetPrograms(), "ProgramID", "Name");
            return View(plan);
        }
        public ActionResult Edit(int id)
        {
            var plan = _planService.FindById(id);
            ViewBag.ProgramID = new SelectList(_planService.GetPrograms(), "ProgramID", "Name",plan.ProgramID);
            if (plan==null)
            {
                return HttpNotFound();
            }
            return View (plan);
        }
        [HttpPost]
        public ActionResult Edit(Plan plan)
        {
            if(ModelState.IsValid)
            {
                _planService.EditPlan(plan);
                return RedirectToAction("Index");
            }
            return View(plan);
        }
        public ActionResult ClosePlan(int id)
        {
            var plan=_planService.FindById(id);
            if(plan!=null)
            {
                plan.Status = (int) PlanStatus.Closed;
                _planService.EditPlan(plan);
                return RedirectToAction("Index", "Plan", new {id = plan.Status});
            }
            return RedirectToAction("index", "Plan", new {id = plan.Status});
        }
        public ActionResult Detail(int id)
        {
            var plan = _planService.FindById(id);

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
