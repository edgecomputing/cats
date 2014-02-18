
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Data.Hub.Repository;
using Cats.Data.Hub.UnitWork;
using NUnit.Framework;
using Moq;
using Cats.Services.Hub;
using Cats.Models.Hubs;

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
            var receive = new List<Receive>()
                              {
                                  new Receive()
                                      {
                                          ReceiveID = Guid.NewGuid(),
                                          PartitionID = 0,
                                          GRN = "001",
                                          CommodityTypeID = 1,
                                          SourceDonorID = 1,
                                          ResponsibleDonorID = 1,
                                          TransporterID = 1,
                                          PlateNo_Prime = "00001",
                                          PlateNo_Trailer = "00002",
                                          DriverName = "Dawit",
                                          WeightBridgeTicketNumber = "012",
                                          WeightBeforeUnloading = 1200,
                                          WeightAfterUnloading = 0,
                                          ReceiptDate = DateTime.Today,
                                          UserProfileID = 1,
                                          CreatedDate = DateTime.Today,
                                          WayBillNo = "001",
                                          CommoditySourceID = 1,
                                          Remark = "this a a test receive",
                                          VesselName = "002",
                                          ReceivedByStoreMan = "Abebe",
                                          PortName = "Dire Dawa",
                                          PurchaseOrder = "002",
                                          SupplierName = "WFP",
                                          ReceiptAllocationID = Guid.NewGuid(),
                                          CommoditySource = new CommoditySource()
                                                                {
                                                                    CommoditySourceID = 1,
                                                                    Name = "Donatiion"

                                                                },
                                          CommodityType = new CommodityType()
                                                              {
                                                                  CommodityTypeID = 1,
                                                                  Name = "FOOD"
                                                              },
                                          Hub = new Models.Hubs.Hub()
                                                    {
                                                        HubID = 1,
                                                        Name = "Kombelcha"
                                                    },
                                          ReceiveDetails = new Collection<ReceiveDetail>()
                                                               {
                                                                   new ReceiveDetail()
                                                                       {
                                                                           ReceiveDetailID = Guid.NewGuid(),
                                                                           PartitionID = 0,
                                                                           ReceiveID = Guid.NewGuid(),
                                                                           //should be given the 
                                                                           TransactionGroupID = null,
                                                                           CommodityID = 1,
                                                                           SentQuantityInUnit = 1200,
                                                                           UnitID = 1,
                                                                           SentQuantityInMT = 1200,
                                                                           Description = "this is a test",
                                                                           Commodity = new Commodity()
                                                                                           {
                                                                                               CommodityID = 1,
                                                                                               Name = "Pulse"
                                                                                           },
                                                                           Unit = new Unit()
                                                                                      {
                                                                                          UnitID = 1,
                                                                                          Name = "kg"
                                                                                      }

                                                                       }
                                                               }



                                      }








                              };

            var reieveRepository = new Mock<IGenericRepository<Receive>>();
            reieveRepository.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<Receive, bool>>>(),
                      It.IsAny<Func<IQueryable<Receive>, IOrderedQueryable<Receive>>>(),
                      It.IsAny<string>())).Returns(receive);
            var sampleGUID = Guid.NewGuid();


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

            var shippingInstructioncodes = new List<ShippingInstruction>()
                                               {
                                                   new ShippingInstruction()
                                                       {
                                                           ShippingInstructionID = 1,
                                                           Value = "123/456",
                                                       },
                                                   new ShippingInstruction()
                                                       {
                                                           ShippingInstructionID = 2,
                                                           Value = "123/789",
                                                       }
                                               };
            var shippingInstructionRepository = new Mock<IGenericRepository<ShippingInstruction>>();

            shippingInstructionRepository.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<ShippingInstruction, bool>>>(),
                      It.IsAny<Func<IQueryable<ShippingInstruction>, IOrderedQueryable<ShippingInstruction>>>(),
                      It.IsAny<string>())).Returns(shippingInstructioncodes);


            unitOfWork.Setup(t => t.ReceiveRepository).Returns(reieveRepository.Object);
            _accountTransactionService = new TransactionService(unitOfWork.Object, null, null, null);

            #endregion
        }

        #region Tests
            
        #endregion

        
    }
}