using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;
using System.Web.Script.Serialization;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class RationControllerTest
    {
        #region SetUp / TearDown

        private RationController _rationController;
        [SetUp]
        public void Init()
        {
            var rations = new List<Ration>()
                              {
                                  new Ration() {RationID = 1, CommodityID = 1, Amount = 15},
                                  new Ration {RationID = 2, CommodityID = 2, Amount = 1},
                                  new Ration() {RationID =3, CommodityID = 3, Amount = 2},
                                  new Ration {RationID = 4, CommodityID = 4, Amount = 4}
                              };
            var commodities = new List<Commodity>
                                  {
                                      new Commodity {CommodityID = 1, CommodityCode = "1", Name = "commodity 1"},
                                      new Commodity {CommodityID = 2, CommodityCode = "2", Name = "commodity 2"},
                                      new Commodity {CommodityID = 3, CommodityCode = "3", Name = "commodity 3"},
                                      new Commodity {CommodityID = 4, CommodityCode = "4", Name = "commodity 4"},
                                      new Commodity {CommodityID = 5, CommodityCode = "5", Name = "commodity 5"},
                                  };
            var rationService = new Mock<IRationService>();
            rationService.Setup(t => t.GetAllRation()).Returns(rations);

            rationService.Setup(t => t.AddRation(It.IsAny<Ration>())).Returns((Ration ration) =>
                                                                                  {
                                                                                      rations.Add(ration);
                                                                                      return true;
                                                                                  });
            rationService.Setup(t => t.EditRation(It.IsAny<Ration>())).Returns((Ration ration) =>
                                                                                   {
                                                                                       var target =
                                                                                           rations.Find(
                                                                                               t =>
                                                                                               t.RationID ==
                                                                                               ration.RationID);
                                                                                       target.Amount = ration.Amount;
                                                                                       return true;
                                                                                   });
            rationService.Setup(t => t.FindById(It.IsAny<int>())).Returns(
                (int id) => rations.Find(t => t.RationID == id));

            rationService.Setup(t => t.DeleteById(It.IsAny<int>())).Returns((int id) =>
                                                                                {
                                                                                    var origin =
                                                                                        rations.Find(
                                                                                            t => t.RationID == id);
                                                                                    rations.Remove(origin);
                                                                                    return true;
                                                                                });

            var commodityService = new Mock<ICommodityService>();
            commodityService.Setup(t => t.FindById(It.IsAny<int>())).Returns((int id) => commodities.Find(t => t.CommodityID == id));

            _rationController = new RationController(rationService.Object, commodityService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _rationController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanReadRation()
        {
            //Act
            var request = new DataSourceRequest();
            var result = _rationController.Ration_Read(request);

            //Assert

            Assert.IsInstanceOf<JsonResult>(result);

        }

        [Test]
        public void CanAddRation()
        {
            //Act
            var request = new DataSourceRequest();
            var rationViewModel = new RationViewModel() { Amount = 11, CommodityID = 5, Commodity = "Commodity 5", RationID = 5 };
            _rationController.Ration_Create(request, rationViewModel);
            var result = _rationController.Ration_Read(request);

            //Assert


            Assert.AreEqual(5, ((DataSourceResult)((JsonResult)result).Data).Total);
        }

        [Test]
        public void CanUpdateRation()
        {
            //Act
            var request = new DataSourceRequest();
            var rationViewModel = new RationViewModel() { Amount = 32, CommodityID = 4, Commodity = "Commodity 4", RationID = 4 };
            _rationController.Ration_Update(request, rationViewModel);
            var result = _rationController.Ration_Read(request);
            var data = ((JsonResult)result).Data;
            var rationsResult =
                (((DataSourceResult)data).Data).Cast<RationViewModel>();
            var updatedRation = rationsResult.ToList().Find(t => t.RationID == 4);
            //Assert
            Assert.AreEqual(4, rationsResult.Count());
            Assert.AreEqual(32, updatedRation.Amount);
        }

        [Test]
        public void CanDeleteRation()
        {
            //Act
            var request = new DataSourceRequest();
            var rationViewModel = new RationViewModel() { Amount = 32, CommodityID = 4, Commodity = "Commodity 4", RationID = 4 };
            _rationController.Ration_Destroy(request, rationViewModel);
            var result = _rationController.Ration_Read(request);
            var data = ((JsonResult)result).Data;
            var rationsResult =
                (((DataSourceResult)data).Data).Cast<RationViewModel>();

            //Assert
            Assert.AreEqual(3, rationsResult.Count());

        }
        #endregion
    }
}
