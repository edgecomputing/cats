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
                                                   DeliveryReconcileID = 1,
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
                                                        TransactionGroup =new TransactionGroup()
                                                                              {
                                                                                  TransactionGroupID = sampleGUID
                                                                              }
                                               }
                                       };
            var deliveryReconcileRepositoy = new Mock<IGenericRepository<DeliveryReconcile>>();
            deliveryReconcileRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<DeliveryReconcile, bool>>>(),
                      It.IsAny<Func<IQueryable<DeliveryReconcile>, IOrderedQueryable<DeliveryReconcile>>>(),
                      It.IsAny<string>())).Returns(deliveryReconciles);
            var reliefRequisitions = new List<ReliefRequisition>()
                                       {
                                           new ReliefRequisition()
                                               {
                                                   RequisitionID = 1,
                                                   RequisitionNo = "REQ-001",
                                                   Month = 12,
                                                   ProgramID = 1,
                                               },
                                           new ReliefRequisition()
                                               {
                                                   RequisitionID = 2,
                                                   RequisitionNo = "REQ-002",
                                                   Month = 11,
                                                   ProgramID = 2,
                                               }
                                       };
            var reliefRequisitionRepositoy = new Mock<IGenericRepository<ReliefRequisition>>();
            reliefRequisitionRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<ReliefRequisition, bool>>>(),
                      It.IsAny<Func<IQueryable<ReliefRequisition>, IOrderedQueryable<ReliefRequisition>>>(),
                      It.IsAny<string>())).Returns(reliefRequisitions);
            reliefRequisitionRepositoy.Setup(t => t.FindById(It.IsAny<int>())).Returns((int id) => reliefRequisitions.
                                                                                                 FirstOrDefault(t => t.RequisitionID == id));

            var units = new List<Unit>()
                                       {
                                           new Unit()
                                               {
                                                   UnitID = 1,
                                                   Name = "Quintal"
                                               },
                                           new Unit()
                                               {
                                                   UnitID = 2,
                                                   Name = "Kg"
                                               },
                                       };
            var unitRepositoy = new Mock<IGenericRepository<Unit>>();
            unitRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<Unit, bool>>>(),
                      It.IsAny<Func<IQueryable<Unit>, IOrderedQueryable<Unit>>>(),
                      It.IsAny<string>())).Returns(units);

            var dispatches = new List<Dispatch>()
                                       {
                                           new Dispatch() {
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
                                       };
            var dispatchRepositoy = new Mock<IGenericRepository<Dispatch>>();
            dispatchRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<Dispatch, bool>>>(),
                      It.IsAny<Func<IQueryable<Dispatch>, IOrderedQueryable<Dispatch>>>(),
                      It.IsAny<string>())).Returns(dispatches);

            var siPCAllocation = new List<SIPCAllocation>()
                                       {
                                           new SIPCAllocation()
                                               {
                                                   SIPCAllocationID = 1,
                                                   FDPID = 1,
                                                   RequisitionDetailID = 1,
                                                   Code = 0001245,
                                                   AllocatedAmount = (decimal)123.45,
                                                   AllocationType = "SI",
                                                   ReliefRequisitionDetail = new ReliefRequisitionDetail()
                                                                                 {
                                                                                    RequisitionDetailID = 1,
                                                                                    RequisitionID = 1,
                                                                                    CommodityID = 1,
                                                                                    BenficiaryNo = 123,
                                                                                    Amount = (decimal)123.45,
                                                                                    FDPID = 1,
                                                                                    DonorID = 1,
                                                                                    ReliefRequisition = new ReliefRequisition()
                                                                                                                   {
                                                                                                                       RequisitionID = 1,
                                                                                                                       RequisitionNo = "REQ-001",
                                                                                                                       Month = 12,
                                                                                                                       ProgramID = 1,
                                                                                                                       RegionID = 1
                                                                                                                   },
                                                                                 }
                                               },
                                           new SIPCAllocation()
                                               {
                                                   SIPCAllocationID = 2,
                                                   FDPID = 1,
                                                   RequisitionDetailID = 1,
                                                   Code = 0001246,
                                                   AllocatedAmount = (decimal)123.45,
                                                   AllocationType = "PC",
                                                   ReliefRequisitionDetail = new ReliefRequisitionDetail()
                                                                                 {
                                                                                    RequisitionDetailID = 1,
                                                                                    RequisitionID = 1,
                                                                                    CommodityID = 1,
                                                                                    BenficiaryNo = 123,
                                                                                    Amount = (decimal)123.45,
                                                                                    FDPID = 1,
                                                                                    DonorID = 1,
                                                                                    ReliefRequisition = new ReliefRequisition()
                                                                                                                   {
                                                                                                                       RequisitionID = 1,
                                                                                                                       RequisitionNo = "REQ-001",
                                                                                                                       Month = 12,
                                                                                                                       ProgramID = 1,
                                                                                                                       RegionID = 1
                                                                                                                   },
                                                                                 }
                                               }
                                       };
            var siPCAllocationRepositoy = new Mock<IGenericRepository<SIPCAllocation>>();
            siPCAllocationRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<SIPCAllocation, bool>>>(),
                      It.IsAny<Func<IQueryable<SIPCAllocation>, IOrderedQueryable<SIPCAllocation>>>(),
                      It.IsAny<string>())).Returns(siPCAllocation);

            var shippingInstructions = new List<ShippingInstruction>()
                                       {
                                           new ShippingInstruction()
                                               {
                                                   ShippingInstructionID = 1,
                                                   Value = "0001245"
                                               },
                                           new ShippingInstruction()
                                               {
                                                   ShippingInstructionID = 1,
                                                   Value = "0001246"
                                               },
                                       };
            var shippingInstructionRepositoy = new Mock<IGenericRepository<ShippingInstruction>>();
            shippingInstructionRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<ShippingInstruction, bool>>>(),
                      It.IsAny<Func<IQueryable<ShippingInstruction>, IOrderedQueryable<ShippingInstruction>>>(),
                      It.IsAny<string>())).Returns(shippingInstructions);

            var projectCodes = new List<ProjectCode>()
                                       {
                                           new ProjectCode()
                                               {
                                                   ProjectCodeID = 1,
                                                   Value = "0001245"
                                               },
                                           new ProjectCode()
                                               {
                                                   ProjectCodeID = 1,
                                                   Value = "0001246"
                                               },
                                       };
            var projectCodeRepositoy = new Mock<IGenericRepository<ProjectCode>>();
            projectCodeRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<ProjectCode, bool>>>(),
                      It.IsAny<Func<IQueryable<ProjectCode>, IOrderedQueryable<ProjectCode>>>(),
                      It.IsAny<string>())).Returns(projectCodes);

            var transactionRepository = new Mock<IGenericRepository<Models.Transaction>>();
            transactionRepository.Setup(t => t.Add(It.IsAny<Models.Transaction>())).Returns(true);
            var transactionGroupRepository = new Mock<IGenericRepository<TransactionGroup>>();
            transactionGroupRepository.Setup(t => t.Add(It.IsAny<TransactionGroup>())).Returns(true);

            unitOfWork.Setup(t => t.GiftCertificateRepository).Returns(giftCertificateRepositoy.Object);
            unitOfWork.Setup(t => t.DeliveryReconcileRepository).Returns(deliveryReconcileRepositoy.Object);
            unitOfWork.Setup(t => t.ReliefRequisitionRepository).Returns(reliefRequisitionRepositoy.Object);

            unitOfWork.Setup(t => t.DispatchRepository).Returns(dispatchRepositoy.Object);
            unitOfWork.Setup(t => t.UnitRepository).Returns(unitRepositoy.Object);
            unitOfWork.Setup(t => t.SIPCAllocationRepository).Returns(siPCAllocationRepositoy.Object);
            unitOfWork.Setup(t => t.ShippingInstructionRepository).Returns(shippingInstructionRepositoy.Object);
            unitOfWork.Setup(t => t.ProjectCodeRepository).Returns(projectCodeRepositoy.Object);
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

        [Test]
        public void ShouldPostSIAllocationTransaction()
        {
            //Act
            var result = _accountTransactionService.PostSIAllocation(1);

            //Assert
            Assert.IsTrue(result);
        }

        #endregion
    }
}
