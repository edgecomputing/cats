using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;

using Moq;
using NUnit.Framework;

namespace DRMFSS.Web.Test
{
    [TestFixture]
    public class AdminUnitControllerTest
    {
        #region SetUp / TearDown

        private AdminUnitController _adminUnitController;
        [SetUp]
        public void Init()
        {
            var adminUnits = new List<AdminUnit>
                {
                    new AdminUnit {AdminUnitID = 1, Name = "Federal", ParentID = null, AdminUnitTypeID = 1},
                    new AdminUnit {AdminUnitID = 2, Name = "Afar", ParentID = 1, AdminUnitTypeID = 2},
                    new AdminUnit {AdminUnitID = 3, Name = "Zone1", ParentID = 2, AdminUnitTypeID = 3},
                    new AdminUnit {AdminUnitID = 4, Name = "Adhar", ParentID = 12, AdminUnitTypeID = 4},
                };
            var adminUnitTypes = new List<AdminUnitType>
                {
                    new AdminUnitType {AdminUnitTypeID = 1, Name = "Federal"},
                    new AdminUnitType {AdminUnitTypeID = 2, Name = "Region"},
                    new AdminUnitType {AdminUnitTypeID = 3, Name = "Zone"},
                    new AdminUnitType {AdminUnitTypeID = 4, Name = "Woreda"},
                };
            var adminUnitService = new Mock<IAdminUnitService>();
            adminUnitService.Setup(t => t.GetAllAdminUnit()).Returns(adminUnits);
            adminUnitService.Setup(t => t.GetAdminUnitType(1)).Returns(adminUnitTypes[1]);
            adminUnitService.Setup(t => t.GetAdminUnitType(2)).Returns(adminUnitTypes[2]);
            adminUnitService.Setup(t => t.GetRegions()).Returns(adminUnits);

            var userProfiles = new List<UserProfile>
                {
                    new UserProfile {UserProfileID = 1, UserName = "Nathnael", Password = "passWord", Email = "123@edge.com"},
                    new UserProfile {UserProfileID = 2, UserName = "Banty", Password="passWord", Email = "321@edge.com"},

                };
            var userProfileService = new Mock<IUserProfileService>();
            userProfileService.Setup(t => t.GetAllUserProfile()).Returns(userProfiles);

            var dispatchAllocation = new List<DispatchAllocation>
                {
                    new DispatchAllocation
                        {
                            DispatchAllocationID = new Guid("8296"),
                            PartitionID = 2,
                            HubID = 2,
                            CommodityID = 2,
                            RequisitionNo = "8296",
                            Amount = 500,
                            Unit = 2,
                            FDPID = 2,
                            IsClosed = false
                        },
                    new DispatchAllocation
                        {
                            DispatchAllocationID = new Guid("3096"),
                            PartitionID = 1,
                            HubID = 1,
                            CommodityID = 1,
                            RequisitionNo = "3096",
                            Amount = 1000,
                            Unit = 1,
                            FDPID = 1,
                            IsClosed = false
                        }
                };
            var dispatchAllocationService = new Mock<IDispatchAllocationService>();
            dispatchAllocationService.Setup(t => t.GetAllDispatchAllocation()).Returns(dispatchAllocation);

            _adminUnitController = new AdminUnitController(adminUnitService.Object, userProfileService.Object, dispatchAllocationService.Object);
        }


        [TearDown]
        public void Dispose()
        {
            _adminUnitController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var result = _adminUnitController.Index() as ViewResult;
            Debug.Assert(result != null, "result != null");
            var model = (result).Model;
            
            //Assert
            Assert.IsInstanceOf<IEnumerable<AdminUnitType>>(model);
            Assert.AreEqual(2, ((IEnumerable<AdminUnitType>)model).Count());
        }

        [Test]
        public void CanViewAdminUnits()
        {
            //ACT
            var result = _adminUnitController.AdminUnits(1) as ViewResult;
            Debug.Assert(result != null, "result != null");
            var model = result.Model;
            //Assert

            Assert.IsInstanceOf<IEnumerable<AdminUnit>>(result.ViewBag.Regions);
            Assert.IsNotNullOrEmpty(result.ViewBag.Title);
            Assert.AreNotEqual(0, result.ViewBag.SelectedTypeId);
            Assert.IsInstanceOf<IOrderedEnumerable<AdminUnit>>(model);
            Assert.AreEqual(1, ((IOrderedEnumerable<AdminUnit>)model).Count());
        }

        [Test]
        public void CanViewCreateZone ()
        {
            var result = _adminUnitController.CreateZone(2) as ViewResult;
            Debug.Assert(result != null, "result != null");
            var model = result.Model;

            //Assert
            Assert.IsInstanceOf<AdminUnitModel>(model);
        }

        [Test]
        public void CanViewCreateWoreda()
        {
            var result = _adminUnitController.CreateWoreda(3) as ViewResult;
            Debug.Assert(result != null, "result != null");
            var model = result.Model;

            //Assert
            Assert.IsInstanceOf<AdminUnitModel>(model);
        }

        [Test]
        public void CanViewCreate()
        {
            //ACT
            var adminUnitModel = new AdminUnitModel { UnitName = "W. Arsi" };
            var jsonResult = _adminUnitController.Create(adminUnitModel) as JsonResult;

            //ASSERT
            Assert.IsNotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanDoEditPostback()
        {
            //ACT
            var adminUnit = new AdminUnit {AdminUnitID = 1, Name = "Region", ParentID = null, AdminUnitTypeID = 2};
            var jsonResult = _adminUnitController.Edit(1, adminUnit) as JsonResult;

            //ASSERT
            Assert.IsNotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanDoDeletePostBack()
        {
            //ACT
            var adminUnit = new AdminUnit { AdminUnitID = 1, Name = "Region", ParentID = null, AdminUnitTypeID = 2 };
            dynamic result = _adminUnitController.Delete(1, adminUnit) as JsonResult;

            //ASSERT
            Assert.IsNotNull(result);
            Assert.AreEqual("AdminUnit/Index", result.redirect);
        }

        [Test]
        public void CanGetRegions()
        {
            //ACT
            dynamic jsonResult = _adminUnitController.GetRegions() as JsonResult;

            //ASSERT
            Assert.IsNotNull(jsonResult);
            Assert.IsInstanceOf<SelectList>(jsonResult.Data);
            Assert.AreEqual(1, jsonResult.Data.Count());
        }

        [Test]
        public void CanGetChildren()
        {
            //ACT
            var result = _adminUnitController.GetChildren(1);
            dynamic jsonResult = result as JsonResult;

            //ASSERT
            Assert.IsNotNull(jsonResult);
            Assert.IsInstanceOf<SelectList>(jsonResult.Data);
        }

        #endregion
    }
}
