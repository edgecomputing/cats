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
    public class FDPSIAllocationController : Controller
    {
        private IReliefRequisitionService _requisitionService;
        private ILedgerService _ledgerService;
        private IHubAllocationService _hubAllocationService;
        private IProjectCodeAllocationService _projectCodeAllocationService;
        public FDPSIAllocationController
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
   //             SIAllocations=item.RegionalRequest.RegionalRequestDetails.First().Fdpid
//                AllocatedAmount = geteAllocatedAmount(item),
 //               SIAllocations = getSIAllocations(item.HubAllocations.ToList()[0].ProjectCodeAllocations.ToList()),
                FDPRequests=getRequestDetail(item),
                FreeSIPCCodes = getSIPCLists(item.RequisitionID, (int)item.CommodityID),

            }).ToList();

            return result;
        }
        public List<FDPRequestViewModel> getRequestDetail(ReliefRequisition Request)
        {
            /*RequisitionId { get; set; }
        public int RequestDetailId { get; set; }
        public int FDPId { get; set; }
        public string FDPName { get; set; }
        public string Name { get; set; }
        public decimal RequestedAmount { get; set; }
        public int WoredaId { get; set; }
        public string WoredaName { get; set; }
             * */
           
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
                Commodity = item.Commodity.Name
            }).ToList();
            return result;
        }
        public FreeSIPC getSIPCLists(int reqId, int CommodityID)
        {
            var hubId = _hubAllocationService.GetAllocatedHubId(reqId);
            List<LedgerService.AvailableShippingCodes> freeSICodes = _ledgerService.GetFreeSICodesByCommodity(hubId, CommodityID);
            List<LedgerService.AvailableProjectCodes> freePCCodes = _ledgerService.GetFreePCCodesByCommodity(hubId, CommodityID);
            FreeSIPC free = new FreeSIPC { FreePCCodes = freePCCodes, FreeSICodes = freeSICodes };
            return free;
        }
        public ActionResult Index(int regionId = 0, int RequisitionID = 0)
        {
            ViewBag.regionId = regionId;
            ViewBag.RequisitionID = RequisitionID;
            return View();
        }

        public ActionResult getRequisitionList(int regionId = 0, int RequisitionID = 0)
        {
            List<RequestAllocationViewModel> list = getIndexList(regionId, RequisitionID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DragDrop(int regionId = 0)
        {

            return View();
        }
        
    }
}
