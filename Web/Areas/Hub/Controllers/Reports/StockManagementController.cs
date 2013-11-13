using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Services.Hub;
using Cats.Models.Hubs.ViewModels.Common;
using Cats.Models.Hubs.ViewModels.Report;
using Cats.Web.Hub;

//using DevExpress.XtraRichEdit.Utils;

namespace Cats.Areas.Hub.Controllers.Reports
{
    
    public class StockManagementController : BaseController
    {
        
        private readonly IUserProfileService _userProfileService;
        private readonly IProgramService _programService;
        private readonly ICommodityTypeService _commodityTypeService;
        private readonly ICommoditySourceService _commoditySourceService;
        private readonly IProjectCodeService _projectCodeService;
        private readonly IShippingInstructionService _shippingInstructionService;
        private readonly IReceiveService _receiveService;
       
        private readonly IStoreService _storeService;
        private readonly IHubService _hubService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IDispatchAllocationService _dispatchAllocationService;
        private readonly IDonorService _donorService;
        //
        // GET: /StockManagement/
        public StockManagementController(IUserProfileService userProfileService, IProgramService programService,
            ICommodityTypeService commodityTypeService, ICommoditySourceService commoditySourceService, 
            IProjectCodeService projectCodeService, IShippingInstructionService shippingInstructionService, 
            IReceiveService receiveService, IStoreService storeService, IHubService hubService,
            IAdminUnitService adminUnitService, IDispatchAllocationService dispatchAllocationService,
            IDonorService donorService)
            : base(userProfileService)
        {
            _userProfileService = userProfileService;
            _programService = programService;
            _commodityTypeService = commodityTypeService;
            _commoditySourceService = commoditySourceService;
            _projectCodeService = projectCodeService;
            _shippingInstructionService = shippingInstructionService;
            _receiveService = receiveService;
           
            _storeService = storeService;
            _hubService = hubService;
            _adminUnitService = adminUnitService;
            _dispatchAllocationService = dispatchAllocationService;
            _donorService = donorService;
        }

        /// <summary>
        /// Show the ETA, MT Expected, MT + % that has arrived, MT + % still to arrive
        /// </summary>
        /// <returns></returns>
        public ActionResult ArrivalsVsReceipts()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var commoditySourceViewModels = _commoditySourceService.GetAllCommoditySourceForReport();
            var portViewModels = _receiveService.GetALlPorts();
            var codesViewModels = ConstantsService.GetAllCodes();
            var commodityTypeViewModels = _commodityTypeService.GetAllCommodityTypeForReprot();
            var programViewModels = _programService.GetAllProgramsForReport();
            var viewModel = new ArrivalsVsReceiptsViewModel(commoditySourceViewModels, portViewModels, codesViewModels,
                commodityTypeViewModels, programViewModels, user);
          
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ArrivalsVsReceiptsReport(ArrivalsVsReceiptsViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            ViewBag.CommoditySources = viewModel.CommoditySourceId == 0 ? "All" : _commoditySourceService.GetAllCommoditySource().Where(c => c.CommoditySourceID == viewModel.CommoditySourceId).Select(c => c.Name).Single();
            
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Receipts()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var commoditySourceViewModels = _commoditySourceService.GetAllCommoditySourceForReport();
            var portViewModels = _receiveService.GetALlPorts();
            var codesViewModels = ConstantsService.GetAllCodes();
            var commodityTypeViewModels = _commodityTypeService.GetAllCommodityTypeForReprot();
            var programViewModels = _programService.GetAllProgramsForReport();
            var storeViewModel = _hubService.GetAllStoreByUser(user);
            var viewModel = new ReceiptsViewModel(codesViewModels, commodityTypeViewModels, programViewModels,
                storeViewModel, commoditySourceViewModels, portViewModels);

            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReceiptsReport(ReceiptsViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            ViewBag.CommoditySources = viewModel.CommoditySourceId == 0 ? "All" : _commoditySourceService.GetAllCommoditySource().Where(c => c.CommoditySourceID == viewModel.CommoditySourceId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult StockBalance()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var codesViewModels = ConstantsService.GetAllCodes();
            var commodityTypeViewModels = _commodityTypeService.GetAllCommodityTypeForReprot();
            var programViewModels = _programService.GetAllProgramsForReport();
            var storeViewModel = _hubService.GetAllStoreByUser(user);
            var viewModel = new StockBalanceViewModel(codesViewModels, commodityTypeViewModels, programViewModels, storeViewModel);

            
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult StockBalanceReport(StockBalanceViewModel viewModel)
        {

            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView() ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Dispatches()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var codesViewModels = ConstantsService.GetAllCodes();
            var commodityTypeViewModels = _commodityTypeService.GetAllCommodityTypeForReprot();
            var programViewModels = _programService.GetAllProgramsForReport();
            var storeViewModels = _hubService.GetAllStoreByUser(user);
            var areaViewModels = _adminUnitService.GetAllAreasForReport();
            var bidRefViewModels = _dispatchAllocationService.GetAllBidRefsForReport();
            var viewModel = new DispatchesViewModel( codesViewModels, commodityTypeViewModels, programViewModels, storeViewModels,
                areaViewModels, bidRefViewModels);
            
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DispatchesReport(DispatchesViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CommittedVsDispatched()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var storeViewModel = _hubService.GetAllStoreByUser(user);
            var areas = _adminUnitService.GetAllAreasForReport();
            var codes = ConstantsService.GetAllCodes();
            var commodityTypes = _commodityTypeService.GetAllCommodityTypeForReprot();
            var programs = _programService.GetAllProgramsForReport();
            var viewModel = new CommittedVsDispatchedViewModel(storeViewModel, areas, codes, commodityTypes, programs, user);
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult CommittedVsDispatchedReport(CommittedVsDispatchedViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult InTransitReprot(InTransitViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult InTransit()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var codes = ConstantsService.GetAllCodes();
            var commodityTypes = _commodityTypeService.GetAllCommodityTypeForReprot();
            var programs = _programService.GetAllProgramsForReport();
            var stores = _hubService.GetAllStoreByUser(user);
            var areas = _adminUnitService.GetAllAreasForReport();
            var types = ConstantsService.GetAllTypes();
            var viewModel = new InTransitViewModel(codes, commodityTypes, programs, stores, areas, types);
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DeliveryAgainstDispatch()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var stores = _hubService.GetAllStoreByUser(user);
            var areas = _adminUnitService.GetAllAreasForReport();
            var codes = ConstantsService.GetAllCodes();
            var commodityTypes = _commodityTypeService.GetAllCommodityTypeForReprot();
            var programs = _programService.GetAllProgramsForReport();
            var types = ConstantsService.GetAllTypes();
            var viewModel = new DeliveryAgainstDispatchViewModel(stores, areas, codes, commodityTypes, programs, types);
            
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DeliveryAgainstDispatchReport(DeliveryAgainstDispatchViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DistributionDeliveryDispatch()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var stores = _hubService.GetAllStoreByUser(user);
            var areas = _adminUnitService.GetAllAreasForReport();
            var codes = ConstantsService.GetAllCodes();
            var commodityTypes = _commodityTypeService.GetAllCommodityTypeForReprot();
            var programs = _programService.GetAllProgramsForReport();
            var viewModel = new DistributionDeliveryDispatchViewModel(codes, commodityTypes, programs, stores, areas);
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DistributionDeliveryDispatchReport(DistributionDeliveryDispatchViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DistributionByOwner()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var stores = _hubService.GetAllStoreByUser(user);
            var areas = _adminUnitService.GetAllAreasForReport();
            var codes = ConstantsService.GetAllCodes();
            var commodityTypes = _commodityTypeService.GetAllCommodityTypeForReprot();
            var programs = _programService.GetAllProgramsForReport();
            var sourceDonors = _donorService.GetAllSourceDonorForReport();
            var responsibleDonors = _donorService.GetAllResponsibleDonorForReport();
            var viewModel = new DistributionByOwnerViewModel(codes, commodityTypes, programs, stores, areas, sourceDonors,
                responsibleDonors);
           
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DistributionByOwnerReport(DistributionByOwnerViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }



        // partial views 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectCode()
        {
            ViewBag.ProjectCode = new SelectList(_projectCodeService.GetAllProjectCodeForReport(), "ProjectCodeId", "ProjectName");
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ShippingInstruction()
        {
            ViewBag.ShippingInstruction = new SelectList(_shippingInstructionService.GetAllShippingInstructionForReport(), "ShippingInstructionId", "ShippingInstructionName");
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AreaPartial()
        {
            
            return PartialView();
        }

        public ActionResult CustomeDate()
        {
            return PartialView();
        }

        public ActionResult MonthToDate()
        {
            return PartialView();
        }
      
    }
}
