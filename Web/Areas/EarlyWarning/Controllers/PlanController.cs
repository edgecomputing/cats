using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
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
        private IHRDPlanService _hrdPlanService;
        private IUserAccountService _userAccountService;
        public PlanController(IHRDPlanService hrdPlanService,IUserAccountService userAccountService)
        {
            _hrdPlanService = hrdPlanService;
            _userAccountService = userAccountService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan_Read([DataSourceRequest] DataSourceRequest request)
        {

            var plans = _hrdPlanService.GetAllHRDPlan().OrderByDescending(m=>m.PlanID);
            var plansToDisplay = GetPlan(plans).ToList();
            return Json(plansToDisplay.ToDataSourceResult(request));
        }

        private IEnumerable<PlanViewModel> GetPlan(IEnumerable<HRDPlan>  plans)
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
                            Program = plan.Program.Name

                        });
        }
        public ActionResult Create()
        {
            var plan = new HRDPlan();
            ViewBag.ProgramID = _hrdPlanService.GetPrograms();
            return View(plan);
        }

        [HttpPost]
        public ActionResult Create(HRDPlan hrdPlan)
        {
            if (ModelState.IsValid)
            {
                _hrdPlanService.AddHRDPlan(hrdPlan);
                return RedirectToAction("Index");

            }
            return View(hrdPlan);
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
