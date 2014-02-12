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
                                                   ShippingInstruction = new ShippingInstruction{ShippingInstructionID = 1,Value = "123/456"},
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
            var sampleGUID = Guid.NewGuid();
            var deliveryReconciles = new List<DeliveryReconcile>()
                                       {
                                           new DeliveryReconcile()
                                               {
                                                   DeliveryReconcileID = 5,
                                                   GRN = "123",
                                                   FDPID = 1,
                                                   DispatchID = sampleGUID,
                                                   WayBillNo = "6554",
                                                   RequsitionNo = "REQ-001",
                                                   HubID = 1,
                                                   GIN = "456",
                                                   ReceivedAmount = (decimal)50.00,
                                                   ReceivedDate = DateTime.Today,
                                                   LossAmount = (decimal)0.00,
                                                   LossReason = null,
                                                   TransactionGroupID = sampleGUID,
                                                   Dispatch = new Dispatch() {
                                                                    DispatchID = sampleGUID,
                                                                    PartitionID = 0,
                                                                    HubID = 1,
                                                                    GIN = "159",
                                                                    FDPID = 1,
                                                                    WeighBridgeTicketNumber = "984985",
                                                                    RequisitionNo = "REQ-001",
                                                                    BidNumber = "BID-123",
                                                                    TransporterID = 1,
                                                                    DriverName = "Abebe",
                                                                    PlateNo_Prime = "3-123456",
                                                                    PlateNo_Trailer = "3-123457",
                                                                    PeriodYear = 2014,
                                                                    PeriodMonth = 12,
                                                                    Round = 1,
                                                                    UserProfileID = 1,
                                                                    DispatchDate = DateTime.Now,
                                                                    CreatedDate = DateTime.Now,
                                                                    Remark = "sample remark",
                                                                    DispatchedByStoreMan = "Kebede",
                                                                    DispatchAllocationID = sampleGUID,
                                                                    OtherDispatchAllocationID = sampleGUID,
                                                                    DispatchAllocation = new DispatchAllocation()
                                                                                             {
                                                                                                 DispatchAllocationID = sampleGUID,
                                                                                                 PartitionID = 0,
                                                                                                 HubID = 1,
                                                                                                 StoreID = 1,
                                                                                                 Year = 2014,
                                                                                                 Month = 12,
                                                                                                 Round = 1,
                                                                                                 DonorID = 1,
                                                                                                 ProgramID = 1,
                                                                                                 CommodityID = 1,
                                                                                                 RequisitionNo = "45328",
                                                                                                 BidRefNo = "REF-123",
                                                                                                 ContractStartDate = DateTime.Now,
                                                                                                 ContractEndDate = DateTime.Now,
                                                                                                 Beneficiery = 123,
                                                                                                 Amount = (decimal)123.45,
                                                                                                 Unit = 1,
                                                                                                 TransporterID = 1,
                                                                                                 FDPID = 1,
                                                                                                 ShippingInstructionID = 1,
                                                                                                 ProjectCodeID = 1,
                                                                                                 TransportOrderID = 1
                                                                                             },
                                                                    DispatchDetails = new List<DispatchDetail>()
                                                                                          {
                                                                                              new DispatchDetail()
                                                                                                  {
                                                                                                      DispatchDetailID = sampleGUID,
                                                                                                      PartitionID = 0,
                                                                                                      TransactionGroupID = sampleGUID,
                                                                                                      DispatchID = sampleGUID,
                                                                                                      CommodityID = 1,
                                                                                                      RequestedQunatityInUnit = (decimal)123.45,
                                                                                                      UnitID = 1,
                                                                                                      RequestedQuantityInMT = (decimal)123.45,
                                                                                                      QuantityPerUnit = (decimal)123.45,
                                                                                                      Description = "Sample Description",
                                                                                                  },
                                                                                              new DispatchDetail()
                                                                                                  {
                                                                                                      DispatchDetailID = sampleGUID,
                                                                                                      PartitionID = 0,
                                                                                                      TransactionGroupID = sampleGUID,
                                                                                                      DispatchID = sampleGUID,
                                                                                                      CommodityID = 1,
                                                                                                      RequestedQunatityInUnit = (decimal)123.45,
                                                                                                      UnitID = 1,
                                                                                                      RequestedQuantityInMT = (decimal)123.45,
                                                                                                      QuantityPerUnit = (decimal)123.45,
                                                                                                      Description = "Sample Description",
                                                                                                  }
                                                                                          },
                                                                         FDP = new FDP()
                                                                              {
                                                                                 FDPID = 1,
                                                                                 Name = "FDP1",
                                                                                 AdminUnitID = 1
                                                                              },
                                                                         Hub = new Hub()
                                                                              {
                                                                                 HubID = 1,
                                                                                 Name = "Hub1",
                                                                              },
                                                                         OtherDispatchAllocation = new OtherDispatchAllocation()
                                                                                                       {
                                                                                                           OtherDispatchAllocationID = sampleGUID
                                                                                                       },
                                                                         Transporter = new Transporter()
                                                                                           {
                                                                                               TransporterID = 1,
                                                                                               Name = "Sample Transporter"
                                                                                           }
                                                                },
                                                        FDP = new FDP()
                                                                  {
                                                                      FDPID = 1,
                                                                      Name = "FDP1",
                                                                      AdminUnitID = 1
                                                                  },
                                                        Hub = new Hub()
                                                                  {
                                                                      HubID = 1,
                                                                      Name = "Hub1",
                                                                  },
                                                        TransactionGroup =new Tr
                                               }
                                       };
            var deliveryReconcileRepositoy = new Mock<IGenericRepository<DeliveryReconcile>>();
            deliveryReconcileRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<DeliveryReconcile, bool>>>(),
                      It.IsAny<Func<IQueryable<DeliveryReconcile>, IOrderedQueryable<DeliveryReconcile>>>(),
                      It.IsAny<string>())).Returns(deliveryReconciles);


            var transactionRepository = new Mock<IGenericRepository<Models.Transaction>>();
            transactionRepository.Setup(t => t.Add(It.IsAny<Models.Transaction>())).Returns(true);
            var transactionGroupRepository = new Mock<IGenericRepository<TransactionGroup>>();
            transactionGroupRepository.Setup(t => t.Add(It.IsAny<TransactionGroup>())).Returns(true);

            unitOfWork.Setup(t => t.GiftCertificateRepository).Returns(giftCertificateRepositoy.Object);
            unitOfWork.Setup(t => t.DeliveryReconcileRepository).Returns(deliveryReconcileRepositoy.Object);
            unitOfWork.Setup(t => t.TransactionRepository).Returns(transactionRepository.Object);
            unitOfWork.Setup(t => t.TransactionGroupRepository).Returns(transactionGroupRepository.Object);
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

        [Test]
        public void ShouldPostDeliveryReconcileReceiptTransaction()
        {
            //Act
            var result = _accountTransactionService.PostDeliveryReconcileReceipt(5);

            //Assert
            Assert.IsTrue(result);
        }

        #endregion
    }
}
