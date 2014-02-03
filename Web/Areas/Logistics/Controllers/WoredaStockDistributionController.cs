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

namespace Cats.Areas.Logistics.Controllers
{
    public class WoredaStockDistributionController : Controller
    {

        private readonly IUtilizationHeaderSerivce _utilizationService;
        private readonly IUtilizationDetailSerivce _utilizationDetailSerivce;
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly UserAccountService _userAccountService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly ICommonService _commonService;
        private readonly IRegionalRequestService _regionalRequestService;
        private readonly IDistributionByAgeDetailService _distributionByAgeDetailService;
        private readonly IReliefRequisitionDetailService _reliefRequisitionDetailService;
        public WoredaStockDistributionController(IUtilizationHeaderSerivce utilizationService, IUtilizationDetailSerivce utilizationDetailSerivce, 
                       UserAccountService userAccountService, IWorkflowStatusService workflowStatusService, ICommonService commonService, 
                        IRegionalRequestService regionalRequestService,IDistributionByAgeDetailService distributionByAgeDetailService,
                         IReliefRequisitionDetailService reliefRequisitionDetailService,IReliefRequisitionService reliefRequisitionService)
        {
            _utilizationService = utilizationService;
            _utilizationDetailSerivce = utilizationDetailSerivce;
            _userAccountService = userAccountService;
            _workflowStatusService = workflowStatusService;
            _commonService = commonService;
            _regionalRequestService = regionalRequestService;
            _distributionByAgeDetailService = distributionByAgeDetailService;
            _reliefRequisitionDetailService = reliefRequisitionDetailService;
            _reliefRequisitionService = reliefRequisitionService;
        }

        //
        // GET: /Logistics/Utilization/

        public ActionResult Index()
        {
           
            ViewBag.RegionCollection = _commonService.GetAminUnits(t => t.AdminUnitTypeID == 2);
           
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms().Take(2),"ProgramId","Name");
            LookUps();
            return View();
        }

        public ActionResult Create(int Woreda = -1, int planID = -1,int programID=-1, int month = -1)
        {
           // var woredaStockDistribution = new WoredaStockDistribution();
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
            var distributionDetail=_commonService.GetFDPs(Woreda);
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
            var regionalRequest =
                _regionalRequestService.FindBy(m => m.PlanID == planID && m.Month == month).FirstOrDefault();
            if (regionalRequest!=null)
            {
                var requisition =
                    _reliefRequisitionService.FindBy(
                        m => m.RegionalRequestID == regionalRequest.RegionalRequestID && m.ZoneID == zoneID).
                        FirstOrDefault();
                if (requisition != null)
                {
                    var woredaStockDistribution = _utilizationService.FindBy(m => m.WoredaID == woredaID && m.Month == month && m.PlanID == planID).FirstOrDefault();
                    if (woredaStockDistribution==null)
                    {
                       //TODO:populateWOredas with detail for the grid
                        var fdpStockDistribution = _commonService.GetFDPs(woredaID);
                        var listOfFdps = new WoredaStockDistributionWithDetailViewModel
                            {
                                WoredaDistributionDetailViewModels = GetWoredaStockDistribution(fdpStockDistribution,requisition),
                                

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
                                                          select new WoredaDistributionDetailViewModel()
                                                              {
                                                                  FdpId = woredaDistributionDetail.FdpId,
                                                                  FDP = woredaDistributionDetail.FDP.Name,
                                                                  RequisitionDetailViewModel = GetRequisionInfo(requisition.RequisitionID, woredaDistributionDetail.FdpId),
                                                                  RequistionNo = requisition.RequisitionNo,
                                                                  Round = requisition.Round,
                                                                  Month = RequestHelper.MonthName(requisition.Month),

                                                                  //CommodityName = ,
                                                                  //Month = ,
                                                                  //AllocatedAmount = ,
                                                                  //RequistionNo = ,
                                                                  //BeginingBalance = 0,
                                                                  //EndingBalance = 0,
                                                                  DistributedAmount = woredaDistributionDetail.DistributedAmount,


                                                              }
                         )


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
        //private IEnumerable<WoredaDistributionDetailViewModel> GetWoredaStockDistributionDetailInfo(int woredaStockDistributionID)
        //{
        //    var woredaStockDistributionDetails = _utilizationDetailSerivce.FindBy(m => m.WoredaStockDistributionID == woredaStockDistributionID);
        //    var woredaDistribution = _utilizationService.FindById(woredaStockDistributionID);
        //    if (woredaStockDistributionDetails!=null)
        //    {
                
            
        //       return (from woredaStockDistributionDetail in woredaStockDistributionDetails
        //            select new WoredaDistributionDetailViewModel()
        //            {
        //                FdpId = woredaStockDistributionDetail.FdpId,
        //                FDP = woredaStockDistributionDetail.FDP.Name,
        //                WoredaStockDistributionID = woredaStockDistributionDetail.WoredaStockDistributionID,
        //                WoredaStockDistributionDetailID = woredaStockDistributionDetail.WoredaStockDistributionID,
        //                DistributedAmount = woredaStockDistributionDetail.DistributedAmount

        //            });
        //    }
        //    var fdpStockDistribution = _commonService.GetFDPs(woredaDistribution.WoredaID);
        //    return  GetWoredaStockDistribution(fdpStockDistribution);

        //}
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
                var BindToModel = GetWoredaDetailMOdel(woredaStockDistribution);
                if (utilization==null)
                {
                    var userProfileId = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).UserProfileID;
                    BindToModel.DistributedBy = userProfileId;
                    BindToModel.DistributionDate = DateTime.Now;
                    var saved=_utilizationService.AddHeaderDistribution(BindToModel);
                    if (saved)
                    {
                        var fdpStockDistribution = _commonService.GetFDPs(woredaStockDistribution.WoredaID);

                        var woredaDistributionDetailViewModels = GetWoredaStockDistribution(fdpStockDistribution,null);


                        var distributionHeader =
                            _utilizationService.FindBy(m => m.WoredaID == woredaStockDistribution.WoredaID &&
                                                            m.PlanID == woredaStockDistribution.PlanID &&
                                                            m.Month == woredaStockDistribution.Month).FirstOrDefault();

                        foreach (var woredaDistributionDetailViewModel in woredaDistributionDetailViewModels)
                        {
                            var distributionDetailModel = new WoredaStockDistributionDetail()
                                {
                                    WoredaStockDistributionID = distributionHeader.WoredaStockDistributionID,
                                    FdpId = woredaDistributionDetailViewModel.FdpId,
                                    DistributedAmount = woredaDistributionDetailViewModel.DistributedAmount


                                };
                            _utilizationDetailSerivce.AddDetailDistribution(distributionDetailModel);
                        }

                        ModelState.AddModelError("Success", @"Distribution Information Successfully Saved");
                        LookUps();
                        //var distributionDetail = _utilizationDetailSerivce.FindBy(m => m.WoredaStockDistributionID == distributionHeader.WoredaStockDistributionID);
                        //distributionHeader.WoredaStockDistributionDetails = distributionDetail;
                        WoredaStockDistributionWithDetailViewModel woredaStockDistributionViewModel = GetWoredaStockDistributionFormDB(distributionHeader);
                        return View(woredaStockDistributionViewModel);
                    }
                }
                utilization.ActualBeneficairies = woredaStockDistribution.ActualBeneficairies;
                utilization.FemaleLessThan5Years = woredaStockDistribution.FemaleLessThan5Years;
                utilization.FemaleBetween5And18Years = woredaStockDistribution.FemaleLessThan5Years;
                utilization.FemaleAbove18Years = woredaStockDistribution.FemaleAbove18Years;
                utilization.MaleLessThan5Years = woredaStockDistribution.MaleLessThan5Years;
                utilization.MaleBetween5And18Years = woredaStockDistribution.MaleBetween5And18Years;
                utilization.MaleAbove18Years = woredaStockDistribution.MaleAbove18Years;
                utilization.SupportTypeID = woredaStockDistribution.SupportTypeID;
                _utilizationService.EditHeaderDistribution(utilization);

                var woredaDistributionHeader = _utilizationService.FindById(utilization.WoredaStockDistributionID);
                WoredaStockDistributionWithDetailViewModel woredaStockDistributionViewModel2 = GetWoredaStockDistributionFormDB(woredaDistributionHeader);
                LookUps();
                return View(woredaStockDistributionViewModel2);
            }
            ModelState.AddModelError("Errors",@"Unable to Save Distribution Information");
            return View();
        }
        public void LookUps()
        {
            ViewBag.Region = new SelectList(_commonService.GetAminUnits(m => m.AdminUnitTypeID == 2), "AdminUnitID", "Name");
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

        public ActionResult WoredaStockDetail_Read([DataSourceRequest] DataSourceRequest request, int woredaStockDistributionID=0, int woredaID=0,int planID=0,int month=0)
        {
            if (woredaID==0 || planID==0 || month==0) return null;
            var zone = _commonService.GetZoneID(woredaID);
            var regionalRequest = _regionalRequestService.FindBy(m => m.PlanID == planID && m.Month == month).FirstOrDefault();
            var requisition = _reliefRequisitionService.FindBy(m => m.RegionalRequestID == regionalRequest.RegionalRequestID
                                                                 && m.ZoneID == zone).FirstOrDefault();
            if (regionalRequest!=null)
            {
              
                if (requisition != null)
                {
                    if (woredaStockDistributionID != 0)
                    {
                        var woredaStockDistribution =
                            _utilizationDetailSerivce.FindBy(
                                m => m.FDP.AdminUnitID == woredaID && m.WoredaStockDistributionID == woredaStockDistributionID);
                        var woredaDistributionDetail = GetWoredaStockDistributionDetail(woredaStockDistribution, requisition);
                        return Json(woredaDistributionDetail.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                    }


                    var fdpStockDistribution = _commonService.GetFDPs(woredaID);
                    var woredaStockDistributionDetail = GetWoredaStockDistribution(fdpStockDistribution,requisition);
                    return Json(woredaStockDistributionDetail.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }


            var fdps = _commonService.GetFDPs(woredaID);
            var detail = GetWoredaStockDistribution(fdps, requisition);
            return Json(detail.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<WoredaDistributionDetailViewModel> GetWoredaStockDistributionDetail(IEnumerable<WoredaStockDistributionDetail> woredaStockDistributionDetails,ReliefRequisition requisition)
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

                          

                            
                            
                        });

        }
        
        private IEnumerable<WoredaDistributionDetailViewModel>  GetWoredaStockDistribution(IEnumerable<FDP> fdps,ReliefRequisition reliefRequisition)
        {
            if (reliefRequisition != null)
            {
                return (from fdp in fdps
                        select new WoredaDistributionDetailViewModel()
                        {
                            FdpId = fdp.FDPID,
                            FDP = fdp.Name,
                            DistributedAmount = 0,
                            RequistionNo = reliefRequisition.RequisitionNo,
                            Round = reliefRequisition.Round,
                            Month = RequestHelper.MonthName(reliefRequisition.RegionalRequest.Month),
                            RequisitionDetailViewModel = GetRequisionInfo(reliefRequisition.RequisitionID, fdp.FDPID)

                        });
            }
            return (from fdp in fdps
                    select new WoredaDistributionDetailViewModel()
                    {
                        FdpId = fdp.FDPID,
                        FDP = fdp.Name,
                        DistributedAmount = 0,
                        

                    });
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
       
        public JsonResult GetMonth(string id)
        {
            try
            {
                var planid = int.Parse(id);

                var months = _regionalRequestService.FindBy(r => r.PlanID == planid).ToList();
                var month = from m in months
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
                Month = woredaStockDistribution.WoredaStockDistributionID,
                SupportTypeID = woredaStockDistribution.SupportTypeID,
                ActualBeneficairies = woredaStockDistribution.ActualBeneficairies,
                MaleBetween5And18Years = woredaStockDistribution.MaleBetween5And18Years,
                MaleAbove18Years = woredaStockDistribution.MaleAbove18Years,
                MaleLessThan5Years = woredaStockDistribution.MaleLessThan5Years,
                FemaleAbove18Years = woredaStockDistribution.FemaleAbove18Years,
                FemaleBetween5And18Years = woredaStockDistribution.FemaleBetween5And18Years,
                
                //WoredaDistributionDetailViewModels = (from woredaDistributionDetail in woredaStockDistribution.WoredaStockDistributionDetails
                //                                      select new WoredaDistributionDetailViewModel()
                //                                      {
                //                                          FdpId = woredaDistributionDetail.FdpId,
                //                                          //FDP = woredaDistributionDetail.FDP.Name,
                //                                          WoredaStockDistributionDetailID = woredaDistributionDetail.WoredaStockDistributionDetailID,
                //                                          WoredaStockDistributionID = woredaDistributionDetail.WoredaStockDistributionID,
                //                                          //RequisitionDetailViewModel = GetRequisionInfo(requisition.RequisitionID, woredaDistributionDetail.FdpId),
                //                                          //RequistionNo = requisition.RequisitionNo,
                //                                          //Round = requisition.Round,
                //                                          //Month = RequestHelper.MonthName(requisition.Month),

                //                                          //CommodityName = ,
                //                                          //Month = ,
                //                                          //AllocatedAmount = ,
                //                                          //RequistionNo = ,
                //                                          //BeginingBalance = 0,
                //                                          //EndingBalance = 0,
                //                                          DistributedAmount = woredaDistributionDetail.DistributedAmount,


                //                                      }
                //     )


            };

            return woredaStockDistributionWithDetailViewModel;
        }
    }
}
