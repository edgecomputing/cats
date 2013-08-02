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

        public HRDController(IAdminUnitService adminUnitService, IHRDService hrdService, 
                             IHRDDetailService hrdDetailService,ICommodityService commodityService)
        {
            _adminUnitService = adminUnitService;
            _hrdService = hrdService;
            _hrdDetailService = hrdDetailService;
            _commodityService = commodityService;
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
            //ViewBag.StatusID = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name", bid.StatusID = 1);
           
            var hrdDetail = _hrdService.GetHRDDetailByHRDID(id).OrderBy(m => m.AdminUnit.AdminUnit2.Name).OrderBy(m => m.AdminUnit.AdminUnit2.AdminUnit2.Name);
            if (hrdDetail != null)
            {
                var detailsToDisplay = GetHRDDetails(hrdDetail).ToList();
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
                        Month = hrd.Month,
                        Year = hrd.Year,
                        //RationID = hrd.RationID,
                        CreatedDate = hrd.CreatedDate,
                        PublishedDate = hrd.PublishedDate

                    });
        }

        //gets hrd Detail Information
        private IEnumerable<HRDDetailViewModel> GetHRDDetails(IEnumerable<HRDDetail> hrdDetails)
        {
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
                            StartingMonth = hrdDetail.StartingMonth,
                            DurationOfAssistance = hrdDetail.DurationOfAssistance,
                            Cereal = (decimal) ((hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries)*0.015),
                            Pulse = (decimal) ((hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * 0.00045),
                            CSB = (decimal) ((hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * 0.0015),
                            Oil = (decimal) ((hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * 0.00045)
                                          

                        });
        }
        public ActionResult Create(int id = 0)
        {
            var hrd = new HRD();
           // hrd.HRDDetails = new List<HRDDetail>();
            //ViewBag.Ration = new SelectList(_rationService.GetAllRation(), "RationID", "Name");
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
                
                var woredas = _adminUnitService.FindBy(m => m.AdminUnitTypeID == 4);
                //var commodities = _commodityService.GetCommonCommodity();
                    //_commodityService.Get(m=>m.CommodityID==1 && m.CommodityID==2 && m.CommodityID==4 && m.CommodityID==8);

                var hrdDetails = (from detail in woredas
                                  select new HRDDetail()
                                  {
                                      WoredaID = detail.AdminUnitID,
                                      StartingMonth = 1,
                                      NumberOfBeneficiaries = 100,
                                      DurationOfAssistance = 8
                                      
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
           
            return View(hrd);
        }

        [HttpPost]
        public ActionResult Edit(HRD hrd)
        {
            if(ModelState.IsValid)
            {
                _hrdService.EditHRD(hrd);
                return RedirectToAction("Index");
            }

            return View(hrd);
        }

    }
}
