using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Cats.Models.ViewModels;
using Cats.Helpers;
using System.Threading;


namespace Cats.Areas.Logistics.Controllers
{
    public class HubAllocationController : Controller
    {
        //
        // GET: /Logistics/HubAllocation/

        
        private IReliefRequisitionDetailService _reliefRequisitionDetailService;
        private IHubService _hubService;
        
        private IHubAllocationService _hubAllocationService;
        public HubAllocationController(IReliefRequisitionDetailService reliefRequisitionDetailService,IHubService hubService,
           IHubAllocationService hubAllocationService)
        {
            this._hubService = hubService;
            this._reliefRequisitionDetailService = reliefRequisitionDetailService;
            this._hubAllocationService = hubAllocationService;
        }


        [HttpGet]
        public JsonResult getApprovedRequisitions()
        {
            var reliefRequisitions = _reliefRequisitionDetailService.Get(null, null, "ReliefRequisition,Donor");
            var result = new List<ReliefRequisitionsViewModel>();

            foreach (var item in reliefRequisitions.ToList())
            {
                var data = new ReliefRequisitionsViewModel();
                data.CommodityName = item.Commodity.Name;
                data.Region = item.ReliefRequisition.AdminUnit1.Name;
                data.Zone = item.ReliefRequisition.AdminUnit1.Name;
                data.Round = item.ReliefRequisition.Round.Value;
                data.RequistionNo = item.ReliefRequisition.RequisitionNo;
                data.Amount = item.Amount;
                data.Beneficiaries = item.BenficiaryNo;
                result.Add(data);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult ApprovedRequesitions()
        {
            
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            return View();
        
        }

        public ActionResult Request(ICollection<ReliefRequisitionDetail> requisitionDetail)
        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            var reliefRequisitions = _reliefRequisitionDetailService.Get(null, null, "ReliefRequisition,Donor");
            return View(reliefRequisitions.ToList());

        }


        [HttpPost]
        public ActionResult hubAllocation(ICollection<ReliefRequisitionDetail> requisitionDetail, FormCollection _Form)
        {
            ViewBag.Hubs = new SelectList(_hubService.GetAllHub(), "HubID", "Name");
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");

            ICollection<ReliefRequisitionDetail> listOfRequsitions = new List<ReliefRequisitionDetail>();
            ReliefRequisitionDetail[] _requisitionDetail;

           _requisitionDetail = requisitionDetail.ToArray();

           var _chkValue = _Form["IsChecked"]; // for this code the _chkValue will return all value of each checkbox that is checked

            
            if (_chkValue != null)
            {

                string[] _arrChkValue = _Form["IsChecked"].ToString().Split(',');

                for (int i = 0; i < _arrChkValue.Length; i++)
                {
                    var _value = _arrChkValue[i]; 
                    listOfRequsitions.Add(_requisitionDetail[int.Parse(_value)]);
                }
            }

            return View(listOfRequsitions);
        }


        public void inserRequisition(ICollection<ReliefRequisitionDetail> requisitionDetail, FormCollection _Form, string datepicker, string rNumber)
        {

            string hub = _Form["hub"].ToString();

            foreach (ReliefRequisitionDetail appRequisition in requisitionDetail)
            {
                HubAllocation new_hub_allocation = new HubAllocation();

                new_hub_allocation.AllocatedBy = appRequisition.CommodityID;
                new_hub_allocation.RequisitionID = appRequisition.RequisitionID;
                new_hub_allocation.AllocationDate = DateTime.Now;
                new_hub_allocation.HubID = int.Parse(hub);
                new_hub_allocation.AllocatedBy = 1;

                _hubAllocationService.AddHubAllocation(new_hub_allocation);
                _hubAllocationService.UpdateRequisitionStatus(appRequisition.ReliefRequisition.RequisitionNo);
            }
        }
        
       
    }
}
