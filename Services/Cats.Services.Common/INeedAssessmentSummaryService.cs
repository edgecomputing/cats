using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Common
{
    public interface INeedAssessmentSummaryService
    {
        List<NeedAssessmentSummary> NeedAssessmentByRegion(string regionName);

        List<NeedASummary> NeedAssessmentByYear(int year);

        List<int> GetYears();
    }
}