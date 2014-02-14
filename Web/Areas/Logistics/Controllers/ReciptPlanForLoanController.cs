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
    public class ReciptPlanForLoanController : Controller
    {
        //
        // GET: /Logistics/ReciptPlanForLoanAndOthers/
        private readonly ILoanReciptPlanService _loanReciptPlanService;
        private readonly ICommonService _commonService;
        private readonly ILoanReciptPlanDetailService _loanReciptPlanDetailService;
        public ReciptPlanForLoanController(ILoanReciptPlanService loanReciptPlanService,ICommonService commonService,
                                                    ILoanReciptPlanDetailService loanReciptPlanDetailService)
        {
            _loanReciptPlanService = loanReciptPlanService;
            _commonService = commonService;
            _loanReciptPlanDetailService = loanReciptPlanDetailService;

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
            //ViewBag.HubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name");
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
            //ViewBag.HubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name",loanReciptPlan.HubID);
            return View(loanReciptPlan);
        }
        [HttpPost]
        public ActionResult Edit(LoanReciptPlan loanReciptPlan)
        {
            if (ModelState.IsValid && loanReciptPlan!=null )
            {
                _loanReciptPlanService.EditLoanReciptPlan(loanReciptPlan);
                return RedirectToAction("Detail",new {id=loanReciptPlan.LoanReciptPlanID});
            }
            ModelState.AddModelError("Errors",@"Unable to update please check fields");
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name", loanReciptPlan.ProgramID);
            ViewBag.CommodityID = new SelectList(_commonService.GetCommodities(), "CommodityID", "Name", loanReciptPlan.CommodityID);
            ViewBag.SourceHubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name", loanReciptPlan.SourceHubID);
            ViewBag.CommodityTypeID = new SelectList(_commonService.GetCommodityTypes(), "CommodityTypeID", "Name");
            ViewBag.CommoditySourceID = new SelectList(_commonService.GetCommoditySource(), "CommoditySourceID", "Name", loanReciptPlan.CommoditySourceID);
            //ViewBag.HubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name", loanReciptPlan.HubID);
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
                        //HubID = loanReciptPlanViewModel.HubID,
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
                            HubName = loanReciptPlan.Hub.Name,
                            SourceHubName = loanReciptPlan.Hub.Name,
                            RefeenceNumber = loanReciptPlan.ReferenceNumber,
                            SiNumber = loanReciptPlan.ShippingInstruction.Value,
                            ProjectCode = loanReciptPlan.ProjectCode,
                            Quantity = loanReciptPlan.Quantity,
                            StatusID = loanReciptPlan.StatusID,
                            Status = _commonService.GetStatusName(WORKFLOW.LocalPUrchase, loanReciptPlan.StatusID)
                        });

        }

        //public ActionResult Approve(int id)
        //{
        //    var loanReciptPlan = _loanReciptPlanService.FindById(id);
        //    if (loanReciptPlan==null)
        //    {
        //        return HttpNotFound();

        //    }
        //    _loanReciptPlanService.ApproveReciptPlan(loanReciptPlan);
        //}
        public ActionResult Detail(int id)
        {
            var loanReciptPlan = _loanReciptPlanService.FindById(id);
            if (loanReciptPlan==null)
            {
                return HttpNotFound();
            }
            return View(loanReciptPlan);
        }
        public ActionResult LoanReciptPlanDetail_Read([DataSourceRequest] DataSourceRequest request, int loanReciptPlanID)
        {
            var loanReciptPlanDetail = _loanReciptPlanDetailService.FindBy(m=>m.LoanReciptPlanID==loanReciptPlanID);
            var loanReciptPlanDetailToDisplay = BidToLoanReciptPlanDetail(loanReciptPlanDetail);
            return Json(loanReciptPlanDetailToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<LoanReciptPlanWithDetailViewModel> BidToLoanReciptPlanDetail(IEnumerable<LoanReciptPlanDetail> loanReciptPlanDetails )
        {
            return (from loanReciptPlanDetail in loanReciptPlanDetails
                    select new LoanReciptPlanWithDetailViewModel
                        {
                            LoanReciptPlanDetailID = loanReciptPlanDetail.LoanReciptPlanDetailID,
                            LoanReciptPlanID = loanReciptPlanDetail.LoanReciptPlanID,
                            HubID = loanReciptPlanDetail.HubID,
                            HubName = loanReciptPlanDetail.Hub.Name,
                            MemoRefrenceNumber = loanReciptPlanDetail.MemoReferenceNumber,
                            Amount = loanReciptPlanDetail.RecievedQuantity,
                            Remaining = _loanReciptPlanDetailService.GetRemainingQuantity(loanReciptPlanDetail.LoanReciptPlanID)
                        });
        }
        public ActionResult ReciptPlan(int id)
        {
            var loanReciptPlan = _loanReciptPlanService.FindById(id);
            if (loanReciptPlan==null)
            {
                return HttpNotFound();
            }
            ViewBag.HubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name");
            var loanReciptPlanViewModel = new LoanReciptPlanWithDetailViewModel()
                {
                    LoanReciptPlanID = id,
                    TotalAmount = loanReciptPlan.Quantity,
                    Remaining = _loanReciptPlanDetailService.GetRemainingQuantity(id)
                };
            ViewBag.Errors = "Out of Quantity! The Remaining Quantity is";
            return View(loanReciptPlanViewModel);
        }
        [HttpPost]
        public ActionResult ReciptPlan(LoanReciptPlanWithDetailViewModel loanReciptPlanDetail)
        {
            if (ModelState.IsValid && loanReciptPlanDetail!=null)
            {
                var loanReciptPlanModel = new LoanReciptPlanDetail()
                    {
                        LoanReciptPlanID = loanReciptPlanDetail.LoanReciptPlanID,
                        HubID = loanReciptPlanDetail.HubID,
                        MemoReferenceNumber = loanReciptPlanDetail.MemoRefrenceNumber,
                        RecievedQuantity = loanReciptPlanDetail.Amount,
                        RecievedDate = DateTime.Today,
                        ApprovedBy = 1
                    };
                _loanReciptPlanDetailService.AddRecievedLoanReciptPlanDetail(loanReciptPlanModel);
                return RedirectToAction("Detail", new {id = loanReciptPlanDetail.LoanReciptPlanID});
            }
            ViewBag.HubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name");
            return View(loanReciptPlanDetail);
        }
       
    }
}
