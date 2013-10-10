using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
   public class UserWarehouseControllerTest
   {
       private UserWarehouseController _userWarehouseController;
       #region SetUp
       [SetUp]
       public void Init()
       {
           var userWarehouse = new List<UserHub>
               {
                   new UserHub {UserHubID = 1, HubID = 1, UserProfileID = 1, IsDefault = "True"},
                   new UserHub {UserHubID = 2, HubID = 1, UserProfileID = 2, IsDefault = "False"}
               };

           var userWarehouseService = new Mock<IUserHubService>();
           userWarehouseService.Setup(m => m.GetAllUserHub()).Returns(userWarehouse);


           var hub = new List<Hub>
               {
                   new Hub {HubID = 1,Name = "One",HubOwnerID = 1},
                   new Hub {HubID = 2,Name = "Two",HubOwnerID = 2},
               };
           var hubService = new Mock<IHubService>();
           hubService.Setup(m => m.GetAllHub()).Returns(hub);

           var userProfile = new List<UserProfile>
               {
                 new UserProfile {UserProfileID = 1,UserName = "Admin",Email = "Admmin@gmail.com",LanguageCode = "en"},
                  new UserProfile {UserProfileID = 2,UserName = "Admin2",Email = "Admmin2@gmail.com",LanguageCode = "am"}
               };
           var userProfileService = new Mock<IUserProfileService>();
           userProfileService.Setup(m => m.GetAllUserProfile()).Returns(userProfile);

           _userWarehouseController=new UserWarehouseController(userWarehouseService.Object,hubService.Object,userProfileService.Object);
       }
       [TearDown]
       public void Dispose()
       {
           _userWarehouseController.Dispose();
       }
       #endregion
       #region Tests

       [Test]
       public void CanShowIndex()
       {
           var result = _userWarehouseController.Index();
           Assert.IsNotNull(result);
       }

       [Test]
       public void CanReadUserWarehouse()
       {
           var request = new DataSourceRequest();
           var result = _userWarehouseController.Read(request);
           Assert.IsInstanceOf<JsonResult>(result);
       }

       [Test]
       public void CanUpdateUserWarehouse()
       {
           var request = new DataSourceRequest();
           var userWarehouseViewModel = new HubUserViewModel { UserHubID = 2, HubID = 1, UserProfileID = 2, IsDefault = "False" };
           var result = _userWarehouseController.HubOwnerUpdate(request, userWarehouseViewModel);
           //Assert
           Assert.IsInstanceOf<JsonResult>(result);

       }

       [Test]
       public void CanCreateUserWarehouse()
       {
           var request = new DataSourceRequest();
           var userWarehouseViewModel = new HubUserViewModel { UserHubID = 2, HubID = 1, UserProfileID = 2, IsDefault = "False" };
           var result = _userWarehouseController.Create(request,userWarehouseViewModel);
           Assert.IsInstanceOf<JsonResult>(result);
       }
       #endregion
   }
}