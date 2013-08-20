using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
    public class RegionalSummaryViewModel
    {
        public string RegionName { get; set; }
        public int HRDID { get; set; }
        public long NumberOfBeneficiaries { get; set; }
        public int DurationOfAssistance { get; set; }
        public decimal Cereal { get; set; }
        public decimal BlededFood { get; set; }
        public decimal Pulse { get; set; }
        public decimal Oil { get; set; }
        public decimal Total { get { return Cereal + Pulse + Oil; } }
    }
}
