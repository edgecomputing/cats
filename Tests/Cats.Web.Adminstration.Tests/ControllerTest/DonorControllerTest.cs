using System.Collections.Generic;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Controllers;
using Cats.Web.Adminstration.Models.ViewModels;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Adminstration.Tests.ControllerTest
{
    [TestFixture]
    public class DonorControllerTest
    {
        private DonorController _donorController;

        #region Setup
        [SetUp]
        public void init()
        {
            var donor = new List<Donor>
         {
             new Donor {DonorID = 1,DonorCode = "WFP",Name = "World Food Program",IsResponsibleDonor=true,IsSourceDonor = false,LongName = "World Food Program"},
             new Donor {DonorID = 2,DonorCode = "UN",Name = "United Nations",IsResponsibleDonor=false,IsSourceDonor = true,LongName = "United Nations"}
         };
            var donorService = new Mock<IDonorService>();
            donorService.Setup(m => m.GetAllDonor()).Returns(donor);

            _donorController = new DonorController(donorService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _donorController.Dispose();
        }


        #endregion

        [Test]
        public void CanReadDonor()
        {
            var request = new DataSourceRequest();
            var result = _donorController.Donor_Read(request);
            Assert.IsInstanceOf<JsonResult>(result);
            
        }
        [Test]
        public void CanShowIndex()
        {
            var result = _donorController.Index();
            Assert.IsNotNull(result);
        }
        [Test]
        public void CanCreateDonor()
        {
            var donor = new DonorModel
                {
                    DonorID = 2,
                    DonorCode = "UN",
                    Name = "United Nations",
                    IsResponsibleDonor = false,
                    IsSourceDonor = true,
                    LongName = "United Nations"
                };
            var request = new DataSourceRequest();
            var result = _donorController.Donor_Create(request, donor);
            Assert.IsInstanceOf<JsonResult>(result);
        }
        
        [Test]
       public void CanDeleteDonor()
        {
            var result = _donorController.Delete(1);
            Assert.IsNotNull(result);
        }

    }
}
