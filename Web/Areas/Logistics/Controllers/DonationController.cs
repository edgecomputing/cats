using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CommonService = Cats.Services.Common.CommonService;
using ICommodityTypeService = Cats.Services.EarlyWarning.ICommodityTypeService;
using IDonorService = Cats.Services.EarlyWarning.IDonorService;
using IHubService = Cats.Services.EarlyWarning.IHubService;
using IProgramService = Cats.Services.EarlyWarning.IProgramService;

namespace Cats.Areas.Logistics.Controllers
{
    public class DonationController : Controller
    {
        //
        // GET: /Logistics/Donation/
        private readonly IReceiptAllocationService _receiptAllocationService;
        private readonly IUserAccountService _userAccountService;
        private readonly GiftCertificateService _giftCertificateService;
        private readonly Cats.Services.Hub.ICommodityService _commodityService;
        private readonly Cats.Services.Hub.ICommodityTypeService _commodityTypeService;
        private readonly Cats.Services.Hub.IProgramService _programService;
        private readonly Cats.Services.Hub.IDonorService _donorService;
        private readonly Cats.Services.Hub.IHubService _hubService;
        private readonly CommonService _commonService;
        private readonly IShippingInstructionService _shippingInstructionService;
        private readonly IGiftCertificateDetailService _giftCertificateDetailService;
        private readonly ICommoditySourceService _commoditySourceService;
       
        public DonationController(
            IReceiptAllocationService receiptAllocationService,
         IUserAccountService userAccountService,
         Cats.Services.Hub.ICommodityService commodityService, 
          CommonService commonService, 
            IShippingInstructionService shippingInstructionService, 
            IGiftCertificateDetailService giftCertificateDetailService, 
            ICommoditySourceService commoditySourceService, GiftCertificateService giftCertificateService, Services.Hub.IDonorService donorService, Services.Hub.IProgramService programService, Services.Hub.ICommodityTypeService commodityTypeService, Services.Hub.IHubService hubService)
        {
            _receiptAllocationService = receiptAllocationService;
          
            _userAccountService = userAccountService;
            _commodityService = commodityService;
           
          
          
         
            _commonService = commonService;
            _shippingInstructionService = shippingInstructionService;
            _giftCertificateDetailService = giftCertificateDetailService;
            _commoditySourceService = commoditySourceService;
            _giftCertificateService = giftCertificateService;
            _donorService = donorService;
            _programService = programService;
            _commodityTypeService = commodityTypeService;
            _hubService = hubService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadReceiptAllocation([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var user = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);
                List<Cats.Models.Hubs.ReceiptAllocation> list = _receiptAllocationService.GetUnclosedAllocationsDetached(3,1,false,user.PreferedWeightMeasurment,1);
                var receiptViewModel = ReceiptAllocationViewModelBinder.BindReceiptAllocationViewModel(list);
                return Json(receiptViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public ActionResult AddNewDonationPlan()
        {
           // PopulateLookup();
            var receiptAllocationViewModel = BindReceiptAllocaitonViewModel();
            return PartialView("AddNewDonationPlan", receiptAllocationViewModel);
        }

        [HttpPost]
        public ActionResult AddNewDonationPlan(Cats.Models.Hubs.ReceiptAllocationViewModel receiptAllocationViewModel)
        {

            ModelState.Remove("SourceHubID");
            ModelState.Remove("SupplierName");
            ModelState.Remove("PurchaseOrder");
           

            
            if (ModelState.IsValid)
            {
                var receiptAllocation = receiptAllocationViewModel.GenerateReceiptAllocation();
                
                if (receiptAllocationViewModel.GiftCertificateDetailID == 0 || receiptAllocationViewModel.GiftCertificateDetailID == null)
                {
                    var shippingInstruction =_shippingInstructionService.FindBy(t => t.Value == receiptAllocationViewModel.SINumber).FirstOrDefault();
                    var gc = new Cats.Models.Hubs.GiftCertificate();
                    if (shippingInstruction != null)
                        gc = _giftCertificateService.FindBySINumber(shippingInstruction.Value);

                    if (gc != null)
                    {
                        var gcd =gc.GiftCertificateDetails.FirstOrDefault(p => p.CommodityID == receiptAllocationViewModel.CommodityID);
                        if (gcd != null) 
                        {
                            receiptAllocation.GiftCertificateDetailID = gcd.GiftCertificateDetailID;
                        }
                    }
                    else
                    {
                        receiptAllocation.GiftCertificateDetailID = null;
                    }
                }
               
               

                receiptAllocation.HubID = receiptAllocationViewModel.HubID;
                receiptAllocation.CommoditySourceID =Cats.Models.Hubs.CommoditySource.Constants.DONATION;
              
                receiptAllocation.ReceiptAllocationID = Guid.NewGuid();
                _receiptAllocationService.AddReceiptAllocation(receiptAllocation);

                return RedirectToAction("Index");
               
                
            }
            

            return RedirectToAction("Index");

        }
        private void PopulateLookup()
        {
           ViewBag.RegionID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 2), "AdminUnitID","Name");
           ViewBag.ProgramId = new SelectList(_commonService.GetPrograms().Take(2), "ProgramID", "Name");
           ViewBag.DonorID = new SelectList(_commonService.GetDonors(), "DonorId", "Name");
           ViewBag.HubID = new SelectList(_hubService.GetAllHub(), "HubID", "Name");
           ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name");
           ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity(), "CommodityID", "Name");

        }

        public ActionResult LoadBySi(string siNumber, int? type)
        {
         
            var user = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);
            var receiptAllocationViewModel = BindReceiptAllocaitonViewModel(); 
            if (siNumber != null)
            {
               
                var gc = _giftCertificateService.FindBySINumber(siNumber);
                if (gc != null && type == Cats.Models.Constant.CommoditySourceConst.Constants.DONATION)
                {
                    receiptAllocationViewModel.Commodities.Clear();
                    receiptAllocationViewModel.Donors.Clear();
                    receiptAllocationViewModel.Programs.Clear();

                    
                    foreach (var giftCertificateDetail in gc.GiftCertificateDetails)
                    {
                        receiptAllocationViewModel.Commodities.Add(giftCertificateDetail.Commodity);
                    }
                   
                    receiptAllocationViewModel.Donors.Add( gc.Donor);
                    receiptAllocationViewModel.DonorID = gc.DonorID;
                    receiptAllocationViewModel.Programs.Add(gc.Program);
                    receiptAllocationViewModel.ProgramID = gc.ProgramID;
                    receiptAllocationViewModel.CommoditySources.Clear();
                   receiptAllocationViewModel.CommoditySourceID = Cats.Models.Constant.CommoditySourceConst.Constants.DONATION;
                 

                   

                    receiptAllocationViewModel.ETA = gc.ETA;
                    receiptAllocationViewModel.SINumber = siNumber;

                }
               
            }



            return PartialView("AddNewDonationPlan", receiptAllocationViewModel);

        }

        private Cats.Models.Hubs.ReceiptAllocationViewModel BindReceiptAllocaitonViewModel()
        {

            var commodities = _commodityService.GetAllCommodity().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var donors = _donorService.GetAllDonor().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var hubs = _hubService.GetAllHub();
            
            var programs = _programService.GetAllProgram().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var commoditySource = _commoditySourceService.GetAllCommoditySource().OrderBy(o => o.Name).ToList();
            var commodityTypes = _commodityTypeService.GetAllCommodityType().OrderBy(o => o.Name).ToList();
            var viewModel = new Cats.Models.Hubs.ReceiptAllocationViewModel(commodities, donors,hubs, programs,commoditySource, commodityTypes,null);

            return viewModel;
        }

    }
}
