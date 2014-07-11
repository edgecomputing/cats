using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class ContactControllerTests
    {
        #region SetUp / TearDown

        private ContactController _contactController;
        [SetUp]
        public void Init()
        {
            var contacts = new List<Contact>
                {
                    new Contact{ ContactID = 1,  FDPID = 1, FirstName = "Abebe", LastName = "Kebede", PhoneNo = "251116123456"},
                    new Contact{ ContactID = 2, FDPID = 2, FirstName = "Abebech", LastName = "Zeleke", PhoneNo = "251911123456"}
                };
            var contactService = new Mock<IContactService>();
            contactService.Setup(t => t.GetAllContact()).Returns(contacts);

            var fdps = new List<FDP>
                {
                    new FDP{FDPID = 1, Name = "Elidar", AdminUnitID = 1},
                    new FDP{FDPID = 2, Name = "Gagu", AdminUnitID = 2},
                };
            var fdpService = new Mock<IFDPService>();
            fdpService.Setup(t => t.GetAllFDP()).Returns(fdps);
            var userProfileService = new Mock<IUserProfileService>();
            _contactController = new ContactController(contactService.Object, fdpService.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _contactController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var viewResult = _contactController.Index(1);

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<IEnumerable<Contact>>(model);
            Assert.AreEqual(1, ((IEnumerable<Contact>)model).Count());
            Assert.IsInstanceOf<Int32>(viewResult.ViewBag.FDPID);
            Assert.IsInstanceOf<string>(viewResult.ViewBag.FDPName);
        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var viewResult = _contactController.Details(1);

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<Contact>(model);
            Assert.IsInstanceOf<Int32>(viewResult.ViewBag.FDPID);
            Assert.IsInstanceOf<string>(viewResult.ViewBag.FDPName);
        }

        [Test]
        public void CanViewCreate()
        {
            //ACT
            var viewResult = _contactController.Create(1) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<Contact>(model);
            Assert.IsInstanceOf<Int32>(viewResult.ViewBag.FDPID);
            Assert.IsInstanceOf<string>(viewResult.ViewBag.FDPName);
        }

        [Test]
        public void CanDoCreatePostBack()
        {
            //ACT
            var contact = new Contact{ FDPID = 1, FirstName = "Kebede", LastName = "Molla", PhoneNo = "251116123456"};
            var redirectToRouteResult = _contactController.Create(contact) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Contact", redirectToRouteResult.RouteValues["controller"]);
        }

        [Test]
        public void CanDoEditPostBack()
        {
            //ACT
            var contact = new Contact { ContactID = 1, FDPID = 1, FirstName = "Kebede", LastName = "Molla", PhoneNo = "251116123456" };
            var redirectToRouteResult = _contactController.Create(contact) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Contact", redirectToRouteResult.RouteValues["controller"]);
        }

        [Test]
        public void CanRedirectDeleteConfirm()
        {
            //ACt
            var redirectToRouteResult = _contactController.DeleteConfirmed(1) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Contact", redirectToRouteResult.RouteValues["controller"]);
        }


        #endregion
    }
}
