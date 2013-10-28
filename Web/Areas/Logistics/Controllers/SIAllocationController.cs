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

        public SIAllocationController(
                            IReliefRequisitionService requisitionService)
                            {
                                this._requisitionService = requisitionService;
                            }

        public List<RequestAllocationViewModel> getIndexList()
        {
            List<ReliefRequisition> req = _requisitionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.HubAssigned);
            var result = req.ToList().Select(item => new RequestAllocationViewModel
            {
               Commodity = item.Commodity.Name,
               RegionName = item.AdminUnit.Name,
                ZoneName = item.AdminUnit1.Name,
            //    RequisitionNo = item.RequisitionNo,
                RequisitionId = item.RequisitionID,
           //     Beneficiaries = item.ReliefRequisitionDetails.Sum(b => b.BenficiaryNo),
                Amount = item.ReliefRequisitionDetails.Sum(a => a.Amount)
                ,SIAllocations = getSIAllocations(item.HubAllocations.ToList()[0].ProjectCodeAllocations.ToList())
            //    Selected = true,
              //  Unit = unitPreference
            }).ToList();
            return result;
        }
        public List<SIAllocation> getSIAllocations(List<Cats.Models.ProjectCodeAllocation> pcAllocations)
        {
            var result = pcAllocations.Where(item=>item.SINumberID!=null).Select(item => new SIAllocation
            {
                ShippingInstructionId=(int)item.SINumberID
                ,ShippingInstructionCode=item.ShippingInstruction.Value
                ,AllocatedAmount=(double)item.Amount_FromSI
            }).ToList();
            return result;
        }
        //
        // GET: /Logistics/SIAllocation/

        public ActionResult Index()
        {
            List<RequestAllocationViewModel> list = getIndexList();
            ViewBag.ReliefRequisitionList = list;//list[0].ReliefRequisitionDetails[0].;
           // ViewBag.Requests = Js(list).;
            return View();
        }
        public JsonResult getList()
        {
            List<RequestAllocationViewModel> list = getIndexList();
            return Json(list,JsonRequestBehavior.AllowGet);
        }

    }
}
