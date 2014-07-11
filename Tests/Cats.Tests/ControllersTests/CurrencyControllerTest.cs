using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Areas.Settings.Controllers;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;
using Cats.Models;

namespace Cats.Tests.ControllersTests
{  [TestFixture]
   public class CurrencyControllerTest
   {
       private CurrencyController _currencyController;

       #region Setup
       [SetUp]
       public void Init()
       {
           var currency = new List<Currency>
               {
                   new Currency {CurrencyID =1,Code = "USD",Name = "US Dollar"},
                   new Currency {CurrencyID =2,Code = "URO",Name = "EURO"}

               };

           var currencyService = new Mock<ICurrencyService>();
           currencyService.Setup(m => m.GetAllCurrency()).Returns(currency);

           _currencyController=new CurrencyController(currencyService.Object);
       }
       [TearDown]
       public void Dispose()
       {
           _currencyController.Dispose();
       }
       #endregion

       #region Tests
       [Test]
       public void CanShowIndex()
       {
           var result = _currencyController.Index();
           Assert.IsNotNull(result);
       }
       [Test]
       public void CanCreateCurrency()
       {
           var result = _currencyController.Create();
           Assert.IsNotNull(result);

       }
       [Test]
       public void CanReadCUrrency()
       {
           var currency = new DataSourceRequest();

           var result = _currencyController.Currency_Read(currency);
           Assert.IsNotNull(result);
           Assert.IsInstanceOf<JsonResult>(result); 

           //Assert.AreEqual(1, (((DataSourceResult)result.Data).Total));
       }
       [Test]
       public void CanDeleteCurrency()
       {
           var id = 1;
           var result = (RedirectToRouteResult)_currencyController.Delete(id);

           Assert.IsNotNull(result);
           //Assert.IsInstanceOf<ContributionDetail>(result.Model);
       }
       #endregion
   }
}
