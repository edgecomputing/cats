using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Logistics;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Cats.ViewModelBinder;
using log4net;
using Cats.Areas.Logistics.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Services.Security;
using Cats.Helpers;
using Cats.Infrastructure;

namespace Cats.Areas.Logistics.Controllers
{
    [Authorize]
    public class PromisedContributionController : Controller
    {
        private readonly IPromisedContributionService promisedContributionService;
        private readonly IUserAccountService userAccountService;
        private readonly IDonorService donorService;
        private readonly IHubService hubService;
        private readonly ICommodityService commodityService;



        public PromisedContributionController(IPromisedContributionService _PromisedContributionService,
            IUserAccountService _userAccountService,
            IDonorService _donorService,
            IHubService _hubService,
            ICommodityService _commodityService
            )
            {

                this.promisedContributionService = _PromisedContributionService;
                this.userAccountService = _userAccountService;
                this.donorService = _donorService;
                this.hubService = _hubService;
                this.commodityService = _commodityService;
            }
        public ActionResult Index()
        {
            return View(GetAllContribution());
        }
        public ActionResult Edit(int id = 0)
        {
            PromisedContribution contribution = new PromisedContribution();
            contribution.ExpectedTimeOfArrival = System.DateTime.Now;
            if (id != 0)
            {
                contribution = promisedContributionService.FindById(id);
            }
            loadListItems();
            return View(contribution);
        }
        [HttpPost]
        public ActionResult Edit(PromisedContribution contribution)
        {
            if (contribution != null && ModelState.IsValid)
            {
                try
                {
                    if (contribution.PromisedContributionId > 0)
                    {
                        promisedContributionService.Update(contribution);
                        ViewBag.Message = "Updated";
                    }
                    else
                    {
                        promisedContributionService.Create(contribution);
                        ViewBag.Message = "Added";
                    }
                        return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                }
            }
            loadListItems();
            return View(contribution);
        }
        public ActionResult Delete(int id = 0)
        {
            promisedContributionService.DeleteById(id);
            return RedirectToAction("Index");
        }
        public void loadListItems()
        {
           // commodityService.FindById(1).CommodityID;
         
            ViewBag.DonorId = new SelectList(donorService.GetAllDonor(), "DonorID", "Name");
            ViewBag.HubId = new SelectList(hubService.GetAllHub(), "HubID", "Name");
            ViewBag.CommodityId = new SelectList(commodityService.GetAllCommodity(), "CommodityID", "Name");
        }

        public List<PromisedContributionViewModel> GetAllContribution()
        {
            List<PromisedContribution> list = this.promisedContributionService.GetAll();
            return convertToViewModelList(list);
        }
        public ActionResult ReadAjax([DataSourceRequest] DataSourceRequest request)
        {
            List<PromisedContribution> list= this.promisedContributionService.GetAll();
            List<PromisedContributionViewModel> listPOCO = convertToViewModelList(list);


            return Json(listPOCO.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            //return jsonstrin
        }
        public List<PromisedContributionViewModel> convertToViewModelList(List<PromisedContribution> list)
        {
            List<PromisedContributionViewModel> listPOCO = new List<PromisedContributionViewModel>();

            foreach (PromisedContribution item in list)
            {
                listPOCO.Add(convertToViewModel(item));
            }
            return listPOCO;
        }
        public PromisedContributionViewModel convertToViewModel(PromisedContribution item)
        {
            string userPreference = userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return new PromisedContributionViewModel
            {
                PromisedContributionId = item.PromisedContributionId,
                PromisedQuantity=item.PromisedQuantity,
                ExpectedTimeOfArrival=item.ExpectedTimeOfArrival,
                strETA = item.ExpectedTimeOfArrival.ToCTSPreferedDateFormat(userPreference),
                DeliveredQuantity=item.DeliveredQuantity,
                DonorId=item.DonorId,
                HubId=item.HubId,
                CommodityId=item.CommodityId,
                CommodityName=item.Commodity.Name,
                HubName=item.Hub.Name,
                DonorName=item.Donor.Name
            };

        }
    }
}