using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Helpers;
using Cats.Models.Constant;
using Cats.Models.Hubs;
using Cats.Services.EarlyWarning;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Cats.ViewModelBinder;

namespace Cats.Areas.Logistics.Controllers
{
    public class DistributionController : Controller
    {
        private ITransportOrderService _transportOrderService;
        private IWorkflowStatusService _workflowStatusService;

        private IDispatchAllocationService _dispatchAllocationService;
        private IDistributionService _distributionService;

        public DistributionController(ITransportOrderService transportOrderService,
                                      IWorkflowStatusService workflowStatusService,
                                      IDispatchAllocationService dispatchAllocationService,
                                      IDistributionService distributionService)
        {
            _transportOrderService = transportOrderService;
            _workflowStatusService = workflowStatusService;
            _dispatchAllocationService = dispatchAllocationService;
            _distributionService = distributionService;

        }
        //
        // GET: /Logistics/Distribution/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dispatches(int id)
        {
            //id--transportorderid
            var transportOrder = _transportOrderService.FindById(id);
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var datePref = UserAccountHelper.UserCalendarPreference();

            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder,
                                                                                                    datePref, statuses);

            var dispatch = _dispatchAllocationService.GetTransportOrderDispatches(id);
            
            var target = new TransportOrderDispatchViewModel
                             {DispatchViewModels = SetDatePreference(dispatch), TransportOrderViewModel = transportOrderViewModel};
            return View(target);
        }
        private List<DispatchViewModel> SetDatePreference(List<DispatchViewModel> dispatches )
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

        public ActionResult ReviveGRN(Guid dispatchId)
        {
            
            return View();
        }
        public ActionResult ReceivingNote(Guid distributionId)
        {
            return View();
        }
    }
}
