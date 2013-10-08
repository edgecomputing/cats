using System.Collections.Generic;
using System.Globalization;
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
    public class AdminControllerTests
    {
        #region SetUp / TearDown

        private AdminController _adminController;
        [SetUp]
        public void Init()
        {
            var userProfiles = new List<UserProfile>
                {
                    new UserProfile {UserProfileID = 1, UserName = "Nathnael", Password = "passWord", Email = "123@edge.com"},
                    new UserProfile {UserProfileID = 2, UserName = "Banty", Password="passWord", Email = "321@edge.com"},

                };
            var userProfileService = new Mock<IUserProfileService>();
            userProfileService.Setup(t => t.GetAllUserProfile()).Returns(userProfiles);

            var userRoles = new List<UserRole>
                {
                    new UserRole {UserRoleID = 1, RoleID = 1, UserProfileID = 1},
                    new UserRole {UserRoleID = 2, RoleID = 1, UserProfileID = 2},
                    new UserRole {UserRoleID = 3, RoleID = 2, UserProfileID = 2}

                };
            var userRoleService = new Mock<IUserRoleService>();
            userRoleService.Setup(t => t.GetAllUserRole()).Returns(userRoles);

            var roles = new List<Role>
                {
                    new Role {RoleID = 1, SortOrder = 2, Name = "Data Entry", Description = "Data Entry"},
                    new Role {RoleID = 2, SortOrder = 4, Name = "Warehouse Supervisor", Description = "Warehouse Supervisor"},
                    new Role {RoleID = 3, SortOrder = 1, Name= "Admin", Description = "Administrator"},
                };
            var roleService = new Mock<IRoleService>();
            roleService.Setup(t => t.GetAllRole()).Returns(roles);
            
            var userHubs = new List<UserHub>
                {
                    new UserHub {UserHubID = 1, UserProfileID = 1, HubID = 1},
                    new UserHub {UserHubID = 2, UserProfileID = 2, HubID = 2},
                    new UserHub {UserHubID = 3, UserProfileID = 1, HubID = 3},
                };
            var userHUbService = new Mock<IUserHubService>();
            userHUbService.Setup(t => t.GetAllUserHub()).Returns(userHubs);

            var hubs = new List<Models.Hub.Hub>
                {
                    new Models.Hub.Hub {HubID = 1, Name = "Adama", HubOwnerID = 1},
                    new Models.Hub.Hub {HubID = 2, Name = "Kombolcha", HubOwnerID = 2},
                    new Models.Hub.Hub {HubID = 3, Name = "Diredawa", HubOwnerID = 3},
                };
            var hubService = new Mock<IHubService>();
            hubService.Setup(t => t.GetAllHub()).Returns(hubs);

            _adminController = new AdminController(userProfileService.Object, userRoleService.Object, roleService.Object, userHUbService.Object, hubService.Object);

        }

        [TearDown]
        public void Dispose()
        {
            _adminController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var result = _adminController.Index();
            var model = result.Model;
            //Assert

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<IEnumerable<UserProfile>>(model);
            Assert.AreEqual(2, ((IEnumerable<UserProfile>)model).Count());
        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var result = _adminController.Details(1);
            var model = result.Model;

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<UserProfile>(model);
            Assert.IsNotNullOrEmpty(((UserProfile)model).UserProfileID.ToString(CultureInfo.InvariantCulture));
            Assert.IsNotNullOrEmpty(((UserProfile)model).UserName);
        }

        [Test]
        public void CanDoPostBackCreate()
        {
            //ACT
            var userProfile = new UserProfile {UserName = "AbebeBiqila", Password = "abebe123", Email = "abebe@edge.com"};
            var result = _adminController.Create(userProfile);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsNotNullOrEmpty(userProfile.UserProfileID.ToString(CultureInfo.InvariantCulture));
            Assert.IsInstanceOf<int>(userProfile.UserProfileID);
        }

        [Test]
        public void CanDoPostBackEdit()
        {
            //ACT
            var userProfile = new UserProfile { UserProfileID = 1, UserName = "AbebeBiqila", Password = "abebe123", Email = "abebe@edge.com" };
            var result = _adminController.Edit(userProfile);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<UserProfile>(userProfile.UserProfileID.ToString(CultureInfo.InvariantCulture));
        }

        [Test]
        public void CanDoPostBackDelete()
        {
            //ACT
            var result = _adminController.Delete(1);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void CanRedirectDeleteConfirm()
        {
            //ACt
            var redirectToRouteResult = _adminController.DeleteConfirmed(1) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Home", redirectToRouteResult.RouteValues["controller"]);

        }

        [Test]
        public void CanViewUserRoles()
        {
            //ACT
            var result = _adminController.UserRoles("Nathnael");
            var model = ((ViewResult)result).Model;
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<UserRolesModel>(model);
        }

        [Test]
        public void CanViewUserHubs()
        {
            //ACT
            var result = _adminController.UserHubs("Nathnael");
            var model = ((ViewResult)result).Model;
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<UserRolesModel>(model);
        }

        #endregion
    }
}
