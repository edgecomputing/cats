using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Areas.Logistics.Controllers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.Logistics;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{   
    [TestFixture]
    public class ReciptPlanForLoanControllerTest
    {
        private ReciptPlanForLoanController _reciptPlanForLoanController;

        #region SetUp

        [SetUp]
        public void Init()
        {
            var loanReciptPlan = new List<LoanReciptPlan>
                {
                    new LoanReciptPlan
                        {
                            LoanReciptPlanID = 1,ShippingInstructionID = 1,ProgramID = 1,
                            ProjectCode="WFP-2005",ReferenceNumber = "REF-2001",CommoditySourceID = 1,
                            CommodityID = 1,Quantity = 200,StatusID = 1,
                            ShippingInstruction = new ShippingInstruction()
                                {
                                    ShippingInstructionID = 1,
                                    Value = "123456"
                                }
                        }

                };
            var loanService = new Mock<ILoanReciptPlanService>();
            loanService.Setup(m => m.GetAllLoanReciptPlan()).Returns(loanReciptPlan);
            var loanReciptPlanDetail = new List<LoanReciptPlanDetail>
                {
                    new LoanReciptPlanDetail()
                        {
                            LoanReciptPlanDetailID = 1,
                            LoanReciptPlanID = 2,
                            HubID = 1,
                            ApprovedBy = 1,
                            RecievedDate = new DateTime(12/12/2012),
                            RecievedQuantity = 200,
                            
                        },
                    new LoanReciptPlanDetail
                        {
                            LoanReciptPlanDetailID = 2,
                            LoanReciptPlanID = 2,
                            HubID = 2,
                            ApprovedBy = 2,
                            RecievedDate = new DateTime(12/12/2012),
                            RecievedQuantity = 100
                        }
                };
            var loanDetailService = new Mock<ILoanReciptPlanDetailService>();
            loanDetailService.Setup(m => m.GetAllLoanReciptPlanDetail()).Returns(loanReciptPlanDetail);

             var hub = new List<Hub>
                {
                    new Hub { HubID = 1,Name = "Adama"},
                    new Hub { HubID = 2,Name = "Deradawa"}
                };

            var commonService = new Mock<ICommonService>();
            commonService.Setup(m => m.GetAllHubs()).Returns(hub);

            commonService.Setup(
               t =>
               t.GetCommodities(It.IsAny<Expression<Func<Commodity, bool>>>(),
                                It.IsAny<Func<IQueryable<Commodity>, IOrderedQueryable<Commodity>>>(),
                                It.IsAny<string>())).Returns(new List<Commodity>() { new Commodity() { CommodityID = 1, Name = "CSB" } });

            commonService.Setup(
             t =>
             t.GetCommodityTypes(It.IsAny<Expression<Func<CommodityType, bool>>>(),
                              It.IsAny<Func<IQueryable<CommodityType>, IOrderedQueryable<CommodityType>>>(),
                              It.IsAny<string>())).Returns(new List<CommodityType>() { new CommodityType() { CommodityTypeID = 1, Name = "Food" } });

            commonService.Setup(
             t =>
             t.GetPrograms(It.IsAny<Expression<Func<Program, bool>>>(),
                              It.IsAny<Func<IQueryable<Program>, IOrderedQueryable<Program>>>(),
                              It.IsAny<string>())).Returns(new List<Program>() { new Program() { ProgramID = 1, Name = "PSNP" } });
            commonService.Setup(t=>t.GetStatusName(It.IsAny<WORKFLOW>(), It.IsAny<int>())).Returns("Draft");



            commonService.Setup(
            t =>
            t.GetCommoditySource()).Returns(new List<CommoditySource>() { new CommoditySource() { CommoditySourceID = 1, Name = "Loan" } });

            _reciptPlanForLoanController = new ReciptPlanForLoanController(loanService.Object, commonService.Object, loanDetailService.Object);
        }
        [TearDown]
        public void Dispose()
        {
            _reciptPlanForLoanController.Dispose();
        }
        #endregion

        //[Test]
        //public void CanReadReciptPlan()
        //{
        //    var request = new DataSourceRequest();
        //    var result = _reciptPlanForLoanController.LoanReciptPlan_Read(request);
        //    Assert.IsNotNull(result);
        //}
    }
}
