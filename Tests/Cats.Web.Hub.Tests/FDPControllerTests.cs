using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using NUnit.Framework;
using Cats.Web.Hub.Controllers;
using Moq;

namespace DRMFSS.Web.Test
{
    [TestFixture]
    public class FDPControllerTests 
    {
        #region SetUp / TearDown

        private FDPController _fdpController;
        
        [SetUp]
        public void Init()
        {
            var fdps = new List<FDP>
                {
                    new FDP {Name = "FDP 1", FDPID = 1,AdminUnitID=4,NameAM="FDP AND"},
                    new FDP {Name = "FDP 2", FDPID = 2,AdminUnitID=4,NameAM="FDP Hulet"},
                };
            var fdpService = new Mock<IFDPService>();
            var adminUnitService = new Mock<IAdminUnitService>();
            fdpService.Setup(t => t.GetAllFDP()).Returns(fdps);
            _fdpController = new FDPController(fdpService.Object, adminUnitService.Object);

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
            //ACT
            var result = _fdpController.Index();
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
            var result = _fdpController.Details(1);
            var model = result.Model;
            //Assert

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<FDP>(model);
            Assert.IsNotNullOrEmpty(((FDP)model).FDPID.ToString(CultureInfo.InvariantCulture));
            Assert.IsNotNullOrEmpty(((FDP)model).Name);
        }

        [Test]
        public void CanDoPostBackCreate()
        {
            //ACT
            var fdp = new FDP {Name = "..."};
            var result = _fdpController.Create(fdp);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<int>(fdp.FDPID);
        }

        [Test]
        public void CanViewEdit()
        {
            //ACT
            var result = _fdpController.Edit(1);
            var model = ((ViewResult)result).Model;
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<FDP>(model);
        }

        [Test]
        public void CanDoPostBackEdit()
        {
            //ACT
            var fdp = new FDP {FDPID = 1, Name = "..."};
            var result = _fdpController.Edit(fdp);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<Unit>(fdp.FDPID);
        }

        [Test]
        public void CanDoPostBackDelete()
        {
            //ACT
            var result = _fdpController.Delete(2);
            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        #endregion
    }
}
