using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class RegionalRequestDetailViewModel
    {
        public int RegionalRequestDetailID { get; set; }
        public int RegionalRequestID { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public int Fdpid { get; set; }
        public string FDP { get; set; }
        public decimal Grain { get; set; }
        public decimal Pulse { get; set; }
        public decimal Oil { get; set; }
        public decimal CSB { get; set; }
        public int Beneficiaries { get; set; }
    }
}