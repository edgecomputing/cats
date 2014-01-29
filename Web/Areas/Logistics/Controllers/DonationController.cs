using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Cats.Services.Security;
using Cats.Services.Common;
namespace Cats.Areas.Logistics.Controllers
{
    public class DonationController : Controller
    {
        //
        // GET: /Logistics/Donation/
        private readonly IReceiptAllocationService _receiptAllocationService;
        private readonly IUserAccountService _userAccountService;
        private readonly IGiftCertificateService _giftCertificateService;
        private readonly ICommodityService _commodityService;
        private readonly ICommodityTypeService _commodityTypeService;
        private readonly IProgramService _programService;
        private readonly IDonorService _donorService;
        private readonly IHubService _hubService;
       
        public DonationController(IReceiptAllocationService receiptAllocationService,IGiftCertificateService giftCertificateService, IUserAccountService userAccountService, ICommodityService commodityService, ICommodityTypeService commodityTypeService, IProgramService programService, IDonorService donorService, IHubService hubService)
        {
            _receiptAllocationService = receiptAllocationService;
            _giftCertificateService = giftCertificateService;
            _userAccountService = userAccountService;
            _commodityService = commodityService;
            _commodityTypeService = commodityTypeService;
            _programService = programService;
            _donorService = donorService;
            _hubService = hubService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadReceiptAllocation()
        {
            try
            {
                var user = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);
                List<ReceiptAllocation> list = _receiptAllocationService.GetUnclosedAllocationsDetached(user.DefaultHub.HubID, type, closedToo, user.PreferedWeightMeasurment, CommodityType);
               
            }
            catch (Exception ex)
            {
                return View();

            }
        }
        public ActionResult LoadBySi(string siNumber, int? type)
        {
         
            var user = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);

            if (siNumber != null)
            {
                var receiptAllocationViewModel = BindReceiptAllocaitonViewModel(); ;
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
                   
                    receiptAllocationViewModel.Donors.Add(gc.Donor);
                    receiptAllocationViewModel.DonorID = gc.DonorID;
                    receiptAllocationViewModel.Programs.Add(gc.Program);
                    receiptAllocationViewModel.ProgramID = gc.ProgramID;
                    receiptAllocationViewModel.CommoditySources.Clear();
                   receiptAllocationViewModel.CommoditySourceID = Cats.Models.Constant.CommoditySourceConst.Constants.DONATION;
                 

                   

                    receiptAllocationViewModel.ETA = gc.ETA;
                    receiptAllocationViewModel.SINumber = siNumber;

                }
               
            }


           
            return PartialView("Create", receiptAllocationViewModel);

        }

        private ReceiptAllocationViewModel BindReceiptAllocaitonViewModel()
        {
          
            var commodities = _commodityService.GetAllCommodity().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var donors = _donorService.GetAllDonor().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var hubs = new List<Cats.Models.Hubs.Hub>();
           
            var programs = _programService.GetAllProgram().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
          
            var commodityTypes = _commodityTypeService.GetAllCommodityType().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var viewModel = new ReceiptAllocationViewModel(commodities, donors, programs, commodityTypes);
         
            return viewModel;
        }

    }
}
