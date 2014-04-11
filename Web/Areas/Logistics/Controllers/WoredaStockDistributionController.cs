using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.Logistics;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Cats.Services.Transaction;
namespace Cats.Areas.Logistics.Controllers
{
    public class WoredaStockDistributionController : Controller
    {

        private readonly IUtilizationHeaderSerivce _utilizationService;
        private readonly IUtilizationDetailSerivce _utilizationDetailSerivce;
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly UserAccountService _userAccountService;
        private readonly ICommonService _commonService;
        private readonly IRegionalRequestService _regionalRequestService;
        private readonly IReliefRequisitionDetailService _reliefRequisitionDetailService;
        private readonly ITransactionService _transactionService;
        public WoredaStockDistributionController(
            IUtilizationHeaderSerivce utilizationService,
            IUtilizationDetailSerivce utilizationDetailSerivce, 
            UserAccountService userAccountService,
            ICommonService commonService, 
            IRegionalRequestService regionalRequestService,
            IReliefRequisitionDetailService reliefRequisitionDetailService,
            IReliefRequisitionService reliefRequisitionService,
            ITransactionService transactionService)
        {
            _utilizationService = utilizationService;
            _utilizationDetailSerivce = utilizationDetailSerivce;
            _userAccountService = userAccountService;
            _commonService = commonService;
            _regionalRequestService = regionalRequestService;
            _reliefRequisitionDetailService = reliefRequisitionDetailService;
            _reliefRequisitionService = reliefRequisitionService;
            _transactionService = transactionService;
        }

        //
        // GET: /Logistics/Utilization/

        public ActionResult Index()
        {
           
            ViewBag.RegionCollection = _commonService.GetAminUnits(t => t.AdminUnitTypeID == 2);
           
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms().Take(2),"ProgramId","Name");
            LookUps();
            if (ViewBag.Errors == "errors")
                ModelState.AddModelError("Errors", @"Please Select values from the Left Side");
            return View();
        }

        public ActionResult Create(int Woreda = -1, int planID = -1,int programID=-1, int month = -1)
        {
           if(Woreda==-1||planID==-1 || programID==-1 || month==-1)
           {
              
               LookUps();
               ViewBag.Errors = "errors";
              
               return RedirectToAction("Index");

           }
            var woredaStockDistributionViewModel = new WoredaStockDistributionWithDetailViewModel();
            //woredaStockDistributionViewModel.WoredaDistributionDetailViewModels
            var woredaStockDistribution = CheckWoredaDistribution(Woreda, planID, month);
            if (woredaStockDistribution!=null)
            {
                LookUps(woredaStockDistribution);
                woredaStockDistributionViewModel = woredaStockDistribution;
                woredaStockDistributionViewModel.PlanID = planID;
                woredaStockDistributionViewModel.WoredaID = Woreda;
                woredaStockDistributionViewModel.Month = month;
                woredaStockDistributionViewModel.ProgramID = programID;
                return View(woredaStockDistributionViewModel);
            }
            //ModelState.AddModelError("Errora",@"Request is Not Created for this plan");
            LookUps();
            ViewBag.WoredaName =_commonService.GetAminUnits(m => m.AdminUnitID == woredaStockDistribution.WoredaID).FirstOrDefault().Name;
            var distributionDetail = _commonService.GetFDPs(Woreda);
            //var listOfFdps = GetWoredaStockDistribution(distributionDetail);
            //woredaStockDistributionViewModel.WoredaDistributionDetailViewModels = listOfFdps;
            woredaStockDistributionViewModel.PlanID = planID;
            woredaStockDistributionViewModel.WoredaID = Woreda;
            woredaStockDistributionViewModel.Month = month;
            woredaStockDistributionViewModel.ProgramID = programID;

            return View(woredaStockDistributionViewModel);
        }

        public WoredaStockDistributionWithDetailViewModel CheckWoredaDistribution(int woredaID = -1, int planID = -1, int month = -1)
        {
            if (woredaID == -1 || planID == -1 || month == -1)
                return null;
            //var woredaStockDistribution = _utilizationService.FindBy(m => m.WoredaID == woredaID && m.Month == month && m.PlanID == planID).FirstOrDefault();
            var zoneID = _commonService.GetZoneID(woredaID);
            var regionID = _commonService.GetRegion(zoneID);
            var regionalRequest =_regionalRequestService.FindBy(m => m.PlanID == planID && m.Month == month && m.RegionID==regionID).FirstOrDefault();
            if (regionalRequest!=null)
            {
                var requisition =
                    _reliefRequisitionService.FindBy(
                 m => m.RegionalRequestID == regionalRequest.RegionalRequestID && m.ZoneID == zoneID);
                     
                if (requisition != null)
                {
                    var woredaStockDistribution = _utilizationService.FindBy(m => m.WoredaID == woredaID && m.Month == month && m.PlanID == planID).FirstOrDefault();
                    if (woredaStockDistribution==null)
                    {
                       
                        var fdpStockDistribution = _commonService.GetFDPs(woredaID);

                        var woredaDistributionDetailViewModels = new List<WoredaDistributionDetailViewModel>();
                        foreach (var reliefRequisition in requisition)
                        {
                            var detail = GetWoredaStockDistribution(fdpStockDistribution, reliefRequisition);
                            if (detail!=null)
                            {
                                woredaDistributionDetailViewModels.Add(detail);
                            }
                          
                        }
                        var listOfFdps = new WoredaStockDistributionWithDetailViewModel
                            {
                                WoredaDistributionDetailViewModels = woredaDistributionDetailViewModels,
                                
                            };
                            
                        return listOfFdps;
                    }

                    var woredaStockDistributionWithDetailViewModel = new WoredaStockDistributionWithDetailViewModel()
                    {
                    WoredaStockDistributionID = woredaStockDistribution.WoredaStockDistributionID,
                    WoredaID = woredaStockDistribution.WoredaID,
                    ProgramID = woredaStockDistribution.ProgramID,
                    PlanID = woredaStockDistribution.PlanID,
                    Month = woredaStockDistribution.WoredaStockDistributionID,
                    SupportTypeID = woredaStockDistribution.SupportTypeID,
                    ActualBeneficairies = woredaStockDistribution.ActualBeneficairies,
                    MaleBetween5And18Years = woredaStockDistribution.MaleBetween5And18Years,
                    FemaleLessThan5Years = woredaStockDistribution.FemaleLessThan5Years,
                    MaleAbove18Years = woredaStockDistribution.MaleAbove18Years,
                    MaleLessThan5Years = woredaStockDistribution.MaleLessThan5Years,
                    FemaleAbove18Years = woredaStockDistribution.FemaleAbove18Years,
                    FemaleBetween5And18Years = woredaStockDistribution.FemaleBetween5And18Years,
                    WoredaDistributionDetailViewModels = (from woredaDistributionDetail in woredaStockDistribution.WoredaStockDistributionDetails
                                                          from reliefRequisition in requisition
                                                          where woredaDistributionDetail.CommodityID==reliefRequisition.CommodityID
                                                          select new WoredaDistributionDetailViewModel()
                                                              {
                                                                  WoredaStockDistributionDetailID = woredaDistributionDetail.WoredaStockDistributionDetailID,
                                                                  FdpId = woredaDistributionDetail.FdpId,
                                                                  FDP = woredaDistributionDetail.FDP.Name,
                                                                  CommodityID = GetRequisionInfo(reliefRequisition.RequisitionID, woredaDistributionDetail.FdpId).CommodityID,
                                                                  CommodityName = GetRequisionInfo(reliefRequisition.RequisitionID, woredaDistributionDetail.FdpId).CommodityName,
                                                                  AllocatedAmount = GetRequisionInfo(reliefRequisition.RequisitionID, woredaDistributionDetail.FdpId).AllocatedAmount,
                                                                  NumberOfBeneficiaries = GetRequisionInfo(reliefRequisition.RequisitionID, woredaDistributionDetail.FdpId).BeneficaryNumber,
                                                                  //RequisitionDetailViewModel = GetRequisionInfo(reliefRequisition.RequisitionID, woredaDistributionDetail.FdpId),
                                                                  RequistionNo = reliefRequisition.RequisitionNo,
                                                                  Round = reliefRequisition.Round,
                                                                  Month = RequestHelper.MonthName(reliefRequisition.Month),
                                                                  BeginingBalance = woredaDistributionDetail.StartingBalance,
                                                                  EndingBalance = woredaDistributionDetail.EndingBalance,
                                                                  DistributedAmount = woredaDistributionDetail.DistributedAmount,
                                                                  TotalIn = woredaDistributionDetail.TotalIn,
                                                                  TotalOut = woredaDistributionDetail.TotoalOut,
                                                                  LossAmount = woredaDistributionDetail.LossAmount,
                                                                  LossReason = woredaDistributionDetail.LossReason,
                                                                  


                                                              }
                         ).ToList()


                };

                    return woredaStockDistributionWithDetailViewModel;
                }
                return null;

            }
            return null;
           
        }
        private RequisitionDetailViewModel GetRequisionInfo(int requisitionID,int fdpID)
        {
            var requisitionDetail =_reliefRequisitionDetailService.FindBy(m => m.RequisitionID == requisitionID && m.FDPID == fdpID).FirstOrDefault();
            if (requisitionDetail != null)
            {
                var requisitonDetailInfo = new RequisitionDetailViewModel()
                    {
                        CommodityID = requisitionDetail.CommodityID,
                        CommodityName = requisitionDetail.Commodity.Name,
                        BeneficaryNumber = requisitionDetail.BenficiaryNo,
                        AllocatedAmount = requisitionDetail.Amount
                    };
                return requisitonDetailInfo;
            }
            return null;
        }
       
        private WoredaStockDistribution GetWoredaDetailMOdel(WoredaStockDistributionWithDetailViewModel distributionViewModel)
        {
            if (distributionViewModel!=null)
            {
                var distributionModel = new WoredaStockDistribution()
                    {
                        WoredaStockDistributionID = distributionViewModel.WoredaStockDistributionID,
                        ProgramID = distributionViewModel.ProgramID,
                        PlanID = distributionViewModel.PlanID,
                        WoredaID = distributionViewModel.WoredaID,
                        Month = distributionViewModel.Month,
                        ActualBeneficairies = distributionViewModel.ActualBeneficairies,
                        MaleBetween5And18Years = distributionViewModel.MaleBetween5And18Years,
                        MaleLessThan5Years = distributionViewModel.MaleLessThan5Years,
                        MaleAbove18Years = distributionViewModel.MaleAbove18Years,
                        FemaleAbove18Years = distributionViewModel.FemaleAbove18Years,
                        FemaleBetween5And18Years = distributionViewModel.FemaleBetween5And18Years,
                        FemaleLessThan5Years = distributionViewModel.FemaleLessThan5Years,
                        SupportTypeID = distributionViewModel.SupportTypeID

                    };
                return distributionModel;
            }
            return null;
        }
        [HttpPost]
        public ActionResult Create(WoredaStockDistributionWithDetailViewModel woredaStockDistribution)
        {
            if (woredaStockDistribution!=null && ModelState.IsValid)
            {
                var utilization = _utilizationService.FindBy(m => m.WoredaID == woredaStockDistribution.WoredaID && 
                                                             m.PlanID == woredaStockDistribution.PlanID && 
                                                             m.Month==woredaStockDistribution.Month).FirstOrDefault();
                
                if (utilization==null)
                {
                    var bindToModel = GetWoredaDetailMOdel(woredaStockDistribution);
                    var userProfileId = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).UserProfileID;
                    bindToModel.DistributedBy = userProfileId;
                    bindToModel.DistributionDate = DateTime.Now;
                    var saved = _utilizationService.AddHeaderDistribution(bindToModel);
                    if (saved)
                    {
                        //var fdpStockDistribution = _commonService.GetFDPs(woredaStockDistribution.WoredaID);

                        //var woredaDistributionDetailViewModels = GetWoredaStockDistribution(fdpStockDistribution,null);


                        var distributionHeader =_utilizationService.FindBy(m => m.WoredaID == woredaStockDistribution.WoredaID &&
                                                            m.PlanID == woredaStockDistribution.PlanID &&
                                                            m.Month == woredaStockDistribution.Month).FirstOrDefault();
                       
                        foreach (var woredaDistributionDetailViewModel in woredaStockDistribution.WoredaDistributionDetailViewModels)
                        {
                            var distributionDetailModel = new WoredaStockDistributionDetail()
                                {
                                    WoredaStockDistributionID = distributionHeader.WoredaStockDistributionID,
                                    FdpId = woredaDistributionDetailViewModel.FdpId,
                                    CommodityID = woredaDistributionDetailViewModel.CommodityID,
                                    StartingBalance = woredaDistributionDetailViewModel.BeginingBalance,
                                    EndingBalance = woredaDistributionDetailViewModel.EndingBalance,
                                    TotalIn = woredaDistributionDetailViewModel.TotalIn,
                                    TotoalOut = woredaDistributionDetailViewModel.TotalOut,
                                    LossAmount = woredaDistributionDetailViewModel.LossAmount,
                                    LossReason = woredaDistributionDetailViewModel.LossReason,
                                    DistributedAmount = woredaDistributionDetailViewModel.DistributedAmount



                                };
                            _utilizationDetailSerivce.AddDetailDistribution(distributionDetailModel);
                        }

                        ModelState.AddModelError("Success", @"Distribution Information Successfully Saved");
                        LookUps(woredaStockDistribution);


                        if (distributionHeader != null)
                        {
                            _transactionService.PostDistribution(distributionHeader.WoredaStockDistributionID);
                        }
                        WoredaStockDistributionWithDetailViewModel woredaStockDistributionViewModel = GetWoredaStockDistributionFormDB(distributionHeader);

                        return RedirectToAction("Create",
                                                new { Woreda = woredaStockDistributionViewModel.WoredaID,
                                                      planID = woredaStockDistributionViewModel.PlanID,
                                                      programID = woredaStockDistributionViewModel.ProgramID,
                                                      month = woredaStockDistributionViewModel.Month
                                                });
                        // return View(woredaStockDistributionViewModel);
                    }
                }
                else
                {
                    utilization.ActualBeneficairies = woredaStockDistribution.ActualBeneficairies;
                    utilization.FemaleLessThan5Years = woredaStockDistribution.FemaleLessThan5Years;
                    utilization.FemaleBetween5And18Years = woredaStockDistribution.FemaleLessThan5Years;
                    utilization.FemaleAbove18Years = woredaStockDistribution.FemaleAbove18Years;
                    utilization.MaleLessThan5Years = woredaStockDistribution.MaleLessThan5Years;
                    utilization.MaleBetween5And18Years = woredaStockDistribution.MaleBetween5And18Years;
                    utilization.MaleAbove18Years = woredaStockDistribution.MaleAbove18Years;
                    utilization.SupportTypeID = woredaStockDistribution.SupportTypeID;
                    _utilizationService.EditHeaderDistribution(utilization);

                    var woredaDistributionDetails = _utilizationDetailSerivce.FindBy(m => m.WoredaStockDistributionID == utilization.WoredaStockDistributionID);
                    if (woredaDistributionDetails != null)
                    {
                        foreach (var woredaDistributionDetailViewModel in woredaStockDistribution.WoredaDistributionDetailViewModels)
                        {
                            var woredaDistributionDetail =_utilizationDetailSerivce.FindById(woredaDistributionDetailViewModel.WoredaStockDistributionDetailID);
                            if (woredaDistributionDetail!=null)
                            {
                                woredaDistributionDetail.CommodityID = woredaDistributionDetailViewModel.CommodityID;
                                woredaDistributionDetail.StartingBalance =woredaDistributionDetailViewModel.BeginingBalance;
                                woredaDistributionDetail.EndingBalance = woredaDistributionDetailViewModel.EndingBalance;
                                woredaDistributionDetail.TotalIn = woredaDistributionDetailViewModel.TotalIn;
                                woredaDistributionDetail.TotoalOut = woredaDistributionDetailViewModel.TotalOut;
                                woredaDistributionDetail.LossAmount = woredaDistributionDetailViewModel.LossAmount;
                                woredaDistributionDetail.LossReason = woredaDistributionDetailViewModel.LossReason;
                                woredaDistributionDetail.DistributedAmount =woredaDistributionDetailViewModel.DistributedAmount;
                                _utilizationDetailSerivce.EditDetailDistribution(woredaDistributionDetail);

                            }
                        }

            
                    }
                    LookUps();
                    return RedirectToAction("Create",
                                                    new
                                                    {
                                                        Woreda = utilization.WoredaID,
                                                        planID = utilization.PlanID,
                                                        programID = utilization.ProgramID,
                                                        month = utilization.Month
                                                    });
                }
                
                //WoredaStockDistributionWithDetailViewModel woredaStockDistributionViewModel2 = GetWoredaStockDistributionFormDB(woredaDistributionHeader);
              
            }
            ModelState.AddModelError("Errors",@"Unable to Save Distribution Information");
            return View();
        }
        public void LookUps()
        {
            ViewBag.Region = new SelectList(_commonService.GetAminUnits(m => m.AdminUnitTypeID == 2), "AdminUnitID", "Name","--Select Region--");
            ViewBag.Zone = new SelectList(_commonService.FindBy(m => m.AdminUnitTypeID == 3 && m.ParentID == 3), "AdminUnitID", "Name");
            ViewBag.Woreda = new SelectList(_commonService.FindBy(m => m.AdminUnitTypeID == 4 && m.ParentID == 19), "AdminUnitID", "Name");
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name");
            //ViewBag.Month = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            ViewBag.SupportTypeID = new SelectList(_commonService.GetAllSupportType(), "SupportTypeID", "Description");
        }
        
         public void LookUps(WoredaStockDistributionWithDetailViewModel distributionInfo)
        {
            
             
            ViewBag.Region = new SelectList(_commonService.GetAminUnits(m => m.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.Zone = new SelectList(_commonService.FindBy(m => m.AdminUnitTypeID == 3 && m.ParentID == 3), "AdminUnitID", "Name");
            ViewBag.Woreda = new SelectList(_commonService.FindBy(m => m.AdminUnitTypeID == 4 && m.ParentID == 19), "AdminUnitID", "Name",distributionInfo.WoredaID);
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name",distributionInfo.ProgramID);
            //ViewBag.Month = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            ViewBag.SupportTypeID = new SelectList(_commonService.GetAllSupportType(), "SupportTypeID", "Description");
        }
        public ActionResult ReadRequestionNumbers([DataSourceRequest] DataSourceRequest request, 
                                                  int zoneId=-1, 
                                                  int programId = -1,
                                                  int planId = -1,
                                                  int round =-1,
                                                   int month=-1)
        {
            if (zoneId == -1 || programId ==-1 || planId ==-1)
                return null;
            if (programId == 1 && (month == -1 && round == -1))
                return null;
            if (programId == 2 && round == -1)
                return null;

           

            
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisition = _utilizationService.GetRequisitions(zoneId,programId,planId,6,month,round);
            var requisitionViewModel =UtilizationViewModelBinder.GetUtilizationViewModel(requisition);
            return Json(requisitionViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult WoredaStockDetail_Read([DataSourceRequest] DataSourceRequest request, int woredaStockDistributionID = 0, int woredaID = 0, int planID = 0, int month = 0)
        {
            if (woredaID == 0 || planID == 0 || month == 0) return null;
            var zone = _commonService.GetZoneID(woredaID);
            var region = _commonService.GetRegion(zone);
            var regionalRequest = _regionalRequestService.FindBy(m => m.PlanID == planID && m.Month == month && m.RegionID == region).FirstOrDefault();

            if (regionalRequest != null)
            {
                var requisitions = _reliefRequisitionService.FindBy(m => m.RegionalRequestID == regionalRequest.RegionalRequestID
                                                                && m.ZoneID == zone);

                if (requisitions != null)
                {
                    if (woredaStockDistributionID != 0)
                    {
                        var woredaStockDistribution =
                            _utilizationDetailSerivce.FindBy(
                                m => m.FDP.AdminUnitID == woredaID && m.WoredaStockDistributionID == woredaStockDistributionID);
                        var woredaDistributionDetail = new List<WoredaDistributionDetailViewModel>();
                        foreach (var reliefRequisition in requisitions)
                        {
                            woredaDistributionDetail = GetWoredaStockDistributionDetail(woredaStockDistribution, reliefRequisition);


                        }

                        return Json(woredaDistributionDetail.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                    }


                    var fdpStockDistribution = _commonService.GetFDPs(woredaID);
                    var woredaStockDistributionDetail = new List<WoredaDistributionDetailViewModel>();
                    foreach (var requisition in requisitions)
                    {
                        var detail = GetWoredaStockDistribution(fdpStockDistribution, requisition);
                        woredaStockDistributionDetail.Add(detail);
                    }

                    return Json(woredaStockDistributionDetail.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }


            //var fdps = _commonService.GetFDPs(woredaID);
            //var detail = GetWoredaStockDistribution(fdps, requisitions);
            return Json(new WoredaDistributionDetailViewModel(), JsonRequestBehavior.AllowGet);
        }

        private List<WoredaDistributionDetailViewModel> GetWoredaStockDistributionDetail(IEnumerable<WoredaStockDistributionDetail> woredaStockDistributionDetails,ReliefRequisition requisition)
        {
           
            return (from woredaStockDistributionDetail in woredaStockDistributionDetails
                    select new WoredaDistributionDetailViewModel()
                        {
                            FdpId = woredaStockDistributionDetail.FdpId,
                            FDP = woredaStockDistributionDetail.FDP.Name,
                            WoredaStockDistributionID = woredaStockDistributionDetail.WoredaStockDistributionID,
                            WoredaStockDistributionDetailID = woredaStockDistributionDetail.WoredaStockDistributionDetailID,
                            DistributedAmount = woredaStockDistributionDetail.DistributedAmount,
                            BeginingBalance = woredaStockDistributionDetail.StartingBalance,
                            EndingBalance = woredaStockDistributionDetail.EndingBalance,
                            TotalIn = woredaStockDistributionDetail.TotalIn,
                            TotalOut = woredaStockDistributionDetail.TotoalOut,
                            LossAmount = woredaStockDistributionDetail.LossAmount,
                            RequistionNo = requisition.RequisitionNo,
                            Round = requisition.Round,
                            Month = RequestHelper.MonthName(requisition.RegionalRequest.Month),
                            CommodityName = requisition.Commodity.Name,
                            RequisitionDetailViewModel = GetRequisionInfo(requisition.RequisitionID, woredaStockDistributionDetail.FdpId)  
                        }).ToList();

        }

        private WoredaDistributionDetailViewModel GetWoredaStockDistribution(IEnumerable<FDP> fdps, ReliefRequisition reliefRequisition)
        {
            
            
                return (from fdp in fdps
                        from detail in reliefRequisition.ReliefRequisitionDetails
                        where fdp.FDPID==detail.FDPID
                        select new WoredaDistributionDetailViewModel()
                        {
                            FdpId = fdp.FDPID,
                            FDP = fdp.Name,
                            DistributedAmount = 0,
                            RequistionNo = reliefRequisition.RequisitionNo,
                            Round = reliefRequisition.Round,
                            Month = RequestHelper.MonthName(reliefRequisition.RegionalRequest.Month),
                            CommodityID = detail.CommodityID,
                            CommodityName = detail.Commodity.Name,
                            AllocatedAmount = detail.Amount,
                            NumberOfBeneficiaries = detail.BenficiaryNo,
                            //RequisitionDetailViewModel = new RequisitionDetailViewModel()
                            //    {
                            //        CommodityID = detail.CommodityID,
                            //        CommodityName = detail.Commodity.Name,
                            //        AllocatedAmount = detail.Amount,
                            //        BeneficaryNumber = detail.BenficiaryNo

                            //    },
                            //GetRequisionInfo(reliefRequisition.RequisitionID, fdp.FDPID)

                        }).FirstOrDefault();
            
            //return (from fdp in fdps
            //        select new WoredaDistributionDetailViewModel()
            //        {
            //            FdpId = fdp.FDPID,
            //            FDP = fdp.Name,
            //            DistributedAmount = 0,
                        

            //        }).ToList();
        }
      
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateWoredaDistribution([DataSourceRequest] DataSourceRequest request,WoredaDistributionDetailViewModel woredaDistributionDetail )
        {
            if (woredaDistributionDetail != null && ModelState.IsValid)
            {
                var detail = _utilizationDetailSerivce.FindById(woredaDistributionDetail.WoredaStockDistributionDetailID);
                if (detail != null)
                {
                    detail.WoredaStockDistributionID = woredaDistributionDetail.WoredaStockDistributionID;
                    detail.DistributedAmount = woredaDistributionDetail.DistributedAmount;
                    detail.TotalIn = woredaDistributionDetail.TotalIn;
                    detail.TotoalOut = woredaDistributionDetail.TotalOut;
                    detail.StartingBalance = woredaDistributionDetail.BeginingBalance;
                    detail.EndingBalance = woredaDistributionDetail.EndingBalance;
                    detail.LossAmount = woredaDistributionDetail.LossAmount;
                 

                    _utilizationDetailSerivce.EditDetailDistribution(detail);
                }

            }
            return Json(new[] { woredaDistributionDetail }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult GetPlans(string id,int zoneID)
        {
            var programId = int.Parse(id);
            var plans = _commonService.GetRequisitionGeneratedPlan(programId,zoneID);
            return Json(new SelectList(plans.ToList(), "PlanID", "PlanName"), JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GetMonth(string id,int zoneID)
        {
            try
            {
                var planid = int.Parse(id);
                var requisition = _reliefRequisitionService.FindBy(m => m.ZoneID == zoneID).Select(m => m.RegionalRequestID).Distinct();
                var request = _regionalRequestService.FindBy(m => requisition.Contains(m.RegionalRequestID) && m.PlanID == planid) .ToList();
                //var months = _regionalRequestService.FindBy(r => r.PlanID == planid).ToList();
                var month = from m in request
                             select new {month = m.Month};
                var distinctMonth = month.Distinct();
                return Json(new SelectList(distinctMonth, "month", "month"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return null;
            }
          
        }
        public JsonResult GetRound(string id)
        {
            try
            {
                var planid = int.Parse(id);
                var rounds = _regionalRequestService.FindBy(r => r.PlanID == planid).ToList();
                var round = from r in rounds
                            where r.Round != null
                            select new {round = r.Round};
                var distinctRound = round.Distinct();
                return Json(new SelectList(distinctRound, "round", "round"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return null;
            }
           
        }
        public JsonResult GetAdminUnits()
        {
            var r = (from region in _commonService.GetRegions()
                     select new
                     {

                         RegionID = region.AdminUnitID,
                         RegionName = region.Name,
                         Zones = from zone in _commonService.GetZones(region.AdminUnitID)
                                 select new
                                 {
                                     ZoneID = zone.AdminUnitID,
                                     ZoneName = zone.Name,
                                     Woredas = from woreda in _commonService.GetWoreda(zone.AdminUnitID)
                                               select new
                                               {
                                                   WoredaID = woreda.AdminUnitID,
                                                   WoredaName = woreda.Name
                                               }
                                 }
                     }
                    );
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        private WoredaStockDistributionWithDetailViewModel GetWoredaStockDistributionFormDB(WoredaStockDistribution woredaStockDistribution)
        {
            var woredaStockDistributionWithDetailViewModel = new WoredaStockDistributionWithDetailViewModel()
            {
                WoredaStockDistributionID = woredaStockDistribution.WoredaStockDistributionID,
                WoredaID = woredaStockDistribution.WoredaID,
                ProgramID = woredaStockDistribution.ProgramID,
                PlanID = woredaStockDistribution.PlanID,
                Month = woredaStockDistribution.Month,
                SupportTypeID = woredaStockDistribution.SupportTypeID,
                ActualBeneficairies = woredaStockDistribution.ActualBeneficairies,
                MaleBetween5And18Years = woredaStockDistribution.MaleBetween5And18Years,
                MaleAbove18Years = woredaStockDistribution.MaleAbove18Years,
                MaleLessThan5Years = woredaStockDistribution.MaleLessThan5Years,
                FemaleAbove18Years = woredaStockDistribution.FemaleAbove18Years,
                FemaleBetween5And18Years = woredaStockDistribution.FemaleBetween5And18Years,
                
            };

            return woredaStockDistributionWithDetailViewModel;
        }
    }
}
