using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.Hubs;
using Cats.Services.Administration;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Helpers;
namespace Cats.Areas.Logistics.Controllers
{
    [Authorize]
    public class DeliveryController : Controller
    {
        private readonly ITransportOrderService _transportOrderService;
        private readonly IWorkflowStatusService _workflowStatusService;

        private readonly IDispatchAllocationService _dispatchAllocationService;
        private readonly IDeliveryService _deliveryService;
        private readonly IDispatchService _dispatchService;
        private readonly IDeliveryDetailService _deliveryDetailService;
        private readonly INotificationService _notificationService;

        private readonly IActionTypesService _actionTypeService;

        private readonly IUserAccountService _userAccountService;
        private readonly Cats.Services.EarlyWarning.ICommodityService _commodityService;
        private readonly Cats.Services.EarlyWarning.IUnitService _unitService;
        private readonly Cats.Services.Transaction.ITransactionService _transactionService;

        private readonly IBusinessProcessService _businessProcessService;
        private readonly IApplicationSettingService _applicationSettingService;
        private readonly ITransporterPaymentRequestService _transporterPaymentRequestService;

        public DeliveryController(ITransportOrderService transportOrderService,
                                      IWorkflowStatusService workflowStatusService,
                                      IDispatchAllocationService dispatchAllocationService,
                                      IDeliveryService deliveryService,
            IDispatchService dispatchService,
            IDeliveryDetailService deliveryDetailService,
            INotificationService notificationService, IActionTypesService actionTypeService, IUserAccountService userAccountService,
            Cats.Services.EarlyWarning.ICommodityService commodityService, Cats.Services.EarlyWarning.IUnitService unitService,
            Cats.Services.Transaction.ITransactionService transactionService, IBusinessProcessService businessProcessService, IApplicationSettingService applicationSettingService, ITransporterPaymentRequestService transporterPaymentRequestService)
        {
            _transportOrderService = transportOrderService;
            _workflowStatusService = workflowStatusService;
            _dispatchAllocationService = dispatchAllocationService;
            _deliveryService = deliveryService;
            _dispatchService = dispatchService;
            _deliveryDetailService = deliveryDetailService;
            _notificationService = notificationService;

            _actionTypeService = actionTypeService;

            _userAccountService = userAccountService;
            _commodityService = commodityService;
            _unitService = unitService;
            _transactionService = transactionService;
            _businessProcessService = businessProcessService;
            _applicationSettingService = applicationSettingService;
            _transporterPaymentRequestService = transporterPaymentRequestService;
        }
        //
        // GET: /Logistics/delivery/

        public ActionResult Index()
        {

            //  ViewBag.TransportOrderId = 3073;
            return View();
        }
        public ActionResult Dispatches(int id)
        {
            //id--transportorderid
            var transportOrder = _transportOrderService.Get(t => t.TransportOrderID == id, null, "Transporter").FirstOrDefault();
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var currentUser = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);

            var datePref = currentUser.DatePreference;

            ViewBag.TransportOrderId = id;
            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder,
                                                                                                    datePref, statuses);

            return View(transportOrderViewModel);
        }
        public ActionResult GetGINsWithoutGRN(int id)
        {
            var dispatch = _dispatchAllocationService.GetTransportOrderDispatches(id);

            foreach (var dispatchViewModel in dispatch)
            {
                var dispatchId = dispatchViewModel.DispatchID;
                var delivery = _deliveryService.FindBy(t => t.DispatchID == dispatchId).FirstOrDefault();
                dispatchViewModel.GRNReceived = delivery != null;
                if (delivery != null)
                    dispatchViewModel.DeliveryID = delivery.DeliveryID;
            }
            var dispatchView = SetDatePreference(dispatch);
            return Json(dispatchView.Where(t => !t.GRNReceived).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGINsWithGRN(int id)
        {
            var dispatch = _dispatchAllocationService.GetTransportOrderDispatches(id);

            foreach (var dispatchViewModel in dispatch)
            {
                var dispatchId = dispatchViewModel.DispatchID;
                var delivery = _deliveryService.FindBy(t => t.DispatchID == dispatchId).FirstOrDefault();
                dispatchViewModel.GRNReceived = delivery != null;
                if (delivery != null)
                    dispatchViewModel.DeliveryID = delivery.DeliveryID;
            }
            var dispatchView = SetDatePreference(dispatch);
            return Json(dispatchView.Where(t => t.GRNReceived).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReadDeliveryNotes(int id)
        {
            var dispatchIds =
                _dispatchService.Get(t => t.DispatchAllocation.TransportOrderID == id).Select(t => t.DispatchID).ToList();

            var deliveries = _deliveryService.Get(t => dispatchIds.Contains(t.DispatchID.Value), null, "DeliveryDetails").ToList();

            var deliveryViewModels = deliveries.Select(EditGoodsReceivingNote);
            return Json(deliveryViewModels, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReadDeliveryNotesDiscripancy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var dispatchIds =
                _dispatchService.Get(t => t.DispatchAllocation.TransportOrderID == id).Select(t => t.DispatchID).ToList();
            var deliveries = _deliveryService.Get(t => dispatchIds.Contains(t.DispatchID.Value), null, "DeliveryDetails").Where(t =>
            {
                var firstOrDefault = t.DeliveryDetails.FirstOrDefault();
                return firstOrDefault != null && (firstOrDefault.ReceivedQuantity < firstOrDefault.SentQuantity);
            }).ToList();
            var deliveryViewModels = deliveries.Select(BindDeliveryViewModel).ToList();
            return Json(deliveryViewModels, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadGRN(Guid id)
        {
            var delivery = _deliveryService.Get(t => t.DispatchID == id).FirstOrDefault();
            var deliveryViewModel = new GRNViewModel();
            if (delivery != null)
            {
                deliveryViewModel = BindDeliveryViewModel(delivery);
                var deliveryDetail =
                    _deliveryDetailService.Get(t => t.DeliveryID == delivery.DeliveryID, null, "Commodity,Unit,Delivery").
                        FirstOrDefault();
                if (deliveryDetail != null)
                {
                    deliveryViewModel.DeliveryID = deliveryDetail.DeliveryID;
                    deliveryViewModel.CommodityID = deliveryDetail.CommodityID;
                    deliveryViewModel.UnitID = deliveryDetail.UnitID;
                    deliveryViewModel.SentQuantity = deliveryDetail.SentQuantity.ToQuintal();
                    deliveryViewModel.ReceivedQuantity = deliveryDetail.ReceivedQuantity.ToQuintal();
                    deliveryViewModel.Commodity = deliveryDetail.Commodity.Name;
                    deliveryViewModel.Unit = deliveryDetail.Unit.Name;
                    deliveryViewModel.DeliveryBy = deliveryDetail.Delivery.DriverName;
                }
            }
            else
            {
                deliveryViewModel.RefNo = _transporterPaymentRequestService.Get().OrderByDescending(d => d.TransporterPaymentRequestID).Select(s => s.ReferenceNo).FirstOrDefault();
                var dispatchObj = _dispatchService.FindBy(t => t.DispatchID == id).FirstOrDefault();
                if (dispatchObj != null)
                {
                    var dispatchDetail = dispatchObj.DispatchDetails.FirstOrDefault();
                    if (dispatchDetail != null)
                    {
                        //deliveryViewModel.SentQuantity = dispatchDetail.RequestedQunatityInUnit;
                        deliveryViewModel.SentQuantity = dispatchDetail.RequestedQuantityInMT.ToQuintal(); //chahge to quintal. they receive using only quintal
                         deliveryViewModel.UnitID = dispatchDetail.UnitID;
                        deliveryViewModel.Unit = "Quintal";//They always want to receive using quintal// _unitService.FindById(int.Parse(dispatchDetail.UnitID.ToString())).Name;
                    }
                    deliveryViewModel.CommodityID = dispatchObj.DispatchAllocation.CommodityID;
                    deliveryViewModel.Commodity = dispatchObj.DispatchAllocation.Commodity.Name;
                    if (dispatchObj.DispatchAllocation.Unit != 0)
                    {
                       
                    }
                    deliveryViewModel.DeliveryBy = dispatchObj.DriverName;
                   
                }
            }
            deliveryViewModel.DispatchID = id;
            var firstOrDefault = _dispatchService.FindBy(t => t.DispatchID == id).FirstOrDefault();
            if (firstOrDefault != null)
                deliveryViewModel.InvoiceNo = firstOrDefault.GIN;
            
            return Json(deliveryViewModel, JsonRequestBehavior.AllowGet);
        }
        private GRNViewModel BindDeliveryViewModel(Delivery delivery)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var firstOrDefault = _dispatchService.FindBy(t => t.DispatchID == delivery.DispatchID).FirstOrDefault();
            var deliveryViewModel = new GRNViewModel();
            if (firstOrDefault != null)
            {
                deliveryViewModel = new GRNViewModel
                {
                    DeliveryID = delivery.DeliveryID,
                    InvoiceNo = delivery.InvoiceNo,
                    Program = firstOrDefault.DispatchAllocation.Program.Name,
                    BidNumber = firstOrDefault.BidNumber,
                    DispatchDatePref = firstOrDefault.DispatchDate.ToShortDateString(),
                    CreatedDatePref = firstOrDefault.DispatchDate.ToString(),
                    ReceivingNumber = delivery.ReceivingNumber,
                    WayBillNo = delivery.WayBillNo,
                    ReceivedBy = delivery.ReceivedBy,
                    ReceivedDate = delivery.ReceivedDate != null ? delivery.ReceivedDate.Value.ToShortDateString() : "",
                    DeliveryBy = delivery.DeliveryBy,
                    DeliveryDate = delivery.DeliveryDate != null ? delivery.DeliveryDate.Value.ToShortDateString() : "",
                    DocumentReceivedDate = delivery.DocumentReceivedDate != null ? delivery.DocumentReceivedDate.Value.ToShortDateString() : "",
                    RequisitionNo = delivery.RequisitionNo,
                    Zone=delivery.FDP.AdminUnit.AdminUnit2.Name,
                    Woreda = delivery.FDP.AdminUnit.Name,
                    FDP = delivery.FDP.Name,
                    PlateNoPrimary = delivery.PlateNoPrimary,
                    PlateNoTrailler = delivery.PlateNoTrailler,
                    DriverName = delivery.DriverName,
                    DispatchID = delivery.DispatchID,
                    RefNo = _transporterPaymentRequestService.FindBy(r=>r.GIN==delivery.InvoiceNo).Select(t=>t.ReferenceNo).FirstOrDefault()
                };
            }
            return deliveryViewModel;
        }
        private IEnumerable<DeliveryViewModel> BindDeliveryViewModels(IEnumerable<Delivery> deliveries)
        {
            var deliveryViewModels = new List<DeliveryViewModel>();
            foreach (var delivery in deliveries)
            {
                var deliveryViewModel = new DeliveryViewModel();
                deliveryViewModel.ReceivingNumber = delivery.ReceivingNumber;
                deliveryViewModel.WayBillNo = delivery.WayBillNo;
                deliveryViewModel.ReceivedBy = delivery.ReceivedBy;
                deliveryViewModel.ReceivedDate = delivery.ReceivedDate;
                deliveryViewModel.DeliveryBy = delivery.DeliveryBy;
                deliveryViewModel.DocumentReceivedDate = delivery.DocumentReceivedDate;
                deliveryViewModels.Add(deliveryViewModel);
            }
            return deliveryViewModels;
        }
        private List<DispatchViewModel> SetDatePreference(List<DispatchViewModel> dispatches)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;

            foreach (var dispatchViewModel in dispatches)
            {
                dispatchViewModel.CreatedDatePref =
                    dispatchViewModel.CreatedDate.ToCTSPreferedDateFormat(datePref);
                dispatchViewModel.DispatchDatePref =
                    dispatchViewModel.DispatchDate.ToCTSPreferedDateFormat(datePref);
            }
            return dispatches;
        }

        public ActionResult ReceiveGRN(Guid dispatchId)
        {
            var dispatch = _dispatchService.Get(t => t.DispatchID == dispatchId, null,
                "FDP,FDP.AdminUnit,FDP.AdminUnit.AdminUnit2,FDP.AdminUnit.AdminUnit2.AdminUnit2,Transporter,Hub").FirstOrDefault();

            var delivery = CreateGoodsReceivingNote(dispatch);
            return View(delivery);
        }
        [HttpPost]
        public ActionResult ReceiveGRN(DeliveryViewModel deliveryViewModel)
        {
            if (ModelState.IsValid)
            {
                int transportOrderId = 0;

                var dispatch = _dispatchService.Get(t => t.DispatchID == deliveryViewModel.DispatchID, null,
                    "DispatchDetails,DispatchAllocation").FirstOrDefault();
                
                var delivery = new Delivery();
                delivery.DeliveryBy = deliveryViewModel.DeliveryBy;
                delivery.DeliveryDate = deliveryViewModel.DeliveryDate;
                delivery.DispatchID = deliveryViewModel.DispatchID;
                delivery.DeliveryID = deliveryViewModel.DeliveryID;
                delivery.DocumentReceivedBy = deliveryViewModel.DocumentReceivedBy;
                delivery.DocumentReceivedDate = deliveryViewModel.DocumentReceivedDate;
                delivery.DonorID = deliveryViewModel.DonorID;
                delivery.DriverName = deliveryViewModel.DriverName;
                delivery.FDPID = deliveryViewModel.FDPID;
                delivery.HubID = deliveryViewModel.HubID;
                delivery.InvoiceNo = deliveryViewModel.InvoiceNo;
                delivery.PlateNoPrimary = deliveryViewModel.PlateNoPrimary;
                delivery.PlateNoTrailler = deliveryViewModel.PlateNoTrailler;
                delivery.ReceivedBy = deliveryViewModel.ReceivedBy;
                delivery.ReceivedDate = deliveryViewModel.ReceivedDate;
                delivery.ReceivingNumber = deliveryViewModel.ReceivingNumber;
                delivery.RequisitionNo = deliveryViewModel.RequisitionNo;
                delivery.TransporterID = deliveryViewModel.TransporterID;
                delivery.WayBillNo = deliveryViewModel.WayBillNo;
                if (dispatch != null)
                {


                    foreach (var dispatchDetail in dispatch.DispatchDetails)
                    {
                        var deliveryDetail = new DeliveryDetail();
                        deliveryDetail.DeliveryID = delivery.DeliveryID;
                        deliveryDetail.DeliveryDetailID = Guid.NewGuid();
                        deliveryDetail.CommodityID = dispatchDetail.CommodityID;
                        deliveryDetail.ReceivedQuantity = 0;
                        deliveryDetail.SentQuantity = dispatchDetail.RequestedQuantityInMT;
                        deliveryDetail.UnitID = dispatchDetail.UnitID;
                        delivery.DeliveryDetails.Add(deliveryDetail);

                    }


                    _deliveryService.AddDelivery(delivery);

                    var dispatchAllocation = dispatch.DispatchAllocation;
                    if (dispatchAllocation != null)
                    {
                        transportOrderId = dispatchAllocation.TransportOrderID.HasValue ? dispatchAllocation.TransportOrderID.Value : 0;
                    }
                }
                return RedirectToAction("EditGRN", "Delivery", new { Area = "Logistics", id = delivery.DeliveryID });
            }

            return View(deliveryViewModel);
        }
        [HttpPost]
        public ActionResult EditGRN(GRNViewModel delivery)
        {
            var originaldelivery = _deliveryService.Get(t => t.DispatchID == delivery.DispatchID, null,
                "FDP,FDP.AdminUnit,FDP.AdminUnit.AdminUnit2,FDP.AdminUnit.AdminUnit2.AdminUnit2,Hub").FirstOrDefault();
            var newdelivery = new Delivery();
            var deliveryDetail = new DeliveryDetail();
            if (originaldelivery != null)
            {
                newdelivery = _deliveryService.FindBy(t => t.DeliveryID == originaldelivery.DeliveryID).FirstOrDefault();
                TransporterPaymentRequest newTransportPaymentRequest = null;
                newTransportPaymentRequest = _transporterPaymentRequestService.FindBy(t => t.GIN == delivery.InvoiceNo).FirstOrDefault();
               

                if (newdelivery != null)
                {
                    newdelivery.DispatchID = delivery.DispatchID;
                    newdelivery.ReceivingNumber = delivery.ReceivingNumber;
                    newdelivery.WayBillNo = delivery.WayBillNo;
                    newdelivery.ReceivedBy = delivery.ReceivedBy;
                    newdelivery.ReceivedDate = delivery.ReceivedDate != null ? DateTime.Parse(delivery.ReceivedDate) : DateTime.Now;
                    newdelivery.DeliveryBy = delivery.DeliveryBy;
                    newdelivery.DeliveryDate = delivery.DeliveryDate != null ? DateTime.Parse(delivery.DeliveryDate) : DateTime.Now;
                    newdelivery.DocumentReceivedDate = delivery.DocumentReceivedDate != null ? DateTime.Parse(delivery.DocumentReceivedDate) : DateTime.Now;
                    
                    _deliveryService.EditDelivery(newdelivery);

                    deliveryDetail =
                        _deliveryDetailService.Get(t => t.DeliveryID == newdelivery.DeliveryID, null,
                                                       "Commodity,Unit").FirstOrDefault();
                    if (deliveryDetail != null)
                    {
                        deliveryDetail.ReceivedQuantity = delivery.ReceivedQuantity.ToMetricTone(); //save it using MT
                        _deliveryDetailService.EditDeliveryDetail(deliveryDetail);

                    }
                    if(newTransportPaymentRequest!=null)
                    {
                        newTransportPaymentRequest.ReferenceNo = delivery.RefNo;
                        _transporterPaymentRequestService.EditTransporterPaymentRequest(newTransportPaymentRequest);
                    }

                }

            }
            else
            {
                var dispatch = _dispatchService.FindBy(t => t.DispatchID == delivery.DispatchID).FirstOrDefault();
                //newDelivery = new Delivery();
                newdelivery.DeliveryID = Guid.NewGuid();
                newdelivery.DispatchID = delivery.DispatchID;
                newdelivery.ReceivingNumber = delivery.ReceivingNumber;
                newdelivery.WayBillNo = delivery.WayBillNo;
                newdelivery.ReceivedBy = delivery.ReceivedBy;
                newdelivery.ReceivedDate = delivery.ReceivedDate != null ? DateTime.Parse(delivery.ReceivedDate) : DateTime.Now;
                newdelivery.DeliveryBy = delivery.DeliveryBy;
                newdelivery.DeliveryDate = delivery.DeliveryDate != null ? DateTime.Parse(delivery.DeliveryDate) : DateTime.Now;
                newdelivery.DocumentReceivedDate = delivery.DocumentReceivedDate != null ? DateTime.Parse(delivery.DocumentReceivedDate) : DateTime.Now;
                if (dispatch != null)
                {
                    var dispatchAllocation =
                        _dispatchAllocationService.FindBy(m => m.DispatchAllocationID == dispatch.DispatchAllocationID).
                            FirstOrDefault();
                    if (dispatchAllocation != null && dispatchAllocation.ShippingInstructionID!=null)
                    {
                        newdelivery.DonorID = _deliveryService.GetDonorID(dispatchAllocation.ShippingInstruction.Value);
                    }
                    if (dispatch.DriverName != null)
                        newdelivery.DriverName = dispatch.DriverName;
                    //newDistribution.FDP = dispatch.FDP;
                    if (dispatch.FDPID != null)
                        newdelivery.FDPID = int.Parse(dispatch.FDPID.ToString());
                    //newDistribution.Hub = dispatch.Hub;
                    newdelivery.HubID = dispatch.HubID;
                    newdelivery.InvoiceNo = dispatch.GIN;
                    newdelivery.PlateNoPrimary = dispatch.PlateNo_Prime;
                    newdelivery.PlateNoTrailler = dispatch.PlateNo_Trailer;
                    newdelivery.RequisitionNo = dispatch.RequisitionNo;
                    newdelivery.TransporterID = dispatch.TransporterID;
                }
                //_deliveryService.AddDelivery(newdelivery);

                deliveryDetail.DeliveryDetailID = Guid.NewGuid();
                deliveryDetail.CommodityID = delivery.CommodityID;
                deliveryDetail.UnitID = delivery.UnitID;
                deliveryDetail.SentQuantity = delivery.SentQuantity.ToMetricTone();//save it using MT
                deliveryDetail.ReceivedQuantity = delivery.ReceivedQuantity.ToMetricTone();//save it using MT
                //deliveryDetail.Delivery.DeliveryBy = _commodityService.FindById(delivery.CommodityID);
                //deliveryDetail.Unit = _unitService.FindById(delivery.UnitID);
                newdelivery.DeliveryDetails = new List<DeliveryDetail> { deliveryDetail };
                _deliveryService.AddDelivery(newdelivery);

                var transporterPaymentRequest = new TransporterPaymentRequest();
                transporterPaymentRequest.ReferenceNo =  delivery.RefNo;
                transporterPaymentRequest.GIN = delivery.InvoiceNo;

                //var firstOrDefault = _transportOrderService.Get(t => t.TransporterID == newdelivery.TransporterID && t.StatusID >= 3).FirstOrDefault();
                var transportOrderId = delivery.TransportOrderID;
                if (transportOrderId != null)
                    transporterPaymentRequest.TransportOrderID = transportOrderId;

                transporterPaymentRequest.DeliveryID = newdelivery.DeliveryID;
                transporterPaymentRequest.ShortageBirr = (decimal)0.00;
                int BP_PR = _applicationSettingService.getPaymentRequestWorkflow();

                if (BP_PR != 0)
                {
                    BusinessProcessState createdstate = new BusinessProcessState
                    {
                        DatePerformed = DateTime.Now,
                        PerformedBy = "System",
                        Comment = "Created workflow for Payment Request"

                    };
                    //_PaymentRequestservice.Create(request);

                    BusinessProcess bp = _businessProcessService.CreateBusinessProcess(BP_PR, transporterPaymentRequest.TransporterPaymentRequestID,
                                                                                        "PaymentRequest", createdstate);
                    transporterPaymentRequest.BusinessProcessID = bp.BusinessProcessID;
                    transporterPaymentRequest.RequestedDate = DateTime.Now;
                    _transporterPaymentRequestService.AddTransporterPaymentRequest(transporterPaymentRequest);
                }
                ViewBag.ErrorMessage1 = "The workflow assosiated with Payment Request doesnot exist.";
                ViewBag.ErrorMessage2 = "Please make sure the workflow is created and configured.";
            }
            var deliveryViewModel = new GRNViewModel();
            if (newdelivery != null && deliveryDetail != null)
            {
                var deliveryWithFDP = _deliveryService.Get(t => t.DeliveryID == newdelivery.DeliveryID, null, "FDP").FirstOrDefault();
                deliveryViewModel = BindDeliveryViewModel(deliveryWithFDP);
                var deliveryDetailWithComodityUnit = _deliveryDetailService.Get(t => t.DeliveryDetailID == deliveryDetail.DeliveryDetailID, null, "Commodity,Unit").FirstOrDefault();
                if (deliveryDetailWithComodityUnit != null)
                {
                    deliveryViewModel.DeliveryID = deliveryDetailWithComodityUnit.DeliveryID;
                    deliveryViewModel.CommodityID = deliveryDetailWithComodityUnit.CommodityID;
                    deliveryViewModel.UnitID = deliveryDetailWithComodityUnit.UnitID;
                    deliveryViewModel.SentQuantity = deliveryDetailWithComodityUnit.SentQuantity;
                    deliveryViewModel.ReceivedQuantity = deliveryDetailWithComodityUnit.ReceivedQuantity;
                    deliveryViewModel.Commodity = deliveryDetailWithComodityUnit.Commodity.Name;
                    deliveryViewModel.Unit = deliveryDetailWithComodityUnit.Unit.Name;

                }
            }

            return Json(deliveryViewModel, JsonRequestBehavior.AllowGet);
            //return View("Dispatches", distributionViewModel);
        }


        public ActionResult EditGRN(DeliveryViewModel deliveryViewModel)
        {
            if (ModelState.IsValid)
            {
                int transportOrderId = 0;

                var delivery = _deliveryService.Get(t => t.DeliveryID == deliveryViewModel.DeliveryID, null,
                    "DeliveryDetails,FDP,Hub").FirstOrDefault();

                var dispatch = _dispatchService.Get(t => t.DispatchID == deliveryViewModel.DispatchID, null,
                   "DispatchAllocation").FirstOrDefault();

                delivery.DeliveryBy = deliveryViewModel.DeliveryBy;
                delivery.DeliveryDate = deliveryViewModel.DeliveryDate;


                delivery.DocumentReceivedBy = deliveryViewModel.DocumentReceivedBy;
                delivery.DocumentReceivedDate = deliveryViewModel.DocumentReceivedDate;

                delivery.DriverName = deliveryViewModel.DriverName;

                delivery.InvoiceNo = deliveryViewModel.InvoiceNo;
                delivery.PlateNoPrimary = deliveryViewModel.PlateNoPrimary;
                delivery.PlateNoTrailler = deliveryViewModel.PlateNoTrailler;
                delivery.ReceivedBy = deliveryViewModel.ReceivedBy;
                delivery.ReceivedDate = deliveryViewModel.ReceivedDate;
                delivery.ReceivingNumber = deliveryViewModel.ReceivingNumber;
                delivery.RequisitionNo = deliveryViewModel.RequisitionNo;
                delivery.TransporterID = deliveryViewModel.TransporterID;
                delivery.WayBillNo = deliveryViewModel.WayBillNo;
                _deliveryService.EditDelivery(delivery);
                if (dispatch.DispatchAllocation.TransportOrderID.HasValue)
                    transportOrderId = dispatch.DispatchAllocation.TransportOrderID.Value;
                return RedirectToAction("Dispatches", "Delivery", new { Area = "Logistics", id = transportOrderId });
            }

            return View(deliveryViewModel);
        }

        public ActionResult ReadDeliveryDetail([DataSourceRequest]DataSourceRequest request, Guid deliveryID)
        {
            var deliveryDetails =
                _deliveryDetailService.Get(t => t.DeliveryID == deliveryID, null, "Commodity,Unit").
                    ToList();

            var deliveryDetailsViewModels = BindDeliveryDetailViewModel(deliveryDetails);

            return Json(deliveryDetailsViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private List<DeliveryDetailViewModel> BindDeliveryDetailViewModel(List<DeliveryDetail> deliveryDetails)
        {
            var deliveryDetailViewModels = new List<DeliveryDetailViewModel>();
            foreach (var deliveryDetail in deliveryDetails)
            {
                var deliveryDetailViewModel = new DeliveryDetailViewModel();
                deliveryDetailViewModel.CommodityID = deliveryDetail.CommodityID;
                deliveryDetailViewModel.Commodity = deliveryDetail.Commodity.Name;
                deliveryDetailViewModel.DeliveryDetailID = deliveryDetail.DeliveryDetailID;
                deliveryDetailViewModel.DeliveryID = deliveryDetail.DeliveryID;
                deliveryDetailViewModel.ReceivedQuantity = deliveryDetail.ReceivedQuantity;
                deliveryDetailViewModel.SentQuantity = deliveryDetail.SentQuantity;
                deliveryDetailViewModel.UnitID = deliveryDetail.UnitID;
                deliveryDetailViewModel.Unit = deliveryDetail.Unit.Name;
                deliveryDetailViewModels.Add(deliveryDetailViewModel);
            }
            return deliveryDetailViewModels;
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateDeliveryDetail([DataSourceRequest] DataSourceRequest request, DeliveryDetailViewModel deliveryDetail)
        {
            if (deliveryDetail != null && ModelState.IsValid)
            {
                var target = _deliveryDetailService.FindById(deliveryDetail.DeliveryDetailID);
                if (target != null)
                {
                    target.ReceivedQuantity = deliveryDetail.ReceivedQuantity;
                    _deliveryDetailService.EditDeliveryDetail(target);
                }
            }

            if (deliveryDetail.ReceivedQuantity < deliveryDetail.SentQuantity)
            {
                var delivery =
                    _deliveryService.FindBy(t => t.DeliveryID == deliveryDetail.DeliveryID).
                        FirstOrDefault();
                var id = delivery.DispatchID;
                var dispatch = _dispatchService.Get(t => t.DispatchID == id, null, "DispatchAllocation,DispatchAllocation.Transporter").FirstOrDefault();
                if (dispatch != null)
                {
                    var transportOrderId = dispatch.DispatchAllocation.TransportOrderID.HasValue
                                             ? dispatch.DispatchAllocation.TransportOrderID.Value
                                             : 0;

                    SendNotification(transportOrderId,
                        dispatch.DispatchAllocation.Transporter.Name);
                }

            }
            return Json(new[] { deliveryDetail }.ToDataSourceResult(request, ModelState));
        }
        private void SendNotification(int transportOrderId, string transporterName)
        {
            try
            {
                string destinationURl;
                if (Request.Url.Host != null)
                {
                    if (Request.Url.Host == "localhost")
                    {
                        destinationURl = "http://" + Request.Url.Authority +
                                         "/Logistics/Delivery/Dispatches/" +
                                         transportOrderId;
                        return;
                    }
                    destinationURl = "http://" + Request.Url.Authority +
                                     Request.ApplicationPath +
                                     "/Logistics/Delivery/Dispatches/" +
                                     transportOrderId;

                    _notificationService.AddNotificationForProcurmentForGRNDiscripancy(destinationURl, transportOrderId, transporterName);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult ReceivingNote(Guid deliveryID)
        {
            return View();
        }
        private DeliveryViewModel CreateGoodsReceivingNote(Cats.Models.Hubs.Dispatch dispatch)
        {
            if (dispatch == null) return new DeliveryViewModel();
            var delivery = new DeliveryViewModel();
            delivery.DeliveryDate = DateTime.Today;
            delivery.DispatchID = dispatch.DispatchID;
            delivery.DeliveryDate = DateTime.Today;
            delivery.DocumentReceivedBy = UserAccountHelper.GetUser(User.Identity.Name).UserProfileID;
            delivery.DocumentReceivedDate = DateTime.Today;
            delivery.DeliveryID = Guid.NewGuid();
            //delivery.DonorID=dispatch.
            delivery.DriverName = dispatch.DriverName;
            delivery.FDPID = dispatch.FDPID.Value;
            delivery.HubID = dispatch.HubID;
            delivery.TransporterID = dispatch.TransporterID;
            delivery.InvoiceNo = dispatch.GIN;
            delivery.PlateNoPrimary = dispatch.PlateNo_Prime;
            delivery.PlateNoTrailler = dispatch.PlateNo_Trailer;
            delivery.RequisitionNo = dispatch.RequisitionNo;
            delivery.FDP = dispatch.FDP.Name;
            delivery.Region = dispatch.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name;
            delivery.Zone = dispatch.FDP.AdminUnit.AdminUnit2.Name;
            delivery.Woreda = dispatch.FDP.AdminUnit.Name;
            delivery.Hub = dispatch.Hub.Name;
            delivery.Transporter = dispatch.Transporter.Name;


            //foreach (var dispatchDetail in dispatch.DispatchDetails)
            //{
            //    var deliveryDetail = new DistributionDetail();
            //    deliveryDetail.DistributionID = distribution.DistributionID;
            //    deliveryDetail.DistributionDetailID = Guid.NewGuid();
            //    deliveryDetail.CommodityID = dispatchDetail.CommodityID;
            //    deliveryDetail.ReceivedQuantity = 0;
            //    deliveryDetail.SentQuantity = dispatchDetail.RequestedQuantityInMT;
            //    deliveryDetail.UnitID = dispatchDetail.UnitID;


            //}
            return delivery;
        }
        private DeliveryViewModel EditGoodsReceivingNote(Delivery delivery)
        {

            if (delivery == null) return new DeliveryViewModel();
            var dispatch = _dispatchService.Get(t => t.DispatchID == delivery.DispatchID, null,
               "FDP,FDP.AdminUnit,FDP.AdminUnit.AdminUnit2,FDP.AdminUnit.AdminUnit2.AdminUnit2,Transporter,Hub").FirstOrDefault();

            var deliveryViewModel = new DeliveryViewModel();

            deliveryViewModel.DispatchID = delivery.DispatchID;
            deliveryViewModel.DeliveryDate = delivery.DeliveryDate;
            deliveryViewModel.DocumentReceivedBy = delivery.DocumentReceivedBy;
            deliveryViewModel.DocumentReceivedDate = delivery.DocumentReceivedDate;
            deliveryViewModel.DeliveryID = delivery.DeliveryID;
            //distribution.DonorID=dispatch.
            deliveryViewModel.DriverName = delivery.DriverName;
            deliveryViewModel.FDPID = delivery.FDPID;
            deliveryViewModel.HubID = delivery.HubID;
            deliveryViewModel.TransporterID = delivery.TransporterID;
            deliveryViewModel.InvoiceNo = delivery.InvoiceNo;
            deliveryViewModel.PlateNoPrimary = delivery.PlateNoPrimary;
            deliveryViewModel.PlateNoTrailler = delivery.PlateNoTrailler;
            deliveryViewModel.RequisitionNo = delivery.RequisitionNo;
            deliveryViewModel.FDP = dispatch.FDP.Name;
            deliveryViewModel.Region = dispatch.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name;
            deliveryViewModel.Zone = dispatch.FDP.AdminUnit.AdminUnit2.Name;
            deliveryViewModel.Woreda = dispatch.FDP.AdminUnit.Name;
            deliveryViewModel.Hub = dispatch.Hub.Name;
            deliveryViewModel.DeliveryBy = delivery.DeliveryBy;
            deliveryViewModel.ReceivedBy = delivery.ReceivedBy;
            deliveryViewModel.ReceivedDate = delivery.ReceivedDate;
            deliveryViewModel.ReceivingNumber = delivery.ReceivingNumber;
            deliveryViewModel.InvoiceNo = delivery.InvoiceNo;
            deliveryViewModel.WayBillNo = delivery.WayBillNo;
            deliveryViewModel.RequisitionNo = delivery.RequisitionNo;
            deliveryViewModel.Transporter = dispatch.Transporter.Name;
            deliveryViewModel.Status = delivery.Status;
            deliveryViewModel.ActionTypeRemark = delivery.ActionTypeRemark;
            var pref = UserAccountHelper.UserCalendarPreference();
            deliveryViewModel.DeliveryDatePref = delivery.DeliveryDate.HasValue
                                                         ? delivery.DeliveryDate.Value.ToCTSPreferedDateFormat(pref)
                                                         : "";
            deliveryViewModel.ReceivedDatePref = delivery.ReceivedDate.HasValue
                                                         ? delivery.ReceivedDate.Value.ToCTSPreferedDateFormat(pref)
                                                         : "";
            deliveryViewModel.DocumentReceivedDatePref = delivery.DocumentReceivedDate.HasValue
                                                                 ? delivery.DocumentReceivedDate.Value.
                                                                       ToCTSPreferedDateFormat(pref)
                                                                 : "";

            deliveryViewModel.ContainsDiscripancy =
                delivery.DeliveryDetails.Any(t => t.ReceivedQuantity < t.SentQuantity);
            //foreach (var dispatchDetail in dispatch.DispatchDetails)
            //{
            //    var deliveryDetail = new DistributionDetail();
            //    deliveryDetail.DistributionID = distribution.DistributionID;
            //    deliveryDetail.DistributionDetailID = Guid.NewGuid();
            //    deliveryDetail.CommodityID = dispatchDetail.CommodityID;
            //    deliveryDetail.ReceivedQuantity = 0;
            //    deliveryDetail.SentQuantity = dispatchDetail.RequestedQuantityInMT;
            //    deliveryDetail.UnitID = dispatchDetail.UnitID;


            //}
            return deliveryViewModel;
        }


        public ActionResult DiscripancyAction(Guid id)
        {
            var delivery = _deliveryService.Get(t => t.DeliveryID == id, null,
                "FDP,FDP.AdminUnit,FDP.AdminUnit.AdminUnit2,FDP.AdminUnit.AdminUnit2.AdminUnit2,Hub").FirstOrDefault();
            ViewBag.ActionTypes = new SelectList(_actionTypeService.GetAllActionType(), "ActionId", "Name");
            var deliveryViewModel = EditGoodsReceivingNote(delivery);
            return View(deliveryViewModel);
        }

        public ActionResult SaveDiscripancy(DeliveryViewModel _deliveryViewModel, FormCollection collection)
        {

            var actionType = int.Parse(collection["Actiontype"].ToString(CultureInfo.InvariantCulture));
            var remark = collection["Remark"].ToString(CultureInfo.InvariantCulture);
            var TO = collection.Keys[2].ToString(CultureInfo.InvariantCulture);
            var delivery = _deliveryService.Get(t => t.DeliveryID == _deliveryViewModel.DeliveryID).Single();
            delivery.Status = (int)Cats.Models.Constant.DistributionStatus.Closed;
            delivery.ActionType = actionType;
            delivery.ActionTypeRemark = remark;
            _deliveryService.EditDelivery(delivery);
            return RedirectToAction("Dispatches", new { id = TO });
        }

       public ActionResult RejectToHubs(string id)
       {
           var dispach = _dispatchService.FindBy(g => g.GIN == id).FirstOrDefault();
           if(dispach!=null)
           {
               _dispatchService.RejectToHubs(dispach);
               return Json(true, JsonRequestBehavior.AllowGet);
           }
           return Json(false, JsonRequestBehavior.AllowGet);
       }
    }
}
