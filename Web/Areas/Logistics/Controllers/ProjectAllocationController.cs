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
        
        public ProjectAllocationController(IRegionalRequestService reliefRequistionService
           , IFDPService fdpService
            , IAdminUnitService adminUnitService,
            IProgramService programService,
            ICommodityService commodityService,
            IRegionalRequestDetailService reliefRequisitionDetailService,
            IProjectCodeAllocationService projectCodeAllocationService, 
            IProjectCodeService projectCodeService,
            IShippingInstructionService shippingInstructionService, IHubService hubService)
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
        }

        public ActionResult HubAllocationDetail()
        {
            ViewBag.Region = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.Zone = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 3), "AdminUnitID", "Name");
            var hubAllocation = _projectCodeAllocationService.GetHubAllocation(t=>t.HubAllocationID==0);
            return View(hubAllocation);
            
        }
        [HttpPost]
        public ActionResult HubAllocationDetail(Cats.Models.HubAllocation hubAllocation)
        {

            IEnumerable<Cats.Models.HubAllocation> detail =
               _projectCodeAllocationService.Get(
                   t => t.ReliefRequisition.RegionID == hubAllocation.ReliefRequisition.RegionID && t.ReliefRequisition.ZoneID == hubAllocation.ReliefRequisition.ZoneID);
             
            return View(detail);
            
        }
        [HttpGet]
        public ActionResult ProjectCodeAllocation(int id, int? requisition)
        {
            
            var hubInfo = new List<Cats.Models.HubAllocation>();
            
            List<Cats.Models.ProjectCode> projects = _projectCodeService.GetAllProjectCode();
            
            var selectList = new SelectList(projects,"ProjectCodeID", "value");

            ViewBag.ProjectCodeID = selectList;
            
            List<ShippingInstruction> siList = _shippingInstructionService.GetAllShippingInstruction();
            var siSelectList = new SelectList(siList, "ShippingInstructionID", "Value");

            ViewBag.ShippingInstructionID = siSelectList;
            
            var detail = _projectCodeAllocationService.FindBy(t => t.HubAllocationID == id);
            
            var input = (
                from item in detail
                select new ProjectCodeAllocation
                        {
                            HubAllocationID = item.HubAllocationID,
                            //HubAllocationID = _hubService.FindById(item.HubAllocationID)
                            Input = new ProjectCodeAllocation.ProjectCodeAllocationInput()
                            {
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
        public ActionResult Add(Cats.Areas.Logistics.Models.ProjectCodeAllocation newData , int HubAllocationID)
        {
            IProjectCodeAllocationService _projectcCodeAllocationService = new ProjectCodeAllocationService();

            Cats.Models.ProjectCodeAllocation Allocation = new Cats.Models.ProjectCodeAllocation
            {
                ShippingInstructionID = newData.ShippingInstructionID, 
                Amount_SI = newData.Amount_SI, ProjectCodeID = newData.ProjectCodeID, Amount_Project = newData.Amount_Project, 
                HubAllocationID = newData.HubAllocationID };
            _projectCodeAllocationService.Save(Allocation);

            
            return RedirectToAction("ProjectCodeAllocation", "ProjectAllocation", new { id = HubAllocationID});
            //return View();
        }
        
    }
}