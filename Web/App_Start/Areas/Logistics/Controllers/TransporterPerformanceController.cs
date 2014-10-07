using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Dispatch = Cats.Models.Hubs.Dispatch;
using IAdminUnitService = Cats.Services.EarlyWarning.IAdminUnitService;
using IHubService = Cats.Services.EarlyWarning.IHubService;

namespace Cats.Areas.Logistics.Controllers
{
    [Authorize]
    public class TransporterPerformanceController : Controller
    {
        //
        // GET: /Logistics/TransporterPerformance/
        private readonly ITransportOrderService _transportOrderService;
        private readonly IUserAccountService _userAccountService;
        private readonly IDispatchAllocationService _dispatchAllocationService;
        private readonly ITransportOrderDetailService _transportOrderDetailService;
        private readonly IHubService _hubService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IDispatchService _dispatchService;
        private readonly IDeliveryService _deliveryService;
        public TransporterPerformanceController(ITransportOrderService transportOrderService,IUserAccountService userAccountService,
                                              IDispatchAllocationService dispatchAllocationService, ITransportOrderDetailService transportOrderDetailService,
                                              IHubService hubService, IAdminUnitService adminUnitService,IDispatchService dispatchService,IDeliveryService deliveryService)
        {
            _transportOrderService = transportOrderService;
            _userAccountService = userAccountService;
            _dispatchAllocationService = dispatchAllocationService;
            _transportOrderDetailService = transportOrderDetailService;
            _hubService = hubService;
            _adminUnitService = adminUnitService;
            _dispatchService = dispatchService;
            _deliveryService = deliveryService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TransporterPerformance_Read([DataSourceRequest] DataSourceRequest request, string searchIndex)
        {
            var transportOrder = _transportOrderService.Get(m => m.ContractNumber.Contains(searchIndex))
                               .Where(m=>m.StatusID==(int)TransportOrderStatus.Closed && m.TransportOrderDetails.Sum(s=>s.QuantityQtl)>0)
                               .OrderByDescending(m=>m.TransportOrderID);
            var transportOrderToDisplay = GetTransportOrder(transportOrder);
            return Json(transportOrderToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<TransporterPerformanceViewModel> GetTransportOrder(IEnumerable<TransportOrder> transportOrders)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from transportOrder in transportOrders
                    select new TransporterPerformanceViewModel
                    {
                        TransporterID = transportOrder.TransporterID,
                        TransporterName = transportOrder.Transporter.Name,
                        TransporterOrderID = transportOrder.TransportOrderID,
                        ContractNumber = transportOrder.ContractNumber,
                        TransportOrderNumber = transportOrder.TransportOrderNo,
                        StartDate = transportOrder.StartDate.ToCTSPreferedDateFormat(datePref),
                        TotalQuantity = transportOrder.TransportOrderDetails.Sum(m => m.QuantityQtl),
                        NoOfDaysToComplete =(int) (transportOrder.EndDate.Subtract(transportOrder.StartDate)).TotalDays,
                        PickedUpSofar = GetDispatchAllocation(transportOrder.TransportOrderID)*10, 
                        Delivered = GetDelivered(transportOrder.TransportOrderID),
                        ElapsedDays =(int)(DateTime.Now.Subtract(transportOrder.StartDate)).TotalDays,
                    });
                
            
           
        }
        public JsonResult GetContractNumbers()
        {


            var contractNumbers = (from contractNumber in _transportOrderService.GetAllTransportOrder()
                                   where contractNumber.StatusID == (int) TransportOrderStatus.Closed 
                                   select contractNumber.ContractNumber).Distinct().ToList();
          

            return Json(contractNumbers, JsonRequestBehavior.AllowGet);
        }

        private Decimal GetDispatchAllocation(int transportOrderID)
        {
            var dispatches = _dispatchService.Get(t => t.DispatchAllocation.TransportOrderID == transportOrderID).ToList();
            var totaldispatched= dispatches.Sum(dispatch => dispatch.DispatchDetails.Sum(m => m.DispatchedQuantityInMT));

            return totaldispatched;
        }

        private decimal GetDelivered(int transportOrderID)
        {
            var dispatchIds = _dispatchService.Get(t => t.DispatchAllocation.TransportOrderID == transportOrderID).Select(t => t.DispatchID).ToList();
            var deliveries = _deliveryService.Get(t => dispatchIds.Contains(t.DispatchID.Value), null, "DeliveryDetails");
            return deliveries.Sum(delivery => delivery.DeliveryDetails.Sum(m => m.ReceivedQuantity));
        }
     

        #region transporter performance detail


        public ActionResult TransportOrderPerformanceDetail(int id = -1)
        {
            ViewBag.TransportOrderId = id;
            return View();
        }

        public JsonResult GetRegions()
        {
            var regions = _adminUnitService.GetRegions().Select(r => new
            {
                Name = r.Name,
                AdminUnitID = r.AdminUnitID
            });
            return Json(regions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHubs()
        {
            var hubs = _hubService.FindBy(h=>h.HubOwnerID == 1).Select(h => new
            {
                Name = h.Name,
                HubID = h.HubID
            });
            return Json(hubs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTransporter(int transportOrderId)
        {
            var transporter = _transportOrderService.FindBy(t => t.TransportOrderID == transportOrderId).Select(r => new
            {
                Name = r.Transporter.Name,
                subCity = r.Transporter.SubCity,
                kebele = r.Transporter.Kebele,
                houseNo = r.Transporter.HouseNo,
                TelephoneNo = r.Transporter.TelephoneNo,
                MobileNo = r.Transporter.MobileNo,
                Email = r.Transporter.Email,
                ContratNo = r.ContractNumber,
                bidNo = r.BidDocumentNo,
                TransporOrderStartDate = r.StartDate,
                TransportOrderEndDate = r.EndDate,
                TransportOrderNo = r.TransportOrderNo
            }).FirstOrDefault();

            return Json(transporter, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetDispatches(int transportOrderId)
        {
            var transportOrderDetail =
                _transportOrderDetailService.FindBy(t => t.TransportOrderID == transportOrderId).Select(r => new
                {
                    fdp = r.FDP.Name,
                    zone = r.FDP.AdminUnit.AdminUnit2.Name,
                    woreda = r.FDP.AdminUnit.Name,
                    region = r.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name,
                    hub = _hubService.FindById(r.SourceWarehouseID).Name,
                    amount = r.QuantityQtl,
                    requisitionNo = r.ReliefRequisition.RequisitionNo,
                    tariff = r.TariffPerQtl,
                    commodity = r.Commodity.Name,
                    dispatchedAmount=_dispatchService.GetFDPDispatch(transportOrderId,r.FdpID),
                    DeliveredAmount = _deliveryService.GetFDPDelivery(transportOrderId, r.FdpID)
                    //zone = r.AdminUnit.Name
                });
            return Json(transportOrderDetail, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetFilteredTransportOrderDetail(int regionId, int hubId, int transportOrderId, DateTime startDate, DateTime EndDate)
        {


            var transportOrderDetail =
                _transportOrderDetailService.FindBy(t => t.Hub.HubID == hubId && t.TransportOrderID == transportOrderId && t.FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionId
                && (t.TransportOrder.StartDate >= startDate && t.TransportOrder.EndDate <= EndDate)).Select(r => new
                {
                    fdp = r.FDP.Name,
                    zone = r.FDP.AdminUnit.AdminUnit2.Name,
                    woreda = r.FDP.AdminUnit.Name,
                    region = r.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name,
                    hub = _hubService.FindById(r.SourceWarehouseID).Name,
                    amount = r.QuantityQtl,
                    requisitionNo = r.ReliefRequisition.RequisitionNo,
                    tariff = r.TariffPerQtl,
                    commodity = r.Commodity.Name,
                    dispatchedAmount = _dispatchService.GetFDPDispatch(transportOrderId, r.FdpID),
                    DeliveredAmount = _deliveryService.GetFDPDelivery(transportOrderId, r.FdpID)
                });
            return Json(transportOrderDetail, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
