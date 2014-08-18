using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
        private Guid _receiptAllocationId;

        public ReceiveNewController(IUserProfileService userProfileService,
            IReceiptAllocationService receiptAllocationService,
            IReceiveService receiveService,
            ICommodityService commodityService)
            : base(userProfileService)
        {
            _userProfileService = userProfileService;
            _receiptAllocationService = receiptAllocationService;
            _receiveService = receiveService;
            _commodityService = commodityService;
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
                    return View(viewModel);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(ReceiveNewViewModel viewModel)
        {
            //Todo: Implementation of Transaction on receive 
            return View();
        }

        [HttpGet]
        public JsonResult AutoCompleteCommodity(string term)
        {
            var result = (from commodity in _commodityService.GetAllCommodity()
                          where commodity.Name.ToLower().StartsWith(term.ToLower())
                          select commodity.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
