using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class TransactionControllerTests
    {
        #region SetUp / TearDown

        private TransactionController _transactionController;
        [SetUp]
        public void Init()
        {
            var transactionService = new Mock<ITransactionService>();
            var ledgerServie = new Mock<ILedgerService>();
            var commodityService=new Mock<ICommodityService>();
            var userProfileService = new Mock<IUserProfileService>();
            _transactionController = new TransactionController(transactionService.Object, ledgerServie.Object,
                                                               commodityService.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _transactionController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void ShouldDisplayJournal()
        {
            //Act
            var result = _transactionController.Journal();

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<List<Transaction>>(((ViewResult)result).Model);
        }

        [Test]
        public void ShouldDisplayJournalPartial()
        {
            //Act
            var result = _transactionController.LedgerPartial();

            //Assert
            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.IsInstanceOf<List<Transaction>>(((ViewResult)result).Model);
        }
        [Test]
        public void ShouldDisplayGetAccounts()
        {
            //Act
            var result = _transactionController.GetAccounts(1);

            //Assert
            Assert.IsInstanceOf<JsonResult>(result);
        }

        #endregion
    }
}
