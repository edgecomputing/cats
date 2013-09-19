using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using NUnit.Framework;
using Cats.Web.Hub.Controllers;
using Moq;

namespace DRMFSS.Web.Test
{
    [TestFixture]
    public class StoreControllerTests 
    {
        #region SetUp / TearDown

        private StoreController _storeController;
        [SetUp]
        public void Init()
        {
            var stores = new List<Store>
                {
                    new Store   {StoreID=1,Name = "Store 1",HubID=1,IsActive=true,IsTemporary=false,Number=101,StackCount=1,StoreManName="Mezgebu"},
                    new Store   {StoreID=2,Name = "Store 2",HubID=2,IsActive=true,IsTemporary=false,Number=101,StackCount=1,StoreManName="Mezgebu"},
                    new Store   {StoreID=3,Name = "Store 3",HubID=2,IsActive=true,IsTemporary=false,Number=101,StackCount=1,StoreManName="Mezgebu"}
                };
            var storeService = new Mock<IStoreService>();
            storeService.Setup(t => t.GetAllStore()).Returns(stores);
            storeService.Setup(t => t.GetAllByHUbs(null)).Returns(stores);

            var user = new UserProfile();
           
            var users = new List<UserProfile>
                {
                    new UserProfile(),
                    new UserProfile()
                };
            var userService = new Mock<IUserProfileService>();
            userService.Setup(t => t.GetUser("admin")).Returns(user);

            _storeController = new StoreController(storeService.Object, userService.Object);

        }

        [TearDown]
        public void Dispose()
        {
            _storeController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var result = _storeController.Index();
            var model = ((ViewResult)result).Model;
            //Assert

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<IEnumerable<Store>>(model);
            Assert.AreEqual(2, ((IEnumerable<Store>)model).Count());
        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var result = _storeController.Details(1);
            var model = result.Model;
            //Assert

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<Store>(model);
           
            Assert.IsNotNullOrEmpty(((Store)model).Name);
        }

        [Test]
        public void CanDoPostBackCreate()
        {
            //ACT
            var store =new Store   {Name = "Store 4",HubID=2,IsActive=true,IsTemporary=false,Number=101,StackCount=1,StoreManName="Mezgebu"};
            var result = _storeController.Create(store);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<int>(store.StoreID);
        }

        [Test]
        public void CanViewEdit()
        {
            //ACT
            var result = _storeController.Edit(1);
            var model = ((ViewResult)result).Model;
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<Store>(model);
        }

        [Test]
        public void CanDoPostBackEdit()
        {
            //ACT
            var store = new Store { Name = "Store 4", HubID = 2, IsActive = true, IsTemporary = false, Number = 101, StackCount = 1, StoreManName = "Mezgebu" };
            var result = _storeController.Edit(store);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<int>(store.StoreID);
        }

        [Test]
        public void CanDoPostBackDelete()
        {
            //ACT
            var result = _storeController.Delete(2);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        #endregion
    }
}
