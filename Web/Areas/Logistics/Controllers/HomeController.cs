using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Models.Constant;
using Cats.Models.Hubs;
using Cats.Models.ViewModels.HRD;
using Cats.Services.EarlyWarning;
using Cats.Services.Hub.Interfaces;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using hub = Cats.Services.Hub;
using Cats.Models;
using Cats.Helpers;
using System.Data;
using TransporterViewModel = Cats.Models.ViewModels.TransporterViewModel;

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
        private readonly hub.DispatchDetailService _dispatchDetailService;
        private readonly ISIPCAllocationService _sipcAllocationService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IHRDService _hrdService;
        private readonly IHRDDetailService _hrdDetailService;
        private readonly IRationDetailService _rationDetailService;
        private readonly IProgramService _programService;
        private readonly IBidWinnerService _bidWinnerService;
        private readonly IBidService _bidService;
        private readonly IStockStatusService _stockStatusService;

        public HomeController(IReliefRequisitionService reliefRequisitionService,
            hub.IDispatchAllocationService dispatchAllocationService,
            IUserAccountService userAccountService,
            ITransportOrderService transportOrderService,
            ITransportOrderDetailService transportOrderDetailService,
            hub.DispatchService dispatchService,
            hub.DispatchDetailService dispatchDetailService,
            ISIPCAllocationService sipcAllocationService,
            IAdminUnitService adminUnitService,
            IHRDService hrdService,
            IBidWinnerService bidWinnerService,
            IBidService bidService,
            IHRDDetailService hrdDetailService,
            IRationDetailService rationDetailService,
            IProgramService programService,
            IStockStatusService stockStatusService)
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
            _hrdDetailService = hrdDetailService;
            _rationDetailService = rationDetailService;
            _programService = programService;
            _stockStatusService = stockStatusService;
        }

        public ActionResult Index()
        {
            var currentUser = UserAccountHelper.GetUser(HttpContext.User.Identity.Name);
            ViewBag.RegionName = currentUser.RegionID != null ? _adminUnitService.FindById(currentUser.RegionID ?? 0).Name : "";

            var hrd = _hrdService.FindBy(m => m.Status == 3).FirstOrDefault();
            if (hrd == null)
            {
                return HttpNotFound();
            }

            var summary = GetHRDSummary(hrd.HRDID);
            return View(summary);

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
                                                 new
                                                 {
                                                     detail.RequisitionID,
                                                     detail.FDP.Name,
                                                     sum = from dispatch in _dispatchService.FindBy(r => r.RequisitionNo == detail.ReliefRequisition.RequisitionNo)
                                                           group dispatch by dispatch.RequisitionNo into d
                                                           select new
                                                           {
                                                               d.Key,
                                                               d
                                                           }
                                                 }
                               };
            //from requisition in requisitions 
            //select requisition.details.
            return Json(requisitions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTransportContractInfo()
        {
            var contracts = _transportOrderService.FindBy(t => t.StatusID >= 3);
            var info = (
                        from contract in contracts
                        select new
                            {
                                contract = contract.ContractNumber,
                                transporter = contract.Transporter.Name,
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
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public int DaysLeft(TransportOrder transportOrder)
        {
            var days = -1;

            if ((int)(transportOrder.StartDate - DateTime.Now).TotalDays > 0)
            {
                days = (int)(transportOrder.EndDate - DateTime.Now).TotalDays;
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
            var regions = _adminUnitService.GetRegions().Select(r => new
                                                                        {
                                                                            name = r.Name,
                                                                            id = r.AdminUnitID
                                                                        });
            return Json(regions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRequestType()
        {
            var programTypes = _programService.GetAllProgram().Select(p => new
            {
                name = p.Name,
                id = p.ProgramID
            });
            return Json(programTypes, JsonRequestBehavior.AllowGet);
        }

        #region hrd

        private IEnumerable<HRDViewModel> GetHrds(IEnumerable<HRD> hrds)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from hrd in hrds
                    select new HRDViewModel
                    {
                        HRDID = hrd.HRDID,
                        Season = hrd.Season.Name,
                        Year = hrd.Year,
                        Ration = hrd.Ration.RefrenceNumber,
                        CreatedDate = hrd.CreatedDate,
                        CreatedBy = hrd.UserProfile.FirstName + " " + hrd.UserProfile.LastName,
                        PublishedDate = hrd.PublishedDate,
                        StatusID = hrd.Status,
                        CreatedDatePref = hrd.CreatedDate.ToCTSPreferedDateFormat(datePref),
                        PublishedDatePref = hrd.PublishedDate.ToCTSPreferedDateFormat(datePref),
                        Plan = hrd.Plan.PlanName

                    });
        }


        public ActionResult CurrentHrdRead()
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            DateTime latestDate = _hrdService.Get(m => m.Status == 3).Max(m => m.PublishedDate);
            var hrds = _hrdService.FindBy(m => m.Status == 3 && m.PublishedDate == latestDate);
            var hrdsToDisplay = GetHrds(hrds).ToList();
            return Json(hrdsToDisplay, JsonRequestBehavior.AllowGet);
        }

        private DataTable GetHRDSummary(int id)
        {
            var weightPref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).PreferedWeightMeasurment;
            var hrd = _hrdService.FindById(id);
            var hrdDetails =
                _hrdDetailService.Get(t => t.HRDID == id, null,
                                      "AdminUnit,AdminUnit.AdminUnit2,AdminUnit.AdminUnit2.AdminUnit2").ToList();
            var rationDetails = _rationDetailService.Get(t => t.RationID == hrd.RationID, null, "Commodity");
            var dt = HRDViewModelBinder.TransposeDataSummary(hrdDetails, rationDetails, weightPref);
            return dt;
        }

        #endregion

        #region Bid

        public JsonResult GetBids()
        {
            var bids = _bidService.GetAllBid().Where(b => b.StatusID == (int)Cats.Models.Constant.BidStatus.Active).Select(b => new
                                                                                                                         {
                                                                                                                             BidNo = b.BidNumber,
                                                                                                                             BidId = b.BidID
                                                                                                                         });
            return Json(bids, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Cats.Models.ViewModels.TransporterViewModel> GetBidWinners(IEnumerable<BidWinner> bidWinners)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from bidWinner in bidWinners
                    select new Cats.Models.ViewModels.TransporterViewModel()
                    {

                        TransporterID = bidWinner.TransporterID,
                        Name = bidWinner.Transporter.Name,
                        RegioName = _adminUnitService.FindById(bidWinner.Transporter.Region).Name,
                        SubCity = bidWinner.Transporter.SubCity,
                        ZoneName = _adminUnitService.FindById(bidWinner.Transporter.Zone).Name,
                        TelephoneNo = bidWinner.Transporter.TelephoneNo,
                        Capital = bidWinner.Transporter.Capital,
                        MobileNo = bidWinner.Transporter.MobileNo,
                        Desitnation = _adminUnitService.FindById(bidWinner.DestinationID).Name,
                        Source = _adminUnitService.FindById(bidWinner.SourceID).Name,
                        Fdp = bidWinner.AdminUnit.Name
                    }).Distinct();
        }
        public JsonResult GetListOfBidWinners(string id)
        {
            var selectedBidWinners = new List<TransporterViewModel>();
            var bid = _bidService.FindBy(t => t.BidNumber == id).SingleOrDefault();
            var bidWinner = _bidWinnerService.FindBy(m => m.BidID == bid.BidID);
            if (bidWinner != null)
            {
                selectedBidWinners = GetBidWinners(bidWinner).ToList();
            }
            return Json(selectedBidWinners, JsonRequestBehavior.AllowGet);
        }
       

        #endregion

        #region request


        public JsonResult ImportantNumbers(string id, string requestName)
        {
            if (string.IsNullOrEmpty(requestName))
            {
                var requests =
                _reliefRequisitionService.GetAllReliefRequisition()
                .Where(d => d.AdminUnit.Name == id)
                .GroupBy(s => s.Status)
                .Select(c => new
                {
                    Status = c.Key,
                    Count = c.Count(),
                });
                return Json(requests, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var request =
              _reliefRequisitionService.GetAllReliefRequisition()
              .Where(d => d.AdminUnit.Name == id && d.Program.Name == requestName)
              .GroupBy(s => s.Status)
              .Select(c => new
              {
                  Status = c.Key,
                  Count = c.Count(),
              });
                return Json(request, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetRequisitions(int id)
        {
            var requestes = _reliefRequisitionService.GetAllReliefRequisition()
                .Where(s => s.Status == id)
                .Select(r => new
                {
                    reqNo = r.RequisitionNo,
                    zone = r.AdminUnit1.Name,
                    beneficiaries = r.ReliefRequisitionDetails.Sum(d => d.BenficiaryNo),
                    amount = r.ReliefRequisitionDetails.Sum(d => d.Amount),
                    commodity = r.Commodity.Name,
                    regionId = r.RegionalRequest.RegionID,
                    RegionName = r.AdminUnit.Name,
                    RequestType = r.Program.Name,
                    round = r.RegionalRequest.Round,
                    month = r.RegionalRequest.Month,
                    year = r.RegionalRequest.Year,
                });
            return Json(requestes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSiPcAllocation()
        {
            var siPcAllocated = _sipcAllocationService.FindBy(r => r.ReliefRequisitionDetail.ReliefRequisition.Status == (int)Cats.Models.Constant.ReliefRequisitionStatus.ProjectCodeAssigned).Select(s => new
                                                                                {
                                                                                    reqNo = s.ReliefRequisitionDetail.ReliefRequisition.RequisitionNo,
                                                                                    beneficiaries = s.ReliefRequisitionDetail.BenficiaryNo,
                                                                                    amount = s.AllocatedAmount,
                                                                                    commodity = s.ReliefRequisitionDetail.ReliefRequisition.Commodity.Name,
                                                                                    allocationType = s.AllocationType,
                                                                                    regionId = s.ReliefRequisitionDetail.ReliefRequisition.RegionID,
                                                                                    RegionName = s.ReliefRequisitionDetail.ReliefRequisition.AdminUnit.Name,
                                                                                    program = s.ReliefRequisitionDetail.ReliefRequisition.Program.Name
                                                                                });
            return Json(siPcAllocated, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region transporters

        public JsonResult GetTransporters()
        {
            var transporters =
                _transportOrderService.FindBy(s => s.StatusID < (int)(Cats.Models.Constant.TransportOrderStatus.Closed)).Select(p => new
                {
                    name = p.Transporter.Name,
                    region = _adminUnitService.FindById(p.Transporter.Region).Name,
                    zone = _adminUnitService.FindById(p.Transporter.Zone).Name,
                    transportOrderNo = p.TransportOrderNo,
                    mobileNo = p.Transporter.MobileNo
                });
            return Json(transporters, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dispatch Allocations

        //public JsonResult GetRecentDispatchAllocations(int regionId )
        //{
        //    var dispatchAllocation =
        //        _dispatchAllocationService.FindBy(m=>m.DispatchedAmount > 0 && m.FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID==regionId).OrderByDescending(m => m.DispatchAllocationID).
        //            Select(p => new
        //                {
        //                    hub=p.Hub.Name,
        //                    bidNumber=p.BidRefNo,
        //                    fdpName=p.FDP.Name,
        //                    dispatched=p.DispatchedAmount,
        //                    remaining=p.RemainingQuantityInQuintals
        //                }).Take(10);
        //    return Json(dispatchAllocation, JsonRequestBehavior.AllowGet);
        //}
        #endregion
        public JsonResult GetDispatchAllocation(int program, DateTime date)
        {
            var st = _stockStatusService.GetHubDispatchAllocation(program, date);

            var q = (from s in st
                     select new HubDispatchAllocationViewModel()
                     {
                         HubName = s.HubName,
                         TotalFreestock = s.TotalFreestock.ToPreferedWeightUnit(),
                         TotalPhysicalStock = s.TotalPhysicalStock.ToPreferedWeightUnit(),
                         DispatchedAmount = s.DispatchedAmount.ToPreferedWeightUnit(),
                         Remaining = s.Remaining.ToPreferedWeightUnit()
                         
                     });
            return Json(q, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}