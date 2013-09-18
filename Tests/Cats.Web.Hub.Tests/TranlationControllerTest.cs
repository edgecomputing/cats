using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DRMFSS.Web.Controllers.Utilities;
using Moq;
using NUnit.Framework;
using DRMFSS.BLL.Services;
using DRMFSS.Web.Controllers;
using DRMFSS.BLL;
namespace DRMFSS.Web.Test
{
    [TestFixture]
    public class TranlationControllerTest
    {
        #region SetUp / TearDown

        private TranslationController _translationController;

        [SetUp]
        public void Init()
        {
            var tranlations = new List<Translation>
                                  {
                                      new Translation{LanguageCode = "LOO1",Phrase = "PHRASE ONE",TranslatedText = "PHRASE TRANSLATED ONE",TranslationID = 0},
                                      new Translation{LanguageCode = "L002",Phrase = "PHRASE TWO",TranslatedText = "PHRASE TRANSLATED TWO",TranslationID = 1}
                                  };

            var translationservice = new Mock<ITranslationService>();
            _translationController = new TranslationController(translationservice.Object);
        }
       
        [TearDown]
        public void Dispose()
        {
            _translationController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //Act
            var result = _translationController.Index() as ViewResult;


            //Assert
            Assert.NotNull(result);

            var model = result.Model;
            Assert.IsInstanceOf<Translation>(model);

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<List<Translation>>(((ViewResult) result).Model);
            Assert.AreEqual(2, ((IEnumerable<Translation>) model).Count());

        }

        [Test]
        public void CanViewEdit()
        {
            //Act
            var result = _translationController.Edit(1);
            var model = ((ViewResult)result).Model;

            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<Translation>(model);
        }

        [Test]
        public void CanViewCreate()
        {
            //Act
            var translation = new Translation
                                  {
                                      LanguageCode = "001",
                                      Phrase = "phrase to be translated",
                                      TranslatedText = "translated"
                                  };
            var result = _translationController.Save(translation);

            //Assert
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsInstanceOf<int>(translation.TranslationID);
        }
        #endregion

    }
}
