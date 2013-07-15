using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Services.EarlyWarning;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
//using Cats.Models;
using Cats.Services;
using ProjectCodeAllocation = Cats.Areas.Logistics.Models.ProjectCodeAllocation;

namespace Cats.Areas.Logistics.Controllers
{
    public class ProjectAllocationController : Controller
    {
        //
        // GET: /Logistics Project Allocation/

        private IRegionalRequestService _regionalRequestService;
        private IFDPService _fdpService;
        //private IRoundService _roundService;
        private IAdminUnitService _adminUnitService;
        private IProgramService _programService;
        private ICommodityService _commodityService;
        private IRegionalRequestDetailService _reliefRequisitionDetailService;
        private IProjectCodeAllocationService _projectCodeAllocationService;
        private IProjectCodeService _projectCodeService;
        private IShippingInstructionService _shippingInstructionService;
        private IHubService _hubService;
        private IHubAllocationService _hubAllocationService;
        private IReliefRequisitionService _requisitionService;
        private ITransactionService _transactionService;
        public ProjectAllocationController(IRegionalRequestService reliefRequistionService
           , IFDPService fdpService
            , IAdminUnitService adminUnitService,
            IProgramService programService,
            ICommodityService commodityService,
            IRegionalRequestDetailService reliefRequisitionDetailService,
            IProjectCodeAllocationService projectCodeAllocationService, 
            IProjectCodeService projectCodeService,
            IShippingInstructionService shippingInstructionService, 
            IHubService hubService, 
            IHubAllocationService hubAllocationService,
            IReliefRequisitionService requisitionService, ITransactionService transactionservice)
        {
            this._regionalRequestService = reliefRequistionService;
            this._adminUnitService = adminUnitService;
            this._commodityService = commodityService;
            this._fdpService = fdpService; 
            this._programService = programService;
            this._reliefRequisitionDetailService = reliefRequisitionDetailService;
            this._projectCodeAllocationService = projectCodeAllocationService;
            this._projectCodeService = projectCodeService;
            this._shippingInstructionService = shippingInstructionService;
            this._hubService = hubService;
            this._hubAllocationService = hubAllocationService;
            this._requisitionService = requisitionService;
            this._transactionService = transactionservice;
        }

        //public ActionResult HubAllocationDetail()
        //{
        //    var seleList = new SelectList(_hubService.GetAllHub(), "HubId", "Name");
        //    ViewBag.HubId = seleList;
        //    ViewBag.Zone = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 3), "AdminUnitID", "Name");
        //    var hubAllocation = _projectCodeAllocationService.GetHubAllocation(t=>t.HubAllocationID==0);
        //    return View(hubAllocation);
            
        //}
        [HttpPost]
        public ActionResult HubAllocationDetail(Cats.Models.HubAllocation hubAllocation)
        {
            var seleList = new SelectList(_hubService.GetAllHub(), "HubId", "Name");
            ViewBag.HubId = seleList;
            IEnumerable<Cats.Models.HubAllocation> detail =
               _projectCodeAllocationService.Get(
                   t => t.HubID == hubAllocation.HubID);
             
            return View(detail);
            
        }
        public  ActionResult Allocate()
        {
            ViewBag.HubId = new SelectList(_hubService.GetAllHub(), "HubId", "Name");
            var hubAllocation = _projectCodeAllocationService.GetHubAllocationByHubID(0);
            return View(hubAllocation);
        }
        public ActionResult HubAllocatedList(int hubId)
        {
            var hubAllocations= new List<Cats.Models.HubAllocation>();
            var hubAllocation = _projectCodeAllocationService.GetHubAllocationByHubID(hubId);
            var firstOrDefault = hubAllocation.FirstOrDefault();
            if (firstOrDefault != null)
            {
                var reqId = firstOrDefault.RequisitionID;
                foreach (var allocation in hubAllocation)
                {
                    var req = _requisitionService.FindById(allocation.RequisitionID);
                    hubAllocations.Add(new Cats.Models.HubAllocation 
                                           {
                                               ReliefRequisition = req,
                                               HubAllocationID = allocation.HubAllocationID,
                                           }); 
                }
            }


            return View(hubAllocations);
        }
        
        [HttpGet]
        public ActionResult ProjectCodeAllocation(int id, int requisitionId)
        {
            
            var previousProjectAllocation = _projectCodeAllocationService.FindBy(t => t.HubAllocationID == id);
            var hubAllocationInfo = _projectCodeAllocationService.GetHubAllocationByID(id);
            var requisition = _requisitionService.FindById(requisitionId);
            List<Cats.Models.ProjectCode> projectFromTransaction = _transactionService.getAllProjectByHubCommodity(hubAllocationInfo.HubID, requisition.Commodity.CommodityID);
            var selectList = new SelectList(projectFromTransaction, "ProjectCodeID", "value");
            ViewBag.ProjectCodeID = selectList;
            List<ShippingInstruction> siList = _transactionService.getAllSIByHubCommodity(hubAllocationInfo.HubID, requisition.Commodity.CommodityID);
            var siSelectList = new SelectList(siList, "ShippingInstructionID","Value" );
            ViewBag.ShippingInstructionID = siSelectList;

            ViewBag.HubName = hubAllocationInfo.Hub.Name;
            ViewBag.HubAllocationID = hubAllocationInfo.HubAllocationID;
            ViewBag.requested = requisition.ReliefRequisitionDetails.FirstOrDefault().Amount;
            ViewBag.allocated = previousProjectAllocation.Sum(t => t.Amount_Project);
            ViewBag.projectBalance = _transactionService.getProjectBalance(hubAllocationInfo.HubID,
                                                                           requisition.Commodity.CommodityID);
            ViewBag.SIBalance = _transactionService.getSIBalance(hubAllocationInfo.HubID,
                                                                 requisition.Commodity.CommodityID);

            var input = (
                from item in previousProjectAllocation
                select new ProjectCodeAllocation
                        {
                            Hub = hubAllocationInfo.Hub.Name,
                            HubAllocationID = item.HubAllocationID,
                            Input = new ProjectCodeAllocation.ProjectCodeAllocationInput()
                            {
                                Hub = hubAllocationInfo.Hub.Name,
                                HubAllocationID =  item.HubAllocationID,
                                ProjectCodeAllocationID = item.ProjectCodeAllocationID,  
                                ProjectCodeID = item.ProjectCodeID,
                                SINumberID = item.ShippingInstructionID,
                                Amount_FromProject = item.Amount_Project,
                                Amount_FromSI = item.Amount_SI
                            }

                        });

            return View(input);
            
        }

        
        [HttpPost]
        public ActionResult Add(Cats.Models.ProjectCodeAllocation newData)
        {

            _projectCodeAllocationService.Save(newData);
            
            var hubAllocationInfo = _projectCodeAllocationService.GetHubAllocationByID(newData.HubAllocationID);
            return RedirectToAction("ProjectCodeAllocation", "ProjectAllocation", new { id = newData.HubAllocationID, requisitionId = hubAllocationInfo.RequisitionID});
            
        }
        
    }
}