using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.Security;
using hub = Cats.Services.Hub;
using Cats.Models;
using Cats.Helpers;
namespace Cats.Areas.Logistics.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Logistics/Home/
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly hub.IDispatchAllocationService _dispatchAllocationService;
        private readonly IUserAccountService _userAccountService;
        private readonly ITransportOrderService _transportOrderService;
        private readonly ITransportOrderDetailService _transportOrderDetailService;
        private readonly hub.IDispatchService _dispatchService;
        private readonly hub.IDispatchDetailService _dispatchDetailService;
        private readonly Cats.Services.Logistics.ISIPCAllocationService _sipcAllocationService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IHRDService _hrdService;
        
        private readonly IBidWinnerService _bidWinnerService;
        private readonly IBidService _bidService;

        public HomeController(IReliefRequisitionService reliefRequisitionService,
            hub.IDispatchAllocationService dispatchAllocationService,
            IUserAccountService userAccountService,
            ITransportOrderService transportOrderService,
            ITransportOrderDetailService transportOrderDetailService,
            hub.DispatchService dispatchService,
            hub.DispatchDetailService dispatchDetailService, ISIPCAllocationService sipcAllocationService, IAdminUnitService adminUnitService, IHRDService hrdService, IBidWinnerService bidWinnerService, IBidService bidService)
        {
            this._reliefRequisitionService = reliefRequisitionService;
            _dispatchAllocationService = dispatchAllocationService;
            _userAccountService = userAccountService;
            _transportOrderService = transportOrderService;
            _transportOrderDetailService = transportOrderDetailService;
            _dispatchService = dispatchService;
            _dispatchDetailService = dispatchDetailService;
            _sipcAllocationService = sipcAllocationService;
            _adminUnitService = adminUnitService;
            _hrdService = hrdService;
            _bidWinnerService = bidWinnerService;
            _bidService = bidService;
        }

        public ActionResult Index()
        {
            ViewBag.RegionID = 2;
            ViewBag.RegionName = "Afar";
            return View();
        }

        public JsonResult GetRecievedRequisitions()
        {
            return Json(_reliefRequisitionService.GetRequisitionsSentToLogistics(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDispatchAllocationByHub(int hubId)
        {
            var user = _userAccountService.GetUserInfo(User.Identity.Name);
           
            var fdpAllocations = _dispatchAllocationService.GetCommitedAllocationsByHubDetached(hubId, user.PreferedWeightMeasurment);

            return Json(fdpAllocations, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllocationSummary()
        {
            return new JsonResult();
        }

        public JsonResult GetTransportContractExecution()
        {
            var contracts = _transportOrderService.FindBy(t => t.StatusID >= 3);
            var requisitions = from contract in contracts
                               select new
                               {
                                       contract.TransportOrderNo,
                                       details = from detail in
                                                     _transportOrderDetailService.FindBy(
                                                         d => d.TransportOrderID == contract.TransportOrderID)
                                                 select
                                                     new {detail.RequisitionID, detail.FDP.Name,
                                                          sum = from dispatch in _dispatchService.FindBy(r => r.RequisitionNo == detail.ReliefRequisition.RequisitionNo)
                                                            group dispatch by dispatch.RequisitionNo into d
                                                            select new { 
                                                                d.Key,
                                                                d                      
                                                            }
                                                     }
                               };
            //from requisition in requisitions 
            //select requisition.details.
            return Json(requisitions, JsonRequestBehavior.AllowGet);
        }

        public  JsonResult GetTransportContractInfo()
        {
            var contracts = _transportOrderService.FindBy(t => t.StatusID >= 3);
            var info = (
                        from contract in contracts 
                        select new
                            {
                                contract = contract.ContractNumber,
                                transporter = contract.Transporter.Name ,
                                owner = contract.Transporter.OwnerName,
                                //daysLeft = (int)(contract.EndDate - DateTime.Now).TotalDays,
                                daysLeft = DaysLeft(contract),
                                //daysToStart = (int)(contract.StartDate - DateTime.Now).TotalDays,
                                daysToStart = DaysToStart(contract),
                                //daysElapsed = (int)(DateTime.Now - contract.StartDate).TotalDays,
                                daysElapsed = DaysElapsed(contract),
                                //percentage = 50
                                duration = (int)(contract.EndDate - contract.StartDate).TotalDays,
                                percentage = ((contract.EndDate - DateTime.Now).TotalDays / (contract.EndDate - contract.StartDate).TotalDays) * 100
                            }
                       );
            return Json(info,JsonRequestBehavior.AllowGet);
        }

        public int DaysLeft( TransportOrder transportOrder)
        {
            var days = -1;

            if ((int)(transportOrder.StartDate - DateTime.Now).TotalDays>0)
            {
                days = (int) (transportOrder.EndDate - DateTime.Now).TotalDays;
            }
            return days;
        }

        public int DaysToStart(TransportOrder transportOrder)
        {
            var days = -1;

            if ((int)(transportOrder.StartDate - DateTime.Now).TotalDays < 0)
            {
                days = (int)(DateTime.Now - transportOrder.StartDate).TotalDays;
            }
            return days;
        }

        public int DaysElapsed(TransportOrder transportOrder)
        {
            var days = -1;

            if ((int)(transportOrder.StartDate - DateTime.Now).TotalDays > 0)
            {
                days = (int)(DateTime.Now - transportOrder.StartDate).TotalDays;
            }
            return days;
        }



        #region "Dashboard"

        public JsonResult GetRegions()
        {
            var regions = _adminUnitService.GetRegions().Select(r=> new
                                                                        {
                                                                            name = r.Name,
                                                                            id=r.AdminUnitID
                                                                        });
            return Json(regions, JsonRequestBehavior.AllowGet);
        }

        #region hrd

        public ActionResult CurrentHrdRead()
        {
             var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            DateTime latestDate = _hrdService.Get(m => m.Status == 3).Max(m => m.PublishedDate);
            var hrds = _hrdService.FindBy(m => m.Status == 3 && m.PublishedDate == latestDate).Select(h=> new 
                                                                                                              {
                                                                                                                  plan = h.Plan.PlanName,
                                                                                                                  ration =h.Ration.RefrenceNumber,
                                                                                                                  createdBy = h.CreatedBY,
                                                                                                                  createdDate = h.CreatedDate.ToCTSPreferedDateFormat(datePref),
                                                                                                                  status = h.Status,
                                                                                                                  PublishedDate = h.PublishedDate.ToCTSPreferedDateFormat(datePref)
                                                                                                                  
                                                                                                              });
            return Json(hrds,JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Bid

        public JsonResult GetBids()
        {
            var bids = _bidService.GetAllBid().Where(b=>b.StatusID == (int) Cats.Models.Constant.BidStatus.Active).Select(b=> new
                                                                                                                         {
                                                                                                                             BidNo = b.BidNumber,
                                                                                                                             BidId=b.BidID
                                                                                                                         });
            return Json(bids, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<BidWinnerViewModel> GetBidWinners(IEnumerable<BidWinner> bidWinners)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from bidWinner in bidWinners
                    select new BidWinnerViewModel()
                    {
                        BidWinnnerID = bidWinner.BidWinnerID,
                        TransporterID = bidWinner.TransporterID,
                        TransporterName = bidWinner.Transporter.Name,
                        SourceWarehouse = bidWinner.Hub.Name,
                        Woreda = bidWinner.AdminUnit.Name,
                        Region = bidWinner.AdminUnit.AdminUnit2.AdminUnit2.Name,
                        WinnerTariff = bidWinner.Tariff,
                        Quantity = bidWinner.Amount,
                        BidID = bidWinner.BidID,
                        BidPlanID = bidWinner.Bid.TransportBidPlanID,
                        BidMumber = bidWinner.Bid.BidNumber,
                        BidStartDate = bidWinner.Bid.StartDate.ToCTSPreferedDateFormat(datePref),
                        BidEndDate = bidWinner.Bid.EndDate.ToCTSPreferedDateFormat(datePref),
                        BidOpeningDate = bidWinner.Bid.OpeningDate.ToCTSPreferedDateFormat(datePref),
                        StatusID = bidWinner.Status,

                        //Status = _workflowStatusService.GetStatusName(WORKFLOW.BidWinner, int(bidWinner.Status.Value))

                    });
        }
        public JsonResult  GetListOfBidWinners(string id)
        {
            var selectedBidWinners = new SelectedBidWinnerViewModel();
            var bid = _bidService.FindBy(t => t.BidNumber == id).SingleOrDefault();
            var bidWinner = _bidWinnerService.FindBy(m => m.BidID == bid.BidID );
            if (bidWinner != null)
            {
                selectedBidWinners.Bidwinners = GetBidWinners(bidWinner).ToList();
            }
            return Json(selectedBidWinners,JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region request


        public JsonResult ImportantNumbers (string id)
        {
            var requests =
                _reliefRequisitionService.GetAllReliefRequisition().Where(d=>d.AdminUnit.Name == id).GroupBy(s => s.Status).Select(c => new
                                                                                                           {
                                                                                                               Status = c.Key,
                                                                                                               Count = c.Count(),

                                                                                                           }).Distinct();
            return Json(requests, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetRequiasitions(int id)
        {
            var requestes = _reliefRequisitionService.GetAllReliefRequisition().Where(s=>s.Status == id).Select(r => new
                                                                                                  {
                                                                                                      reqNo= r.RequisitionNo,
                                                                                                      zone= r.AdminUnit1.Name,
                                                                                                      beneficiaries = r.ReliefRequisitionDetails.Sum(d=>d.BenficiaryNo),
                                                                                                      amount  = r.ReliefRequisitionDetails.Sum(d=>d.Amount),
                                                                                                      commodity = r.Commodity.Name,
                                                                                                      regionId  = r.RegionalRequest.RegionID,
                                                                                                      RegionName = r.AdminUnit.Name
                                                                                                  });
            return Json(requestes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSiPcAllocation()
        {
            var siPcAllocated = _sipcAllocationService.GetAll().Select(s => new
                                                                                {
                                                                                    reqNo = s.ReliefRequisitionDetail.ReliefRequisition.RequisitionNo,
                                                                                    beneficiaries = s.ReliefRequisitionDetail.BenficiaryNo,
                                                                                    amount = s.AllocatedAmount,
                                                                                    commodity = s.ReliefRequisitionDetail.ReliefRequisition.Commodity.Name,
                                                                                    allocationType = s.AllocationType,
                                                                                    regionId = s.ReliefRequisitionDetail.ReliefRequisition.RegionID,
                                                                                    RegionName =s.ReliefRequisitionDetail.ReliefRequisition.AdminUnit.Name
                                                                                });
            return Json(siPcAllocated, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }
}