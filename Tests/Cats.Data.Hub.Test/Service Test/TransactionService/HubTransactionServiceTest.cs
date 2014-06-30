
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Data.Hub.Repository;
using Cats.Data.Hub.UnitWork;
using Cats.Models.Hubs.ViewModels;
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
        private IShippingInstructionService shippingInstructionService;

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
            var commodity = new List<Commodity>()
                                {
                                    new Commodity()
                                        {
                                            CommodityID = 1,
                                            Name = "Pulse"
                                        },
                                    new Commodity()
                                        {
                                            CommodityID = 2,
                                            Name = "Cereal"
                                        }
                                };
            var CommodityRepository = new Mock<IGenericRepository<Commodity>>();
            CommodityRepository.Setup(t => t.FindById(It.IsAny<int>())).Returns(
                (int id) => commodity.FirstOrDefault(t => t.CommodityID == id));
            
            var commodityType = new List<CommodityType>()
                                    {
                                        new CommodityType()
                                            {
                                                
                                                CommodityTypeID = 1,
                                                Name = "Food"
                                            },
                                            new CommodityType()
                                                {
                                                    CommodityTypeID = 2,
                                                    Name = "Non-Food"
                                                }
                                    };

            var commodityTypeRepository =new  Mock<IGenericRepository<CommodityType>>();
            commodityTypeRepository.Setup(t => t.FindById(It.IsAny<int>())).Returns((int id) => commodityType.
                                                                                                 FirstOrDefault(t => t.CommodityTypeID == id));

            commodityTypeRepository.Setup(t =>
                t.Get(It.IsAny<Expression<Func<CommodityType, bool>>>(),
                      It.IsAny<Func<IQueryable<CommodityType>, IOrderedQueryable<CommodityType>>>(),
                      It.IsAny<string>())).Returns(commodityType);
           

           

            var shippingInstructioncodes = new List<ShippingInstruction>()
                                               {
                                                   new ShippingInstruction()
                                                       {
                                                           ShippingInstructionID = 1,
                                                           Value = "si-0002",
                                                       },
                                                   new ShippingInstruction()
                                                       {
                                                           ShippingInstructionID = 2,
                                                           Value = "si-003",
                                                       }
                                               };

            Mock<IShippingInstructionService> MockShippingInstructionService = new Mock<IShippingInstructionService>();
            MockShippingInstructionService.Setup(s => s.GetSINumberIdWithCreate("si-0002")).Returns(shippingInstructioncodes.FirstOrDefault());

            var shippingInstructionRepository = new Mock<IGenericRepository<ShippingInstruction>>();

            shippingInstructionRepository.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<ShippingInstruction, bool>>>(),
                      It.IsAny<Func<IQueryable<ShippingInstruction>, IOrderedQueryable<ShippingInstruction>>>(),
                      It.IsAny<string>())).Returns(shippingInstructioncodes);




            var account = new List<Account>()
                              {
                                  new Account()
                                      {
                                          AccountID = 1,
                                          EntityID = 1,
                                          EntityType = "Sample Entity"
                                      }
                              };

            var accountRepository = new Mock<IGenericRepository<Account>>();
            var MockAccountService = new Mock<IAccountService>();
            MockAccountService.Setup(a => a.GetAccountIdWithCreate("Sample Entity", 1)).Returns(1);

             var projectCodes = new List<ProjectCode>()
                                   {
                                       new ProjectCode()
                                           {
                                               ProjectCodeID = 1,
                                               Value = "pro-001"
                                           },
                                       new ProjectCode()
                                           {
                                               ProjectCodeID = 1,
                                               Value = "0001246"
                                           },
                                   };

            var MockProjectCodeService = new Mock<IProjectCodeService>();
            MockProjectCodeService.Setup(p => p.GetProjectCodeIdWIthCreate("pro-001")).Returns(projectCodes.FirstOrDefault());
           
            var projectCodeRepositoy = new Mock<IGenericRepository<ProjectCode>>();
            projectCodeRepositoy.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<ProjectCode, bool>>>(),
                      It.IsAny<Func<IQueryable<ProjectCode>, IOrderedQueryable<ProjectCode>>>(),
                      It.IsAny<string>())).Returns(projectCodes);


            shippingInstructionService = MockShippingInstructionService.Object;
            unitOfWork.Setup(t => t.ReceiveRepository).Returns(reieveRepository.Object);
            unitOfWork.Setup(t => t.CommodityTypeRepository).Returns(commodityTypeRepository.Object);
            unitOfWork.Setup(t => t.CommodityRepository).Returns(CommodityRepository.Object);
            _accountTransactionService = new TransactionService(unitOfWork.Object, MockAccountService.Object, shippingInstructionService, MockProjectCodeService.Object);


        }
        #endregion
        #region Tests
        [Test]
        public void DoesReceiptTransactionSave()
        {

            var receiveViewModel = new List<ReceiveViewModel>()
                                       {
                                           new ReceiveViewModel()
                                               {
ChangeStoreManPermanently = false,
Commodities = new List<Commodity>()
                  {
                      new Commodity()
                          {
                              CommodityID = 1,
                              Name = "Pulse"
                          }
                  },CommodityGrades = new List<CommodityGrade>()
                                          {
                                              new CommodityGrade()
                                                  {
                                                      CommodityGradeID = 1,
                                                      Name = "one",
                                                      Description = ""
                                                  }
                                          },CommoditySourceID = 1,
                                          CommoditySourceText = "Donation",
                                          CommoditySources = new List<CommoditySource>()
                                                                 {
                                                                     new CommoditySource()
                                                                         {
                                                                             CommoditySourceID = 1,
                                                                             Name = "Donation"
                                                                         }
                                                                 },CommodityTypeID = 1,
                                                                 CommodityTypes = new List<CommodityType>()
                                                                                      {
                                                                                          new CommodityType()
                                                                                              {
                                                                                                  CommodityTypeID = 1,
                                                                                                  Name = "Food"
                                                                                              }
                                                                                      },CreatedDate = DateTime.Today,
                                                                                      Donors = new List<Donor>()
                                                                                                   {
                                                                                                       new Donor()
                                                                                                           {
                                                                                                               DonorID = 1,
                                                                                                               Name = "WFP"
                                                                                                           }
                                                                                                   },
                                                                                                   DriverName = "Dawit",
                                                                                                    GRN = "grn-001",
                                                                                                    HubID = 1,
                                                                                                    Hubs = new List<Models.Hubs.Hub>()
                                                                                                               {
                                                                                                                   new Models.Hubs.Hub()
                                                                                                                       {
                                                                                                                           HubID = 1,
                                                                                                                           Name = "Kombelcha"
                                                                                                                       }
                                                                                                               }, ReceiptAllocationID = Guid.NewGuid(),
                                                                                                               PurchaseOrder = "p-001",
                                                                                                               ProjectNumber = "pro-001",
                                                                                                               Programs = new List<Program>()
                                                                                                                              {
                                                                                                                                  new Program()
                                                                                                                                      {
                                                                                                                                          ProgramID = 1,
                                                                                                                                          Name = "Relief"
                                                                                                                                      }
                                                                                                                              },ProgramID = 1,
                                                                                                                              PortName = "Kombelacha",
                                                                                                                              PlateNo_Trailer = "trailer 001",
                                                                                                                              PlateNo_Prime = "prime 002",
                                                                                                                              JSONUpdatedCommodities = "",
                                                                                                                              JSONPrev = "",
                                                                                                                              JSONInsertedCommodities  ="",
                                                                                                                              JSONDeletedCommodities = "",
                                                                                                                              IsEditMode = false,
                                                                                                                              ReceiptDate = DateTime.Today,
                                                                                                                              ReceiveDetails = new List<ReceiveDetailViewModel>()
                                                                                                                                                   {
                                                                                                                                                       new ReceiveDetailViewModel()
                                                                                                                                                           {
                                                                                                                                                                CommodityGradeID = 1,
                                                                                                                                                                CommodityID = 1,
                                                                                                                                                                Description = "",
                                                                                                                                                                ReceiveDetailCounter = 1,
                                                                                                                                                                ReceiveDetailID = Guid.NewGuid(),
                                                                                                                                                                ReceiveID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                                                                                                                                                                ReceivedQuantityInMT = 1200,
                                                                                                                                                                ReceivedQuantityInUnit = 12,
                                                                                                                                                                SentQuantityInMT = 1200,
                                                                                                                                                                SentQuantityInUnit = 12,
                                                                                                                                                                UnitID = 1

                                                                                                                                                           }
                                                                                                                                                   },ReceiveID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                                                                                                                                                    ReceivedByStoreMan = "Dawit",
                                                                                                                                                    Remark = "A test View Model",
                                                                                                                                                    ResponsibleDonorID = 1,
                                                                                                                                                    SINumber = "si-0002",
                                                                                                                                                    Stores = new List<Store>()
                                                                                                                                                                 {
                                                                                                                                                                     new Store()
                                                                                                                                                                         {
                                                                                                                                                                             Hub = new Models.Hubs.Hub()
                                                                                                                                                                                       {
                                                                                                                                                                                           HubID = 1,
                                                                                                                                                                                           Name = "DireDawa"
                                                                                                                                                                                       },Name = "Sample Store",
                                                                                                                                                                                       HubID = 1,
                                                                                                                                                                                       IsActive = true,
                                                                                                                                                                                       IsTemporary = false,
                                                                                                                                                                                       Number = 2,
                                                                                                                                                                                       StackCount = 5,
                                                                                                                                                                                      StoreID = 1,
                                                                                                                                                                                      StoreManName = "Sisay",
                                                                                                                                                                                     

                                                                                                                                                                         }
                                                                                                                                                                 },StoreID = 1,
                                                                                                                                                                 Stacks = new List<AdminUnitItem>()
                                                                                                                                                                              {
                                                                                                                                                                                  new AdminUnitItem()
                                                                                                                                                                                      {
                                                                                                                                                                                          Id = 1,
                                                                                                                                                                                          Name = "Abebe"
                                                                                                                                                                                      }
                                                                                                                                                                              },StackNumber = 5,
                                                                                                                                                                              SourceHubText = "",
                                                                                                                                                                              SourceHubID = 1,
                                                                                                                                                                              SourceDonorID = 1,
                                                                                                                                                                              SupplierName = "Tana",
                                                                                                                                                                              TicketNumber = "t-001",
                                                                                                                                                                              TransporterID = 1,
                                                                                                                                                                              Transporters = new List<Transporter>()
                                                                                                                                                                                                 {
                                                                                                                                                                                                     new Transporter()
                                                                                                                                                                                                         {
                                                                                                                                                                                                             TransporterID = 1,
                                                                                                                                                                                                             Name = "tana",
                                                                                                                                                                                                            
                                                                                                                                                                                                         }
                                                                                                                                                                                                 },
                                                                                                                                                                                                               UserProfileID = 1,
                                                                                                                                                                                                               VesselName = "Vessele Name",
                                                                                                                                                                                                               WayBillNo = "wayBill - 001",
                                                                                                                                                                                                               WeightAfterUnloading = 0,
                                                                                                                                                                                                               WeightBeforeUnloading = 1200
                                                                                                                                                                              
                                                                                                                                                    


                                               }
                                       };

            var userProfile = new List<UserProfile>()
                                  {
                                      new UserProfile()
                                          {
                                              UserProfileID = 1,
                                              UserName = "Admin",
                                              FirstName = "Admin",
                                              DefaultHubObj = new Models.Hubs.Hub()
                                                               {
                                                                   HubID = 1,
                                                                   Name = "DireDawa"
                                                               }
                                          }
                                  };
            bool result = _accountTransactionService.SaveReceiptTransaction(receiveViewModel.FirstOrDefault(), userProfile.FirstOrDefault());
        }
        #endregion


    }
}