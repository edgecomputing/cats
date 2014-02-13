using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Cats.Models;

namespace Cats.Areas.GiftCertificate.Models
{
    public class GiftCertificateDetailsViewModel
    {
        //[Required(ErrorMessage = "Gift Certificate Detail is required")]
        public Int32 GiftCertificateDetailID { get; set; }

        //[Required(ErrorMessage = "Transaction Group is required")]
        public Int32 TransactionGroupID { get; set; }

        [Required(ErrorMessage = "Gift Certificate is required")]
        public Int32 GiftCertificateID { get; set; }

        [Required(ErrorMessage = "Commodity is required")]
        public Int32 CommodityID { get; set; }

        //public Decimal GrossWeightInMT { get; set; }

        [Required(ErrorMessage = "Weight In MT is required")]
        [Range(0.5, 999999.99)]
        public Decimal WeightInMT { get; set; }

        private string _billOfLoading;

        [StringLength(50)]
        [UIHint("WayBillWarning")]
        [Display(Name = "Bill of Loading")]
        public String BillOfLoading
        {
            get { return _billOfLoading; }
            set { _billOfLoading = value; }
        }

        [Required(ErrorMessage = "Account Number is required")]
        [Display(Name = "Acc. No")]
        public Int32 AccountNumber { get; set; }

        [Required(ErrorMessage = "Estimated Price is required")]
        [Range(0, 9999999999999.99)]
        [Display(Name="Est. Price")]
        public Decimal EstimatedPrice { get; set; }

        [Required(ErrorMessage = "Estimated Tax is required")]
        [Range(0, 9999999999999.99)]
        public Decimal EstimatedTax { get; set; }

        [Required(ErrorMessage = "Year Purchased is required")]
        [Range(2000, 3000)]
        [Display(Name="Year Purchased")]
        public Int32 YearPurchased { get; set; }

        [Required(ErrorMessage = "Fund Source is required")]
        public Int32 DFundSourceID { get; set; }

        [Required(ErrorMessage = "Currency is required")]
        public Int32 DCurrencyID { get; set; }

        [Required(ErrorMessage = "Budget Type is required")]
        public Int32 DBudgetTypeID { get; set; }

    
        [Display(Name="Expiry Date")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name="Commodiy")]
        public string CommodityName { get; set; }
        public string FundSource { get; set; }
        public GiftCertificateDetailsViewModel()
        {
            this.YearPurchased = DateTime.Now.Year;
            this.DBudgetTypeID = 9;
            this.DFundSourceID = 5;
            this.DCurrencyID = 1;
            this.BillOfLoading = "";
        }
    }
}
