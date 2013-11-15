using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.EarlyWarning.Models
{
    public class PlanWithHRDViewModel
    {
        public Plan Plan { get; set; }
        public List<HRD> HRDs { get; set; }
        public List<NeedAssessment> NeedAssessments { get; set; }
    }
}