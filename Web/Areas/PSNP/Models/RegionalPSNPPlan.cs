using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.PSNP
{
    public class RegionalPSNPPlanViewModel
    {

        //RegionalPSNPPlanID
        [Display(Name = "ID")]
        public int RegionalPSNPPlanID { get; set; }

        //Year
        [Display(Name = "Year")]
        public int Year { get; set; }

        //Duration
        [Display(Name = "Duration")]
        public int Duration { get; set; }

        //Region
        [Display(Name = "Region")]
        public string RegionName { get; set; }

        //RegionID
        [Display(Name = "Region ID")]
        public int RegionID { get; set; }

    }

    public class PSNPPlanDetailView
    {
        [Display(Name = "FDP ID")]
        public int FDPID { get; set; }

        [Display(Name = "FDP")]
        public string FDPName { get; set; }

        [Display(Name = "WoredaID")]
        public int WoredaID { get; set; }

        [Display(Name = "Woreda")]
        public string WoredaName { get; set; }

        [Display(Name = "ZoneID")]
        public int ZoneID { get; set; }

        [Display(Name = "Zone")]
        public string ZoneName { get; set; }

        [Display(Name = "Region ID")]
        public int RegionID { get; set; }

        [Display(Name = "Region")]
        public string RegionName { get; set; }

        //RegionalPSNPPlanDetailID
        [Display(Name = "ID")]
        public int RegionalPSNPPlanDetailID { get; set; }

        //RegionalPSNPPlanID
        [Display(Name = "Plan ID")]
        public int RegionalPSNPPlanID { get; set; }

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
    }
    public class RegionalPSNPPlanDetailViewModel
    {

        //RegionalPSNPPlanDetailID
        [Display(Name = "ID")]
        public int RegionalPSNPPlanDetailID { get; set; }

        //RegionalPSNPPlanID
        [Display(Name = "Plan ID")]
        public int RegionalPSNPPlanID { get; set; }


        //PlanedFDP
        [Display(Name = "FDP")]
        public string PlanedFDPName { get; set; }

        //PlanedFDPID
        [Display(Name = "FDP ID")]
        public int PlanedFDPID { get; set; }


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


    }
}