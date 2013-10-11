using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Controllers;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Adminstration.Tests.ControllerTest
{  [TestFixture]
   public class HubControllerTest
   {
       private HubController _hubController;
       #region SetUp
       [SetUp]
       public void Init()
       {
           var hub = new List<Hub>
               {
                   new Hub {HubID = 1,Name = "One",HubOwnerID = 1},
                   new Hub {HubID = 2,Name = "Two",HubOwnerID = 2},
               };
           var hubService = new Mock<IHubService>();
           hubService.Setup(m => m.GetAllHub()).Returns(hub);

           var hubOwner = new List<HubOwner>
               {
                   new HubOwner {HubOwnerID = 1,Name = "Owner1",LongName = "Hub Owner 1"},
                   new HubOwner {HubOwnerID = 2,Name = "Owner2",LongName = "Hub Owner 2"}

               };
           var hubOwnerService = new Mock<IHubOwnerService>();
           hubOwnerService.Setup(m => m.GetAllHubOwner()).Returns(hubOwner);

           _hubController=new HubController(hubService.Object,hubOwnerService.Object);  

       }
       [TearDown]
       public void Dispose()
       {
           _hubController.Dispose();
       }
#endregion

       #region Tests
       [Test]
       public void CanShowIndex()
       {
          var result= _hubController.Index();
           Assert.IsNotNull(result);
           
       }
       [Test]
       public void CanReadHub()
       {
           var request = new DataSourceRequest();
           var result = _hubController.Hub_Read(request);
           
           //Assert

           Assert.IsInstanceOf<JsonResult>(result);
       }
       [Test]
       public void CanDestroyHub()
       {
           var request = new DataSourceRequest();
           var hub = new Hub {HubID = 1, Name = "One", HubOwnerID = 1};
           var result = _hubController.Commodity_Destroy(request, hub);

           //Assert
           Assert.IsInstanceOf<JsonResult>(result);
           Assert.IsNotNull(result);
       }
       [Test]
       public void CanUpdateHub()
       {
           var request = new DataSourceRequest();
           var hub = new Hub { HubID = 1, Name = "One", HubOwnerID = 1 };
           var result = _hubController.Commodity_Update(request, hub);

           //Assert
           Assert.IsInstanceOf<DataSourceRequest>(request);
           Assert.IsNotNull(result);

       }
       #endregion
   }
}
