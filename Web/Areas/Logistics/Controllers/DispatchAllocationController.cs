using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Helpers;
using Cats.Models.Constant;
using Cats.Services.EarlyWarning;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Logistics.Controllers
{
    public class DispatchAllocationController : Controller
    {
        //
        // GET: /Logistics/DispatchAllocation/

        private readonly IReliefRequisitionDetailService _reliefRequisitionDetailService;
        private readonly IReliefRequisitionService _reliefRequisitionService;

        public DispatchAllocationController(IReliefRequisitionService reliefRequisitionService, IReliefRequisitionDetailService reliefRequisitionDetailService)
        {
            _reliefRequisitionService = reliefRequisitionService;
            _reliefRequisitionDetailService = reliefRequisitionDetailService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HubAllocation([DataSourceRequest]DataSourceRequest request)
        {
            var requisititions = _reliefRequisitionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.Approved);
            var requisitionViewModel = HubAllocationViewModelBinder.ReturnRequisitionGroupByReuisitionNo(requisititions);
            return Json(requisitionViewModel.ToDataSourceResult(request));
        }

        public ActionResult AllocateProjectCode([DataSourceRequest]DataSourceRequest request)
        {
            var requisititions = _reliefRequisitionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.HubAssigned);
            var requisitionViewModel = HubAllocationViewModelBinder.ReturnRequisitionGroupByReuisitionNo(requisititions);
            return Json(requisitionViewModel.ToDataSourceResult(request));
        }
    }
}
