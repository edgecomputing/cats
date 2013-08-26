using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    public class HRDControllerTest
    {
        private HRDController _hrdController;

        #region SetUp

        [SetUp]
        public void init()
        {
            var hrd = new List<HRD>
                {
                    new HRD { HRDID =1,RationID =1,SeasonID = 1, Status = 1,CreatedBY = 1,CreatedDate =new DateTime(12/12/2012),
                              PublishedDate =new DateTime(12/12/2013),Year = 2012,NeedAssessmentID = 1},
                    new HRD { HRDID =2,RationID =1,SeasonID = 2, Status = 2,CreatedBY = 2,CreatedDate =new DateTime(12/12/2012),
                              PublishedDate =new DateTime(12/12/2013),Year = 2012,NeedAssessmentID = 2},
                   
                };

            var hrdService = new Mock<IHRDService>();
            hrdService.Setup(m => m.GetAllHRD()).Returns(hrd);

            var hrdDetail = new List<HRDDetail>
                {
                    new HRDDetail {HRDDetailID = 1,HRDID = 1,WoredaID = 10,NumberOfBeneficiaries = 100,DurationOfAssistance = 3,StartingMonth = 1},
                    new HRDDetail {HRDDetailID = 2,HRDID = 1,WoredaID = 11,NumberOfBeneficiaries = 100,DurationOfAssistance = 3,StartingMonth = 1}
                };

            var hrdDetailService = new Mock<IHRDDetailService>();
            hrdDetailService.Setup(m => m.GetAllHRDDetail()).Returns(hrdDetail);

            var adminUnit = new List<AdminUnit>
                {
                    new AdminUnit { AdminUnitID =1,Name = "Afar"},
                    new AdminUnit { AdminUnitID =2,Name = "Amhara"}
                };
            var adminUnitService = new Mock<IAdminUnitService>();
            adminUnitService.Setup(m => m.GetAllAdminUnit()).Returns(adminUnit);

            var commodity = new List<Commodity>
                {
                    new Commodity {CommodityID = 1,Name = "CSB"},
                    new Commodity {CommodityID = 2,Name = "Pulse"}
                };
            var commodityService = new Mock<ICommodityService>();
            commodityService.Setup(m => m.GetAllCommodity()).Returns(commodity);

            var ration = new List<Ration>
                {
                    new Ration {RationID = 1,IsDefaultRation = true},
                    new Ration {RationID=2,IsDefaultRation = false}
                };
            var rationService = new Mock<IRationService>();
            rationService.Setup(m => m.GetAllRation()).Returns(ration);

            var rationDetail = new List<RationDetail>
                {
                    new RationDetail {RationDetailID = 1,RationID = 1,CommodityID = 1,Amount = 12},
                    new RationDetail {RationDetailID = 2,RationID = 1,CommodityID = 2,Amount = 10}
                };

            var rationDetailService = new Mock<IRationDetailService>();
            rationDetailService.Setup(m => m.GetAllRationDetail()).Returns(rationDetail);

            var needAssessment = new List<NeedAssessmentHeader>
                {
                    new NeedAssessmentHeader {NAHeaderId = 1, NeedAssessment = new NeedAssessment {NeedADate = new DateTime(12/12/2012)} }
                };

            var needAssessmentService = new Mock<INeedAssessmentHeaderService>();
            needAssessmentService.Setup(m => m.GetAllNeedAssessmentHeader()).Returns(needAssessment);

            var needAssessmentDetail = new List<NeedAssessmentDetail>
                {
                    ////new NeedAssessmentDetail {NAId = 1,NAHeaderId = 1,District = 12}
                };
            var needAssessmentDetailService = new Mock<INeedAssessmentDetailService>();
            needAssessmentDetailService.Setup(m => m.GetAllNeedAssessmentDetail()).Returns(needAssessmentDetail);

            var workFlowStatus = new List<WorkflowStatus>
                {
                     new WorkflowStatus {
                                          Description = "Draft",
                                          StatusID = 1,
                                          WorkflowID = 1
                                        },
                                  new WorkflowStatus
                                      {
                                          Description = "Approved",
                                          StatusID = 2,
                                          WorkflowID = 1
                                      },
                                  new WorkflowStatus
                                      {
                                          Description = "Published",
                                          StatusID = 3,
                                          WorkflowID = 1
                                      }
                };
            var workFlowStatusService = new Mock<IWorkflowStatusService>();

            var season = new List<Season>
                {
                    new Season {SeasonID = 1,Name = "Belge"},
                    new Season {SeasonID = 2,Name = "Mehere"}
                };

            var seasonService = new Mock<ISeasonService>();
            seasonService.Setup(m => m.GetAllSeason()).Returns(season);

            _hrdController = new HRDController(adminUnitService.Object, hrdService.Object, rationService.Object, rationDetailService.Object, 
                                               hrdDetailService.Object, commodityService.Object,needAssessmentDetailService.Object,needAssessmentService.Object,
                                               workFlowStatusService.Object,seasonService.Object);


        }

        [TearDown]
        public void Dispose()
        {
            _hrdController.Dispose();
        }

        #endregion

        #region Tests
        [Test]
        public void CanShowIndex()
        {
            var result = _hrdController.Index();

            Assert.IsNotNull(result);
        }
        [Test]
        public void CanReadHRD()
        {
            var hrd = new DataSourceRequest();
          
            var result = (JsonResult)_hrdController.HRD_Read(hrd);

            Assert.IsNotNull(result);
           // Assert.AreEqual(1, (((DataSourceResult)result.Data).Total));
  
        }

        [Test]
        public void CanReadHRDDetail()
        {
            var hrdDetail = new DataSourceRequest();
            var id = 2;

            var result = (RedirectToRouteResult)_hrdController.HRDDetail_Read(hrdDetail, id);

            Assert.IsNotNull(result);
        }
     
        //[Test]
        //public void CanEditHRD()
        //{
        //    //var result = _hrdController.Edit(1);

        //    //Assert.IsNotNull(result);
        //    ////Assert.IsInstanceOf<HRD>(result.Model);
        //}

        //[Test]
        //public void CanApproveDraftHRDs()
        //{
        //    var result = (ViewResult)_hrdController.ApproveHRD(1);

        //    Assert.IsNotNull(result);
        //    //    Assert.IsInstanceOf<HRD>(result.Model);

        //}

        #endregion
    }
}
