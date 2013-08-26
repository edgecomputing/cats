using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

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
        public ContributionController(IContributionService contributionService,
                                      IContributionDetailService contributionDetailService,
                                      IDonorService donorService, ICommodityService commodityService,
                                      IHRDService hrdService)
        {
            _contributionService = contributionService;
            _contributionDetailService = contributionDetailService;
            _donorService = donorService;
            _commodityService = commodityService;
            _hrdService = hrdService;
        }

        public ActionResult Index()
        {
            var contribution = _contributionService.GetAllContribution();
            return View(contribution);
        }

        public ActionResult Create()
        {
            var contribution = new Contribution();
            var hrds = _hrdService.GetAllHRD();
            var hrdName = (from item in hrds
                           select new {item.HRDID, Name = string.Format("{0}-{1}", item.Season.Name, item.Year)}).ToList
                ();
            ViewBag.HRDID = new SelectList(hrdName, "HRDID", "Name");
            ViewBag.DonorID = new SelectList(_donorService.GetAllDonor(), "DonorID", "Name");
            ViewBag.Year = DateTime.Now.Year;
            return View(contribution);

        }

        [HttpPost]
        public ActionResult Create(Contribution contribution)
        {
            if (ModelState.IsValid)
            {
                var donors = _donorService.FindBy(m => m.DonorID == contribution.DonorID);
                var details = (from donor in donors
                               select new ContributionDetail()
                                   {
                                       CommodityID = 1,
                                       PledgeDate = DateTime.Now,
                                       Quantity = 0
                                   }).ToList();

                contribution.ContributionDetails = details;
                _contributionService.AddContribution(contribution);
                return RedirectToAction("Details","Contribution",new {id=contribution.ContributionID});
            }
            return View(contribution);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Contribution_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ContributionDetailViewModel> details)
        {
            var results = new List<ContributionDetailViewModel>();

            if (details != null && ModelState.IsValid)
            {
                foreach (var detail in details)
                {
                    //detail.ContributionID = results.ContributiionID;
                    //detail.ContributionDetailID = results.ContributionDetailID;
                    //_contributionDetailService.AddContributionDetail(detail);
            
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Details(int id)
        {
            var contribution = _contributionService.Get(m => m.ContributionID == id,null,"ContributionDetails").FirstOrDefault();
            if(contribution!=null)
            {
                return View(contribution);
            }
            return RedirectToAction("Index");
        }
        private IEnumerable<ContributionViewModel> GetContribution (IEnumerable<Contribution> contribution)
        {
            return (from contributions in contribution
                    select new ContributionViewModel()
                        {
                            ContributionID = contributions.ContributionID,
                            HRD = contributions.HRD.Season.Name +"-"+contributions.HRD.Year,
                            Donor = contributions.Donor.DonorCode,
                            Year = contributions.Year
                             
                        });
        }
        public ActionResult Contribution_Read([DataSourceRequest] DataSourceRequest request)
        {

            var contributions = _contributionService.GetAllContribution().OrderByDescending(m => m.ContributionID);
            var hrdsToDisplay = GetContribution(contributions).ToList();
            return Json(hrdsToDisplay.ToDataSourceResult(request));
        }

        private IEnumerable<ContributionDetailViewModel> GetContributionDetail(Contribution contribution)
        {
            var contributionDetail = contribution.ContributionDetails;
            return (from contributionDetails in contributionDetail
                    select new ContributionDetailViewModel()
                        {
                            ContributionDetailID = contributionDetails.ContributionDetailID,
                            ContributionID = contributionDetails.ContributiionID,
                            Commodity = contributionDetails.Commodity.Name,
                            PledgeReferenceNumber = contributionDetails.PledgeReferenceNo,
                            PledgeDate = (DateTime) contributionDetails.PledgeDate,
                            Quantity = contributionDetails.Quantity


                        });

        }

        public ActionResult ContributionDetail_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {


            //var hrdDetail = _hrdService.GetHRDDetailByHRDID(id).OrderBy(m => m.AdminUnit.AdminUnit2.Name).OrderBy(m => m.AdminUnit.AdminUnit2.AdminUnit2.Name);
            var contribution = _contributionService.Get(m => m.ContributionID == id, null, "ContributionDetails").FirstOrDefault();

            if (contribution != null)
            {
                var detailsToDisplay = GetContributionDetail(contribution).ToList();
                return Json(detailsToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }
    }
}
