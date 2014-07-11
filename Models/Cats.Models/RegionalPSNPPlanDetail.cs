using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cats.Models;

namespace Cats.Models
{
    public class RegionalPSNPPlanDetail
    {

        //RegionalPSNPPlanDetailID
        [Display(Name = "ID")]
        public int RegionalPSNPPlanDetailID { get; set; }

        //RegionalPSNPPlan
        [Display(Name = "Plan")]
        public RegionalPSNPPlan RegionalPSNPPlan { get; set; }

        //RegionalPSNPPlanID
        [Display(Name = "Plan ID")]
        public virtual int RegionalPSNPPlanID { get; set; }


        //PlanedFDP
        [Display(Name = "Woreda")]
        public virtual AdminUnit PlanedWoreda { get; set; }

        //PlanedFDPID
        [Display(Name = "Woreda ID")]
        public int  PlanedWoredaID { get; set; }


        //BeneficiaryCount
        [Display(Name = "No of Beneficiary")]
        public int BeneficiaryCount { get; set; }

        //FoodRatio
        [Display(Name = "Food Ratio")]
        public int FoodRatio { get; set; }

        //CashRatio
        [Display(Name = "Cash Ratio")]
        public int CashRatio { get; set; }

        //Item3Ratio
        [Display(Name = "Item3 Ratio")]
        public int Item3Ratio { get; set; }

        //Item4Ratio
        [Display(Name = "Item4 Ratio")]
        public int Item4Ratio { get; set; }

        [Display(Name = "Starting Month")]
        public int StartingMonth { get; set; }

    }
}