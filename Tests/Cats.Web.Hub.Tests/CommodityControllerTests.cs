using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class CommodityControllerTests
    {
        #region SetUp / TearDown
        private CommodityController _commodityController;
        [SetUp]
        public void Init()
        {
            var commodities = new List<Commodity>
                {
                    new Commodity {CommodityID = 1, Name = "Cereal", CommodityCode = "CER", CommodityTypeID = 1, ParentID = null},
                    new Commodity {CommodityID = 5, Name = "Wheat", CommodityCode = null, CommodityTypeID = 1, ParentID = 1},
                };
            var commodityService = new Mock<ICommodityService>();
            commodityService.Setup(t => t.GetAllCommodity()).Returns(commodities);

            var commodityTypes = new List<CommodityType>
                {
                    new CommodityType {CommodityTypeID = 1, Name = "Food"},
                    new CommodityType {CommodityTypeID = 2, Name = "Non Food"}
                };
            var commodityTypeService = new Mock<ICommodityTypeService>();
            commodityTypeService.Setup(t => t.GetAllCommodityType()).Returns(commodityTypes);
            var userProfileService = new Mock<IUserProfileService>();
            _commodityController = new CommodityController(commodityTypeService.Object, commodityService.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _commodityController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var result = _commodityController.Index() as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model;

            //ASSERT
            Assert.IsInstanceOf<IEnumerable<CommodityType>>(result.ViewBag.CommodityTypes);
            Assert.IsInstanceOf<SelectList>(result.ViewBag.ParentID);
            Assert.IsInstanceOf<Int32>(result.ViewBag.SelectedCommodityID);
            Assert.IsInstanceOf<IOrderedEnumerable<Commodity>>(result.ViewBag.Parents);
            Assert.IsInstanceOf<IEnumerable<Commodity>>(model);
            Assert.AreEqual(2, ((IOrderedEnumerable<Commodity>)model).Count());
        }

        [Test]
        public void CanViewCommodityListPartial()
        {
            //ACT
            var result = _commodityController.CommodityListPartial() as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model;

            //Assert
            Assert.AreEqual(false, result.ViewBag.ShowParentCommodity);
            Assert.IsInstanceOf<SelectList>(result.ViewBag.ParentID);
            Assert.IsInstanceOf<Int32>(result.ViewBag.SelectedCommodityID);
            Assert.IsInstanceOf<IEnumerable<Commodity>>(model);
            Assert.AreEqual(2, ((IOrderedEnumerable<Commodity>)model).Count());
        }

        [Test]
        public void CanViewSubCommodityListPartial()
        {
            //ACT
            var result = _commodityController.SubCommodityListPartial(1) as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model;

            //Assert
            Assert.AreEqual(true, result.ViewBag.ShowParentCommodity);
            Assert.IsInstanceOf<Int32>(result.ViewBag.SelectedCommodityID);
            Assert.IsInstanceOf<IEnumerable<Commodity>>(model);
            Assert.AreEqual(1, ((IOrderedEnumerable<Commodity>)model).Count());
        }

        [Test]
        public void CanViewGetParentList()
        {
            //ACT
            var jsonResult = _commodityController.GetParentList() as JsonResult;
            Assert.IsNotNull(jsonResult);
            var model = jsonResult.Data;
            
            //ASSERT
            Assert.IsInstanceOf<SelectList>(model);
            Assert.AreEqual(1, ((IOrderedEnumerable<Commodity>)model).Count());
        }

        [Test]
        public void CanDoUpdate()
        {
            //ACT
            var viewResult = _commodityController.Update(1) as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model;

            //ASSERT
            Assert.IsInstanceOf<SelectList>(viewResult.ViewBag.ParentID);
            Assert.IsInstanceOf<IEnumerable<Commodity>>(viewResult.ViewBag.Parents);
            Assert.IsInstanceOf<IEnumerable<Commodity>>(model);
        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var viewResult = _commodityController.Details(5);
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model;

            //ASSERT
            Assert.IsInstanceOf<Commodity>(model);
        }

        [Test]
        public void CanDoCreate()
        {
            //ACT
            var viewResult = _commodityController.Create(0, 1) as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model;

            //ASSERT
            Assert.IsInstanceOf<SelectList>(viewResult.ViewBag.ParentID);
            Assert.AreEqual(1, ((SelectList)viewResult.ViewBag.ParentID).Count());
            Assert.AreEqual(1, ((SelectList)viewResult.ViewBag.CommodityTypeID).Count());
            Assert.AreEqual("Food", viewResult.ViewBag.CommodityType);
            Assert.AreEqual("Cereal", viewResult.ViewBag.ParentCommodity);
            Assert.AreEqual(1, viewResult.ViewBag.SelectedCommodityID);
            Assert.AreEqual(true, viewResult.ViewBag.isParent);
            Assert.IsInstanceOf<Commodity>(model);
        }

        [Test]
        public void CanDoCreatePostback()
        {
            //ACT
            var commodity = new Commodity{Name = "Rice", CommodityCode = null, CommodityTypeID = 1, ParentID = 1, CommodityID = 1};
            var result = _commodityController.Create(commodity);
            Assert.IsNotNull(result);
            var viewResult = result as ViewResult;
            var model = viewResult.Model;
            var jsonResult = result as JsonResult;

            //ASSERT
            Assert.IsNotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
            Assert.IsInstanceOf<Commodity>(model);
        }

        [Test]
        public void CanDoEdit()
        {
            //ACT
            var parentViewResult = _commodityController.Edit(1) as ViewResult;
            Assert.IsNotNull(parentViewResult);
            var parentModel = parentViewResult.Model;
            var childViewResult = _commodityController.Edit(5) as ViewResult;
            Assert.IsNotNull(childViewResult);
            var childModel = childViewResult.Model;

            //ASSERT
            Assert.IsInstanceOf<Commodity>(parentModel);
            Assert.IsInstanceOf<SelectList>(parentViewResult.ViewBag.ParentID);
            Assert.IsInstanceOf<SelectList>(parentViewResult.ViewBag.CommodityTypeID);
            Assert.AreEqual(false, parentViewResult.ViewBag.ShowParentCommodity);
            Assert.AreEqual(true, parentViewResult.ViewBag.isParent);

            Assert.IsInstanceOf<Commodity>(childModel);
            Assert.IsInstanceOf<SelectList>(childViewResult.ViewBag.ParentID);
            Assert.IsInstanceOf<SelectList>(childViewResult.ViewBag.CommodityTypeID);
            Assert.AreEqual(true, childViewResult.ViewBag.ShowParentCommodity);
            Assert.AreEqual(false, childViewResult.ViewBag.isParent);
            
        }

        [Test]
        public void CanViewParentCommodities()
        {
            //ACT
            var result = _commodityController.ParentCommodities(1);
            var jsonResult = result as JsonResult;

            //ASSERT
            Assert.IsNotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.IsInstanceOf<SelectList>(data);
        }
        
        [Test]
        public void CanDoEditPostBack()
        {
            //ACT
            var commodity = new Commodity
                {CommodityID = 1, Name = "Cereal", CommodityCode = "CER", CommodityTypeID = 1, ParentID = null};
            var result = _commodityController.Edit(commodity);
            var jsonResult = result as JsonResult;

            //ASSERT
            Assert.IsNotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanDoDeletePostBack()
        {
            //ACT
            var redirectToRouteResult = _commodityController.DeleteConfirmed(5) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Commodity", redirectToRouteResult.RouteValues["controller"]);
        }

        [Test]
        public void CanReturnCommodityParentListByType()
        {
            //ACT
            var jsonResult = _commodityController.CommodityParentListByType(1, 1) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            Assert.NotNull(jsonResult.Data);
            Assert.IsNotInstanceOf<EmptyResult>(jsonResult.Data);
            Assert.IsInstanceOf<SelectList>(jsonResult.Data);
        }

        [Test]
        public void CanReturnCommodityListByType()
        {
            //ACT
            var jsonResult = _commodityController.CommodityListByType(1, 1, "123", 1) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            Assert.NotNull(jsonResult.Data);
            Assert.IsNotInstanceOf<EmptyResult>(jsonResult.Data);
            Assert.IsInstanceOf<ArrayList>(jsonResult.Data);
        }

        #endregion
    }
}
