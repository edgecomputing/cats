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


        public PriceQuotationController(ITransportBidPlanService transportBidPlanServiceParam
                                            , IAdminUnitService adminUnitServiceParam
                                            , IProgramService programServiceParam
                                            , ITransportBidPlanDetailService transportBidPlanDetailServiceParam
                                            , IHubService hubServiceParam
                                            , ITransportBidQuotationService bidQuotationServiceParam
                                            , ITransporterService transporterServiceParam
                                            , IBidService bidServiceParam
                                            , ITransportBidQuotationService transportBidQuotationService
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
        }

        public void LoadLookups()
        {
            ViewBag.BidPlanID = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
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
            int bidID = model.BidPlanID;


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
        [ProcurementAuthorize(operation = ProcurementCheckAccess.Operation.Bid_Planning)]
        public ActionResult SaveBidProposals([DataSourceRequest] DataSourceRequest request, List<PriceQuotationDetail> bidProposals)
        {
            if (bidProposals != null && ModelState.IsValid)
            {
                foreach (var priceQuotationDetail in bidProposals)
                {
                    var detail = _transportBidQuotationService.FindById(priceQuotationDetail.TransportBidQuotationID);
                    
                    if (detail != null)
                    {
                        detail.TransportBidQuotationID = priceQuotationDetail.TransportBidQuotationID;
                        detail.BidID = priceQuotationDetail.BidID;
                        detail.TransporterID = priceQuotationDetail.TransporterID;
                        detail.SourceID = priceQuotationDetail.SourceID;
                        detail.DestinationID = priceQuotationDetail.DestinationID;
                        _transportBidQuotationService.UpdateTransportBidQuotation(detail);
                    }
                }
                

            }
            return Json(new[] { bidProposals }.ToDataSourceResult(request, ModelState));
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
            /*LoadLookups();
            ViewBag.ModelFilter = model;
            ViewBag.SelectedRegion = _adminUnitService.FindById(model.RegionID);
            int bidID = model.BidPlanID;
            

            ViewBag.SelectedTransporter = _transporterService.FindById(model.TransporterID);
            Bid SelectedBid = _bidService.FindById(bidID);
             ViewBag.SelectedBid =SelectedBid;
             int bidPlanID = SelectedBid.TransportBidPlanID;

             List<GoodsMovementDetailViewModel> quotationDestinations = GetPlannedDistribution(bidPlanID, model.RegionID);
             List<PriceQuotationDetailViewModel> qoutation = GetPriceQuotation(quotationDestinations,model.TransporterID, bidID);
             */
            return View(qoutation);

        }

        [HttpGet]
        public ActionResult BidProposal()
        {
            PriceQuotationFilterViewModel filter = new PriceQuotationFilterViewModel();
            ViewBag.Filter = filter;
            LoadLookups();
            return View(filter);
        }

        [HttpPost]
        public  ActionResult BidProposal(PriceQuotationFilterViewModel filter)
        {
            
            ViewBag.filter = filter;
            LoadLookups();
            return View(filter);
            //ViewBag.BidPlanID = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber",filter.BidPlanID);
            //ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name",filter.RegionID);
            //ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name",filter.TransporterID);
           
            //ViewBag.SelectedRegion = _adminUnitService.FindById(filter.RegionID);
            //ViewBag.SelectedTransporter = _transporterService.FindById(filter.RegionID);
            
            //ViewBag.SelectedRegion = _adminUnitService.FindById(filter.RegionID);
            //Bid SelectedBid = _bidService.FindById(filter.BidPlanID);
            
            //ViewBag.SelectedBid = SelectedBid;
            
            //var proposals  = _transportBidQuotationService.FindBy(m => m.BidID == filter.BidPlanID
            //                                          && m.TransporterID == filter.TransporterID
            //                                          && m.RegionID == filter.RegionID);
            
            //var s = (
            //            from proposal in proposals
            //            select new PriceQuotationDetail
            //                {
            //                    SourceWarehouse = proposal.Source.Name,
            //                    Zone = proposal.Destination.AdminUnit2.Name,
            //                    Woreda = proposal.Destination.AdminUnit2.AdminUnit2.Name,
            //                    Tariff = proposal.Tariff,
            //                    Remark = proposal.Remark
            //                }
            //        );

            // List<PriceQuotationDetail> f = new List<PriceQuotationDetail>();
            
            //f.Add(new PriceQuotationDetail()
            //    {
            //        SourceWarehouse = "Addis Ababa",
            //        Zone = "Anjuak",
            //        Woreda = "Abobo",
            //        Tariff = 25,
            //        Remark = "Well done"
            //    });

            //return View();
        }

        public ActionResult DeleteAjax(int TransportBidQuotationID)
        {
            _bidQuotationService.DeleteById(TransportBidQuotationID);
            return Json("{}");
        }

        public ActionResult ReadBidProposals([DataSourceRequest] DataSourceRequest request, int bidNumber, int regionID, int transporterID)
        {
            //var d = _transportBidQuotationService.GetAllTransportBidQuotation();
            var d = _transportBidQuotationService.FindBy(t=>t.BidID==bidNumber
                                                         && t.TransporterID==transporterID 
                                                         && t.Destination.AdminUnit2.AdminUnit2.AdminUnitID==regionID
                                                         );
           var s = (from transportBidQuotation in d
                     select new PriceQuotationDetail()
                     {
                         SourceWarehouse = transportBidQuotation.Source.Name,
                         Zone = transportBidQuotation.Destination.AdminUnit2.Name,
                         Woreda = transportBidQuotation.Destination.Name,
                         Tariff = transportBidQuotation.Tariff,
                         Remark = transportBidQuotation.Remark,
                         BidID = transportBidQuotation.BidID,
                         DestinationID = transportBidQuotation.DestinationID,
                         SourceID =  transportBidQuotation.SourceID,
                         TransportBidQuotationID = transportBidQuotation.TransportBidQuotationID,
                         TransporterID = transportBidQuotation.TransporterID
                     }
                    );
            return Json(s.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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