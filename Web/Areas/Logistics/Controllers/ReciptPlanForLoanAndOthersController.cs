using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.Logistics;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Logistics.Controllers
{
    public class ReciptPlanForLoanAndOthersController : Controller
    {
        //
        // GET: /Logistics/ReciptPlanForLoanAndOthers/
        private readonly ILoanReciptPlanService _loanReciptPlanService;
        private readonly ICommonService _commonService;
        public ReciptPlanForLoanAndOthersController(ILoanReciptPlanService loanReciptPlanService,ICommonService commonService)
        {
            _loanReciptPlanService = loanReciptPlanService;
            _commonService = commonService;

        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name");
            ViewBag.CommodityID = new SelectList(_commonService.GetCommodities(), "CommodityID", "Name");
            ViewBag.SourceHubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name");
            ViewBag.CommodityTypeID = new SelectList(_commonService.GetCommodityTypes(), "CommodityTypeID", "Name");
            ViewBag.CommoditySourceID = new SelectList(_commonService.GetCommoditySource(), "CommoditySourceID", "Name");
            ViewBag.HubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name");
            var loanReciptPlanViewModel = new LoanReciptPlanViewModel();
            return View(loanReciptPlanViewModel);

        }
        [HttpPost]
        public ActionResult Create(LoanReciptPlanViewModel loanReciptPlanViewModel)
        {
            if (ModelState.IsValid && loanReciptPlanViewModel!=null)
            {
                var loanReciptPlan = GetLoanReciptPlan(loanReciptPlanViewModel);
                _loanReciptPlanService.AddLoanReciptPlan(loanReciptPlan);
                ModelState.AddModelError("Sucess",@"Sucessfully Saved");
                return RedirectToAction("Index");
            }
            return View(loanReciptPlanViewModel);
        }
        public ActionResult Edit(int id)
        {
            var loanReciptPlan = _loanReciptPlanService.FindById(id);
            if (loanReciptPlan==null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name",loanReciptPlan.ProgramID);
            ViewBag.CommodityID = new SelectList(_commonService.GetCommodities(), "CommodityID", "Name",loanReciptPlan.CommodityID);
            ViewBag.SourceHubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name",loanReciptPlan.SourceHubID);
            ViewBag.CommodityTypeID = new SelectList(_commonService.GetCommodityTypes(), "CommodityTypeID", "Name");
            ViewBag.CommoditySourceID = new SelectList(_commonService.GetCommoditySource(), "CommoditySourceID", "Name",loanReciptPlan.CommoditySourceID);
            ViewBag.HubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name",loanReciptPlan.HubID);
            return View(loanReciptPlan);
        }
        [HttpPost]
        public ActionResult Edit(LoanReciptPlan loanReciptPlan)
        {
            if (ModelState.IsValid && loanReciptPlan!=null )
            {
                _loanReciptPlanService.EditLoanReciptPlan(loanReciptPlan);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("Errors",@"Unable to update please check fields");
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name", loanReciptPlan.ProgramID);
            ViewBag.CommodityID = new SelectList(_commonService.GetCommodities(), "CommodityID", "Name", loanReciptPlan.CommodityID);
            ViewBag.SourceHubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name", loanReciptPlan.SourceHubID);
            ViewBag.CommodityTypeID = new SelectList(_commonService.GetCommodityTypes(), "CommodityTypeID", "Name");
            ViewBag.CommoditySourceID = new SelectList(_commonService.GetCommoditySource(), "CommoditySourceID", "Name", loanReciptPlan.CommoditySourceID);
            ViewBag.HubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name", loanReciptPlan.HubID);
            return View(loanReciptPlan);
        }
        private LoanReciptPlan GetLoanReciptPlan(LoanReciptPlanViewModel loanReciptPlanViewModel)
        {
          
                var loanReciptPlan = new LoanReciptPlan()
                    {
                        ProgramID = loanReciptPlanViewModel.ProgramID,
                        CommodityID = loanReciptPlanViewModel.ProgramID,
                        CommoditySourceID = loanReciptPlanViewModel.CommoditySourceID,
                        ShippingInstructionID = _commonService.GetShippingInstruction(loanReciptPlanViewModel.SiNumber),
                        SourceHubID = loanReciptPlanViewModel.SourceHubID,
                        HubID = loanReciptPlanViewModel.HubID,
                        ProjectCode = loanReciptPlanViewModel.ProjectCode,
                        ReferenceNumber = loanReciptPlanViewModel.RefeenceNumber,
                        Quantity = loanReciptPlanViewModel.Quantity,
                        CreatedDate = DateTime.Today,
                        StatusID = (int)LocalPurchaseStatus.Draft
                    };
                return loanReciptPlan;
        }
        public ActionResult LoanReciptPlan_Read([DataSourceRequest] DataSourceRequest request)
        {
            var reciptPlan = _loanReciptPlanService.GetAllLoanReciptPlan().OrderByDescending(m => m.LoanReciptPlanID);
            var reciptPlanToDisplay = BindToViewModel(reciptPlan);
           return Json(reciptPlanToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<LoanReciptPlanViewModel> BindToViewModel(IEnumerable<LoanReciptPlan> loanReciptPlans)
        {
            return (from loanReciptPlan in loanReciptPlans
                    select new LoanReciptPlanViewModel
                        {
                            LoanReciptPlanID = loanReciptPlan.LoanReciptPlanID,
                            ProgramName = loanReciptPlan.Program.Name,
                            CommodityName = loanReciptPlan.Commodity.Name,
                            CommoditySourceName = loanReciptPlan.CommoditySource.Name,
                            HubName = loanReciptPlan.OriginHub.Name,
                            SourceHubName = loanReciptPlan.Hub.Name,
                            RefeenceNumber = loanReciptPlan.ReferenceNumber,
                            SiNumber = loanReciptPlan.ShippingInstruction.Value,
                            ProjectCode = loanReciptPlan.ProjectCode,
                            Quantity = loanReciptPlan.Quantity,
                            StatusID = loanReciptPlan.StatusID,
                            Status = _commonService.GetStatusName(WORKFLOW.LocalPUrchase, loanReciptPlan.StatusID)
                        });

        }
        
    }
}
