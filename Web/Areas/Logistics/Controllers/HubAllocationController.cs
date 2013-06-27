using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.Logistics.Controllers
{
    public class HubAllocationController : Controller
    {
        //
        // GET: /Logistics/HubAllocation/
        private IReliefRequistionService _reliefRequistionService;

        public ActionResult Index()
        {
            var reliefrequistions = _reliefRequistionService.GetAllReliefRequistion();
            return View(reliefrequistions.ToList());
            
            
        }

    }
}
