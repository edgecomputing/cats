using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Areas.Hub.Controllers.Allocations
{
    [Authorize]
    public class ReceiptAllocationController : BaseController
    {
       
        private readonly IReceiptAllocationService _receiptAllocationService;
        private readonly IUserProfileService _userProfileService;
        private readonly ICommoditySourceService _commoditySourceService;
        private readonly IGiftCertificateService _giftCertificateService;
        private readonly ICommodityService _commodityService;
        private readonly IDonorService _donorService;
        private readonly IGiftCertificateDetailService _giftCertificateDetailService;
        private readonly IHubService _hubService;
        private readonly IProgramService _programService;
        private readonly ICommodityTypeService _commodityTypeService;
        private readonly IShippingInstructionService _shippingInstructionService;
        public ReceiptAllocationController(IReceiptAllocationService receiptAllocationService,
            IUserProfileService userProfileService,
            ICommoditySourceService commoditySourceService,
            IGiftCertificateService giftCertificateService,
            ICommodityService commodityService,
            IDonorService donorService,
            IGiftCertificateDetailService giftCertificateDetailService,
            IHubService hubService,
            IProgramService programService,
            ICommodityTypeService commodityTypeService, IShippingInstructionService shippingInstructionService)
            : base(userProfileService)
        {
            this._receiptAllocationService = receiptAllocationService;
            this._userProfileService = userProfileService;
            this._commoditySourceService = commoditySourceService;
            this._giftCertificateService = giftCertificateService;
            this._commodityService = commodityService;
            this._donorService = donorService;
            this._giftCertificateDetailService = giftCertificateDetailService;
            this._hubService = hubService;
            this._programService = programService;
            this._commodityTypeService = commodityTypeService;
            this._shippingInstructionService = shippingInstructionService;
        }

        public ActionResult SelfReference(int HubID, int? SourceHubID)
        {
            if (HubID == SourceHubID)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        private ReceiptAllocation FromReceiptAllocationViewModel(ReceiptAllocationViewModel viewModel, ReceiptAllocation entityModel)
        {

            
                   // ReceiptAllocationID = receiptAllocation.ReceiptAllocationID,
                    entityModel.PartitionID = viewModel.PartitionID;
                    entityModel.IsCommited = viewModel.IsCommited;
                    entityModel.ETA = viewModel.ETA;
                    entityModel.ProjectNumber = viewModel.ProjectNumber;
                    entityModel.GiftCertificateDetailID = viewModel.GiftCertificateDetailID;
                    entityModel.CommodityID = viewModel.CommodityID;
                   // CommodityName = receiptAllocation.Commodity.Name,
                    entityModel.SINumber = viewModel.SINumber;
                    entityModel.UnitID = viewModel.UnitID;
                    entityModel.QuantityInUnit = viewModel.QuantityInUnit;
                    entityModel.QuantityInMT = viewModel.QuantityInMT;
                    entityModel.RemainingBalanceInMT = viewModel.RemainingBalanceInMT;
                    entityModel.ReceivedQuantityInMT = viewModel.ReceivedQuantityInMT;
                    entityModel.DonorID = viewModel.DonorID;
                    entityModel.ProgramID = viewModel.ProgramID;
                    entityModel.CommoditySourceID = viewModel.CommoditySourceID;
                    entityModel.IsClosed = viewModel.IsClosed;
                    entityModel.PurchaseOrder = viewModel.PurchaseOrder;
                    entityModel.SupplierName = viewModel.SupplierName;
                    entityModel.SourceHubID = viewModel.SourceHubID;
                    entityModel.OtherDocumentationRef = viewModel.OtherDocumentationRef;
                    entityModel.Remark = viewModel.Remark;
                

            return entityModel;
        }

        public ActionResult QuantityNotValid(Decimal QuantityInMt, string SINumber)
        {

            decimal AlreadyAllocateDBalance = _receiptAllocationService.GetBalanceForSI(SINumber);
            // decimal TotalAllocatedOnGiftCetificate =
            //     _giftCertificateServiceDetail.GetAll().Where(p=>p.GiftCertificate.SINumber == SINumber).Aggregate(q=>q.WeightInMT);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SINotUnique(string SINUmber, int? CommoditySourceID)
        {
            if (CommoditySourceID.HasValue)
                return Json(IsSIValid(SINUmber, CommoditySourceID.Value), JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllocationList(string SInumber, int type)
        {
           UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            var list =
                _receiptAllocationService.GetAllByTypeMerged(type).Where(
                    p => p.SINumber == SInumber && p.HubID == user.DefaultHub.Value);
            //foreach (ReceiptAllocation receiptAllocation in list)
            //{
            //    if (user.UserAllowedHubs.Any(x => x.HubID == receiptAllocation.HubID))
            //    {
            //        receiptAllocation.UserNotAllowedHub = true;
            //    }
            //    else
            //    {
            //        receiptAllocation.UserNotAllowedHub = false;
            //    }
            //}
            ViewBag.CommoditySourceType = type;
            return PartialView("AllocationList", list);
        }

        public ActionResult Index()
        {
            ViewBag.SI = Request["si"];
            //By default set to DONATION
            int sourceType =CommoditySource.Constants.DONATION;

            if (Request["type"] != null)
            {
                sourceType = Convert.ToInt32(Request["type"]);
            }

            List<ReceiptAllocation> list = _receiptAllocationService.GetAllByTypeMerged(sourceType);

            ViewBag.CommoditySourceType = sourceType;

            if (CommoditySource.Constants.DONATION == sourceType)
            {
                ViewBag.CommoditySourceTypeText = _commoditySourceService.FindById(sourceType).Name;
            }
            //else if (CommoditySource.Constants.TRANSFER == sourceType)
            //{
            //    ViewBag.CommoditySourceTypeText = _commoditySourceService.FindById(sourceType).Name;
            //}
            else if (CommoditySource.Constants.LOCALPURCHASE == sourceType)
            {
                ViewBag.CommoditySourceTypeText = _commoditySourceService.FindById(sourceType).Name;
            }
            else if (CommoditySource.Constants.TRANSFER == sourceType ||
                   CommoditySource.Constants.REPAYMENT == sourceType ||
                    CommoditySource.Constants.LOAN == sourceType ||
                    CommoditySource.Constants.SWAP == sourceType)
            {
                ViewBag.CommoditySourceTypeText = "Loan, Repayment, transfer and Swap";
            }

            return View(list);
        }

        public ActionResult Create(int? type)
        {
            var viewModel = BindReceiptAllocaitonViewModel();

          
          

            if (Request["type"] != null)
            {
                viewModel.CommoditySourceID = ViewBag.AllocationType = Convert.ToInt32(Request["type"]);
            }
            else
            {
                viewModel.CommoditySourceID = ViewBag.AllocationType = 1;
            }

            return View(viewModel);
        }
        private ReceiptAllocationViewModel BindReceiptAllocaitonViewModel()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var commodities = _commodityService.GetAllCommodity().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var donors = _donorService.GetAllDonor().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var hubs = new List<Cats.Models.Hubs.Hub>();
            if (user != null)
            {
                //Hubs = new List<Hub>() { user.DefaultHub };
                hubs = _hubService.GetAllWithoutId(user.DefaultHub.Value).DefaultIfEmpty().OrderBy(o => o.Name)
                    .ToList();
            }
            else
            {

                hubs =
                    _hubService.GetAllHub().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            }
            var programs = _programService.GetAllProgram().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var commoditySources = _commoditySourceService.GetAllCommoditySource().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var commodityTypes = _commodityTypeService.GetAllCommodityType().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var viewModel = new ReceiptAllocationViewModel(commodities, donors, hubs, programs, commoditySources, commodityTypes, user);
            viewModel.HubID = user.DefaultHub.Value;
            return viewModel;
        }
        //
        // POST: /Create

        [HttpPost]
        public ActionResult Create(ReceiptAllocationViewModel receiptAllocationViewModel)
        {
            if (receiptAllocationViewModel.CommoditySourceID ==CommoditySource.Constants.DONATION)
            {
                ModelState.Remove("SourceHubID");
                ModelState.Remove("SupplierName");
                ModelState.Remove("PurchaseOrder");

            }
            else if (receiptAllocationViewModel.CommoditySourceID ==CommoditySource.Constants.LOCALPURCHASE)
            {
                ModelState.Remove("DonorID");
                ModelState.Remove("SourceHubID");
            }
            else
            {
                ModelState.Remove("DonorID");
                ModelState.Remove("SupplierName");
                ModelState.Remove("PurchaseOrder");
            }

            if (!(IsSIValid(receiptAllocationViewModel.SINumber, receiptAllocationViewModel.CommoditySourceID)))
            {
                ModelState.AddModelError("SINumber", "");
            }

            if (ModelState.IsValid)
            {
               ReceiptAllocation receiptAllocation = receiptAllocationViewModel.GenerateReceiptAllocation();
                //for creation make the giftCetificate null if it's from 
                if (receiptAllocationViewModel.GiftCertificateDetailID == 0 ||
                    receiptAllocationViewModel.GiftCertificateDetailID == null
                    )
                {
                    var shippingInstruction =
                        _shippingInstructionService.FindBy(t => t.Value == receiptAllocationViewModel.SINumber).
                            FirstOrDefault();
                    var GC = new Cats.Models.Hubs.GiftCertificate();
                    if(shippingInstruction!=null)
                        GC = _giftCertificateService.FindBySINumber(shippingInstruction.ShippingInstructionID);

                    if (GC != null)
                    {
                        var GCD =
                            GC.GiftCertificateDetails.FirstOrDefault(
                                p => p.CommodityID == receiptAllocationViewModel.CommodityID);
                        if (GCD != null) //&& GCD.GiftCertificateDetailID;
                        {
                            receiptAllocation.GiftCertificateDetailID = GCD.GiftCertificateDetailID;
                        }
                    }
                    else
                    {
                        receiptAllocation.GiftCertificateDetailID = null;
                    }
                }
                int typeOfGridToReload = receiptAllocation.CommoditySourceID;
                int commType = _commodityService.FindById(receiptAllocation.CommodityID).CommodityTypeID;
                //override to default hub
               UserProfile user = _userProfileService.GetUser(User.Identity.Name);
                receiptAllocation.HubID = user.DefaultHub.Value;

                if (typeOfGridToReload != Cats.Models.Hubs.CommoditySource.Constants.DONATION &&
                    typeOfGridToReload != Cats.Models.Hubs.CommoditySource.Constants.LOCALPURCHASE)
                {
                    typeOfGridToReload = Cats.Models.Hubs.CommoditySource.Constants.LOAN;
                }
                receiptAllocation.ReceiptAllocationID = Guid.NewGuid();
                _receiptAllocationService.AddReceiptAllocation(receiptAllocation);

                return Json(new { gridId = typeOfGridToReload, CommodityTypeID = commType, status =true}, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            //check this out later
            //return this.Create(receiptAllocationViewModel.CommoditySourceID);
            //ModelState.Remove("SINumber");
            //TODO:Check if commenting out has any effect
            //=================================================
            //receiptAllocationViewModel.InitalizeViewModel();
            //================================================
            return PartialView(receiptAllocationViewModel);

        }

        public ActionResult Edit(String allocationId)
        {

            var receiptAllocation = _receiptAllocationService.FindById(Guid.Parse(allocationId));
           UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            var receiptAllocationViewModel =BindReceiptAllocaitonViewModel();
            if (receiptAllocation != null) // && receiptAllocation.IsCommited == false)
            {
                receiptAllocationViewModel.IsCommited = receiptAllocation.IsCommited;
                receiptAllocationViewModel.ReceiptAllocationID = receiptAllocation.ReceiptAllocationID;
                receiptAllocationViewModel.PartitionID = receiptAllocation.PartitionID;
                receiptAllocationViewModel.ProjectNumber = receiptAllocation.ProjectNumber;
                receiptAllocationViewModel.CommodityID = receiptAllocation.CommodityID;
                //LOAD THE respective type and 
                //TODO remove the other Types
                receiptAllocationViewModel.CommodityTypeID = receiptAllocation.Commodity.CommodityTypeID;

                receiptAllocationViewModel.SINumber = receiptAllocation.SINumber;
                receiptAllocationViewModel.QuantityInMT = receiptAllocation.QuantityInMT;
                if (receiptAllocation.QuantityInUnit == null)
                    receiptAllocationViewModel.QuantityInUnit = 0;
                else
                    receiptAllocationViewModel.QuantityInUnit = receiptAllocation.QuantityInUnit.Value;
                receiptAllocationViewModel.HubID = receiptAllocation.HubID;
                receiptAllocationViewModel.ETA = receiptAllocation.ETA;
                receiptAllocationViewModel.DonorID = receiptAllocation.DonorID;
                receiptAllocationViewModel.GiftCertificateDetailID = receiptAllocation.GiftCertificateDetailID;
                receiptAllocationViewModel.ReceiptAllocationID = receiptAllocation.ReceiptAllocationID;
                receiptAllocationViewModel.ProgramID = receiptAllocation.ProgramID;
                receiptAllocationViewModel.CommoditySourceID = receiptAllocation.CommoditySourceID;
                receiptAllocationViewModel.SourceHubID = receiptAllocation.SourceHubID;
                receiptAllocationViewModel.PurchaseOrder = receiptAllocation.PurchaseOrder;
                receiptAllocationViewModel.SupplierName = receiptAllocation.SupplierName;
                receiptAllocationViewModel.OtherDocumentationRef = receiptAllocation.OtherDocumentationRef;
                receiptAllocationViewModel.Remark = receiptAllocation.Remark;

                var shippingInstruction =
                        _shippingInstructionService.FindBy(t => t.Value == receiptAllocationViewModel.SINumber).
                            FirstOrDefault();
                    var GC = new Cats.Models.Hubs.GiftCertificate();
                    if(shippingInstruction!=null)
                        GC = _giftCertificateService.FindBySINumber(shippingInstruction.ShippingInstructionID);
                if (GC != null && receiptAllocation.CommoditySourceID ==CommoditySource.Constants.DONATION)
                {
                    receiptAllocationViewModel.Commodities.Clear();
                    receiptAllocationViewModel.Donors.Clear();
                    receiptAllocationViewModel.Programs.Clear();
                    foreach (GiftCertificateDetail giftCertificateDetail in GC.GiftCertificateDetails)
                    {
                        receiptAllocationViewModel.Commodities.Add(giftCertificateDetail.Commodity);
                    }
                    //Commodity commodity = receiptAllocationViewModel.Commodities.FirstOrDefault();
                    //if (commodity != null) receiptAllocationViewModel.CommodityID = commodity.CommodityID;
                    receiptAllocationViewModel.Donors.Add(GC.Donor);
                    receiptAllocationViewModel.DonorID = GC.DonorID;
                    receiptAllocationViewModel.Programs.Add(GC.Program);
                    receiptAllocationViewModel.ProgramID = GC.ProgramID;
                    receiptAllocationViewModel.CommoditySources.Clear();
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.DONATION));
                    receiptAllocationViewModel.CommoditySourceID =CommoditySource.Constants.DONATION;

                }
                //else
                //{
                int sourceType = receiptAllocation.CommoditySourceID;

                if (CommoditySource.Constants.DONATION == sourceType)

                {
                    receiptAllocationViewModel.CommoditySources.Clear();
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.DONATION));
                }
                //else if (CommoditySource.Constants.TRANSFER == sourceType)
                //{
                //    receiptAllocationViewModel.CommoditySources.Clear();
                //    receiptAllocationViewModel.CommoditySources.Add(
                //        _commoditySourceService.FindById(CommoditySource.Constants.TRANSFER));
                //}
                else if (CommoditySource.Constants.LOCALPURCHASE == sourceType)
                {
                    receiptAllocationViewModel.CommoditySources.Clear();
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.LOCALPURCHASE));
                }
                else if (CommoditySource.Constants.TRANSFER == sourceType ||
                        CommoditySource.Constants.REPAYMENT == sourceType ||
                        CommoditySource.Constants.LOAN == sourceType ||
                        CommoditySource.Constants.SWAP == sourceType)
                {
                    receiptAllocationViewModel.CommoditySources.Clear();
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.REPAYMENT));
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.LOAN));
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.SWAP));
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.TRANSFER));

                }
                // }
            }
            ViewBag.CommoditySourceType = receiptAllocation != null
                                              ? receiptAllocation.CommoditySourceID
                                              :CommoditySource.Constants.DONATION;

            if (receiptAllocation != null && receiptAllocation.Receives.Any())
                ViewBag.receiveUnderAllocation = true;

            return PartialView("Edit", receiptAllocationViewModel);

        }
        public ActionResult Edit2(String allocationId)
        {

            var receiptAllocation = _receiptAllocationService.FindById(Guid.Parse(allocationId));
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            var receiptAllocationViewModel = BindReceiptAllocaitonViewModel();
            if (receiptAllocation != null) // && receiptAllocation.IsCommited == false)
            {
                receiptAllocationViewModel.IsCommited = receiptAllocation.IsCommited;
                receiptAllocationViewModel.ReceiptAllocationID = receiptAllocation.ReceiptAllocationID;
                receiptAllocationViewModel.PartitionID = receiptAllocation.PartitionID;
                receiptAllocationViewModel.ProjectNumber = receiptAllocation.ProjectNumber;
                receiptAllocationViewModel.CommodityID = receiptAllocation.CommodityID;
                //LOAD THE respective type and 
                //TODO remove the other Types
                receiptAllocationViewModel.CommodityTypeID = receiptAllocation.Commodity.CommodityTypeID;

                receiptAllocationViewModel.SINumber = receiptAllocation.SINumber;
                receiptAllocationViewModel.QuantityInMT = receiptAllocation.QuantityInMT;
                if (receiptAllocation.QuantityInUnit == null)
                    receiptAllocationViewModel.QuantityInUnit = 0;
                else
                    receiptAllocationViewModel.QuantityInUnit = receiptAllocation.QuantityInUnit.Value;
                receiptAllocationViewModel.HubID = receiptAllocation.HubID;
                receiptAllocationViewModel.ETA = receiptAllocation.ETA;
                receiptAllocationViewModel.DonorID = receiptAllocation.DonorID;
                receiptAllocationViewModel.GiftCertificateDetailID = receiptAllocation.GiftCertificateDetailID;
                receiptAllocationViewModel.ReceiptAllocationID = receiptAllocation.ReceiptAllocationID;
                receiptAllocationViewModel.ProgramID = receiptAllocation.ProgramID;
                receiptAllocationViewModel.CommoditySourceID = receiptAllocation.CommoditySourceID;
                receiptAllocationViewModel.SourceHubID = receiptAllocation.SourceHubID;
                receiptAllocationViewModel.PurchaseOrder = receiptAllocation.PurchaseOrder;
                receiptAllocationViewModel.SupplierName = receiptAllocation.SupplierName;
                receiptAllocationViewModel.OtherDocumentationRef = receiptAllocation.OtherDocumentationRef;
                receiptAllocationViewModel.Remark = receiptAllocation.Remark;

                var shippingInstruction =
                        _shippingInstructionService.FindBy(t => t.Value == receiptAllocationViewModel.SINumber).
                            FirstOrDefault();
                var GC = new Cats.Models.Hubs.GiftCertificate();
                if (shippingInstruction != null)
                    GC = _giftCertificateService.FindBySINumber(shippingInstruction.ShippingInstructionID);
                if (GC != null && receiptAllocation.CommoditySourceID == CommoditySource.Constants.DONATION)
                {
                    receiptAllocationViewModel.Commodities.Clear();
                    receiptAllocationViewModel.Donors.Clear();
                    receiptAllocationViewModel.Programs.Clear();
                    foreach (GiftCertificateDetail giftCertificateDetail in GC.GiftCertificateDetails)
                    {
                        receiptAllocationViewModel.Commodities.Add(giftCertificateDetail.Commodity);
                    }
                    //Commodity commodity = receiptAllocationViewModel.Commodities.FirstOrDefault();
                    //if (commodity != null) receiptAllocationViewModel.CommodityID = commodity.CommodityID;
                    receiptAllocationViewModel.Donors.Add(GC.Donor);
                    receiptAllocationViewModel.DonorID = GC.DonorID;
                    receiptAllocationViewModel.Programs.Add(GC.Program);
                    receiptAllocationViewModel.ProgramID = GC.ProgramID;
                    receiptAllocationViewModel.CommoditySources.Clear();
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.DONATION));
                    receiptAllocationViewModel.CommoditySourceID = CommoditySource.Constants.DONATION;

                }
                //else
                //{
                int sourceType = receiptAllocation.CommoditySourceID;

                if (CommoditySource.Constants.DONATION == sourceType)
                {
                    receiptAllocationViewModel.CommoditySources.Clear();
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.DONATION));
                }
                //else if (CommoditySource.Constants.TRANSFER == sourceType)
                //{
                //    receiptAllocationViewModel.CommoditySources.Clear();
                //    receiptAllocationViewModel.CommoditySources.Add(
                //        _commoditySourceService.FindById(CommoditySource.Constants.TRANSFER));
                //}
                else if (CommoditySource.Constants.LOCALPURCHASE == sourceType)
                {
                    receiptAllocationViewModel.CommoditySources.Clear();
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.LOCALPURCHASE));
                }
                else if (CommoditySource.Constants.TRANSFER == sourceType ||
                        CommoditySource.Constants.REPAYMENT == sourceType ||
                        CommoditySource.Constants.LOAN == sourceType ||
                        CommoditySource.Constants.SWAP == sourceType)
                {
                    receiptAllocationViewModel.CommoditySources.Clear();
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.REPAYMENT));
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.LOAN));
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.SWAP));
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.TRANSFER));

                }
                // }
            }
            ViewBag.CommoditySourceType = receiptAllocation != null
                                              ? receiptAllocation.CommoditySourceID
                                              : CommoditySource.Constants.DONATION;

            if (receiptAllocation != null && receiptAllocation.Receives.Any())
                ViewBag.receiveUnderAllocation = true;

            return PartialView("Edit2", receiptAllocationViewModel);

        }
        [HttpPost]
        public ActionResult Edit2(ReceiptAllocationViewModel receiptAllocationViewModel)
        {
            if (receiptAllocationViewModel.CommoditySourceID == CommoditySource.Constants.DONATION)
            {
                ModelState.Remove("SourceHubID");
                ModelState.Remove("SupplierName");
                ModelState.Remove("PurchaseOrder");

            }
            else if (receiptAllocationViewModel.CommoditySourceID == CommoditySource.Constants.LOCALPURCHASE)
            {
                ModelState.Remove("DonorID");
                ModelState.Remove("SourceHubID");
            }
            else
            {
                ModelState.Remove("DonorID");
                ModelState.Remove("SupplierName");
                ModelState.Remove("PurchaseOrder");
            }


            if (!(IsSIValid(receiptAllocationViewModel.SINumber, receiptAllocationViewModel.CommoditySourceID)))
            {
                ModelState.AddModelError("SINumber", "");
            }

            if (ModelState.IsValid)
            {
                //ReceiptAllocation receiptAllocation = receiptAllocationViewModel.GenerateReceiptAllocation();
                ReceiptAllocation receiptAllocation =_receiptAllocationService.FindByID(Guid.Parse(receiptAllocationViewModel.ReceiptAllocationID.ToString()));
                FromReceiptAllocationViewModel(receiptAllocationViewModel, receiptAllocation);
                int typeOfGridToReload = receiptAllocation.CommoditySourceID;
                int commType = _commodityService.FindById(receiptAllocation.CommodityID).CommodityTypeID;
                //override to default hub
                UserProfile user = _userProfileService.GetUser(User.Identity.Name);
                receiptAllocation.HubID = user.DefaultHub.Value;
                if (typeOfGridToReload != Cats.Models.Hubs.CommoditySource.Constants.DONATION &&
                    typeOfGridToReload != Cats.Models.Hubs.CommoditySource.Constants.LOCALPURCHASE)
                {
                    typeOfGridToReload = Cats.Models.Hubs.CommoditySource.Constants.LOAN;
                }
                //TODO:Check savechanges -> EditRecieptAllocation
                _receiptAllocationService.EditReceiptAllocation(receiptAllocation);
                return Json(new {status=1, gridId = typeOfGridToReload, CommodityTypeID = commType }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            //return this.Create(receiptAllocationViewModel.CommoditySourceID);
            //ModelState.Remove("SINumber");
            //TODO:Check if commenting out has any effect
            //================================================
            // receiptAllocationViewModel.InitalizeViewModel();
            //=============================================
            return PartialView(receiptAllocationViewModel);
        }
        public ActionResult SIMustBeInGift(string SINUmber, int? CommoditySourceID)
        {
            if (CommoditySourceID.HasValue && CommoditySourceID.Value ==CommoditySource.Constants.DONATION)
            {
                var shippingInstruction = _shippingInstructionService.FindBy(t => t.Value == SINUmber).FirstOrDefault();
                var mustBeInGift = new Cats.Models.Hubs.GiftCertificate();
                if(shippingInstruction!=null)
                    mustBeInGift = _giftCertificateService.FindBySINumber(shippingInstruction.ShippingInstructionID);
                return Json((mustBeInGift != null), JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private bool IsSIValid(string SINumber, int CommoditySourceID)
        {
            //check allocation and gc for the same record
            var shippingInstruction = _shippingInstructionService.FindBy(t => t.Value == SINumber).FirstOrDefault();
            var fromGc = new Cats.Models.Hubs.GiftCertificate();
            if(shippingInstruction!=null)
                fromGc = _giftCertificateService.FindBySINumber(shippingInstruction.ShippingInstructionID);
            bool fromRall =
                _receiptAllocationService.IsSINSource(CommoditySource.Constants.DONATION, SINumber);

            if (CommoditySourceID ==CommoditySource.Constants.LOCALPURCHASE)
            {
                if
                    (!((fromGc == null) && !(fromRall)))
                {
                    return false;
                }
            }
            //just incase the user is bad
            bool fromRallt =
                _receiptAllocationService.IsSINSource(CommoditySource.Constants.LOCALPURCHASE, SINumber);

            if (CommoditySourceID ==CommoditySource.Constants.DONATION)
            {
                //var mustBeInGift = _giftCertificateService.FindBySINumber(SINumber);
                if
                    (fromRallt)
                {
                    //) && (mustBeInGift != null)))
                    return false;
                }

            }
            return true;
        }

        [HttpPost]
        public ActionResult Edit(ReceiptAllocationViewModel receiptAllocationViewModel)
        {
            if (receiptAllocationViewModel.CommoditySourceID ==CommoditySource.Constants.DONATION)
            {
                ModelState.Remove("SourceHubID");
                ModelState.Remove("SupplierName");
                ModelState.Remove("PurchaseOrder");

            }
            else if (receiptAllocationViewModel.CommoditySourceID ==CommoditySource.Constants.LOCALPURCHASE)
            {
                ModelState.Remove("DonorID");
                ModelState.Remove("SourceHubID");
            }
            else
            {
                ModelState.Remove("DonorID");
                ModelState.Remove("SupplierName");
                ModelState.Remove("PurchaseOrder");
            }


            if (!(IsSIValid(receiptAllocationViewModel.SINumber, receiptAllocationViewModel.CommoditySourceID)))
            {
                ModelState.AddModelError("SINumber", "");
            }

            if (ModelState.IsValid)
            {
               ReceiptAllocation receiptAllocation = receiptAllocationViewModel.GenerateReceiptAllocation();
                int typeOfGridToReload = receiptAllocation.CommoditySourceID;
                int commType = _commodityService.FindById(receiptAllocation.CommodityID).CommodityTypeID;
                //override to default hub
               UserProfile user = _userProfileService.GetUser(User.Identity.Name);
                receiptAllocation.HubID = user.DefaultHub.Value;
                if (typeOfGridToReload != Cats.Models.Hubs.CommoditySource.Constants.DONATION &&
                    typeOfGridToReload != Cats.Models.Hubs.CommoditySource.Constants.LOCALPURCHASE)
                {
                    typeOfGridToReload = Cats.Models.Hubs.CommoditySource.Constants.LOAN;
                }
                //TODO:Check savechanges -> EditRecieptAllocation
                _receiptAllocationService.EditReceiptAllocation(receiptAllocation);
                return Json(new {gridId = typeOfGridToReload, CommodityTypeID = commType}, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            //return this.Create(receiptAllocationViewModel.CommoditySourceID);
            //ModelState.Remove("SINumber");
            //TODO:Check if commenting out has any effect
            //================================================
           // receiptAllocationViewModel.InitalizeViewModel();
            //=============================================
            return PartialView(receiptAllocationViewModel);
        }

        public ActionResult LoadByAllocationIdPartial(string allocationId)
        {
            return RedirectToAction("Edit", new {allocationId = allocationId});
        }

        public ActionResult LoadBySIPartial(string SInumber, int? type)
        {
           UserProfile user = _userProfileService.GetUser(User.Identity.Name);
           
            var receiptAllocationViewModel = BindReceiptAllocaitonViewModel();;
            receiptAllocationViewModel.HubID = user.DefaultHub.Value;
            if (SInumber != null)
            {
                var shippingInstruction = _shippingInstructionService.FindBy(t => t.Value == SInumber).FirstOrDefault();
                var GC = new Cats.Models.Hubs.GiftCertificate();
                if(shippingInstruction!=null)
                    GC = _giftCertificateService.FindBySINumber(shippingInstruction.ShippingInstructionID);
                if (GC != null && type ==CommoditySource.Constants.DONATION)
                {
                    receiptAllocationViewModel.Commodities.Clear();
                    receiptAllocationViewModel.Donors.Clear();
                    receiptAllocationViewModel.Programs.Clear();
                    foreach (GiftCertificateDetail giftCertificateDetail in GC.GiftCertificateDetails)
                    {
                        receiptAllocationViewModel.Commodities.Add(giftCertificateDetail.Commodity);
                    }
                    //Commodity commodity = receiptAllocationViewModel.Commodities.FirstOrDefault();
                    //receiptAllocationViewModel.CommodityID = commodity.CommodityID;
                    receiptAllocationViewModel.Donors.Add(GC.Donor);
                    receiptAllocationViewModel.DonorID = GC.DonorID;
                    receiptAllocationViewModel.Programs.Add(GC.Program);
                    receiptAllocationViewModel.ProgramID = GC.ProgramID;
                    receiptAllocationViewModel.CommoditySources.Clear();
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.DONATION));
                    receiptAllocationViewModel.CommoditySourceID =CommoditySource.Constants.DONATION;
                    var hubs = new List<Cats.Models.Hubs.Hub>();
                    //foreach (var c in receiptAllocationViewModel.Hubs)
                    //{
                    //    var bySI = _receiptAllocationService.FindBySINumber(SInumber);

                    //    if(bySI.Find(p=>p.HubID == c.HubID) == null )
                    //    {
                    //        hubs.Add(c);    
                    //    }
                    //}

                    //if(hubs.Count() != 0)
                    //{
                    //    receiptAllocationViewModel.Hubs.Clear();
                    //    receiptAllocationViewModel.Hubs = hubs;
                    //}
                    hubs.Add(user.DefaultHubObj);
                    receiptAllocationViewModel.Hubs = hubs;

                    receiptAllocationViewModel.ETA = GC.ETA;
                    receiptAllocationViewModel.SINumber = SInumber;
                }
                else
                {
                    receiptAllocationViewModel.CommoditySources.Remove(
                        _commoditySourceService.FindById(CommoditySource.Constants.DONATION));
                    //remove the donor and add the 
                }
            }


            int sourceType =CommoditySource.Constants.DONATION;
            if (Request["type"] != null)
            {
                sourceType = Convert.ToInt32(Request["type"]);
            }

            //List<ReceiptAllocation> list = _receiptAllocationService.GetAllByType(sourceType);

            ViewBag.CommoditySourceType = sourceType;

            if (CommoditySource.Constants.DONATION == sourceType)
            {
                ViewBag.CommoditySourceTypeText = _commoditySourceService.FindById(sourceType).Name;
                receiptAllocationViewModel.CommoditySources.Clear();
                receiptAllocationViewModel.CommoditySources.Add(
                    _commoditySourceService.FindById(CommoditySource.Constants.DONATION));
                receiptAllocationViewModel.CommoditySourceID =CommoditySource.Constants.DONATION;

            }
            //else if (CommoditySource.Constants.TRANSFER == sourceType)
            //{
            //    ViewBag.CommoditySourceTypeText = _commoditySourceService.FindById(sourceType).Name;
            //    receiptAllocationViewModel.CommoditySources.Clear();
            //    receiptAllocationViewModel.CommoditySources.Add(
            //        _commoditySourceService.FindById(CommoditySource.Constants.TRANSFER));
            //    receiptAllocationViewModel.CommoditySourceID =CommoditySource.Constants.TRANSFER;
            //}
            else if (CommoditySource.Constants.LOCALPURCHASE == sourceType)
            {
                ViewBag.CommoditySourceTypeText = _commoditySourceService.FindById(sourceType).Name;
                receiptAllocationViewModel.CommoditySources.Clear();
                receiptAllocationViewModel.CommoditySources.Add(
                    _commoditySourceService.FindById(CommoditySource.Constants.LOCALPURCHASE));
                receiptAllocationViewModel.CommoditySourceID =CommoditySource.Constants.LOCALPURCHASE;
            }
            else if (CommoditySource.Constants.TRANSFER == sourceType ||
                    CommoditySource.Constants.REPAYMENT == sourceType ||
                    CommoditySource.Constants.LOAN == sourceType ||
                    CommoditySource.Constants.SWAP == sourceType)
            {
                ViewBag.CommoditySourceTypeText = "Loan, Repayment, transfer and Swap";
                receiptAllocationViewModel.CommoditySources.Clear();
                receiptAllocationViewModel.CommoditySources.Add(
                    _commoditySourceService.FindById(CommoditySource.Constants.REPAYMENT));
                receiptAllocationViewModel.CommoditySources.Add(
                    _commoditySourceService.FindById(CommoditySource.Constants.LOAN));
                receiptAllocationViewModel.CommoditySources.Add(
                    _commoditySourceService.FindById(CommoditySource.Constants.SWAP));
                receiptAllocationViewModel.CommoditySources.Add(
                    _commoditySourceService.FindById(CommoditySource.Constants.TRANSFER));
            }

            receiptAllocationViewModel.SINumber = SInumber;
            return PartialView("Create", receiptAllocationViewModel);

        }

        public ActionResult GetAvailableSINumbers()
        {
            //var shippingInstruction = _shippingInstructionService.FindBy(t => t.Value == SInumber).FirstOrDefault();
            //    var GC = new Cats.Models.Hubs.GiftCertificate();
            IEnumerable<SelectListItemModel> SINumbers;
            //if(shippingInstruction!=null)

            SINumbers = from item in _giftCertificateService.GetAllGiftCertificate()
                        select new SelectListItemModel()
                            {
                                Id = item.ShippingInstructionID.ToString(),
                                //GiftCertificateID.ToString(),
                                Name = item.ShippingInstruction.Value
                            };
            return Json(new SelectList(SINumbers.OrderBy(o => o.Name), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAvailableSINumbersAsText(bool? AllSIs, int commoditySoureType)
            //we can accept null and defualt to donation
        {
           UserProfile user = _userProfileService.GetUser(User.Identity.Name);

            if (AllSIs != null && AllSIs == true)
            {
                IEnumerable<SelectListItemModel> SINumbers = new List<SelectListItemModel>();
                IEnumerable<SelectListItemModel> SINumbersRA = new List<SelectListItemModel>();

                if (commoditySoureType == Cats.Models.Hubs.CommoditySource.Constants.DONATION)
                {
                    SINumbers = from item in _giftCertificateService.GetAllGiftCertificate()
                                select new SelectListItemModel()
                                           {
                                               Id = item.ShippingInstructionID.ToString(),
                                               Name = item.ShippingInstruction.Value,
                                               Collection = "0"
                                           };
                }
                else
                {
                    SINumbersRA =
                        from r in _receiptAllocationService.GetSIsWithOutGiftCertificate(commoditySoureType)
                        select new SelectListItemModel()
                                   {
                                       Id = r,
                                       Name = r,
                                       Collection = "1"
                                   };
                }
                var SINumbersAll = SINumbers.Union(SINumbersRA).Distinct(new UniqueNameComparer());

                return Json(SINumbersAll, JsonRequestBehavior.AllowGet);
            }
            else
            {
                IEnumerable<SelectListItemModel> SINumbersGCD = new List<SelectListItemModel>();
                IEnumerable<SelectListItemModel> SINumbersRA = new List<SelectListItemModel>();

                if (commoditySoureType == Cats.Models.Hubs.CommoditySource.Constants.DONATION)
                {
                    SINumbersGCD = from p in _giftCertificateDetailService.GetUncommitedSIs()
                                   select new SelectListItemModel()
                                              {
                                                  Id = p,
                                                  Name = p,
                                                  Collection = "0"
                                              };
                }
                else
                {
                    SINumbersRA =
                        from r in _receiptAllocationService.GetSIsWithOutGiftCertificate(commoditySoureType)
                        select new SelectListItemModel()
                                   {
                                       Id = r,
                                       Name = r,
                                       Collection = "1"
                                   };
                }
                var SINumbersUnCommited = SINumbersGCD.Union(SINumbersRA).Distinct(new UniqueNameComparer());

                return Json(SINumbersUnCommited, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult GetSIBalances()
        {
            var list = _giftCertificateService.GetSIBalances();
            return PartialView("SIBalance", list);
        }


        [HttpPost]
        public ActionResult CommitAllocation(string[] checkedRecords, int? SINumber)
        {
           UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            if (checkedRecords != null)
            {
                _receiptAllocationService.CommitReceiveAllocation(checkedRecords, user);
            }

            //return AllocationList(SINumber.Value);
            return RedirectToAction("Index", new {si = SINumber}); //return View("Index");
        }

        public ActionResult GetBalance(string siNumber, int? commodityId) //, int? hubID)
        {

            if (siNumber != null) // && commodityId.HasValue)
            {
               UserProfile user = _userProfileService.GetUser(User.Identity.Name);
                decimal GCbalance = 0;
                decimal allocateBalance = 0;
                string commodity = "";
                var shippingInstruction = _shippingInstructionService.FindBy(t => t.Value == siNumber).FirstOrDefault();
                var GC = new Cats.Models.Hubs.GiftCertificate();
                if(shippingInstruction!=null)
                    GC = _giftCertificateService.FindBySINumber(shippingInstruction.ShippingInstructionID);
                if (GC != null && shippingInstruction!=null)
                {

                    foreach (var GCD in GC.GiftCertificateDetails)
                    {
                        GCbalance = GCbalance + GCD.WeightInMT;
                        commodity = GCD.Commodity.Name;
                    }

                    allocateBalance = _receiptAllocationService.GetBalanceForSI(shippingInstruction.Value); //, commodityId.Value);

                }
                //else
                //{
                //    GCbalance = 1;
                //    allocateBalance = 0;
                //    //balance = 1;
                //}
                decimal balance = GCbalance - allocateBalance;
                //TODO: make sure this function works for multi row gift certificate details.
                return Json(new {commodity, total = GCbalance, balance = balance}, JsonRequestBehavior.AllowGet);
            }
            return new EmptyResult();
        }

        public ActionResult Delete(string id)
        {
            var delAllocation = _receiptAllocationService.FindById(Guid.Parse(id));
            return View("Delete", delAllocation);
        }

        //
        // POST: /Commodity/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {

            var delAllocation = _receiptAllocationService.FindById(Guid.Parse(id));
            int typeOfallocation = delAllocation.CommoditySourceID;
            if (delAllocation != null)
            {
                _receiptAllocationService.DeleteByID(Guid.Parse(id));
                return RedirectToAction("Index", "ReceiptAllocation", new {type = typeOfallocation});
            }

            ViewBag.ERROR_MSG = "This Allocation is being referenced, so it can't be deleted";
            ViewBag.ERROR = true;
            return this.Delete(id);
            //return View("Delete", delAllocation); //this.Delete(id);

        }

        public ActionResult Close(string id)
        {
            var delAllocation = _receiptAllocationService.FindById(Guid.Parse(id));
            return PartialView("Close", delAllocation);
        }

        [HttpPost]
        public ActionResult CloseAjax(string ReceiptAllocationID)
        {
            var delAllocation = _receiptAllocationService.FindById(Guid.Parse(ReceiptAllocationID));

            if (delAllocation != null)
            {
                _receiptAllocationService.CloseById(Guid.Parse(ReceiptAllocationID));
                return Json(new { status = 1}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = 0}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Close")]
        public ActionResult CloseConfirmed(string id)
        {

            var delAllocation = _receiptAllocationService.FindById(Guid.Parse(id));

            if (delAllocation != null)
            {
                int typeOfGridToReload = delAllocation.CommoditySourceID;
                int commType = delAllocation.Commodity.CommodityTypeID;
                _receiptAllocationService.CloseById(Guid.Parse(id));
                //return the type of the allocation closed so that we can reload that respective grid(i.e. not every grid)
                if (typeOfGridToReload != Cats.Models.Hubs.CommoditySource.Constants.DONATION &&
                    typeOfGridToReload != Cats.Models.Hubs.CommoditySource.Constants.LOCALPURCHASE)
                {
                    typeOfGridToReload = Cats.Models.Hubs.CommoditySource.Constants.LOAN;
                }
                return Json(new {gridId = typeOfGridToReload, CommodityTypeID = commType}, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("index", "Receive");
            }
            return this.Close(id);
        }

        public ActionResult LocalPurchase()
        {
            return View();
        }

        public ActionResult FromHubs()
        {
            return View();
        }

        public ActionResult Loans()
        {
            return View();
        }

        public ActionResult GenerateProjectCode(string SINumber,int? DonorID,int? CommodityID, Decimal? QuantityInMT){ 

        //“<Donor Code>-<Commodity Code>-<Quantity-Allocated-To-Hub>/<Quantity-On-Gift-Certificate> 
        
            string projectCode = null;
            
            if (DonorID != null)
            {
                Donor repositoryDonorFindById = _donorService.FindById(DonorID.Value);
                if (repositoryDonorFindById != null && repositoryDonorFindById.DonorCode != null)
                    projectCode += repositoryDonorFindById.DonorCode.ToUpperInvariant();
            }

            if (CommodityID != null)
            {
                Commodity repositoryCommodityFindById = _commodityService.FindById(CommodityID.Value);
                if (repositoryCommodityFindById != null && repositoryCommodityFindById.CommodityCode != null)
                {
                    projectCode += "-" + repositoryCommodityFindById.CommodityCode.ToUpperInvariant();

                }
            }

            projectCode += "-" + ((QuantityInMT ?? 0).ToString()).ToUpperInvariant();
            var shippingInstruction = _shippingInstructionService.FindBy(t => t.Value == SINumber).FirstOrDefault();
            if (shippingInstruction!=null && _giftCertificateService.FindBySINumber(shippingInstruction.ShippingInstructionID) != null &&
                    _giftCertificateService.FindBySINumber(shippingInstruction.ShippingInstructionID).GiftCertificateDetails.Any(
                        p => p.CommodityID == CommodityID))
                    projectCode += "/" +
                                    _giftCertificateService.FindBySINumber(shippingInstruction.ShippingInstructionID).GiftCertificateDetails.Where(
                                        p => p.CommodityID == CommodityID).Sum(q => q.WeightInMT);

            return Json(projectCode , JsonRequestBehavior.AllowGet );
        }
        
    }
}
