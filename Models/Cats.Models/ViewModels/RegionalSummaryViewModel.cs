using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
    public class RegionalSummaryViewModel
    {
        [Display(Name = "Region")]
        public string RegionName { get; set; }
        public int HRDID { get; set; }
        [Display(Name = "Beneficiaries")]
        public long NumberOfBeneficiaries { get; set; }
        public int DurationOfAssistance { get; set; }
        public decimal Cereal { get; set; }
        public decimal BlededFood { get; set; }
        public decimal Pulse { get; set; }
        public decimal Oil { get; set; }
        public decimal Total { get { return Cereal + Pulse + Oil + BlededFood; } }
        public int AdminUnitID { get; set; }
        public string AdminUnitName { get; set; }
    }
    public class CompareHrdViewModel
    {
        public int HrdID { get; set; }
        public string HrdName { get; set; }
        public decimal BeneficiaryNumber { get; set; }
        public string Region { get; set; }
        public decimal Percentage { get; set; }
    }
}
