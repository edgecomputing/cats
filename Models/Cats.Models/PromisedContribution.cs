using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LanguageHelpers.Localization.DataAnnotations;

namespace Cats.Models
{
    public class PromisedContributionPOCO
    {
        [Display(Name = "ID")]
        public int PromisedContributionId { get; set; }

        [Display(Name = "Promised Quantity")]
        public float? PromisedQuantity { get; set; }

        [Display(Name = "Delivered Quantity")]
        public float? DeliveredQuantity { get; set; }

        [Display(Name = "ETA")]
        public System.DateTime ExpectedTimeOfArrival { get; set; }

        [Display(Name = "Donor ID")]
        public int DonorId { get; set; }

        [Display(Name = "Commodity ID")]
        public int CommodityId { get; set; }

        [Display(Name = "Hub ID")]
        public int HubId { get; set; }



    }

    public class PromisedContribution : PromisedContributionPOCO
    {
        public virtual Commodity Commodity { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual Hub Hub { get; set; }

    }
    public class PromisedContributionViewModel : PromisedContributionPOCO
    {
        [Display(Name = "ETA")]
        public string strETA { get; set; }
        public string CommodityName { get; set; }
        public string DonorName { get; set; }
        public string HubName { get; set; }
    }
}


