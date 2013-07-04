using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Data.Repository;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.EarlyWarning;

using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class ReliefRequisitionControllerTests
    {
        #region SetUp / TearDown

        private ReliefRequisitionController _reliefRequisitionController;
        private ICommodityService _commodityService;
        private IRegionalRequestService _regionalRequestService;
        private List<RegionalRequest> _regionalRequests ;
        [SetUp]
        public void Init()
        {
            List<ReliefRequisition> reliefRequisitions = new List<ReliefRequisition>()
                                     {
                                         new ReliefRequisition()
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
                                                 RequestedDate = DateTime.Today

                                             },
                                         new ReliefRequisition()
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
                                                 RequestedBy = 2
                                                 ,
                                                 RequestedDate = DateTime.Today

                                             },
                                     };

            var commodityList = new List<Commodity>()
                                    {
                                        new Commodity
                                            {
                                                CommodityID = 1
                                                ,
                                                CommodityCode = "C1"
                                                ,
                                                CommodityTypeID = 1
                                                ,
                                                Name = "CSB"

                                            },
                                        new Commodity
                                            {
                                                CommodityID = 2
                                                ,
                                                CommodityCode = "C2"
                                                ,
                                                CommodityTypeID = 2
                                                ,
                                                Name = "Grain"

                                            },
                                        new Commodity
                                            {
                                                CommodityID = 3
                                                ,
                                                CommodityCode = "C3"
                                                ,
                                                CommodityTypeID = 3
                                                ,
                                                Name = "Oil"

                                            },
                                        new Commodity
                                            {
                                                CommodityID = 4
                                                ,
                                                CommodityCode = "C4"
                                                ,
                                                CommodityTypeID = 4
                                                ,
                                                Name = "Pulse"

                                            }
                                    };
            var regionalRequests = new List<RegionalRequest>()
                                       {
                                           new RegionalRequest
                                               {
                                                   ProgramId = 1
                                                   ,
                                                   Round = 1
                                                   ,
                                                   RegionID = 1
                                                   ,
                                                   RegionalRequestID = 1
                                                   ,
                                                   RequistionDate = DateTime.Today
                                                   ,
                                                   Year = DateTime.Today.Year
                                                   ,
                                                   RegionalRequestDetails = new List<RegionalRequestDetail>
                                                                                {
                                                                                    new RegionalRequestDetail
                                                                                        {
                                                                                            Beneficiaries = 100
                                                                                            ,
                                                                                            CSB = 10
                                                                                            ,
                                                                                            Grain = 20
                                                                                            ,
                                                                                            Oil = 30
                                                                                            ,
                                                                                            Pulse = 40
                                                                                            ,
                                                                                            Fdpid = 1
                                                                                            ,
                                                                                            RegionalRequestID = 1
                                                                                            ,
                                                                                            RegionalRequestDetailID = 1
                                                                                        },
                                                                                    new RegionalRequestDetail
                                                                                        {
                                                                                            Beneficiaries = 100
                                                                                            ,
                                                                                            CSB = 50
                                                                                            ,
                                                                                            Grain = 60
                                                                                            ,
                                                                                            Oil = 70
                                                                                            ,
                                                                                            Pulse = 80
                                                                                            ,
                                                                                            Fdpid = 2
                                                                                            ,
                                                                                            RegionalRequestID = 1
                                                                                            ,
                                                                                            RegionalRequestDetailID = 2
                                                                                        }
                                                                                }
                                               }

                                       };
            var mockReliefRequistionService = new Mock<IReliefRequisitionService>();
            mockReliefRequistionService.Setup(t => t.GetAllReliefRequisition()).Returns(reliefRequisitions);

            var mockRegionalRequestService = new Mock<IRegionalRequestService>();
            mockRegionalRequestService.Setup(t => t.GetAllReliefRequistion()).Returns(regionalRequests);

            mockRegionalRequestService.Setup(
                t => t.Get(It.IsAny<Expression<Func<RegionalRequest, bool>>>(), null, It.IsAny<string>())).Returns(regionalRequests.AsQueryable());


            var mockCommodityService = new Mock<ICommodityService>();
            mockCommodityService.Setup(m => m.GetCommoidtyId(It.IsAny<string>())).Returns((string propertyNameCSB) =>
            {
                return commodityList.Find(
                      t =>
                      t.Name.ToLower() ==
                      propertyNameCSB.ToLower()).CommodityID;
            });
            _regionalRequests = regionalRequests;
            _commodityService = mockCommodityService.Object;
            _regionalRequestService = mockRegionalRequestService.Object;

            _reliefRequisitionController = new ReliefRequisitionController(mockReliefRequistionService.Object, mockRegionalRequestService.Object, mockCommodityService.Object);





        }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Tests

        [Test]
        public void Count_Of_Requistion_Should_Be_2()
        {
            //Arange





            //Act
            var view = _reliefRequisitionController.Requistions();


            //Asert
            Assert.IsInstanceOf<ViewResult>(view);
            Assert.AreEqual(((IEnumerable<ReliefRequisition>)view.Model).Count(), 2);



        }

        [Test]
        public void Request_CSB_Column_Should_Map_To_1_In_Commodity_Id()
        {
            //Act

            var commodityId = _commodityService.GetCommoidtyId("CSB");

            //Assert
            Assert.AreEqual(1, commodityId);


        }
        [Test]
        public void Request_Grain_Column_Should_Map_To_2_In_Commodity_Id()
        {
            //Act

            var commodityId = _commodityService.GetCommoidtyId("Grain");

            //Assert
            Assert.AreEqual(2, commodityId);


        }
        [Test]
        public void Request_Oil_Column_Should_Map_To_3_In_Commodity_Id()
        {
            //Act

            var commodityId = _commodityService.GetCommoidtyId("oil");

            //Assert
            Assert.AreEqual(3, commodityId);


        }
        [Test]
        public void Request_Pulse_Column_Should_Map_To_4_In_Commodity_Id()
        {
            //Act

            var commodityId = _commodityService.GetCommoidtyId("Pulse");

            //Assert
            Assert.AreEqual(4, commodityId);


        }
        [Test]
        public void Can_Create_Requistion()
        {
            //Arange 
            var regionalRequest = _regionalRequests.Find(t=>t.RegionalRequestID==1);
            var commodityId = 1;
            var requisiton = _reliefRequisitionController.CreateRequisition(regionalRequest, commodityId,1);

            Assert.AreEqual(commodityId, requisiton.CommodityID);
            Assert.AreEqual(2, requisiton.ReliefRequisitionDetails.Count);
            Assert.AreEqual(commodityId,requisiton.ReliefRequisitionDetails.First().CommodityID);

        }

        [Test]
        public void Can_Create_Requistion_From_Request()
        {
            //Arange 
            List<ReliefRequisition> reliefRequisitions = _reliefRequisitionController.CreateRequistionFromRequest(1);
            var requestCommodity = new RegionalRequestDetail();
            var commdities = new int?[]
                                 {
                                     _commodityService.GetCommoidtyId(requestCommodity.GrainName),
                                     _commodityService.GetCommoidtyId(requestCommodity.OilName),
                                     _commodityService.GetCommoidtyId(requestCommodity.PulseName),
                                     _commodityService.GetCommoidtyId(requestCommodity.CSBName)
                                 };
           

            Assert.AreEqual(4, reliefRequisitions.Count);
            Assert.IsTrue(reliefRequisitions.All(t => commdities.Contains(t.CommodityID)));
           
            Assert.IsTrue(reliefRequisitions.All(t=>t.ReliefRequisitionDetails.Count == 2));
        }

        [Test]
        public void Can_Create_New_Requistion()
        {            
            var view = _reliefRequisitionController.NewRequisiton(1);
            //Asert
            Assert.IsInstanceOf<ViewResult>(view);
            Assert.AreEqual(((IEnumerable<ReliefRequisition>)view.Model).Count(), 4);
        }

        #endregion
    }
}
