using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Areas.Logistics.Controllers;
using Cats.Services.Logistics;
using Moq;
using NUnit.Framework;
namespace Cats.Tests.ControllersTests
{
    

    [TestFixture]
    public class DonationPlanTest
    {
        private DonationController _donationController;
        
#region Setup

    public void Init()
    {
        var donationHeader = new List<DonationPlanHeader>()
                                 {
                                     new DonationPlanHeader()
                                         {
                                             DonationHeaderPlanID = 1,
                                             ShippingInstructionId = 1,
                                             GiftCertificateID = 1,
                                             CommodityID = 1,
                                             DonorID = 1,
                                             ProgramID = 1,
                                             ETA = new DateTime(1/1/2000),
                                             Vessel = null,
                                             ReferenceNo = "001",
                                             ModeOfTransport = 1,
                                             TransactionGroupID = new Guid(),
                                             IsCommited = false,
                                             EnteredBy = 1,
                                             AllocationDate = DateTime.Now,
                                             Remark = "test 1",
                                             Commodity = new Commodity()
                                                             {
                                                                 CommodityID = 1,
                                                                 Name = "Cereal"
                                                             },
                                                             DonationPlanDetails = new Collection<DonationPlanDetail>()
                                                             {
                                                                 new DonationPlanDetail()
                                                                     {
                                                                         DonationDetailPlanID = 2,
                                                                         DonationHeaderPlanID = 1,
                                                                         HubID = 1,
                                                                         AllocatedAmount = 100,
                                                                         ReceivedAmount = 50,
                                                                         Balance = 50,
                                                                         Hub = new Hub()
                                                                                   {
                                                                                       HubID = 1,
                                                                                       Name = "Addis Ababa"
                                                                                   }
                                                                     },
                                                                     new DonationPlanDetail()
                                                                         {
                                                                             DonationDetailPlanID = 3,
                                                                             DonationHeaderPlanID = 1,
                                                                             HubID = 2,
                                                                             AllocatedAmount = 200,
                                                                             ReceivedAmount = 150,
                                                                             Balance = 150,
                                                                             Hub = new Hub()
                                                                                       {
                                                                                           HubID = 2,
                                                                                           Name = "Nathret"
                                                                                       }
                                                                         },
                                                                         new DonationPlanDetail()
                                                                             {
                                                                                 DonationDetailPlanID = 4,
                                                                                 DonationHeaderPlanID = 1,
                                                                                 HubID = 3,
                                                                                 AllocatedAmount = 300,
                                                                                 ReceivedAmount = 250,
                                                                                 Balance = 250,
                                                                                 Hub = new Hub()
                                                                                           {
                                                                                               HubID = 3,
                                                                                               Name = "Kombelcha"
                                                                                           }
                                                                             }
                                                             }
                                                             ,Donor = new Donor()
                                                                          {
                                                                              DonorID = 1,
                                                                              Name = "WPF"
                                                                          },Program = new Program()
                                                                                          {
                                                                                              ProgramID = 1,
                                                                                              Name = "Releif"
                                                                                          },UserProfile = new UserProfile()
                                                                                                              {
                                                                                                                  UserProfileID = 1,
                                                                                                                  UserName = "Dawit"
                                                                                                              }


                                         }
                                 };

        var donationHeaderService = new Mock<IDonationPlanHeaderService>();
        donationHeaderService.Setup(d => d.GetAllDonationPlanHeader()).Returns(donationHeader);

    }




#endregion
    }

}
