using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Models.ViewModels;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Logistics.Controllers
{
     [Authorize]
    public class UnContractedTransportersController : Controller
    {

        #region Fields

            private readonly IBidWinnerService _bidWinnerService;
            private readonly IUserAccountService _userAccountService;
            private readonly IBidService _bidService;
            private readonly IAdminUnitService _adminUnitService;

        #endregion

        #region Ctor

        public UnContractedTransportersController(IBidWinnerService bidWinnerService, 
                                                    IUserAccountService userAccountService, 
                                                    IBidService bidService,
                                                    IAdminUnitService adminUnitService)
        {

            _bidWinnerService = bidWinnerService;
            _userAccountService = userAccountService;
            _bidService = bidService;
            _adminUnitService = adminUnitService;

        }

        #endregion

        #region ActionMethods

       
        public ActionResult List(int id)
        {
            ViewData["ID"] = id;
            
            return View();
        }


        public ActionResult GetList([DataSourceRequest]DataSourceRequest request,int id)
        {

            IList<UnContractedTransportersListViewModel> selectedUnSignedBidWinners = new List<UnContractedTransportersListViewModel>();
          
            Bid selectedBid = _bidService.FindBy(t => t.BidID == id ).FirstOrDefault();

            var unSignedbidWinner = _bidWinnerService.FindBy(m => m.BidID == selectedBid.BidID
                                                            && m.ExpiryDate > DateTime.Now
                                                            && m.Status != (int)BidWinnerStatus.Signed);

            if (unSignedbidWinner != null && unSignedbidWinner.Any())
                selectedUnSignedBidWinners = GetBidWinners(unSignedbidWinner).ToList();


            return Json(selectedUnSignedBidWinners.ToDataSourceResult(request),JsonRequestBehavior.AllowGet);//,JsonRequestBehavior.AllowGet
        }

        #endregion

        #region Methods
         
        private IEnumerable<UnContractedTransportersListViewModel> GetBidWinners(IEnumerable<BidWinner> bidWinners)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;

            return (from bidWinner in bidWinners
                    select new UnContractedTransportersListViewModel()
                    {

                        TransporterId = bidWinner.TransporterID,
                        Name = bidWinner.Transporter.Name,
                        RegionName = _adminUnitService.FindById(bidWinner.Transporter.Region).Name,
                        SubCity = bidWinner.Transporter.SubCity,
                        ZoneName = _adminUnitService.FindById(bidWinner.Transporter.Zone).Name,
                        TelephoneNo = bidWinner.Transporter.TelephoneNo,
                        Capital = bidWinner.Transporter.Capital,
                        MobileNo = bidWinner.Transporter.MobileNo,
                        Destination = _adminUnitService.FindById(bidWinner.DestinationID).Name,
                        Source = _adminUnitService.FindById(bidWinner.SourceID).Name,
                        Fdp = bidWinner.AdminUnit.Name

                    }).Distinct();
        }

        #endregion

    }
}
