using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Services.EarlyWarning;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
//using Cats.Models;
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

        public ActionResult List()
        {
            var reliefrequistions = _reliefRequistionService.GetAllReliefRequisition();
            List<string> reqisitions =new List<string>();
            foreach(var req in reliefrequistions)
            {
                reqisitions.Add(req.RequisitionNo);
            }
            ViewData["Requisitions"] = reqisitions;
            return View();
        } 

        public ActionResult Index()
        {
            IDispatchAllocationDetailService _dispatchAllocationDetailService = new DispatchAllocationDetailService();

            var detaail = _dispatchAllocationDetailService.FindBy(t => t.RequisitionNo.Equals("31637"));
            var input = (from item in detaail
                         select new DispatchAllocation
                         {
                             RequisitionNo = item.RequisitionNo,
                             HubID = item.HubID,
                             CommodityID = item.CommodityID,
                             Beneficiery = item.Beneficiery,
                             Amount = item.Amount,
                             Unit = item.Unit,
                             ProjectCodeID=item.ProjectCodeID,
                             ShippingInstructionID=item.ShippingInstructionID,
                             Input = new DispatchAllocation.DispatchAllocationInput()
                             {
                                 Number = item.DispatchAllocationID,
                                 ProjectCodeID = item.ProjectCodeID,
                                 ShippingInstructionID = item.ShippingInstructionID
                             }
                         });
            return View(input);
        }
        [HttpGet]
        public ActionResult DispatchDetail(int id)
        {
            IDispatchAllocationDetailService _dispatchAllocationDetailService = new DispatchAllocationDetailService();
            string reqNumber = id.ToString();
            var detaail = _dispatchAllocationDetailService.FindBy(t => t.RequisitionNo.Equals(reqNumber));
            var input = (from item in detaail
                         select new DispatchAllocation
                         {
                             RequisitionNo=item.RequisitionNo,
                             HubID=item.HubID,
                             CommodityID=item.CommodityID,
                             Beneficiery=item.Beneficiery,
                             Amount=item.Amount,
                             Unit=item.Unit,
                            Input=new DispatchAllocation.DispatchAllocationInput()
                            {
                                Number=item.DispatchAllocationID,
                                ProjectCodeID=item.ProjectCodeID,
                                ShippingInstructionID=item.ShippingInstructionID
                            }
                         });
            return View(input);
        }
        [HttpPost]
        public ActionResult Edit(List<DispatchAllocation.DispatchAllocationInput> input)
        {
            IDispatchAllocationDetailService _dispatchAllocationDetailService = new DispatchAllocationDetailService();
            //Guid requId = 0;
            foreach (var dispatchDetail in input)
            {

                var tempDispatchAllocation =
                    _dispatchAllocationDetailService.FindById(dispatchDetail.Number);
                //requId = tempDispatchAllocation.DispatchAllocationID;
                tempDispatchAllocation.ProjectCodeID = dispatchDetail.ProjectCodeID;
                tempDispatchAllocation.ShippingInstructionID = dispatchDetail.ShippingInstructionID;
              
            }
            _dispatchAllocationDetailService.Save();


            return RedirectToAction("DispatchDetail", "ProjectAllocation");
        }
        public ActionResult test()
        {
            return View();
        }
    }
}