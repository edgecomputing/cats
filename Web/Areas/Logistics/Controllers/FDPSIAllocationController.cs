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

       

        public ActionResult Index(int regionId=0)
        {
            
            return View();
        }
        public ActionResult DragDrop(int regionId = 0)
        {

            return View();
        }
        
    }
}
