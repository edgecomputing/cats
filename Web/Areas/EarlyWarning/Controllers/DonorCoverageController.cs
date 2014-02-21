using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Administration;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using IAdminUnitService = Cats.Services.EarlyWarning.IAdminUnitService;
using IDonorService = Cats.Services.EarlyWarning.IDonorService;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class DonorCoverageController : Controller
    {
        //
        // GET: /EarlyWarning/DonorCoverage/
        private IDonorService _donorService;
        private IAdminUnitService _adminUnitService;
        private IHRDService _hrdService;
        private IHrdDonorCoverageDetailService _hrdDonorCoverageDetailService;
        private IHrdDonorCoverageService _hrdDonorCoverageService;
        private IUserAccountService _userAccountService;

        public DonorCoverageController(IDonorService donorService,IAdminUnitService adminUnitService,
                                      IHRDService hrdService,IHrdDonorCoverageDetailService hrdDonorCoverageDetailService,
                                      IHrdDonorCoverageService hrdDonorCoverageService,IUserAccountService userAccountService)
        {
            _donorService = donorService;
            _adminUnitService = adminUnitService;
            _hrdService = hrdService;
            _hrdDonorCoverageDetailService = hrdDonorCoverageDetailService;
            _hrdDonorCoverageService = hrdDonorCoverageService;
            _userAccountService = userAccountService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var hrdDonorCoverage = new HrdDonorCoverage();
            ViewBag.DonorID =new SelectList(_donorService.GetAllDonor(),"DonorID","DonorCode");
            var hrds = _hrdService.GetAllHRD().Where(m => m.Status != (int)HRDStatus.Draft);
            var hrd =(from item in hrds
                 select new { item.HRDID, Name = string.Format("{0}-{1}", item.Season.Name, item.Year) }).ToList();
            ViewBag.HRDID = new SelectList(hrd,"HRDID","Name");
            

            return View(hrdDonorCoverage);
        }
        [HttpPost]
        public ActionResult Create(HrdDonorCoverage hrdDonorCoverage)
        {
            if (ModelState.IsValid)
            {
                hrdDonorCoverage.CreatedDate = DateTime.Now.Date;
                _hrdDonorCoverageService.AddHrdDonorCoverage(hrdDonorCoverage);
                return RedirectToAction("Detail",new {id=hrdDonorCoverage.HRDDOnorCoverageID});
            }
            return View(hrdDonorCoverage);
        }
        public ActionResult Detail(int id)
        {
            var hrdDonorCoverage = _hrdDonorCoverageService.FindById(id);
            if (hrdDonorCoverage==null)
            {
                return HttpNotFound();
            }
            return View(hrdDonorCoverage);
        }
        public ActionResult DonorCoverage_Read([DataSourceRequest] DataSourceRequest request)

        {
            var donorCovarage = _hrdDonorCoverageService.GetAllHrdDonorCoverage().ToList();
            var donorCoverageToDisplay = GetDonorCoverage(donorCovarage);
            return Json(donorCoverageToDisplay.ToDataSourceResult(request));
        }
        public ActionResult HrdDonorCoverageDetail_Read([DataSourceRequest] DataSourceRequest request,int id=0)
        {
            var donorCoverageDetail = _hrdDonorCoverageDetailService.FindBy(m => m.HRDDonorCoverageID == id).ToList();
            var donorCoverageDetailDisplay = GetDonorCoverageDetail(donorCoverageDetail);
            return Json(donorCoverageDetailDisplay.ToDataSourceResult(request));
        }
        private IEnumerable<HRDDonorCoverageViewModel> GetDonorCoverage(IEnumerable<HrdDonorCoverage> hrdDonorCoverages)
        {   
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from hrdDonorCoverage in hrdDonorCoverages
                    select new HRDDonorCoverageViewModel()
                        {
                            HrdDonorCovarageID = hrdDonorCoverage.HRDDOnorCoverageID,
                            hrdID = hrdDonorCoverage.HRDID,
                            Season = hrdDonorCoverage.Hrd.Season.Name,
                            Year = hrdDonorCoverage.Hrd.Year,
                            DonorName = hrdDonorCoverage.Donor.Name,
                            CreatedDate = hrdDonorCoverage.CreatedDate.ToCTSPreferedDateFormat(datePref),
                            NoCoveredWoredas = _hrdDonorCoverageService.NumberOfCoveredWoredas(hrdDonorCoverage.HRDDOnorCoverageID)
                            
                        });
        }
        private IEnumerable<HrdDonorCoverageDetailViewModel> GetDonorCoverageDetail(IEnumerable<HrdDonorCoverageDetail> hrdDonorCoverageDetails)
        {
            return (from coverageDetail in hrdDonorCoverageDetails
                    select new HrdDonorCoverageDetailViewModel()
                        {
                            HrdDonorCoverageDetailID = coverageDetail.HRDDonorCoverageDetailID,
                            HrdDonorCoverageID = coverageDetail.HRDDonorCoverageID,
                            WoredaID = coverageDetail.WoredaID,
                            Woreda = coverageDetail.AdminUnit.Name,
                            Zone = coverageDetail.AdminUnit.AdminUnit2.Name,
                            Region = coverageDetail.AdminUnit.AdminUnit2.AdminUnit2.Name

                        });
        }
        public ActionResult AddWoreda(int id)
        {
            //var donorCoverage = _hrdDonorCoverageService.FindById(id);
            ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
            ViewBag.ZoneID = new SelectList(_adminUnitService.FindBy(m=>m.AdminUnitTypeID==3), "AdminUnitID", "Name");
            ViewBag.WoredaID = new SelectList(_adminUnitService.FindBy(m => m.AdminUnitTypeID == 4), "AdminUnitID", "Name");
            var addWoredaViewModel = new AddWoredaViewModel();
            addWoredaViewModel.DonorCoverageID = id;
            return PartialView(addWoredaViewModel);
        }
        [HttpPost]
        public ActionResult AddWoreda(AddWoredaViewModel addWoredaViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var donorCoverageDetail = GetCoverageDetail(addWoredaViewModel);
                    _hrdDonorCoverageDetailService.AddWoredas(donorCoverageDetail);
                    return RedirectToAction("Detail", new {id = addWoredaViewModel.DonorCoverageID});
                }
                catch (Exception e)
                {
                    
                    ModelState.AddModelError("Errors","Unable to Add Woreda");
                }
            }
            return RedirectToAction("Detail", new { id = addWoredaViewModel.DonorCoverageID });
        }
        private HrdDonorCoverageDetail GetCoverageDetail(AddWoredaViewModel addWoredaViewModel)
        {
            var hrdDonorCoverageDetail = new HrdDonorCoverageDetail()
                {
                    HRDDonorCoverageID = addWoredaViewModel.DonorCoverageID,
                    WoredaID = addWoredaViewModel.WoredaID
                };
            return hrdDonorCoverageDetail;
        }
        public JsonResult GetAdminUnits()
        {
            var r = (from region in _adminUnitService.GetRegions()
                     select new
                     {

                         RegionID = region.AdminUnitID,
                         RegionName = region.Name,
                         Zones = from zone in _adminUnitService.GetZones(region.AdminUnitID)
                                 select new
                                 {
                                     ZoneID = zone.AdminUnitID,
                                     ZoneName = zone.Name,
                                     Woredas = from woreda in _adminUnitService.GetWoreda(zone.AdminUnitID)
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
    }
}
