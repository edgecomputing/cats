using System;
using System.Collections.Generic;
using System.Linq;
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
                           select new { item.HRDID, Name = string.Format("{0}-{1}", item.Season.Name, item.Year) }).ToList
                ();
            ViewBag.HRDID = new SelectList(hrdName, "HRDID", "Name");
            //ViewBag.HRDID = new SelectList(_hrdService.GetAllHRD(), "HRDID", "Year");
            ViewBag.DonorID = new SelectList(_donorService.GetAllDonor(), "DonorID", "Name");
            ViewBag.Year = DateTime.Now.Year;
            return View(contribution);

        }

        [HttpPost]
        public ActionResult Create(Contribution contribution)
        {
            if (contribution !=null && ModelState.IsValid)
            {
                //var donors = _donorService.FindBy(m => m.DonorID == contribution.DonorID);
                //var details = (from donor in donors
                //               select new ContributionDetail()
                //                   {
                //                       CommodityID = 1,
                //                       PledgeReferenceNo = "AB123",
                //                       PledgeDate = DateTime.Now,
                //                       Quantity = 0
                //                   }).ToList();

                //contribution.ContributionDetails = details;
                _contributionService.AddContribution(contribution);
                return RedirectToAction("Details","Contribution",new {id=contribution.ContributionID});
            }
            return View(contribution);
        }
        public ActionResult Details(int id)
        {
            var contribution = _contributionService.Get(m => m.ContributionID == id, null, "ContributionDetails").FirstOrDefault();
            ViewBag.DonorID = contribution.Donor.Name;
            ViewData["CommodityID"] = _commodityService.GetAllCommodity();
            if (contribution != null)
            {
                return View(contribution);
            }
            return RedirectToAction("Index");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ContributionDetail_Create([DataSourceRequest] DataSourceRequest request, ContributionDetailViewModel details,int id)
        {
            if (details != null && ModelState.IsValid)
            {
                details.ContributionID = id;

                _contributionDetailService.AddContributionDetail(BindContributionDetail(details));
            }

            return Json(new[] { details }.ToDataSourceResult(request, ModelState));
        }
        private ContributionDetail BindContributionDetail(ContributionDetailViewModel contributionDetailViewModel)
        {
            if (contributionDetailViewModel == null) return null;
            var contributionDetail = new ContributionDetail()
            {
                ContributionDetailID = contributionDetailViewModel.ContributionDetailID,
                ContributiionID = contributionDetailViewModel.ContributionID,
                CommodityID = contributionDetailViewModel.CommodityID,
                PledgeReferenceNo = contributionDetailViewModel.PledgeReferenceNumber,
                PledgeDate = contributionDetailViewModel.PledgeDate,
                Quantity = contributionDetailViewModel.Quantity


            };
            return contributionDetail;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ContributionDetail_Update([DataSourceRequest] DataSourceRequest request, ContributionDetailViewModel contributionDetailViewModel)
        {
            if (contributionDetailViewModel != null && ModelState.IsValid)
            {
                var origin = _contributionDetailService.FindById(contributionDetailViewModel.ContributionDetailID);
                origin.Quantity = contributionDetailViewModel.Quantity;
                origin.PledgeDate = contributionDetailViewModel.PledgeDate;
                origin.PledgeReferenceNo = contributionDetailViewModel.PledgeReferenceNumber;
                origin.CommodityID = contributionDetailViewModel.CommodityID;
                _contributionDetailService.EditContributionDetail(origin);
            }
            return Json(new[] { contributionDetailViewModel }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ContributionDetail_Destroy([DataSourceRequest] DataSourceRequest request, ContributionDetailViewModel contributionDetailViewModel)
        {
            if (contributionDetailViewModel != null && ModelState.IsValid)
            {
                _contributionDetailService.DeleteById(contributionDetailViewModel.ContributionDetailID);
            }
            return Json(ModelState.ToDataSourceResult());
        }
        
        private IEnumerable<ContributionViewModel> GetContribution (IEnumerable<Contribution> contribution)
        {
            return (from contributions in contribution
                    select new ContributionViewModel()
                        {
                            ContributionID = contributions.ContributionID,
                            HRD = contributions.HRD.Season.Name + "-" + contributions.HRD.Year,
                            HRDID = contributions.HRDID,
                            Donor = contributions.Donor.Name,
                            DonorID = contributions.DonorID,
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
                            CommodityID = contributionDetails.CommodityID,
                            Commodity = contributionDetails.Commodity.Name,
                            PledgeReferenceNumber = contributionDetails.PledgeReferenceNo,
                            PledgeDate = contributionDetails.PledgeDate,
                            Quantity = contributionDetails.Quantity


                        });

        }

        public ActionResult ContributionDetail_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {

            var contribution = _contributionService.Get(m => m.ContributionID == id, null, "ContributionDetails").FirstOrDefault();

            if (contribution != null)
            {
                var detailsToDisplay = GetContributionDetail(contribution).ToList();
                return Json(detailsToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ContributionSummary_Read([DataSourceRequest] DataSourceRequest request, int ? year)
        {
            var contributionYear = year ?? 0;
            var contribution = _contributionService.Get(m => m.Year == contributionYear, null, "ContributionDetails");
            //var contribution = _contributionService.GetAllContribution();

            if (contribution != null)
            {
                var detailsToDisplay = GetSummary(contribution).ToList();
                return Json(detailsToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index");
        }


        public ActionResult ContributionSummary()
        {
            //var contribution = _contributionService.GetAllContribution().Where(m=>m.Year==id).ToList();
            //return View(contribution);
            ViewBag.Year =new SelectList(_contributionService.GetAllContribution(), "Year", "Year").Distinct();
            return View();
        }
        private IEnumerable<ContributionSummaryViewModel> GetSummary(IEnumerable<Contribution> contribution)
        {
            //var details = contribution.ContributionDetails;
            return (from summary in contribution 
                    from detail in summary.ContributionDetails
                    select new ContributionSummaryViewModel()
                        {
                            ContributionID = detail.ContributiionID,
                            HRDID = summary.HRDID,
                            DonorID = summary.DonorID,
                            Donor = summary.Donor.Name,
                            CommodityID = detail.CommodityID,
                            Commodity = detail.Commodity.Name,
                            Ammount = detail.Quantity
                        });
        }


    
        }
    }

