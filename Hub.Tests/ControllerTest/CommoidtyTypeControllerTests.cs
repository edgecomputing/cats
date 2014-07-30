using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.Settings.Controllers;
using Cats.Models;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;
using Cats.Areas.Hub.Controllers;
using Cats.Areas.Settings.Models.ViewModels;
using Cats.Services.Hub;

namespace Hub.Tests.ControllerTest
{
    [TestFixture]
    public class CommoidtyTypeControllerTests
    {
        private CommodityTypeController _commodityTypeController;
        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            //var commodityTypeService = new Mock<ICommodityTypeService>();
            //var commodityTypes = new List<CommodityType>
            //                         {
            //                             new CommodityType() {CommodityTypeID = 1, Name = "CSB"},
            //                             new CommodityType() {CommodityTypeID = 2, Name = "Oil"}
            //                         };
            //commodityTypeService.Setup(t => t.GetAllCommodityType()).Returns(commodityTypes);
            //commodityTypeService.Setup(t => t.AddCommodityType(It.IsAny<CommodityType>())).Returns(true);
            //commodityTypeService.Setup(t => t.EditCommodityType(It.IsAny<CommodityType>())).Returns(true);
            //commodityTypeService.Setup(t => t.DeleteById(It.IsAny<int>())).Returns(true);
            //_commodityTypeController=new CommodityTypeController(commodityTypeService.Object);
        }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Tests

        [Test]
        public void CanReadCommodityType()
        {
            //var request = new DataSourceRequest();
            //var result = _commodityTypeController.CommodityType_Read(request);
            //Assert.IsInstanceOf<JsonResult>(result);
        }
        [Test]
        public void ShouldCreateCommodityType()
        {
            //var commdityType = new CommodityTypeViewModel() { CommodityTypeId = 1, Name = "CSB" };
            //var request = new DataSourceRequest();
            //var result = _commodityTypeController.CommodityType_Create(request, commdityType);
            //Assert.IsInstanceOf<JsonResult>(result);
        }
        [Test]
        public void ShouldUpdateCommodityType()
        {
            //var commdityType = new CommodityTypeViewModel() { CommodityTypeId = 1, Name = "CSB" };
            //var request = new DataSourceRequest();
            //var result = _commodityTypeController.CommodityType_Update(request,commdityType);
            //Assert.IsInstanceOf<JsonResult>(result);
        }
        //[Test]
        //public void ShouldDeleteCommodityType()
        //{
        //    var commdityType = new CommodityTypeViewModel() {CommodityTypeId = 1, Name = "CSB"};
        //    var request = new DataSourceRequest();
        //    var result = _commodityTypeController.CommodityType_Destroy(request, commdityType);
        //    Assert.IsInstanceOf<JsonResult>(result);
        //}
        #endregion
    }
}
