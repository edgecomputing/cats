using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    public class ContributionControllerTest
    {
        private readonly ContributionController _contributionController;

#region Setup
        [SetUp]
        public void Init()
        {
            var contribution = new List<Contribution>
                {
                    new Contribution {ContributionID = 1,HRDID = 1,DonorID = 1,Year = 2012},
                    new Contribution {ContributionID = 2,HRDID = 2,DonorID = 1,Year = 2012}

                };


            var contributionService = new Mock<IContributionService>();
            contributionService.Setup(m => m.GetAllContribution()).Returns(contribution);


            var contributionDetail = new List<ContributionDetail>()
                {
                    new ContributionDetail
                        {
                            ContributionDetailID = 1,
                            ContributiionID = 1,
                            CommodityID = 1,
                            PledgeDate = new DateTime(12/12/12),
                            PledgeReferenceNo = "ADE123",
                            Quantity = 100
                        },

                         new ContributionDetail
                        {
                            ContributionDetailID = 2,
                            ContributiionID = 1,
                            CommodityID = 2,
                            PledgeDate = new DateTime(12/12/12),
                            PledgeReferenceNo = "ADE123",
                            Quantity = 100
                        }
                };

            var contributionDetailService = new Mock<IContributionDetailService>();
            contributionDetailService.Setup(m => m.GetAllContributionDetail()).Returns(contributionDetail);
        }
#endregion


    }
}
