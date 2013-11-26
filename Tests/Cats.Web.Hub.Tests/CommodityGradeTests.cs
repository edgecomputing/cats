using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class CommodityGradeTests
    {
        #region SetUp / TearDown

        private CommodityGradeController _commodityGradeController;
        [SetUp]
        public void Init()
        {
            var commodityGrades = new List<CommodityGrade>
                {
                    new CommodityGrade {CommodityGradeID = 1, Name = "1st Grade", Description = null, SortOrder = 1},
                    new CommodityGrade {CommodityGradeID = 2, Name = "2nd Grade", Description = null, SortOrder = 2},
                    new CommodityGrade{CommodityGradeID = 3, Name = "3rd Grade", Description = null, SortOrder = 3},
                    new CommodityGrade{CommodityGradeID = 4, Name = "4th Grade", Description = null, SortOrder = 4},
                };
            var commodityGradeService = new Mock<ICommodityGradeService>();
            commodityGradeService.Setup(t => t.GetAllCommodityGrade()).Returns(commodityGrades);
            commodityGradeService.Setup(t => t.FindById(1)).Returns(commodityGrades[0]);
            commodityGradeService.Setup(t => t.FindById(2)).Returns(commodityGrades[1]);
            commodityGradeService.Setup(t => t.FindById(3)).Returns(commodityGrades[2]);
            commodityGradeService.Setup(t => t.FindById(4)).Returns(commodityGrades[3]);
            var userProfileService = new Mock<IUserProfileService>();
            _commodityGradeController = new CommodityGradeController(commodityGradeService.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _commodityGradeController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void ConViewindex()
        {
            //ACT
            var viewResult = _commodityGradeController.Index() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<IEnumerable<CommodityGrade>>(model);
            Assert.AreEqual(4, ((IEnumerable<CommodityGrade>)model).Count());
        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var viewResult = _commodityGradeController.Details(1);

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<CommodityGrade>(model);
        }

        [Test]
        public void CanDoCreatePostBack()
        {
            //ACT
            var commodityGrade = new CommodityGrade {Name = "5th Grade", Description = null, SortOrder = 5};
            var jsonResult = _commodityGradeController.Create(commodityGrade) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanDoEditPostBack()
        {
            //ACT
            var commodityGrade = new CommodityGrade {CommodityGradeID = 1, Name = "1st Grade", Description = null, SortOrder = 1 };
            var jsonResult = _commodityGradeController.Edit(commodityGrade) as JsonResult;

            //ASSERT
            Assert.NotNull(jsonResult);
            dynamic data = jsonResult.Data;
            Assert.AreEqual(true, data.success);
        }

        [Test]
        public void CanRedirectDeleteConfirm()
        {
            //ACt
            var redirectToRouteResult = _commodityGradeController.DeleteConfirmed(1) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("CommodityGrade", redirectToRouteResult.RouteValues["controller"]);
        }

        #endregion
    }
}
