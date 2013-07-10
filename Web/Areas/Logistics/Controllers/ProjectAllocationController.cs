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
        private IProjectCodeAllocationService _projectCodeAllocationService;

        public ProjectAllocationController(
            IReliefRequisitionService reliefRequistionService,
            IProjectCodeAllocationService dispatchAllocationService)
        {
            this._reliefRequistionService = reliefRequistionService;
            this._projectCodeAllocationService = dispatchAllocationService;
        }
        public ProjectAllocationController()
        {
            
        }
       
       
        public ActionResult Index()
        {
            //IProjectCodeAllocationService _projectcCodeAllocationService = new ProjectCodeAllocationService();
            //List<Cats.Models.HubAllocation> HubAllocated = _projectcCodeAllocationService.GetAllRequisitionsInHubAllocation();
            List<ActionLink> list = new List<ActionLink>();

            List<Cats.Models.HubAllocation> HubAllocated = new List<Cats.Models.HubAllocation>();
            HubAllocated.Add(new Cats.Models.HubAllocation { RequisitionID = 31637});
            HubAllocated.Add(new Cats.Models.HubAllocation { RequisitionID = 27319});
            HubAllocated.Add(new Cats.Models.HubAllocation { RequisitionID = 31703});
            HubAllocated.Add(new Cats.Models.HubAllocation { RequisitionID = 27268});
            foreach (var item in HubAllocated)
            {
                list.Add(new ActionLink
                {
                    Text = item.RequisitionID.ToString(),
                    Controller = "ProjectAllocation",
                    Action = "DispatchDetail",
                    Id= Convert.ToInt16(item.RequisitionID)
                });
            }
            ViewData["RequisitionList"] = list;
            var detaail = _projectCodeAllocationService.GetHubAllocation(t => t.HubAllocationID == 0);

            var input = (from item in detaail
                         select new HubAllocation
                         {
                             RequisitionID = item.RequisitionID,
                             HubID = item.HubID,
                             //Hub = item.Hub,
                             HubAllocationID = item.HubAllocationID,
                             AllocatedBy = item.AllocatedBy,
                             //Unit = item.Unit,
                             //ProjectCodeID = item.ProjectCodeID,
                             //ShippingInstructionID = item.ShippingInstructionID,
                             //Input = new DispatchAllocation.DispatchAllocationInput()
                             //{
                             //    Number = item.DispatchAllocationID,
                             //    ProjectCodeID = item.ProjectCodeID,
                             //    ShippingInstructionID = item.ShippingInstructionID
                             //}
                         });
            return View(input);
            //return View(detaail);
        }
        [HttpGet]
        public ActionResult HubAllocationDetail()
        {
            //IProjectCodeAllocationService _projectcCodeAllocationService = new ProjectCodeAllocationService();
            //var hubAllocated = _projectcCodeAllocationService.GetAllRequisitionsInHubAllocation();
            var list = new List<ActionLink>();

            var hubAllocated = new List<Cats.Models.HubAllocation>
                                                               {
                                                                   new Cats.Models.HubAllocation {RequisitionID = 0, },
                                                                   new Cats.Models.HubAllocation {RequisitionID = 27319},
                                                                   new Cats.Models.HubAllocation {RequisitionID = 31703},
                                                                   new Cats.Models.HubAllocation {RequisitionID = 27268}
                                                               };
            foreach (var item in hubAllocated)
            {
                list.Add(new ActionLink
                {
                    Text = item.RequisitionID.ToString(),
                    Controller = "ProjectAllocation",
                    Action = "DispatchDetail",
                    Id = Convert.ToInt16(item.RequisitionID)
                });
            }
            ViewData["RequisitionList"] = list;
            //IProjectCodeAllocationService _dispatchAllocationDetailService = new ProjectCodeAllocationService();
            //List<Cats.Models.HubAllocation> hubAllocatedDetail = _dispatchAllocationDetailService.GetHubAllocation(t=>t.RequisitionID==0);
            var hubAllocatedDetail = new List<Cats.Models.HubAllocation>();
            hubAllocatedDetail.Add(new Cats.Models.HubAllocation 
            { 
                RequisitionID = 1, 
                Hub = new Cats.Models.Hub { HubId = 1, Name = "Combolcha" }, 
                ReliefRequisition = new Cats.Models.ReliefRequisition 
                { 
                    RequisitionNo = "2564", 
                    Round = 1, 
                    CommodityID = 2, 
                    RequisitionID = 1
                } 
            });
            return View(hubAllocatedDetail);
        }
       
        [HttpGet]
        public ActionResult ProjectCodeAllocation(int id)
        {
            // IProjectCodeAllocationService _projectcCodeAllocationService = new ProjectCodeAllocationService();
            List<ActionLink> list = new List<ActionLink>();
            //List<Cats.Models.HubAllocation> HubAllocated = _projectcCodeAllocationService.GetAllRequisitionsInHubAllocation();
            List<Cats.Models.HubAllocation> HubAllocated = new List<Cats.Models.HubAllocation>();
            HubAllocated.Add(new Cats.Models.HubAllocation { RequisitionID = 31637 });
            HubAllocated.Add(new Cats.Models.HubAllocation { RequisitionID = 27319 });
            HubAllocated.Add(new Cats.Models.HubAllocation { RequisitionID = 31703 });
            HubAllocated.Add(new Cats.Models.HubAllocation { RequisitionID = 27268 });
            foreach (var item in HubAllocated)
            {
                list.Add(new ActionLink
                {
                    Text = item.RequisitionID.ToString(),
                    Controller = "ProjectAllocation",
                    Action = "DispatchDetail",
                    Id = Convert.ToInt16(item.RequisitionID)
                });
            }
            ViewData["RequisitionList"] = list;
            //IProjectCodeAllocationService _projectcCodeAllocationService = new ProjectCodeAllocationService();
            string reqNumber = id.ToString();
            //var hubInfo = _projectCodeAllocationService.GetHubAllocation(t => t.HubAllocationID == id);

            var hubInfo = new List<Cats.Models.HubAllocation>();
            hubInfo.Add(new Cats.Models.HubAllocation
            {
                AllocationDate = System.DateTime.Now,
                Hub = new Cats.Models.Hub { HubId = 1, Name = "Combolcha" },
                ReliefRequisition = new Cats.Models.ReliefRequisition
                {
                    RequisitionNo = "2564",
                    Round = 1,
                    CommodityID = 2,
                    RequisitionID = 1
                }
            });
            var allocation = hubInfo.FirstOrDefault();
            if (allocation != null) ViewBag.HubName = allocation.Hub.Name;
            ViewBag.Requisition = hubInfo.FirstOrDefault().ReliefRequisition.RequisitionNo;

            Dictionary<string, string> project_List = new Dictionary<string, string>();
            project_List.Add("101", "WFP 1240.82/210 MT.");
            project_List.Add("100", "USAID 001");
            project_List.Add("83", "CHINA 5000");
            var selectList = new SelectList(project_List,
                      "Value", "Key"
                      ); 

            ViewBag.ProjectList = selectList;

            Dictionary<string, string> SI_List = new Dictionary<string, string>();
            SI_List.Add("89", "00017044.");
            SI_List.Add("93", "00017512B");
            SI_List.Add("95", "CHINA AID");
            var SI_selectList = new SelectList(SI_List,
                      "Value", "Key"
                      );
            ViewBag.SICodeList = SI_selectList;
            string req=hubInfo.FirstOrDefault().ReliefRequisition.RequisitionNo;
            //var hubAllocationID = _projectCodeAllocationService.GetHubAllocation(t => t.ReliefRequisition.RequisitionNo == req);
            List<HubAllocation> _HubAllocation = new List<HubAllocation>();
            _HubAllocation.Add(new HubAllocation
            {
                RequisitionID = 1,
                HubID = 1,
                HubAllocationID = 1
            });
            int hubID = _HubAllocation.FirstOrDefault().HubAllocationID;
            var detail = _projectCodeAllocationService.FindBy(t => t.HubAllocationID == 0);
            
            List<ProjectCodeAllocation> listHubAllocation = new List<ProjectCodeAllocation>();
            var input = (
                from item in detail
                select new ProjectCodeAllocation
            
            //listHubAllocation.Add(new ProjectCodeAllocation 
                        {
                            //RequisitionNumber = Convert.ToInt16(hubInfo.FirstOrDefault().ReliefRequisition.RequisitionNo),
                            //HubAllocationID = id,
                            //Hub = hubInfo.FirstOrDefault().Hub.Name,
                            //ProjectCodeID = null,
                            //Amount_Project = 0,
                            //SINumberID = null,
                            //Amount_SI = 0,
                            Input = new ProjectCodeAllocation.ProjectCodeAllocationInput()
                            {
                                
                                ProjectCodeID = item.ProjectCodeID,
                                SINumberID = item.SINumberID,
                                Amount_FromProject = item.Amount_Project,
                                Amount_FromSI = item.Amount_SI
                            }

                        });
            return View(input);
            //return View(listHubAllocation);
        }
        public ActionResult Add()
        {
            //var detail = _projectCodeAllocationService.FindBy(t => t.HubAllocation.ReliefRequisition.RequisitionNo == requisition.ToString());
            var input = (
                //from item in detail
                //         select 
                         new ProjectCodeAllocation
                         {
                             
                             Input = new ProjectCodeAllocation.ProjectCodeAllocationInput()
                             {
                                 ProjectCodeID = 1,
                                 SINumberID = 1,
                                 Amount_FromProject = 2342,
                                 Amount_FromSI = 4242
                             }

                         });

            return RedirectToAction("ProjectCodeAllocation", "ProjectAllocation", new {id=0});
        }
        [HttpPost]
        //public ActionResult Edit(List<DispatchAllocation.DispatchAllocationInput> input)
        public ActionResult Edit(ProjectCodeAllocation newData)
        {
            IProjectCodeAllocationService _projectcCodeAllocationService = new ProjectCodeAllocationService();
      
            Cats.Models.ProjectCodeAllocation Allocation = new Cats.Models.ProjectCodeAllocation { SINumberID = newData.SINumberID, 
                Amount_SI = newData.Amount_SI, ProjectCodeID = newData.ProjectCodeID, Amount_Project = newData.Amount_Project, 
                HubAllocationID = newData.HubAllocationID };
            _projectCodeAllocationService.Save(Allocation);

            
            return RedirectToAction("ProjectCodeAllocation", "ProjectAllocation", new { id = 0});
            //return View();
        }
        
    }
}