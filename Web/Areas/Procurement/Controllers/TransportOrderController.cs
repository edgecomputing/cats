using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Infrastructure;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Areas.Logistics.Models;
using log4net;

namespace Cats.Areas.Procurement.Controllers
{
    public class TransportOrderController : Controller
    {
        //
        // GET: /Procurement/TransportOrder/
        private readonly ITransportOrderService _transportOrderService;

        private readonly ITransportRequisitionService _transportRequisitionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly ILog _log;


        public TransportOrderController(ITransportOrderService transportOrderService, 
            ITransportRequisitionService transportRequisitionService, 
            IWorkflowStatusService workflowStatusService,ILog log)
        {
            this._transportOrderService = transportOrderService;
            this._transportRequisitionService = transportRequisitionService;
            this._workflowStatusService = workflowStatusService;

        }




        [HttpGet]
        public ViewResult TransportRequisitions()
        {
            var transportRequisitions = _transportRequisitionService.Get(t => t.Status == (int)TransportRequisitionStatus.Approved);
            var transportReqInput = (from item in transportRequisitions
                                     select new TransportRequisitionSelect
                                                {
                                                    CertifiedBy = item.CertifiedBy,
                                                    CertifiedDate = item.CertifiedDate,
                                                    RequestedBy = item.RequestedBy,
                                                    RequestedDate = item.RequestedDate,
                                                    StatusName = _workflowStatusService.GetStatusName(WORKFLOW.TRANSPORT_REQUISITION, item.Status),
                                                    Status = item.Status,
                                                    RequestDateET = EthiopianDate.GregorianToEthiopian(item.RequestedDate),
                                                    CertifiedDateET = EthiopianDate.GregorianToEthiopian(item.CertifiedDate),
                                                    TransportRequisitionID = item.TransportRequisitionID,
                                                    TransportRequisitionNo = item.TransportRequisitionNo,
                                                    Input = new TransportRequisitionSelect.TransportRequisitionSelectInput
                                                              {
                                                                  Number = item.TransportRequisitionID,
                                                                  IsSelected = false
                                                              }


                                                });


            return View(transportReqInput.ToList());
        }
        public FileResult Print(int id)
        {
            var reportPath = Server.MapPath("~/Report/Procurment/TransportOrder.rdlc");
            var reportData = _transportOrderService.GeTransportOrderRpt(id);
            var dataSourceName = "TransportOrders";
            var result = ReportHelper.PrintReport(reportPath, reportData, dataSourceName);

            return File(result.RenderBytes, result.MimeType);
        }

        [HttpPost]
        public ActionResult TransportRequisitions(IList<SelectFromGrid> input)
        {
            try
            {
                var requisionIds = (from item in input where (item.IsSelected != null ? ((string[])item.IsSelected)[0] : "off") == "on" select item.Number).ToList();
                return CreateTransportOrder(requisionIds);
            }
            catch (Exception exception)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(exception,_log);
                return View("TransportRequisitions", "TransportOrder");
            }


        }

        public ActionResult CreateTransportOrder(IEnumerable<int> requisitionToDispatches)
        {

            _transportOrderService.CreateTransportOrder(requisitionToDispatches);


            return RedirectToAction("Index", "TransportOrder");
        }

        public ViewResult Index()
        {


            return View();
        }

        public ActionResult TransportOrder_Read([DataSourceRequest] DataSourceRequest request)
        {
            var transportOrders = _transportOrderService.GetAllTransportOrder();
            var transportOrderViewModels =
                (from itm in transportOrders select BindTransportOrderViewModel(itm));
            return Json(transportOrderViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private TransportOrderViewModel BindTransportOrderViewModel(TransportOrder transportOrder)
        {
            TransportOrderViewModel transportOrderViewModel = null;
            if (transportOrder != null)
            {
                transportOrderViewModel = new TransportOrderViewModel();
                transportOrderViewModel.BidDocumentNo = transportOrder.BidDocumentNo;
                transportOrderViewModel.OrderDate = transportOrder.OrderDate;
                transportOrderViewModel.OrderDateET =
                    EthiopianDate.GregorianToEthiopian(transportOrder.OrderDate);
                transportOrderViewModel.ContractNumber = transportOrder.ContractNumber;
                transportOrderViewModel.PerformanceBondReceiptNo = transportOrder.PerformanceBondReceiptNo;
                transportOrderViewModel.OrderExpiryDate = transportOrder.OrderExpiryDate;
                transportOrderViewModel.OrderExpiryDateET = EthiopianDate.GregorianToEthiopian(transportOrder.OrderExpiryDate);
                transportOrderViewModel.Transporter = transportOrder.Transporter.Name ;
                transportOrderViewModel.RequestedDispatchDate = transportOrder.RequestedDispatchDate;
                transportOrderViewModel.RequestedDispatchDateET =  EthiopianDate.GregorianToEthiopian(transportOrder.RequestedDispatchDate);
                transportOrderViewModel.TransporterID = transportOrder.TransporterID;
                transportOrderViewModel.TransportOrderNo = transportOrder.TransportOrderNo;
                transportOrderViewModel.TransportOrderID = transportOrder.TransportOrderID;
              
            }
            return transportOrderViewModel;
            
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var transportOrder = _transportOrderService.FindById(id);
            if (transportOrder == null)
            {
                return HttpNotFound();
            }
            return View(transportOrder);

        }
        [HttpPost]
        public ActionResult Edit(TransportOrder transportOrder)
        {
            if (ModelState.IsValid)
            {
                var target = _transportOrderService.FindById(transportOrder.TransportOrderID);
                target.TransportOrderNo = transportOrder.TransportOrderNo;
                target.ContractNumber = transportOrder.ContractNumber;
                target.OrderDate = transportOrder.OrderDate;
                target.OrderExpiryDate = transportOrder.OrderExpiryDate;
                target.RequestedDispatchDate = transportOrder.RequestedDispatchDate;
                target.PerformanceBondReceiptNo = transportOrder.PerformanceBondReceiptNo;
                target.BidDocumentNo = transportOrder.BidDocumentNo;
                target.TransporterSignedDate = transportOrder.TransporterSignedDate;
                target.TransporterSignedName = transportOrder.TransporterSignedName;
                target.ConsignerDate = transportOrder.ConsignerDate;
                target.ConsignerName = transportOrder.ConsignerName;
                _transportOrderService.EditTransportOrder(target);
                return RedirectToAction("Index", "TransportOrder");
            }
            return View(transportOrder);
        }
        public ActionResult Details(int id)
        {
            var transportOrder = _transportOrderService.Get(t => t.TransportOrderID == id, null, "TransportOrderDetails.FDP,TransportOrderDetails.FDP.AdminUnit,TransportOrderDetails.Commodity,TransportOrderDetails.Hub,TransportOrderDetails.ReliefRequisition").FirstOrDefault();
            var transportOrderViewModel = BindTransportOrderViewModel(transportOrder);
            ViewData["Transport.order.detail.ViewModel"] = transportOrder ==null ? null :
                GetDetail(transportOrder.TransportOrderDetails);
            return View(transportOrderViewModel);
        }
        private IEnumerable<TransportOrderDetailViewModel> GetDetail(IEnumerable<TransportOrderDetail> transportOrderDetails)
        {
            
            var transportOrderDetailViewModels =
                (from itm in transportOrderDetails select BindTransportOrderDetailViewModel(itm));
            return transportOrderDetailViewModels;
        } 
        private TransportOrderDetailViewModel BindTransportOrderDetailViewModel(TransportOrderDetail transportOrderDetail)
        {
            TransportOrderDetailViewModel transportOrderDetailViewModel = null;
            if(transportOrderDetail !=null)
            {
                transportOrderDetailViewModel = new TransportOrderDetailViewModel();
                transportOrderDetailViewModel.FdpID = transportOrderDetail.FdpID;
                transportOrderDetailViewModel.FDP = transportOrderDetail.FDP.Name;
                transportOrderDetailViewModel.CommodityID = transportOrderDetail.CommodityID;
                transportOrderDetailViewModel.Commodity = transportOrderDetail.Commodity.Name;
                transportOrderDetailViewModel.DonorID = transportOrderDetail.DonorID;
                transportOrderDetailViewModel.OriginWarehouse = transportOrderDetail.Hub.Name;
                transportOrderDetailViewModel.QuantityQtl = transportOrderDetail.QuantityQtl;
                transportOrderDetailViewModel.RequisitionID = transportOrderDetail.RequisitionID;
                transportOrderDetailViewModel.RequisitionNo = transportOrderDetail.ReliefRequisition.RequisitionNo;
                transportOrderDetailViewModel.SourceWarehouseID = transportOrderDetail.SourceWarehouseID;
                transportOrderDetailViewModel.TariffPerQtl = transportOrderDetail.TariffPerQtl;
                transportOrderDetailViewModel.Woreda = transportOrderDetail.FDP.AdminUnit.Name;
               
            }
            return transportOrderDetailViewModel;
        }
        public ActionResult TransportOrder()
        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            var reliefRequisitions = _transportOrderService.GetTransportOrderReleifRequisition(6);
            var transOrderDetailList = new List<TransportOrderDetail>();
            var reqItems = reliefRequisitions as ReliefRequisition[] ?? reliefRequisitions.ToArray();
            foreach (var reqItem in reqItems)
            {
                var result = _transportOrderService.GetTransportOrderDetail(reqItem.RequisitionID).ToList();
                transOrderDetailList.AddRange(result);
            }
            return View(transOrderDetailList);


        }

        public ActionResult TransportOrderDetail(int id)
        {
            var detailTransportOrders = _transportOrderService.GetTransportOrderDetailByTransportId(id);
            if (detailTransportOrders == null)
            {
                return HttpNotFound();
            }
            return View(detailTransportOrders.ToList());
        }
    }
}
