using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using NUnit.Framework;
using Cats.Web.Hub.Controllers;
using Moq;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class UnitControllerTests 
    {
        #region SetUp / TearDown

        private UnitController _unitController;
        [SetUp]
        public void Init()
        {
            var units = new List<Unit>
                {
                    new Unit {Name = "Kg", UnitID = 1},
                    new Unit {Name = "Ml", UnitID = 2},
                };
            var unitService = new Mock<IUnitService>();
            unitService.Setup(t => t.GetAllUnit()).Returns(units);
            var userProfileService = new Mock<IUserProfileService>();
            _unitController = new UnitController(unitService.Object,userProfileService.Object);

        }

        [TearDown]
        public void Dispose()
        {
            _unitController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var result = _unitController.Index();
            var model = result.Model;
            //Assert

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<IEnumerable<Unit>>(model);
            Assert.AreEqual(2, ((IEnumerable<Unit>)model).Count());
        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var result = _unitController.Details(1);
            var model = result.Model;
            //Assert

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<Unit>(model);  
            Assert.IsNotNullOrEmpty(((Unit)model).UnitID.ToString(CultureInfo.InvariantCulture));
            Assert.IsNotNullOrEmpty(((Unit)model).Name);
        }

        [Test]
        public void CanDoPostBackCreate()
        {
            //ACT
            var unit = new Unit {Name = "Litre"};
            var result = _unitController.Create(unit);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<int>(unit.UnitID);
        }

        [Test]
        public void CanViewEdit()
        {
            //ACT
            var result = _unitController.Edit(1);
            var model = ((ViewResult)result).Model;
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<Unit>(model);
        }

        [Test]
        public void CanDoPostBackEdit()
        {
            //ACT
            var unit = new Unit {UnitID = 1, Name = "Mt"};
            var result = _unitController.Edit(unit);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<Unit>(unit.UnitID);
        }

        [Test]
        public void CanDoPostBackDelete()
        {
            //ACT
            var result = _unitController.Delete(2);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        #endregion
    }
}
