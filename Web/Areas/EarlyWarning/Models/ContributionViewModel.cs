using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class ContributionViewModel
    {
        public int ContributionID { get; set; }
        public int Year { get; set; }
        public string HRD { get; set; }
        public string Donor { get; set; }

    }
}