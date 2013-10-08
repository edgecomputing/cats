using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class DonorControllerTests
    {
        #region SetUp / TearDown

        private DonorController _donorController;
        [SetUp]
        public void Init()
        {
            var donors = new List<Donor>
                {
                    new Donor{DonorID = 1, Name = "UN - World Food Program", DonorCode = "WFP", IsResponsibleDonor = true, IsSourceDonor = true, LongName = "UN - World Food Program"},
                    new Donor{DonorID = 2, Name = "UK", DonorCode = "UK", IsResponsibleDonor = true, IsSourceDonor = true, LongName = "United Kingdom"},
                };
            var donorService = new Mock<IDonorService>();
            donorService.Setup(t => t.GetAllDonor()).Returns(donors);
            var userProfileService = new Mock<IUserProfileService>();
            _donorController = new DonorController(donorService.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _donorController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var viewResult = _donorController.Index();
            Assert.NotNull(viewResult);
            var model = viewResult.Model;

            //ASSERT
            Assert.IsInstanceOf<Donor>(model);
            Assert.AreEqual(2, ((IEnumerable<Donor>)model).Count());
        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var viewResult = _donorController.Details(1);
            Assert.NotNull(viewResult);
            var model = viewResult.Model;

            //ASSERT
            Assert.IsInstanceOf<Donor>(model);
        }

        [Test]
        public void CanCreatePostBack()
        {
            //ACT
            var donor = new Donor
                {
                    Name = "USAID",
                    DonorCode = "UAD",
                    IsResponsibleDonor = false,
                    IsSourceDonor = true,
                    LongName = "United States Agency For International Development"
                };
            var jsonResult = _donorController.Create(donor) as JsonResult;
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanEditPostBack()
        {
            //ACT
            var donor = new Donor
            {
                DonorID = 3,
                Name = "USAID",
                DonorCode = "UAD",
                IsResponsibleDonor = true,
                IsSourceDonor = true,
                LongName = "United States Agency For International Development"
            };
            var jsonResult = _donorController.Edit(donor) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanDoDeleteConfirmed()
        {
            //ACt
            var redirectToRouteResult = _donorController.DeleteConfirmed(1) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Donor", redirectToRouteResult.RouteValues["controller"]);
        }

        #endregion
    }
}
