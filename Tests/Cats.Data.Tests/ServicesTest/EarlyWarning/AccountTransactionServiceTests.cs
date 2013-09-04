using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Data.Repository;
using Cats.Models;
using Cats.Services.Transaction;
using NUnit.Framework;
using Moq;
using Cats.Data.UnitWork;
using System.Linq.Expressions;

namespace Cats.Data.Tests.ServicesTest.EarlyWarning
{
    [TestFixture]
    public class AccountTransactionServiceTests
    {
        #region SetUp / TearDown

        private TransactionService _accountTransactionService; 
        [SetUp]
        public void Init()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var giftCertificates = new List<GiftCertificate>()
                                       {
                                           new GiftCertificate()
                                               {
                                                   StatusID = 1,
                                                   ProgramID = 1,
                                                   DonorID = 1,
                                                   GiftCertificateID = 1,
                                                   ETA = DateTime.Today,
                                                   ReferenceNo = "REF-001",
                                                   SINumber = "SI-001",
                                                   Vessel = "MRSEL",
                                                   PortName = "Semera",
                                                   IsPrinted = false,
                                                   GiftDate = DateTime.Today,
                                                   DModeOfTransport = 1,
                                                   GiftCertificateDetails = new List<GiftCertificateDetail>()
                                                                                {
                                                                                    new GiftCertificateDetail()
                                                                                        {
                                                                                            CommodityID = 1,
                                                                                            BillOfLoading = "B-001",
                                                                                            DBudgetTypeID = 1,
                                                                                            WeightInMT = 12,
                                                                                            DFundSourceID = 1,
                                                                                            GiftCertificateID = 1,
                                                                                            GiftCertificateDetailID = 2,
                                                                                            TransactionGroupID = 1,



                                                                                        }
                                                                                }

                                               }
                                       };
            var giftCertificateRepositoy = new Mock<IGenericRepository<GiftCertificate>>();
            giftCertificateRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<GiftCertificate, bool>>>(),
                      It.IsAny<Func<IQueryable<GiftCertificate>, IOrderedQueryable<GiftCertificate>>>(),
                      It.IsAny<string>())).Returns(giftCertificates);

            var transactionRepository = new Mock<IGenericRepository<Models.Transaction>>();
            transactionRepository.Setup(t => t.Add(It.IsAny<Models.Transaction>())).Returns(true);

            unitOfWork.Setup(t => t.GiftCertificateRepository).Returns(giftCertificateRepositoy.Object);
            unitOfWork.Setup(t => t.TransactionRepository).Returns(transactionRepository.Object);
            unitOfWork.Setup(t => t.Save());
            _accountTransactionService=new TransactionService(unitOfWork.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _accountTransactionService.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void ShouldPostGiftCertificateTransaction()
        {
           //Act
            var result = _accountTransactionService.PostGiftCertificate(1);

            //Assert
            Assert.IsTrue(result);
        }

        #endregion
    }
}
