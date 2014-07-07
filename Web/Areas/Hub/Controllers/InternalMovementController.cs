using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Hub;
using Cats.Models.Hubs.ViewModels;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Common;
using Cats.Web.Hub;

namespace Cats.Areas.Hub.Controllers
{
    public class InternalMovementController : BaseController
    {

        private readonly IUserProfileService _userProfileService;
        private readonly IInternalMovementService _internalMovementService;
        private readonly ITransactionService _transactionService;
        private readonly IStoreService _storeService;
        private readonly IProjectCodeService _projectCodeService;
        private readonly IShippingInstructionService _shippingInstructionService;
        private readonly ICommodityService _commodityService;
        private readonly IHubService _hubService;
        private readonly IProgramService _programService;
        private readonly IUnitService _unitService;
        private readonly IDetailService _detailService;
        //
        // GET: /InternalMovement/
        public InternalMovementController(IUserProfileService userProfileService, IInternalMovementService internalMovementService,
            ITransactionService transactionService, IStoreService storeService, IProjectCodeService projectCodeService,
            IShippingInstructionService shippingInstructionService, ICommodityService commodityService, IHubService hubService,
            IProgramService programService, IUnitService unitService, IDetailService detailService)
            : base(userProfileService)
        {
            _userProfileService = userProfileService;
            _internalMovementService = internalMovementService;
            _transactionService = transactionService;
            _storeService = storeService;
            _projectCodeService = projectCodeService;
            _shippingInstructionService = shippingInstructionService;
            _commodityService = commodityService;
            _hubService = hubService;
            _programService = programService;
            _unitService = unitService;
            _detailService = detailService;
        }

        public ActionResult Index()
        {
            return View(_internalMovementService.GetAllInternalMovmentLog().OrderByDescending(c => c.SelectedDate));
        }

        public ActionResult Create()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var fromStore = _hubService.GetAllStoreByUser(user);
            var commodities = _commodityService.GetAllParents();
            var programs = _programService.GetAllProgramsForReport();
            var units = _unitService.GetAllUnit();
            var toStore = _hubService.GetAllStoreByUser(user);
            var reasons = _detailService.GetReasonByMaster(Master.Constants.REASON_FOR_INTERNAL_MOVMENT);
            var viewModel = new InternalMovementViewModel(fromStore, commodities, programs, units, toStore, reasons);
            return View(viewModel);
        }
        public ActionResult EditPartial()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var fromStore = _hubService.GetAllStoreByUser(user);
            var commodities = _commodityService.GetAllParents();
            var programs = _programService.GetAllProgramsForReport();
            var units = _unitService.GetAllUnit();
            var toStore = _hubService.GetAllStoreByUser(user);
            var reasons = _detailService.GetReasonByMaster(Master.Constants.REASON_FOR_INTERNAL_MOVMENT);
            var viewModel = new InternalMovementViewModel(fromStore, commodities, programs, units, toStore, reasons);
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EditPartial(InternalMovementViewModel viewModel)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var fromStore = _hubService.GetAllStoreByUser(user);
            var commodities = _commodityService.GetAllParents();
            var programs = _programService.GetAllProgramsForReport();
            var units = _unitService.GetAllUnit();
            var toStore = _hubService.GetAllStoreByUser(user);
            var reasons = _detailService.GetReasonByMaster(Master.Constants.REASON_FOR_INTERNAL_MOVMENT);
            var newViewModel = new InternalMovementViewModel(fromStore, commodities, programs, units, toStore, reasons);
            if (viewModel.QuantityInMt > _transactionService.GetCommodityBalanceForStack(viewModel.FromStoreId, viewModel.FromStackId, viewModel.CommodityId, viewModel.ShippingInstructionId, viewModel.ProjectCodeId))
            {
                ModelState.AddModelError("QuantityInMt", "you dont have sufficent ammout to transfer");
                return View(newViewModel);
            }
            if (viewModel.QuantityInMt <= 0)
            {
                ModelState.AddModelError("QuantityInMt", "You have nothing to transfer");
                return View(newViewModel);
            }

           // _internalMovementService.AddNewInternalMovement(viewModel, user);
            _transactionService.SaveInternalMovementTrasnsaction(viewModel, user);
            return RedirectToAction("Index", "InternalMovement");
        }

        [HttpPost]
        public ActionResult Create(InternalMovementViewModel viewModel)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var fromStore = _hubService.GetAllStoreByUser(user);
            var commodities = _commodityService.GetAllParents();
            var programs = _programService.GetAllProgramsForReport();
            var units = _unitService.GetAllUnit();
            var toStore = _hubService.GetAllStoreByUser(user);
            var reasons = _detailService.GetReasonByMaster(Master.Constants.REASON_FOR_INTERNAL_MOVMENT);
            var newViewModel = new InternalMovementViewModel(fromStore, commodities, programs, units, toStore, reasons);
            if (viewModel.QuantityInMt > _transactionService.GetCommodityBalanceForStack(viewModel.FromStoreId, viewModel.FromStackId, viewModel.CommodityId, viewModel.ShippingInstructionId, viewModel.ProjectCodeId))
            {
                ModelState.AddModelError("QuantityInMt", "you dont have sufficent ammout to transfer");
                return View(newViewModel);
            }
            if (viewModel.QuantityInMt <= 0)
            {
                ModelState.AddModelError("QuantityInMt", "You have nothing to transfer");
                return View(newViewModel);
            }
            _transactionService.SaveInternalMovementTrasnsaction(viewModel,user);
          //  _internalMovementService.AddNewInternalMovement(viewModel, user);
            return RedirectToAction("Index", "InternalMovement");
        }

        public ActionResult IsQuantityValid(decimal QuantityInMt, int? FromStoreId, int? FromStackId, int? CommodityId, int? ShippingInstructionId, int? ProjectCodeId)
        {
            bool result = true;
            if (FromStackId.HasValue)
            {
                if (FromStoreId.HasValue && CommodityId.HasValue && ShippingInstructionId.HasValue && ProjectCodeId.HasValue)
                {

                    if ((QuantityInMt > _transactionService.GetCommodityBalanceForStack(FromStoreId.Value, FromStackId.Value, CommodityId.Value, ShippingInstructionId.Value, ProjectCodeId.Value)))
                    {
                        result = false;
                    }

                }
                else
                {
                    result = false;
                }
            }
            else
            {
                if (FromStoreId.HasValue && CommodityId.HasValue && ShippingInstructionId.HasValue && ProjectCodeId.HasValue)
                {

                    if ((QuantityInMt > _transactionService.GetCommodityBalanceForStack2(FromStoreId.Value, CommodityId.Value, ShippingInstructionId.Value, ProjectCodeId.Value)))
                    {
                        result = false;
                    }

                }
                else
                {
                    result = false;
                }
            }
            return (Json(result, JsonRequestBehavior.AllowGet));
        }

        public ActionResult GetStacksForFromStore(int? FromStoreId, int? SINumber)
        {
            if (FromStoreId.HasValue && SINumber.HasValue)
            {
                return Json(new SelectList(_storeService.GetStacksWithSIBalance(FromStoreId.Value, SINumber.Value), JsonRequestBehavior.AllowGet));
            }
            else
            {
                return Json(new SelectList(new List<ShippingInstructionViewModel>()));
            }
        }

        public ActionResult GetStacksForToStore(int? ToStoreId, int? FromStoreId, int? FromStackId)
        {
            if (ToStoreId.HasValue && FromStackId.HasValue && FromStoreId.HasValue)
            {
                return Json(new SelectList(_storeService.GetStacksByToStoreIdFromStoreIdFromStack(ToStoreId.Value, FromStoreId.Value, FromStackId.Value), JsonRequestBehavior.AllowGet));
            }
            else
            {
                return Json(new SelectList(new List<ShippingInstructionViewModel>()));
            }
        }

        public ActionResult GetProjecCodetForCommodity(int? CommodityId)
        {
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            var projectCodes = _projectCodeService.GetProjectCodesForCommodity(user.DefaultHub.Value, CommodityId.Value);
            return Json(new SelectList(projectCodes, "ProjectCodeId", "ProjectName"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSINumberForProjectCode(int? ProjectCodeId,int?parentCommodityId)
        {
            if (ProjectCodeId.HasValue)
            {
                UserProfile user = _userProfileService.GetUser(User.Identity.Name);
                return Json(new SelectList(_shippingInstructionService.GetShippingInstructionsForProjectCode(user.DefaultHub.Value, ProjectCodeId.Value,parentCommodityId.Value), "ShippingInstructionId", "ShippingInstructionName"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SelectList(new List<ShippingInstructionViewModel>()));
            }

        }

        public ActionResult GetFromStoreForParentCommodity(int? commodityParentId, int? SINumber)
        {
            if (commodityParentId.HasValue && SINumber.HasValue)
            {
                UserProfile user = _userProfileService.GetUser(User.Identity.Name);
                return Json(new SelectList(ConvertStoreToStoreViewModel(_storeService.GetStoresWithBalanceOfCommodityAndSINumber(commodityParentId.Value, SINumber.Value, user.DefaultHub.Value)), "StoreId", "StoreName"));
            }
            else
            {
                return Json(new SelectList(new List<StoreViewModel>()));
            }
        }

        public ActionResult ViewDetial(string TransactionId)
        {
            var internalMovment = _internalMovementService.GetAllInternalMovmentLog().FirstOrDefault(c => c.TransactionId == Guid.Parse(TransactionId));
            return PartialView(internalMovment);
        }

        public ActionResult SINumberBalance(int? parentCommodityId, int? projectcode, int? SINumber, int? StoreId, int? StackId)
        {
            StoreBalanceViewModel viewModel = new StoreBalanceViewModel();
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            if (!StoreId.HasValue && !StackId.HasValue && parentCommodityId.HasValue && projectcode.HasValue && SINumber.HasValue)
            {
                viewModel.ParentCommodityNameB = _commodityService.FindById(parentCommodityId.Value).Name;
                viewModel.ProjectCodeNameB = _projectCodeService.FindById(projectcode.Value).Value;
                viewModel.ShppingInstructionNumberB = _shippingInstructionService.FindById(SINumber.Value).Value;
                viewModel.QtBalance = _transactionService.GetCommodityBalanceForHub(user.DefaultHub.Value, parentCommodityId.Value, SINumber.Value, projectcode.Value);
            }
            else if (StoreId.HasValue && !StackId.HasValue && parentCommodityId.HasValue && projectcode.HasValue && SINumber.HasValue)
            {
                viewModel.ParentCommodityNameB = _commodityService.FindById(parentCommodityId.Value).Name;
                viewModel.ProjectCodeNameB = _projectCodeService.FindById(projectcode.Value).Value;
                viewModel.ShppingInstructionNumberB = _shippingInstructionService.FindById(SINumber.Value).Value;
                viewModel.QtBalance = _transactionService.GetCommodityBalanceForStore(StoreId.Value, parentCommodityId.Value, SINumber.Value, projectcode.Value);
                var store = _storeService.FindById(StoreId.Value);
                viewModel.StoreNameB = string.Format("{0} - {1}", store.Name, store.StoreManName);
            }

            else if (StoreId.HasValue && StackId.HasValue && parentCommodityId.HasValue && projectcode.HasValue && SINumber.HasValue)
            {
                viewModel.ParentCommodityNameB = _commodityService.FindById(parentCommodityId.Value).Name;
                viewModel.ProjectCodeNameB = _projectCodeService.FindById(projectcode.Value).Value;
                viewModel.ShppingInstructionNumberB = _shippingInstructionService.FindById(SINumber.Value).Value;
                viewModel.QtBalance = _transactionService.GetCommodityBalanceForStack(StoreId.Value, StackId.Value, parentCommodityId.Value, SINumber.Value, projectcode.Value);
                var store = _storeService.FindById(StoreId.Value);
                viewModel.StoreNameB = string.Format("{0} - {1}", store.Name, store.StoreManName);
                viewModel.StackNumberB = StackId.Value.ToString();
            }

            return View("SINumberBalance", viewModel);
        }


        IEnumerable<StoreViewModel> ConvertStoreToStoreViewModel(IEnumerable<Store> Stores)
        {
            var viewModel = new List<StoreViewModel>();
            foreach (var store in Stores)
            {
                viewModel.Add(new StoreViewModel { StoreId = store.StoreID, StoreName = string.Format("{0} - {1} ", store.Name, store.StoreManName) });
            }

            return viewModel;
        }

    }
}
