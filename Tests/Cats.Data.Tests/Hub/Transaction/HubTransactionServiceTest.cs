using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cats.Data.UnitWork;
using NUnit.Framework;
using Moq;
using Cats.Services.Hub;
namespace Cats.Data.Tests.Hub.Transaction
{
    [TestFixture]
    public class HubTransactionServiceTest
    {
         #region SetUp / TearDown

        private TransactionService _accountTransactionService;
        [SetUp]
        public void Init()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
        }


        #endregion

        #region Tests

        #endregion

    }
}
