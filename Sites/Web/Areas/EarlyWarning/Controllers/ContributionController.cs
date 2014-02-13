using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Helpers;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class ContributionController : Controller
    {
        private IContributionService _contributionService;
        private IContributionDetailService _contributionDetailService;
        private IDonorService _donorService;
        private ICurrencyService _currencyService;
        private IHRDService _hrdService;
        private ICommodityService _commodityService;
        private IInkindContributionDetailService _inkindContributionDetailService;
        // GET: /EarlyWarning/Contribution/
        public ContributionController(IContributionService contributionService,
                                      IContributionDetailService contributionDetailService,
                                      IDonorService donorService, ICurrencyService currencyService,
                                      IHRDService hrdService, ICommodityService commodityService,
                                      IInkindContributionDetailService inkindContributionDetailService)
        {
            _contributionService = contributionService;
            _contributionDetailService = contributionDetailService;
            _donorService = donorService;
            _currencyService = currencyService;
            _hrdService = hrdService;
            _commodityService = commodityService;
            _inkindContributionDetailService = inkindContributionDetailService;
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
            if (contribution != null && ModelState.IsValid)
            {
                contribution.Year = DateTime.Now.Year;
                _contributionService.AddContribution(contribution);
                if (contribution.ContributionType == "In-Kind")
                    return RedirectToAction("InkindDetails", "Contribution", new { id = contribution.ContributionID });

                return RedirectToAction("Details", "Contribution", new { id = contribution.ContributionID });
            }


            ViewBag.HRDID = new SelectList(_hrdService.GetAllHRD(), "HRDID", "Year", contribution.HRDID);
            ViewBag.DonorID = new SelectList(_donorService.GetAllDonor(), "DonorID", "Name", contribution.DonorID);
            ViewBag.Year = contribution.Year;
            return View(contribution);
        }
        public ActionResult Details(int id)
        {
            var contribution = _contributionService.Get(m => m.ContributionID == id, null, "ContributionDetails").FirstOrDefault();
            ViewBag.DonorID = contribution.Donor.Name;
            ViewBag.CurrencyID = _currencyService.GetAllCurrency();
            if (contribution != null)
            {
                return View(contribution);
            }
            return RedirectToAction("Index");
        }
        public ActionResult InkindDetails(int id)
        {
            var contribution = _contributionService.Get(m => m.ContributionID == id, null, "InkindContributionDetails").FirstOrDefault();
            ViewBag.DonorID = contribution.Donor.Name;
            ViewBag.CommodityID = _commodityService.GetAllCommodity();
            if (contribution != null)
            {
                return View(contribution);
            }
            return RedirectToAction("Index");

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ContributionDetail_Create([DataSourceRequest] DataSourceRequest request, ContributionDetailViewModel details, int id)
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
                ContributionID = contributionDetailViewModel.ContributionID,
                CurrencyID = contributionDetailViewModel.CurrencyID,
                PledgeReferenceNo = contributionDetailViewModel.PledgeReferenceNumber,
                PledgeDate = contributionDetailViewModel.PledgeDate,
                Amount = contributionDetailViewModel.Amount


            };
            return contributionDetail;
        }

        private InKindContributionDetail BindInKindContributionDetail(InKindContributionDetailViewModel inKindContributionDetailViewModel)
        {
            if (inKindContributionDetailViewModel == null) return null;
            var inkindContributionDetail = new InKindContributionDetail()
                {
                    InKindContributionDetailID = inKindContributionDetailViewModel.InKindContributionDetailID,
                    ContributionID = inKindContributionDetailViewModel.ContributionID,
                    ReferenceNumber = inKindContributionDetailViewModel.ReferencNumber,
                    ContributionDate = inKindContributionDetailViewModel.ContributionDate,
                    CommodityID = inKindContributionDetailViewModel.CommodityID,
                    Amount = inKindContributionDetailViewModel.Amount

                };
            return inkindContributionDetail;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ContributionDetail_Update([DataSourceRequest] DataSourceRequest request, ContributionDetailViewModel contributionDetailViewModel)
        {
            if (contributionDetailViewModel != null && ModelState.IsValid)
            {
                var origin = _contributionDetailService.FindById(contributionDetailViewModel.ContributionDetailID);
                if (origin != null)
                {
                    origin.ContributionID = contributionDetailViewModel.ContributionID;
                    origin.PledgeReferenceNo = contributionDetailViewModel.PledgeReferenceNumber;
                    origin.PledgeDate = contributionDetailViewModel.PledgeDate;
                    origin.Amount = contributionDetailViewModel.Amount;
                    origin.CurrencyID = contributionDetailViewModel.CurrencyID;
                    _contributionDetailService.EditContributionDetail(origin);
                }
            }
            return Json(new[] { contributionDetailViewModel }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InKindContributionDetail_Create([DataSourceRequest] DataSourceRequest request, InKindContributionDetailViewModel inKindDetails, int id)
        {
            if (inKindDetails != null && ModelState.IsValid)
            {
                inKindDetails.ContributionID = id;

                _inkindContributionDetailService.AddInKindContributionDetail(BindInKindContributionDetail(inKindDetails));
            }

            return Json(new[] { inKindDetails }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InKindContributionDetail_Update([DataSourceRequest] DataSourceRequest request, InKindContributionDetailViewModel inKindContributionDetailViewModel)
        {
            if (inKindContributionDetailViewModel != null && ModelState.IsValid)
            {
                var origin = _inkindContributionDetailService.FindById(inKindContributionDetailViewModel.InKindContributionDetailID);
                if (origin != null)
                {
                    origin.InKindContributionDetailID = inKindContributionDetailViewModel.InKindContributionDetailID;
                    origin.ContributionID = inKindContributionDetailViewModel.ContributionID;
                    origin.ReferenceNumber = inKindContributionDetailViewModel.ReferencNumber;
                    origin.ContributionDate = inKindContributionDetailViewModel.ContributionDate;
                    origin.CommodityID = inKindContributionDetailViewModel.CommodityID;
                    origin.Amount = inKindContributionDetailViewModel.Amount;
                    _inkindContributionDetailService.EditInKindContributionDetail(origin);
                }

            }
            return Json(new[] { inKindContributionDetailViewModel }.ToDataSourceResult(request, ModelState));
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

        private IEnumerable<ContributionViewModel> GetContribution(IEnumerable<Contribution> contribution)
        {
            return (from contributions in contribution
                    select new ContributionViewModel()
                        {
                            ContributionID = contributions.ContributionID,
                            HRD = contributions.HRD.Season.Name + "-" + contributions.HRD.Year,
                            HRDID = contributions.HRDID,
                            Donor = contributions.Donor.Name,
                            DonorID = contributions.DonorID,
                            Year = contributions.Year,
                            ContributionType = contributions.ContributionType

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
                            ContributionID = contributionDetails.ContributionID,
                            CurrencyID = contributionDetails.CurrencyID,
                            Currency = contributionDetails.Currency.Name,
                            PledgeReferenceNumber = contributionDetails.PledgeReferenceNo,
                            PledgeDate = contributionDetails.PledgeDate,
                            Amount = contributionDetails.Amount,
                            PledgeDatePref = contributionDetails.PledgeDate.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference())


                        });

        }

        private IEnumerable<InKindContributionDetailViewModel> GetInKindContributionDetail(Contribution contribution)
        {
            var inkindContributionDetails = contribution.InKindContributionDetails;
            return (from inkindContributionDetail in inkindContributionDetails
                    select new InKindContributionDetailViewModel()
                        {
                            InKindContributionDetailID = inkindContributionDetail.InKindContributionDetailID,
                            ContributionID = inkindContributionDetail.ContributionID,
                            ReferencNumber = inkindContributionDetail.ReferenceNumber,
                            ContributionDate = inkindContributionDetail.ContributionDate,
                            Commodity = inkindContributionDetail.Commodity.Name,
                            CommodityID = inkindContributionDetail.CommodityID,
                            Amount = inkindContributionDetail.Amount

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
        public ActionResult InkindContributionDetail_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            var contribution = _contributionService.Get(m => m.ContributionID == id, null, "InKindContributionDetails").FirstOrDefault();

            if (contribution != null)
            {
                var detailsToDisplay = GetInKindContributionDetail(contribution).ToList();
                return Json(detailsToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ContributionSummary_Read([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var hrdID = id ?? 0;
            var contribution = _contributionService.Get(m => m.HRDID == hrdID, null, "ContributionDetails");
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
            var hrds = _hrdService.GetAllHRD();
            var hrdName = (from item in hrds
                           select new { item.HRDID, Name = string.Format("{0}-{1}", item.Season.Name, item.Year) }).ToList
                ();
            ViewBag.HRDID = new SelectList(hrdName, "HRDID", "Name");
            ViewBag.Year = new SelectList(_contributionService.GetAllContribution(), "Year", "Year").Distinct();
            return View();
        }
        private IEnumerable<ContributionSummaryViewModel> GetSummary(IEnumerable<Contribution> contribution)
        {
            //var details = contribution.ContributionDetails;
            return (from summary in contribution
                    from detail in summary.ContributionDetails
                    select new ContributionSummaryViewModel()
                        {
                            ContributionID = detail.ContributionID,
                            HRDID = summary.HRDID,
                            DonorID = summary.DonorID,
                            Donor = summary.Donor.Name,
                            CurrencyID = detail.CurrencyID,
                            Currency = detail.Currency.Name,
                            Ammount = detail.Amount
                        });
        }

        public ActionResult Delete(int id)
        {
            var contributionDetail = _contributionDetailService.FindById(id);
            if (contributionDetail != null)
            {
                _contributionDetailService.DeleteContributionDetail(contributionDetail);
                return RedirectToAction("Details", "Contribution", new { id = contributionDetail.ContributionID });
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteInKind(int id)
        {
            var inKindContributionDetail = _inkindContributionDetailService.FindById(id);
            if(inKindContributionDetail != null)
            {
                _inkindContributionDetailService.DeleteInKindContributionDetail(inKindContributionDetail);
                return RedirectToAction("InkindDetails", "Contribution", new {id = inKindContributionDetail.ContributionID});
            }
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InKindContributionDetail_Destroy([DataSourceRequest] DataSourceRequest request, InKindContributionDetailViewModel inKindContributionDetailViewModel)
        {
            if (inKindContributionDetailViewModel != null && ModelState.IsValid)
            {
                _inkindContributionDetailService.DeleteById(inKindContributionDetailViewModel.InKindContributionDetailID);
            }
            return Json(ModelState.ToDataSourceResult());
        }


    }
}

