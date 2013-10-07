using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Newtonsoft.Json;
using Telerik.Web.Mvc;
using System;
using Cats.Models.Hub.ViewModels.Dispatch;

namespace Cats.Web.Hub.Controllers
{ 
    [Authorize]
    public class DispatchController : BaseController
    {
        
        private readonly IDispatchAllocationService _dispatchAllocationService;
        private readonly IDispatchService _dispatchService;
        private readonly IUserProfileService _userProfileService;
        private readonly IOtherDispatchAllocationService _otherDispatchAllocationService;
        private readonly IDispatchDetailService _dispatchDetailService;
        private readonly IUnitService _unitService;
        private readonly ICommodityTypeService _commodityTypeService;
        private readonly IProgramService _programService;
        private readonly ITransporterService _transporterService;
        private readonly IPeriodService _periodService;
        private readonly ICommodityService _commodityService;
        private readonly ITransactionService _transactionService;
        private readonly IStoreService _storeService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IHubService _hubService;
        private readonly IFDPService _fdpService;
        private readonly IProjectCodeService _projectCodeService;
        private readonly IShippingInstructionService _shippingInstructionService;

        public DispatchController(IDispatchAllocationService dispatchAllocationService, IDispatchService dispatchService,
            IUserProfileService userProfileService, IOtherDispatchAllocationService otherDispatchAllocationService,
            IDispatchDetailService dispatchDetailService, IUnitService unitService, ICommodityTypeService commodityTypeService,
            IProgramService programService, ITransporterService transporterService, IPeriodService periodService, 
            ICommodityService commodityService, ITransactionService transactionService, IStoreService storeService,
            IAdminUnitService adminUnitService, IHubService hubService, IFDPService fdpService,
            IProjectCodeService projectCodeService, IShippingInstructionService shippingInstructionService)
            : base(userProfileService)
        {
            _dispatchAllocationService = dispatchAllocationService;
            _dispatchService = dispatchService;
            _userProfileService = userProfileService;
            _otherDispatchAllocationService = otherDispatchAllocationService;
            _dispatchDetailService = dispatchDetailService;
            _unitService = unitService;
            _commodityTypeService = commodityTypeService;
            _programService = programService;
            _transporterService = transporterService;
            _periodService = periodService;
            _commodityService = commodityService;
            _transactionService = transactionService;
            _storeService = storeService;
            _adminUnitService = adminUnitService;
            _hubService = hubService;
            _fdpService = fdpService;
            _projectCodeService = projectCodeService;
            _shippingInstructionService = shippingInstructionService;
        }

        public ViewResult Index()
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null)
            {
                var user = _userProfileService.GetUser(membershipUser.UserName);
                var toFdps =
                    _dispatchAllocationService.GetCommitedAllocationsByHubDetached(
                        user.DefaultHub.HubID, _userProfileService.GetUser(membershipUser.UserName).
                            PreferedWeightMeasurment.ToUpperInvariant(), null, null, null);
                var loans = _otherDispatchAllocationService.GetAllToOtherOwnerHubs(user);
                var transfer = _otherDispatchAllocationService.GetAllToCurrentOwnerHubs(user);
                var adminUnit = new List<AdminUnit> { _adminUnitService.FindById(1) };
                var commodityTypes = _commodityTypeService.GetAllCommodityType();
                var model = new DispatchHomeViewModel(toFdps, loans, transfer, commodityTypes, adminUnit, user);
                return View(model);
            }
            return null;
        }

        [GridAction]
        public ActionResult GetFdpAllocations(bool? closed, int? adminUnitID, int? commodityType)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var fdpAllocations = _dispatchAllocationService.GetCommitedAllocationsByHubDetached(user.DefaultHub.HubID, user.PreferedWeightMeasurment, closed, adminUnitID, commodityType);
            return View(new GridModel(fdpAllocations));
        }

        [GridAction]
        public ActionResult GetLoanAllocations(bool? closed, int? commodityType)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var loanAllocations = _otherDispatchAllocationService.GetCommitedLoanAllocationsDetached(user, closed, commodityType);
            return View(new GridModel(loanAllocations));
        }

        [GridAction]
        public ActionResult GetTransferAllocations(bool? closed, int? commodityType)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var transferAllocations = _otherDispatchAllocationService.GetCommitedTransferAllocationsDetached(user, closed, commodityType);
            return View(new GridModel(transferAllocations));
        }

        [GridAction]
        public ActionResult DispatchListGrid(string dispatchAllocationID)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            //TODO cascade using allocation id
            var dispatchs = _dispatchService.ByHubIdAndAllocationIDetached(user.DefaultHub.HubID, Guid.Parse(dispatchAllocationID));
            return View(new GridModel(dispatchs));
        }

        [GridAction]
        public ActionResult DispatchOtherListGrid(string otherDispatchAllocationID)
        {
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            //TODO cascade using allocation id
            List<DispatchModelModelDto> otherDispatchs = _dispatchService.ByHubIdAndOtherAllocationIDetached(user.DefaultHub.HubID, Guid.Parse(otherDispatchAllocationID));
            return View(new GridModel(otherDispatchs));
        }
        
        [GridAction]
        public ActionResult DispatchListGridListGrid(string dispatchID)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            //(user.DefaultHub.HubID)
            var receiveDetails = _dispatchDetailService.ByDispatchIDetached(Guid.Parse(dispatchID), user.PreferedWeightMeasurment);
            return View(new GridModel(receiveDetails));
        }
        public ViewResult Log()
        {
            var dispatches = _dispatchService.GetAllDispatch();
            var user = _userProfileService.GetUser(User.Identity.Name);
            return View(dispatches.Where(p => p.HubID == user.DefaultHub.HubID).ToList());
        }

        //GIN unique validation

        public virtual ActionResult NotUnique(string gin, string dispatchID)
        {

            var dispatch = _dispatchService.GetDispatchByGIN(gin);
            var user = _userProfileService.GetUser(User.Identity.Name);
            
            Guid guidParse;
            if (Guid.TryParse(dispatchID, out guidParse))
            {
                guidParse = Guid.Parse(dispatchID);
            }

            if (dispatch == null || ((dispatch.DispatchID != Guid.Empty) && (dispatch.DispatchID == guidParse)))// new one or edit no problem 
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(dispatch.HubID == user.DefaultHub.HubID ? 
                string.Format("{0} is invalid, there is an existing record with the same GIN", gin) : 
                string.Format("{0} is invalid, there is an existing record with the same GIN at another Warehouse", gin), 
                JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Dispatch/Create

        public   ActionResult Create(string ginNo, int type)
        {

            ViewBag.Units = _unitService.GetAllUnit();

            var dispatch = _dispatchService.GetDispatchByGIN(ginNo);
            var user = _userProfileService.GetUser(User.Identity.Name);
            if (dispatch != null)
            {
                if (user.DefaultHub != null && user.DefaultHub.HubID == dispatch.HubID)
                {
                    PrepareEdit(dispatch, user, type);
                    var transaction = _dispatchService.GetDispatchTransaction(dispatch.DispatchID);
                    var dis = DispatchModel.GenerateDispatchModel(dispatch, transaction);
                    return View(dis);
                }
                PrepareCreate(type);
                var comms = new List<DispatchDetailModel>();
                ViewBag.SelectedCommodities = comms;
                var  theViewModel = new DispatchModel {Type = type, DispatchDetails = comms};
                ViewBag.Message = "The selected GIN Number doesn't exist on your default warehouse. Try changing your default warehouse.";
                return View(theViewModel);
            }
            else
            {
                PrepareCreate(type);
                var comms = new List<DispatchDetailModel>();
                ViewBag.SelectedCommodities = comms;
                var theViewModel = new DispatchModel {Type = type, DispatchDetails = comms};

                if (Request["type"] != null && Request["allocationId"] != null)
                {
                    var allocationId= Guid.Parse(Request["allocationId"]);
                    var allocationTypeId = Convert.ToInt32(Request["type"]);

                    if(allocationTypeId == 1)//to FDP
                    {
                        DispatchAllocation toFDPDispatchAllocation = _dispatchAllocationService.FindById(allocationId);
                        
                        theViewModel.FDPID = toFDPDispatchAllocation.FDPID;
                        PrepareFDPForEdit(toFDPDispatchAllocation.FDPID);
                        
                        theViewModel.RequisitionNo = toFDPDispatchAllocation.RequisitionNo;
                        theViewModel.BidNumber = toFDPDispatchAllocation.BidRefNo;
                        theViewModel.SINumber = toFDPDispatchAllocation.ShippingInstruction.Value;
                        theViewModel.ProjectNumber = toFDPDispatchAllocation.ProjectCode.Value;

                        theViewModel.CommodityTypeID = toFDPDispatchAllocation.Commodity.CommodityTypeID;
                        ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name",toFDPDispatchAllocation.Commodity.CommodityTypeID);
                       
                        if (toFDPDispatchAllocation.ProgramID.HasValue)
                        {
                            theViewModel.ProgramID = toFDPDispatchAllocation.ProgramID.Value;
                            ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name", theViewModel.ProgramID);
                        }
                        if (toFDPDispatchAllocation.TransporterID.HasValue)
                            theViewModel.TransporterID = toFDPDispatchAllocation.TransporterID.Value;
                            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name", theViewModel.TransporterID);
                        if (toFDPDispatchAllocation.Year.HasValue)
                            theViewModel.Year = toFDPDispatchAllocation.Year.Value;
                                    var years = (from y in _periodService.GetYears()
                         select new { Name = y, Id = y }).ToList();
                            ViewBag.Year = new SelectList(years, "Id", "Name"); 
                            ViewBag.Year = new SelectList(years, "Id", "Name",theViewModel.Year);            
                        if (toFDPDispatchAllocation.Month.HasValue)
                            theViewModel.Month = toFDPDispatchAllocation.Month.Value;
                        var months = (from y in _periodService.GetMonths(theViewModel.Year)
                                      select new { Name = y, Id = y }).ToList();
                        ViewBag.Month = new SelectList(months, "Id", "Name", theViewModel.Month);
                        if (toFDPDispatchAllocation.Round.HasValue)
                            theViewModel.Round = toFDPDispatchAllocation.Round.Value;
                        theViewModel.DispatchAllocationID = toFDPDispatchAllocation.DispatchAllocationID;

                    }
                    else //allocationTypeId == 2
                    {
                        var otherDispatchAllocation = _otherDispatchAllocationService.FindById(allocationId);
                        theViewModel.ToHubID = otherDispatchAllocation.ToHubID;
                        theViewModel.SINumber = otherDispatchAllocation.ShippingInstruction.Value;
                        theViewModel.ProjectNumber = otherDispatchAllocation.ProjectCode.Value;
                        theViewModel.ProgramID = otherDispatchAllocation.ProgramID;

                        theViewModel.CommodityTypeID = otherDispatchAllocation.Commodity.CommodityTypeID;
                        ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name", otherDispatchAllocation.Commodity.CommodityTypeID);

                        ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name", theViewModel.ProgramID);
                        theViewModel.OtherDispatchAllocationID = otherDispatchAllocation.OtherDispatchAllocationID;
                    }

                }

                return View(theViewModel);
            }
        } 

        //
        // POST: /Dispatch/Create

        [GridAction]
        public   ActionResult SelectDispatchsCommodities(string dispatchId)
        {
            var commodities = new List<DispatchDetailModel>();
            if (dispatchId != null)
            {
                var user = _userProfileService.GetUser(User.Identity.Name);
                commodities = DispatchDetailModel.GenerateDispatchDetailModels(_dispatchService.FindById(Guid.Parse(dispatchId)).DispatchDetails);
                //commodities = (from c in repository.DispatchDetail.GetDispatchDetail(Guid.Parse(dispatchId))
                //              select new Models.DispatchDetailModel()
                //              {
                //                  CommodityID = c.CommodityID,
                //                  CommodityName = c.Commodity.Name,
                //                  Description = c.Description,
                //                  Id = c.DispatchDetailID,
                //                  DispatchedQuantityMT = c.DispatchedQuantityInMT,
                //                  RequestedQuantityMT = c.RequestedQuantityInMT,
                //                  RequestedQuantity = c.RequestedQunatityInUnit,
                //                  DispatchedQuantity = c.DispatchedQuantityInUnit,
                //                  Unit = c.UnitID,
                //                 // DispatchID = c.DispatchID
                //              }).ToList();

                foreach (var gridCommodities in commodities)
                {
                    if (user.PreferedWeightMeasurment.Equals("qn"))
                    {
                        gridCommodities.DispatchedQuantityMT *= 10;
                        gridCommodities.RequestedQuantityMT *= 10;
                    }
                }

                string str = Request["prev"];
                if (GetSelectedCommodities(str) != null)
                {
                    // TODO: revise this section
                    var allCommodities = GetSelectedCommodities(Request["prev"].ToString(CultureInfo.InvariantCulture));
                    var count = -1;
                    foreach (var dispatchDetailViewModelComms in allCommodities)
                    {
                        if (dispatchDetailViewModelComms.Id == null)
                        {
                            //dispatchDetailViewModelComms.Id = count--;
                            dispatchDetailViewModelComms.DispatchDetailCounter = count--;
                            commodities.Add(dispatchDetailViewModelComms);
                        }
                        //
                        // TODO the lines below are too nice to have but we need to look into the performance issue and 
                        // policies (i.e. editing should not be allowed ) may be only for quanitities 
                         //
                        else //replace the commodity read from the db by what's comming from the user
                        {
                            commodities.Remove(commodities.Find(p => p.Id == dispatchDetailViewModelComms.Id));
                            commodities.Add(dispatchDetailViewModelComms);
                        }
                    }
                }
            }
            else
            {
                string str = Request["prev"];
                if (GetSelectedCommodities(str) != null)
                {
                    commodities = GetSelectedCommodities(Request["prev"].ToString(CultureInfo.InvariantCulture));
                    int count = -1;
                    foreach (var rdm in commodities)
                    {
                        if (rdm.Id != null)
                        {
                            rdm.DispatchDetailCounter = count--;
                        }
                    }
                }
            }
                
            ViewBag.Commodities = _commodityService.GetAllParents().Select(c => new CommodityModel() { Id = c.CommodityID, Name = c.Name }).ToList();
            //TODO do we really need the line below 
            //PrepareCreate(1);
            return View(new GridModel(commodities));
        }

        private void PrepareCreate(int type)
        {
            var years = (from y in _periodService.GetYears()
                         select new { Name = y, Id = y }).ToList();
            ViewBag.Year = new SelectList(years, "Id", "Name"); 
            ViewBag.Month = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");


            var user = _userProfileService.GetUser(User.Identity.Name);

            ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name");
            ViewBag.StoreID = new SelectList(_storeService.GetStoreByHub(user.DefaultHub.HubID), "StoreID", "Name");
            ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name");

            ViewData["Commodities"] = _commodityService.GetAllParents().Select(c => new CommodityModel { Id = c.CommodityID, Name = c.Name }).ToList();
            ViewBag.Units = _unitService.GetAllUnit();
            if (type == 1)
            {
                PrepareFDPCreate();
            }
            else if (type == 2)
            {
                var hub = _hubService.GetAllHub();
                hub.Remove(user.DefaultHub);
                ViewBag.ToHUBs = new SelectList(hub.Select(p => new { Name = string.Format("{0} - {1}", p.Name, p.HubOwner.Name), p.HubID }), "HubID", "Name");
            }

            var comms = new List<DispatchDetailModel>();
            ViewBag.SelectedCommodities = comms;
            ViewBag.StackNumber = new SelectList(Enumerable.Empty<SelectListItem>());
        }

        private void PrepareFDPCreate()
        {
            ViewBag.SelectedRegionId = new SelectList(_adminUnitService.GetRegions().Select(p => new{Id = p.AdminUnitID, p.Name}), "Id", "Name");
            ViewBag.SelectedWoredaId = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            ViewBag.FDPID = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            ViewBag.SelectedZoneId = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
        }

        [HttpPost]
        public   ActionResult Create(DispatchModel dispatchModel)
        {
            
            var user = _userProfileService.GetUser(User.Identity.Name);

            var insertCommodities = new List<DispatchDetailModel>();
            var updateCommodities = new List<DispatchDetailModel>();
            var prevCommodities = new List<DispatchDetailModel>();
            if (dispatchModel.JSONPrev != null)
            {
                 prevCommodities = GetSelectedCommodities(dispatchModel.JSONPrev);

                //Even though they are updated they are not saved so move them in to the inserted at the end of a succcessful submit
                int count = 0;
                foreach (var dispatchDetailAllViewModels in prevCommodities)
                {
                    if (dispatchDetailAllViewModels.Id != null)
                    {
                        count--;
                        dispatchDetailAllViewModels.DispatchDetailCounter = count;
                        insertCommodities.Add(dispatchDetailAllViewModels);
                    }
                    else
                    {
                        updateCommodities.Add(dispatchDetailAllViewModels);
                    }
                }

                ViewBag.ReceiveDetails = prevCommodities;
                ViewBag.SelectedCommodities = prevCommodities;
                dispatchModel.DispatchDetails = prevCommodities;

                //this check need's to be revisited
                if (!prevCommodities.Any())
                {
                    ModelState.AddModelError("DispatchDetails", @"Please add atleast one commodity to save this Dispatch");
                }
                string errorMessage = null;
                foreach (var dispatchDetailViewModel in prevCommodities)
                {
                    var validationContext = new ValidationContext(dispatchDetailViewModel, null, null);
                    var validationResults = dispatchDetailViewModel.Validate(validationContext);
                    errorMessage = validationResults.Aggregate(errorMessage, (current, v) => string.Format("{0}, {1}", current, v.ErrorMessage));
                    var comms = _commodityService.FindById(dispatchDetailViewModel.CommodityID);
                    var commType = _commodityTypeService.FindById(dispatchModel.CommodityTypeID);
                    if (dispatchModel.CommodityTypeID != comms.CommodityTypeID)
                        ModelState.AddModelError("DispatchDetails", comms.Name + @" is not of type " + commType.Name);
                }
                if (errorMessage != null)
                {
                    ModelState.AddModelError("DispatchDetails", errorMessage);
                }
            }else
            {
                ModelState.AddModelError("DispatchDetails", @"Please add atleast one commodity to save this Dispatch");
            }
            if (dispatchModel.Type != 1)
            {
                ModelState.Remove("FDPID");
                ModelState.Remove("RegionID");
                ModelState.Remove("WoredaID");
                ModelState.Remove("ZoneID");
                ModelState.Remove("BidNumber");
                dispatchModel.BidNumber = "00000";
                //NOT really needed 
                ModelState.Remove("Year");
                ModelState.Remove("Month");
            }
            else
            {
                ModelState.Remove("ToHubID");
            }
            
            if (ModelState.IsValid && user != null)
            {

                if (dispatchModel.ChangeStoreManPermanently)
                {
                    var storeTobeChanged = _storeService.FindById(dispatchModel.StoreID);
                    if (storeTobeChanged != null && dispatchModel.ChangeStoreManPermanently)
                        storeTobeChanged.StoreManName = dispatchModel.DispatchedByStoreMan;
                }
                var dispatch = dispatchModel.GenerateDipatch(user);
                //if (dispatch.DispatchID == null )
                if(dispatchModel.DispatchID == null)
                {

                    dispatchModel.DispatchDetails = prevCommodities;
                    foreach (var gridCommodities in prevCommodities)
                    {
                        if (user.PreferedWeightMeasurment.Equals("qn"))
                        {
                            gridCommodities.DispatchedQuantityMT /= 10;
                            gridCommodities.RequestedQuantityMT /= 10;
                        }
                    }
                    //InsertDispatch(dispatchModel, user);
                    _transactionService.SaveDispatchTransaction(dispatchModel, user);
                }
                else
                {

                   // List<Models.DispatchDetailModel> insertCommodities = GetSelectedCommodities(dispatchModel.JSONInsertedCommodities);
                    var deletedCommodities = GetSelectedCommodities(dispatchModel.JSONDeletedCommodities);
                   // List<Models.DispatchDetailModel> updateCommodities = GetSelectedCommodities(dispatchModel.JSONUpdatedCommodities);
                    dispatch.HubID = user.DefaultHub.HubID;
                    dispatch.Update(GenerateDispatchDetail(insertCommodities),
                        GenerateDispatchDetail(updateCommodities),
                        GenerateDispatchDetail(deletedCommodities));

                }
                
                return RedirectToAction("Index");
             }
            //List<Models.DispatchDetailModel> details = GetSelectedCommodities(dispatchModel.JSONInsertedCommodities);
            //Session["SELCOM"] = details;

            //UserProfile user =UserProfile.GetUser(User.Identity.Name);
            PrepareCreate(dispatchModel.Type);

            if (dispatchModel.FDPID != null)
            {
                PrepareFDPForEdit(dispatchModel.FDPID);
                dispatchModel.WoredaID = _fdpService.FindById(dispatchModel.FDPID.Value).AdminUnitID;
            } //PrepareEdit(dispatchModel.GenerateDipatch(), user,dispatchModel.Type);
            return View(dispatchModel);
        }
        //TODO remove this function later
        private void InsertDispatch(DispatchModel dispatchModel, UserProfile user)
        {
            List<DispatchDetailModel> commodities = GetSelectedCommodities(dispatchModel.JSONInsertedCommodities);
            dispatchModel.DispatchDetails = commodities;
            _transactionService.SaveDispatchTransaction(dispatchModel,user);
        }

       
       

        private static List<DispatchDetail> GenerateDispatchDetail(IEnumerable<DispatchDetailModel> c)
        {
            if (c != null)
            {
                return (from m in c
                        let requestedQuantityMt = m.RequestedQuantityMT
                        where requestedQuantityMt != null
                        let requestedQuantity = m.RequestedQuantity
                        where requestedQuantity != null
                        select new DispatchDetail
                            {
                        CommodityID = m.CommodityID,
                        Description = m.Description,
                        //DispatchDetailID = m.Id,
                        RequestedQuantityInMT = requestedQuantityMt.Value,
                        //DispatchedQuantityInMT = c.DispatchedQuantityMT,
                        //DispatchedQuantityInUnit = c.DispatchedQuantity,
                        RequestedQunatityInUnit = requestedQuantity.Value,
                        UnitID = m.Unit
                    }).ToList();
                }
            return new List<DispatchDetail>();
        }

        public   ActionResult Months(int year)
        {
            var months = from s in _periodService.GetMonths(year)
                         select new { Name = s, Id = s };
            return Json(new SelectList(months,"Id","Name"), JsonRequestBehavior.AllowGet);
        }


        public   ActionResult JsonDispatch(string ginNo)
        {
            var dispatch = _dispatchService.GetDispatchByGIN(ginNo);
            if (dispatch != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return new EmptyResult();
        }

        public   ActionResult _DispatchPartial(string ginNo, int type)
        {
            ViewBag.Units = _unitService.GetAllUnit();
            
            var dispatch = _dispatchService.GetDispatchByGIN(ginNo);
            var user = _userProfileService.GetUser(User.Identity.Name);
            if (dispatch != null)
            {
                type = dispatch.Type;//override the type by the data type coming from the DB(i.e. load the DB data by overriding the type)
                if (user.DefaultHub != null && user.DefaultHub.HubID == dispatch.HubID)
                {
                    PrepareEdit(dispatch, user, type);
                    var transaction = _dispatchService.GetDispatchTransaction(dispatch.DispatchID);
                    return PartialView("DispatchPartial", DispatchModel.GenerateDispatchModel(dispatch,transaction));
                }
                PrepareCreate(type);
                ViewBag.Message = "The selected GIN Number doesn't exist on your default warehouse. Try changing your default warehouse.";
                return PartialView("DispatchPartial", new DispatchModel());
            }
            PrepareCreate(type);
            return PartialView("DispatchPartial", new DispatchModel());
        }

        private void PrepareEdit(Dispatch dispatch, UserProfile user, int type)
        {
            var years = (from y in _periodService.GetYears()
                         select new { Name = y, Id = y }).ToList();
            var months = (from y in _periodService.GetMonths(dispatch.PeriodYear)
                         select new { Name = y, Id = y }).ToList();
            ViewBag.Year = new SelectList(years, "Id", "Name", dispatch.PeriodYear);
            ViewBag.Month = new SelectList(months, "Id", "Name", dispatch.PeriodMonth);
            ViewData["Units"] = _unitService.GetAllUnit().Select(p => new { Id = p.UnitID, p.Name}).ToList();
            var transaction = _dispatchService.GetDispatchTransaction(dispatch.DispatchID);
            

            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name", dispatch.TransporterID);
            if (type == 1)
            {
                PrepareFDPForEdit(dispatch.FDPID);
            }
            else if (type == 2)
            {
                var tran = _dispatchService.GetDispatchTransaction(dispatch.DispatchID);
                //TODO I think there need's to be a check for this one 
                ViewBag.ToHUBs = tran != null ? new SelectList(_hubService.GetAllHub().Select(p => new {Name = string.Format("{0} - {1}",p.Name,p.HubOwner.Name), HubID = p.HubID}), "HubID", "Name", tran.Account.EntityID) : null;
            }

            if (transaction != null)
            {
                ViewBag.StoreID = new SelectList(_storeService.GetStoreByHub(user.DefaultHub.HubID), "StoreID", "Name", transaction.StoreID);
                ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name", transaction.ProgramID);
                if (transaction.Stack != null)
                    ViewBag.StackNumbers = new SelectList(transaction.Store.Stacks.Select(p => new { Name = p, Id = p }), "Id", "Name", transaction.Stack.Value);
                ViewData["Commodities"] = _commodityService.GetAllParents().Select(c => new CommodityModel { Id = c.CommodityID, Name = c.Name }).ToList();
                ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name",transaction.Commodity.CommodityTypeID);
            }
            else
            {
                ViewBag.StoreID = new SelectList(_storeService.GetStoreByHub(user.DefaultHub.HubID), "StoreID",
                                                 "Name"); //, transaction.StoreID);
                ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name");
                    //, transaction.ProgramID);
                //TODO i'm not so sure about the next line
                var firstOrDefault = _storeService.GetAllStore().FirstOrDefault();
                if (firstOrDefault != null)
                    ViewBag.StackNumbers =
                        new SelectList(firstOrDefault.Stacks.Select(p => new {Name = p, Id = p}), "Id",
                                       "Name"); //, transaction.Stack.Value); )//transaction.Store.Stacks
                ViewData["Commodities"] =
                    _commodityService.GetAllParents().Select(
                        c => new CommodityModel {Id = c.CommodityID, Name = c.Name}).ToList();
                ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name");
            }
            var comms = new List<DispatchDetailModel>();
            ViewBag.SelectedCommodities = comms;
        }

        private void PrepareFDPForEdit(int? fdpid)
        {
            var unitModel = new AdminUnitModel();
            var fdp = fdpid != null ? _fdpService.FindById(fdpid.Value) : null;
            if (fdp != null)
            {
                unitModel.SelectedWoredaId = fdp.AdminUnitID;
                if (fdp.AdminUnit.ParentID != null) unitModel.SelectedZoneId = fdp.AdminUnit.ParentID.Value;

                unitModel.SelectedRegionId = _adminUnitService.GetRegionByZoneId(unitModel.SelectedZoneId);
                ViewBag.SelectedRegionId = new SelectList(_adminUnitService.GetRegions().Select(p => new { Id = p.AdminUnitID, p.Name}).OrderBy(o => o.Name), "Id", "Name", unitModel.SelectedRegionId);
                ViewBag.SelectedZoneId = new SelectList(GetChildren(unitModel.SelectedRegionId).OrderBy(o => o.Name), "Id", "Name", unitModel.SelectedZoneId);
                ViewBag.SelectedWoredaId = new SelectList(GetChildren(unitModel.SelectedZoneId).OrderBy(o => o.Name), "Id", "Name", unitModel.SelectedWoredaId);
                ViewBag.FDPID = new SelectList(GetFdps(unitModel.SelectedWoredaId).OrderBy(o => o.Name), "Id", "Name", fdp.FDPID);
            }
            else
            {
                ViewBag.SelectedRegionId = new SelectList(unitModel.Regions, "Id", "Name");
                ViewBag.SelectedWoredaId = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
                ViewBag.FDPID = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
                ViewBag.SelectedZoneId = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            }
        }
        
        //
        // GET: /Dispatch/Edit/5

        public   ActionResult Edit(string id)
        {
            Dispatch dispatch = _dispatchService.FindById(Guid.Parse(id));
            //ViewBag.PeriodID = new SelectList(db.Periods, "PeriodID", "PeriodID", dispatch.PeriodID);
            ViewBag.StoreID = new SelectList(_storeService.GetAllStore(), "StoreID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name", dispatch.TransporterID);
            ViewBag.HubID = new SelectList(_hubService.GetAllHub(), "WarehouseID", "Name", dispatch.HubID);
            return View("Edit", dispatch);
        }

        //
        // POST: /Dispatch/Edit/5

        [HttpPost]
        public   ActionResult Edit(Dispatch dispatch)
        {
            if (ModelState.IsValid)
            {
                _dispatchService.EditDispatch(dispatch);
                return RedirectToAction("Index");
            }
            ViewBag.PeriodID = new SelectList(_periodService.GetAllPeriod(), "PeriodID", "PeriodID", _periodService.GetPeriod(dispatch.PeriodYear, dispatch.PeriodMonth).PeriodID);
            var user = _userProfileService.GetUser(User.Identity.Name);
            ViewBag.StoreID = new SelectList(_storeService.GetStoreByHub(user.DefaultHub.HubID), "StoreID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name", dispatch.TransporterID);
            ViewBag.HubID = new SelectList(user.UserHubs, "HubID", "Name", dispatch.HubID);
            ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name");
            return View(dispatch);
        }

     

        
        public List<AdminUnitItem> GetChildren(int parentId)
        {
            var units = from item in _adminUnitService.GetChildren(parentId)
                        select new AdminUnitItem
                        {
                            Id = item.AdminUnitID,
                            Name = item.Name
                        };
            return units.ToList();
        }

        public List<AdminUnitItem> GetFdps(int woredaId)
        {
            var fdps = from p in _fdpService.GetFDPsByWoreda(woredaId)
                       select new AdminUnitItem { Id = p.FDPID, Name = p.Name };
            return fdps.ToList();
        }

        private List<DispatchDetailModel> GetSelectedCommodities(string jsonArray)
        {
            List<DispatchDetailModel> commodities = null;
            if (!string.IsNullOrEmpty(jsonArray))
            {
                    commodities = JsonConvert.DeserializeObject<List<DispatchDetailModel>>(jsonArray);      
            }
            return commodities;
        }

        public ActionResult IsProjectValid(string projectNumber)
        {
            var count = _projectCodeService.GetProjectCodeId(projectNumber);
            var result = (count > 0);
            return (Json(result, JsonRequestBehavior.AllowGet));
        }

        public ActionResult IsSIValid(string siNumber, int? fdpid)
        {
            bool result;
            if(fdpid != null)
            {
                result=  _shippingInstructionService.HasBalance(siNumber, fdpid.Value);
            }else
            {
                result = _shippingInstructionService.GetShipingInstructionId(siNumber) > 0;
            }
             
            return (Json(result, JsonRequestBehavior.AllowGet));
        }

        public ActionResult FDPBalance(string requisitionNo, int fdpid)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var repositoryDispatchGetFDPBalance = _dispatchService.GetFDPBalance(fdpid, requisitionNo);

            if (repositoryDispatchGetFDPBalance.CommodityTypeID == 1)
            {
                if (user.PreferedWeightMeasurment.ToUpperInvariant() == "MT")
                {
                    repositoryDispatchGetFDPBalance.mesure = "MT";
                    repositoryDispatchGetFDPBalance.multiplier = 1;
                }
                else
                {
                    repositoryDispatchGetFDPBalance.mesure = "Qtl";
                    repositoryDispatchGetFDPBalance.multiplier = 10;
                }
            }
            else
            {
                repositoryDispatchGetFDPBalance.TotalAllocation *= 10;
                repositoryDispatchGetFDPBalance.CurrentBalance *= 10;
                //TODO fix the line below it's not corrcet for some cases
                
                repositoryDispatchGetFDPBalance.mesure = "Unit";
                repositoryDispatchGetFDPBalance.multiplier = 1;
            }

            return Json(repositoryDispatchGetFDPBalance, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AvailbaleCommodities(string siNumber)
        {
            return Json(_dispatchService.GetAvailableCommodities(siNumber, _userProfileService.GetUser(User.Identity.Name).DefaultHub.HubID).Select(p => new { Value = p.CommodityID, Text = p.Name })
                , JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonRegionZones(string requisitionNumber)
        {
            List<DispatchAllocation> allocations = _dispatchAllocationService.GetAllocations(requisitionNumber);
            if(allocations.Count > 0)
            {
                var firstOrDefault = allocations.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    var region = firstOrDefault.FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID;
                    var zone = firstOrDefault.FDP.AdminUnit.AdminUnit2.AdminUnitID;
                    return Json(new {region, zone}, JsonRequestBehavior.AllowGet);
                }
            }
            return Json( "" , JsonRequestBehavior.AllowGet);
        }
    }
}
