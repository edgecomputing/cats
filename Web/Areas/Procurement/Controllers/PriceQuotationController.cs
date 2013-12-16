using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Helpers;
using Cats.Models;
using Cats.Data;
using Cats.Services.Procurement;
using Cats.Services.EarlyWarning;
using Cats.Areas.Procurement.Models;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
namespace Cats.Areas.Procurement.Controllers
{
    public class PriceQuotationController : Controller
    {
        private readonly ITransportBidPlanService _transportBidPlanService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IProgramService _programService;
        private readonly ITransportBidPlanDetailService _transportBidPlanDetailService;
        private readonly IHubService _hubService;
        private readonly ITransportBidQuotationService _bidQuotationService;
        private readonly IBidService _bidService;
        private readonly ITransporterService _transporterService;
        private readonly ITransportBidQuotationService _transportBidQuotationService;
        private readonly IBidWinnerService _bidWinnerService;


        public PriceQuotationController(ITransportBidPlanService transportBidPlanServiceParam
                                            , IAdminUnitService adminUnitServiceParam
                                            , IProgramService programServiceParam
                                            , ITransportBidPlanDetailService transportBidPlanDetailServiceParam
                                            , IHubService hubServiceParam
                                            , ITransportBidQuotationService bidQuotationServiceParam
                                            , ITransporterService transporterServiceParam
                                            , IBidService bidServiceParam
                                            , ITransportBidQuotationService transportBidQuotationService
                                            , IBidWinnerService bidWinnerService
            )
        {
            this._transportBidPlanService = transportBidPlanServiceParam;
            this._adminUnitService = adminUnitServiceParam;
            this._programService = programServiceParam;
            this._transportBidPlanDetailService = transportBidPlanDetailServiceParam;
            this._hubService = hubServiceParam;
            this._bidQuotationService = bidQuotationServiceParam;
            this._bidService = bidServiceParam;
            this._transporterService = transporterServiceParam;
            this._transportBidQuotationService = transportBidQuotationService;
            this._bidWinnerService = bidWinnerService;
        }

        public void LoadLookups()
        {
            ViewBag.BidID = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");
        }

        public void LoadPLookups()
        {
            ViewBag.BidPlanID = new SelectList(_transportBidPlanService.GetAllTransportBidPlan(), "TransportBidPlanID", "Year");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");
        }

        //
        // GET: /Procurement/RFQ/EditStart
        
        [HttpGet]
        public ActionResult Index()
        {
            LoadLookups();
            var model = new PriceQuotationFilterViewModel();
            //return View(model);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(TransportBidQuotation transportQuote)
        {
            if (ModelState.IsValid)
            {
                List<TransportBidQuotation> existing =
                    _bidQuotationService.FindBy(t => t.BidID == transportQuote.BidID
                                               && t.TransporterID == transportQuote.TransporterID
                                               && t.SourceID == transportQuote.SourceID
                                               && t.DestinationID == transportQuote.DestinationID
                                               );
                if (existing.Count == 1)
                {
                    TransportBidQuotation edited = existing[0];
                    //                    transportQuote.TransportBidQuotationID = edited.TransportBidQuotationID;
                    edited.Tariff = transportQuote.Tariff;
                    edited.Remark = transportQuote.Remark;
                    edited.Position = transportQuote.Position;
                    edited.IsWinner = transportQuote.IsWinner;
                    _bidQuotationService.UpdateTransportBidQuotation(edited);
                }
                else
                {
                    _bidQuotationService.AddTransportBidQuotation(transportQuote);
                }
                return View(transportQuote);

            }
            return RedirectToAction("Index");
        }
        public List<GoodsMovementDetailViewModel> GetPlannedDistribution(int BidPlanID, int RegionID)
        {
            List<TransportBidPlanDetail> regionalPlan
                = _transportBidPlanDetailService.FindBy(
                                                t => t.BidPlanID == BidPlanID
                                                && t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == RegionID);

            List<TransportBidPlanDetail> regionalPlanSorted =
                (from planDetail in regionalPlan
                 orderby planDetail.Source.Name, planDetail.Destination.AdminUnit2.Name, planDetail.DestinationID, planDetail.ProgramID
                 select planDetail

                 ).ToList();


            List<GoodsMovementDetailViewModel> regionPlanDistinct = (from rg in regionalPlanSorted

                                                                     select new GoodsMovementDetailViewModel
                                                                     {
                                                                         //SourceWarehouse = rg.Source,
                                                                         SourceName = rg.Source.Name,
                                                                         SourceID = rg.Source.HubID,
                                                                         DestinationZone = rg.Destination.AdminUnit2.Name,
                                                                         RegionName = rg.Destination.AdminUnit2.AdminUnit2.Name,
                                                                         RegionID = rg.Destination.AdminUnit2.AdminUnit2.AdminUnitID,
                                                                         //DestinationWoreda = rg.Destination,
                                                                         DestinationName = rg.Destination.Name,
                                                                         DestinationID = rg.Destination.AdminUnitID
                                                                     })

             .GroupBy(rg => new { rg.SourceName, rg.DestinationZone, rg.DestinationName })

             .Select(s => (GoodsMovementDetailViewModel)s.FirstOrDefault()).ToList();
            return regionPlanDistinct.ToList();

        }

        public List<PriceQuotationDetailViewModel> GetPriceQuotation(List<GoodsMovementDetailViewModel> movement, int TransporterID, int BidID)
        {
            List<PriceQuotationDetailViewModel> qoutation = (from rg in movement
                                                             select new PriceQuotationDetailViewModel
                                                             {
                                                                 SourceName = rg.SourceName,
                                                                 SourceID = rg.SourceID,
                                                                 DestinationZone = rg.DestinationZone,
                                                                 RegionName = rg.RegionName,
                                                                 RegionID = rg.RegionID,
                                                                 DestinationName = rg.DestinationName,
                                                                 DestinationID = rg.DestinationID,
                                                                 //Tariff=10,
                                                                 //Remark="Remark",
                                                                 TransporterID = TransporterID,
                                                                 BidID = BidID,
                                                                 QuotationID = rg.SourceID * 10 + rg.DestinationID
                                                             }).ToList();
            return qoutation;
        }
        
        public Dictionary<string, PriceQuotationDetailViewModel> organizeList(List<PriceQuotationDetailViewModel> quoteList)
        {
            System.Collections.Generic.Dictionary<string, PriceQuotationDetailViewModel> ret = new Dictionary<string, PriceQuotationDetailViewModel>();

            foreach (var i in quoteList)
            {
                string hash = i.BidID + "_" + i.TransporterID + "_" + i.SourceID + "_" + i.DestinationID;
                ret.Add(hash, i);// = i;
            }
            return ret;
        }
        
        public List<PriceQuotationDetailViewModel> populateForm(PriceQuotationFilterViewModel model)
        {
            Session["PriceQuotationFilter"] = model;
            LoadLookups();
            ViewBag.ModelFilter = model;
            ViewBag.SelectedRegion = _adminUnitService.FindById(model.RegionID);
            int bidID = model.BidID;


            ViewBag.SelectedTransporter = _transporterService.FindById(model.TransporterID);
            Bid SelectedBid = _bidService.FindById(bidID);
            ViewBag.SelectedBid = SelectedBid;
            int bidPlanID = SelectedBid.TransportBidPlanID;

            List<GoodsMovementDetailViewModel> quotationDestinations = GetPlannedDistribution(bidPlanID, model.RegionID);
            List<PriceQuotationDetailViewModel> qoutation = GetPriceQuotation(quotationDestinations, model.TransporterID, bidID);

            // return View(qoutation);

            List<TransportBidQuotation> transporterQuote = _bidQuotationService.FindBy(t => t.BidID == bidID && t.TransporterID == model.TransporterID);
            Dictionary<string, PriceQuotationDetailViewModel> transporterQuoteDictionary = organizeList(qoutation);

            foreach (TransportBidQuotation i in transporterQuote)
            {
                string hash = i.BidID + "_" + i.TransporterID + "_" + i.SourceID + "_" + i.DestinationID;
                if (transporterQuoteDictionary.ContainsKey(hash))
                {
                    PriceQuotationDetailViewModel pq = transporterQuoteDictionary[hash];
                    pq.TransportBidQuotationID = i.TransportBidQuotationID;
                    pq.Tariff = (int)i.Tariff;
                    pq.Remark = i.Remark;
                    pq.IsWinner = i.IsWinner;
                    pq.Rank = i.Position;

                }
            }
            return qoutation;
        }

        ////update HRD detail information
        //[AcceptVerbs(HttpVerbs.Post)]
        //[EarlyWarningAuthorize(operation = EarlyWarningCheckAccess.Operation.Modify_HRD)]
        //public ActionResult HRDDetail_Update([DataSourceRequest] DataSourceRequest request, HRDDetailViewModel hrdDetails)
        //{
        //    if (hrdDetails != null && ModelState.IsValid)
        //    {
        //        var detail = _hrdDetailService.FindById(hrdDetails.HRDDetailID);
        //        if (detail != null)
        //        {
        //            detail.HRDID = hrdDetails.HRDID;
        //            detail.DurationOfAssistance = hrdDetails.DurationOfAssistance;
        //            detail.NumberOfBeneficiaries = hrdDetails.NumberOfBeneficiaries;
        //            detail.StartingMonth = hrdDetails.StartingMonth;
        //            detail.WoredaID = hrdDetails.WoredaID;

        //            _hrdDetailService.EditHRDDetail(detail);
        //        }

        //    }
        //    return Json(new[] { hrdDetails }.ToDataSourceResult(request, ModelState));
        //    //return Json(ModelState.ToDataSourceResult());
        //}
        
        [AcceptVerbs(HttpVerbs.Post)]
        //[ProcurementAuthorize(operation = ProcurementCheckAccess.Operation.Bid_Planning)]
        public ActionResult SaveBidProposals([DataSourceRequest] DataSourceRequest request, PriceQuotationDetail bidProposal)
        {
            if (bidProposal != null && ModelState.IsValid)
            {
                //var detail = _transportBidQuotationService.FindById(bidProposal.TransportBidQuotationID);
                var detai = _transportBidQuotationService.FindBy(t=>
                                                                    t.BidID==bidProposal.BidID
                                                                 && t.SourceID==bidProposal.SourceID 
                                                                 && t.DestinationID==bidProposal.DestinationID
                                                                 && t.TransporterID==bidProposal.TransporterID);
                    var detail = detai.FirstOrDefault();
                    
                    if (detail != null)
                    {
                        //detail.TransportBidQuotationID = bidProposal.TransportBidQuotationID;
                        detail.BidID = bidProposal.BidID;
                        detail.TransporterID = bidProposal.TransporterID;
                        detail.SourceID = bidProposal.SourceID;
                        detail.DestinationID = bidProposal.DestinationID;
                        detail.Tariff = bidProposal.Tariff;
                        detail.Remark = bidProposal.Remark;
                        detail.IsWinner = false;
                        _transportBidQuotationService.UpdateTransportBidQuotation(detail);

                        
                    }

                    else
                    {
                        var newProposal = new TransportBidQuotation();
                        //newProposal.TransportBidQuotationID = bidProposal.TransportBidQuotationID;
                        newProposal.BidID = bidProposal.BidID;
                        newProposal.TransporterID = bidProposal.TransporterID;
                        newProposal.SourceID = bidProposal.SourceID;
                        newProposal.DestinationID = bidProposal.DestinationID;
                        newProposal.Tariff = bidProposal.Tariff;
                        newProposal.Remark = bidProposal.Remark;
                        newProposal.IsWinner = false;
                        _transportBidQuotationService.AddTransportBidQuotation(newProposal);
                    }

                int region = _adminUnitService.FindById(bidProposal.DestinationID).AdminUnit2.AdminUnit2.AdminUnitID;
                var changeable = _transportBidQuotationService.FindBy(t =>
                                                                     t.BidID == bidProposal.BidID
                                                                     && t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == region);
                    
                foreach (var transportBidQuotation in  changeable)
                    {
                        transportBidQuotation.IsWinner = false;
                        _transportBidQuotationService.UpdateTransportBidQuotation(transportBidQuotation);
                    }
            }
            
            return Json(new[] { bidProposal }.ToDataSourceResult(request, ModelState));
            //return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult bidProposals ()
        {
            LoadLookups();
            return View();
        }

        //GET: /Procurement/RFQ/EditStart
        [HttpPost]
        public ActionResult EditStart(PriceQuotationFilterViewModel model)
        {
            List<PriceQuotationDetailViewModel> qoutation = populateForm(model);
            Session["PriceQuotationFilter"] = model;
            return View(qoutation);

        }

        [HttpGet]
        public ActionResult BidProposal()
        {
            var filter = new PriceQuotationFilterViewModel();
            ViewBag.filter = filter;
            LoadLookups();
            return View(filter);
        }

        [HttpPost]
        public  ActionResult BidProposal(PriceQuotationFilterViewModel filter)
        {
            ViewBag.filter = filter;
            LoadLookups();
            return View(filter);
        }

        [HttpGet]
        public ActionResult GenerateWinners()
        {
            var filter = new WinnersGeneratorParameters();
            ViewBag.filter = filter;
            ViewBag.BidID = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            return View(filter);
        }

        [HttpPost]
        public ActionResult GenerateWinners(WinnersGeneratorParameters filter)
        {
            ViewBag.filter = filter;
            ViewBag.BidID = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            //ModelState.AddModelError("Success", "Winner transporters for the specific BID and Region are generated.");
            return View(filter);
        }

        public ActionResult DeleteAjax(int TransportBidQuotationID)
        {
            _bidQuotationService.DeleteById(TransportBidQuotationID);
            return Json("{}");
        }

        public ActionResult ReadBidProposals([DataSourceRequest] DataSourceRequest request, int bidID, int regionID, int transporterID)
        {
            //var d = _transportBidQuotationService.FindBy(t=>t.BidID==bidPlanID
            //                                             && t.TransporterID==transporterID 
            //                                             && t.Destination.AdminUnit2.AdminUnit2.AdminUnitID==regionID
            //                                             );
            //var bid= _bidService.FindById(bidPlanID);
            //bid.BidID;

            //ModelState.AddModelError("Success", "Reading....");
            int planID = _bidService.FindById(bidID).TransportBidPlanID;
            
            var bidPlanDetail =
                _transportBidPlanDetailService.FindBy(t => t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == regionID
                                                        && t.BidPlanID == planID
                                                        /*&& t.Quantity > 0*/);
            var df = (from planDetail in bidPlanDetail 
                     group planDetail by new
                         {
                             planDetail.DestinationID,
                             planDetail.SourceID
                         }
                         into  gr select gr
                      );
            
            var detailPlans = df.Select(d => d.ToList()).Select(er => er.FirstOrDefault()).ToList();


            //var s = (from transportBidQuotation in d
            //         select new PriceQuotationDetail()
            //         {
            //             SourceWarehouse = transportBidQuotation.Source.Name,
            //             Zone = transportBidQuotation.Destination.AdminUnit2.Name,
            //             Woreda = transportBidQuotation.Destination.Name,
            //             Tariff = transportBidQuotation.Tariff,
            //             Remark = transportBidQuotation.Remark,
            //             BidID = transportBidQuotation.BidID,
            //             DestinationID = transportBidQuotation.DestinationID,
            //             SourceID = transportBidQuotation.SourceID,
            //             TransportBidQuotationID = transportBidQuotation.TransportBidQuotationID*10 + transporterID,
            //             TransporterID = transportBidQuotation.TransporterID
            //         }
            //        );

            var result = new List<PriceQuotationDetail>();

            foreach (var transportBidPlanDetail in detailPlans)
            {
                var pdetail = transportBidPlanDetail;

                var detail = _transportBidQuotationService.FindBy(t => t.BidID == bidID
                                                                && t.SourceID == pdetail.SourceID
                                                                && t.DestinationID == pdetail.DestinationID
                                                                && t.TransporterID == transporterID).FirstOrDefault();
                if (detail !=null)
                {
                    var t = new PriceQuotationDetail
                        {
                            SourceWarehouse = detail.Source.Name,
                            Zone = detail.Destination.AdminUnit2.Name,
                            Woreda = detail.Destination.Name,
                            Tariff = detail.Tariff,
                            Remark = detail.Remark,
                            BidID = detail.BidID,
                            DestinationID = detail.DestinationID,
                            SourceID = detail.SourceID,
                            TransportBidQuotationID = detail.TransportBidQuotationID*10 + transporterID,
                            TransporterID = detail.TransporterID
                            
                        };
                    result.Add(t);
                    continue;
                   
                }

                var n = new PriceQuotationDetail()
                    {
                        SourceWarehouse = transportBidPlanDetail.Source.Name,
                        Zone = transportBidPlanDetail.Destination.AdminUnit2.Name,
                        Woreda = transportBidPlanDetail.Destination.Name,
                        Tariff = 0,
                        Remark = String.Empty,
                        BidID = bidID,
                        DestinationID = transportBidPlanDetail.DestinationID,
                        SourceID = transportBidPlanDetail.SourceID,
                        TransportBidQuotationID = transportBidPlanDetail.TransportBidPlanDetailID * 10 + transporterID,
                        TransporterID = transporterID
                    };
                result.Add(n);   
            }
            
            //var s = (from transportBidQuotation in bidPlanDetail
            //         select new PriceQuotationDetail()
            //         {
            //             SourceWarehouse = transportBidQuotation.Source.Name,
            //             Zone = transportBidQuotation.Destination.AdminUnit2.Name,
            //             Woreda = transportBidQuotation.Destination.Name,
            //             Tariff = 0,
            //             Remark = String.Empty,
            //             BidID = transportBidQuotation.BidPlanID,
            //             DestinationID = transportBidQuotation.DestinationID,
            //             SourceID = transportBidQuotation.SourceID,
            //             TransportBidQuotationID = transportBidQuotation.TransportBidPlanDetailID*10 + transporterID,
            //             TransporterID = transporterID
            //         }
            //        );
            
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public void ClearBidWinners()
        {
            int bidNumber = 15;
            int regionID = 2;

            var oldWinners =
               _bidWinnerService.FindBy(
                   b => b.BidID == bidNumber && b.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionID);


            foreach (var oldWinner in oldWinners)
            {
                _bidWinnerService.DeleteBidWinner(oldWinner);
            }
        }

        private bool IdentifyWinners(int bidNumber, int regionID)
        {
            bool result = false;

           

            var rawData = _transportBidQuotationService.FindBy(
                                                t => t.BidID == bidNumber 
                                                && t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == regionID
                                                && !t.IsWinner);

            if (rawData !=null)
            {
                var grouped = (
                                  from r in rawData
                                  group r by new
                                      {
                                          r.DestinationID,
                                          r.SourceID
                                      }
                                  into g select g
                              );


                foreach (var eachgroup in grouped)
                {
                    var candidates = eachgroup.ToList();

                    var firstWinners = (
                                         candidates.Where(candidate => candidate.Tariff == candidates.Min(t => t.Tariff))
                                       );

                    var secondCandidates = candidates.Where(t => t.Tariff > candidates.Min(d => d.Tariff));
                     
                    var transportBidQuotations = secondCandidates as List<TransportBidQuotation> ??
                                                 secondCandidates.ToList();
                    var secondWinners = (
                                            transportBidQuotations.Where(
                                            secondCadidate =>
                                            secondCadidate.Tariff == transportBidQuotations.Min(t => t.Tariff))
                                        );
                    
                    var firstBidWinners = TransformBidQuotationToBidWinner(firstWinners.ToList(), 1);
                    var secondBidWinners = TransformBidQuotationToBidWinner(secondWinners.ToList(), 2);

                    foreach (var firstBidWinner in firstBidWinners)
                    {
                        _bidWinnerService.AddBidWinner(firstBidWinner);
                    }

                    foreach (var secondBidWinner in secondBidWinners)
                    {
                        _bidWinnerService.AddBidWinner(secondBidWinner);
                    }

                }

                foreach (var transportBidQuotation in rawData)
                {
                     transportBidQuotation.IsWinner = true;
                    _transportBidQuotationService.UpdateTransportBidQuotation(transportBidQuotation);
                }

                result = true;
            }
            _bidWinnerService.Save();
            return result;
            
            //if(rawData != null)
            //{
            //    foreach (var data in rawData)
            //    {
            //        var each = data;

            //        var candidates = (from raw in rawData
            //                          where raw.DestinationID == each.DestinationID && raw.SourceID == each.SourceID
            //                          select raw);

            //        var winner = (
            //                         from candidate in candidates
            //                         orderby candidate.Tariff
            //                         select candidate
            //                     );

            //        var bidWinner = new BidWinner();
            //        bidWinner.SourceID = winner.First().SourceID;
            //        bidWinner.DestinationID = winner.First().DestinationID;
            //        bidWinner.BidID = winner.First().BidID;
            //        bidWinner.TransportOrderID = 5;
            //        bidWinner.CommodityID = 1;
            //        bidWinner.TransporterID = winner.First().TransporterID;
            //        bidWinner.Amount = 500;
            //        bidWinner.Tariff = winner.First().Tariff;
            //        bidWinner.Position = winner.First().Position;
            //        bidWinner.Status = 1;
            //        bidWinner.ExpiryDate = DateTime.Today;

            //        _bidWinnerService.AddBidWinner(bidWinner);
            //    }
            //}
        }

        public List<BidWinner> TransformBidQuotationToBidWinner(List<TransportBidQuotation> proposals, int rank)
        {
            var winners = new List<BidWinner>();
            foreach (var proposal in proposals)
            {
                var winner = new BidWinner();
                winner.SourceID = proposal.SourceID;
                winner.DestinationID = proposal.DestinationID;
                winner.BidID = proposal.BidID;
                winner.CommodityID = 1;
                winner.TransporterID = proposal.TransporterID;
                winner.Amount = 0;
                winner.Tariff = proposal.Tariff;
                winner.Position = rank;
                winner.Status = 1;
                winner.ExpiryDate = DateTime.Today;
                winner.BidWinnerID = 0;

                winners.Add(winner);
            }
            return winners;
        }

        public ActionResult ReadBidWinners([DataSourceRequest] DataSourceRequest request, int bidNumber, int regionID)
        {
            //List<BidWinnerViewModel> s = null;

            //if (bidPlanID != 0 && regionID != 0)
            //{
            var dr = _bidWinnerService.FindBy(t =>
                    t.BidID == bidNumber
                    && t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionID
                );
            
            //string nam4e = dr.FirstOrDefault().Hub.Name;

            var r = new List<BidWinnerViewModel>();

            if(IdentifyWinners(bidNumber, regionID))
            {
                var d = _bidWinnerService.FindBy(t => 
                    t.BidID == bidNumber
                    && t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionID
                );

                _bidWinnerService.Save();

                string name = d.FirstOrDefault().Hub.Name;
                
                if (d !=null)
                {
                    r = d.Select(bidWinner => new BidWinnerViewModel()
                        {
                            BidWinnnerID = bidWinner.BidWinnerID,
                            SourceWarehouse = bidWinner.Hub.Name,
                            Zone = bidWinner.AdminUnit.AdminUnit2.Name,
                            Woreda = bidWinner.AdminUnit.Name,
                            TransporterName = bidWinner.Transporter.Name,
                            Rank = bidWinner.Position,
                            WinnerTariff = bidWinner.Tariff,
                            SourceId = bidWinner.SourceID,
                            DestinationId = bidWinner.DestinationID,
                            TransporterID = bidWinner.TransporterID
                        }).ToList();
                }
            }

            return Json(r.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            
            // if (d!=null)
            //{
            

            //IEnumerable<BidWinnerViewModel> s = (from bidWinner in d
            //                 select new BidWinnerViewModel()
            //                     {
            //                         BidWinnnerID = bidWinner.BidWinnerID,
            //                         SourceWarehouse = bidWinner.Hub.Name,
            //                         Zone = bidWinner.AdminUnit.AdminUnit2.Name,
            //                         Woreda = bidWinner.AdminUnit.Name,
            //                         TransporterName = bidWinner.Transporter.Name,
            //                         Rank = bidWinner.Position,
            //                         WinnerTariff = bidWinner.Tariff,
            //                         SourceId = bidWinner.SourceID,
            //                         DestinationId = bidWinner.DestinationID,
            //                         TransporterID = bidWinner.TransporterID
            //                     }
            //                );
            //}
        //}

   
        }

        public ActionResult ReadAjax([DataSourceRequest] DataSourceRequest request)
        {
            PriceQuotationFilterViewModel model = (PriceQuotationFilterViewModel)Session["PriceQuotationFilter"];
            List<PriceQuotationDetailViewModel> qoutation = populateForm(model);
            return Json(qoutation.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditAjax([DataSourceRequest] DataSourceRequest request, PriceQuotationDetailViewModel item)
        {
            if (ModelState.IsValid)
            {
                List<TransportBidQuotation> existing =
                    _bidQuotationService.FindBy(t => t.BidID == item.BidID
                                               && t.TransporterID == item.TransporterID
                                               && t.SourceID == item.SourceID
                                               && t.DestinationID == item.DestinationID
                                               );
                var edited = new TransportBidQuotation();
                if (existing.Count == 1)
                {
                    edited = existing[0];
                }
                //                    transportQuote.TransportBidQuotationID = edited.TransportBidQuotationID;
                edited.Tariff = item.Tariff;
                edited.Remark = item.Remark;
                edited.Position = item.Rank;
                edited.IsWinner = item.IsWinner;

                edited.TransporterID = item.TransporterID;
                edited.SourceID = item.SourceID;
                edited.DestinationID = item.DestinationID;
                edited.BidID = item.BidID;

                // edited.
                if (existing.Count == 1)
                {
                    _bidQuotationService.UpdateTransportBidQuotation(edited);
                }
                else
                {
                    _bidQuotationService.AddTransportBidQuotation(edited);
                }
                return Json(new[] { item }.ToDataSourceResult(request, ModelState));

            }
            return Json("{}", JsonRequestBehavior.AllowGet);
        }
    }
}