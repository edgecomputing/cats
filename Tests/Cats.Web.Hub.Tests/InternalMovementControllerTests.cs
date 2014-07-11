using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Models.Hubs.ViewModels;
using Cats.Models.Hubs.ViewModels.Common;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class InternalMovementControllerTests
    {
        #region SetUp / TearDown

        private InternalMovementController _internalMovementController;

        [SetUp]

        public void Init()
        {
            var userProfiles = new List<UserProfile>
                {
                    new UserProfile
                        {UserProfileID = 1, UserName = "Nathnael", Password = "passWord", Email = "123@edge.com"},
                    new UserProfile
                        {UserProfileID = 2, UserName = "Banty", Password = "passWord", Email = "321@edge.com"},

                };
            var userProfileService = new Mock<IUserProfileService>();
            userProfileService.Setup(t => t.GetAllUserProfile()).Returns(userProfiles);

            var internalMovements = new List<InternalMovementLogViewModel>
                {
                    new InternalMovementLogViewModel
                        {
                            TransactionId = new Guid(),
                            FromStore = "Adama",
                            SelectedDate = DateTime.Now,
                            FromStack = 1,
                            RefernaceNumber = "jkkj",
                            CommodityName = "Wheat",
                            Program = "PSNP",
                            ProjectCodeName = "dcsc"
                        },
                    new InternalMovementLogViewModel
                        {
                            TransactionId = new Guid(),
                            FromStore = "Kombolcha",
                            SelectedDate = DateTime.Now,
                            FromStack = 2,
                            RefernaceNumber = "sdcz",
                            CommodityName = "osdmd",
                            Program = "Relief",
                            ProjectCodeName = "adsdk"
                        },
                };
            var internalMovementService = new Mock<IInternalMovementService>();
            internalMovementService.Setup(t => t.GetAllInternalMovmentLog()).Returns(internalMovements);

            //var transactions = new List<Transaction>
            //    {
            //        new Transaction {TransactionID = new Guid()},
            //        new Transaction {TransactionID = new Guid()},
            //    };
            //var transactionService = new Mock<ITransactionService>();
            //transactionService.Setup(t => t.GetCommodityBalanceForHub(1,1,1,1)).Returns();
            //TODO: Finish up seeding data before testing

        }

        [TearDown]
        public void Dispose()
        {
            _internalMovementController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var viewResult = _internalMovementController.Index() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<IEnumerable<InternalMovementLogViewModel>>(model);

            #endregion
        }

        [Test]
        public void CanViewCreate()
        {
            //ACT
            var viewResult = _internalMovementController.Create() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<InternalMovementViewModel>(model);
        }

        [Test]
        public void CanDoCreatePostback()
        {
            //ACT
            var internalMovementViewModel = new InternalMovementViewModel
                {
                    //TODO: Seed data before tesing
                };
            var viewResult = _internalMovementController.Create(internalMovementViewModel) as ViewResult;
            
            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<InternalMovementViewModel>(model);
        }

        [Test]
        public void CanDoIsQuantityValid()
        {
            //ACT
            var jsonResult = _internalMovementController.IsQuantityValid(10000, 1,1,1,1,1) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanGetStacksForFromStore()
        {
            //ACT
            var jsonResult = _internalMovementController.GetStacksForFromStore(1,1) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.IsInstanceOf<SelectList>(data);
        }

        [Test]
        public void CanGetStacksForToStore()
        {
            //ACT
            var jsonResult = _internalMovementController.GetStacksForToStore(1, 1, 1) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.IsInstanceOf<SelectList>(data);
        }

        [Test]
        public void CanGetProjecCodetForCommodity()
        {
            //ACT
            var jsonResult = _internalMovementController.GetProjecCodetForCommodity(1) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.IsInstanceOf<SelectList>(data);
            Assert.AreEqual("ProjectCodeId", ((SelectList)data).DataValueField);
            Assert.AreEqual("ProjectName", ((SelectList)data).DataTextField);
            Assert.IsInstanceOf<IEnumerable<ProjectCodeViewModel>>(((SelectList)data).Items);
        }

        [Test]
        public void CanGetSINumberForProjectCode()
        {
            //ACT
            var jsonResult = _internalMovementController.GetSINumberForProjectCode(1) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.IsInstanceOf<SelectList>(data);
            Assert.AreEqual("ShippingInstructionId", ((SelectList)data).DataValueField);
            Assert.AreEqual("ShippingInstructionName", ((SelectList)data).DataTextField);
            Assert.IsInstanceOf<IEnumerable<ShippingInstructionViewModel>>(((SelectList)data).Items);
        }

        [Test]
        public void CanGetFromStoreForParentCommodity()
        {
            //ACT
            var jsonResult = _internalMovementController.GetFromStoreForParentCommodity(1,1) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.IsInstanceOf<SelectList>(data);
            Assert.AreEqual("StoreId", ((SelectList)data).DataValueField);
            Assert.AreEqual("StoreName", ((SelectList)data).DataTextField);
            Assert.IsInstanceOf<IEnumerable<Store>>(((SelectList)data).Items);
        }

        [Test]
        public void CanViewDetial()
        {
            //ACT
            var viewResult = _internalMovementController.ViewDetial("123") as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<InternalMovementLogViewModel>(model);
        }

        [Test]
        public void CanSINumberBalance()
        {
            //ACT
            var viewResult = _internalMovementController.SINumberBalance(1,1,1,1,1) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<StoreBalanceViewModel>(model);
        }

    }
}
