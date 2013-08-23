using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class ContributionController : Controller
    {
        private readonly IContributionService _contributionService;
        private readonly IContributionDetailService _contributionDetailService;
        private readonly IDonorService _donorService; 
        private readonly ICommodityService _commodityService;
        private readonly IHRDService _hrdService;
        // GET: /EarlyWarning/Contribution/
        public ContributionController(IContributionService contributionService,IContributionDetailService contributionDetailService,
                                      IDonorService donorService,ICommodityService commodityService,IHRDService hrdService)
        {
            _contributionService = contributionService;
            _contributionDetailService = contributionDetailService;
            _donorService = donorService;
            _commodityService = commodityService;
            _hrdService = hrdService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var contribution = new Contribution();
            var hrds = _hrdService.GetAllHRD();
            var hrdName =(from item in hrds
                select new { item.HRDID, Name = string.Format("{0}-{1}", item.Season.Name, item.Year) }).ToList();
            ViewBag.HRDID = new SelectList(hrdName, "HRDID", "Name");
            ViewBag.DonorID = new SelectList(_donorService.GetAllDonor(), "DonorID", "Name");
            ViewBag.Year = DateTime.Now.Year;
            return View(contribution);

        }
        [HttpPost]
        public ActionResult Create(Contribution contribution)
        {
            if(ModelState.IsValid)
            {
                var donors = _donorService.FindBy(m => m.DonorID == contribution.DonorID);
                var details = (from donor in donors
                               select new ContributionDetail()
                                   {
                                       CommodityID = 1,
                                       PledgeReferenceNo ="huw45",
                                       Quantity = 0
                                   }).ToList();

                contribution.ContributionDetails = details;           
                _contributionService.AddContribution(contribution);
                return RedirectToAction("Index");
            }
            return View(contribution);
        }
    }
}
