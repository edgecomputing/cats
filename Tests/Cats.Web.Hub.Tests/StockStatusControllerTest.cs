using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers.Reports;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    public class StockStatusControllerTest
    {
        private StockStatusController _stockStatusController;

        #region SetUp
        [SetUp]
        void Init()
        {
            var userProfile = new List<UserProfile>
                {
                    new UserProfile {UserProfileID = 1, UserName = "Abebe", Password = "Abebe", Email = "123@yahoo.com"},
                    new UserProfile {UserProfileID = 2, UserName = "Kebede", Password = "Bekele", Email = "123@yahoo.com"}
                };
            var userProfileService = new Mock<IUserProfileService>();
            userProfileService.Setup(t => t.GetAllUserProfile()).Returns(userProfile);

            var commodity = new List<Commodity>
                {
                    new Commodity {CommodityID = 1, Name = "Cereal", CommodityCode = "CER", CommodityTypeID = 1, ParentID = null},
                    new Commodity {CommodityID = 2, Name = "CSB", CommodityCode = "CSB", CommodityTypeID = 1, ParentID = 1}
                };
            var commodityService = new Mock<ICommodityService>();
            commodityService.Setup(t => t.GetAllCommodity()).Returns(commodity);

            var hub = new List<Models.Hub.Hub>
                {
                    new Models.Hub.Hub {HubID = 1,Name = "Adama",HubOwnerID = 1},
                    new Models.Hub.Hub { HubID =2,Name = "Kombolcha",HubOwnerID = 1},
                    new Models.Hub.Hub { HubID =3,Name = "Diredawa",HubOwnerID = 1}

                };
            var hubService = new Mock<IHubService>();
            hubService.Setup(t => t.GetAllHub()).Returns(hub);

            _stockStatusController=new StockStatusController(userProfileService.Object,commodityService.Object,hubService.Object);


        }

        [TearDown]
        public void Dispose()
        {
            _stockStatusController.Dispose();
        }

        #endregion

        #region Tests
        [Test]
        public void CanShowIndex()
        {
            var result = _stockStatusController.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model;
            Assert.IsInstanceOf<Models.Hub.Hub>(model);

        }

        [Test]
        public void CanShowFreeStockInfo()
        {
            var result = _stockStatusController.FreeStock() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model;
            Assert.IsInstanceOf<Models.Hub.Hub>(model);
        }
        [Test]
        public void CanShowCommodityByID()
        {
            var result = _stockStatusController.Commodity(1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model;
            Assert.IsInstanceOf<Commodity>(model);
        }

        [Test]
        public void CanShowReceipts()
        {
            var result = _stockStatusController.Receipts();
            Assert.IsNotNull(result);


        }

        //[Test]
        //public void CanShowDispatch()
        //{
        //    var result = _stockStatusController.Dispatch();
        //    Assert.IsNotNull(result);
        //}

        #endregion
    }
}
