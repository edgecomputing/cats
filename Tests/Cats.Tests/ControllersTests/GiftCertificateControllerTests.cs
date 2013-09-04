using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Areas.GiftCertificate.Models;
using Cats.Models;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Transaction;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class GiftCertificateControllerTests
    {
        #region SetUp / TearDown

        private GiftCertificateController _giftCertificateController;
        [SetUp]
        public void Init()
        {
           
            var giftDetails = new List<GiftCertificateDetail>()
                                  {
                                      new GiftCertificateDetail
                                          {
                                              GiftCertificateID = 1,
                                              AccountNumber = 1,
                                              BillOfLoading = "1",
                                              Detail = new Detail {DetailID = 1, Name = "WFP"},
                                              CommodityID = 1,
                                              Commodity = new Commodity() {CommodityID = 1, Name = "CSB"}
                                          }
                                  };
            ;
            var gifts = new List<GiftCertificate>()
                            {
                                new GiftCertificate()
                                    {GiftCertificateID = 1, ProgramID = 1, ReferenceNo = "1", SINumber = "SI-001",DonorID=1,Donor=new Donor(){DonorID=1,Name="WFP"},GiftCertificateDetails=giftDetails},
                                new GiftCertificate()
                                    {GiftCertificateID = 1, ProgramID = 1, ReferenceNo = "1", SINumber = "SI-001",DonorID=1,Donor=new Donor(){DonorID=1,Name="WFP"},GiftCertificateDetails=giftDetails},
                                new GiftCertificate()
                                    {GiftCertificateID = 1, ProgramID = 1, ReferenceNo = "1", SINumber = "SI-001",DonorID=1,Donor=new Donor(){DonorID=1,Name="WFP"},GiftCertificateDetails=giftDetails}
                            };


            var commonService = new Mock<ICommonService>();
            commonService.Setup(
                t =>
                t.GetCommodities(It.IsAny<Expression<Func<Commodity, bool>>>(),
                                 It.IsAny<Func<IQueryable<Commodity>, IOrderedQueryable<Commodity>>>(),
                                 It.IsAny<string>())).Returns(new List<Commodity>()
                                                                  {new Commodity() {CommodityID = 1, Name = "CSB"}});
            commonService.Setup(
               t =>
               t.GetCommodityTypes(It.IsAny<Expression<Func<CommodityType, bool>>>(),
                                It.IsAny<Func<IQueryable<CommodityType>, IOrderedQueryable<CommodityType>>>(),
                                It.IsAny<string>())).Returns(new List<CommodityType>() { new CommodityType() { CommodityTypeID = 1, Name = "CSB++" } });
            commonService.Setup(
               t =>
               t.GetDetails(It.IsAny<Expression<Func<Detail, bool>>>(),
                                It.IsAny<Func<IQueryable<Detail>, IOrderedQueryable<Detail>>>(),
                                It.IsAny<string>())).Returns(new List<Detail>() { new Detail() { DetailID = 1, Name = "CSB" } });
            commonService.Setup(
               t =>
               t.GetDonors(It.IsAny<Expression<Func<Donor, bool>>>(),
                                It.IsAny<Func<IQueryable<Donor>, IOrderedQueryable<Donor>>>(),
                                It.IsAny<string>())).Returns(new List<Donor>() { new Donor() { DonorID = 1, Name = "WFP" } });
            commonService.Setup(
             t =>
             t.GetPrograms(It.IsAny<Expression<Func<Program, bool>>>(),
                              It.IsAny<Func<IQueryable<Program>, IOrderedQueryable<Program>>>(),
                              It.IsAny<string>())).Returns(new List<Program>() { new Program() { ProgramID = 1, Name = "PSNP" } });

            var giftCertificateService = new Mock<IGiftCertificateService>();
            giftCertificateService.Setup(t => t.IsSINumberNewOrEdit(It.IsAny<string>(), It.IsAny<int>())).Returns(true);
            giftCertificateService.Setup(t => t.Get(It.IsAny<Expression<Func<GiftCertificate,bool>>>(),It.IsAny<Func<IQueryable<GiftCertificate>,IOrderedQueryable<GiftCertificate>>>(),It.IsAny<string>())).Returns(gifts);
            giftCertificateService.Setup(t => t.AddGiftCertificate(It.IsAny<GiftCertificate>())).Returns(true);
            var giftCertificateDetailService = new Mock<IGiftCertificateDetailService>();
            var transactionService = new Mock<ITransactionService>();
            var letterTemplateService = new Mock<ILetterTemplateService>();
            transactionService.Setup(t => t.PostGiftCertificate(It.IsAny<int>())).Returns(true);
            _giftCertificateController = new GiftCertificateController(giftCertificateService.Object, giftCertificateDetailService.Object, commonService.Object, transactionService.Object, letterTemplateService.Object);
        }

        [TearDown]
        public void Dispose()
        { _giftCertificateController.Dispose();}

        #endregion

        #region Tests

        [Test]
        public void ShouldReturnTrueIsSINumberNewOrEdit()
        {
            //ACT
            var result = _giftCertificateController.NotUnique("SI-001", 1);

            //Assert

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsTrue((bool)((JsonResult)result).Data);
        }
        [Test]
        public void ShouldDisplayGiftCertificates()
        {
            //Act
            var result = _giftCertificateController.Index();

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<List<GiftCertificateViewModel>>(((ViewResult)result).Model);
        }

        [Test]
        public void ShouldPrepareGiftCertificateForCreate()
        {
            //Act

            var result = _giftCertificateController.Create();

            //Assert

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<GiftCertificateViewModel>(((ViewResult)result).Model);
        }
        #endregion
    }
}
