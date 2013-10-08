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
    public class CommodityTypeControllerTests
    {
        #region SetUp / TearDown

        private CommodityTypeController _commodityTypeController;
        [SetUp]
        public void Init()
        {
            var commodityTypes = new List<CommodityType>
                {
                    new CommodityType{ CommodityTypeID = 1, Name = "Food"}, 
                    new CommodityType{ CommodityTypeID = 2, Name = "Non Food"}
                };
            var commodityTypeService = new Mock<ICommodityTypeService>();
            commodityTypeService.Setup(t => t.GetAllCommodityType()).Returns(commodityTypes);
            var userProfileService = new Mock<IUserProfileService>();
            _commodityTypeController = new CommodityTypeController(commodityTypeService.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _commodityTypeController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var viewResult = _commodityTypeController.Index();

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<IEnumerable<CommodityType>>(model);
            Assert.AreEqual(2, ((IEnumerable<CommodityType>)model).Count());
        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var viewResult = _commodityTypeController.Details(1);

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<CommodityType>(model);
        }

        [Test]
        public void CanDoCreatePostBack()
        {
            //ACT
            var commodityType = new CommodityType { Name = "Cloth" };
            var jsonResult = _commodityTypeController.Create(commodityType) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanDoEditPostBack()
        {
            //ACT
            var commodityType = new CommodityType { Name = "Donation" };
            var jsonResult = _commodityTypeController.Edit(commodityType) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanRedirectDeleteConfirm()
        {
            //ACt
            var redirectToRouteResult = _commodityTypeController.DeleteConfirmed(1) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("CommodityType", redirectToRouteResult.RouteValues["controller"]);
        }

        #endregion
    }
}
