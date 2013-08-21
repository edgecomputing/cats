using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
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
            var adminUnitService = new Mock<IAdminUnitService>();
            var hrdService = new Mock<IHRDService>();
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
            Assert.IsInstanceOf<List<HRDCompareViewModel>>(((ViewResult)result).Model);
        }

        #endregion
    }
}
