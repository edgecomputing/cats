using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Cats.ViewModelBinder;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class HRDControllerTests
    {
        #region SetUp / TearDown

        private HRDController _hrdController;
        [SetUp]
        public void Init()
        {
            var hrds = new List<HRD>
                          {
                              new HRD()
                                  {
                                      Year = 2013,
                                      Season = new Season() {Name = "Mehere", SeasonID = 1},
                                      RationID = 1,
                                      HRDDetails =
                                          new List<HRDDetail>()
                                              {
                                                  new HRDDetail()
                                                      {
                                                          DurationOfAssistance = 2,
                                                          HRDDetailID = 1,
                                                          NumberOfBeneficiaries = 300,
                                                          WoredaID = 1,
                                                          AdminUnit =
                                                              new AdminUnit()
                                                                  {
                                                                      Name = "Woreda",
                                                                      AdminUnitID = 2,
                                                                      AdminUnit2 =
                                                                          new AdminUnit()
                                                                              {
                                                                                  Name = "Zone",
                                                                                  AdminUnitID = 3,
                                                                                  AdminUnit2 =
                                                                                      new AdminUnit()
                                                                                          {
                                                                                              Name = "Region",
                                                                                              AdminUnitID = 6
                                                                                          }
                                                                              }
                                                                  }
                                                      }
                                              }
                                  }
                          };
            var hrdService = new Mock<IHRDService>();
            hrdService.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<HRD, bool>>>(), It.IsAny<Func<IQueryable<HRD>, IOrderedQueryable<HRD>>>(),
                      It.IsAny<string>())).Returns(hrds);


            var adminUnitService = new Mock<IAdminUnitService>();
            adminUnitService.Setup(t => t.GetRegions()).Returns(new List<AdminUnit>()
                                                                    {new AdminUnit() {AdminUnitID = 1, Name = "Region"}});
         
            var rationService = new Mock<IRationService>();
            var rationDetailService = new Mock<IRationDetailService>();
            var hrdDetailService = new Mock<IHRDDetailService>();
            var commodityService = new Mock<ICommodityService>();
            var needAssesmentDetailService = new Mock<INeedAssessmentDetailService>();
            var needAssesmentHeaderService = new Mock<INeedAssessmentHeaderService>();
            var workflowSatusService = new Mock<IWorkflowStatusService>();
            var seasonService = new Mock<ISeasonService>();
            _hrdController = new HRDController(adminUnitService.Object, 
                hrdService.Object, 
                rationService.Object, 
                rationDetailService.Object,
                hrdDetailService.Object,
                commodityService.Object,
                needAssesmentDetailService.Object,
                needAssesmentHeaderService.Object,
                workflowSatusService.Object,
                seasonService.Object
                );
        }

        [TearDown]
        public void Dispose()
        { _hrdController.Dispose();
            
        }

        #endregion

        #region Tests

        [Test]
        public void ShouldCompareTwoHRD()
        {
            //ACT
            var result = _hrdController.Compare();

            //Assert

            Assert.IsInstanceOf<ViewResult>(result);
           
        }

        #endregion
    }
}
