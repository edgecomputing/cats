using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;

namespace Cats.Areas.Finance.Controllers
{
    public class TransportOrderPerformanceController : Controller
    {
        private readonly ITransportOrderService _transportOrderService;
        private readonly ITransportOrderDetailService _transportOrderDetailService;
        private readonly IHubService _hubService;
        private readonly IAdminUnitService _adminUnitService;
        public TransportOrderPerformanceController(ITransportOrderService transportOrderService, ITransporterService transporterService, ITransportOrderDetailService transportOrderDetailService, IHubService hubService, IAdminUnitService adminUnitService)
        {
            _transportOrderService = transportOrderService;
            _transportOrderDetailService = transportOrderDetailService;
            _hubService = hubService;
            _adminUnitService = adminUnitService;
        }

        //
        // GET: /Finance/TransportOrderPerformance/

        public ActionResult Index(int id = 3072)
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
            var hubs = _hubService.GetAllHub().Select(h => new 
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
                                                                                                               Name =r.Transporter.Name,
                                                                                                               subCity =r.Transporter.SubCity,
                                                                                                               kebele =r.Transporter.Kebele,
                                                                                                               houseNo =r.Transporter.HouseNo,
                                                                                                               TelephoneNo=r.Transporter.TelephoneNo,
                                                                                                               MobileNo=r.Transporter.MobileNo,
                                                                                                               Email =r.Transporter.Email,
                                                                                                               ContratNo=r.ContractNumber,
                                                                                                               bidNo =r.BidDocumentNo,
                                                                                                               TransporOrderStartDate= r.StartDate,
                                                                                                               TransportOrderEndDate= r.EndDate,
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
                                                                                                                     commodity =r.Commodity.Name,
                                                                                                                    //zone = r.AdminUnit.Name
                                                                                                                 });
            return Json(transportOrderDetail,JsonRequestBehavior.AllowGet);
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
    }
}
