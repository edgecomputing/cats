using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cats.Web.Adminstration.Controllers;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Models.ViewModels;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;
using Cats.Models;

namespace Cats.Web.Adminstration.Tests.ControllerTest
{
    [TestFixture]
   public class WoredaDonorTest
    {
        private DonorWoredaController _donorWoredaController;

        [SetUp]
        public void Init()
        {
            var woredas = new List<WoredasByDonor>()
                              {
                                  new WoredasByDonor()
                                      {
                                          DonorWoredaId = 2,
                                          WoredaId = 56,
                                          DonorId = 1
                                      },
                                  new WoredasByDonor()
                                      {
                                          DonorWoredaId = 4,
                                          WoredaId = 57,
                                          DonorId = 2
                                      }
                              };

            var woredasByDonorService = new Mock<IWoredaDonorService>();
            woredasByDonorService.Setup(w => w.GetAllWoredaDonor()).Returns(woredas);
            _donorWoredaController = new DonorWoredaController(null,null,woredasByDonorService.Object);

        }
        [TearDown]
        public void TearDown()
        {
            _donorWoredaController.Dispose();
        }

        #region tests
        [Test]
        public void CanShowIndex()
        {
            var result = _donorWoredaController.Index();
            Assert.IsNotNull(result);
        }
        [Test]
        public void CanReadData()
        {
            var request = new DataSourceRequest();
            var result = _donorWoredaController.DonorWoredaRead(request);

            Assert.IsInstanceOf<JsonResult>(request);
            Assert.IsNotNull(result);
        }

        [Test]
        public void CanCreateNew()
        {
            var request = new DataSourceRequest();
            var woredaViewModel = new DonorWoredaViewModel()
                                      {
                                          WoredaDonorInt = 1,
                                          WoredaId = 56,
                                          WoredaName = "Elidar",
                                          DonorId = 1
                                      };
            var result = _donorWoredaController.DonorWoredaCreate(request, woredaViewModel);
            

            Assert.IsInstanceOf<JsonResult>(request);

        }

        [Test]
        public void CanDeleteWoredaDonor()
        {
            var result = _donorWoredaController.Delete(1);
            Assert.IsInstanceOf<ActionResult>(result);
        }
        #endregion

    }
}
