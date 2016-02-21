using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Areas.Logistics.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ICommonService = Cats.Services.Common.ICommonService;

namespace Cats.Areas.Logistics.Controllers
{
    [Authorize]
    public class ReciptPlanForLoanController : Controller
    {
        //
        // GET: /Logistics/ReciptPlanForLoanAndOthers/
        private readonly ILoanReciptPlanService _loanReciptPlanService;
        private readonly ICommonService _commonService;
        private readonly ILoanReciptPlanDetailService _loanReciptPlanDetailService;
        private readonly IUserAccountService _userAccountService;
        private readonly ICommodityService _commodityService;
        public ReciptPlanForLoanController(ILoanReciptPlanService loanReciptPlanService,ICommonService commonService,
                                           ILoanReciptPlanDetailService loanReciptPlanDetailService,IUserAccountService userAccountService,
                                           ICommodityService commodityService)
        {
            _loanReciptPlanService = loanReciptPlanService;
            _commonService = commonService;
            _loanReciptPlanDetailService = loanReciptPlanDetailService;
            _userAccountService = userAccountService;
            _commodityService = commodityService;

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
            ViewBag.CommoditySourceID = new SelectList(_commonService.GetCommoditySource(), "CommoditySourceID", "Name",2);
            ViewBag.LoanSource = new SelectList(_commonService.GetDonors(), "DonorID", "Name");
            //ViewBag.HubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name");
            var loanReciptPlanViewModel = new LoanReciptPlanViewModel();
            loanReciptPlanViewModel.CommoditySourceName = _commonService.GetCommditySourceName(2);//commodity source for Loan
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
            ViewBag.CommodityID = new SelectList(_commodityService.FindBy(m=>m.ParentID==loanReciptPlan.Commodity.ParentID), "CommodityID", "Name",loanReciptPlan.CommodityID);
            //ViewBag.SourceHubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name",loanReciptPlan.SourceHubID);
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
            ViewBag.CommodityTypeID = new SelectList(_commonService.GetCommodityTypes(), "CommodityTypeID", "Name");
            ViewBag.CommoditySourceID = new SelectList(_commonService.GetCommoditySource(), "CommoditySourceID", "Name", loanReciptPlan.CommoditySourceID);
            return View(loanReciptPlan);
        }
        private LoanReciptPlan GetLoanReciptPlan(LoanReciptPlanViewModel loanReciptPlanViewModel)
        {
          
                var loanReciptPlan = new LoanReciptPlan()
                    {
                        ProgramID = loanReciptPlanViewModel.ProgramID,
                        CommodityID = loanReciptPlanViewModel.CommodityID,
                        CommoditySourceID = 2,//only for loan
                        ShippingInstructionID = _commonService.GetShippingInstruction(loanReciptPlanViewModel.SiNumber),
                        LoanSource = loanReciptPlanViewModel.LoanSource.ToString(),
                        //HubID = loanReciptPlanViewModel.HubID,
                        ProjectCode = loanReciptPlanViewModel.ProjectCode,
                        ReferenceNumber = loanReciptPlanViewModel.RefeenceNumber,
                        Quantity = loanReciptPlanViewModel.Quantity,
                        CreatedDate = DateTime.Today,
                        StatusID = (int)LocalPurchaseStatus.Draft,
                        IsFalseGRN = loanReciptPlanViewModel.IsFalseGRN
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
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var list = new List<LoanReciptPlanViewModel>();
            var reciptPlans = loanReciptPlans as List<LoanReciptPlan> ?? loanReciptPlans.ToList();
            foreach (var loanReciptPlan in reciptPlans)
            {
                var loanSource = loanReciptPlan.LoanSource;
                var intLoanSource = Convert.ToInt32(loanSource);
                var firstOrDefault =
                    _commonService.GetDonors(d => d.DonorID == intLoanSource).FirstOrDefault();
                var loan = new LoanReciptPlanViewModel
                               {
                                   LoanReciptPlanID = loanReciptPlan.LoanReciptPlanID,
                                   ProgramName = loanReciptPlan.Program.Name,
                                   CommodityName = loanReciptPlan.Commodity.Name,
                                   CommoditySourceName = loanReciptPlan.CommoditySource.Name,
                                   LoanSource = intLoanSource
                               };

                if (firstOrDefault != null) loan.Donor = firstOrDefault.Name;
                //SourceHubName = loanReciptPlan.Hub.Name,
                loan.RefeenceNumber = loanReciptPlan.ReferenceNumber;
                loan.SiNumber = loanReciptPlan.ShippingInstruction.Value;
                loan.ProjectCode = loanReciptPlan.ProjectCode;
                loan.Quantity = loanReciptPlan.Quantity;
                loan.StatusID = loanReciptPlan.StatusID;
                loan.CreatedDate = loanReciptPlan.CreatedDate.ToCTSPreferedDateFormat(datePref);
                loan.Status = _commonService.GetStatusName(WORKFLOW.LocalPUrchase, loanReciptPlan.StatusID);
                loan.IsFalseGRN = loanReciptPlan.IsFalseGRN;

                list.Add(loan);

            }

            return list;
            //return (from loanReciptPlan in reciptPlans
            //        let loanSource = loanReciptPlan.LoanSource
            //        where loanSource != null
            //        let intLoanSource = Convert.ToInt32(loanSource)
            //        let firstOrDefault = _commonService.GetDonors(d=>d.DonorID == Convert.ToInt32(loanSource)).FirstOrDefault()
            //        where firstOrDefault != null
            //        select new LoanReciptPlanViewModel
            //            {
            //                LoanReciptPlanID = loanReciptPlan.LoanReciptPlanID,
            //                ProgramName = loanReciptPlan.Program.Name,
            //                CommodityName = loanReciptPlan.Commodity.Name,
            //                CommoditySourceName = loanReciptPlan.CommoditySource.Name,
            //                LoanSource = intLoanSource,
            //                Donor = firstOrDefault.Name,
            //                //SourceHubName = loanReciptPlan.Hub.Name,
            //                RefeenceNumber = loanReciptPlan.ReferenceNumber,
            //                SiNumber = loanReciptPlan.ShippingInstruction.Value,
            //                ProjectCode = loanReciptPlan.ProjectCode,
            //                Quantity = loanReciptPlan.Quantity,
            //                StatusID = loanReciptPlan.StatusID,
            //                CreatedDate = loanReciptPlan.CreatedDate.ToCTSPreferedDateFormat(datePref),
            //                Status = _commonService.GetStatusName(WORKFLOW.LocalPUrchase, loanReciptPlan.StatusID),
            //                IsFalseGRN = loanReciptPlan.IsFalseGRN
            //            });

        }

        public ActionResult Approve(int id)
        {
            var loanReciptPlan = _loanReciptPlanService.FindBy(m=>m.LoanReciptPlanID==id).FirstOrDefault();
            if (loanReciptPlan == null)
            {
                return HttpNotFound();

            }
            _loanReciptPlanService.ApproveRecieptPlan(loanReciptPlan);
            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id)
        {
            var loanReciptPlan = _loanReciptPlanService.FindById(id);
           


            if (loanReciptPlan==null)
            {
                return HttpNotFound();
            }

            var loan = new LoanReciptPlanViewModel
            {
                RefeenceNumber = loanReciptPlan.ReferenceNumber,
                SiNumber = loanReciptPlan.ShippingInstruction.Value,
                ProjectCode = loanReciptPlan.ProjectCode,
                Quantity = loanReciptPlan.Quantity,
                ProgramName = loanReciptPlan.Program.Name,
                CommodityName = loanReciptPlan.Commodity.Name,
                CommoditySourceName = loanReciptPlan.CommoditySource.Name,
                LoanReciptPlanID = loanReciptPlan.LoanReciptPlanID
            };

            var loanSource = loanReciptPlan.LoanSource;
            var intLoanSource = Convert.ToInt32(loanSource);
            var firstOrDefault =
                _commonService.GetDonors(d => d.DonorID == intLoanSource).FirstOrDefault();
            if (firstOrDefault != null) loan.Donor = firstOrDefault.Name;
            loan.StatusID = loanReciptPlan.StatusID;
            return View(loan);
        }
        public JsonResult GetMaxSINo()
        {
            var result =
                _loanReciptPlanService.GetAllLoanReciptPlan().Select(m => m.ShippingInstruction.Value);
            var siList = result.Select(si => Regex.Match(si, @"\d+").Value).Select(data => Convert.ToInt32(data)).ToList();
            int resultInt = siList.Max() + 1;
           return Json("LOAN-" + resultInt, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoanReciptPlanDetail_Read([DataSourceRequest] DataSourceRequest request, int loanReciptPlanID)
        {
            var loanReciptPlanDetail = _loanReciptPlanDetailService.FindBy(m=>m.LoanReciptPlanID==loanReciptPlanID);
            var loanReciptPlanDetailToDisplay = BidToLoanReciptPlanDetail(loanReciptPlanDetail);
            return Json(loanReciptPlanDetailToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<LoanReciptPlanWithDetailViewModel> BidToLoanReciptPlanDetail(IEnumerable<LoanReciptPlanDetail> loanReciptPlanDetails )
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from loanReciptPlanDetail in loanReciptPlanDetails
                    select new LoanReciptPlanWithDetailViewModel
                        {
                            LoanReciptPlanDetailID = loanReciptPlanDetail.LoanReciptPlanDetailID,
                            LoanReciptPlanID = loanReciptPlanDetail.LoanReciptPlanID,
                            HubID = loanReciptPlanDetail.HubID,
                            HubName = loanReciptPlanDetail.Hub.Name,
                           // MemoRefrenceNumber = loanReciptPlanDetail.MemoReferenceNumber,
                            Amount = loanReciptPlanDetail.RecievedQuantity,
                            CreatedDate = loanReciptPlanDetail.RecievedDate.ToCTSPreferedDateFormat(datePref),
                            Remaining = _loanReciptPlanDetailService.GetRemainingQuantity(loanReciptPlanDetail.LoanReciptPlanID)
                            
                        });
        }

        public ActionResult LoanReciptPlanDetail_Delete([DataSourceRequest] DataSourceRequest request, LoanReciptPlanWithDetailViewModel loanReciptPlanWithDetailViewModel)
        {
            if (loanReciptPlanWithDetailViewModel != null)
            {
                _loanReciptPlanDetailService.DeleteById(loanReciptPlanWithDetailViewModel.LoanReciptPlanDetailID);

            }
            return Json(ModelState.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
           
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
            ViewBag.Errors = "Out of Quantity! The Remaining Quantity is:";
            return View(loanReciptPlanViewModel);
        }
        [HttpPost]
        public ActionResult ReciptPlan(LoanReciptPlanWithDetailViewModel loanReciptPlanDetail)
        {
            var userID = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).UserProfileID;
            if (ModelState.IsValid && loanReciptPlanDetail!=null)
            {
                var loanReciptPlanModel = new LoanReciptPlanDetail()
                    {
                        LoanReciptPlanID = loanReciptPlanDetail.LoanReciptPlanID,
                        HubID = loanReciptPlanDetail.HubID,
                        //MemoReferenceNumber = loanReciptPlanDetail.MemoRefrenceNumber,
                        RecievedQuantity = loanReciptPlanDetail.Amount,
                        RecievedDate = DateTime.Today,
                        ApprovedBy = userID
                    };
                _loanReciptPlanDetailService.AddRecievedLoanReciptPlanDetail(loanReciptPlanModel);
                return RedirectToAction("Detail", new {id = loanReciptPlanDetail.LoanReciptPlanID});
            }
            ViewBag.HubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name");
            return View(loanReciptPlanDetail);
        }
        public ActionResult Delete(int id)
        {
            var loanReciptPlan = _loanReciptPlanService.FindById(id);
            if (loanReciptPlan!=null)
            {
                if (loanReciptPlan.StatusID==(int)LocalPurchaseStatus.Draft)
                {
                    _loanReciptPlanService.DeleteLoanWithDetail(loanReciptPlan);
                    return RedirectToAction("Index","ReciptPlanForLoan");
                }
                else
                {
                    if (_loanReciptPlanService.DeleteLoanReciptAllocation(loanReciptPlan))
                    {
                        _loanReciptPlanService.DeleteLoanWithDetail(loanReciptPlan);
                        return RedirectToAction("Index", "ReciptPlanForLoan");
                    }
                    else
                    {
                        TempData["Received"] = "Loan Recipt Plan can not be Deleted. It has already been Received!";
                        return RedirectToAction("Index");
                    }
                }
                
            }
            return RedirectToAction("Index", "ReciptPlanForLoan");
        }
        public ActionResult Revert(int id)
        {
            var loanReciptPlan = _loanReciptPlanService.FindById(id);

            if (loanReciptPlan != null)
            {
                if (!_loanReciptPlanService.DeleteLoanReciptAllocation(loanReciptPlan))
                {
                    TempData["Received"] = "Loan Recipt Plan can not be Reverted. It has already been Received!";
                    return RedirectToAction("Index");
                }
                loanReciptPlan.StatusID = (int)LocalPurchaseStatus.Draft;
                _loanReciptPlanService.EditLoanReciptPlan(loanReciptPlan);
                TempData["Reverted"] = "Loan Recipt Plan is Reverted to Draft";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Unable to revert Loan Recipt Plan!";
            return RedirectToAction("Index","ReciptPlanForLoan");
        }
       
    }
}
