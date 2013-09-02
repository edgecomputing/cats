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

    public class NAS
    {

        public NAS() { }

        //public NAS(string region, string belgB, int meherB ) {
        //    this.RegionName = region;
        //    this.Belg_Beneficiaries = belgB;
        //    this.Meher_Beneficiaries = meherB;
        //}

        public string RegionName { get; set; }
        public int Belg_Beneficiaries { get; set; }
        public int Meher_Beneficiaries { get; set; }
    }
}
