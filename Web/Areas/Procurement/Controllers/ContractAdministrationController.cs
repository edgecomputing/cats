using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.Hubs;
using Cats.Services.EarlyWarning;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ITransporterService = Cats.Services.Procurement.ITransporterService;

namespace Cats.Areas.Procurement.Controllers
{
    public class ContractAdministrationController : Controller
    {
        private readonly IPaymentRequestService _paymentRequestService;
        private readonly ITransporterService _transporterService;
        private readonly ITransportOrderService _transportOrderService;
        private readonly IUserAccountService _userAccountService;
        private readonly IDispatchAllocationService _dispatchAllocationService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IDeliveryService _distributionService;
        private readonly IBidWinnerService _bidWinnerService;
        private readonly Cats.Services.EarlyWarning.IAdminUnitService _adminUnitService;
        private readonly IReliefRequisitionService _reliefRequisition;

        public ContractAdministrationController(IPaymentRequestService paymentRequestService, ITransporterService transporterService,
            ITransportOrderService transportOrderService, IUserAccountService userAccountService, IDispatchAllocationService dispatchAllocationService, 
            IWorkflowStatusService workflowStatusService, IDeliveryService distributionService, IBidWinnerService bidWinnerService,
            Cats.Services.EarlyWarning.IAdminUnitService adminUnitService, IReliefRequisitionService reliefRequisition)
        {
            _adminUnitService = adminUnitService;
            _paymentRequestService = paymentRequestService;
            _transporterService = transporterService;
            _transportOrderService = transportOrderService;
            _userAccountService = userAccountService;
            _dispatchAllocationService = dispatchAllocationService;
            _workflowStatusService = workflowStatusService;
            _distributionService = distributionService;
            _bidWinnerService = bidWinnerService;
            _reliefRequisition = reliefRequisition;
        }
        //
        // GET: /Procurement/ContractAdministration/

        public ActionResult Index(int transporterID)
        {
            var transporterObj = _transporterService.FindById(transporterID);
            ViewBag.TransporterID = transporterID;
            ViewBag.PaymentRequests= (IEnumerable<Cats.Models.PaymentRequest>)_paymentRequestService
                                        .Get(t=>t.TransportOrder.TransporterID == transporterID, null, "TransportOrder").ToList();
            ViewBag.TransporterName = transporterObj.Name;
            ViewBag.TransporterAddress = "Region: " + transporterObj.Region
                                            + "  |  Sub-City: " + transporterObj.SubCity
                                            + "  |  Telephone: " + transporterObj.TelephoneNo;
            ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
            //ViewBag.TransporterOrderID = new SelectList(_transportOrderService.GetTransporter(), "TransporterID", "Name", 0);
            //ViewBag.Round = new SelectList(new int[] {1,2,3,4,5,6,7,8,9,10,11,12}, 0);
            //var target = new TransportOrderDispatchViewModel { DispatchViewModels = dispatchView.Where(t => !t.GRNReceived).ToList(), DispatchViewModelsWithGRN = dispatchView.Where(t => t.GRNReceived).ToList(), TransportOrderViewModel = transportOrderViewModel };
            return View();
        }

        public ActionResult BusinessProcessHistory(int id)
        {
            ViewBag.BusinessProcessID = id;
            return View();
        }

        public ActionResult ActiveTO_Read([DataSourceRequest] DataSourceRequest request, int transporterID, int regionId = -1, string transporterOrderNo = null)//, int round = -1, int transporterOrderId = -1)
        {
            List<TransportOrder> activeTOs;
            if (transporterOrderNo == null)
            {

                activeTOs =
                    _transportOrderService.Get(
                        t =>
                        t.StatusID >= 3 && t.TransporterID == transporterID &&
                        t.TransportOrderDetails.FirstOrDefault().FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID ==
                        regionId ).
                        ToList();
            }
            else
            {

                var dispatchedTransportOrders = _dispatchAllocationService.GetDispatchedTransportOrders();

                activeTOs =
                    _transportOrderService.Get(
                        t =>dispatchedTransportOrders.Contains(t.TransportOrderID) &&
                        t.StatusID >= 3 && t.TransporterID == transporterID &&
                        t.TransportOrderDetails.FirstOrDefault().FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID ==
                        regionId).Where(t => t.TransportOrderNo.ToUpperInvariant().Contains(transporterOrderNo.ToUpperInvariant())).
                        ToList();
            }
             var activeTOsViewModel = GetActiveTOsListViewModel(activeTOs);
            return Json(activeTOsViewModel.ToDataSourceResult(request));
        }

        private IEnumerable<ActiveTransportOrderViewModel> GetActiveTOsListViewModel(IEnumerable<TransportOrder> transportOrders)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from transportOrder in transportOrders
                    let transportOrderDetail = transportOrder.TransportOrderDetails.FirstOrDefault()
                    //where transportOrderDetail != null
                    let requisition = _reliefRequisition.FindById(transportOrderDetail.RequisitionID)
                    select new ActiveTransportOrderViewModel()
                    {
                        TransportOrderID = transportOrder.TransportOrderID,
                        TransportOderNo = transportOrder.TransportOrderNo,
                        Region = (transportOrderDetail == null)?"-" : transportOrderDetail.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name,
                        RequsitionNo = (requisition == null) ? "-" : requisition.RequisitionNo,
                        Round = (requisition == null )? "-" : requisition.Round.ToString(),
                        StartedOn = transportOrder.StartDate.ToCTSPreferedDateFormat(datePref),
                        SignedDate = transportOrder.TransporterSignedDate.ToCTSPreferedDateFormat(datePref),
                        RemainingDays = (transportOrder.EndDate - transportOrder.StartDate).TotalDays.ToString(),
                        Progress = ((((DateTime.Now - transportOrder.StartDate).TotalDays) / ((transportOrder.EndDate - transportOrder.StartDate).TotalDays)) * 100) > 100 ? 100.ToString() + "%"
                        : ((((DateTime.Now - transportOrder.StartDate).TotalDays) / ((transportOrder.EndDate - transportOrder.StartDate).TotalDays)) * 100).ToString("#0.00") + "%"

                    });
        }

        public ActionResult OutstandingDeliveryNotes_Read([DataSourceRequest] DataSourceRequest request, int transporterID)
        {
            var transportOrderObj = _transportOrderService.Get(t => t.StatusID == 3 && t.TransporterID == transporterID).FirstOrDefault();
            if (transportOrderObj == null)
            {
                return null;
            }
            else
            {
                var dispatch = _dispatchAllocationService.GetTransportOrderDispatches(transportOrderObj.TransportOrderID);

                foreach (var dispatchViewModel in dispatch)
                {
                    var dispatchId = dispatchViewModel.DispatchID;
                    var distribution = _distributionService.FindBy(t => t.DispatchID == dispatchId).FirstOrDefault();
                    dispatchViewModel.GRNReceived = distribution != null;
                    if (distribution != null)
                        dispatchViewModel.DeliveryID = distribution.DeliveryID;
                }
                var dispatchView = SetDatePreference(dispatch);
                return Json(dispatchView.ToDataSourceResult(request));
            }
        }

        private List<DispatchViewModel> SetDatePreference(List<DispatchViewModel> dispatches)
        {
            foreach (var dispatchViewModel in dispatches)
            {
                dispatchViewModel.CreatedDatePref =
                    dispatchViewModel.CreatedDate.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference());
                dispatchViewModel.DispatchDatePref =
                    dispatchViewModel.DispatchDate.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference());
            }
            return dispatches;
        }

        public ActionResult WoredasInCurrentContract_Read([DataSourceRequest] DataSourceRequest request, int transporterID)
        {
            var bidWinnerDestinations =
                _bidWinnerService.Get(t => t.Status == 3 && t.TransporterID == transporterID, null, "AdminUnit, AdminUnit.AdminUnit2, AdminUnit.AdminUnit2.AdminUnit2").ToList();
            var woredasInCurrentContract = GetWoredasInCurrentContract(bidWinnerDestinations);
            return Json(woredasInCurrentContract.ToDataSourceResult(request));
        }

        private IEnumerable<WoredasInCurrentContract> GetWoredasInCurrentContract(IEnumerable<BidWinner> bidWinnerDestinations)
        {
            return (from bidWinnerDestination in bidWinnerDestinations
                    select new WoredasInCurrentContract()
                    {
                        Region = _adminUnitService.FindById(bidWinnerDestination.DestinationID).Name,
                        Zone = bidWinnerDestination.AdminUnit.AdminUnit2.Name,
                        Woreda = bidWinnerDestination.AdminUnit.AdminUnit2.AdminUnit2.Name,
                        Tariff = bidWinnerDestination.Tariff.ToString()
                    });
        }

    }
}
