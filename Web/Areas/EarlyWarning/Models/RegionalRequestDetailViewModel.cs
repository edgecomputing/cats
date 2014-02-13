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
        public int WoredaId { get; set; }
        public int Beneficiaries { get; set; }
        public int PlannedBeneficiaries { get; set; }
    }
}