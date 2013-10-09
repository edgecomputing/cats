using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class HubOwnerControllerTests
    {
        #region SetUp / TearDown

        private HubOwnerController _hubOwnerController;
        [SetUp]
        public void Init()
        {
            var hubOwners = new List<HubOwner>
                {
                    new HubOwner{HubOwnerID=1,LongName="Abebe Tesfaye",Name="Abebe"},
                    new HubOwner{HubOwnerID=2,LongName="Tesfaye Deghu",Name="Tesfaye"}
                };
            var hubOwnerService = new Mock<IHubOwnerService>();
            hubOwnerService.Setup(t => t.GetAllHubOwner()).Returns(hubOwners);
            /*
            var fdps = new List<FDP>
                {
                    new FDP{FDPID = 1, Name = "Elidar", AdminUnitID = 1},
                    new FDP{FDPID = 2, Name = "Gagu", AdminUnitID = 2},
                };
            var fdpService = new Mock<IFDPService>();
            fdpService.Setup(t => t.GetAllFDP()).Returns(fdps);, fdpService.Object*/
            var userProfileService = new Mock<IUserProfileService>();
            _hubOwnerController = new HubOwnerController(hubOwnerService.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _hubOwnerController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var viewResult = _hubOwnerController.Index() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<IEnumerable<HubOwner>>(model);
            Assert.AreEqual(1, ((IEnumerable<HubOwner>)model).Count());

        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var viewResult = _hubOwnerController.Details(1);

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<HubOwner>(model);
           
        }

        [Test]
        public void CanViewCreate()
        {
            //ACT
            var viewResult = _hubOwnerController.Create() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<HubOwner>(model);
        }

        [Test]
        public void CanDoCreatePostBack()
        {
            //ACT
            var hubOwner = new HubOwner { LongName = "Belay Zeleke", Name = "Beze" };
            var redirectToRouteResult = _hubOwnerController.Create(hubOwner) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Contact", redirectToRouteResult.RouteValues["controller"]);
        }

        [Test]
        public void CanDoEditPostBack()
        {
            //ACT
            var hubOwner = new HubOwner { LongName = "Belay Zeleke", Name = "Beze" };
            var redirectToRouteResult = _hubOwnerController.Create(hubOwner) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("HubOwner", redirectToRouteResult.RouteValues["controller"]);
        }
        
        [Test]
        public void CanRedirectDeleteConfirm()
        {
            //ACt
            var redirectToRouteResult = _hubOwnerController.DeleteConfirmed(1) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("HubOwner", redirectToRouteResult.RouteValues["controller"]);
        }


        #endregion
    }
}
