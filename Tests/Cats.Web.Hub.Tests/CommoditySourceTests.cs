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
    public class CommoditySourceTests
    {
        #region SetUp / TearDown

        private CommoditySourceController _commoditySourceController;
        [SetUp]
        public void Init()
        {
            var commoditySource = new List<CommoditySource>
                {
                    new CommoditySource {CommoditySourceID = 1, Name = "Donation"},
                    new CommoditySource {CommoditySourceID = 2, Name = "Loan"},
                    new CommoditySource {CommoditySourceID = 3, Name = "Local Purchase"},
                };
            var commoditySourceService = new Mock<ICommoditySourceService>();
            commoditySourceService.Setup(t => t.GetAllCommoditySource()).Returns(commoditySource);
            var userProfileService = new Mock<IUserProfileService>();
            _commoditySourceController = new CommoditySourceController(commoditySourceService.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _commoditySourceController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var viewResult = _commoditySourceController.Index();

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<IEnumerable<CommoditySource>>(model);
            Assert.AreEqual(3, ((IEnumerable<CommoditySource>)model).Count());
        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var viewResult = _commoditySourceController.Details(1);

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<CommoditySource>(model);
        }

        [Test]
        public void CanDoCreatePostBack()
        {
            //ACT
            var commoditySource = new CommoditySource { Name = "Return" };
            var jsonResult = _commoditySourceController.Create(commoditySource) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanViewEdit()
        {
            //ACT
            var viewResult = _commoditySourceController.Edit(1) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<CommoditySource>(model);
        }
        
        [Test]
        public void CanDoEditPostBack()
        {
            //ACT
            var commoditySource = new CommoditySource { Name = "Donation" };
            var jsonResult = _commoditySourceController.Edit(commoditySource) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanRedirectDeleteConfirm()
        {
            //ACt
            var redirectToRouteResult = _commoditySourceController.DeleteConfirmed(1) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("CommoditySource", redirectToRouteResult.RouteValues["controller"]);
        }

        #endregion
    }
}
