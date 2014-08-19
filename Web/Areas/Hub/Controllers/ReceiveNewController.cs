using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Hub.Models;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Areas.Hub.Controllers
{
    public class ReceiveNewController : BaseController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IReceiptAllocationService _receiptAllocationService;
        private readonly IReceiveService _receiveService;
        private readonly ICommodityService _commodityService;
        private readonly IUnitService _unitService;
        private readonly IStoreService _storeService;
        private readonly ITransactionService _transactionService;
        private Guid _receiptAllocationId;

        public ReceiveNewController(IUserProfileService userProfileService,
            IReceiptAllocationService receiptAllocationService,
            IReceiveService receiveService,
            ICommodityService commodityService,
            IUnitService unitService,
            IStoreService storeService,
            ITransactionService transactionService)
            : base(userProfileService)
        {
            _userProfileService = userProfileService;
            _receiptAllocationService = receiptAllocationService;
            _receiveService = receiveService;
            _commodityService = commodityService;
            _unitService = unitService;
            _storeService = storeService;
            _transactionService = transactionService;
        }


        public ActionResult Create(string receiptAllocationId)
        {
            if (String.IsNullOrEmpty(receiptAllocationId)) return View();
            _receiptAllocationId = Guid.Parse(receiptAllocationId);

            var receiptAllocation = _receiptAllocationService.FindById(_receiptAllocationId);

            var user = _userProfileService.GetUser(User.Identity.Name);

            if (receiptAllocation != null)
            {
                if (user.DefaultHub != null && receiptAllocation.HubID == user.DefaultHub.Value)
                {
                    var viewModel = _receiveService.ReceiptAllocationToReceive(receiptAllocation);
                    viewModel.CurrentHub = user.DefaultHub.Value;

                    var commodities = _commodityService.GetAllCommodityViewModelsByParent(receiptAllocation.CommodityID);
                    ViewData["commodities"] = commodities;
                    ViewData["units"] = _unitService.GetAllUnitViewModels();
                    return View(viewModel);
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult Create(ReceiveNewViewModel viewModel)
        {
            //Todo: change to support multiple receive detail 

            var receiptAllocation = _receiptAllocationService.FindById(viewModel.ReceiptAllocationId);

            #region Fix to ModelState

            switch (viewModel.CommoditySourceTypeId)
            {
                case CommoditySource.Constants.DONATION:
                    ModelState.Remove("SourceHub");
                    ModelState.Remove("SupplierName");
                    ModelState.Remove("PurchaseOrder");
                    break;
                case CommoditySource.Constants.LOCALPURCHASE:
                    ModelState.Remove("SourceHub");
                    break;
                default:
                    ModelState.Remove("DonorID");
                    ModelState.Remove("ResponsibleDonorID");
                    ModelState.Remove("SupplierName");
                    ModelState.Remove("PurchaseOrder");
                    break;
            }

            #endregion

            if (!ModelState.IsValid) return View(viewModel);

            //check if the detail are not null 
            if (viewModel.ReceiveDetailNewViewModel != null)
            {
                #region GRN validation

                if (!_receiveService.IsGrnUnique(viewModel.Grn))
                {
                    ModelState.AddModelError("GRN", @"GRN already existed");
                    PopulateCombox(receiptAllocation.CommodityID);
                    return View(viewModel);
                }

                #endregion

                #region Validate receive amount 

                if (_receiveService.IsReceiveExcedeAllocation(viewModel.ReceiveDetailNewViewModel,
                    viewModel.ReceiptAllocationId))
                {
                    ModelState.AddModelError("ReceivedId", "Hey you are trying to receive more than allocated");
                    PopulateCombox(receiptAllocation.CommodityID);
                    return View(viewModel);
                }

                #endregion

                //Save transaction 
                _transactionService.ReceiptTransaction(viewModel);

                return RedirectToAction("Index", "Receive");
            }
            else
            {
                ModelState.AddModelError("ReceiveDetails", "Please add at least one commodity");
            }
            PopulateCombox(receiptAllocation.CommodityID);
            return View(viewModel);
        }

        private void PopulateCombox(int parentCommodity)
        {
            var commodities = _commodityService.GetAllCommodityViewModelsByParent(parentCommodity);
            ViewData["commodities"] = commodities;
            ViewData["units"] = _unitService.GetAllUnitViewModels();
        }

        [HttpGet]
        public JsonResult GetStroes(int hubId)
        {
            return Json((from c in _storeService.GetStoreByHub(hubId) select new StoreViewModel
            {
                StoreId = c.StoreID,
                Name =  c.Name
            }), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetStacks(int? storeId)
        {
            if (storeId == null)
                return Json(new SelectList(Enumerable.Empty<StackViewModel>()), JsonRequestBehavior.AllowGet);
            var store = _storeService.FindById(storeId.Value);
            var stacks = new List<StackViewModel>();
            stacks.AddRange(store.Stacks.Select(i => new StackViewModel { Name = i.ToString(), Id = i }));
            return Json(stacks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AutoCompleteCommodity(string term)
        {
            var result = (from commodity in _commodityService.GetAllCommodity()
                          where commodity.Name.ToLower().StartsWith(term.ToLower())
                          select commodity.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void PopulateCommodities()
        {

        }

    }
}
