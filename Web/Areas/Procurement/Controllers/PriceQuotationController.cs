using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Helpers;
using Cats.Models;
using Cats.Data;
using Cats.Services.Procurement;
using Cats.Services.EarlyWarning;
using Cats.Services.Common;
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
        private readonly ITransportBidQuotationHeaderService _transportBidQuotationHeaderService;
        private readonly IBidWinnerService _bidWinnerService;
        private readonly IBusinessProcessService _BusinessProcessService;
        private readonly IApplicationSettingService _ApplicationSettingService;
        private readonly ITransportOrderService _transportOrderService;
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
                                            , IBusinessProcessService businessProcessService
                                            , IApplicationSettingService applicationSettingService
                                            , ITransportBidQuotationHeaderService transportBidQuotationHeaderService
                                            , ITransportOrderService transportOrderService  )
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
            this._BusinessProcessService = businessProcessService;
            this._transportBidQuotationHeaderService = transportBidQuotationHeaderService;
            this._ApplicationSettingService = applicationSettingService;
            _transportOrderService = transportOrderService;
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
            //LoadLookups();
            //var model = new PriceQuotationFilterViewModel();
            //return View(model);
            return Redirect(@Url.Action("BidProposalHeader","PriceQuotation"));
            //return View();
        }

        //[HttpPost]
        //public ActionResult Edit(TransportBidQuotation transportQuote)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        List<TransportBidQuotation> existing =
        //            _bidQuotationService.FindBy(t => t.BidID == transportQuote.BidID
        //                                       && t.TransporterID == transportQuote.TransporterID
        //                                       && t.SourceID == transportQuote.SourceID
        //                                       && t.DestinationID == transportQuote.DestinationID
        //                                       );
        //        if (existing.Count == 1)
        //        {
        //            TransportBidQuotation edited = existing[0];
        //            //                    transportQuote.TransportBidQuotationID = edited.TransportBidQuotationID;
        //            edited.Tariff = transportQuote.Tariff;
        //            edited.Remark = transportQuote.Remark;
        //            edited.Position = transportQuote.Position;
        //            edited.IsWinner = transportQuote.IsWinner;
        //            _bidQuotationService.UpdateTransportBidQuotationHeader(edited);
        //        }
        //        else
        //        {
        //            _bidQuotationService.AddTransportBidQuotationHeader(transportQuote);
        //        }
        //        return View(transportQuote);

        //    }
        //    return RedirectToAction("Index");
        //}
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

        [AcceptVerbs(HttpVerbs.Post)]
        //[ProcurementAuthorize(operation = ProcurementCheckAccess.Operation.Bid_Planning)]
        public ActionResult SaveBidProposals([DataSourceRequest] DataSourceRequest request, List<PriceQuotationDetail> models)
        {
            foreach (PriceQuotationDetail bidProposal in models)
            {
                if (bidProposal != null && ModelState.IsValid)
                {
                    //var detail = _transportBidQuotationService.FindById(bidProposal.TransportBidQuotationID);
                    var detai = _transportBidQuotationService.FindBy(t =>
                                                                        t.BidID == bidProposal.BidID
                                                                     && t.SourceID == bidProposal.SourceID
                                                                     && t.DestinationID == bidProposal.DestinationID
                                                                     && t.TransporterID == bidProposal.TransporterID);
                    var detail = detai.FirstOrDefault();

                    if (detail != null)
                    {
                        //detail.TransportBidQuotationID = bidProposal.TransportBidQuotationID;
                        detail.BidID = bidProposal.BidID;
                        detail.TransportBidQuotationHeaderID = bidProposal.HeaderId;
                        detail.TransporterID = bidProposal.TransporterID;
                        detail.SourceID = bidProposal.SourceID;
                        detail.DestinationID = bidProposal.DestinationID;
                        detail.Tariff = bidProposal.Tariff;
                        detail.Remark = bidProposal.Remark ?? String.Empty;
                        detail.IsWinner = false;
                        try
                        {
                            _transportBidQuotationService.UpdateTransportBidQuotation(detail);
                        }
                        catch (Exception e) { }

                    }

                    else
                    {
                        var newProposal = new TransportBidQuotation();
                        //newProposal.TransportBidQuotationID = bidProposal.TransportBidQuotationID;
                        newProposal.BidID = bidProposal.BidID;
                        newProposal.TransportBidQuotationHeaderID = bidProposal.HeaderId;
                        newProposal.TransporterID = bidProposal.TransporterID;
                        newProposal.SourceID = bidProposal.SourceID;
                        newProposal.DestinationID = bidProposal.DestinationID;
                        newProposal.Tariff = bidProposal.Tariff;
                        newProposal.Remark = bidProposal.Remark ?? String.Empty;
                        newProposal.IsWinner = false;
                        try
                        {
                            _transportBidQuotationService.AddTransportBidQuotation(newProposal);
                        }
                        catch (Exception e) { }
                    }

                    //int region = _adminUnitService.FindById(bidProposal.DestinationID).AdminUnit2.AdminUnit2.AdminUnitID;
                    //var changeable = _transportBidQuotationService.FindBy(t =>
                    //                                                     t.BidID == bidProposal.BidID);
                       

                    //foreach (var transportBidQuotation in changeable)
                    //{
                    //    transportBidQuotation.IsWinner = false;
                    //    _transportBidQuotationService.UpdateTransportBidQuotation(transportBidQuotation);
                    //}
                }
            }
            return Json(models.ToDataSourceResult(request, ModelState));

        }

        public ActionResult bidProposals()
        {
            LoadLookups();
            return View();
        }

        [HttpGet]
        public ActionResult BidProposal()
        {
            var filter = new PriceQuotationFilterViewModel();
            ViewBag.filter = filter;
            LoadLookups();
            return View(filter);
        }

        public ActionResult BidProposalHeader()
        {
            if (TempData["CustomError"] != null) { ModelState.AddModelError("Errors", TempData["CustomError"].ToString()); }

            if (TempData["CustomMessage"] != null) { ModelState.AddModelError("Success", TempData["CustomMessage"].ToString()); }
            
            return View();
        }

        public ActionResult Read_BidProposals([DataSourceRequest]DataSourceRequest request)
        {
            var proposals = _transportBidQuotationHeaderService.GetAllTransportBidQuotationHeader().OrderByDescending(t=>t.TransportBidQuotationHeaderID);
            var r = (from proposal in proposals
                     select new TransportBidQuotationHeaderViewModel()
                     {
                         TransportBidQuotationHeaderID = proposal.TransportBidQuotationHeaderID,
                         BidNumber = proposal.Bid.BidNumber,
                         //BidBondAmount = proposal.BidBondAmount,
                         OffersCount = proposal.TransportBidQuotations.Count,
                         Region = proposal.AdminUnit.Name,
                         Status = _transportBidQuotationHeaderService.GetStatus(proposal.TransportBidQuotationHeaderID),//  proposal.Status == 1 ? "Draft" : "Approved",
                         Transporter = proposal.Transporter.Name,
                         EnteredBy = proposal.EnteredBy,
                         BidID = proposal.Bid.BidID,
                         RegionId = proposal.AdminUnit.AdminUnitID,
                         TransporterId = proposal.Transporter.TransporterID,
                         CanApprove = CanApprove(proposal.Bid)
                     });

            //var r = new List<TransportBidQuotationHeaderViewModel>
            //    {
            //        new TransportBidQuotationHeaderViewModel()
            //            {
            //                TransportBidQuotationHeaderID = 12,
            //                BidNumber = "ID0021",
            //                BidBondAmount = 201,
            //                OffersCount = 50,
            //                Region = "Afar",
            //                Status = 1,
            //                Transporter = "Woyera",
            //                EnteredBy = "Amen"
            //            }
            //    };

            return Json(r.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public bool CanApprove(Bid bid)
        {
             var user = (UserIdentity)System.Web.HttpContext.Current.User.Identity;
            return bid.UserProfileId == user.Profile.UserProfileID;
        }

        public ActionResult BidProposalHeader_delete(int id)
        {
            var proposal = _transportBidQuotationHeaderService.FindById(id);
            try
            {
                _transportBidQuotationHeaderService.DeleteTransportBidQuotationHeader(proposal);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Errors", @"Unable to delete bid header");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            ViewBag.BIDS = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.Regions = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.Transporters = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");
            var header = new TransportBidQuotationHeader();
            var user = (UserIdentity)System.Web.HttpContext.Current.User.Identity;
            header.EnteredBy = user.FullName;
            return View(header);
        }

        public ActionResult ApproveProposal(int id)
        {
            var proposal = _transportBidQuotationHeaderService.FindById(id);
            proposal.Status = 2;
            _transportBidQuotationHeaderService.UpdateTransportBidQuotationHeader(proposal);
            ModelState.AddModelError("Success", @"Bid proposal approved");
            return RedirectToAction("BidProposalHeader");
        }

        [HttpPost]
        public ActionResult Create(TransportBidQuotationHeader bidProposal)
        {
            ViewBag.BIDS = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.Regions = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.Transporters = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");
           
            if (ModelState.IsValid)
            {

                var exisiting = _transportBidQuotationHeaderService.FindBy(m => m.BidId == bidProposal.BidId
                                                                && m.TransporterId == bidProposal.TransporterId
                                                                && m.RegionID == bidProposal.RegionID);

                if (exisiting.Count==0)
                {
                    bidProposal.Status = 1;
                    bidProposal.BidQuotationDate = DateTime.Now;
                    _transportBidQuotationHeaderService.AddTransportBidQuotationHeader(bidProposal);
                    TempData["CustomMessage"] = "Bid proposal successfully created";
                    return RedirectToAction("BidProposalHeader");
                }
                else
                {
                    TempData["CustomError"] = "Such a bid proposal has already been proposed";
                }
                
            }
            return RedirectToAction("BidProposalHeader");
        }

        [HttpPost]
        public ActionResult BidProposal(PriceQuotationFilterViewModel filter)
        {
            ViewBag.filter = filter;
            LoadLookups();
            return View(filter);
        }

        public ActionResult Details(int id)
        {
            var d = _transportBidQuotationHeaderService.FindById(id);
            var s = new TransportBidQuotationHeaderViewModel
                {
                    TransportBidQuotationHeaderID = d.TransportBidQuotationHeaderID,
                    BidNumber = d.Bid.BidNumber,
                    BidBondAmount = d.Bid.BidBondAmount,
                    OffersCount = d.TransportBidQuotations.Count,
                    Region = d.AdminUnit.Name,
                    Status = d.Status == 1 ? "Draft" : "Approved",
                    Transporter = d.Transporter.Name,
                    EnteredBy = d.EnteredBy,
                    BidID = d.Bid.BidID,
                    RegionId = d.AdminUnit.AdminUnitID,
                    TransporterId = d.Transporter.TransporterID
                };
            return View(s);
        }

        [HttpGet]
        public ActionResult GenerateWinners()
        {
            var filter = new WinnersGeneratorParameters();
            ViewBag.filter = filter;
            ViewBag.BidID = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            if (TempData["CustomErrorMessage"] != null) { ModelState.AddModelError("Errors", TempData["CustomErrorMessage"].ToString()); }
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
            var bid = _bidService.FindById(bidID) ?? null;

            var result = new List<PriceQuotationDetail>();

            if (bid != null)
            {
                var bidPlanDetail =
                    _transportBidPlanDetailService.FindBy(t => t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == regionID
                                                            && t.BidPlanID == bid.TransportBidPlanID
                    /*&& t.Quantity > 0*/);
                var df = (from planDetail in bidPlanDetail
                          group planDetail by new
                          {
                              planDetail.DestinationID,
                              planDetail.SourceID
                          }
                              into gr
                              select gr
                          );

                var detailPlans = df.Select(d => d.ToList()).Select(er => er.FirstOrDefault()).ToList();


                foreach (var transportBidPlanDetail in detailPlans)
                {
                    var pdetail = transportBidPlanDetail;

                    var detail = _transportBidQuotationService.FindBy(t => t.BidID == bidID
                                                                    && t.SourceID == pdetail.SourceID
                                                                    && t.DestinationID == pdetail.DestinationID
                                                                    && t.TransporterID == transporterID).FirstOrDefault();
                    if (detail != null)
                    {
                        var t = new PriceQuotationDetail
                        {
                            SourceWarehouse = detail.Hub.Name,
                            Zone = detail.AdminUnit.AdminUnit2.Name,
                            Woreda = detail.AdminUnit.Name,
                            Tariff = detail.Tariff,
                            Remark = detail.Remark,
                            BidID = detail.BidID,
                            DestinationID = detail.DestinationID,
                            SourceID = detail.SourceID,
                            TransportBidQuotationID = detail.TransportBidQuotationID * 10 + transporterID,
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
            }

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Read_Proposals([DataSourceRequest] DataSourceRequest request, int bidProposalId)
        {
            var s = _transportBidQuotationHeaderService.FindById(bidProposalId);
            int bidID = s.Bid.BidID;
            int regionID = s.AdminUnit.AdminUnitID;
            int transporterID = s.Transporter.TransporterID;

            var bid = _bidService.FindById(bidID) ?? null;

            var result = new List<PriceQuotationDetail>();

            if (bid != null)
            {
                var bidPlanDetail =
                    _transportBidPlanDetailService.FindBy(t => t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == regionID
                                                            && t.BidPlanID == bid.TransportBidPlanID
                    /*&& t.Quantity > 0*/);
                var df = (from planDetail in bidPlanDetail
                          group planDetail by new
                          {
                              planDetail.DestinationID,
                              planDetail.SourceID
                          }
                              into gr
                              select gr
                          );

                var detailPlans = df.Select(d => d.ToList()).Select(er => er.FirstOrDefault()).ToList();


                foreach (var transportBidPlanDetail in detailPlans)
                {
                    var pdetail = transportBidPlanDetail;

                    var detail = _transportBidQuotationService.FindBy(t => t.BidID == bidID
                                                                    && t.SourceID == pdetail.SourceID
                                                                    && t.DestinationID == pdetail.DestinationID
                                                                    && t.TransporterID == transporterID).FirstOrDefault();
                    if (detail != null)
                    {
                        var t = new PriceQuotationDetail
                        {
                            SourceWarehouse = detail.Hub.Name,
                            Zone = detail.AdminUnit.AdminUnit2.Name,
                            Woreda = detail.AdminUnit.Name,
                            Tariff = detail.Tariff,
                            Remark = detail.Remark,
                            //
                            BidID = detail.BidID,
                            DestinationID = detail.DestinationID,
                            SourceID = detail.SourceID,
                            HeaderId = bidProposalId,
                            TransportBidQuotationID = detail.TransportBidQuotationID * 10 + transporterID,
                            //
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
                        //
                        BidID = bidID,
                        DestinationID = transportBidPlanDetail.DestinationID,
                        SourceID = transportBidPlanDetail.SourceID,
                        HeaderId = bidProposalId,
                        //
                        TransportBidQuotationID = transportBidPlanDetail.TransportBidPlanDetailID * 10 + transporterID,
                        TransporterID = transporterID
                    };
                    result.Add(n);
                }
            }

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

        public ActionResult IdentifyWinners(int bidNumber, int regionID)
        {
            //bool result = false;
            var f = new WinnersGeneratorParameters()
                {
                    BidID = bidNumber,
                    RegionID = regionID
                };
            var comparable = new List<TransportBidQuotationHeader>();
            var bid = _bidService.FindById(bidNumber);
            if (bid!=null)
            {
                var transportOrderCreated =
                    _transportOrderService.FindBy(m => m.BidDocumentNo == bid.BidNumber).FirstOrDefault();
                if (transportOrderCreated!=null)
                {
                    TempData["CustomErrorMessage"] = "Bid Winner Can not be generated (Transport Order already created from this bid)";
                    return RedirectToAction("GenerateWinners", "PriceQuotation");
                }
            }
            
            
            var bidWinners = _bidWinnerService.FindBy(m=>m.BidID==bidNumber);
            if (bidWinners != null)
            {
                comparable = _transportBidQuotationHeaderService.FindBy(m => m.RegionID == regionID && m.BidId == bidNumber && m.Status >=2);
                foreach (var winner in bidWinners)
                {
                    _bidWinnerService.DeleteBidWinner(winner);
                }
                
            }
            else
            {
                comparable = _transportBidQuotationHeaderService.FindBy(m => m.RegionID == regionID && m.BidId == bidNumber && m.Status == 2);
           
            }
            ViewBag.Status = comparable==null ? 1 : 2;

            
            //TempData["Error"] = "There are no new proposals, winners may already have been identified";
            var tr = new List<TransportBidQuotation>();

            foreach (var transportBidQuotationHeader in comparable)
            {
                tr.AddRange(transportBidQuotationHeader.TransportBidQuotations);
                //transportBidQuotationHeader.TransportBidQuotations
            }

          
            var rawData = tr;


            if (rawData.Count > 0)
            {
                var grouped = (
                                  from r in rawData
                                  group r by new
                                      {
                                          r.DestinationID,
                                          r.SourceID
                                      }
                                      into g
                                      select g
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

                    int bpid = _ApplicationSettingService.getBidWinnerWorkflow();


                    foreach (var firstBidWinner in firstBidWinners)
                    {
                        _bidWinnerService.AddBidWinner(firstBidWinner);
                        //BusinessProcess bp = _BusinessProcessService.CreateBusinessProcessForObject(bpid, firstBidWinner.BidWinnerID, "Bid Winner");
                        //firstBidWinner.BusinessProcess = bp;
                        //firstBidWinner.BusinessProcessID = bp.BusinessProcessID;
                    }

                    foreach (var secondBidWinner in secondBidWinners)
                    {
                        _bidWinnerService.AddBidWinner(secondBidWinner);
                        //BusinessProcess bp = _BusinessProcessService.CreateBusinessProcessForObject(bpid, secondBidWinner.BidWinnerID, "Bid Winner");
                        //secondBidWinner.BusinessProcess = bp;
                    }

                }


                foreach (var transportBidQuotationHeader in comparable)
                {
                    transportBidQuotationHeader.Status = 3;
                    _transportBidQuotationHeaderService.UpdateTransportBidQuotationHeader(transportBidQuotationHeader);
                }

                

                //result = true;
            }
            
            _bidWinnerService.Save();
            
            return RedirectToAction("Winners", new {BidID = bidNumber, RegionID=regionID});
          
        }

        public ActionResult Winners(int BidID , int RegionID)
        {
            if (ViewBag.Status==1)
            {
                ModelState.AddModelError("Error", @"There are no new proposals, winners may have already been identified");
            }
            else if(ViewBag.Status==2)
            {
                ModelState.AddModelError("Success", @"Winners Successfully identified!");
            }
            else
            {
                ModelState.AddModelError("info", @"Showing already generated winners");   
            }

            //ViewBag.filter = filter;
            ViewBag.BidID = BidID;
            ViewBag.RegionID = RegionID;
            return View();
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
           // var s = _bidWinnerService.GetAllBidWinner();
            
            var dr = _bidWinnerService.FindBy(t =>
                    t.BidID == bidNumber
                    && t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionID
                );

            //string nam4e = dr.FirstOrDefault().Hub.Name;

            var r = new List<BidWinnerViewModel>();

            //if (IdentifyWinners(bidNumber, regionID))
            if (true)
            {
                var d = _bidWinnerService.FindBy(t =>
                    t.BidID == bidNumber
                    && t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionID
                );

                _bidWinnerService.Save();

                //string name = d.FirstOrDefault().Hub.Name;

                if (d != null)
                {
                    r = d.Select(bidWinner => new BidWinnerViewModel()
                        {
                            BidWinnnerID = bidWinner.BidWinnerID,
                            SourceWarehouse = bidWinner.Hub.Name,
                            Zone = bidWinner.AdminUnit.AdminUnit2.Name,
                            Woreda = bidWinner.AdminUnit.Name,
                            TransporterName = bidWinner.Transporter.Name,
                            Rank = bidWinner.Position ?? 1,
                            WinnerTariff = bidWinner.Tariff,
                            SourceId = bidWinner.SourceID,
                            DestinationId = bidWinner.DestinationID,
                            TransporterID = bidWinner.TransporterID
                        }).ToList();
                }
            }

            return Json(r.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit_proposal(int id)
        {
            ViewBag.BIDS = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.Regions = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.Transporters = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");
            //ViewBag.EnteredBy = "Me";
            var header = _transportBidQuotationHeaderService.FindById(id);
            return View(header);
        }

    
        [HttpPost]
        public ActionResult Edit_proposal(TransportBidQuotationHeader proposal)
        {
            ViewBag.BIDS = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.Regions = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.Transporters = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");

            if (ModelState.IsValid)
            {
                var e = _transportBidQuotationHeaderService.FindById(proposal.TransportBidQuotationHeaderID);
                
//                e.BidBondAmount = proposal.BidBondAmount;

                _transportBidQuotationHeaderService.UpdateTransportBidQuotationHeader(e);
                ModelState.AddModelError("Success", @"Proposal updated successfully");
                return RedirectToAction("BidProposalHeader");               
            }
           return View(proposal);
        }

        public ActionResult ReadApprovedProposals([DataSourceRequest] DataSourceRequest request, int bidID, int regionID)
        {
            var proposals = _transportBidQuotationHeaderService.FindBy(m => m.BidId == bidID
                && m.RegionID == regionID && m.Status >= 2);

            var r = (from proposal in proposals
                     select new TransportBidQuotationHeaderViewModel()
                     {
                         TransportBidQuotationHeaderID = proposal.TransportBidQuotationHeaderID,
                         BidNumber = proposal.Bid.BidNumber,
                         //BidBondAmount = proposal.BidBondAmount,
                         OffersCount = proposal.TransportBidQuotations.Count,
                         Region = proposal.AdminUnit.Name,
                         Status = proposal.Status == 1 ? "Draft" : "Approved",
                         Transporter = proposal.Transporter.Name,
                         EnteredBy = proposal.EnteredBy,
                         BidID = proposal.Bid.BidID,
                         RegionId = proposal.AdminUnit.AdminUnitID,
                         TransporterId = proposal.Transporter.TransporterID
                     });
            return Json(r.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        //START~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~B
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
        
        //GET: /Procurement/RFQ/EditStart
        [HttpPost]
        public ActionResult EditStart(PriceQuotationFilterViewModel model)
        {
            List<PriceQuotationDetailViewModel> qoutation = populateForm(model);
            Session["PriceQuotationFilter"] = model;
            return View(qoutation);

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
        //END~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~B
    }
}