using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Controllers;
using Cats.Web.Adminstration.Models.ViewModels;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Adminstration.Tests
{
    [TestFixture]
    public class FDPControllerTest
    {
        #region SetUp / TearDown

        private FDPController _fdpController;
        [SetUp]
        public void Init()
        {
            var fdp = new List<FDP>
                {
                    new FDP(){FDPID = 1, Name = "fdp1",AdminUnitID = 1},
                    new FDP(){FDPID = 2, Name = "fdp2",AdminUnitID = 2},
                    new FDP(){FDPID = 3,Name = "fdp3",AdminUnitID = 3},
                };
            var adminUnit = new List<AdminUnit>
                {
                    new AdminUnit(){AdminUnitID = 1, Name = "adminUnit1"},
                    new AdminUnit(){AdminUnitID = 2, Name="adminUnit2"},
                    new AdminUnit(){AdminUnitID = 3,Name="adminUnit3"}
                };

            var fdpService = new Mock<IFDPService>();
            fdpService.Setup(t => t.GetAllFDP()).Returns(fdp);
            var adminUnitService = new Mock<IAdminUnitService>();
            adminUnitService.Setup(t => t.GetAllAdminUnit()).Returns(adminUnit);

            _fdpController = new FDPController(adminUnitService.Object,fdpService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _fdpController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //Act
            var result = _fdpController.Index();

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void CanFDPJsonRead()
        {
            //Act
            var kendoDataRequest = new DataSourceRequest();
            var jsonResult= _fdpController.FDP_Read(kendoDataRequest,1);

            //Assert
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            var results = data.Data as List<FDPViewModel>; 
            Assert.IsInstanceOf<List<FDPViewModel>>(results);
            Assert.NotNull(results);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void CanFDPCreateWork()
        {
            //ACT
            var kendoDataRequest = new DataSourceRequest();
            var fdpViewModel = new FDPViewModel()
                {
                    FDPID = 1,
                    Name = "fdpViewModel1",
                    AdminUnitID = 1
                };
            var jsonResult = _fdpController.FDP_Create(kendoDataRequest,fdpViewModel, 1) as JsonResult; 

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            var results = data.Data as FDPViewModel;
            Assert.IsInstanceOf<List<FDPViewModel>>(results);
            Assert.NotNull(results);
        }

        [Test]
        public void CanFDPUpdate()
        {
            var kendoDataRequest = new DataSourceRequest();
            var fdpViewModel = new FDPViewModel()
            {
                FDPID = 1,
                Name = "fdpViewModel1",
                AdminUnitID = 1
            };
            var jsonResult = _fdpController.FDP_Update(kendoDataRequest, fdpViewModel) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            var results = data.Data as FDPViewModel;
            Assert.NotNull(results);
            Assert.IsInstanceOf<List<FDPViewModel>>(results);
        }

        [Test]
        public void CanFDPDestory()
        {
            var kendoDataRequest = new DataSourceRequest();
            var fdpViewModel = new FDPViewModel()
            {
                FDPID = 1,
                Name = "fdpViewModel1",
                AdminUnitID = 1
            };
            var jsonResult = _fdpController.FDP_Destroy(kendoDataRequest, fdpViewModel) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            var results = data.Data as FDPViewModel;
            Assert.NotNull(results);
            Assert.IsInstanceOf<List<ModelStateDictionary>>(results);
        }

        [Test]
        public void CanGetCascadeRegions()
        {
            
        }

        #endregion
    }
}
