using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Controllers;
using Cats.Web.Adminstration.Models.ViewModels;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Adminstration.Tests.ControllerTest
{  
   [TestFixture]
   public  class UnitControllerTest
   {
       private UnitController _unitController;
       #region SetUp
       [SetUp]
       public void Init()
       {
           var unit = new List<Unit>()
               {
                   new Unit {UnitID = 1,Name = "Kilo"},
                   new Unit {UnitID = 2,Name = "Bag"}
               };
           var unitService = new Mock<IUnitService>();
           unitService.Setup(m => m.GetAllUnit()).Returns(unit);

           _unitController=new UnitController(unitService.Object);
       }
       [TearDown]
       public void Dispose()
       {
           _unitController.Dispose();
       }
       #endregion
       #region Tests
       [Test]
       public void CanShowIndex()
       {
           var result = _unitController.Index();
           Assert.IsNotNull(result);
       }
       [Test]
       public void CanReadUnit()
       {
           var request = new DataSourceRequest();
           var result = _unitController.Unit_Read(request);
           Assert.IsInstanceOf<JsonResult>(result);
           Assert.IsNotNull(result);
       }
       [Test]
       public void CanCreateUnit()
       {
           var request = new DataSourceRequest();
           var unitViewModel = new UnitViewModel{UnitID = 1,UnitName = "Killo Gram"};
           var result = _unitController.Unit_Create(request, unitViewModel);

           Assert.IsInstanceOf<JsonResult>(result);
       }
       [Test]
       public void CanDeleteUnit()
       {
           var result = _unitController.Delete(1);
           Assert.IsInstanceOf<ActionResult>(result);
       }
#endregion

   }
}
