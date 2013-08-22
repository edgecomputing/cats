using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Services.EarlyWarning;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class GiftCertificateControllerTests
    {
        #region SetUp / TearDown

        private GiftCertificateController _giftCertificateController;
        [SetUp]
        public void Init()
        {
            var giftCertificateService = new Mock<IGiftCertificateService>();
            var giftCertificateDetailService = new Mock<IGiftCertificateDetailService>();
            _giftCertificateController = new GiftCertificateController(giftCertificateService.Object, giftCertificateDetailService.Object);
        }

        [TearDown]
        public void Dispose()
        { _giftCertificateController.Dispose();}

        #endregion

        #region Tests

        [Test]
        public void ShouldReturnTrueSiDoesntExsistOrBeingEdited()
        {
            //ACT
            var result = _giftCertificateController.NotUnique("SI-001", 1);

            //Assert

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsTrue((bool)((JsonResult)result).Data);
        }

        #endregion
    }
}
