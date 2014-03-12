using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Data.Hub;
using Cats.Data.Hub.Repository;
using Cats.Data.Hub.UnitWork;
//using Cats.Data.Repository;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using NUnit.Framework;
using Moq;
using Cats.Data.Hub;
using System.Linq.Expressions;
using TransactionGroup = Cats.Models.Hubs.TransactionGroup;

namespace Cats.Tests.Service_Tests
{
    /// <summary>
    /// Summary description for HubTransactionServiceTest
    /// </summary>
    [TestFixture]
    public class HubTransactionServiceTest
    {
        #region SetUp / TearDown

        private TransactionService _hubTransactionService;
        [SetUp]
        public void Init()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var dispatches = new List<Dispatch>()
                                       {
                                           new Dispatch()
                                                {
                                                    DispatchID = Guid.NewGuid(),
                                                    PartitionID = 1,
                                                    HubID = 1,
                                                    GIN = "GIN-123",
                                                    FDPID = 1,
                                                    WeighBridgeTicketNumber = "",
                                                    RequisitionNo = "REQ-123",
                                                    BidNumber = "00000",
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
                                                    Remark = "Sample Remark",
                                                    DispatchedByStoreMan = "Kebede",
                                                }
                                                    
                                       };
            var dispatchRepositoy = new Mock<IGenericRepository<Dispatch>>();
            dispatchRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<Dispatch, bool>>>(),
                      It.IsAny<Func<IQueryable<Dispatch>, IOrderedQueryable<Dispatch>>>(),
                      It.IsAny<string>())).Returns(dispatches);
            dispatchRepositoy.Setup(t => t.FindById(It.IsAny<Guid>())).Returns((Guid id) => dispatches.
                                                                                                 FirstOrDefault(t => t.DispatchID == id));

            var commodityTypes = new List<CommodityType>()
                                       {
                                           new CommodityType()
                                                {
                                                    CommodityTypeID = 1,
                                                    Name = "Cereal"
                                                },
                                           new CommodityType()
                                                {
                                                    CommodityTypeID = 2,
                                                    Name = "CSB"
                                                }         
                                       };
            var commodityTypeRepositoy = new Mock<IGenericRepository<CommodityType>>();
            commodityTypeRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<CommodityType, bool>>>(),
                      It.IsAny<Func<IQueryable<CommodityType>, IOrderedQueryable<CommodityType>>>(),
                      It.IsAny<string>())).Returns(commodityTypes);
            commodityTypeRepositoy.Setup(t => t.FindById(It.IsAny<int>())).Returns((int id) => commodityTypes.
                                                                                                 FirstOrDefault(t => t.CommodityTypeID == id));

            var transactionRepository = new Mock<IGenericRepository<Transaction>>();
            transactionRepository.Setup(t => t.Add(It.IsAny<Transaction>())).Returns(true);
            var transactionGroupRepository = new Mock<IGenericRepository<TransactionGroup>>();
            transactionGroupRepository.Setup(t => t.Add(It.IsAny<TransactionGroup>())).Returns(true);

            unitOfWork.Setup(t => t.DispatchRepository).Returns(dispatchRepositoy.Object);
            unitOfWork.Setup(t => t.CommodityTypeRepository).Returns(commodityTypeRepositoy.Object);
            unitOfWork.Setup(t => t.TransactionRepository).Returns(transactionRepository.Object);
            unitOfWork.Setup(t => t.TransactionGroupRepository).Returns(transactionGroupRepository.Object);
            unitOfWork.Setup(t => t.Save());
            _hubTransactionService = new TransactionService(unitOfWork.Object,null,null,null);
        }

        [TearDown]
        public void Dispose()
        {
            _hubTransactionService.Dispose();
        }

        #endregion
        //[Test]
        //public void ShouldSaveDispatchTransactionTransaction()
        //{
        //    var commodities = new List<Commodity>()
        //                          {
        //                              new Commodity()
        //                                  {
        //                                      CommodityID = 1,
        //                                      Name = "Commodity1"
        //                                  },
        //                              new Commodity()
        //                                  {
        //                                      CommodityID = 2,
        //                                      Name = "Commodity2"
        //                                  }
        //                          };
        //    var transporters = new List<Transporter>()
        //                           {
        //                               new Transporter()
        //                                   {
        //                                       TransporterID = 1,
        //                                       Name = "Transporter1"
        //                                   },
        //                               new Transporter()
        //                                   {
        //                                       TransporterID = 2,
        //                                       Name = "Transporter2"
        //                                   }
        //                           };
        //    var units = new List<Unit>()
        //                    {
        //                        new Unit()
        //                            {
        //                                UnitID = 1,
        //                                Name = "Unit1"
        //                            },
        //                        new Unit()
        //                            {
        //                                UnitID = 2,
        //                                Name = "Unit2"
        //                            }
        //                    };
        //    var fdps = new List<FDP>()
        //                   {
        //                       new FDP()
        //                           {
        //                               FDPID = 1,
        //                               Name = "FDP1"
        //                           },
        //                       new FDP()
        //                           {
        //                               FDPID = 2,
        //                               Name = "FDP2"
        //                           }
        //                   };
        //    var programs = new List<Program>()
        //                       {
        //                           new Program()
        //                               {
        //                                   ProgramID = 1,
        //                                   Name = "Program1"
        //                               },
        //                           new Program()
        //                               {
        //                                   ProgramID = 2,
        //                                   Name = "Program2"
        //                               }
        //                       };
        //    var regions = new List<AdminUnit>()
        //                      {
        //                          new AdminUnit()
        //                              {
        //                                  AdminUnitID = 1,
        //                                  Name = "AdminUnit1"
        //                              },
        //                          new AdminUnit()
        //                              {
        //                                  AdminUnitID = 2,
        //                                  Name = "AdminUnit2"
        //                              }
        //                      };
        //    var zones = new List<AdminUnit>()
        //                      {
        //                          new AdminUnit()
        //                              {
        //                                  AdminUnitID = 3,
        //                                  Name = "AdminUnit3"
        //                              },
        //                          new AdminUnit()
        //                              {
        //                                  AdminUnitID = 4,
        //                                  Name = "AdminUnit4"
        //                              }
        //                      };
        //    var stores = new List<Store>()
        //                     {
        //                         new Store()
        //                             {
        //                                 StoreID = 1,
        //                                 Name = "Store1"
        //                             },
        //                         new Store()
        //                             {
        //                                 StoreID = 2,
        //                                 Name = "Store2"
        //                             }
        //                     };
        //    var dispatchModel = new DispatchModel(commodities,transporters,units,fdps,programs,regions,zones,stores)
        //                                {
        //                                    DispatchDetails = new List<DispatchDetailModel>()
        //                                                          {
        //                                                              new DispatchDetailModel()
        //                                                                  {
        //                                                                      Id = Guid.NewGuid(),
        //                                                                      DispatchID = Guid.NewGuid(),
        //                                                                      DispatchDetailCounter = 1,
        //                                                                      CommodityName = "Commodiy1",
        //                                                                      CommodityID = 1,
        //                                                                      RequestedQuantityMT = 100,
        //                                                                      DispatchedQuantityMT = 100,
        //                                                                      RequestedQuantity = 100,
        //                                                                      DispatchedQuantity = 100,
        //                                                                      Unit = 1,
        //                                                                      Description = "Sample Description"
        //                                                                  },
        //                                                               new DispatchDetailModel()
        //                                                                  {
        //                                                                      Id = Guid.NewGuid(),
        //                                                                      DispatchID = Guid.NewGuid(),
        //                                                                      DispatchDetailCounter = 1,
        //                                                                      CommodityName = "Commodiy1",
        //                                                                      CommodityID = 1,
        //                                                                      RequestedQuantityMT = 100,
        //                                                                      DispatchedQuantityMT = 100,
        //                                                                      RequestedQuantity = 100,
        //                                                                      DispatchedQuantity = 100,
        //                                                                      Unit = 1,
        //                                                                      Description = "Sample Description"
        //                                                                  }
        //                                                          },
        //                                    CommodityTypeID = 1,
        //                                    Type = 1,
        //                                    BidNumber = "00000",
        //                                    DispatchAllocationID = Guid.NewGuid(),
        //                                    OtherDispatchAllocationID = Guid.NewGuid(),
        //                                    ProgramID = 1,
        //                                    FDPID = 1,
        //                                    SINumber = "SI-123",
        //                                    ProjectNumber = "PC-123",
        //                                    StackNumber = 123,
        //                                    StoreID = 1,
        //                                };

        //    var user = new UserProfile()
        //                   {
        //                       UserProfileID = 1,
        //                       UserName = "admin",
        //                       Password = "password",
        //                       FirstName = "Abebe",
        //                       LastName = "Balcha",
        //                       GrandFatherName = "Asnake",
        //                       ActiveInd = true,
        //                       LoggedInInd = true,
        //                       FailedAttempts = 100,
        //                       LanguageCode = "AM",
        //                       DatePreference = "EC",
        //                       PreferedWeightMeasurment = "Qtl",
        //                       MobileNumber = "+251911123456",
        //                       Email = "email@cats.com",
        //                       DefaultHub = new Hub()
        //                                        {
        //                                            HubID = 1,
        //                                            Name = "DireDawa"
        //                                        }
        //                   };
        //    //Act
        //    //var result = _hubTransactionService.SaveDispatchTransaction(dispatchModel,user);

        //    //Assert
        //    //Assert.IsTrue(result);
        //}
    }
}
