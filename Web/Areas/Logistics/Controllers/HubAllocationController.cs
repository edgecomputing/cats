using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Helpers;

namespace Cats.Areas.Logistics.Controllers
{
    public class HubAllocationController : Controller
    {
        //
        // GET: /Logistics/HubAllocation/

        
        private IReliefRequisitionDetailService _reliefRequisitionDetailService;
        private IHubService _hubService;
        public HubAllocationController(IReliefRequisitionDetailService reliefRequisitionDetailService,
            IHubService hubService)
        {
            this._hubService = hubService;
            this._reliefRequisitionDetailService = reliefRequisitionDetailService;
            
        }



        public ActionResult ApprovedRequesitions()
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

            ICollection<ReliefRequisitionDetail> listOfRequsitions=null;
            ReliefRequisitionDetail[] _requisitionDetail;

           _requisitionDetail = requisitionDetail.ToArray();

            var _chkValue = _Form["chkApprovedRequests"]; // for this code the _chkValue will return all value of each checkbox that is checked


            if (_chkValue != null)
            {

                string[] _arrChkValue = _Form["chkApprovedRequests"].ToString().Split(',');

                for (int i = 0; i < _arrChkValue.Length; i++)
                {
                    var _value = _arrChkValue[i]; // 
                    listOfRequsitions.Add(_requisitionDetail[int.Parse(_value)]);
                }
            }

            return View(requisitionDetail);
        }

      

        
       
    }
}
