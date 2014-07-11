using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Data.Repository;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.ViewModels;
using Cats.Services.Logistics;
using Moq;
using NUnit.Framework;

namespace Cats.Data.Tests.ServicesTest.Logistics
{
    [TestFixture]
    public class BeneficiaryAllocationServiceTests
    {
        #region SetUp / TearDown

        private BeneficiaryAllocationService _beneficiaryAllocationService;
        [SetUp]
        public void Init()
        {
            var reliefRequisitionDetails = new List<ReliefRequisitionDetail>()
                                               {
                                                   new ReliefRequisitionDetail()
                                                       {
                                                           Amount = 10,
                                                           BenficiaryNo = 11,
                                                           CommodityID = 12,
                                                           DonorID = 13,
                                                           FDPID = 14,
                                                           RequisitionID = 1,
                                                           RequisitionDetailID = 1, 
                                                           Commodity = new Commodity(){
                                                               CommodityID = 12,
                                                               Name = "Grain"
                                                           },
                                                           FDP=new FDP
                                                                   {
                                                                       FDPID=14,
                                                                       Name="FDP-234",
                                                                     AdminUnit= new AdminUnit()
                                                                                               {
                                                                                                   AdminUnitID = 1,
                                                                                                   Name = "R1"
                                                                                               }
                                                                   },
                                                           Donor=new Donor
                                                                     {
                                                                         Name="WFP",
                                                                         DonorID=23,
                                                                         
                                                                     },
                                                           ReliefRequisition = new ReliefRequisition()
                                                                                   {
                                                                                       RequisitionNo = "REQ001"
                                                                                       ,
                                                                                       ApprovedBy = 1
                                                                                       ,
                                                                                       ApprovedDate = DateTime.Today
                                                                                       ,
                                                                                       CommodityID = 1
                                                                                       ,
                                                                                       ProgramID = 1
                                                                                       ,
                                                                                       RegionID = 1
                                                                                       ,
                                                                                       RequisitionID = 1
                                                                                       ,
                                                                                       Status = 1
                                                                                       ,
                                                                                       Round = 1
                                                                                       ,
                                                                                       ZoneID = 1
                                                                                       ,
                                                                                       RequestedBy = 1
                                                                                       ,
                                                                                       RegionalRequestID = 1,
                                                                                       RequestedDate = DateTime.Today
                                                                                       ,
                                                                                       Program =
                                                                                           new Program()
                                                                                               {
                                                                                                   ProgramID = 1,
                                                                                                   Name = "P1"
                                                                                               },
                                                                                       AdminUnit1 =
                                                                                           new AdminUnit()
                                                                                               {
                                                                                                   AdminUnitID = 1,
                                                                                                   Name = "R1"
                                                                                               }
                                                                                       ,
                                                                                       AdminUnit =
                                                                                           new AdminUnit
                                                                                               {
                                                                                                   AdminUnitID = 2,
                                                                                                   Name = "A2",
                                                                                                   AdminUnit2 =
                                                                                                       new AdminUnit
                                                                                                           {
                                                                                                               AdminUnitID
                                                                                                                   = 3,
                                                                                                               Name =
                                                                                                                   "A3"
                                                                                                           }
                                                                                               }


                                                                                   },
                                                       }
                                                   ,
                                                   new ReliefRequisitionDetail
                                                       {
                                                           Amount = 20,
                                                           BenficiaryNo = 21,
                                                           CommodityID = 12,
                                                           DonorID = 23,
                                                           FDPID = 24,
                                                           RequisitionID = 1,
                                                           RequisitionDetailID = 2, 
                                                           Commodity = new Commodity(){
                                                               CommodityID = 12,
                                                               Name = "Grain"
                                                           },
                                                            FDP=new FDP
                                                                   {
                                                                       FDPID=24,
                                                                       Name="FDP-244",
                                                                     AdminUnit= new AdminUnit()
                                                                                               {
                                                                                                   AdminUnitID = 1,
                                                                                                   Name = "R1"
                                                                                               }
                                                                   },
                                                           Donor=new Donor
                                                                     {
                                                                         Name="WFP",
                                                                         DonorID=23,
                                                                         
                                                                     },
                                                           ReliefRequisition = new ReliefRequisition()
                                                                                   {
                                                                                       RequisitionNo = "REQ001"
                                                                                       ,
                                                                                       ApprovedBy = 2
                                                                                       ,
                                                                                       ApprovedDate = DateTime.Today
                                                                                       ,
                                                                                       CommodityID = 2
                                                                                       ,
                                                                                       ProgramID = 2
                                                                                       ,
                                                                                       RegionID = 2
                                                                                       ,
                                                                                       RequisitionID = 2
                                                                                       ,
                                                                                       Status = 1
                                                                                       ,
                                                                                       Round = 2
                                                                                       ,
                                                                                       ZoneID = 2
                                                                                       ,
                                                                                       RequestedBy = 2,
                                                                                       RegionalRequestID = 1,
                                                                                       RequestedDate = DateTime.Today
                                                                                       ,
                                                                                       Program =
                                                                                           new Program()
                                                                                               {
                                                                                                   ProgramID = 1,
                                                                                                   Name = "P1"
                                                                                               },
                                                                                       AdminUnit1 =
                                                                                           new AdminUnit()
                                                                                               {
                                                                                                   AdminUnitID = 1,
                                                                                                   Name = "R1"
                                                                                               }
                                                                                       ,
                                                                                       AdminUnit =
                                                                                           new AdminUnit
                                                                                               {
                                                                                                   AdminUnitID = 2,
                                                                                                   Name = "A2",
                                                                                                   AdminUnit2 =
                                                                                                       new AdminUnit
                                                                                                           {
                                                                                                               AdminUnitID
                                                                                                                   = 3,
                                                                                                               Name =
                                                                                                                   "A3"
                                                                                                           }
                                                                                               }
                                                                                   },
                                                       },
                                               };
            var reliefRequisitionDetailRepository = new Mock<IGenericRepository<ReliefRequisitionDetail>>();
            reliefRequisitionDetailRepository.Setup(
                t => t.Get(It.IsAny<Expression<Func<ReliefRequisitionDetail, bool>>>(), null, It.IsAny<string>())).
                Returns(
                    reliefRequisitionDetails.AsQueryable());
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(t => t.ReliefRequisitionDetailRepository).Returns(reliefRequisitionDetailRepository.Object);
            _beneficiaryAllocationService = new BeneficiaryAllocationService(unitOfWork.Object);

        }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Tests

        [Test]
        public void CanGetListBenficiaryAllocation()
        {
            //Act
            var result = _beneficiaryAllocationService.GetBenficiaryAllocation();

            //Assert

            Assert.IsInstanceOf<List<BeneficiaryAllocation>>(result.ToList());
        }

        #endregion
    }
}
