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

        public DeliveryController(ITransportOrderService transportOrderService,
                                      IWorkflowStatusService workflowStatusService,
                                      IDispatchAllocationService dispatchAllocationService,
                                      IDeliveryService deliveryService,
            IDispatchService dispatchService,
            IDeliveryDetailService deliveryDetailService,
            INotificationService notificationService, IActionTypesService actionTypeService, IUserAccountService userAccountService,
            Cats.Services.EarlyWarning.ICommodityService commodityService, Cats.Services.EarlyWarning.IUnitService unitService)

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
            var currentUser =  _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);

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
            var deliveries = _deliveryService.Get(t => dispatchIds.Contains(t.DispatchID.Value)).ToList();
            //var deliveryViewModels = deliveries.Select(EditGoodsReceivingNote).Select(t => t.ContainsDiscripancy);
            var deliveryViewModels = new List<GRNViewModel>();
            foreach (var delivery in deliveries)
            {
                var localCopyDelivery = delivery;
                var deliveryDetail =
                    _deliveryDetailService.Get(t => t.DeliveryID == localCopyDelivery.DeliveryID).FirstOrDefault();
                if (deliveryDetail != null && deliveryDetail.ReceivedQuantity < deliveryDetail.SentQuantity)
                {
                    deliveryViewModels.Add(BindDeliveryViewModel(delivery));
                }
            }

            return Json(deliveryViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadGRN(Guid id)
        {
            var delivery = _deliveryService.Get(t => t.DispatchID == id).FirstOrDefault();
            var deliveryViewModel = new GRNViewModel();
            if (delivery != null)
            {
                deliveryViewModel = BindDeliveryViewModel(delivery);
                var deliveryDetail =
                    _deliveryDetailService.Get(t => t.DeliveryID == delivery.DeliveryID, null, "Commodity,Unit").
                        FirstOrDefault();
                if (deliveryDetail != null)
                {
                    deliveryViewModel.DeliveryID = deliveryDetail.DeliveryID;
                    deliveryViewModel.CommodityID = deliveryDetail.CommodityID;
                    deliveryViewModel.UnitID = deliveryDetail.UnitID;
                    deliveryViewModel.SentQuantity = deliveryDetail.SentQuantity;
                    deliveryViewModel.ReceivedQuantity = deliveryDetail.ReceivedQuantity;
                    deliveryViewModel.Commodity = deliveryDetail.Commodity.Name;
                    deliveryViewModel.Unit = deliveryDetail.Unit.Name;
                }
            }
            else
            {
                //var dispatchObj = _dispatchService.FindById(delivery.DispatchID);
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
            var deliveryViewModel = new GRNViewModel
                {
                    DeliveryID = delivery.DeliveryID,
                    InvoiceNo = delivery.InvoiceNo,
                    ReceivingNumber = delivery.ReceivingNumber,
                    WayBillNo = delivery.WayBillNo,
                    ReceivedBy = delivery.ReceivedBy,
                    ReceivedDate = delivery.ReceivedDate != null ? delivery.ReceivedDate.Value.ToShortDateString() : "",
                    DeliveryBy = delivery.DeliveryBy,
                    DeliveryDate = delivery.DeliveryDate != null ? delivery.DeliveryDate.Value.ToShortDateString() : "",
                    DocumentReceivedDate = delivery.DocumentReceivedDate != null ? delivery.DocumentReceivedDate.Value.ToShortDateString() : ""
                };
            return deliveryViewModel;
        }
        private IEnumerable<DeliveryViewModel> BindDeliverynViewModels(IEnumerable<Delivery> deliveries)
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


                    _deliveryService.AddDistribution(delivery);

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
                newdelivery = _deliveryService.FindBy(t => t.DeliveryID == delivery.DeliveryID).FirstOrDefault();
                if (newdelivery != null)
                {
                    newdelivery.DispatchID = delivery.DispatchID;
                    newdelivery.ReceivingNumber = delivery.ReceivingNumber;
                    newdelivery.WayBillNo = delivery.WayBillNo;
                    newdelivery.ReceivedBy = delivery.ReceivedBy;
                    newdelivery.ReceivedDate = DateTime.Parse(delivery.ReceivedDate);
                    newdelivery.DeliveryBy = delivery.DeliveryBy;
                    newdelivery.DeliveryDate = DateTime.Parse(delivery.DeliveryDate);
                    newdelivery.DocumentReceivedDate = DateTime.Parse(delivery.DocumentReceivedDate);
                    _deliveryService.EditDistribution(newdelivery);

                    deliveryDetail =
                        _deliveryDetailService.Get(t => t.DeliveryID == newdelivery.DeliveryID, null,
                                                       "Commodity,Unit").FirstOrDefault();
                    if (deliveryDetail != null)
                    {
                        deliveryDetail.ReceivedQuantity = delivery.ReceivedQuantity;
                        _deliveryDetailService.EditDistributionDetail(deliveryDetail);
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
                newdelivery.ReceivedDate = DateTime.Parse(delivery.ReceivedDate);
                newdelivery.DeliveryBy = delivery.DeliveryBy;
                newdelivery.DeliveryDate = DateTime.Parse(delivery.DeliveryDate);
                newdelivery.DocumentReceivedDate = DateTime.Parse(delivery.DocumentReceivedDate);
                if (dispatch != null)
                {
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
                _deliveryService.AddDistribution(newdelivery);

                deliveryDetail.DeliveryID = newdelivery.DeliveryID;
                deliveryDetail.CommodityID = delivery.CommodityID;
                deliveryDetail.UnitID = delivery.UnitID;
                deliveryDetail.SentQuantity = delivery.SentQuantity;
                deliveryDetail.ReceivedQuantity = delivery.ReceivedQuantity;
                deliveryDetail.Commodity = _commodityService.FindById(delivery.CommodityID);
                deliveryDetail.Unit = _unitService.FindById(delivery.UnitID);
                _deliveryDetailService.AddDistributionDetail(deliveryDetail);
            }
            var deliveryViewModel = BindDeliveryViewModel(newdelivery);
            if(deliveryViewModel!=null)
            {
                deliveryViewModel.DeliveryID = deliveryDetail.DeliveryID;
                deliveryViewModel.CommodityID = deliveryDetail.CommodityID;
                deliveryViewModel.UnitID = deliveryDetail.UnitID;
                deliveryViewModel.SentQuantity = deliveryDetail.SentQuantity;
                deliveryViewModel.ReceivedQuantity = deliveryDetail.ReceivedQuantity;
                deliveryViewModel.Commodity = deliveryDetail.Commodity.Name;
                deliveryViewModel.Unit = deliveryDetail.Unit.Name;
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
                _deliveryService.EditDistribution(delivery);
                if (dispatch.DispatchAllocation.TransportOrderID.HasValue)
                    transportOrderId = dispatch.DispatchAllocation.TransportOrderID.Value;
                return RedirectToAction("Dispatches", "Delivery", new { Area = "Logistics", id = transportOrderId });
            }

            return View(deliveryViewModel);
        }

        public ActionResult ReadDistributionDetail([DataSourceRequest]DataSourceRequest request, Guid deliveryID)
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
                    _deliveryDetailService.EditDistributionDetail(target);
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
                    }
                    else
                    {
                        destinationURl = "http://" + Request.Url.Authority +
                                        Request.ApplicationPath +
                                         "/Logistics/Delivery/Dispatches/" +
                                         transportOrderId;
                    }

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
            //    var distributionDetail = new DistributionDetail();
            //    distributionDetail.DistributionID = distribution.DistributionID;
            //    distributionDetail.DistributionDetailID = Guid.NewGuid();
            //    distributionDetail.CommodityID = dispatchDetail.CommodityID;
            //    distributionDetail.ReceivedQuantity = 0;
            //    distributionDetail.SentQuantity = dispatchDetail.RequestedQuantityInMT;
            //    distributionDetail.UnitID = dispatchDetail.UnitID;


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
            //    var distributionDetail = new DistributionDetail();
            //    distributionDetail.DistributionID = distribution.DistributionID;
            //    distributionDetail.DistributionDetailID = Guid.NewGuid();
            //    distributionDetail.CommodityID = dispatchDetail.CommodityID;
            //    distributionDetail.ReceivedQuantity = 0;
            //    distributionDetail.SentQuantity = dispatchDetail.RequestedQuantityInMT;
            //    distributionDetail.UnitID = dispatchDetail.UnitID;


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
            _deliveryService.EditDistribution(delivery);
            return RedirectToAction("Dispatches", new { id = TO });
        }


    }
}
