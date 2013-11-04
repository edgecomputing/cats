using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Repository;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.Logistics;
using Moq;
using NUnit.Framework;

namespace Cats.Data.Tests.ServicesTest.Logistics
{
    [TestFixture]
    public class TransportRequisitionServiceTests
    {
        #region SetUp / TearDown

        private List<int> _reliefRequisitions;
        private List<TransportRequisition> _transportRequisitions;
        private TransportRequisitionService _transportRequisitionService;
        private TransportRequisition _transportRequisition;
        private IList<ReliefRequisition> reliefRequisitions;
        [SetUp]
        public void Init()
        {
            _transportRequisitions = new List<TransportRequisition>();
            _reliefRequisitions = new List<int> { 1 };
            var unitOfWork = new Mock<IUnitOfWork>();
            _transportRequisition = new TransportRequisition
            {
                Status = 1,
                RequestedDate = DateTime.Today,
                RequestedBy = 1,
                CertifiedBy = 1,
                CertifiedDate = DateTime.Today,
                Remark = "",
                TransportRequisitionNo = "REQ-001",
                TransportRequisitionID = 1,
                TransportRequisitionDetails = new List<TransportRequisitionDetail>()
                                                                                  {
                                                                                      new TransportRequisitionDetail
                                                                                          {
                                                                                              RequisitionID = 1,
                                                                                              TransportRequisitionDetailID
                                                                                                  = 1,
                                                                                              TransportRequisitionID = 1
                                                                                          }
                                                                                  }

            };
            reliefRequisitions = new List<ReliefRequisition>()
                                      {
                                          
                                          new ReliefRequisition()
                                              {
                                                  RegionID = 1,
                                                  ProgramID = 1,
                                                  CommodityID = 1,
                                                  ZoneID = 2,
                                                  RequisitionNo = "REQ-001",
                                                  Round = 1,
                                                  RegionalRequestID = 1,
                                                  RequisitionID = 1,
                                                  Status = 4,

                                                  AdminUnit = new AdminUnit
                                                                  {
                                                                      AdminUnitID = 2,
                                                                      Name = "Zone1"
                                                                  },
                                                  AdminUnit1 = new AdminUnit
                                                                   {
                                                                       AdminUnitID = 1,
                                                                       Name = "Region1"
                                                                   },
                                                  Commodity = new Commodity
                                                                  {
                                                                      CommodityID = 1,
                                                                      CommodityCode = "C1",
                                                                      Name = "CSB"
                                                                  },
                                                  //HubAllocations = new List<HubAllocation>(){new HubAllocation()
                                                  //                    {
                                                  //                        HubAllocationID = 1,
                                                  //                        HubID = 1,
                                                  //                        RequisitionID = 1,
                                                  //                        Hub = new Hub
                                                  //                                  {
                                                  //                                      HubID = 1,
                                                  //                                      Name = "Test Hub",

                                                  //                                  }

                                                  //                    }},

                                                  ReliefRequisitionDetails = new List<ReliefRequisitionDetail>
                                                                                 {
                                                                                     new ReliefRequisitionDetail()
                                                                                         {
                                                                                             RequisitionID = 1,
                                                                                             RequisitionDetailID = 1,
                                                                                             Amount = 100,
                                                                                             CommodityID = 1,
                                                                                             FDPID = 1,
                                                                                             BenficiaryNo = 10,
                                                                                             DonorID = 1,
                                                                                             FDP=new FDP
                                                                                                     {
                                                                                                         AdminUnitID=1,
                                                                                                         FDPID=1,
                                                                                                         Name="FDP1"
                                                                                                     }
                                                                                             
                                                                                         },
                                                                                     new ReliefRequisitionDetail()
                                                                                         {
                                                                                             RequisitionID = 1,
                                                                                             RequisitionDetailID = 2,
                                                                                             Amount = 50,
                                                                                             CommodityID = 1,
                                                                                             FDPID = 2,
                                                                                             BenficiaryNo = 10,
                                                                                             DonorID = 1,
                                                                                             FDP=new FDP
                                                                                                     {
                                                                                                         AdminUnitID=1,
                                                                                                         FDPID=2,
                                                                                                         Name="FDP2"
                                                                                                     }
                                                                                         },
                                                                                     new ReliefRequisitionDetail()
                                                                                         {
                                                                                             RequisitionID = 1,
                                                                                             RequisitionDetailID = 3,
                                                                                             Amount = 60,
                                                                                             CommodityID = 1,
                                                                                             FDPID = 3,
                                                                                             BenficiaryNo = 10,
                                                                                             DonorID = 1,
                                                                                             FDP=new FDP
                                                                                                     {
                                                                                                         AdminUnitID=1,
                                                                                                         FDPID=3,
                                                                                                         Name="FDP3"
                                                                                                     }
                                                                                         },
                                                                                     new ReliefRequisitionDetail()
                                                                                         {
                                                                                             RequisitionID = 1,
                                                                                             RequisitionDetailID = 4,
                                                                                             Amount = 70,
                                                                                             CommodityID = 1,
                                                                                             FDPID = 2,
                                                                                             BenficiaryNo = 10,
                                                                                             DonorID = 1,
                                                                                             FDP=new FDP
                                                                                                     {
                                                                                                         AdminUnitID=1,
                                                                                                         FDPID=4,
                                                                                                         Name="FDP4"
                                                                                                     }
                                                                                         }
                                                                                 }
                                              }
                                      };
            var _workflowstatuses = new List<WorkflowStatus>
                                            {
                                                new WorkflowStatus
                                                    {
                                                        StatusID = 1,
                                                        WorkflowID = 2,
                                                        Description = "Approved",

                                                    }
                                            };

            var _hubAllocation = new List<HubAllocation>
                                         {
                                             new HubAllocation
                                                 {
                                                     RequisitionID = 1,
                                                     HubID = 1,
                                                     HubAllocationID = 1,
                                                     Hub=new Hub
                                                             {
                                                                 Name="Hub 1",
                                                                 HubID=1,

                                                             }
                                                 }
                                         };
            //_transportRequisition = new TransportRequisition()
            //                            {
            //                                Status = 1,
            //                                RequestedBy = 1,
            //                                CertifiedBy = 1,
            //                                CertifiedDate = DateTime.Today,
            //                                RequestedDate = DateTime.Today,
            //                                TransportRequisitionID = 1,
            //                                TransportRequisitionNo = "T-001",
            //                                Remark = "comment"
            //                            };
            var mockReliefRequisitionRepository = new Mock<IGenericRepository<ReliefRequisition>>();
            mockReliefRequisitionRepository.Setup(
                t => t.Get(It.IsAny<Expression<Func<ReliefRequisition, bool>>>(), It.IsAny<Func<IQueryable<ReliefRequisition>, IOrderedQueryable<ReliefRequisition>>>(), It.IsAny<string>())).Returns(
                    (Expression<Func<ReliefRequisition, bool>> perdicate, Func<IQueryable<ReliefRequisition>, IOrderedQueryable<ReliefRequisition>> obrderBy, string prop) =>
                    {
                        var
                            result = reliefRequisitions.AsQueryable();
                        return result;
                    }
                );
            mockReliefRequisitionRepository.Setup(t => t.FindById(It.IsAny<int>())).Returns((int id) => reliefRequisitions
                                                                                                            .ToList().
                                                                                                            Find
                                                                                                            (t =>
                                                                                                             t.
                                                                                                                 RequisitionID ==
                                                                                                             id));
            unitOfWork.Setup(t => t.ReliefRequisitionRepository).Returns(mockReliefRequisitionRepository.Object);
            var transportRequisitionReqository = new Mock<IGenericRepository<TransportRequisition>>();

            transportRequisitionReqository.Setup(t => t.Add(It.IsAny<TransportRequisition>())).Returns(
                (TransportRequisition transportRequisiton) =>
                {
                    _transportRequisitions.Add(transportRequisiton);
                    return true;
                });
            transportRequisitionReqository.Setup(t => t.FindById(It.IsAny<int>())).Returns((int id) =>
                                                                                               {
                                                                                                   return
                                                                                                       _transportRequisition;
                                                                                               });
            unitOfWork.Setup(t => t.TransportRequisitionRepository).Returns(transportRequisitionReqository.Object);
            unitOfWork.Setup(t => t.Save());

            var workflowStatusRepository = new Mock<IGenericRepository<WorkflowStatus>>();
            workflowStatusRepository.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<WorkflowStatus, bool>>>(),
                      It.IsAny<Func<IQueryable<WorkflowStatus>, IOrderedQueryable<WorkflowStatus>>>(),
                      It.IsAny<string>())).Returns(_workflowstatuses);

            unitOfWork.Setup(t => t.WorkflowStatusRepository).Returns(workflowStatusRepository.Object);
            var hubAllocationRepository = new Mock<IGenericRepository<HubAllocation>>();
            hubAllocationRepository.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<HubAllocation, bool>>>(),
                      It.IsAny<Func<IQueryable<HubAllocation>, IOrderedQueryable<HubAllocation>>>(),
                      It.IsAny<string>())).Returns(_hubAllocation);
            unitOfWork.Setup(t => t.HubAllocationRepository).Returns(hubAllocationRepository.Object);

            var adminUnitRepository = new Mock<IGenericRepository<AdminUnit>>();
            adminUnitRepository.Setup(t => t.FindById(It.IsAny<int>())).Returns(new
                                                                                    AdminUnit()
                                                                                    {
                                                                                        AdminUnitID = 2,
                                                                                        Name = "Zone1",
                                                                                        AdminUnit2 = new AdminUnit
                                                                                                         {
                                                                                                             AdminUnitID
                                                                                                                 = 1,
                                                                                                             Name =
                                                                                                                 "Region1"
                                                                                                         }
                                                                                    }
                );
            unitOfWork.Setup(t => t.AdminUnitRepository).Returns(adminUnitRepository.Object);

            var programRepository = new Mock<IGenericRepository<Program>>();
            programRepository.Setup(t => t.FindById(It.IsAny<int>())).Returns(new Program
                                                                                  {
                                                                                      ProgramID = 1,
                                                                                      Name = "PSNP",
                                                                                      Description = "PSNP Des."
                                                                                  });
            unitOfWork.Setup(t => t.ProgramRepository).Returns(programRepository.Object);

            _transportRequisitionService = new TransportRequisitionService(unitOfWork.Object);


        }

        [TearDown]
        public void Dispose()
        {
            _transportRequisitionService.Dispose();
        }

        #endregion

        #region Tests
        [Test]
        public void CanGenerateRequisitonReadyToDispatch()
        {




            //Act 

            var requisitionToDispatch = _transportRequisitionService.GetRequisitionToDispatch().ToList();

            //Assert

            Assert.IsInstanceOf<IList<RequisitionToDispatch>>(requisitionToDispatch);
            Assert.AreEqual(1, requisitionToDispatch.Count());
        }
        [Test]
        public void ShouldCreateTransportRequision()
        {

            //Act
            var result = _transportRequisitionService.CreateTransportRequisition(_reliefRequisitions);
            //Assert
            Assert.IsInstanceOf<TransportRequisition>(result);
        }

        [Test]
        public void SholdNotCreateTransportRequisitionIfNoReliefRequisition()
        {
            //Act
            var result = _transportRequisitionService.CreateTransportRequisition(new List<int>());
            //Assert
            Assert.IsNull(result);
        }


        [Test]
        public void ShouldAddTransportRequisition()
        {
            //Act
            var count = _transportRequisitions.Count;
            _transportRequisitionService.AddTransportRequisition(_transportRequisition);

            var result = _transportRequisitions.Count;

            Assert.AreEqual(1 + count, result);
        }

        [Test]
        public void IsProjectCodeAndOrSINumberAssigned()
        {
            //Act

            var requisition = _transportRequisitionService.GetRequisitionToDispatch();
            var result = requisition.ToList().TrueForAll(t => t.RequisitionStatus == (int)ReliefRequisitionStatus.ProjectCodeAssigned);//Project Si Code Assigned,indirectly it means Hub also assigned
            //Assert

            Assert.IsTrue(result);
        }

        [Test]
        public void CanApproveTransportRequisition()
        {
            //Act

            var result = _transportRequisitionService.ApproveTransportRequisition(1);

            //Assert
            var status = _transportRequisition.Status;
            Assert.IsTrue(result);
            Assert.AreEqual((int)TransportRequisitionStatus.Approved, status);



        }
        #endregion
    }
}
