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
        public PlanController(IPlanService hrdPlanService,IUserAccountService userAccountService,ICommonService commonService)
        {
            _hrdPlanService = hrdPlanService;
            _userAccountService = userAccountService;
            _commonService = commonService;
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
            return View(plan);
        }
        public ActionResult Edit(int id)
        {
            var plan = _hrdPlanService.FindById(id);
            if (plan==null)
            {
              
            }
            return View ();
        }
    }
}
