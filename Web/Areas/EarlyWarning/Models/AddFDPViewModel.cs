using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class AddFDPViewModel
    {
        public int RegionalRequestID { get; set; }
        public int RegionalRequestDetailID { get; set; }
        public int FDPID { get; set; }
        public int Beneficiaries { get; set; }
        public int RegionID { get; set; }
        public int ZoneID { get; set; }
        public int WoredaID { get; set; }
       
    }
}