using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Data.Repository;
using Cats.Models.Hubs;
using Cats.Services.Transaction;
using NUnit.Framework;
using Moq;
using Cats.Data.UnitWork;
using System.Linq.Expressions;
using Hub = Cats.Models.Hub;
using TransactionGroup = Cats.Models.TransactionGroup;

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
            var commodities = new List<Commodity>()
                                  {
                                      new Commodity()
                                          {
                                              CommodityID = 1,
                                              Name = "Commodity1"
                                          },
                                      new Commodity()
                                          {
                                              CommodityID = 2,
                                              Name = "Commodity2"
                                          }
                                  };
            var transporters = new List<Transporter>()
                                   {
                                       new Transporter()
                                           {
                                               TransporterID = 1,
                                               Name = "Transporter1"
                                           },
                                       new Transporter()
                                           {
                                               TransporterID = 2,
                                               Name = "Transporter2"
                                           }
                                   };
            var units = new List<Unit>()
                            {
                                new Unit()
                                    {
                                        UnitID = 1,
                                        Name = "Unit1"
                                    },
                                new Unit()
                                    {
                                        UnitID = 2,
                                        Name = "Unit2"
                                    }
                            };
            var fdps = new List<FDP>()
                           {
                               new FDP()
                                   {
                                       FDPID = 1,
                                       Name = "FDP1"
                                   },
                               new FDP()
                                   {
                                       FDPID = 2,
                                       Name = "FDP2"
                                   }
                           };
            var programs = new List<Program>()
                               {
                                   new Program()
                                       {
                                           ProgramID = 1,
                                           Name = "Program1"
                                       },
                                   new Program()
                                       {
                                           ProgramID = 2,
                                           Name = "Program2"
                                       }
                               };
            var regions = new List<AdminUnit>()
                              {
                                  new AdminUnit()
                                      {
                                          AdminUnitID = 1,
                                          Name = "AdminUnit1"
                                      },
                                  new AdminUnit()
                                      {
                                          AdminUnitID = 2,
                                          Name = "AdminUnit2"
                                      }
                              };
            var zones = new List<AdminUnit>()
                              {
                                  new AdminUnit()
                                      {
                                          AdminUnitID = 3,
                                          Name = "AdminUnit3"
                                      },
                                  new AdminUnit()
                                      {
                                          AdminUnitID = 4,
                                          Name = "AdminUnit4"
                                      }
                              };
            var stores = new List<Store>()
                             {
                                 new Store()
                                     {
                                         StoreID = 1,
                                         Name = "Store1"
                                     },
                                 new Store()
                                     {
                                         StoreID = 2,
                                         Name = "Store2"
                                     }
                             };
            var dispatchModels = new List<DispatchModel>()
                                       {
                                           new DispatchModel(commodities,transporters,units,fdps,programs,regions,zones,stores)
                                                {

                                                }
                                                    
                                       };
            var dispatchModelRepositoy = new Mock<IGenericRepository<DispatchModel>>();
            dispatchModelRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<DispatchModel, bool>>>(),
                      It.IsAny<Func<IQueryable<DispatchModel>, IOrderedQueryable<DispatchModel>>>(),
                      It.IsAny<string>())).Returns(dispatchModels);
            dispatchModelRepositoy.Setup(t => t.FindById(It.IsAny<Guid>())).Returns((Guid id) => dispatchModels.
                                                                                                 FirstOrDefault(t => t.DispatchID == id));

            var transactionRepository = new Mock<IGenericRepository<Models.Transaction>>();
            transactionRepository.Setup(t => t.Add(It.IsAny<Models.Transaction>())).Returns(true);
            var transactionGroupRepository = new Mock<IGenericRepository<TransactionGroup>>();
            transactionGroupRepository.Setup(t => t.Add(It.IsAny<TransactionGroup>())).Returns(true);

            //unitOfWork.Setup(t => t.DispatchRepositoy).Returns(dispatchModelRepositoy.Object);
            unitOfWork.Setup(t => t.TransactionRepository).Returns(transactionRepository.Object);
            unitOfWork.Setup(t => t.TransactionGroupRepository).Returns(transactionGroupRepository.Object);
            unitOfWork.Setup(t => t.Save());
            _hubTransactionService = new TransactionService(unitOfWork.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _hubTransactionService.Dispose();
        }

        #endregion
        [Test]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
