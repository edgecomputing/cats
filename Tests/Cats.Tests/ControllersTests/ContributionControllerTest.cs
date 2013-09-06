using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    public class ContributionControllerTest
    {
        private ContributionController _contributionController;

#region Setup
        [SetUp]
        public void Init()
        {
            var contribution = new List<Contribution>
                {
                    new Contribution {ContributionID = 1,HRDID = 1,DonorID = 1,Year = 2012},
                    new Contribution {ContributionID = 2,HRDID = 2,DonorID = 1,Year = 2012}

                };


            var contributionService = new Mock<IContributionService>();
            contributionService.Setup(m => m.GetAllContribution()).Returns(contribution);


            var contributionDetail = new List<ContributionDetail>()
                {
                    new ContributionDetail
                        {
                            ContributionDetailID = 1,
                            ContributionID = 1,
                            CurrencyID = 1,
                            PledgeDate = new DateTime(12/12/12),
                            PledgeReferenceNo = "ADE123",
                            Amount = 100
                        },

                         new ContributionDetail
                        {
                            ContributionDetailID = 2,
                            ContributionID = 1,
                            CurrencyID = 2,
                            PledgeDate = new DateTime(12/12/12),
                            PledgeReferenceNo = "ADE123",
                            Amount = 100
                        }
                };

            var contributionDetailService = new Mock<IContributionDetailService>();
            contributionDetailService.Setup(m => m.GetAllContributionDetail()).Returns(contributionDetail);

            var donor = new List<Donor>()
                {
                    new Donor {DonorID = 1, DonorCode = "USAID"},
                    new Donor {DonorID = 1, DonorCode = "WFP"},
                };

            var donorService = new Mock<IDonorService>();
            donorService.Setup(m => m.GetAllDonor()).Returns(donor);

            var hrd = new List<HRD>
                {
                    new HRD { HRDID =1,RationID =1,SeasonID = 1, Status = 1,CreatedBY = 1,CreatedDate =new DateTime(12/12/2012),
                              PublishedDate =new DateTime(12/12/2013),Year = 2012},
                    new HRD { HRDID =2,RationID =1,SeasonID = 2, Status = 2,CreatedBY = 2,CreatedDate =new DateTime(12/12/2012),
                              PublishedDate =new DateTime(12/12/2013),Year = 2012},
                   
                };

            var hrdService = new Mock<IHRDService>();
            hrdService.Setup(m => m.GetAllHRD()).Returns(hrd);

            var currency = new List<Currency>
                {
                    new Currency {CurrencyID = 1,Code = "USD",Name = "US Dollar"},
                    new Currency {CurrencyID = 2,Code = "Birr",Name = "Ethiopian Birr"}
                };
            var currencyService = new Mock<ICurrencyService>();
            currencyService.Setup(m => m.GetAllCurrency()).Returns(currency);

            _contributionController=new ContributionController(contributionService.Object,contributionDetailService.Object,
                                   donorService.Object,currencyService.Object,hrdService.Object);

        }

        [TearDown]
        public void Dispose()
        {
            _contributionController.Dispose();
        }
#endregion
        
        #region Tests
        //[Test]
        //public void CanReadContribution()
        //{
        //    var contribution = new DataSourceRequest();

        //    var result = (JsonResult)_contributionController.Contribution_Read(contribution);

        //    Assert.IsNotNull(result);
        //    // Assert.AreEqual(1, (((DataSourceResult)result.Data).Total));

        //}
        [Test]
        public void CanShowIndex()
        {
            ActionResult actual = _contributionController.Index();
            ViewResult result = actual as ViewResult;
            Assert.IsNotNull(result);
        }
        [Test]
        public void CanReadContributionDetail()
        {
            var contributionDetail = new DataSourceRequest();
            var id = 2;

            var result = (RedirectToRouteResult)_contributionController.ContributionDetail_Read(contributionDetail, id);

            Assert.IsNotNull(result);

        }
        [Test]
        public void CanDeleteContribution()
        {
            var id = 1;
            var result = (RedirectToRouteResult)_contributionController.Delete(id);
            
            Assert.IsNotNull(result);
            //Assert.IsInstanceOf<ContributionDetail>(result.Model);
        }
        #endregion
        //[Test]
        //public void CanShowContributionDetails()
        //{
        //    var id = 1;
        //    var result = (RedirectToRouteResult)_contributionController.Details(id);
        //    Assert.IsNotNull(result);
        //}
    }
}
