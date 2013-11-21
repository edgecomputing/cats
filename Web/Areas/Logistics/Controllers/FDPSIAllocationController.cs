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
using Cats.Services.Logistics;

namespace Cats.Areas.Logistics.Controllers
{
    public class FDPSIAllocationController : Controller
    {
        private readonly IReliefRequisitionService _requisitionService;
        private readonly ILedgerService _ledgerService;
        private readonly IHubAllocationService _hubAllocationService;
        private readonly IProjectCodeAllocationService _projectCodeAllocationService;
        private readonly IHubService _hubService;
        private readonly ISIPCAllocationService _allocationService;
        public FDPSIAllocationController
            (
             IReliefRequisitionService requisitionService
            , ILedgerService ledgerService
            , IHubAllocationService hubAllocationService
            , IProjectCodeAllocationService projectCodeAllocationService
            , IHubService hubService
            ,ISIPCAllocationService allocationService
            )
            {
                this._requisitionService = requisitionService;
                this._ledgerService = ledgerService;
                this._hubAllocationService = hubAllocationService;
                this._projectCodeAllocationService = projectCodeAllocationService;
                this._hubService = hubService;
                this._allocationService = allocationService;
            }

        public List<RequestAllocationViewModel> getIndexList(int regionId = 0,int RequisitionID=0)
        {
            List<ReliefRequisition> req;// = _requisitionService.FindBy(r => r.RegionID == regionId && r.Status == (int)ReliefRequisitionStatus.HubAssigned);
            
            if(RequisitionID>0)
            {
                req = _requisitionService.FindBy(r => r.RequisitionID==RequisitionID && r.RegionID == regionId && r.Status == (int)ReliefRequisitionStatus.HubAssigned);
            }
            else
            {
               req = _requisitionService.FindBy(r => r.RegionID == regionId && r.Status == (int)ReliefRequisitionStatus.HubAssigned);

            }
            var result = req.ToList().Select(item => new RequestAllocationViewModel
            {
                Commodity = item.Commodity.Name,
                RegionName = item.AdminUnit.Name,
                ZoneName = item.AdminUnit1.Name,
                RequisitionId = item.RequisitionID,
                CommodityId = (int)item.CommodityID,
                Amount = item.ReliefRequisitionDetails.Sum(a => a.Amount),
                HubAllocationID = item.HubAllocations.ToList()[0].HubAllocationID,
                HubName = item.HubAllocations.ToList()[0].Hub.Name,
           //     SIAllocations=item.ReliefRequisitionDetails.RegionalRequestDetails.First().Fdpid
//                AllocatedAmount = geteAllocatedAmount(item),
 //               SIAllocations = getSIAllocations(item.HubAllocations.ToList()[0].ProjectCodeAllocations.ToList()),
                FDPRequests=getRequestDetail(item),
                FreeSIPCCodes = getSIPCLists(item.RequisitionID, (int)item.CommodityID),

            }).ToList();

            return result;
        }
        public List<FDPRequestViewModel> getRequestDetail(ReliefRequisition Request)
        {

            var result = Request.ReliefRequisitionDetails.ToList().Select(item => new FDPRequestViewModel
            {
                RequisitionId=Request.RequisitionID,
                RequestDetailId = item.RequisitionDetailID,
                FDPId=item.FDPID,
                FDPName=item.FDP.Name,
                Name = item.FDP.Name,
                RequestedAmount=item.Amount,
                WoredaId=item.FDP.AdminUnitID,
                WoredaName=item.FDP.AdminUnit.Name,
                Commodity = item.Commodity.Name,
                Allocations = ToAllocationViewModel(item.SIPCAllocations.ToList())
            }).ToList();
            return result;
        }
        public List<SIPCAllocationViewModel> ToAllocationViewModel( List<SIPCAllocation> allocation)
        {
            var result = allocation.Select(item => new SIPCAllocationViewModel
            {
                SIPCAllocationID=item.ISPCAllocationID,
                Code=item.Code,
                AllocatedAmount=item.AllocatedAmount,
                AllocationType=item.AllocationType,
                RequisitionDetailID = item.RequisitionDetailID
            }).ToList();
            return result;
        }
        public FreeSIPC getSIPCLists(int reqId, int CommodityID)
        {
            var hubId = 0;// _hubAllocationService.GetAllocatedHubId(reqId);
            List<LedgerService.AvailableShippingCodes> freeSICodes = _ledgerService.GetFreeSICodesByCommodity(hubId, CommodityID);
            List<LedgerService.AvailableProjectCodes> freePCCodes = _ledgerService.GetFreePCCodesByCommodity(hubId, CommodityID);
            FreeSIPC free = new FreeSIPC { FreePCCodes = freePCCodes, FreeSICodes = freeSICodes };
            return free;
        }
        public ActionResult Index(int regionId = 0, int RequisitionID = 0)
        {
            ViewBag.regionId = regionId;
            ViewBag.RequisitionID = RequisitionID;
            ViewBag.Hubs = _hubService.GetAllHub();
            ViewBag.Allocations = _allocationService.GetAll();
            return View();
        }

        public ActionResult getRequisitionList(int regionId = 0, int RequisitionID = 0)
        {
            List<RequestAllocationViewModel> list = getIndexList(regionId, RequisitionID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult updateRequisitionStatus(int RequisitionId)
        {
            ReliefRequisition req = _requisitionService.FindById(RequisitionId);
            req.Status = 4;
            _requisitionService.EditReliefRequisition(req);
            List<RequestAllocationViewModel> list = new List<RequestAllocationViewModel> ();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
    }
}
