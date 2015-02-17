using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Infrastructure;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Models.ViewModels.HRD;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Areas.Logistics.Models;
using log4net;
using Cats.ViewModelBinder;
using Cats.Helpers;

namespace Cats.Areas.Procurement.Controllers
{
    public class DuplicatedController : Controller
    {
        private readonly ITransportOrderService _transportOrderService;
        private readonly ITransportOrderDetailService _transportOrderDetailService;
        private readonly ITransportRequisitionService _transportRequisitionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly ILog _log;
        private readonly IAdminUnitService _adminUnitService;
        private readonly ITransReqWithoutTransporterService _transReqWithoutTransporterService;
        private readonly ITransporterService _transporterService;
        private readonly ITransportBidQuotationService _bidQuotationService;

        public DuplicatedController(ITransportOrderService transportOrderService,
            ITransportRequisitionService transportRequisitionService,
            IWorkflowStatusService workflowStatusService, ILog log,
            ITransReqWithoutTransporterService transReqWithoutTransporterService, ITransportOrderDetailService transportOrderDetailService,
            IAdminUnitService adminUnitService, ITransporterService transporterService, ITransportBidQuotationService bidQuotationService)
        {
            this._transportOrderService = transportOrderService;
            this._transportRequisitionService = transportRequisitionService;
            this._workflowStatusService = workflowStatusService;
            _log = log;
            _adminUnitService = adminUnitService;
            _transporterService = transporterService;
            _transReqWithoutTransporterService = transReqWithoutTransporterService;
            _transportOrderDetailService = transportOrderDetailService;
            _bidQuotationService = bidQuotationService;
        }
        //
        // GET: /Procurement/Duplicated/

        public ActionResult Index(int id = 0, int woredaId = 0, int transporterId = 0, int zoneId = 0, int transReqId = 0)
        {
            ViewBag.Month = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            ViewBag.TransportOrdrStatus = id;
            ViewBag.TransportOrderTitle = id == 0
                                              ? "Draft"
                                              : _workflowStatusService.GetStatusName(WORKFLOW.TRANSPORT_ORDER, id);
            var allTransporters = _transportOrderService.GetTransporter();

            ViewBag.TransporterID = transporterId == 0 ? new SelectList(allTransporters, "TransporterID", "Name", 0) : new SelectList(allTransporters, "TransporterID", "Name", transporterId);
            ViewBag.Zones = zoneId == 0 ? new SelectList(_transportOrderService.GetZone(transReqId), "ZoneId", "ZoneName") : new SelectList(_transportOrderService.GetZone(transReqId), "ZoneId", "ZoneName", zoneId);

            ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
            var viewModel = GetRequisitionsWithoutTransporter(woredaId, transReqId);

            ViewBag.TransReq = transReqId == 0 ? new SelectList(_transReqWithoutTransporterService.Get(t => t.IsAssigned == false).Select(u => u.TransportRequisitionDetail != null ? new
            {
                TransReqID = u.TransportRequisitionDetail.TransportRequisition.TransportRequisitionID,
                TransReqNo = u.TransportRequisitionDetail.TransportRequisition.TransportRequisitionNo
            } : null).Distinct(), "TransReqID", "TransReqNo")
                               : new SelectList(_transReqWithoutTransporterService.Get(t => t.IsAssigned == false).Select(u => u.TransportRequisitionDetail != null ? new
                               {
                                   TransReqID = u.TransportRequisitionDetail.TransportRequisition.TransportRequisitionID,
                                   TransReqNo = u.TransportRequisitionDetail.TransportRequisition.TransportRequisitionNo
                               } : null).Distinct(), "TransReqID", "TransReqNo", transReqId);


         
           
            return View(viewModel);
        }


        public TransportRequisitionWithTransporter GetRequisitionsWithoutTransporter(int woredaId, int transReqId)
        {
            var req = new TransportRequisitionWithTransporter();




            var transReqDetail = _transportRequisitionService.FindBy(f => f.TransportRequisitionID == transReqId).SelectMany(d => d.TransportRequisitionDetails);

            var reliefRequisitionDetail = transReqDetail.SelectMany(detail => detail.ReliefRequisition.ReliefRequisitionDetails).ToList();
            var filteredTrans = reliefRequisitionDetail.Where(d => d.FDP.AdminUnit.AdminUnitID == woredaId).Select(s => s.RequisitionDetailID).ToList();


            var x = _transReqWithoutTransporterService.FindBy(m => filteredTrans.Contains(m.RequisitionDetailID) && m.IsAssigned == false);// &&
            //  m.ReliefRequisitionDetail != null && (m.IsAssigned == false)).OrderByDescending(t => t.TransportRequisitionDetailID));
            //}
            //var transReqWithoutTransport = _transReqWithoutTransporterService.Get(m => m.ReliefRequisitionDetail != null && (m.IsAssigned == false && m.ReliefRequisitionDetail.FDP.AdminUnit.AdminUnitID == woredaId && m.TransportRequisitionDetail.TransportRequisition.TransportRequisitionID == transReqId)).Distinct().OrderByDescending(t => t.TransportRequisitionDetailID);
            req.TransReqwithOutTransporters = GetTransReqWithoutTransporter(x).ToList();
            return req;
        }

        private IEnumerable<TransportRequisitionWithoutWinnerModel> GetTransReqWithoutTransporter(IEnumerable<TransReqWithoutTransporter> transReqWithoutTransporter)
        {

            return (from detail in transReqWithoutTransporter
                    from requisitionDetail in detail.TransportRequisitionDetail.ReliefRequisition.ReliefRequisitionDetails.
                                    Where(m => m.RequisitionDetailID == detail.RequisitionDetailID)
                    select new TransportRequisitionWithoutWinnerModel()
                    {
                        TransportRequisitionID = detail.TransportRequisitionDetailID,
                        TransReqWithoutTransporterID = detail.TransReqWithoutTransporterID,
                        RequisitionDetailID = detail.RequisitionDetailID,
                        Woreda = requisitionDetail.FDP.AdminUnit.Name,
                        FDP = requisitionDetail.FDP.Name,
                        QuantityQtl = requisitionDetail.Amount.ToPreferedWeightUnit(),
                        Commodity = requisitionDetail.Commodity.Name,
                        CommodityID = requisitionDetail.CommodityID,
                        FdpID = requisitionDetail.FDPID,
                        HubID = requisitionDetail.ReliefRequisition.HubAllocations.First().HubID,
                        OriginWarehouse = requisitionDetail.ReliefRequisition.HubAllocations.First().Hub.Name,
                        RequisitionID = detail.ReliefRequisitionDetail.RequisitionID,
                        beneficiaryNumber = detail.TransportRequisitionDetail.ReliefRequisition.ReliefRequisitionDetails.First().BenficiaryNo,
                        RequisitionNo = detail.TransportRequisitionDetail.ReliefRequisition.RequisitionNo
                    });




        }

        public ActionResult LoadUnAssinedByReqNo(int id, int woredaId, int zone, int transporter, int TransReqID)
        {
            return RedirectToAction("Index", new { id = id, woredaId = woredaId, transporterId = transporter, zoneId = zone, transReqId = TransReqID });
        }

        public ActionResult Delete(TransportRequisitionWithTransporter requisitionWithTransporter)
        {

            var selectedTransRequision = requisitionWithTransporter.TransReqwithOutTransporters.Where(m => m.Selected == true);
            try
            {
                foreach (var transportRequisitionWithoutWinnerModel in selectedTransRequision)
                {
                    _transReqWithoutTransporterService.DeleteById(
                        transportRequisitionWithoutWinnerModel.TransReqWithoutTransporterID);
                    _transReqWithoutTransporterService.Save();

                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(ex, _log);
                ModelState.AddModelError("Errors", @"Unable to delete the requisition");
            }

            return RedirectToAction("Index");
        }

    }
}
