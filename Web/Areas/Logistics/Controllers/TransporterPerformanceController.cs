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
using Cats.Services.Procurement;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using IAdminUnitService = Cats.Services.EarlyWarning.IAdminUnitService;
using IHubService = Cats.Services.EarlyWarning.IHubService;

namespace Cats.Areas.Logistics.Controllers
{
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
        public TransporterPerformanceController(ITransportOrderService transportOrderService,IUserAccountService userAccountService,
                                              IDispatchAllocationService dispatchAllocationService, ITransportOrderDetailService transportOrderDetailService, IHubService hubService, IAdminUnitService adminUnitService)
        {
            _transportOrderService = transportOrderService;
            _userAccountService = userAccountService;
            _dispatchAllocationService = dispatchAllocationService;
            _transportOrderDetailService = transportOrderDetailService;
            _hubService = hubService;
            _adminUnitService = adminUnitService;
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
                        PickedUpSofar = GetDispatchAllocation(transportOrder.TransportOrderID),
                        ElapsedDays =(int)(DateTime.Now.Subtract(transportOrder.StartDate)).TotalDays,
                    });
                
            
           
        }
        public JsonResult GetContractNumbers()
        {


            var contractNumbers = (from contractNumber in _transportOrderService.GetAllTransportOrder()
                                   select contractNumber.ContractNumber).Distinct().ToList();
            // .Except(
            //from allocated in _receiptAllocationService.GetAllReceiptAllocation()
            //select allocated.SINumber).ToList();

            return Json(contractNumbers, JsonRequestBehavior.AllowGet);
        }

        private Decimal GetDispatchAllocation(int transportOrderID)
        {
            var dispatchAllocations = _dispatchAllocationService.FindBy(m => m.TransportOrderID == transportOrderID);
            var dispatchedSoFar = dispatchAllocations.Sum(m => m.Amount);
            return dispatchedSoFar;
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
                    //zone = r.AdminUnit.Name
                });
            return Json(transportOrderDetail, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetFilteredTransportOrderDetail(int regionId, int hubId, int transportOrderId, DateTime startDate, DateTime EndDate)
        {


            var transportOrderDetail =
                _transportOrderDetailService.FindBy(t => t.Hub.HubID == hubId && t.TransportOrderID == transportOrderId && t.FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionId
                && (t.TransportOrder.StartDate <= startDate && t.TransportOrder.EndDate >= EndDate)).Select(r => new
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
                    //zone = r.AdminUnit.Name
                });
            return Json(transportOrderDetail, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
