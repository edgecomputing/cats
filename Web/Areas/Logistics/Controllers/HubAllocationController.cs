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
        public ActionResult hubAllocation(ICollection<ReliefRequisitionDetail> requisitionDetail, int[] IsChecked)
        {
            ViewBag.Hubs = new SelectList(_hubService.GetAllHub(), "HubID","Name");
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");

            //get the id from each selected checkbox
            foreach (int item in IsChecked)
            {
                             
            }

          
            return View(requisitionDetail);
        }

        public void insert()
        {
            
        }

        
       
    }
}
