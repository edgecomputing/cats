using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models.Constant;
using Cats.Models.ViewModels.Bid;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Common;
using System;
using Cats.Models;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Logistics.Controllers
{
    public class PaymentRequestController:Controller
    {
        private readonly IBusinessProcessService _BusinessProcessService;
        private readonly IBusinessProcessStateService _BusinessProcessStateService;
        private readonly IApplicationSettingService _ApplicationSettingService;
        private readonly IPaymentRequestService _PaymentRequestservice;
        private readonly ITransportOrderService _TransportOrderService;
        private readonly IWorkflowStatusService _workflowStatusService;

        public PaymentRequestController(IBusinessProcessService _paramBusinessProcessService
                                        , IBusinessProcessStateService _paramBusinessProcessStateService
                                        , IApplicationSettingService _paramApplicationSettingService
                                        , IPaymentRequestService _paramPaymentRequestservice
                                        , ITransportOrderService _paramTransportOrderService
                                        ,IWorkflowStatusService workflowStatusService
                                        )
            {

                _BusinessProcessService=_paramBusinessProcessService;
                _BusinessProcessStateService=_paramBusinessProcessStateService;
                _ApplicationSettingService=_paramApplicationSettingService;
                _PaymentRequestservice =_paramPaymentRequestservice;
                _TransportOrderService = _paramTransportOrderService;
            _workflowStatusService = workflowStatusService;

            }

        public void LoadLookups()
        {
            ViewBag.TransportOrderID = new SelectList(_TransportOrderService.GetAllTransportOrder(), "TransportOrderID", "TransportOrderNo");
        }

        //
        // GET: /Procurement/PaymentRequest/

        public ActionResult Index()
        {
            LoadLookups();
            //var list = (IEnumerable<PaymentRequest>)_PaymentRequestservice.GetAll();
            var list = (IEnumerable<PaymentRequest>)_PaymentRequestservice.FindBy(t=>t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo<=2);
            return View(list);
        }

        public ActionResult Create()
        {
            LoadLookups();
            return View();
        }

        //
        // POST: /PSNP/RegionalPSNPPlan/Create

        [HttpPost]
        public ActionResult Create(PaymentRequest request)
        {
            if (ModelState.IsValid)
            {
                   int BP_PR = _ApplicationSettingService.getPaymentRequestWorkflow();
                   
                   if (BP_PR != 0)
                    {
                        BusinessProcessState createdstate = new BusinessProcessState
                        {
                            DatePerformed = DateTime.Now,
                            PerformedBy = "System",
                            Comment = "Created workflow for Payment Request"

                        };
                        //_PaymentRequestservice.Create(request);

                        BusinessProcess bp = _BusinessProcessService.CreateBusinessProcess(BP_PR,request.PaymentRequestID,
                                                                                           "PaymentRequest", createdstate);
                        request.BusinessProcessID = bp.BusinessProcessID;
                        _PaymentRequestservice.Create(request);
                        //_PaymentRequestservice.Update(request);
                        return RedirectToAction("Index");
                    }
                    ViewBag.ErrorMessage1 = "The workflow assosiated with Payment Request doesnot exist.";
                    ViewBag.ErrorMessage2 = "Please make sure the workflow is created and configured.";
                }
                LoadLookups();
                ModelState.AddModelError("Errors", "Could not create Request Plan.");
                return View(request);
            }

        public ActionResult Promote(BusinessProcessState st)
        {
            _BusinessProcessService.PromotWorkflow(st);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var paymentRequest = _PaymentRequestservice.FindById(id);
            ViewBag.TransportOrderID = new SelectList(_TransportOrderService.GetAllTransportOrder(), "TransportOrderID", "TransportOrderNo",paymentRequest.TransportOrderID);
            return View(paymentRequest);
        }
        [HttpPost]
        public ActionResult Edit(PaymentRequest paymentRequest)
        {
            if (ModelState.IsValid)
            {
                _PaymentRequestservice.Update(paymentRequest);
                return RedirectToAction("Index");
            }

            return View(paymentRequest);
        }
        public ActionResult PaymentRequestDetail(int transportOrderID)
        {
            var transportOrder = _TransportOrderService.Get(t => t.TransportOrderID == transportOrderID, null, "TransportOrderDetails.FDP,TransportOrderDetails.FDP.AdminUnit,TransportOrderDetails.Commodity,TransportOrderDetails.Hub,TransportOrderDetails.ReliefRequisition").FirstOrDefault();
            var datePref = UserAccountHelper.GetUser(User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder, datePref, statuses);
            ViewData["Transport.order.detail.ViewModel"] = transportOrder == null ? null :
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
            if (transportOrderDetail != null)
            {
                transportOrderDetailViewModel = new TransportOrderDetailViewModel
                {
                    FdpID = transportOrderDetail.FdpID,
                    FDP = transportOrderDetail.FDP.Name,
                    CommodityID = transportOrderDetail.CommodityID,
                    Commodity = transportOrderDetail.Commodity.Name,
                    DonorID = transportOrderDetail.DonorID,
                    OriginWarehouse = transportOrderDetail.Hub.Name,
                    QuantityQtl = transportOrderDetail.QuantityQtl,
                    RequisitionID = transportOrderDetail.RequisitionID,
                    RequisitionNo =
                        transportOrderDetail.ReliefRequisition.RequisitionNo,
                    SourceWarehouseID = transportOrderDetail.SourceWarehouseID,
                    TariffPerQtl = transportOrderDetail.TariffPerQtl,
                    Woreda = transportOrderDetail.FDP.AdminUnit.Name
                };
            }
            return transportOrderDetailViewModel;
        }
    }
}