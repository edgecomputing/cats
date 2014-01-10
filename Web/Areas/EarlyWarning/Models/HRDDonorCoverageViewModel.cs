using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class HRDDonorCoverageViewModel
    {
        public int HrdDonorCovarageID { get; set; }
        public int DonorID { get; set; }
        public string DonorName { get; set; }   
        public int hrdID { get; set; }
        public string Season { get; set; }
        public int Year { get; set; }
        //public string HRDName { get; set; }
        public string CreatedDate { get; set; }
        public int NoCoveredWoredas { get; set; }

        public string HRDName
        {
            get { return string.Format("{0} {1}", Season, Year); }
        }
    }
    public class HrdDonorCoverageDetailViewModel
    {
        public int HrdDonorCoverageDetailID { get; set; }
        public int HrdDonorCoverageID { get; set; }
        public int WoredaID { get; set; }
        public string Woreda { get; set; }
        public string Zone { get; set; }
        public string Region { get; set; }
    }
    public class AddWoredaViewModel
    {
        public int DonorCoverageID { get; set; }
        public int RegionID { get; set; }
        public int ZoneID { get; set; }
        public int WoredaID { get; set; }
    }
}