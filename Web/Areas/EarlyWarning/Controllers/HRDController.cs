using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.ViewModels.HRD;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class HRDController : Controller
    {
        //
        // GET: /EarlyWarning/HRD/
        private IAdminUnitService _adminUnitService;
        private IHRDService _hrdService;
        private IRationService _rationService;
        private IHRDDetailService _hrdDetailService;
        private ICommodityService _commodityService;
        private IRationDetailService _rationDetailService;
        private INeedAssessmentDetailService _needAssessmentDetailService;
        private INeedAssessmentHeaderService _needAssessmentService;

        public HRDController(IAdminUnitService adminUnitService, IHRDService hrdService, 
                             IRationService rationservice,IRationDetailService rationDetailService,
                             IHRDDetailService hrdDetailService,ICommodityService commodityService,
                             INeedAssessmentDetailService needAssessmentDetailService,INeedAssessmentHeaderService needAssessmentService)
        {
            _adminUnitService = adminUnitService;
            _hrdService = hrdService;
            _hrdDetailService = hrdDetailService;
            _commodityService = commodityService;
            _rationService = rationservice;
            _rationDetailService = rationDetailService;
            _needAssessmentDetailService = needAssessmentDetailService;
            _needAssessmentService = needAssessmentService;
        }

        public ActionResult Index()
        {

            var hrd = _hrdService.GetAllHRD();
            return View(hrd);
        }
        public ActionResult HRDDetail(int id = 0)
        {
            ViewData["Month"] = RequestHelper.GetMonthList();
            var hrd = _hrdService.Get(m => m.HRDID == id, null, "HRDDetails").FirstOrDefault();

            if (hrd != null)
            {
                return View(hrd);
            }
            return RedirectToAction("Index");
        }

        public ActionResult HRD_Read([DataSourceRequest] DataSourceRequest request)
        {

            var hrds = _hrdService.GetAllHRD().OrderByDescending(m => m.HRDID);
            var hrdsToDisplay = GetHrds(hrds).ToList();
            return Json(hrdsToDisplay.ToDataSourceResult(request));
        }

        public ActionResult HRDDetail_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            
           
            //var hrdDetail = _hrdService.GetHRDDetailByHRDID(id).OrderBy(m => m.AdminUnit.AdminUnit2.Name).OrderBy(m => m.AdminUnit.AdminUnit2.AdminUnit2.Name);
            var hrd = _hrdService.Get(m => m.HRDID == id, null, "HRDDetails").FirstOrDefault();

            if (hrd != null)
            {
                var detailsToDisplay = GetHRDDetails(hrd).ToList();
                return Json(detailsToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }
        //gets hrd information
        private IEnumerable<HRDViewModel> GetHrds(IEnumerable<HRD> hrds)
        {
            return (from hrd in hrds
                    select new HRDViewModel()
                    {
                        HRDID = hrd.HRDID,
                        Month = RequestHelper.MonthName(hrd.Month),
                        Year = hrd.Year,
                        Ration = hrd.Ration.RefrenceNumber,
                        CreatedDate = hrd.CreatedDate,
                        CreatedBy = hrd.UserProfile.FirstName + " " + hrd.UserProfile.LastName,
                        PublishedDate = hrd.PublishedDate

                    });
        }

        ////gets hrd Detail Information
        //private IEnumerable<HRDDetailViewModel> GetHRDSummary(HRD hrd)
        //{
        //    var hrdDetails = hrd.HRDDetails;
        //    var rationDetails = _rationService.FindById(hrd.RationID).RationDetails;
        //    return (from hrdDetail in hrdDetails
        //            select new HRDDetailViewModel()
        //            {
        //                    HRDDetailID = hrdDetail.HRDDetailID,
        //                    HRDID = hrdDetail.HRDID,
        //                    WoredaID = hrdDetail.WoredaID,
        //                    Zone = hrdDetail.AdminUnit.AdminUnit2.Name,
        //                    Region = hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.Name,
        //                    Woreda = hrdDetail.AdminUnit.Name,
        //                    NumberOfBeneficiaries = (int) GetTotalBeneficiaries(hrdDetail.HRDID, hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID),
        //                    //hrdDetail.NumberOfBeneficiaries.CompareTo(hrdDetail.AdminUnit.AdminUnit2.AdminUnitID),
        //                    StartingMonth = hrdDetail.StartingMonth,
        //                    DurationOfAssistance = hrdDetail.DurationOfAssistance,
        //                    Cereal = (decimal)((hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * 0.015),
        //                    Pulse = (decimal)((hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * 0.00045),
        //                    BlendedFood = (decimal)((hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * 0.0015),
        //                    Oil = (decimal)((hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * 0.00045)


        //                });
        //}

        private double GetTotalBeneficiaries(int hrdID,int regionId)
        {
            var hrdDetails = _hrdService.FindById(hrdID).HRDDetails;
            decimal totalBeneficiary =
            (from hrdDetail in hrdDetails
             where hrdDetail.HRDID == hrdID && hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionId
             select hrdDetail.NumberOfBeneficiaries).Sum();

            return (double)totalBeneficiary;
        }

        private IEnumerable<HRDDetailViewModel> GetHRDDetails(HRD hrd)
        {
            var hrdDetails = hrd.HRDDetails;
            var rationDetails = _rationService.FindById(hrd.RationID).RationDetails;
            return (from hrdDetail in hrdDetails
                    select new HRDDetailViewModel()
                    {
                        HRDDetailID = hrdDetail.HRDDetailID,
                        HRDID = hrdDetail.HRDID,
                        WoredaID = hrdDetail.WoredaID,
                        Zone = hrdDetail.AdminUnit.AdminUnit2.Name,
                        Region = hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.Name,
                        Woreda = hrdDetail.AdminUnit.Name,
                        NumberOfBeneficiaries = hrdDetail.NumberOfBeneficiaries,
                        //(int)GetTotalBeneficiaries(hrdDetail.HRDID, hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID),
                        StartingMonth = hrdDetail.StartingMonth,
                        DurationOfAssistance = hrdDetail.DurationOfAssistance,
                        Cereal = (hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * (rationDetails.Single(m =>m.CommodityID == 1).Amount),
                        Pulse = (hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * (rationDetails.Single(m =>m.CommodityID == 2).Amount),
                        BlendedFood = (hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * (rationDetails.Single(m =>m.CommodityID == 3).Amount),
                        Oil = (hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * (rationDetails.Single(m =>m.CommodityID == 4).Amount)


                    });
        }
        public ActionResult Create()
        {
            var hrd = new HRD();
           // hrd.HRDDetails = new List<HRDDetail>();
            ViewBag.Year = DateTime.Today.Year;
            ViewBag.RationID = new SelectList(_rationService.GetAllRation(), "RationID", "RefrenceNumber",hrd.RationID=1);
            ViewBag.NeedAssessmentID = new SelectList(_needAssessmentService.GetAllNeedAssessmentHeader(), "NAHeaderId",
                                                      "NeedACreatedDate");
            ViewData["Month"] = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            var woredas = _adminUnitService.FindBy(m => m.AdminUnitTypeID == 3);
             var commodities = _commodityService.GetAllCommodity();

            var hrdDetails = (from detail in woredas
                              select new HRDDetail()
                              {
                                  WoredaID = detail.AdminUnitID,
                                  NumberOfBeneficiaries = 0
                                 
                              }).ToList();
            hrd.HRDDetails = hrdDetails;


            return View(hrd);
        }

         public JsonResult GetRation()
        {
           
           
            var ration = _rationService.Get(t=>t.IsDefaultRation,null,"RationDetails").FirstOrDefault();
            var rationViewModel = (from item in ration.RationDetails
                                   select new
                                              {
                                                  _commodityService.FindById(item.CommodityID).Name,
                                                  Value = item.Amount
                                              });
            return Json(rationViewModel, JsonRequestBehavior.AllowGet);
        }
        //update HRD detail information
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult HRDDetail_Update([DataSourceRequest] DataSourceRequest request, HRDDetailViewModel hrdDetails)
        {
            if (hrdDetails != null && ModelState.IsValid)
            {
                var detail = _hrdDetailService.FindById(hrdDetails.HRDDetailID);
                if (detail != null)
                {
                    detail.HRDID = hrdDetails.HRDID;
                    detail.DurationOfAssistance = hrdDetails.DurationOfAssistance;
                    detail.NumberOfBeneficiaries = hrdDetails.NumberOfBeneficiaries;
                    detail.StartingMonth = hrdDetails.StartingMonth;
                    detail.WoredaID = hrdDetails.WoredaID;
                  
                    _hrdDetailService.EditHRDDetail(detail);
                }
                
            }
            return Json(new[] { hrdDetails }.ToDataSourceResult(request, ModelState));
            //return Json(ModelState.ToDataSourceResult());
        }
        private DateTime GetGregorianDate(string ethiopianDate)
        {
            DateTime convertedGregorianDate;
            try
            {
                convertedGregorianDate = DateTime.Parse(ethiopianDate);
            }
            catch (Exception ex)
            {
                var strEth = new getGregorianDate();
                convertedGregorianDate = strEth.ReturnGregorianDate(ethiopianDate);
            }
            return convertedGregorianDate;
        }

        [HttpPost]
        public ActionResult Create(HRD hrd, string create, string published)
        {
            DateTime dateCreated = DateTime.Now;
            DateTime DatePublished = DateTime.Now;

            dateCreated = GetGregorianDate(create);
            DatePublished = GetGregorianDate(published);

            hrd.CreatedDate = dateCreated;
            hrd.PublishedDate = DatePublished;

            if (ModelState.IsValid)
            {
                var userid = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserAccountId;
                var woredas = _adminUnitService.FindBy(m => m.AdminUnitTypeID == 4);
                //var commodities = _commodityService.GetCommonCommodity();
                    //_commodityService.Get(m=>m.CommodityID==1 && m.CommodityID==2 && m.CommodityID==4 && m.CommodityID==8);
                hrd.CreatedBY = userid;
                var hrdDetails = (from detail in woredas
                                  select new HRDDetail()
                                  {
                                      WoredaID = detail.AdminUnitID,
                                      StartingMonth = 1,
                                      NumberOfBeneficiaries = _needAssessmentDetailService.GetNeedAssessmentBeneficiaryNo((int) hrd.NeedAssessmentID,detail.AdminUnitID),
                                      DurationOfAssistance = _needAssessmentDetailService.GetNeedAssessmentMonths((int) hrd.NeedAssessmentID,detail.AdminUnitID)
                                      
                                  }).ToList();
                
                hrd.HRDDetails = hrdDetails;
                _hrdService.AddHRD(hrd);

            }

            return RedirectToAction("Index");
        }
        //HRD/Edit/2
        public ActionResult Edit(int id)
        {
            var hrd = _hrdService.Get(m => m.HRDID == id, null, "HRDDetails").FirstOrDefault();
            ViewBag.Month = new SelectList(RequestHelper.GetMonthList(), "Id", "Name", hrd.Month);
            ViewBag.RationID = new SelectList(_rationService.GetAllRation(), "RationID", "RefrenceNumber",hrd.RationID);
            ViewBag.NeedAssessmentID = new SelectList(_needAssessmentService.GetAllNeedAssessmentHeader(), "NAHeaderId",
                                                     "NeedACreatedDate",hrd.NeedAssessmentID);

           
            return View(hrd);
        }

        [HttpPost]
        public ActionResult Edit(HRD hrd)
        {
            var userid = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserAccountId;
            hrd.CreatedBY = userid;
            if(ModelState.IsValid)
            {
                _hrdService.EditHRD(hrd);
                return RedirectToAction("Index");
            }

            return View(hrd);
        }

    }
}
