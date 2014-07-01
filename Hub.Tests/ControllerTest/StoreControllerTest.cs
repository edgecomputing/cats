using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cats.Models.Hubs;

using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;
using Cats.Areas.Hub.Controllers;
using Cats.Services.Hub;

namespace Hub.Tests.ControllerTest
{  
  [TestFixture]
   public class StoreControllerTest
   {
       private StoreController _storeController;
       #region SetUp
       [SetUp]
       public void init()
       {
           //var store = new List<Store>
           //    {
           //        new Store {StoreID = 1,Name = "One",Number = 1,IsActive = true,IsTemporary = false,HubID = 1,StackCount = 1,StoreManName = "Store man1"},
           //        new Store {StoreID = 2,Name = "Two",Number = 2,IsActive = false,IsTemporary = true,HubID = 2,StackCount = 2,StoreManName = "Store man2"}
           //    };
           //var storeService = new Mock<IStoreService>();
           //storeService.Setup(m => m.GetAllStore()).Returns(store);

           //var hub = new List<Cats.Models.Hubs.Hub>
           //    {
           //      new Cats.Models.Hubs.Hub {HubID = 1,Name = "Diredawa",HubOwnerID =1},
           //      new Cats.Models.Hubs.Hub {HubID = 2,Name = "Kombolecha",HubOwnerID =2}
           //    };

           //var hubService = new Mock<IHubService>();
           //hubService.Setup(m => m.GetAllHub()).Returns(hub);
           //_storeController=new StoreController(storeService.Object,hubService.Object);
       }
      [TearDown]
      public void Dispose()
      {
          _storeController.Dispose();
      }
       #endregion
       #region Test
      [Test]
      public void CanShowIndex()
      {
          var result = _storeController.Index();
          Assert.IsNotNull(result);
      }
      //[Test]
      //public void CanReadStore()
      //{
      //    var request = new DataSourceRequest();
      //    var result = _storeController.Store_Read(request);
      //    Assert.IsInstanceOf<JsonResult>(result);
      //   // Assert.IsNotNull(result);
      //}
      [Test]
      public void CanCreateStore()
      {
          //var store = new StoreViewModel { StoreID = 1, Name = "One", Number = 1, IsActive = true, IsTemporary = false, HubID = 1, StackCount = 1, StoreManName = "Store man1" };
          //var request = new DataSourceRequest();
          //var result = _storeController.Store_Create(request, store);
          //Assert.IsInstanceOf<JsonResult>(result);
      }
      //[Test]
      //public void CanUpdateStore()
      //{
      //    var request = new DataSourceRequest();
      //    var storeViewModel = new StoreViewModel { StoreID = 1, Name = "One", Number = 1, IsActive = true, 
      //                          IsTemporary = false, HubID = 1, StackCount = 1, StoreManName = "Store man1" };
      //    var result = _storeController.Store_Update(request, storeViewModel);
      //    //Assert
      //    Assert.IsInstanceOf<JsonResult>(result);

      //}
      [Test]
      public void CanDeleteStore()
      {
          var result = _storeController.Delete(1);
          //Assert.IsInstanceOf<ActionResult>(result);
          Assert.IsNotNull(result);
      }
       #endregion
   }
}
