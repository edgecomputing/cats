using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageHelpers.Localization.DataAnnotations;

namespace Cats.Models.ViewModels.RegionalPSNPPlan
{
    public class RegionalPSNPPlanViewModel
    {
        public int RegionalPSNPPlanID { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public int Region { get; set; }
        public int Ration { get; set; }
        public int Status { get; set; }
    }
}
