using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Services.EarlyWarning;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Services;
namespace Cats.Areas.Logistics.Controllers
{
    public class ProjectAllocationController : Controller
    {
        //
        // GET: /Logistics Project Allocation/

        private IReliefRequisitionService _reliefRequistionService;
        private IDispatchAllocationDetailService _dispatchAllocationDetailService;

        public ProjectAllocationController(IReliefRequisitionService reliefRequistionService,
            IDispatchAllocationDetailService dispatchAllocationService)
        {
            this._reliefRequistionService = reliefRequistionService;
            this._dispatchAllocationDetailService = dispatchAllocationService;
        }
        public ProjectAllocationController()
        {
        }
        public ActionResult getRRD()
        {

            var reliefrequistions = _reliefRequistionService.GetAllReliefRequisition();
            return View(reliefrequistions.ToList());
        }
       
        public ActionResult DispatchDetail()
        {
            IDispatchAllocationDetailService _dispatchAllocationDetailService = new DispatchAllocationDetailService();

            List<DispatchAllocation> detaail = _dispatchAllocationDetailService.FindBy(t => t.RequisitionNo.Equals("31637"));
            return View(detaail);
        }

        public ActionResult test()
        {
            return View();
        }
    }
}