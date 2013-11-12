using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class NeedAssessmentPlanViewModel
    {
        public int NeedAssessmentID { get; set; }
        public int PlanID { get; set; }
        public string AssessmentName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Year { get; set; }
    }
}