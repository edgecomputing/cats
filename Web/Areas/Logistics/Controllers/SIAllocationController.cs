using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.EarlyWarning;
using Cats.Services.Transaction;
using Cats.ViewModelBinder;
using log4net;
using Cats.Services.Common;
using Cats.Areas.Logistics.Models;

namespace Cats.Areas.Logistics.Controllers
{
    public class SIAllocationController : Controller
    {
        private IReliefRequisitionService _requisitionService;
        private ILedgerService _ledgerService;
        private IHubAllocationService _hubAllocationService;
        private IProjectCodeAllocationService _projectCodeAllocationService;
        public SIAllocationController
            (
             IReliefRequisitionService requisitionService
            , ILedgerService ledgerService
            , IHubAllocationService hubAllocationService
            , IProjectCodeAllocationService projectCodeAllocationService
            )
            {
                this._requisitionService = requisitionService;
                this._ledgerService = ledgerService;
                this._hubAllocationService = hubAllocationService;
                this._projectCodeAllocationService = projectCodeAllocationService;
            }

        public List<RequestAllocationViewModel> getIndexList(int regionId = 0)
        {
            List<ReliefRequisition> req = _requisitionService.FindBy(r => r.RegionID==regionId && r.Status == (int)ReliefRequisitionStatus.HubAssigned);
            var result = req.ToList().Select(item => new RequestAllocationViewModel
            {
               Commodity = item.Commodity.Name,
               RegionName = item.AdminUnit.Name,
                ZoneName = item.AdminUnit1.Name,
                RequisitionId = item.RequisitionID,
               CommodityId = (int)item.CommodityID,
                Amount = item.ReliefRequisitionDetails.Sum(a => a.Amount),
               HubAllocationID = item.HubAllocations.ToList()[0].HubAllocationID,
               HubName = item.HubAllocations.ToList()[0].Hub.Name
                ,SIAllocations = getSIAllocations(item.HubAllocations.ToList()[0].ProjectCodeAllocations.ToList())
                ,FreeSIPCCodes = getSIPCLists(item.RequisitionID,(int)item.CommodityID)
            }).ToList();
            return result;
        }
        public Cats.Models.ProjectCodeAllocation convertToEntityModel(AllocationAction allocationAction)
        {
            DateTime date;
           // DateTime.Now
            return new Cats.Models.ProjectCodeAllocation
            {
                SINumberID = allocationAction.ShippingInstructionId,
                Amount_FromSI = (int)allocationAction.AllocatedAmount,
                ProjectCodeAllocationID = allocationAction.AllocationId,
                AlloccationDate = DateTime.Now,
                AllocatedBy = 1,
                HubAllocationID = allocationAction.HubAllocationID
            };
        }
        public List<SIAllocation> getSIAllocations(List<Cats.Models.ProjectCodeAllocation> pcAllocations)
        {
            var result = pcAllocations.Where(item=>item.SINumberID!=null).Select(item => new SIAllocation
            {
                ShippingInstructionId=(int)item.SINumberID
                ,ShippingInstructionCode=item.ShippingInstruction.Value
                ,AllocatedAmount=(double)item.Amount_FromSI
                ,AllocationId=(int)item.ProjectCodeAllocationID
                ,AllocationType="SI"
            }).ToList();
            var pcresult = pcAllocations.Where(item => item.ProjectCodeID != null).Select(item => new SIAllocation
            {
                ShippingInstructionId = (int)item.ProjectCodeID,
                ShippingInstructionCode = item.ProjectCode.Value,
                AllocatedAmount = (double)item.Amount_FromProject,
                AllocationId = (int)item.ProjectCodeAllocationID,
                AllocationType = "PC"
            });
            return result.Union(pcresult).ToList();
        }
        public FreeSIPC getSIPCLists(int reqId, int CommodityID)
        {
            var hubId = _hubAllocationService.GetAllocatedHubId(reqId);
            List<LedgerService.AvailableShippingCodes> freeSICodes = _ledgerService.GetFreeSICodesByCommodity(hubId, CommodityID);
            List<LedgerService.AvailableProjectCodes> freePCCodes = _ledgerService.GetFreePCCodesByCommodity(hubId, CommodityID);
            FreeSIPC free = new FreeSIPC { FreePCCodes = freePCCodes, FreeSICodes = freeSICodes };
            return free;
        }
        //
        // GET: /Logistics/SIAllocation/

        public ActionResult Index(int regionId=0)
        {
            List<RequestAllocationViewModel> list = getIndexList();
            ViewBag.ReliefRequisitionList = list;//list[0].ReliefRequisitionDetails[0].;
            ViewBag.regionId = regionId;
            return View();
        }
        public JsonResult getList(int regionId=0)
        {
            List<RequestAllocationViewModel> list = getIndexList(regionId);
            return Json(list,JsonRequestBehavior.AllowGet);
        }
        public JsonResult updateAllocation(int regionId,List<AllocationAction> allocationAction)
        {
            foreach(AllocationAction aa in allocationAction)
            {
                if (aa.Action == "delete")
                {
                    _projectCodeAllocationService.DeleteById((int)aa.AllocationId);
                }
                else
                {
                    Cats.Models.ProjectCodeAllocation newProjectAllocation = convertToEntityModel(aa);
                    if (aa.AllocationType == "PC")
                    {
                        newProjectAllocation.Amount_FromProject = newProjectAllocation.Amount_FromSI;
                        newProjectAllocation.ProjectCodeID = newProjectAllocation.SINumberID;
                        newProjectAllocation.Amount_FromSI=0;
                        newProjectAllocation.SINumberID=null;
                    }
                    if (aa.Action == "add")
                    {
                        _projectCodeAllocationService.AddProjectCodeAllocation(newProjectAllocation, aa.RequisitionId, false);
                    }
                    else
                    {
                        _projectCodeAllocationService.EditProjectCodeAllocationDetail(newProjectAllocation);
                    }
                }
            }
            List<RequestAllocationViewModel> list = getIndexList(regionId);
            return Json(list, JsonRequestBehavior.AllowGet); 
        }
        public JsonResult http_getSIPCLists(int reqId, int CommodityID)
        {
            var hubId = _hubAllocationService.GetAllocatedHubId(reqId);
            List<LedgerService.AvailableShippingCodes> freeSICodes = _ledgerService.GetFreeSICodesByCommodity(hubId, CommodityID);
            List<LedgerService.AvailableProjectCodes> freePCCodes = _ledgerService.GetFreePCCodesByCommodity(hubId, CommodityID);
            FreeSIPC free = new FreeSIPC { FreePCCodes = freePCCodes, FreeSICodes = freeSICodes };
            return Json(free, JsonRequestBehavior.AllowGet);
        }

    }
}
