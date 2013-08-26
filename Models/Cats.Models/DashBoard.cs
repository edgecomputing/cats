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
}
