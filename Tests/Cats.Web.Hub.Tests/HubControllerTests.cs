using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class HubControllerTests
    {
        #region SetUp / TearDown

        private HubController _hubController;

        [SetUp]
        public void Init()
        {
            var hubs = new List<Cats.Models.Hubs.Hub>
                {
                    new Models.Hubs.Hub {HubID = 1, Name = "Adama", HubOwnerID = 1},
                    new Models.Hubs.Hub {HubID = 2, Name = "Kombolcha", HubOwnerID = 2},
                };
            var hubServices = new Mock<IHubService>();
            hubServices.Setup(t => t.GetAllHub()).Returns(hubs);

            var hubOwners = new List<HubOwner>
                {
                    new HubOwner {HubOwnerID = 1, Name = "DRMFSS"},
                    new HubOwner {HubOwnerID = 2, Name = "EFSRA"}
                };
            var hubOwnerService = new Mock<HubOwnerService>();
            hubOwnerService.Setup(t => t.GetAllHubOwner()).Returns(hubOwners);
            var userProfileService = new Mock<IUserProfileService>();
            _hubController = new HubController(hubOwnerService.Object, hubServices.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _hubController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var viewResult = _hubController.Index() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<IEnumerable<Models.Hubs.Hub>>(model);
        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var viewResult = _hubController.Details(1);

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<Models.Hubs.Hub>(model);
        }

        [Test]
        public void CanViewCreate()
        {
            //ACT
            var viewResult = _hubController.Create() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<SelectList>(viewResult.ViewBag.HubOwnerID);
        }

        [Test]
        public void CanDoCreatePostBack()
        {
            //ACT
            var hub = new Models.Hubs.Hub { Name = "Diredawa", HubOwnerID = 1 };
            var jsonResult = _hubController.Create(hub) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanViewEdit()
        {
            //ACT
            var viewResult = _hubController.Edit(1) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<SelectList>(viewResult.ViewBag.HubOwnerID);
            Assert.IsInstanceOf<Models.Hubs.Hub>(model);
            #endregion
        }

        [Test]
        public void CanEditPostBack()
        {
            //ACT
            var hub = new Models.Hubs.Hub { HubID = 1, Name = "Adama", HubOwnerID = 1 };
            var jsonResult = _hubController.Edit(hub) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanDoDeleteConfirmed()
        {
            //ACt
            var redirectToRouteResult = _hubController.DeleteConfirmed(1) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Hub", redirectToRouteResult.RouteValues["controller"]);
        }
    }
}
