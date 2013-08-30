using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class Beneficiaries
    {
        public string RegionName { get; set; }
        public int BeneficiariesCount { get; set; }
    }
    public class Request
    {
        public string RegionName { get; set; }
        public int RequestsCount { get; set; }
    }

    public class RegionalBeneficiaries
    {
        public string RegionName { get; set; }
        public decimal Request { get; set; }
        public decimal Allocation { get; set; }
        public decimal HRD { get; set; }
    }

    public class ZonalBeneficiaries
    {
        public string Month { get; set; }
        public string Zone { get; set; }
        public decimal Request { get; set; }
        public decimal Allocation { get; set; }
        public decimal HRD { get; set; }

    }
}
