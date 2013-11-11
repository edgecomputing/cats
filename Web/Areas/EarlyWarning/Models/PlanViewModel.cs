using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class PlanViewModel
    {
        public int planID { get; set; }
        public string planName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Program { get; set; }
        public int ProgramID { get; set; }
    }
}