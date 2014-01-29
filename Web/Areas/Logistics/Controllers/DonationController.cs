using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Hub;
using UserProfile = Cats.Models.Hubs.UserProfile;

namespace Cats.Areas.Logistics.Controllers
{
    public class DonationController : Controller
    {
        //
        // GET: /Logistics/Donation/
        private readonly ReceiptAllocationService _receiptAllocationService;
        private readonly IUserProfileService _userProfileService;

        public DonationController(ReceiptAllocationService receiptAllocationService, IUserProfileService userProfileService)
        {
            _receiptAllocationService = receiptAllocationService;
            _userProfileService = userProfileService;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult LoadBySIPartial(string SInumber, int? type)
        {
         
            var user = _userProfileService.GetUser(User.Identity.Name);
          
            receiptAllocationViewModel.HubID = user.DefaultHub.HubID;
            if (SInumber != null)
            {
                Models.Hubs.GiftCertificate GC = _giftCertificateService.FindBySINumber(SInumber);
                if (GC != null && type == CommoditySource.Constants.DONATION)
                {
                    receiptAllocationViewModel.Commodities.Clear();
                    receiptAllocationViewModel.Donors.Clear();
                    receiptAllocationViewModel.Programs.Clear();
                    foreach (GiftCertificateDetail giftCertificateDetail in GC.GiftCertificateDetails)
                    {
                        receiptAllocationViewModel.Commodities.Add(giftCertificateDetail.Commodity);
                    }
                   
                    receiptAllocationViewModel.Donors.Add(GC.Donor);
                    receiptAllocationViewModel.DonorID = GC.DonorID;
                    receiptAllocationViewModel.Programs.Add(GC.Program);
                    receiptAllocationViewModel.ProgramID = GC.ProgramID;
                    receiptAllocationViewModel.CommoditySources.Clear();
                    receiptAllocationViewModel.CommoditySources.Add(
                        _commoditySourceService.FindById(CommoditySource.Constants.DONATION));
                    receiptAllocationViewModel.CommoditySourceID = CommoditySource.Constants.DONATION;
                    var hubs = new List<Cats.Models.Hubs.Hub>();
                   
                    hubs.Add(user.DefaultHub);
                    receiptAllocationViewModel.Hubs = hubs;

                    receiptAllocationViewModel.ETA = GC.ETA;
                    receiptAllocationViewModel.SINumber = SInumber;
                }
                else
                {
                    receiptAllocationViewModel.CommoditySources.Remove(
                        _commoditySourceService.FindById(CommoditySource.Constants.DONATION));
                    
                }
            }


            int sourceType = CommoditySource.Constants.DONATION;
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
                receiptAllocationViewModel.CommoditySourceID = CommoditySource.Constants.DONATION;

            }
           
            

            receiptAllocationViewModel.SINumber = SInumber;
            return PartialView("Create", receiptAllocationViewModel);

        }


    }
}
