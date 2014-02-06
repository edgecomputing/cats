using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface IDonationPlanDetailService
    {

        bool AddDonationPlanDetail(DonationPlanDetail donationPlanDetail);
        bool DeleteDonationPlanDetail(DonationPlanDetail donationPlanDetail);
        bool DeleteById(int id);
        bool EditDonationPlanDetail(DonationPlanDetail donationPlanDetail);
        DonationPlanDetail FindById(int id);
        List<DonationPlanDetail> GetAllDonationPlanDetail();
        List<DonationPlanDetail> FindBy(Expression<Func<DonationPlanDetail, bool>> predicate);


    }
}


